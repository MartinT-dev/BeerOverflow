using Microsoft.EntityFrameworkCore;
using BeerOverflow.Data;
using System;
using System.Collections.Generic;
using System.Text;
using BeerOverflow.Data.BeerOverflowContext;

namespace BeerOverflow.ServiceTest
{
    public static class TestUtils
    {
        public static DbContextOptions<BeeroverflowContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<BeeroverflowContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
