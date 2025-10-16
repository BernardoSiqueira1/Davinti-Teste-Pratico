namespace Davinti___Teste_Pratico.Domain;

public class Contato
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    public byte Idade { get; set; }
    
    public List<Telefone> Telefones { get; set; } = new List<Telefone>();
    public Contato()
    {
        
    }

}