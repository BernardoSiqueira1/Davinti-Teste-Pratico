using Davinti___Teste_Pratico.Domain;
using Davinti___Teste_Pratico.Infra.Repository;

namespace Davinti___Teste_Pratico.Services;

public class ContatoService
{
    
    private readonly ContatoRepository _contatoRepository;
    private readonly TelefoneRepository _telefoneRepository;
    
    private readonly LoggingService _service;

    public ContatoService(ContatoRepository contatoRepository, TelefoneRepository telefoneRepository, LoggingService service)
    {
        _contatoRepository = contatoRepository;
        _telefoneRepository = telefoneRepository;
        _service = service;
    }

    
    
    public async Task<Contato> ObterContato(int id)
    {
        var contato = await _contatoRepository.ObterPorId(id);
        return contato;
    }

    
    
    public async Task<IEnumerable<Contato>> ObterContatosNomeOuTelefone(String termo)
    {
        return await _contatoRepository.ObterPorNomeOuTelefone(termo);
    }
    
    
    public async Task AdicionarNovoContato(Contato contato)
    {
        int idContato = await _contatoRepository.Inserir(contato);
        await _telefoneRepository.Inserir(contato, idContato);
    }

    
    
    public async Task RemoverContato(int id)
    {
       await _contatoRepository.Excluir(id);
       await _service.LogExclusaoContato(id);

    }

    
    
    public async Task EditarContato(Contato contato)
    {
        await _contatoRepository.Atualizar(contato);
    }

}