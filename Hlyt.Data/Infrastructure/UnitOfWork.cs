namespace Hlyt.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private HlytDataContex dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected HlytDataContex DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        #region IUnitOfWork Members

        public void Commit()
        {
            DataContext.Commit();
        }

        #endregion
    }
}
