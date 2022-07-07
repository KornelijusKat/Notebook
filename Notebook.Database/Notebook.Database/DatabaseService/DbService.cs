using Microsoft.EntityFrameworkCore;
using Notebook.Database.DatabaseRecords;
using Notebook.Database.ObjectDto;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Notebook.Database.DatabaseService
{
   public class DbService : IDbService
    {
        private readonly NoteContext _context;
        public DbService(NoteContext context)
        {
            _context = context;
        }
        public void AddCategory(string name,Guid userid)
        {
            var newCategory = new Category(name);
            newCategory.UserId = userid;
            _context.Categories.Add(newCategory);
            _context.SaveChanges();        
        }
        public void EditCategory(Guid id,string nextName)
        {
            var editObj = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (editObj != null)
            {
                editObj.Name = nextName;
                _context.SaveChanges();
            }
        }
        public void AddRecord(RecordDto record, byte[] image)
        {
            var newRecord = new Record(record.Name,image,record.Information,record.UserId);
            newRecord.CategoryId = record.CategoryId;
            _context.Records.Add(newRecord);
            _context.SaveChanges();
        }
        public void ChangeRecordCategory(Guid Id, RecordDto newData)
        {
            var editObj = _context.Records.FirstOrDefault(x => x.Id == Id);
            if (editObj != null)
            {
                editObj.Name = newData.Name;
                editObj.CategoryId = newData.CategoryId;
                _context.SaveChanges();
            }
        }
        public ICollection<Record> ShowRecordsByCategory(Guid categoryId)
        {
            var category = _context.Categories.Include(d=>d.RecordList).FirstOrDefault(x => x.Id == categoryId);
            if(category == null)
            {
                return null;
            }
            return category.RecordList;
        }
        public Record SearchRecordByID(Guid id)
        {
            var record = _context.Records.FirstOrDefault(x => x.Id == id);
            if(record == null)
            {
                return null;
            }
            return record;
        }
        public void RemoveRecord(Guid id)
        {
            var recordToDelete = _context.Records.FirstOrDefault(x => x.Id == id);
            if (recordToDelete != null)
            {
                _context.Records.Remove(recordToDelete);
                _context.SaveChanges();
            }
        }
        public void DeleteCategory(Guid id)
        {
            var deleteObj = _context.Categories.Include(d=>d.RecordList).FirstOrDefault(x => x.Id == id);
            if( deleteObj != null)
            {
                _context.Categories.Remove(deleteObj);
                foreach (var record in deleteObj.RecordList)
                {
                    record.CategoryId = null;
                }
                _context.SaveChanges();
            }
        }
    }
}
