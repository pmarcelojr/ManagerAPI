using System;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
        {
            try 
            {
                var userDTO = _mapper.Map<UserDTO>(userViewModel);
                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultViewModel{
                    Message = "Usuário criado com sucesso!",
                    Sucess = true,
                    Data = userCreated,
                });
            }
            catch(DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch(Exception) 
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/users/get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var allUsers = await _userService.Get();

                return Ok(new ResultViewModel
                {
                    Message = "Usuários encontrados com sucesso!",
                    Sucess = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());                
            }
        }

        [HttpGet]
        [Route("/api/v1/users/get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var user = await _userService.Get(id);

                if (user == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o ID informado.",
                        Sucess = true,
                        Data = user
                    });
                }

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/users/get-by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var user = await _userService.GetByEmail(email);

                if (user == null)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o email informado.",
                        Sucess = true,
                        Data = user
                    });
                }                

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = user
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/users/search-by-name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allUsers = await _userService.SearchByName(name);

                if (allUsers.Count == 0)
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum usuário foi encontrado com o nome informado",
                        Sucess = true,
                        Data = null
                    });
                }                

                return Ok(new ResultViewModel
                {
                    Message = "Usuário encontrado com sucesso!",
                    Sucess = true,
                    Data = allUsers
                });
            }
            catch (DomainException ex)
            {
                return Ok(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}