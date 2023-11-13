using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Computer.Dapper
{
    public abstract class BaseApplicationService
    {
        protected IApplicationDbConnection DbConnectionHelper { get; }
        public BaseApplicationService(IServiceProvider serviceProvider)
        {
            DbConnectionHelper = serviceProvider.GetRequiredService<IApplicationDbConnection>(); ;
        }

    }
}
