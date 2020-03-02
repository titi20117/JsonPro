using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            decimal salary = 0;
            try
            {
                salary = Decimal.Parse(employeeSalary);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse'{0}'.", employeeSalary);
            }
            int employeeId = SetIdNewEmployee();

            var newEmployeeEntreprise = "{ 'Id': " + employeeId + ", 'FirstName': '" + employeeFirstName + "', 'LastName': '" + employeeLastName
                + "', 'Salary': " + salary + "}";

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
                Console.WriteLine("Новый сотрудник добавлен");
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
            var json = File.ReadAllText(DataConfig.GetFileJson());
            try
            {
                var jObject = JObject.Parse(json);
                JArray employeeArray = (JArray)jObject["employee"];
                Console.Write("Введите ID Для удаления сотрудника : ");
                var employeeId = Convert.ToInt32(Console.ReadLine());

                if (employeeId > 0)
                {
                    var employeeToDeleted = employeeArray.FirstOrDefault(obj => obj["Id"].Value<int>() == employeeId);
                    if (employeeToDeleted == null)
                    {
                        Console.WriteLine("Такой Ид не существует");
                    }
                    else
                    {
                        employeeArray.Remove(employeeToDeleted);

                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(DataConfig.GetFileJson(), output);
                        Console.WriteLine("Сотрудник удален !");
                    }
                }
                else
                {
                    Console.Write("Ид не существует !, попробуйте еще раз");
                    DeleteEmployee();
                }
            }
            catch (Exception)
            {

                throw;
            }
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

        public JArray GetAllEmploys()
        {
            var json = File.ReadAllText(DataConfig.GetFileJson());
         
                var jObject = JObject.Parse(json);
                JArray employees = (JArray)jObject["employee"];
             
                return employees;
        
        }


        public void GetEmployee()
        {
            var json = File.ReadAllText(DataConfig.GetFileJson());
            try
            {
                var jObject = JObject.Parse(json);
                JArray employeeArray = (JArray)jObject["employee"];
                Console.Write("Введите ID Для сотрудника : ");
                var employeeId = Convert.ToInt32(Console.ReadLine());

                if (employeeId > 0)
                {
                    var employeeArr = employeeArray.FirstOrDefault(obj => obj["Id"].Value<int>() == employeeId);
                    if (employeeArr == null)
                    {
                        Console.WriteLine("Такой Ид не существует");
                    }
                    else
                    {
                        Console.WriteLine("Сотрудник Ид : " + employeeArr["Id"]);
                        Console.WriteLine("Сотрудник Имя : " + employeeArr["FirstName"]);
                        Console.WriteLine("Сотрудник Фамилия : " + employeeArr["LastName"]);
                        Console.WriteLine("Сотрудник Оплата : " + employeeArr["Salary"] + "\n");
                    }
                }
                else
                {
                    Console.Write("невалидный Ид, попробуйте еще раз");
                    GetEmployee();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateEmployee()
        {
            string json = File.ReadAllText(DataConfig.GetFileJson());

            try
            {
                var jObject = JObject.Parse(json);
                JArray employeeArray = (JArray)jObject["employee"];
                Console.Write("Введите Ид Сотрудника для изменения : ");
                int employeeId = Convert.ToInt32(Console.ReadLine());

                if (employeeId > 0)
                {
                    var employeeArr = employeeArray.Where(obj => obj["Id"].Value<int>() == employeeId).ToList();
                    if (employeeArr.Count == 0)
                    {
                        Console.WriteLine("Такой Ид не существует");
                    }
                    else
                    {
                        Console.Write("Введите новый имя сотрудник : ");
                        string employeeFirstName = Convert.ToString(Console.ReadLine());
                        foreach (var item in employeeArr)
                        {
                            item["FirstName"] = !string.IsNullOrEmpty(employeeFirstName) ? employeeFirstName : "";
                        }

                        jObject["employee"] = employeeArray;
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText(DataConfig.GetFileJson(), output);
                        Console.WriteLine("Сотрудник обновлен !");
                    }
                }
                else
                {
                    Console.WriteLine("Невалидный Ид, попробуйте еще раз");
                    UpdateEmployee();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update error : {0}", ex.Message.ToString());
            }
        }
    }
}
