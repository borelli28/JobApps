using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Models;
using App.Data;

namespace App.Services;

public interface IJobService
{
    Task<IEnumerable<Job>> GetAllJobsAsync();
    Task<Job> CreateJobAsync(Job job);
    Task<Job> GetJobByIdAsync(string id);
    Task UpdateJobAsync(Job job);
    Task DeleteJobAsync(string id);
    Task<bool> JobExistsAsync(string id);
}

public class JobService : IJobService
{
    private readonly AppDbContext _context;

    public JobService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetAllJobsAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job> CreateJobAsync(Job job)
    {
        _context.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<Job> GetJobByIdAsync(string id)
    {
        return await _context.Jobs.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task UpdateJobAsync(Job updatedJob)
    {
        var existingJob = await _context.Jobs.FindAsync(updatedJob.Id);
        if (existingJob != null)
        {
            existingJob.Status = updatedJob.Status;
            existingJob.Title = updatedJob.Title;
            existingJob.Company = updatedJob.Company;
            existingJob.Url = updatedJob.Url;
            existingJob.Location = updatedJob.Location;
            if (updatedJob.Note != null)
            {
                existingJob.Note = updatedJob.Note;
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteJobAsync(string id)
    {
        var job = await GetJobByIdAsync(id);
        if (job != null)
        {
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> JobExistsAsync(string id)
    {
        return await _context.Jobs.AnyAsync(e => e.Id == id);
    }
}
