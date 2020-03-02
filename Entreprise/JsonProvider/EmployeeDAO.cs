using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JsonProvider
{
    public class EmployeeDAO : IEmployeeDAO
    {
        public void AddEmployee()
        {
            Console.Write("\n Введите сотрудник FirstName : ");
            string employeeFirstName = Console.ReadLine();
            Console.Write("\n Введите сотрудник LastName : ");
            string employeeLastName = Console.ReadLine();
            Console.Write("\n Введите сотрудник Salary : ");
            string employeeSalary = Console.ReadLine();
            int employeeId = SetIdNewEmployee();

            var newEmployeeEntreprise = "{ 'Id': " + employeeId + ", 'FirstName': " + employeeFirstName + ", 'LastName': " + employeeLastName
                + ", 'Salary': " + employeeSalary + "'}";

            try
            {
                var json = File.ReadAllText(DataConfig.GetFileJson());
                var jsonObj = JObject.Parse(json);
                var employeeArray = jsonObj.GetValue("employee") as JArray;
                var newEmployee = JObject.Parse(newEmployeeEntreprise);
                employeeArray.Add(newEmployee);

                jsonObj["employee"] = employeeArray;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(DataConfig.GetFileJson(), newJsonResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("добавить ощибку : " + ex.Message.ToString());
            }
        }

        private int SetIdNewEmployee()
        {
            var result = 0;
            var json = File.ReadAllText(DataConfig.GetFileJson());
            var jObject = JObject.Parse(json);
            if(jObject != null)
            {
                JArray employeeArray = (JArray)jObject["employee"];
                if(employeeArray != null)
                {
                    foreach (var item in employeeArray)
                    {
                        if (result < item["Id"].ToObject<int>())
                            result = item["Id"].ToObject<int>();
                    }
                }
            }
            return result + 1;
        }

        public void DeleteEmployee()
        {
            throw new NotImplementedException();
        }

        public void GetAllEmployees()
        {
            var json = File.ReadAllText(DataConfig.GetFileJson());
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray employees = (JArray)jObject["employee"];
                    if(employees != null)
                    {
                        foreach (var item in employees)
                        {
                            Console.WriteLine("Сотрудник Ид : " + item["Id"]);
                            Console.WriteLine("Сотрудник Имя : " + item["FirstName"]);
                            Console.WriteLine("Сотрудник Фамилия : " + item["LastName"]);
                            Console.WriteLine("Сотрудник Оплата : " + item["Salary"]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetEmployee()
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee()
        {
            throw new NotImplementedException();
        }
    }
}
