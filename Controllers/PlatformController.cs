using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Conrollers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;

        public PlatformController(
            IPlatformRepo repository,
            IMapper mapper,
            ICommandDataClient commandDataCleint)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataCleint;
        }
        [HttpGet]
        [Route("GetPlatforms")]
        public ActionResult<IEnumerable<PlateformReadDtos>> GetPlatforms()
        {
            Console.WriteLine("---> getting platforms----");
            var platformsItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlateformReadDtos>>(platformsItems));
        }


        ///  [Route("GetPlatformsById")]
        [HttpGet("{id}", Name = "GetPlatformsById")]
        public ActionResult<PlateformReadDtos> GetPlatformsById(int id)
        {
            Console.WriteLine("---> getting platforms----");
            var platformsItems = _repository.GetPlatformbyId(id);
            if (platformsItems != null)
            {
                return Ok(_mapper.Map<PlateformReadDtos>(platformsItems));
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<PlateformReadDtos>> CreatePlatform(PlatformCreateDtos platformcreatedtos)
        {
            var platformModel = _mapper.Map<platform>(platformcreatedtos);
            _repository.CreatePlatform(platformModel);

            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlateformReadDtos>(platformModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetPlatformsById), new { id = platformReadDto.Id }, platformReadDto);


        }
    }
}