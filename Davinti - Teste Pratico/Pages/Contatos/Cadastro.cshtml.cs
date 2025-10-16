using Davinti___Teste_Pratico.Domain;
using Davinti___Teste_Pratico.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Davinti___Teste_Pratico.Pages;

public class Cadastro : PageModel
{
    
    private readonly ContatoService _service;
    
    [BindProperty]
    public Contato contato { get; set; } = new Contato();

    public Cadastro(ContatoService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        if (contato.Telefones.Count == 0)
            contato.Telefones.Add(new Telefone());
    }

    public IActionResult OnPostAddTelefone()
    {
        contato.Telefones.Add(new Telefone());
        return Page();
    }

    public async Task<IActionResult> OnPostSalvar()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _service.AdicionarNovoContato(contato);
        return RedirectToPage("/Index");
    }
    
}