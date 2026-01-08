namespace Playground.Domain.Entities.ddd.Gym
{
    public class Membership
    {
        public Guid Id { get; private set; }
        public Guid MemberId { get; private set; }

        private readonly List<FreezePeriod> _freezePeriods = new();
        public IReadOnlyCollection<FreezePeriod> FreezePeriods => _freezePeriods.AsReadOnly();
    }
}