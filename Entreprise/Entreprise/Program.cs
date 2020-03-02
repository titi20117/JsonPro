using JsonProvider;
using System;

namespace Entreprise
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeDAO operation = new EmployeeDAO();
            Console.WriteLine("Выберите вариант : \n1 - Add\n2 - Update\n3- Select Employee\n4 - Delete Employee\n5 - Select All Employees");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    operation.AddEmployee();
                    break;
                case "2":
                    operation.UpdateEmployee();
                    break;
                case "3":
                    operation.GetEmployee();
                    break;
                case "4":
                    operation.DeleteEmployee();
                    break;
                case "5":
                    operation.GetAllEmployees();
                    break;
                default:
                    break;
            }
            Console.ReadKey();
        }
    }
}
