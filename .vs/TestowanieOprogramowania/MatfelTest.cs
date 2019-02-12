using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestowanieOprogramowania
{
    [TestClass]
    public class MatfelTest
    {
        [TestMethod]
        public void Searching_Products()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");


                var searchBox = driver.FindElementById("menu_search_text");
                searchBox.SendKeys("Programowanie");
                searchBox.SendKeys(Keys.Enter);
                Thread.Sleep(3000);
                Assert.IsTrue(driver.PageSource.Contains("Wyniki wyszukiwania"));
                

            }

        }

        [TestMethod]
        public void Test_LoggingIn()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                var loginLink = driver.FindElementByLinkText("Zaloguj się");
                loginLink.Click();

                var usernameInput = driver.FindElementByName("login");
                usernameInput.SendKeys("usernamelogin1");

                var usernamePass = driver.FindElementByName("password");
                usernamePass.SendKeys("userpasswd");

                var loginButton = driver.FindElementByCssSelector("button[class='btn signin_button']");
                loginButton.Click();

                Assert.IsTrue(driver.PageSource.Contains("Podany login lub hasło nie jest poprawne."));
            }

        }

        [TestMethod]
        public void Test_Registering()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                var loginLink = driver.FindElementByLinkText("Zarejestruj się");
                loginLink.Click();

                var nameInput = driver.FindElementById("client_firstname_copy");
                nameInput.SendKeys("username");
                var surnameInput = driver.FindElementById("client_lastname_copy");
                surnameInput.SendKeys("username");
                var streetInput = driver.FindElementById("client_street");
                streetInput.SendKeys("street 5");
                var zipInput = driver.FindElementById("client_zipcode");
                zipInput.SendKeys("00-000");
                var cityInput = driver.FindElementById("client_city");
                cityInput.SendKeys("City");
                var emailInput = driver.FindElementById("client_email");
                emailInput.SendKeys("maild33444s2@mail.com");
                var phoneInput = driver.FindElementById("client_phone");
                phoneInput.SendKeys("000000000");
                var loginInput = driver.FindElementById("client_login");
                loginInput.SendKeys("Logind2444s3");
                var passwdInput = driver.FindElementById("client_password");
                passwdInput.SendKeys("password");
                var passwd2Input = driver.FindElementById("repeat_password");
                passwd2Input.SendKeys("password");
                var agreeInput = driver.FindElementById("terms_agree");
                agreeInput.Click();

                var policyButton=driver.FindElementById("ckdsclmrshtdwn");
                policyButton.Click();

                var registerButton = driver.FindElementById("submit_register");
                registerButton.Click();

    
                Thread.Sleep(5000);
                Assert.IsTrue(driver.PageSource.Contains("Cieszymy się z twoich ponownych odwiedzin"));

            }

        }


        [TestMethod]
        public void Test_AddingToBasket()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                var bookType= driver.FindElementByCssSelector("a[title='Atlasy']");
                bookType.Click();
                var bookTypeConcrete = driver.FindElementByCssSelector("a[title='Historyczne']");
                bookTypeConcrete.Click();

                if(driver.PageSource.Contains("Ilość produktów"))
                {
                    var chosenBook = driver.FindElementByCssSelector("a[title='Atlas Historii Świata']");
                    chosenBook.Click();

                    var addBook = driver.FindElementById("projector_button_basket");
                    chosenBook.Click();

                    Assert.IsTrue(driver.PageSource.Contains("LISTA PRODUKTÓW W KOSZYKU")&&driver.PageSource.Contains("Atlas Historii Świata"));

                }


            }

        }

        [TestMethod]
        public void Test_SortingProducts()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                var categoriesButton = driver.FindElementByXPath("//*[@id='menu_categories_top']/ul/li[1]/a");
                categoriesButton.Click();
                var categoryLink = driver.FindElementByXPath("//*[@id='content']/div/ul/li[1]/ul/li[1]/a");
                categoryLink.Click();
                var sortingSettings = driver.FindElementByXPath("//*[@id='paging_setting_top']/form/div[1]/div/div");
                sortingSettings.Click();
                var nameDown = driver.FindElementByXPath("//*[@id='paging_setting_top']/form/div[1]/div/ul/li[2]");
                nameDown.Click();

                Thread.Sleep(3000);
                //*[@id="search"]/div[1]/div[1]/div/span

                IList<IWebElement> elementsSorted = driver.FindElementsByClassName("product-name");
               
                var x = elementsSorted.Select(item => item.Text.Replace(System.Environment.NewLine, ""));
                x = x.Select(item => item).Skip(3);

                var sorted = new List<string>();
                sorted.AddRange(x.OrderByDescending(o => o));
                //System.Diagnostics.Debug.WriteLine(x.ElementAt(0));

                bool equal = true;
                
                for(int i = 0; i < x.LongCount(); i++)
                {
                   // System.Diagnostics.Debug.WriteLine(x.ElementAt(i) + " = " + sorted.ElementAt(i));
                    if (x.ElementAt(i) != sorted.ElementAt(i))
                    {
                        equal = false;
                    }
                }

                Assert.IsTrue(equal);
                
            
            }

        }

        [TestMethod]
        public void Test_NewsletterSignIn()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                var enterMail = driver.FindElementByName("mailing_email");
                enterMail.SendKeys("test@gmail.com");

                var name = driver.FindElementByName("mailing_name");
                name.SendKeys("Tests");
                name.SendKeys(Keys.Enter);
                Thread.Sleep(3000);

                Assert.IsTrue(driver.PageSource.Contains("Na podany adres został wysłany e-mail zawierający link"));
                
            }

        }

        [TestMethod]
        public void Test_NewsletterSignOut()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/newsletter.php");
                

                var enterMail = driver.FindElementByName("mailing_email");
                enterMail.SendKeys("test@gmail.com");

                var name = driver.FindElementByName("mailing_name");
                name.SendKeys("Tests");

                var newsletterButton = driver.FindElementById("newsletter_button_remove");
                newsletterButton.Click();

                Assert.IsTrue(driver.PageSource.Contains("Podany adres e-mail nie został odnaleziony w bazie newslettera."));
                

            }

        }

        //AFTER LOGGING IN TESTS

        [TestMethod]
        public void Test_DiscountCode()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/product-pol-407937-Oxford-Advanced-Learners-Dictionary-9E-DVD-TW.html");

                var basketButton = driver.FindElementById("projector_button_basket");
                basketButton.Click();

                

                var promoCode = driver.FindElementByName("rebates_codes");
                promoCode.SendKeys("H8sdd&6");
                promoCode.SendKeys(Keys.Enter);

                
                Thread.Sleep(3000);

                Assert.IsTrue(driver.PageSource.Contains("Podany kod rabatowy nie istnieje."));

            }

        }

        [TestMethod]
        public void Test_Logout()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/signin.php");

                string login = "test2@gmail.com";
                string password = "test2345";

                driver.FindElementByXPath("//*[@id='menu_additional']/a[1]").Click();
                driver.FindElementByXPath("//*[@id='signin_login_input']").SendKeys(login);
                var signIn = driver.FindElementByXPath("//*[@id='signin_pass_input']");
                signIn.SendKeys(password);
                signIn.SendKeys(Keys.Enter);

                driver.FindElementByXPath("//*[@id='menu_additional']/a[2]").Click() ;
                Assert.IsTrue(driver.PageSource.Contains("Zaloguj się"));
            }

        }

        [TestMethod]
        public void Test_AddDeliveryAdress()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/");

                string login = "test2@gmail.com";
                string password = "test2345";

                driver.FindElementByXPath("//*[@id='menu_additional']/a[1]").Click();
                driver.FindElementByXPath("//*[@id='signin_login_input']").SendKeys(login);
                var signIn = driver.FindElementByXPath("//*[@id='signin_pass_input']");
                signIn.SendKeys(password);
                signIn.SendKeys(Keys.Enter);

                Thread.Sleep(2000);

                driver.FindElementByXPath("//*[@id='menu_additional']/a[1]").Click();
                driver.FindElementByXPath("//*[@id='dane_login']/div/a").Click();

                Thread.Sleep(2000);
                var policyButton = driver.FindElementById("ckdsclmrshtdwn");
                policyButton.Click();
                driver.FindElementById("newDeliveryAddress").Click();
                Thread.Sleep(2000);
                driver.FindElementById("additional_firstname").SendKeys("NewName");
                driver.FindElementById("additional_lastname").SendKeys("NewSurname");
                driver.FindElementById("additional_street").SendKeys("testing 46");
                driver.FindElementById("additional_zipcode").SendKeys("66-555");
                driver.FindElementById("additional_city").SendKeys("Chicago");
                driver.FindElementById("additional_phone").SendKeys("123456789");
                driver.FindElementById("additional_phone").SendKeys(Keys.Enter);
                Thread.Sleep(2000);
                Assert.IsTrue(driver.PageSource.Contains("Zmiany zostały zapisane."));
            }

        }

        [TestMethod]
        public void Test_CheckIfFreeDelivery()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/product-pol-407937-Oxford-Advanced-Learners-Dictionary-9E-DVD-TW.html");

                var basketButton = driver.FindElementById("projector_button_basket");
                basketButton.Click();

                var addOne = driver.FindElementByXPath("//*[@id='basketedit_productslist']/table/tbody/tr[2]/td[4]/div/span/a[2]");
                addOne.Click();

                ////*[@id="basketedit_productslist"]/table/tbody/tr[2]/td[6]/button
                var refreshBasket = driver.FindElementByXPath("//*[@id='basketedit_productslist']/table/tbody/tr[2]/td[6]/button");
                refreshBasket.Click();


                Assert.IsTrue(driver.PageSource.Contains("gratis!"));

            }

        }

        [TestMethod]
        public void Test_AddProductToObserve()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/product-pol-444263-Ilustrowana-gramnatyka-angielska-dla-dzieci.html");

                var addLinkButton = driver.FindElementByXPath("//*[@id='projector_button_observe']");
                addLinkButton.Click();
                Thread.Sleep(3000);
                Assert.IsTrue(driver.PageSource.Contains("Lista obserwowanych zapamiętywana jest tylko dla osób zalogowanych"));

            }

        }

        [TestMethod]
        public void Test_DeleteProductFromObserved()
        {

            using (var driver = new FirefoxDriver())
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://matfel.pl/signin.php");

                string login = "test2@gmail.com";
                string password = "test2345";

                driver.FindElementByXPath("//*[@id='menu_additional']/a[1]").Click();
                driver.FindElementByXPath("//*[@id='signin_login_input']").SendKeys(login);
                var signIn = driver.FindElementByXPath("//*[@id='signin_pass_input']");
                signIn.SendKeys(password);
                signIn.SendKeys(Keys.Enter);

                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://matfel.pl/product-pol-387521-Madre-bajki-TW-GREG.html?rec=102855201");
                driver.FindElementById("projector_button_observe").Click();
                Thread.Sleep(2000);

                //*[@id="n57650"]/tbody/tr/td[3]/a[2]

                driver.FindElementByXPath("//*[@id='n57650']/tbody/tr/td[3]/a[2]").Click();

                Assert.IsTrue(driver.PageSource.Contains("Lista obserwowanych jest pusta."));

            }

        }



    }
}
