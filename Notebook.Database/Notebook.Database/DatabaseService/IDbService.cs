using Notebook.Database.DatabaseRecords;
using Notebook.Database.ObjectDto;
using System;
using System.Collections.Generic;


namespace Notebook.Database.DatabaseService
{
    public interface IDbService
    {
        void AddCategory(string name,Guid userid);
        void AddRecord(RecordDto record, byte[] image);
        void EditCategory(Guid id, string nextName);
        void ChangeRecordCategory(Guid Id, RecordDto newData);
        ICollection<Record> ShowRecordsByCategory(Guid id);
        Record SearchRecordByID(Guid id);
        void RemoveRecord(Guid id);
        void DeleteCategory(Guid id);
    }
}
