using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repository, ILogger<BasketController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new NotImplementedException(nameof(logger));
        }

        [HttpGet("{userName:length(50)}", Name ="GetBasket")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<BasketCart>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            if (basket == null)
            {
                _logger.LogError($"Basket for user {userName} not found");
                return NotFound();
            }
            else return Ok(basket);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<BasketCart>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody] BasketCart basketCart)
        {
            return Ok(await _repository.UpdateBasket(basketCart));
        }

        [HttpDelete("{userName:length(50)}", Name ="DeleteBasket")]
        [ProducesResponseType(typeof(IEnumerable<BasketCart>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketCart>> UpdateBasket(string userName)
        {
            return Ok(await _repository.DeleteBasket(userName));
        }

    }
}
