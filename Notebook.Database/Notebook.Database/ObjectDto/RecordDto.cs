using System;


namespace Notebook.Database.ObjectDto
{
   public class RecordDto
    {
        public string Name { get; set; }
        public string Information { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid UserId { get; set; }

        public RecordDto(string name, string information, Guid? categoryId, Guid userId)
        {
            Name = name;
            Information = information;
            CategoryId = categoryId;
            UserId = userId;
        }
        public RecordDto()
        {

        }
    }
}
