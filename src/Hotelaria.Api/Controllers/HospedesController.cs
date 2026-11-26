using Hotelaria.Application.DTOs;
using Hotelaria.Application.Ports.Entrada;
using Microsoft.AspNetCore.Mvc;

namespace Hotelaria.Api.Controllers
{
    [ApiController]
    [Route("api/hospedes")]
    public class HospedesController(IHospedeService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HospedeResponse>>> Listar(CancellationToken ct) =>
            Ok(await service.ListarAsync(ct));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HospedeResponse>> ObterPorId(Guid id, CancellationToken ct) =>
            Ok(await service.ObterPorIdAsync(id, ct));

        [HttpPost]
        public async Task<ActionResult<HospedeResponse>> Criar(CriarHospedeRequest request, CancellationToken ct)
        {
            var hospede = await service.CriarAsync(request, ct);
            return CreatedAtAction(nameof(ObterPorId), new { id = hospede.Id }, hospede);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<HospedeResponse>> Atualizar(Guid id, AtualizarHospedeRequest request, CancellationToken ct) =>
            Ok(await service.AtualizarAsync(id, request, ct));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id, CancellationToken ct)
        {
            await service.RemoverAsync(id, ct);
            return NoContent();
        }
    }
}
