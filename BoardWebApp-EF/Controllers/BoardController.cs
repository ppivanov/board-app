using System;
using BoardWebApp.Models;
using BoardWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BoardWebApp.Controllers
{
    public class BoardController : Controller 
    {
        private BoardWebAppContext _dbContext;
        public BoardController(BoardWebAppContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public IActionResult Board(int boardId)
        {
            string cookieValue = Request.Cookies[AccountController.CookieId];
            User authenticatedUser = Models.User.AuthenticateBasedOnCookieValue(cookieValue, _dbContext);
            if (authenticatedUser.DoesUserHaveAccessToBoard(boardId, _dbContext))
            {
                return View();
            }
            else
            {
                var homePageData = new HomePageModel(authenticatedUser, _dbContext);
                homePageData.Message = "Access error to board!";
                return RedirectToAction("Index", "Home", homePageData);
            }
        }
    }
}
