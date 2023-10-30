using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Data.Providers.MongoDb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace H1Store.Catalogo.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProdutoController : ControllerBase
	{
		private readonly IProdutoService _produtoService;
		public ProdutoController(IProdutoService produtoService)
		{
			_produtoService = produtoService;
		}

		[HttpPost]
		[Route("Adicionar")]
		public async Task<IActionResult> Post(NovoProdutoViewModel novoProdutoViewModel)
		{
			await _produtoService.Adicionar(novoProdutoViewModel);

			return Ok();
		}

		[HttpPut]
		[Route("Desativar/{id}")]
		public async Task<IActionResult> Put(Guid id)
		{
			await _produtoService.Desativar(id);

			return Ok("Produto desativado com sucesso");
		}
		
		[HttpPut]
		[Route("Reativar/{id}")]
		public async Task<IActionResult> Reativar(Guid id)
		{
			await _produtoService.Reativar(id);

			return Ok("Produto desativado com sucesso");
		}
		
		[HttpPut]
		[Route("Atualizar")]
		public async Task<IActionResult> Atualizar(ProdutoViewModel produto)
		{
			await _produtoService.Atualizar(produto);

			return Ok("Produto atualizado com sucesso");
		}

		[HttpPut]
		[Route("AlterarPreco")]
		public async Task<IActionResult> AlterarPreco(Guid CodigoId, decimal valor )
		{
			await _produtoService.AlterarPreco(CodigoId, valor);

			return Ok("Produto atualizado com sucesso");
		}
		[HttpPut]
		[Route("AtualizarEstoque")]
		public async Task<IActionResult> AtualizarEstoque(Guid CodigoId, int estoque)
		{
			await _produtoService.AtualizarEstoque(CodigoId, estoque);

			return Ok("Produto atualizado com sucesso");
		}

		[HttpGet]
		[Route("ObterTodos")]
		public IActionResult Get()
		{
			return Ok(_produtoService.ObterTodos());
		}
		
		[HttpGet]
		[Route("ObterPorId")]
		public IActionResult ObterPorId(Guid id)
		{
			return Ok(_produtoService.ObterPorId(id));
		}
		
		[HttpGet]
		[Route("ObterPorNome")]
		public IActionResult ObterPorNome(string nome)
		{
			return Ok(_produtoService.ObterPorNome(nome));
		}
	}
}
