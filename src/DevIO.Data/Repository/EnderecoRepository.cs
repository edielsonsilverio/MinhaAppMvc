using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId) =>
            await ObterPorFiltro(f => f.FornecedorId == fornecedorId);
    }
}