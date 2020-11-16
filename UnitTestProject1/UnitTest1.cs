using AddressBookSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
namespace MSTestForRestApiOperations
{
    [TestClass]
    public class TestRestOperations
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// UC23
        /// Tests the add multiple entries using post operation.
        /// </summary>
        [TestMethod]
        public void TestAddMultipleEntriesUsingPostOperation()
        {
            //adding multiple employees to table
            List<ContactInfo> contactList = new List<ContactInfo>();
            contactList.Add(new ContactInfo { name = "nik", address = "pat", phoneNumber = "3124211", email ="nik.com", contactType = "Family" });
            contactList.Add(new ContactInfo { name = "aky", address = "kol", phoneNumber = "211331313", email = "ak.com", contactType = "Frnd" });
            foreach (ContactInfo contact in contactList)
            {
                RestRequest request = new RestRequest("/contacts", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("name", contact.name);
                jObject.Add("address", contact.address);
                jObject.Add("phoneNumber", contact.phoneNumber);
                jObject.Add("email", contact.email);
                jObject.Add("contactType", contact.contactType);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                //derserializing object for assert and checking test case
                ContactInfo dataResponse = JsonConvert.DeserializeObject<ContactInfo>(response.Content);
                Assert.AreEqual(contact.name, dataResponse.name);
            }
        }
    }
}