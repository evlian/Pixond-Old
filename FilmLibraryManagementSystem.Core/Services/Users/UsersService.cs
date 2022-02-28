﻿using FilmLibraryManagementSystem.Core.Abstraction.Services.Users;
using FilmLibraryManagementSystem.Core.Services.Genres;
using FilmLibraryManagementSystem.Data;
using FilmLibraryManagementSystem.Model.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FilmLibraryManagementSystem.Core.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ILogger<GenresService> _logger;
        private readonly FilmLibraryContext _context;

        public UsersService(FilmLibraryContext context, ILogger<GenresService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> AuthenticateUser(User user)
        {
            User found = await _context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();
            if (found == null)
                return null;
            if (found.Password != user.Password)
                return null;
            return found;
        }

        public async Task<User> RegisterUser(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}