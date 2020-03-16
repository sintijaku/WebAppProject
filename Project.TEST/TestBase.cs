using Microsoft.EntityFrameworkCore;
using Project.DATA.Context;

namespace Project.TEST
{
    public abstract class TestBase
    {

        protected MyContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            return new MyContext(options);
        }

    }
}
