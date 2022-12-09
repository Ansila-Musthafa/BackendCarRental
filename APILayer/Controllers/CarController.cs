using BusinessLogicLayer.Interface;
using GlobalEntityLayer.DTO;
using GlobalEntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController ]
    


    public class CarController : ControllerBase
    {
        private readonly ICars _cars;
        public CarController(ICars cars)
        {
            _cars = cars;
        }
        // GET: api/<CarController>

        [HttpGet]
        [Route("Get")]
        [AllowAnonymous]
        public ActionResult<List<CarDetails>> Get()
        {
            return _cars.get();
        }


        [HttpGet]
        [Route("get/{CarID}")]
        [AllowAnonymous]
        public ActionResult<CarDetails> Get(string CarID)
        {
            return _cars.Get(CarID);
        }


        [HttpPost]
        [Route("Add")]
        
        public IActionResult post([FromBody] CarDetailsDTO carDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not valid");
            _cars.post(carDetails);
            return Ok();

        }


        [HttpDelete]
        [Route("Delete/{CarID}")]
        public ActionResult Delete(string CarID)
        {
            
            return Ok(_cars.Delete(Int16.Parse(CarID)));
        }


        [HttpPost]
        [Route("Update/{CarID}")]
        public IActionResult Update(string CarID, CarDetailsDTO carDetailsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not valid");
            return Ok(_cars.Update(Int16.Parse(CarID), carDetailsDTO));

        }

    }
}
