namespace Forum.Web.Controllers
{
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Http;
	using AutoMapper;
	using Domain;
	using BusinessLogic;
	using Models;

	[RoutePrefix("api/messages")]
	public class MessagesController : BaseController
	{
		private readonly IMessageService _messageService;

		private readonly IMapper _mapper;

		public MessagesController(IMessageService messageService, IMapper mapper)
		{
			_messageService = messageService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("{id}", Name = "GetItemById")]
		public async Task<IHttpActionResult> Get(Guid id)
		{
			var message = await _messageService.Get(id);
			return Ok(_mapper.Map<Message, MessageDto>(message));
		}

		[HttpGet]
		[Route("")]
		public async Task<IHttpActionResult> Get([FromUri] ListRequest request)
		{
			request = request ?? new ListRequest();
			var result = await _messageService.Get(request.ToQuery());
			var response = new ListResponse<MessageDto>
			{
				Page = result.Page,
				PageSize = result.PageSize,
				List = result.List.Select(message => _mapper.Map<Message, MessageDto>(message)).ToList(),
				Total = result.Total
			};
			return Ok(response);
		}

		[HttpPost]
		[Route("")]
		public async Task<IHttpActionResult> Create(MessageDto messageDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var message = _mapper.Map(messageDto, new Message());
			message = await _messageService.Create(message);
			var response = Request.CreateResponse(HttpStatusCode.Created, _mapper.Map<Message, MessageDto> (message));

			string uri = Url.Link("GetItemById", new { message.Id });
			response.Headers.Location = new Uri(uri);
			return ResponseMessage(response);
		}

		[HttpPut]
		[Route("")]
		public async Task<IHttpActionResult> Update(MessageDto messageDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _mapper.Map(messageDto, await _messageService.Get(messageDto.Id));
			item = await _messageService.Update(item);
			return Ok(_mapper.Map<Message, MessageDto>(item));
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IHttpActionResult> Delete(Guid id)
		{
			await _messageService.Delete(id);
			return Ok();
		}
	}
}