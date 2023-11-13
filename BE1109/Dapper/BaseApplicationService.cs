namespace BE1109.Dapper
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
