using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Domain.Services;
using API.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace API.UnitTests
{
    [TestClass]
    public class TaskService_SaveAsync_Should
    {
        [TestMethod]
        public async Task TaskService_SaveAsync_Should_ReturnTask()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockAddAsync(Task.FromResult(Generics.Task));

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.SaveAsync(Generics.Task);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Task, typeof(Domain.Models.Task));
            mockTaskRepository.VerifyAddAsync(Times.Once());
        }
        [TestMethod]
        public async Task TaskService_SaveAsync_InputInvalid_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockAddAsyncInvalid();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.SaveAsync(Generics.Task);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Task);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when saving this task:"));
            mockTaskRepository.VerifyAddAsync(Times.Once());
        }
    }
    [TestClass]
    public class TaskService_FindAsync
    {
        [TestMethod]
        public async Task TaskService_FindAsync_InputValid_Should_ReturnTask()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockFindByIdAsync(Task.FromResult(Generics.Task));

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.FindAsync(1);

            Assert.IsNotNull(results);
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
        }
        [TestMethod]
        public async Task TaskService_FindAsync_InputInvalid_Should_ReturnNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockFindByIdAsyncInvalid();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.FindAsync(1);

            Assert.IsNull(results);
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
        }
    }
    [TestClass]
    public class TaskService_ListAsync
    {
        [TestMethod]
        public async Task TaskService_ListAsync_Should_ReturnListTasks()
        {
            var tasks = new List<Domain.Models.Task> { Generics.Task, Generics.Task };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockListAsync(tasks);

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.ListAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 2);
            mockTaskRepository.VerifyListAsync(Times.Once());
        }
        [TestMethod]
        public async Task TaskService_ListAsync_Should_ReturnEmptyList()
        {
            var tasks = new List<Domain.Models.Task> { };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository().MockListAsync(tasks);

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.ListAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 0);
            mockTaskRepository.VerifyListAsync(Times.Once());
        }
    }
    [TestClass]
    public class TaskService_UpdateAsync
    {
        [TestMethod]
        public async Task TaskService_UpdateAsync_Should_UpdateRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsync(Task.FromResult(Generics.Task))
                .MockUpdate();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.UpdateAsync(1, Generics.Task);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Task, typeof(Domain.Models.Task));
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyUpdate(Times.Once());
        }
        [TestMethod]
        public async Task TaskService_UpdateAsync_InputNotFound_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsyncInvalid()
                .MockUpdate();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.UpdateAsync(1, Generics.Task);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Task);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message == Generics.TaskResponseNotFound);
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyUpdate(Times.Never());
        }
        [TestMethod]
        public async Task TaskService_UpdateAsync_InputInvalid_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsync(Task.FromResult(Generics.Task))
                .MockUpdateInvalid();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.UpdateAsync(1, Generics.Task);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Task);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when updating the task:"));
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyUpdate(Times.Once());
        }
    }
    [TestClass]
    public class TaskService_DeleteAsync
    {
        [TestMethod]
        public async Task TaskService_DeleteAsync_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsync(Task.FromResult(Generics.Task))
                .MockRemove();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Task, typeof(Domain.Models.Task));
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyRemove(Times.Once());
        }
        [TestMethod]
        public async Task TaskService_DeleteAsync_InputNotFound_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsyncInvalid()
                .MockRemove();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Task);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message == Generics.TaskResponseNotFound);
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyRemove(Times.Never());
        }
        [TestMethod]
        public async Task TaskService_DeleteAsync_InputInvalid_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockTaskRepository = new MockTaskRepository()
                .MockFindByIdAsync(Task.FromResult(Generics.Task))
                .MockRemoveInvalid();

            var taskService = new TaskService(mockTaskRepository.Object, mockUnitOfWork.Object);

            var results = await taskService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Task);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when deleting the task:"));
            mockTaskRepository.VerifyFindByIdAsync(Times.Once());
            mockTaskRepository.VerifyRemove(Times.Once());
        }
    }
}
