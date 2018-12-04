﻿using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Data.Models;
using DuoVia.FuzzyStrings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentralLendingApi.Services.Services
{
    public interface IProjectService
    {
        Task<int> AddPersonProject(PersonProjectDto personProjectDto);
        Task DeletePersonProject(int personProject_Id);
        Task<IEnumerable<Project>> Get();
        Task<Project> Get(int id);
        Task<IEnumerable<Project>> Suggest(string term);
        Task<IEnumerable<PersonProjectDto>> GetPersonProjects();

    }
    public class ProjectService : IProjectService
    {
        private CentralLendingContext context;
        private IHttpContextAccessor httpContextAccessor;

        public ProjectService(CentralLendingContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddPersonProject(PersonProjectDto personProjectDto)
        {
            PersonProject personProject;
            if (personProjectDto.Id > 0)
            {
                personProject = this.context.PersonProject.SingleOrDefault(pp => pp.Id == personProjectDto.Id);
                personProject.Amount = personProjectDto.Amount;
                personProject.StartDate = personProjectDto.StartDate;
            }
            else
            {
                personProject = HMapper.Mapper.Map<PersonProjectDto, PersonProject>(personProjectDto);
                await context.PersonProject.AddAsync(personProject);
            }

            await context.SaveChangesAsync();
            return personProject.Id;
        }

        public async Task DeletePersonProject(int personProject_Id)
        {
            PersonProject personProject = this.context.PersonProject.SingleOrDefault(pp => pp.Id == personProject_Id);
            context.PersonProject.Remove(personProject);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> Get()
        {
            return await this.context.Project.ToListAsync();
        }

        public async Task<IEnumerable<PersonProjectDto>> GetPersonProjects()
        {
            int personId =int.Parse(this.httpContextAccessor.HttpContext.User.Identity.Name);
            var personProjects =  await this.context.PersonProject.Include(pp => pp.Project).Where(pp => pp.PersonId==personId).ToListAsync();
            return HMapper.Mapper.Map<IEnumerable<PersonProject>, IEnumerable<PersonProjectDto>>(personProjects);
        }

        public async Task<IEnumerable<Project>> Suggest(string term)
        {
            var projects = await this.context.Project.ToListAsync();
            return projects.Where(p => p.Name.FuzzyEquals(term, 0.75) || p.Platform.FuzzyEquals(term, 0.75));
        }

        public async Task<Project> Get(int id)
        {
            return await this.context.Project.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
