using System;
using System.Collections.Generic;


namespace Notebook.Database.DatabaseRecords
{
   public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public byte[] PasswordSalt{ get; set; }
        public byte[] PasswordHash { get; set; }
        public virtual ICollection<Record> RecordList { get; set; }
        public virtual ICollection<Category> CategoryList { get; set; }

     
    }
}
