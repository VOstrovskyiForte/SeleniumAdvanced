using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvanced1
{
    class TestSuite
    {

        public IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void MainTest()
        {


            driver.Navigate().GoToUrl("http://www.leafground.com/home.html");
            By hyperlinkText = By.LinkText("HyperLink");
            //Open Hyperlink in new tab
            new Actions(driver).KeyDown(Keys.Control).Click(driver.FindElement(hyperlinkText)).Perform();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            //Hover Go to Home Page
            var goToHomePageLink = driver.FindElement(By.LinkText("Go to Home Page"));
            new Actions(driver).MoveToElement(goToHomePageLink).Perform();
            //Make screenshot
            var screenshotDestinationPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "ScreenshotFolder", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            Directory.CreateDirectory(screenshotDestinationPath);
            var screenshotFilePath = Path.Combine(@screenshotDestinationPath, "screenshot.png");
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(screenshotFilePath);
            //Close second tab
            driver.Close();
            //Switch to first tab
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            // open website
            driver.Navigate().GoToUrl("https://jqueryui.com/demos/");
            //open Droppable page
            var droppableLink = driver.FindElement(By.LinkText("Droppable"));
            droppableLink.Click();

        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
