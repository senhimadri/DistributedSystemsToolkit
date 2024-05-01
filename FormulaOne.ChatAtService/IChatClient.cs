namespace FormulaOne.ChatAtService;

public interface IChatClient
{
    Task ReceiveMessage(string message);
}

