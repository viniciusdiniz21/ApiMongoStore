using AutoMapper;
using H1Store.Catalogo.Application.Interfaces;
using H1Store.Catalogo.Application.ViewModels;
using H1Store.Catalogo.Domain.Entities;
using H1Store.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Services
{
	public class ProdutoService : IProdutoService
	{
		#region - Construtores
		private readonly IProdutoRepository _produtoRepository;
		private IMapper _mapper;

		public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
		}
		#endregion

		#region - Funções
		public async Task Adicionar(NovoProdutoViewModel novoProdutoViewModel)
		{
			var novoProduto = _mapper.Map<Produto>(novoProdutoViewModel);

			Produto p = new Produto(novoProdutoViewModel.Descricao, novoProdutoViewModel.Descricao, novoProdutoViewModel.Ativo, novoProdutoViewModel.Valor, novoProdutoViewModel.DataCadastro, novoProdutoViewModel.Imagem, novoProdutoViewModel.QuantidadeEstoque);

			await _produtoRepository.Adicionar(novoProduto);

		}

		public async Task Atualizar(ProdutoViewModel produto)
		{
            var buscaProduto = await _produtoRepository.ObterPorId(produto.CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível atualizar um produto que não existe!");

            var novoProduto = _mapper.Map<Produto>(produto);

            await _produtoRepository.Atualizar(novoProduto);
        }

		public async Task Desativar(Guid id)
		{
			var buscaProduto = await _produtoRepository.ObterPorId(id);

			if(buscaProduto == null)  throw new ApplicationException("Não é possível desativar um produto que não existe!");
			
			buscaProduto.Desativar();

			await _produtoRepository.Desativar(buscaProduto);

		}
		public async Task Reativar(Guid id)
		{
			var buscaProduto = await _produtoRepository.ObterPorId(id);

			if(buscaProduto == null)  throw new ApplicationException("Não é possível desativar um produto que não existe!");
			
			buscaProduto.Ativar();

			await _produtoRepository.Desativar(buscaProduto);

		}

		public Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
		{
			throw new NotImplementedException();
		}

		public async Task<ProdutoViewModel> ObterPorId(Guid id)
		{
            return _mapper.Map<ProdutoViewModel>(_produtoRepository.ObterPorId(id));
        }

		public IEnumerable<ProdutoViewModel> ObterTodos()
		{
			return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterTodos());
		}

		public async Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string nome)
		{
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(_produtoRepository.ObterPorNome(nome));
        }

		public async Task AlterarPreco(Guid CodigoId, decimal valor)
		{
            var buscaProduto = await _produtoRepository.ObterPorId(CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível atualizar o valor de um produto que não existe!");

            if(buscaProduto.Valor <= valor) throw new ApplicationException("Não é possível definir o valor como menor ou igual a 0!");

			buscaProduto.AlterarValor(valor);
            
            await _produtoRepository.Atualizar(buscaProduto);
        }

		public async Task AtualizarEstoque(Guid CodigoId, int estoque)
		{
            var buscaProduto = await _produtoRepository.ObterPorId(CodigoId);

            if (buscaProduto == null) throw new ApplicationException("Não é possível atualizar o estoque de um produto que não existe!");

			if (estoque < 0 && buscaProduto.QuantidadeEstoque <= estoque * -1) throw new ApplicationException("Não é possível definir o estoque como menor que 0!");

			if (estoque < 0) buscaProduto.DebitarEstoque(estoque);

            if (estoque > 0) buscaProduto.ReporEstoque(estoque);

            await _produtoRepository.Atualizar(buscaProduto);
        }
		#endregion
	}
}
