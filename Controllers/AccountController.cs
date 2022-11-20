using LawRoomApi.Entities;
using LawRoomApi.Entities.Models;
using LawRoomApi.Entities.Models.UserModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawRoomApi.Controllers;


[Route("Api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager  <User> userManager;
    private readonly RoleManager  <Role> roleManager;

    public AccountController(SignInManager<User> _signInManager,
                              UserManager <User> _userManager,
                              RoleManager <Role> _roleManager)
    {
        signInManager = _signInManager;
        userManager   = _userManager;
        roleManager   = _roleManager;
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(SignUpUser signUpUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(error: "some invalid fields");

        if (signUpUser.Password != signUpUser.Password)
            return BadRequest(error: "password not the same");

        if (await userManager.Users.AnyAsync(x => x.UserName == signUpUser.UserName))
            return BadRequest(error: "UserName is already registred");

        var user = signUpUser.Adapt<User>();

        await userManager.CreateAsync(user, signUpUser.Password);
        await signInManager.SignInAsync(user,isPersistent: true);
        return Ok(user.Adapt<SignInUser>());
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn(SignInUser signInUser)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (!await userManager.Users.AnyAsync(x => x.UserName== signInUser.UserName))
            return NotFound();

        var user = await userManager.Users.FirstAsync(x => x.UserName == signInUser.UserName);
       
        if (user.Password != signInUser.Password)
            return BadRequest();
       
        var result = await signInManager.PasswordSignInAsync(user, user.Password, isPersistent: true, false);

        if (!result.Succeeded)
            return NotFound();

        return Ok();
    }

    [HttpPut]
    [Authorize]
    public async Task <IActionResult> UpdateUser(UpdateUser updateUser)
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return BadRequest("SignUp before");
        user.UserName  = updateUser.UserName ; //?? user.UserName shu korinishda yozib
        user.Password  = updateUser.Password ; // dto dan requiredni olib tashla ,qayta
        user.FirstName = updateUser.FirstName; //sign updan fatqi qolmaydi.
        user.LastName  = updateUser.LastName;
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded) return  BadRequest(error: "cannot update");
        return Ok("updated");
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser()
    {
        var user = await userManager.GetUserAsync(User);
        if (user is null) return BadRequest("SignUp before");
        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest("plaese try again");
        return Ok();
    }
}