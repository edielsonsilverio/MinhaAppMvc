using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id) =>
            await ObterPorFiltro(p => p.Id == id, includeProperties: "Endereco");

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id) =>
            await ObterPorFiltro(p => p.Id == id, includeProperties: "Endereco,Produtos");
    }
}