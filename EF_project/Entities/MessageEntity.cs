using System;

namespace EF_project.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public DateTime DateSent { get; set; }
        public string Contents { get; set; }
        public UserEntity Author { get; set; }
        public ChatEntity Chat { get; set; }
    }
}
