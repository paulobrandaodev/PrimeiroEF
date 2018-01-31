using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PrimeiroEF.Dados;
using PrimeiroEF.Models;

namespace PrimeiroEF.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        Cliente cliente = new Cliente();
        readonly ClienteContexto contexto;

        public ClienteController(ClienteContexto contexto){
            this.contexto = contexto;
        }

        [HttpGet]
        public IEnumerable<Cliente> Listar(){
            return contexto.ClienteNaBase.ToList();
        }

        [HttpGet("{id}")]
        public Cliente Listar(int id){
            return contexto.ClienteNaBase.Where( x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public void Cadastrar([FromBody] Cliente cli){
            contexto.ClienteNaBase.Add(cli);
            contexto.SaveChanges();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id,[FromBody] Cliente cliente){
            if(cliente == null || cliente.Id != id){
                return BadRequest();
            }
            var cli = contexto.ClienteNaBase.FirstOrDefault(x => x.Id == id);
            if(cli == null){
                return NotFound();
            }

            cli.Id           = cliente.Id;
            cli.Nome         = cliente.Nome;
            cli.Email        = cliente.Email;
            cli.Idade        = cliente.Idade;
            cli.DataCadastro = cliente.DataCadastro;

            contexto.ClienteNaBase.Update(cli);
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public IActionResult Apagar(int id){
            var cliente = contexto.ClienteNaBase.Where( x => x.Id == id).FirstOrDefault();
            if(cliente == null){
                return NotFound();
            }

            contexto.ClienteNaBase.Remove(cliente);    
            int rs = contexto.SaveChanges();

            if(rs > 0)
            return Ok();
            else
            return BadRequest();
        }
        

    }
}