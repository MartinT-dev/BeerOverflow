using BeerOverflow.Services.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime() => DateTime.UtcNow;
    }
}
