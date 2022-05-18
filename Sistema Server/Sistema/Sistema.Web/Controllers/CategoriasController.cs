using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Denuncia;
using Sistema.Web.Models.Denuncia;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CategoriasController(DbContextSistema context)
        {
            _context = context;
        }

        // GET: api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<CategoriaViewModel>> Listar()
        {
            var categoria = await _context.Categorias.ToArrayAsync();

            return categoria.Select(c=> new CategoriaViewModel{
                iddenuncia = c.iddenuncia,
                nombre = c.nombre,
                apellido = c.apellido,
                cargo = c.cargo,
                entidad = c.entidad,
                fecha = c.fecha,
                hora = c.hora,
                lugar = c.hora,
                descripcion = c.descripcion
            });
        }

        // GET: api/Categorias/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaViewModel {
               iddenuncia = categoria.iddenuncia,
               nombre = categoria.nombre,
               apellido = categoria.apellido,
               cargo = categoria.cargo,
               entidad = categoria.entidad,
               fecha = categoria.fecha,
               hora = categoria.hora,
               lugar = categoria.lugar,
               descripcion = categoria.descripcion

               

            });
        }

        // PUT: api/Categorias/Actualizar/id
        /* [HttpGet("[action]")]
         public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (model.iddenuncia < = 0)
             {
                 return BadRequest();
             }

             var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.iddenuncia == model.iddenuncia);

             if (categoria == null)
             {
                 return NotFound();
             }

             categoria.nombre = model.nombre;
             categoria.apellido = model.apellido;
             categoria.cargo = model.cargo;
             categoria.entidad = model.entidad;
             categoria.fecha = model.fecha;
             categoria.hora = model.hora;
             categoria.lugar = model.lugar;
             categoria.descripcion = model.descripcion;

             _context.Entry(categoria).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 //Guardar Excepcion
                 return BadRequest();
             }

             return Ok();
         }*/

        // PUT: api/Categorias/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.iddenuncia <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.iddenuncia == model.iddenuncia);

            if (categoria == null)
            {
                return NotFound();
            }

        
            categoria.nombre = model.nombre;
            categoria.apellido = model.apellido;
            categoria.cargo = model.cargo;
            categoria.entidad = model.entidad;
            categoria.fecha = model.fecha;
            categoria.hora = model.hora;
            categoria.lugar = model.lugar;
            categoria.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }




        // POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Categoria categoria = new Categoria
            {
                nombre = model.nombre,
                apellido = model.apellido,
                cargo = model.cargo,
                entidad = model.entidad,
                fecha = model.fecha,
                hora = model.hora,
                lugar = model.lugar,
                descripcion = model.descripcion
            };

            _context.Categorias.Add(categoria);
            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
                     
        }

        // DELETE: api/Categorias/Eliminar/2
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(categoria);
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.iddenuncia == id);
        }
    }
}