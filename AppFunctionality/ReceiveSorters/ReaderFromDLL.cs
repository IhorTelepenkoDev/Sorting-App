using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AppFunctionality.ReceiveSorters
{
    public class ReaderFromDLL
    {
        public InterfaceOfTargetClasses[] GetInstancesOfSpecificClassesInFolder<InterfaceOfTargetClasses>(string pathToFolderWithDLL,
                                                                        object[] constructorArgs = null) where InterfaceOfTargetClasses : class
        {
            var allDllFiles = AllDllFilesInGivenFolder(pathToFolderWithDLL);
            try
            {
                var resultInstances = (
                    from file in allDllFiles
                    let asm = Assembly.LoadFile(file)
                    from type in asm.GetExportedTypes()
                    where typeof(InterfaceOfTargetClasses).IsAssignableFrom(type)
                    select (InterfaceOfTargetClasses)Activator.CreateInstance(type, constructorArgs)
                ).ToArray();

                return resultInstances;
            }
            catch
            {
                return null;
            }
        }

        private static string[] AllDllFilesInGivenFolder(string pathToFolder)
        {
            return Directory.GetFiles(pathToFolder, "*.dll");
        }
    }
}
