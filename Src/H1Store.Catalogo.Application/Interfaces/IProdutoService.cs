using H1Store.Catalogo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Store.Catalogo.Application.Interfaces
{
	public interface IProdutoService
	{
		IEnumerable<ProdutoViewModel> ObterTodos();
		Task<ProdutoViewModel> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoViewModel>> ObterPorNome(string nome);
        Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo);

		Task Adicionar(NovoProdutoViewModel produto);
		Task Atualizar(ProdutoViewModel produto);
		Task AlterarPreco(Guid CodigoId, decimal valor);
		Task AtualizarEstoque(Guid CodigoId, int estoque);
		Task Desativar(Guid id);
		Task Reativar(Guid id);
	}
}
