using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace EF_project
{
    class Program
    {
        static Entities.UserEntity CreateUser(string userName, string email, ApplicationContext db)
        {
                if (db.Users.Any(u => u.NickName == userName))
                    throw new ArgumentException($"Error: User(NickName='{userName}') already exists!");

                Entities.UserEntity newUser = new() { NickName = userName, Email = email };
                db.Users.Add(newUser);
                db.SaveChanges();

                return newUser;
        }

        static Entities.ChatEntity CreateChat(string chatName, ApplicationContext db)
        {
            if (db.Chats.Any(c => c.Name == chatName))
                throw new ArgumentException($"Error: Chat(Name='{chatName}') already exists!");

            Entities.ChatEntity newChat = new() { Name = chatName, DateCreated = DateTime.Now };
            db.Chats.Add(newChat);
            db.SaveChanges();

            return newChat;
        }

        static void WriteMessage(Entities.ChatEntity chat, Entities.UserEntity user, string message, ApplicationContext db)
        {
            if (!db.Chats.Contains(chat))
                throw new ArgumentException($"Error: could not found Chat(Name='{chat.Name}')!");

            if (!db.Users.Contains(user))
                throw new ArgumentException($"Error: could not found User(NickName='{user.NickName}')!");

            if (!user.UserChats.Contains(chat))
                throw new ArgumentException($"Error: User(NickName='{user.NickName}') must be participant of Chat(Name='{chat.Name}')!");

            Entities.MessageEntity msg = new() { Author = user, Contents = message, DateSent = DateTime.Now, Chat = chat };
            chat.Messages.Add(msg);
            db.Messages.Add(msg);
            db.SaveChanges();
        }

        static void AddUserToChat(Entities.UserEntity user, Entities.ChatEntity chat, ApplicationContext db)
        {
            if (!db.Chats.Contains(chat))
                throw new ArgumentException($"Error: could not found Chat(Name='{chat.Name}')!");

            if (!db.Users.Contains(user))
                throw new ArgumentException($"Error: could not found User(NickName='{user.NickName}')!");

            //if (chat.Participants.Contains(user))
            if (user.UserChats.Contains(chat))
                throw new ArgumentException($"Error: User(NickName='{user.NickName}') is already participant of Chat(Name='{chat.Name}')!");

            user.UserChats.Add(chat);
            chat.Participants.Add(user);
            db.SaveChanges();
        }

        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext(needToDelete: true))
            {
                var vasya = CreateUser("Vasya", "vasya.ivanov@mail.ru", db);
                var petya = CreateUser("Petya", "petr@ya.ru", db);
                var masha = CreateUser("Masha", "maria@gmail.com", db);
                var grisha = CreateUser("Grisha", "grisha.perelman@ya.ru", db);

                var friendsChat = CreateChat("Friends Chat", db);
                AddUserToChat(vasya, friendsChat, db);
                AddUserToChat(petya, friendsChat, db);
                AddUserToChat(masha, friendsChat, db);

                WriteMessage(friendsChat, vasya, "всем привет!", db);
                Thread.Sleep(1500);
                WriteMessage(friendsChat, petya, "Здарова =)", db);

                var mathChat = CreateChat("Math chat!", db);
                AddUserToChat(vasya, mathChat, db);
                AddUserToChat(grisha, mathChat, db);

                WriteMessage(mathChat, vasya, "Я придумал два простых числа и два других простых числа", db);
                Thread.Sleep(500);
                WriteMessage(mathChat, vasya, "перемножил первые два, перемножил вторые два, ...", db);
                Thread.Sleep(500);
                WriteMessage(mathChat, grisha, "ии?", db);
                Thread.Sleep(500);
                WriteMessage(mathChat, vasya, "получилось одно и то же!", db);
                Thread.Sleep(500);
                WriteMessage(mathChat, grisha, "во даёт...", db);
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var chats = db.Chats.Include(c => c.Participants).Include(c => c.Messages).ThenInclude(m => m.Author);
                foreach (var chat in chats)
                {
                    Console.WriteLine($"=== Чат: {chat.Name} ===");
                    string participantsList = String.Join(", ", chat.Participants.Select(u => u.NickName));
                    Console.WriteLine($"Собеседники: {participantsList}");
                    Console.WriteLine("Беседа:");
                    foreach (var msg in chat.Messages.OrderBy(m => m.DateSent))
                    {
                        Console.WriteLine($"[{msg.Author.NickName}] ({msg.DateSent}): {msg.Contents}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
