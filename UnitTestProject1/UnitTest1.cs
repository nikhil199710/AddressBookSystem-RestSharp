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
    }
}