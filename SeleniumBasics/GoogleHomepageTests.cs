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
                // You can play with the browser window, not just the contents of webpages!
                webDriver.Manage().Window.Maximize();

                // This will take us to our start page
                webDriver.Url = "http://google.com";

                // Ideally, you want an easy way of identifying elements on a page. In a perfect world your UI developer will put IDs on everything
                // and you can use By.Id() to find things. Choose the most reliable query you can which generally happens in this order:
                // ID > Name > XPath query
                var searchBox = webDriver.FindElement(By.Name("q"));

                // We can assert on the element once we have found it. Using a fluent assertions library such as fluentassertions will
                // make things easier when we come to upgrade this to a BDD automation.
                searchBox.Displayed.Should().BeTrue();

                searchBox.SendKeys("is UI testing worth it?");

                var submitButton = webDriver.FindElement(By.Name("btnK"));
                submitButton.Click();

                // Here we see an example of using an XPath query to find an element. I would never use this query in an actual application
                // as it is likely to be brittle (google's page order could change, the page title could change or a multitude of other things)
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
