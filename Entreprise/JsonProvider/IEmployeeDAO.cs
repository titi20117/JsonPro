using System;
using System.Collections.Generic;
using System.Text;

namespace JsonProvider
{
    interface IEmployeeDAO
    {
        void AddEmployee();
        void UpdateEmployee();
        void GetEmployee();
        void DeleteEmployee();
        void GetAllEmployees();
    }
}
