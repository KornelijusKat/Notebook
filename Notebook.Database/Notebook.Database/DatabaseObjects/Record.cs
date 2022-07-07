using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notebook.Database.DatabaseRecords
{
   public class Record
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Information { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Record(string name, byte[] image, string information, Guid userId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Image = image;
            Information = information;
            UserId = userId;
        }
    }
}
