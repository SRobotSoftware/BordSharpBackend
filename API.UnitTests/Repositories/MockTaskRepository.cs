using API.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;

namespace API.UnitTests
{
    public class MockTaskRepository : Mock<ITaskRepository>
    {
        public MockTaskRepository MockAddAsync(Task result)
        {
            Setup(x => x.AddAsync(It.IsAny<Domain.Models.Task>()))
                .Returns(result);
            return this;
        }
        public MockTaskRepository MockAddAsyncInvalid()
        {
            Setup(x => x.AddAsync(It.IsAny<Domain.Models.Task>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockTaskRepository VerifyAddAsync(Times times)
        {
            Verify(x => x.AddAsync(It.IsAny<Domain.Models.Task>()), times);
            return this;
        }
        public MockTaskRepository MockFindByIdAsync(Task<Domain.Models.Task> result)
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .Returns(result);
            return this;
        }
        public MockTaskRepository MockFindByIdAsyncInvalid()
        {
            Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Domain.Models.Task>(null));
            return this;
        }
        public MockTaskRepository VerifyFindByIdAsync(Times times)
        {
            Verify(x => x.FindByIdAsync(It.IsAny<int>()), times);
            return this;
        }
        public MockTaskRepository MockListAsync(List<Domain.Models.Task> result)
        {
            Setup(x => x.ListAsync())
                .Returns(Task.FromResult<IEnumerable<Domain.Models.Task>>(result));
            return this;
        }
        public MockTaskRepository VerifyListAsync(Times times)
        {
            Verify(x => x.ListAsync(), times);
            return this;
        }
        public MockTaskRepository MockUpdate()
        {
            Setup(x => x.Update(It.IsAny<Domain.Models.Task>()));
            return this;
        }
        public MockTaskRepository MockUpdateInvalid()
        {
            Setup(x => x.Update(It.IsAny<Domain.Models.Task>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockTaskRepository VerifyUpdate(Times times)
        {
            Verify(x => x.Update(It.IsAny<Domain.Models.Task>()), times);
            return this;
        }
        public MockTaskRepository MockRemove()
        {
            Setup(x => x.Remove(It.IsAny<Domain.Models.Task>()));
            return this;
        }
        public MockTaskRepository MockRemoveInvalid()
        {
            Setup(x => x.Remove(It.IsAny<Domain.Models.Task>()))
                .Throws(new System.Exception());
            return this;
        }
        public MockTaskRepository VerifyRemove(Times times)
        {
            Verify(x => x.Remove(It.IsAny<Domain.Models.Task>()), times);
            return this;
        }
    }
}
