using System.Threading.Channels;

namespace FormulaAirline.API.Services;
public interface IMessageProduser
{
    public void SendingMessages<T> (T message);
}
