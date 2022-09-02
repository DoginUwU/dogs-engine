namespace engine.Framework.Lists
{
    interface IHasLifetime
    {
        double LifetimeStart { get; }
        double LifetimeEnd { get; }

        bool IsAlive { get; }
        bool LoadRequired { get; }
        bool RemoveWhenNotAlive { get; }

        void Load();
    }
}
