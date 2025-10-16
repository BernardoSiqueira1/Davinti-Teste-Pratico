using System.Data;
using Dapper;
using Davinti___Teste_Pratico.Domain;

namespace Davinti___Teste_Pratico.Infra.Repository;

public class ContatoRepository
{

    private readonly IDbConnection _dbConnection;

    public ContatoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<int> Inserir(Contato contato)
    {
        var sqlQueryContato = "INSERT INTO Contato (nome, idade) VALUES (@nome, @idade); SELECT LAST_INSERT_ID();";
        var contatoId = await _dbConnection.ExecuteScalarAsync(sqlQueryContato, contato);

        return Convert.ToInt32(contatoId);
    }

    public async Task<Contato> ObterPorId(int id)
    {
        var sql = @"
        SELECT c.id, c.nome, c.idade,
               t.id AS Id,
               t.numero AS NumeroContato,
               t.idcontato AS IdContato
        FROM Contato c
        LEFT JOIN Telefone t ON t.idcontato = c.id
        WHERE c.id = @id;
    ";

        var lookup = new Dictionary<int, Contato>();

        var result = await _dbConnection.QueryAsync<Contato, Telefone, Contato>(
            sql,
            (contato, telefone) =>
            {
                if (!lookup.TryGetValue(contato.Id, out var contatoEntry))
                {
                    contatoEntry = contato;
                    contatoEntry.Telefones = new List<Telefone>();
                    lookup.Add(contatoEntry.Id, contatoEntry);
                }

                if (telefone != null && telefone.Id != 0)
                    contatoEntry.Telefones.Add(telefone);

                return contatoEntry;
            },
            new { id },
            splitOn: "Id"
        );

        return lookup.Values.FirstOrDefault();
    }
    
    public async Task<IEnumerable<Contato>> ObterPorNomeOuTelefone(string busca)
    {
        var sql = @"
        SELECT c.id, c.nome, c.idade, t.id as Id, t.numero AS NumeroContato
        FROM Contato c
        LEFT JOIN Telefone t ON t.idcontato = c.Id
        WHERE c.nome LIKE @busca OR t.numero LIKE @busca;
    ";

        var resultado = await _dbConnection.QueryAsync<Contato, Telefone, Contato>(
            sql,
            (contato, telefone) =>
            {
                contato.Telefones??= new List<Telefone>();
                if (telefone != null)
                    contato.Telefones.Add(telefone);
                return contato;
            },
            new { Busca = $"%{busca}%" },
            splitOn: "Id"
        );
        
        return resultado
            .GroupBy(c => c.Id)
            .Select(g =>
            {
                var contato = g.First();
                contato.Telefones = g.SelectMany(x => x.Telefones).Distinct().ToList();
                return contato;
            });
    }

    public async Task Atualizar(Contato contato)
    {
        var sqlContato = "UPDATE Contato SET nome = @Nome, idade = @Idade WHERE id = @Id";
        await _dbConnection.ExecuteAsync(sqlContato, contato);

        if (contato.Telefones != null && contato.Telefones.Any())
        {
            foreach (var tel in contato.Telefones)
            {
                var sqlTelefone = "UPDATE Telefone SET numero = @NumeroContato WHERE id = @Id";
                await _dbConnection.ExecuteAsync(sqlTelefone, tel);
            }
        }
    }

    public async Task Excluir(int id)
    {
        var sqlTelefones = "DELETE FROM Telefone WHERE IdContato = @Id";
        await _dbConnection.ExecuteAsync(sqlTelefones, new { Id = id });
        
        var sqlContato = "DELETE FROM Contato WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(sqlContato, new { Id = id });
    }


}