using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Domain.Services;
using API.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using System.Linq;
using API.UnitTests.Repositories;

namespace API.UnitTests
{
    [TestClass]
    public class BoardService_SaveAsync_Should
    {
        [TestMethod]
        public async Task BoardService_SaveAsync_Should_ReturnBoard()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockAddAsync(Task.FromResult(Generics.Board));

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.SaveAsync(Generics.Board);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Board, typeof(Domain.Models.Board));
            mockBoardRepository.VerifyAddAsync(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_SaveAsync_InputInvalid_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockAddAsyncInvalid();

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.SaveAsync(Generics.Board);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Board);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when saving this board:"));
            mockBoardRepository.VerifyAddAsync(Times.Once());
        }
    }
    [TestClass]
    public class BoardService_FindAsync_Should
    {
        [TestMethod]
        public async Task BoardService_FindAsync_Should_ReturnBoard()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockFindAsync(Task.FromResult(Generics.Board));

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.FindAsync(1);

            Assert.IsNotNull(results);
            mockBoardRepository.VerifyFindAsync(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_FindAsync_InputInvalid_Should_ReturnNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockFindAsyncInvalid();

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.FindAsync(1);

            Assert.IsNull(results);
            mockBoardRepository.VerifyFindAsync(Times.Once());
        }
    }
    [TestClass]
    public class BoardService_FindWithTasksAsync_Should
    {
        [TestMethod]
        public async Task BoardService_FindWithTasksAsync_Should_ReturnBoardWithTasks()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockFindByIdWithTasksAsync(Generics.Board);

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.FindWithTasksAsync(1);

            Assert.IsNotNull(results);
            mockBoardRepository.VerifyFindByIdWithTasksAsync(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_FindWithTasksAsync_InputInvalid_Should_ReturnNull()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockFindByIdWithTasksAsyncInvalid();

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.FindWithTasksAsync(1);

            Assert.IsNull(results);
            mockBoardRepository.VerifyFindByIdWithTasksAsync(Times.Once());
        }
    }
    [TestClass]
    public class BoardService_ListAsync_Should
    {
        [TestMethod]
        public async Task BoardService_ListAsync_Should_ReturnListBoards()
        {
            var boards = new List<Domain.Models.Board> { Generics.Board, Generics.Board };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockListAsync(boards);

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.ListAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 2);
            mockBoardRepository.VerifyListAsync(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_ListAsync_Should_ReturnEmptyList()
        {
            var boards = new List<Domain.Models.Board> { };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository().MockListAsync(boards);

            var boardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await boardService.ListAsync();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 0);
            mockBoardRepository.VerifyListAsync(Times.Once());
        }
    }
    [TestClass]
    public class BoardService_UpdateAsync
    {
        [TestMethod]
        public async Task BoardService_UpdateAsync_Should_UpdateRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsync(Task.FromResult(Generics.Board))
                .MockUpdate();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.UpdateAsync(1, Generics.Board);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Board, typeof(Domain.Models.Board));
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyUpdate(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_UpdateAsync_InputNotFound_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsyncInvalid()
                .MockUpdate();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.UpdateAsync(1, Generics.Board);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Board);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message == Generics.BoardResponseNotFound);
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyUpdate(Times.Never());
        }
        [TestMethod]
        public async Task BoardService_UpdateAsync_InputInvalid_Should_ReturnBadResponse()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsync(Task.FromResult(Generics.Board))
                .MockUpdateInvalid();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.UpdateAsync(1, Generics.Board);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Board);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when updating the board:"));
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyUpdate(Times.Once());
        }
    }
    [TestClass]
    public class BoardService_DeleteAsync
    {
        [TestMethod]
        public async Task BoardService_DeleteAsync_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsync(Task.FromResult(Generics.Board))
                .MockRemove();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Success);
            Assert.IsInstanceOfType(results.Board, typeof(Domain.Models.Board));
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyRemove(Times.Once());
        }
        [TestMethod]
        public async Task BoardService_DeleteAsync_InputNotFound_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsyncInvalid()
                .MockRemove();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Board);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message == Generics.BoardResponseNotFound);
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyRemove(Times.Never());
        }
        [TestMethod]
        public async Task BoardService_DeleteAsync_InputInvalid_Should_DeleteFromRepository()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBoardRepository = new MockBoardRepository()
                .MockFindAsync(Task.FromResult(Generics.Board))
                .MockRemoveInvalid();

            var BoardService = new BoardService(mockBoardRepository.Object, mockUnitOfWork.Object);

            var results = await BoardService.DeleteAsync(1);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Board);
            Assert.IsFalse(results.Success);
            Assert.IsTrue(results.Message.Contains("An error occurred when deleting the board:"));
            mockBoardRepository.VerifyFindAsync(Times.Once());
            mockBoardRepository.VerifyRemove(Times.Once());
        }
    }
}
