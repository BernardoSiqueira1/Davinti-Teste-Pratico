using Davinti___Teste_Pratico.Domain;
using Davinti___Teste_Pratico.Infra.Repository;
using Davinti___Teste_Pratico.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Davinti___Teste_Pratico.Pages;

public class BuscarContato : PageModel
{
    private readonly ContatoService _service;

    [BindProperty]
    public string TermoBusca { get; set; } = string.Empty;

    public List<Contato> ContatosEncontrados { get; set; } = new();

    public BuscarContato(ContatoService service)
    {
        _service = service;
    }

    public async Task OnPost()
    {
        if (!string.IsNullOrWhiteSpace(TermoBusca))
        {
            var resultado = await _service.ObterContatosNomeOuTelefone(TermoBusca);
            ContatosEncontrados = resultado.ToList();
        }
    }

    public async Task<IActionResult> OnPostExcluirAsync(int id)
    {
        await _service.RemoverContato(id);
        
        if (!string.IsNullOrWhiteSpace(TermoBusca))
        {
            var resultado = await _service.ObterContatosNomeOuTelefone(TermoBusca);
            ContatosEncontrados = resultado.ToList();
        }

        return Page();
    }
}