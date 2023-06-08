using BulkyBook.Tests.UI.StepDefinitions;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Tests.UI.Context
{
    public class ScenarioContext
    {
        public ILoggerFactory loggerFactory { get; set; }
        public ILogger<BaseSteps> logger { get; set; }
        public IWebDriver driver { get; set; }
        public string baseUrl { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
