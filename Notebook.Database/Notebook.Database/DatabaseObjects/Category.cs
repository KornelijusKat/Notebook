using System;
using System.Collections.Generic;


namespace Notebook.Database.DatabaseRecords
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Record> RecordList { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            RecordList = new List<Record>();
           
        }
    }
}
