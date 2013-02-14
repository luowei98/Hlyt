namespace Hlyt.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private HlytDataContex dataContext;

        #region IDatabaseFactory Members

        public HlytDataContex Get()
        {
            return dataContext ?? (dataContext = new HlytDataContex());
        }

        #endregion

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
