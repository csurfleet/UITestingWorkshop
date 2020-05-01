using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace SeleniumBasics
{
    public class GoogleHomepageTests
    {
        [Fact]
        public void Google_Exists()
        {
            // We create a chrome browser but you could use any driver here - notice the IWebDriver target type
            IWebDriver webDriver = new ChromeDriver();

            // IMMEDIATELY after creating the driver we start a try-finally, with the webDriver.Quit() in the finally. This
            // ensures that even if a test fails, the browser window is closed. This is a common gotcha, especially once running your
            // tests in a remote locaton - you won't be able to see your agent's desktop filling up with Chrome windows, but you will notice
            // when it runs out of RAM and crashes ;)
            try
            {
                webDriver.Manage().Window.Maximize();
                webDriver.Url = "http://google.com";

                var searchBox = webDriver.FindElement(By.Name("q"));

                searchBox.Displayed.Should().BeTrue();

                searchBox.SendKeys("is UI testing worth it?");

                var submitButton = webDriver.FindElement(By.Name("btnK"));
                submitButton.Click();

                var result = webDriver.FindElement(By.XPath("//h3[text()='Automated Testing on UI: Is it Really Worth the Effort? - DZone ...']"));
                result.Displayed.Should().BeTrue();
            }
            finally
            {
                webDriver.Quit();
            }

        }
    }
}
