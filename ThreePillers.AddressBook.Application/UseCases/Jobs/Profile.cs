namespace ThreePillers.AddressBook.Application.UseCases.Jobs;

internal sealed class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobDto>();
        CreateMap<CreateJobCommand, Job>();
        CreateMap<UpdateJobCommand, Job>();
    }
}
