namespace EventStore.Monitoring.Infrastructure.Models.Http.Gossip
{
    public enum NodeState
    {
        Initializing,
        Unknown,
        PreReplica,
        CatchingUp,
        Clone,
        Slave,
        PreMaster,
        Master,
        Manager,
        ShuttingDown,
        Shutdown
    }
}
