using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace EasyMicroservices.MapGeneration.Engines
{
    public class AssemblyLoader : AssemblyLoadContext, IDisposable
    {
        public string AssemblyFileName { get; set; }
        public string DirectoryPath { get; set; }
        Assembly LoadedMainAssembly { get; set; }
        public AssemblyLoader(string assemblyFileName)
        {
            AssemblyFileName = assemblyFileName;
            DirectoryPath = Path.GetDirectoryName(AssemblyFileName);
            LoadedMainAssembly = LoadFromAssemblyPath(AssemblyFileName);
        }

        readonly List<Assembly> _loadedAssemblies = new List<Assembly>();

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string path = Path.Combine(DirectoryPath, assemblyName.Name + ".dll");
            if (!File.Exists(path))
            {
                path = Path.Combine(DirectoryPath, assemblyName.Name + ".exe");
                if (!File.Exists(path))
                {
                    return Loaded(Assembly.Load(assemblyName));
                }
            }
            return Loaded(LoadFromAssemblyPath(path));
        }

        Assembly Loaded(Assembly assembly)
        {
            if (!_loadedAssemblies.Contains(assembly))
                _loadedAssemblies.Add(assembly);
            return assembly;
        }

        public Type FindType(string nameSpace, string name)
        {
            return FindTypes(nameSpace, name).FirstOrDefault();
        }

        public IEnumerable<Type> FindTypes(string nameSpace, string name)
        {
            foreach (var type in LoadedMainAssembly.GetTypes())
            {
                if (type.Namespace == nameSpace && type.Name == name)
                    yield return type;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Dispose()
        {
            try
            {
                Unload();
            }
            catch
            {

            }
        }
    }
}
