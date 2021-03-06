using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AppFunctionality.Logging;
using BaseSort;

namespace AppFunctionality.ReceiveSorters
{
    public class DLLSortersReader
    {
        private readonly ILogger log;

        public DLLSortersReader()
        {
            log = Logger.GetInstance();
        }

        public ISorter2D[] GetInstancesOfSortersInFolder(string pathToFolderWithDLLs)
        {
            var allDllFiles = GetPathsToAllDllFilesInGivenFolder(pathToFolderWithDLLs);
            try
            {
                const object[] constructorArgs = null;

                var resultInstances = (from file in allDllFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetExportedTypes()
                    where typeof(ISorter2D).IsAssignableFrom(type)
                    select (ISorter2D) Activator.CreateInstance(type, constructorArgs)).ToArray();

                log.Debug("DLLs with sorters are successfully read from the folder");
                return resultInstances;
            }
            catch (FileNotFoundException)
            {
                log.Error("Path to DLL file is wrong, cannot be checked for sorter");
            }
            catch (ArgumentException)
            {
                log.Error("Wrong format of file path, DLL file cannot be checked");
            }
            catch (FileLoadException)
            {
                log.Warn("Cannot load the DLL file");
            }
            catch (Exception e)
            {
                log.Warn($"DLLs with sorters are not read: {e}");
            }

            return null;
        }

        private static string[] GetPathsToAllDllFilesInGivenFolder(string pathToFolder)
        {
            return Directory.GetFiles(pathToFolder, "*.dll");
        }
    }
}
