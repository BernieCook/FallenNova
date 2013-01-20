namespace FallenNova.DomainModel
{
    /// <summary>
    /// Unit of work interface.
    /// </summary>
    /// <remarks>
    /// EF supports the unit of work pattern out of the box, because of this I didn't need to roll my own. I only need to provide 
    /// a basic interface with which to commit multiple operations within a single transaction,  which this plumbing provides.
    /// </remarks>
    public interface IUnitOfWork
    {
        void Commit();
    }
}
