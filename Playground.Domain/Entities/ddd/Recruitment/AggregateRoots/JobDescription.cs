namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public class JobDescription
{
    public Guid Id { get; private set; }
    public Title Title { get; private set; }
    public Description Description { get; private set; }
    private readonly List<Skill> _requiredSkills = new();
    private readonly List<Skill> _preferredSkills = new();
    private readonly List<Skill> _niceToHaveSkills = new();
    public IReadOnlyCollection<Skill> RequiredSkills => _requiredSkills.AsReadOnly();
    public IReadOnlyCollection<Skill> PreferredSkills => _preferredSkills.AsReadOnly();
    public IReadOnlyCollection<Skill> NiceToHaveSkills => _niceToHaveSkills.AsReadOnly();

    private JobDescription(Title title, Description description, List<Skill> requiredSkills, List<Skill> preferredSkills, List<Skill> niceToHaveSkills)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        _requiredSkills = requiredSkills;
        _preferredSkills = preferredSkills;
        _niceToHaveSkills = niceToHaveSkills;
    }

    public static JobDescription Create(Title title, Description description, List<Skill> requiredSkills, List<Skill> preferredSkills, List<Skill> niceToHaveSkills)
    {
        if (!requiredSkills.Any())
            throw new ArgumentException("At least one required skill must be provided.", nameof(requiredSkills));

        return new JobDescription(title, description, requiredSkills, preferredSkills, niceToHaveSkills);
    }

    public void AddRequiredSkill(Skill skill)
    {
        if (_preferredSkills.Contains(skill) || _niceToHaveSkills.Contains(skill))
            throw new ArgumentException("Skill already exists in preferred or nice-to-have skills.", nameof(skill));

        if (_requiredSkills.Contains(skill))
            return;

        _requiredSkills.Add(skill);
    }

    public void AddPreferredSkill(Skill skill)
    {
        if (_requiredSkills.Contains(skill) || _niceToHaveSkills.Contains(skill))
            throw new ArgumentException("Skill already exists in required or nice-to-have skills.", nameof(skill));

        if (_preferredSkills.Contains(skill))
            return;

        _preferredSkills.Add(skill);
    }

    public void AddNiceToHaveSkill(Skill skill)
    {
        if (_requiredSkills.Contains(skill) || _preferredSkills.Contains(skill))
            throw new ArgumentException("Skill already exists in required or preferred skills.", nameof(skill));

        if (_niceToHaveSkills.Contains(skill))
            return;

        _niceToHaveSkills.Add(skill);
    }

    public void RemoveRequiredSkill(Skill skill)
    {
        if (!_requiredSkills.Contains(skill))
            return;
        _requiredSkills.Remove(skill);
    }

    public void RemovePreferredSkill(Skill skill)
    {
        if (!_preferredSkills.Contains(skill))
            return;
        _preferredSkills.Remove(skill);
    }

    public void RemoveNiceToHaveSkill(Skill skill)
    {
        if (!_niceToHaveSkills.Contains(skill))
            return;
        _niceToHaveSkills.Remove(skill);
    }

    public void UpdateDetails(Title newTitle, Description newDescription)
    {
        Title = newTitle;
        Description = newDescription;
    }
}