using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id) =>
            await ObterPorFiltro(p => p.Id == id, includeProperties: "Fornecedor");

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores() =>
            await ObterTodos(includeProperties: "Fornecedor", orderBy: x => x.OrderBy(p => p.Nome));

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId) =>
            await ObterTodos(p => p.FornecedorId == fornecedorId);
    }
}