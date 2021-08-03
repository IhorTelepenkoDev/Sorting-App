
namespace AppFunctionality.ReceiveArrayFromFile
{
    public interface IArrayReader<T>
    {
        T[,] Read2DArray(string receivedText);
    }
}
