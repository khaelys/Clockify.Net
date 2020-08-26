﻿using System;
using System.Threading.Tasks;
using Clockify.Net;
using Clockify.Net.Models.Projects;
using Clockify.Net.Models.Tasks;
using Clockify.Net.Models.Workspaces;
using Clockify.Tests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Clockify.Tests.Tests
{
    public class TaskTests
    {
        private readonly ClockifyClient _client;
        private string _workspaceId;

        public TaskTests()
        {
            _client = new ClockifyClient();
        }

        [OneTimeSetUp]
        public async Task Setup()
        {
            _workspaceId = await SetupHelper.CreateOrFindWorkspaceAsync(_client, "Clockify.NetTestWorkspace");
        }

        // TODO Uncomment when Clockify add deleting workspaces again

        // [OneTimeTearDown]
        // public async Task Cleanup()
        // {
	       //  var currentUser = await _client.GetCurrentUserAsync();
	       //  var changeResponse =
		      //   await _client.SetActiveWorkspaceFor(currentUser.Data.Id, DefaultWorkspaceFixture.DefaultWorkspaceId);
	       //  changeResponse.IsSuccessful.Should().BeTrue();
        //     var workspaceResponse = await _client.DeleteWorkspaceAsync(_workspaceId);
        //     workspaceResponse.IsSuccessful.Should().BeTrue();
        // }

        [Test]
        public async Task FindAllTasksAsync_ShouldReturnTaskList()
        {
            // Prepare some project
            var projectRequest = new ProjectRequest {Name = "Task find project " + Guid.NewGuid(), Color = "#FF00FF"};
            var createResult = await _client.CreateProjectAsync(_workspaceId, projectRequest);
            createResult.IsSuccessful.Should().BeTrue();
            var projectId = createResult.Data.Id;

            var response = await _client.FindAllTasksAsync(_workspaceId, projectId);
            response.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public async Task CreateTagAsync_ShouldCreteTagAndReturnTagDto()
        {
            // Prepare some project
            var projectRequest = new ProjectRequest {Name = "Create task project " + Guid.NewGuid(), Color = "#FF00FF"};
            var createProjectResult = await _client.CreateProjectAsync(_workspaceId, projectRequest);
            createProjectResult.IsSuccessful.Should().BeTrue();
            var projectId = createProjectResult.Data.Id;

            var taskRequest = new TaskRequest
            {
                Name = "Test task",
            };
            var createResult = await _client.CreateTaskAsync(_workspaceId, projectId, taskRequest);
            createResult.IsSuccessful.Should().BeTrue();
            createResult.Data.Should().NotBeNull();

            var findResult = await _client.FindAllTasksAsync(_workspaceId, projectId);
            findResult.IsSuccessful.Should().BeTrue();
            findResult.Data.Should().ContainEquivalentOf(createResult.Data);
        }

        [Test]
        public async Task CreateTaskAsync_NullName_ShouldThrowArgumentException()
        {
            var taskRequest = new TaskRequest()
            {
                Name = null,
            };
            Func<Task> create = async () => await _client.CreateTaskAsync("", "", taskRequest);

            await create.Should().ThrowAsync<ArgumentException>()
                .WithMessage($"Value cannot be null. (Parameter '{nameof(TaskRequest.Name)}')");
        }
    }
}