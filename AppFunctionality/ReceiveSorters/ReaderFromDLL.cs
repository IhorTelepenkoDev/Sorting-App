using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BaseSort;

namespace AppFunctionality.ReceiveSorters
{
    public class ReaderFromDLL
    {
        public ISorter2D[] GetInstancesOfSortersInFolder(string pathToFolderWithDLL)
        {
            var allDllFiles = GetPathesToAllDllFilesInGivenFolder(pathToFolderWithDLL);
            try
            {
                object[] constructorArgs = null;

                var resultInstances = (from file in allDllFiles
                                       let asm = Assembly.LoadFile(file)
                                       from type in asm.GetExportedTypes()
                                       where typeof(ISorter2D).IsAssignableFrom(type)
                                       select (ISorter2D)Activator.CreateInstance(type, constructorArgs)).ToArray();

                return resultInstances;
            }
            catch
            {
                return null;
            }
        }

        private static string[] GetPathesToAllDllFilesInGivenFolder(string pathToFolder)
        {
            return Directory.GetFiles(pathToFolder, "*.dll");
        }
    }
}
