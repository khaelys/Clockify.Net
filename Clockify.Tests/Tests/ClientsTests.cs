﻿using System;
using System.Threading.Tasks;
using Clockify.Net;
using Clockify.Net.Models.Clients;
using Clockify.Net.Models.Workspaces;
using Clockify.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Clockify.Tests.Tests
{
    public class ClientsTests
    {
        private readonly ClockifyClient _client;
        private string _workspaceId;

        public ClientsTests()
        {
            _client = new ClockifyClient();
        }

        [OneTimeSetUp]
        public async Task Setup()
        {
            _workspaceId = await SetupHelper.CreateOrFindWorkspaceAsync(_client, "ClientTestWorkspace");
        }

        // TODO Uncomment when Clockify add deleting workspaces again
        //[OneTimeTearDown]
        //public async Task Cleanup() {
	       // var currentUser = await _client.GetCurrentUserAsync();
	       // var changeResponse =
		      //  await _client.SetActiveWorkspaceFor(currentUser.Data.Id, DefaultWorkspaceFixture.DefaultWorkspaceId);
	       // changeResponse.IsSuccessful.Should().BeTrue();
        //    var workspaceResponse = await _client.DeleteWorkspaceAsync(_workspaceId);
        //    workspaceResponse.IsSuccessful.Should().BeTrue();
        //}

        [Test]
        public async Task CreateClientAsync_ShouldCreteClientAndReturnClientDto()
        {
            var clientRequest = new ClientRequest {Name = "Test add client " + Guid.NewGuid()};
            var createResult = await _client.CreateClientAsync(_workspaceId, clientRequest);
            createResult.IsSuccessful.Should().BeTrue();
            createResult.Data.Should().NotBeNull();

            var findResult = await _client.FindAllClientsOnWorkspaceAsync(_workspaceId);
            findResult.IsSuccessful.Should().BeTrue();
            findResult.Data.Should().ContainEquivalentOf(createResult.Data);
        }

        [Test]
        public async Task FindAllClientsOnWorkspaceAsync_ShouldReturnClientsList()
        {
            var workspaceResponse = await _client.FindAllClientsOnWorkspaceAsync(_workspaceId);
            workspaceResponse.IsSuccessful.Should().BeTrue();
        }
    }
}