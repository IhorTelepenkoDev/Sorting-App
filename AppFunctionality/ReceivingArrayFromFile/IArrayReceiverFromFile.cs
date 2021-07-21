
namespace AppFunctionality.ReceivingArrayFromFile
{
    internal interface IArrayReceiverFromFile<T>
    {
        string filePath { get; }

        T[,] Receive2DArrayFromFile();
    }
}
