﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Class_Lib
{
    public class Repository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(uint id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

/*
Person (and derived classes)
    Create: add new employees, managers, or administrators
    Read: retrieve a list of people or a specific person
    Update: modify details of a person
    Delete: ensure business rules are followed
Package
    Create: add a new package.
    Read: retrieve packages by status, sender, or destination
    Update: modify package details
    Delete: prevent deletion if the package is not delivered, canceled, or returned
Content
    Create: add content to a package.
    Read: retrieve content for a specific package
    Update: modify content details
    Delete: prevent deletion if the associated package is not delivered
PackageEvent
    Create: log events for a package
    Read: retrieve events for a specific package
    Update: modify event details (if allowed)
    Delete: allow deletion of events
BaseLocation (and derived classes)
    Create: add new locations
    Read: retrieve locations or specific details
    Update: modify location details
    Delete: prevent deletion if employees or packages are assigned
Country
    Create: add new countries
    Read: retrieve a list of countries
    Update: modify country details
    Delete: allow deletion of countries
*/