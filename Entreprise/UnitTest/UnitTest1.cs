using JsonProvider;
using System;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllEmployees()
        {
            var actuals = new EmployeeDAO().GetAllEmploys();
            Assert.True(actuals.Count > 0);
            
        }
    }
}
