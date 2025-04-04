using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopularSeguros.Models;
using Microsoft.EntityFrameworkCore;
using PopularSeguros.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PopularSeguros.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    [ApiController]
    public class PolizasController : ControllerBase
    {
        //Tipo de inyeccion de dependecia
        private readonly DbPolizasContext dbContext;
        public PolizasController(DbPolizasContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<List<PolizaDTO>>> Get()
        {
            try
            {
                var polizas = await dbContext.Polizas
                    .Include(p => p.IdEstadoPolizaNavigation)
                    .Include(p => p.IdTipoPolizaNavigation)
                    .Include(p => p.IdCoberturaNavigation)
                    .Include(p => p.IdAseguradoraNavigation)
                    .Select(p => new PolizaDTO
                    {
                        NumeroPoliza = p.NumeroPoliza,
                        TipoPoliza = p.IdTipoPolizaNavigation.TipoPoliza1,
                        CedulaAsegurado = p.CedulaAsegurado,
                        MontoAsegurado = p.MontoAsegurado,
                        FechaVencimiento = p.FechaVencimiento,
                        FechaEmision = p.FechaEmision,
                        Cobertura = p.IdCoberturaNavigation.Cobertura1,
                        EstadoPoliza = p.IdEstadoPolizaNavigation.EstadoPoliza1,
                        Prima = p.Prima,
                        Periodo = p.Periodo,
                        FechaInclusion = p.FechaInclusion,
                        Aseguradora = p.IdAseguradoraNavigation.Aseguradora1
                    })
                    .ToListAsync();

                return Ok(polizas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al listar las pólizas: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Listar/{numeroPoliza}")]
        public async Task<ActionResult<PolizaDTO>> Get(string numeroPoliza)
        {
            try
            {
                var poliza = await dbContext.Polizas
                    .Include(p => p.IdEstadoPolizaNavigation)
                    .Include(p => p.IdTipoPolizaNavigation)
                    .Include(p => p.IdCoberturaNavigation)
                    .Include(p => p.IdAseguradoraNavigation)
                    .Where(p => p.NumeroPoliza == numeroPoliza)
                    .Select(p => new PolizaDTO
                    {
                        NumeroPoliza = p.NumeroPoliza,
                        TipoPoliza = p.IdTipoPolizaNavigation.TipoPoliza1,
                        CedulaAsegurado = p.CedulaAsegurado,
                        MontoAsegurado = p.MontoAsegurado,
                        FechaVencimiento = p.FechaVencimiento,
                        FechaEmision = p.FechaEmision,
                        Cobertura = p.IdCoberturaNavigation.Cobertura1,
                        EstadoPoliza = p.IdEstadoPolizaNavigation.EstadoPoliza1,
                        Prima = p.Prima,
                        Periodo = p.Periodo,
                        FechaInclusion = p.FechaInclusion,
                        Aseguradora = p.IdAseguradoraNavigation.Aseguradora1
                    })
                    .FirstOrDefaultAsync();
                if (poliza == null)
                {
                    return NotFound($"No se encontró la póliza con número {numeroPoliza}");
                }
                return Ok(poliza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Ocurrió un error al obtener la póliza: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Nueva")]
        public async Task<IActionResult> PostPoliza(PolizaDTO polizaDto)
        {
            try
            {
                // Diccionarios para mapear nombres a IDs
                var tiposPoliza = new Dictionary<string, int>
        {
            { "Vida", 1 },
            { "Automovil", 2 },
            { "Hogar", 3 }
        };

                var coberturas = new Dictionary<string, int>
        {
            { "Accidente", 1 },
            { "Robo", 2 },
            { "Incendio", 3 }
        };

                var estadosPoliza = new Dictionary<string, int>
        {
            { "Activo", 1 },
            { "Cancelado", 2 },
            { "Vencido", 3 }
        };

                var aseguradoras = new Dictionary<string, int>
        {
            { "Popular Seguros", 1 },
            { "INS", 2 },
            { "CCSS", 3 }
        };

                // Validaciones específicas
                if (string.IsNullOrWhiteSpace(polizaDto.NumeroPoliza))
                    return BadRequest("El número de póliza es obligatorio.");

                if (!tiposPoliza.ContainsKey(polizaDto.TipoPoliza))
                    return BadRequest($"El tipo de póliza '{polizaDto.TipoPoliza}' no es válido. Debe ser Vida, Automóvil o Hogar.");

                if (!coberturas.ContainsKey(polizaDto.Cobertura))
                    return BadRequest($"La cobertura '{polizaDto.Cobertura}' no es válida. Debe ser Accidente, Robo o Incendio.");

                if (!estadosPoliza.ContainsKey(polizaDto.EstadoPoliza))
                    return BadRequest($"El estado de la póliza '{polizaDto.EstadoPoliza}' no es válido. Debe ser Activo, Cancelado o Vencido.");

                if (!aseguradoras.ContainsKey(polizaDto.Aseguradora))
                    return BadRequest($"La aseguradora '{polizaDto.Aseguradora}' no es válida. Debe ser Popular Seguros, INS o CCSS.");

                if (polizaDto.MontoAsegurado <= 0)
                    return BadRequest("El monto asegurado debe ser mayor a 0.");

                if (polizaDto.Prima <= 0)
                    return BadRequest("La prima debe ser mayor a 0.");

                // Validar si la cédula existe en la tabla Cliente
                var clienteExiste = await dbContext.Clientes.AnyAsync(c => c.CedulaAsegurado == polizaDto.CedulaAsegurado);
                if (!clienteExiste)
                    return BadRequest($"La cédula '{polizaDto.CedulaAsegurado}' no está registrada en el sistema.");

                // Mapeo de valores descriptivos a IDs
                var nuevaPoliza = new Poliza
                {
                    NumeroPoliza = polizaDto.NumeroPoliza,
                    IdTipoPoliza = tiposPoliza[polizaDto.TipoPoliza],
                    CedulaAsegurado = polizaDto.CedulaAsegurado,
                    MontoAsegurado = polizaDto.MontoAsegurado,
                    FechaVencimiento = polizaDto.FechaVencimiento,
                    FechaEmision = polizaDto.FechaEmision,
                    Prima = polizaDto.Prima,
                    Periodo = polizaDto.Periodo,
                    FechaInclusion = polizaDto.FechaInclusion,
                    IdCobertura = coberturas[polizaDto.Cobertura],
                    IdEstadoPoliza = estadosPoliza[polizaDto.EstadoPoliza],
                    IdAseguradora = aseguradoras[polizaDto.Aseguradora]
                };

                // Inserción en la base de datos
                dbContext.Polizas.Add(nuevaPoliza);
                await dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { num = nuevaPoliza.NumeroPoliza }, nuevaPoliza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "Error al crear la póliza.", Detalle = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar/{num}")]
        public async Task<IActionResult> PutPoliza(string num, PolizaDTO polizaDto)
        {
            try
            {
                // Diccionarios para mapear nombres a IDs
                var tiposPoliza = new Dictionary<string, int>
        {
            { "Vida", 1 },
            { "Automovil", 2 },
            { "Hogar", 3 }
        };

                var coberturas = new Dictionary<string, int>
        {
            { "Accidente", 1 },
            { "Robo", 2 },
            { "Incendio", 3 }
        };

                var estadosPoliza = new Dictionary<string, int>
        {
            { "Activo", 1 },
            { "Cancelado", 2 },
            { "Vencido", 3 }
        };

                var aseguradoras = new Dictionary<string, int>
        {
            { "Popular Seguros", 1 },
            { "INS", 2 },
            { "CCSS", 3 }
        };

                // Validar si la póliza existe
                var polizaExistente = await dbContext.Polizas.FirstOrDefaultAsync(p => p.NumeroPoliza == num);
                if (polizaExistente == null)
                    return NotFound($"La póliza con número '{num}' no está registrada en el sistema.");

                // Validaciones específicas
                if (!tiposPoliza.ContainsKey(polizaDto.TipoPoliza))
                    return BadRequest($"El tipo de póliza '{polizaDto.TipoPoliza}' no es válido. Debe ser Vida, Automovil o Hogar.");

                if (!coberturas.ContainsKey(polizaDto.Cobertura))
                    return BadRequest($"La cobertura '{polizaDto.Cobertura}' no es válida. Debe ser Accidente, Robo o Incendio.");

                if (!estadosPoliza.ContainsKey(polizaDto.EstadoPoliza))
                    return BadRequest($"El estado de la póliza '{polizaDto.EstadoPoliza}' no es válido. Debe ser Activo, Cancelado o Vencido.");

                if (!aseguradoras.ContainsKey(polizaDto.Aseguradora))
                    return BadRequest($"La aseguradora '{polizaDto.Aseguradora}' no es válida. Debe ser Popular Seguros, INS o CCSS.");

                if (polizaDto.MontoAsegurado <= 0)
                    return BadRequest("El monto asegurado debe ser mayor a 0.");

                if (polizaDto.Prima <= 0)
                    return BadRequest("La prima debe ser mayor a 0.");

                // Validar si la cédula del asegurado existe en la tabla Cliente
                var clienteExiste = await dbContext.Clientes.AnyAsync(c => c.CedulaAsegurado == polizaDto.CedulaAsegurado);
                if (!clienteExiste)
                    return BadRequest($"La cédula '{polizaDto.CedulaAsegurado}' no está registrada en el sistema.");

                // Actualizar los valores en la póliza existente
                polizaExistente.IdTipoPoliza = tiposPoliza[polizaDto.TipoPoliza]; // Convierte "Vida" -> 1
                polizaExistente.CedulaAsegurado = polizaDto.CedulaAsegurado;
                polizaExistente.MontoAsegurado = polizaDto.MontoAsegurado;
                polizaExistente.FechaVencimiento = polizaDto.FechaVencimiento;
                polizaExistente.FechaEmision = polizaDto.FechaEmision;
                polizaExistente.Prima = polizaDto.Prima;
                polizaExistente.Periodo = polizaDto.Periodo;
                polizaExistente.FechaInclusion = polizaDto.FechaInclusion;
                polizaExistente.IdCobertura = coberturas[polizaDto.Cobertura]; // Convierte "Accidente" -> 1
                polizaExistente.IdEstadoPoliza = estadosPoliza[polizaDto.EstadoPoliza]; // Convierte "Activo" -> 1
                polizaExistente.IdAseguradora = aseguradoras[polizaDto.Aseguradora]; // Convierte "Popular Seguros" -> 1

                // Guardar cambios en la base de datos
                dbContext.Polizas.Update(polizaExistente);
                await dbContext.SaveChangesAsync();

                return Ok(polizaExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "Error al actualizar la póliza.", Detalle = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{num}")]
        public async Task<IActionResult> Delete(string num)
        {
            try
            {
                var poliza = await dbContext.Polizas.FindAsync(num);
                if (poliza == null)
                    return NotFound($"La póliza con número '{num}' no está registrada en el sistema.");
                dbContext.Polizas.Remove(poliza);
                await dbContext.SaveChangesAsync();
                return Ok(new { Message = "Póliza eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "Error al eliminar la póliza.", Detalle = ex.Message });
            }
        }

    }
}
