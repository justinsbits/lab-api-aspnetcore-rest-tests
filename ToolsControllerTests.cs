using CommanderData;
using CommanderREST.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CommanderREST.tests
{
    public class ToolsControllerTest
    {
        private readonly WebApplicationFactory<CommanderREST.Startup> _factory;

        // !!!
        // starting point...
        // add support clean/seed of datastore prior to each test
        // add more tests
        // ...currently, data assumuptions abound / limited testing

        public ToolsControllerTest()
        {
            _factory = new WebApplicationFactory<CommanderREST.Startup>();
        }
        
        //
        //GET api/tools
        //

        [Fact]
        public async Task GetTools_Returns200OK_WhenDataStoreHasEntity()
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync("/api/tools");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObjectList = JsonConvert.DeserializeObject<List<ToolReadDto>>(responseString);
            
            // Assert
            Assert.NotEmpty(responseObjectList);
        }

        //
        //GET api/tools/{id}
        //

        [Fact]
        public async Task GetTool_Returns404NotFound_WhenDataStoreDoesNotContainId()
        {
            //Arrange 
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/tools/-1");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTool_Returns200OK_WhenDataStoreContainsId()
        {
            //Arrange 
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/tools/1");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        //
        //POST api/tools/
        //
    }
}
