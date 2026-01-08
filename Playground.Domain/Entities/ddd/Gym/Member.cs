namespace Playground.Domain.Entities.ddd.Gym
{
    public class Member
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        // Additional member properties and methods can be added here
    }
}