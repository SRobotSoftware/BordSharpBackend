using API.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.UnitTests.Repositories
{
    public class MockBoardRepository: Mock<IBoardRepository>
    {
        public MockBoardRepository MockAddAsync(Task result)
        {
            Setup(x => x.AddAsync(It.IsAny<Domain.Models.Board>()))
                .Returns(result);
            return this;
        }
        public MockBoardRepository MockAddAsyncInvalid()
        {
            Setup(x => x.AddAsync(It.IsAny<Domain.Models.Board>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockBoardRepository VerifyAddAsync(Times times)
        {
            Verify(x => x.AddAsync(It.IsAny<Domain.Models.Board>()), times);
            return this;
        }
        public MockBoardRepository MockFindAsync(Task<Domain.Models.Board> result)
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .Returns(result);
            return this;
        }
        public MockBoardRepository MockFindAsyncInvalid()
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Domain.Models.Board>(null));
            return this;
        }
        public MockBoardRepository VerifyFindAsync(Times times)
        {
            Verify(x => x.FindByIdAsync(It.IsAny<int>()), times);
            return this;
        }
        public MockBoardRepository MockListAsync(List<Domain.Models.Board> result)
        {
            Setup(x => x.ListAsync())
                .Returns(Task.FromResult<IEnumerable<Domain.Models.Board>>(result));
            return this;
        }
        public MockBoardRepository VerifyListAsync(Times times)
        {
            Verify(x => x.ListAsync(), times);
            return this;
        }
        public MockBoardRepository MockUpdate()
        {
            Setup(x => x.Update(It.IsAny<Domain.Models.Board>()));
            return this;
        }
        public MockBoardRepository MockUpdateInvalid()
        {
            Setup(x => x.Update(It.IsAny<Domain.Models.Board>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockBoardRepository VerifyUpdate(Times times)
        {
            Verify(x => x.Update(It.IsAny<Domain.Models.Board>()), times);
            return this;
        }
        public MockBoardRepository MockRemove()
        {
            Setup(x => x.Remove(It.IsAny<Domain.Models.Board>()));
            return this;
        }
        public MockBoardRepository MockRemoveInvalid()
        {
            Setup(x => x.Remove(It.IsAny<Domain.Models.Board>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockBoardRepository VerifyRemove(Times times)
        {
            Verify(x => x.Remove(It.IsAny<Domain.Models.Board>()), times);
            return this;
        }
        public MockBoardRepository MockFindByIdWithTasksAsync(Domain.Models.Board result)
        {
            Setup(x => x.FindByIdWithTasksAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(result));
            return this;
        }
        public MockBoardRepository MockFindByIdWithTasksAsyncInvalid()
        {
            Setup(x => x.FindByIdWithTasksAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Domain.Models.Board>(null));
            return this;
        }
        public MockBoardRepository VerifyFindByIdWithTasksAsync(Times times)
        {
            Verify(x => x.FindByIdWithTasksAsync(It.IsAny<int>()), times);
            return this;
        }
    }
}
