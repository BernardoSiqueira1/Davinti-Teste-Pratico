using System.Data;
using Dapper;
using Davinti___Teste_Pratico.Domain;

namespace Davinti___Teste_Pratico.Infra.Repository;

public class TelefoneRepository
{
    
    private readonly IDbConnection _dbConnection;

    public TelefoneRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Inserir(Contato contato, int idContato)
    {
        
        var sqlQueryTelefone = "INSERT INTO Telefone (idcontato, numero) VALUES (@IdContato, @NumeroContato)";
        foreach (var tel in contato.Telefones)
        {
            tel.IdContato = (int) idContato;
            await _dbConnection.ExecuteAsync(sqlQueryTelefone, tel);
        }
        
    }
    
}