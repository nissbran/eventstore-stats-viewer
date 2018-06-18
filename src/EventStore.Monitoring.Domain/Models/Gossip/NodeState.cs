namespace EventStore.Monitoring.Domain.Models.Gossip
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
