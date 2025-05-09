﻿using Class_Lib.Backend.Database.Repositories;
using Class_Lib.Backend.Package_related.enums;
using Class_Lib.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Class_Lib.Backend.Package_related.Methods
{
    public class ContentMethods
    {
        private readonly ContentRepository _contentRepository;

        public ContentMethods(ContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        // Create
        public async Task AddContentAsync(Employee user, Content content)
        {
            if (!user.HasPermission(AccessService.PermissionKey.CreateContent))
            {
                throw new UnauthorizedAccessException("Немає дозволу створювати вміст посилки.");
            }

            await _contentRepository.AddAsync(content);
        }

        // Read
        public async Task<IEnumerable<Content>> GetContentsByCustomCriteriaAsync(Employee user, Expression<Func<Content, bool>> filter)
        {
            if (!user.HasPermission(AccessService.PermissionKey.ReadContent))
            {
                throw new UnauthorizedAccessException("Немає доступу до перегляду вмісту посилки.");
            }

            return await _contentRepository.Query()
                .Where(filter)
                .ExecuteAsync();
        }

        // Update
        public async Task UpdateContentAsync(Employee user, Content content)
        {
            if (!user.HasPermission(AccessService.PermissionKey.UpdateContent))
            {
                throw new UnauthorizedAccessException("Немає дозволу змінювати вміст посилки.");
            }

            await _contentRepository.UpdateAsync(content);
        }

        // Delete
        public async Task DeleteContentAsync(Employee user, Content content)
        {
            if (!user.HasPermission(AccessService.PermissionKey.DeleteContent))
            {
                throw new UnauthorizedAccessException("Немає дозволу видаляти вміст посилки.");
            }

            await _contentRepository.DeleteAsync(content);
        }
    }
}
