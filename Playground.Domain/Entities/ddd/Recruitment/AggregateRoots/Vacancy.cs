namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public class Vacancy
{
    public Guid Id { get; private set; }
    public Title VacancyTitle { get; private set; }
    public Description VacancyDescription { get; private set; }
    public Location LocationType { get; private set; }
    public JobType JobType { get; private set; }
    public DateTime PostedDate { get; private set; }
    public DateTime? ClosingDate { get; private set; }

    // Navigation Properties
    public Guid JobDescriptionId { get; private set; }

    private Vacancy(
        Title vacancyTitle,
        Description vacancyDescription,
        Guid jobDescriptionId,
        Location location,
        JobType jobType,
        DateTime postedDate,
        DateTime? closingDate)
    {
        Id = Guid.NewGuid();
        VacancyTitle = vacancyTitle;
        VacancyDescription = vacancyDescription;
        JobDescriptionId = jobDescriptionId;
        LocationType = location;
        JobType = jobType;
        PostedDate = postedDate;
        ClosingDate = closingDate;
    }

    public static Vacancy Create(
        Title vacancyTitle,
        Description vacancyDescription,
        Guid jobDescriptionId,
        Location location,
        JobType jobType,
        DateTime postedDate,
        DateTime? closingDate)
    {
        if (closingDate.HasValue && closingDate <= postedDate)
            throw new ArgumentException("Closing date must be after the posted date.", nameof(closingDate));

        if (postedDate > DateTime.UtcNow)
            throw new ArgumentException("Posted date cannot be in the future.", nameof(postedDate));

        if (jobDescriptionId == Guid.Empty)
            throw new ArgumentException("Job description ID cannot be empty.", nameof(jobDescriptionId));

        return new Vacancy(vacancyTitle, vacancyDescription, jobDescriptionId, location, jobType, postedDate, closingDate);
    }

    public void UpdateClosingDate(DateTime? newClosingDate)
    {
        if (newClosingDate.HasValue && newClosingDate <= PostedDate)
            throw new ArgumentException("Closing date must be after the posted date.", nameof(newClosingDate));

        ClosingDate = newClosingDate;
    }

    public void UpdateJobType(JobType newJobType)
    {
        JobType = newJobType;
    }

    public void UpdateLocation(Location newLocation)
    {
        LocationType = newLocation;
    }

    public void UpdateVacancyDetails(Title newTitle, Description newDescription)
    {
        VacancyTitle = newTitle;
        VacancyDescription = newDescription;
    }

    public void ExtendClosingDate(DateTime additionalDays)
    {
        if (!ClosingDate.HasValue)
            throw new InvalidOperationException("Cannot extend closing date for a vacancy without a closing date.");

        if (additionalDays <= PostedDate)
            throw new ArgumentException("New closing date must be after the posted date.", nameof(additionalDays));

        ClosingDate = additionalDays;
    }

    public void RemoveClosingDate()
    {
        ClosingDate = null;
    }

    public void ChangeJobDescription(Guid newJobDescriptionId)
    {
        if (newJobDescriptionId == Guid.Empty)
            throw new ArgumentException("Job description ID cannot be empty.", nameof(newJobDescriptionId));

        JobDescriptionId = newJobDescriptionId;
    }
}