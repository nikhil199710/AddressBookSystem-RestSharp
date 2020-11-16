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
        /// UC22
        /// Tests the retrieving data function using get operation.
        /// </summary>
        [TestMethod]
        public void TestRetrievingDataUsingGetOperation()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<ContactInfo> dataResponse = JsonConvert.DeserializeObject<List<ContactInfo>>(response.Content);
            Assert.AreEqual(3, dataResponse.Count);
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
        
        /// <summary>
        /// UC24
        /// Tests the update data using put operation.
        /// </summary>
        [TestMethod]
        public void TestUpdateDataUsingPutOperation()
        {
            RestRequest request = new RestRequest("contacts/11", Method.PUT);
            JObject jobject = new JObject();
            jobject.Add("name", "Jasi");
            jobject.Add("contactType", "Friend");
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            ContactInfo dataResponse = JsonConvert.DeserializeObject<ContactInfo>(response.Content);
            Assert.AreEqual(dataResponse.name, "Jasi");
            Assert.AreEqual(dataResponse.contactType, "Friend");
        }
        
         /// <summary>
        /// UC25
        /// Tests the delete data using delete operation.
        /// </summary>
        [TestMethod]
        public void TestDeleteDataUsingDeleteOperation()
        {
            //Arrange
            RestRequest request = new RestRequest("contacts/11", Method.DELETE);
            //Act
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
        
        
    }
}
