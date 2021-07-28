
namespace AppFunctionality.ReceiveArrayFromFile
{
    internal interface IArrayReader<T>
    {
        T[,] Read2DArray(string receivedText);
    }
}
