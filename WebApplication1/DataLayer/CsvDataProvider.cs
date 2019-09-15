using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApplication1.Entities;

namespace WebApplication1.DataLayer
{
    public class CsvDataProvider : IDataProvider
    {
        private readonly string _path;
        private Dictionary<string, List<string[]>> TablesWithRecordsDictionary { get; set; }

        public CsvDataProvider(string path)
        {
            _path = path;
            UpdateTablesFromCsv();
        }

        private void UpdateTablesFromCsv()
        {
            string file = File.ReadAllText(_path);
            var tables = file.Split("\r\n@", StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Split("\r\n").ToList())
                .Select(t => t.Select(record => record.Split(",", StringSplitOptions.RemoveEmptyEntries)).ToList())
                .ToList();
            var tablesWithRecordsDictionary = new Dictionary<string, List<string[]>>();
            foreach (var table in tables)
            {
                var tableKey = table[0][0];
                table.RemoveRange(0, 2);
                var tableRecords = table;
                tablesWithRecordsDictionary.Add(tableKey, tableRecords);
            }

            TablesWithRecordsDictionary = tablesWithRecordsDictionary;
        }
        public IEnumerable<Meeting> GetAllEmployeeMeetings(int employeeId)
        {
            var records = TablesWithRecordsDictionary["EmployeesMeetings"];

            var meetingIds = records
                .Where(em => em[0] == employeeId.ToString())
                .Select(em => int.Parse(em[1]));

            var meetings = TablesWithRecordsDictionary["Meetings"]
                .Where(m =>
                {
                    var mId = int.Parse(m[0]);
                    return meetingIds.Contains(mId);
                })
                .Select(m => new Meeting { Id = int.Parse(m[0]), Title = m[1] });






            return meetings;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var records = TablesWithRecordsDictionary["Employees"];

            var employees = records.Select(r => new Employee { Id = int.Parse(r[0]), FullName = r[1] });
      
            return employees;
        }
    }
}
