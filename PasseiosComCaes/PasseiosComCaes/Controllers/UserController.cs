﻿using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using PasseiosComCaes.Data;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.ViewModels;

namespace PasseiosComCaes.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Cachorro = user.Cachorro,
                    Pecorrido = user.Pecorrido
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Pecorrido = user.Pecorrido,
                Cachorro = user.Cachorro
            };
            return View(userDetailViewModel);   
        }
    }
}
