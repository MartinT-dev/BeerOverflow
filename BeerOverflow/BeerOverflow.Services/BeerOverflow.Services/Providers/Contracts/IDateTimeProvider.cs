using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTime();
    }
}
