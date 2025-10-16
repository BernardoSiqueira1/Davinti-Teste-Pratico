using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace Davinti___Teste_Pratico.Services;

public class LoggingService
{
    private string _caminhoLog;
    public async Task LogExclusaoContato(int id)
    {
        string logMessage = String.Concat("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "]", " Um contato foi excluído - Id do contato: ", id);

        await File.AppendAllTextAsync(_caminhoLog, logMessage, Encoding.UTF8);
    }

    public LoggingService()
    {
        string basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        _caminhoLog = Path.Combine(basePath, "Logs", "logexclusao.txt");
    }
    
}