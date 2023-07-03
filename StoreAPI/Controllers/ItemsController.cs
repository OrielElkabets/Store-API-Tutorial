using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.DTO;
using StoreServices;
using StoreServices.Data.Entity;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")] // localhost:7191/api/items
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsService _itemsService;
        private readonly IMapper _mapper;

        public ItemsController(ItemsService itemsService, IMapper mapper)
        {
            _itemsService = itemsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetItems()
        {
            List<ItemEO> itemEOs = _itemsService.GetItems();
            List<ItemDTO> itemsDTO = new List<ItemDTO>(itemEOs.Count);
            foreach (ItemEO item in itemEOs)
            {
                itemsDTO.Add(_mapper.Map<ItemDTO>(item));
            }
            return Ok(itemsDTO);
        }

        [HttpGet("price_grater_then/{price}")]
        public IActionResult GetItemsPriceGraterThen(double price)
        {
            var itemEOs = _itemsService.GetItemsPriceGraterThen(price);
            if (itemEOs.Count == 0) return NotFound($"there is no item with price grater then: {price}");

            List<ItemDTO> itemsDTO = new List<ItemDTO>(itemEOs.Count);
            foreach (ItemEO item in itemEOs)
            {
                itemsDTO.Add(_mapper.Map<ItemDTO>(item));
            }
            return Ok(itemsDTO);
        }

        [HttpGet("Item/{id}")]
        public IActionResult GetItem(int id)
        {
            var item = _itemsService.GetItem(id);
            if (item is null) return NotFound($"item with ID: {id} not found!");
            return Ok(_mapper.Map<ItemDTO>(item));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("item")]
        public IActionResult CreateItem([FromBody] NewItemDTO item)
        {
            var itemEOs = _itemsService.CreateItem(_mapper.Map<ItemEO>(item));
            List<ItemDTO> itemDTOs = new List<ItemDTO>(itemEOs.Count);
            foreach (ItemEO itemEO in itemEOs)
            {
                itemDTOs.Add(_mapper.Map<ItemDTO>(itemEO));
            }
            return Ok(itemDTOs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("item")]
        public IActionResult UpdateItem([FromBody] ItemDTO item)
        {
            var updatedItem = _itemsService.UpdateItem(_mapper.Map<ItemEO>(item));
            if (updatedItem is null) return NotFound($"the item with ID: {item.Id} is not exist");
            return Ok(_mapper.Map<ItemDTO>(updatedItem));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("item/{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _itemsService.DeleteItem(id);
            if (item is null) return NotFound($"item with ID: {id} is not exist");
            return Ok(_mapper.Map<ItemDTO>(item));
        }
    }
}
