/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;

namespace Common.Services
{
    public abstract class BaseService
    {
        protected ICurrentContextProvider contextProvider;
        protected readonly ContextSession Session;

        protected BaseService(ICurrentContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
            Session = contextProvider.GetCurrentContext();
        }
    }
}
