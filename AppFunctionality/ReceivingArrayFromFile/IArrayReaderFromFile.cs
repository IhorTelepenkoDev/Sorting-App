
namespace AppFunctionality.ReceivingArrayFromFile
{
    internal interface IArrayReaderFromFile<T>
    {
        string filePath { get; }

        T[,] Receive2DArrayFromFile();
    }
}
