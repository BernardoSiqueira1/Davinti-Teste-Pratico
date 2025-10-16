using Davinti___Teste_Pratico.Domain;
using Davinti___Teste_Pratico.Infra.Repository;
using Davinti___Teste_Pratico.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Davinti___Teste_Pratico.Pages;

public class Editar : PageModel
{
    private readonly ContatoService _service;

    [BindProperty]
    public Contato contato { get; set; } = new Contato();

    public Editar(ContatoService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        contato = await _service.ObterContato(id);

        if (contato == null)
            return NotFound();
        
        contato.Telefones ??= new List<Telefone>();

        return Page();
    }

    public IActionResult OnPostAddTelefone()
    {
        contato.Telefones ??= new List<Telefone>();
        contato.Telefones.Add(new Telefone());
        
        return Page();
    }

    public async Task<IActionResult> OnPostSalvarAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        await _service.EditarContato(contato);
        
        return RedirectToPage("/Contatos/BuscarContato");
    }
}