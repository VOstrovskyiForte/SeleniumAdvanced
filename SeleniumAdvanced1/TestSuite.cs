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
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void MainTest()
        {


            driver.Navigate().GoToUrl("http://www.leafground.com/home.html");
            By hyperlinkText = By.LinkText("HyperLink");
            //Open Hyperlink in new tab
            var actions = new Actions(driver);
            actions.KeyDown(Keys.Control).Click(driver.FindElement(hyperlinkText)).Perform();
            actions.Release().Perform();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            //Hover Go to Home Page
            var goToHomePageLink = driver.FindElement(By.LinkText("Go to Home Page"));
            new Actions(driver).MoveToElement(goToHomePageLink).Perform();
            //Make screenshot, will be saved to Debug in folder with current datetime
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
            //go to frame, dragAndDrop
            var defaultFrame = driver.FindElement(By.CssSelector("iframe[src='/resources/demos/droppable/default.html']"));
            driver.SwitchTo().Frame(defaultFrame);
            var smallBoxElement = driver.FindElement(By.Id("draggable"));
            var bigBoxElement = driver.FindElement(By.Id("droppable"));
            new Actions(driver).DragAndDrop(smallBoxElement, bigBoxElement).Perform();
            string bigBoxText = driver.FindElement(By.XPath("//div[@id='droppable']/p")).Text;
            Assert.That(bigBoxText, Is.EqualTo("Dropped!"));
        }


        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
