using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.DataLayer
{
    public interface IDataProvider
    {
        IEnumerable<Employee> GetAllEmployees();

        IEnumerable<Meeting> GetAllEmployeeMeetings(int employeeId);
    }
}
