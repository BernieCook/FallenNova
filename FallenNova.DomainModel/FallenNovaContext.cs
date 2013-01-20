namespace FallenNova.DomainModel
{
    public partial class FallenNovaContext : IUnitOfWork
    {
        public FallenNovaContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}
