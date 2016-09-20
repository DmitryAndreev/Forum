using Forum.Domain;

namespace Forum.WebApi.Controllers
{
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Http;
	using AutoMapper;
	using BusinessLogic;
	using Models;

	[RoutePrefix("api/cards")]
	public class CardsController : BaseController
	{
		private readonly ICardService _cardService;

		private readonly IMapper _mapper;

		public CardsController(ICardService cardService, IMapper mapper)
		{
			_cardService = cardService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("{id}", Name = "GetItemById")]
		public async Task<IHttpActionResult> Get(Guid id)
		{
			var card = await _cardService.Get(id);
			return Ok(_mapper.Map<Card, CardDto>(card));
		}

		[HttpGet]
		[Route("")]
		public async Task<IHttpActionResult> Get([FromUri] ListRequest request)
		{
			request = request ?? new ListRequest();
			var result = await _cardService.Get(request.ToQuery());
			var response = new ListResponse<CardDto>
			{
				Page = result.Page,
				PageSize = result.PageSize,
				List = result.List.Select(card => _mapper.Map<Card, CardDto>(card)).ToList(),
				Total = result.Total
			};
			return Ok(response);
		}

		[HttpPost]
		[Route("")]
		public async Task<IHttpActionResult> Create(CardDto cardDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var card = _mapper.Map(cardDto, new Card());
			card = await _cardService.Create(card);
			var response = Request.CreateResponse(HttpStatusCode.Created, _mapper.Map<Card, CardDto>(card));

			string uri = Url.Link("GetItemById", new {card.Id});
			response.Headers.Location = new Uri(uri);
			return ResponseMessage(response);
		}

		[HttpPut]
		[Route("")]
		public async Task<IHttpActionResult> Update(CardDto cardDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _mapper.Map(cardDto, await _cardService.Get(cardDto.Id));
			item = await _cardService.Update(item);
			return Ok(_mapper.Map<Card, CardDto>(item));
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IHttpActionResult> Delete(Guid id)
		{
			await _cardService.Delete(id);
			return Ok();
		}
	}
}