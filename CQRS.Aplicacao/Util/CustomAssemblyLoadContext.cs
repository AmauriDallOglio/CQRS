using System.Reflection;
using System.Runtime.Loader;

namespace CQRS.Aplicacao.Util
{

    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string caminho )
        {


            if (!File.Exists(caminho))
                throw new FileNotFoundException($"DLL nativa não encontrada: {caminho}");

            return LoadUnmanagedDllFromPath(caminho);
        }
 
        protected override Assembly Load(AssemblyName assemblyName)
        {
            // Não carrega assemblies gerenciados aqui
            return null;
        }



        //public IntPtr LoadUnmanagedLibrary(string absolutePath)
        //{
        //    return LoadUnmanagedDll(absolutePath);
        //}

        //protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath)
        //{
        //    var customPath = Path.Combine(@"C:\\Amauri\\GitHub\\CQRS\\CQRS\\CQRS.Aplicacao\\DinkToPdfLib\\libwkhtmltox.dll", unmanagedDllPath);


        //    return LoadUnmanagedDllFromPath(customPath);
        //}

        //protected override Assembly Load(AssemblyName assemblyName)
        //{
        //    return null;
        //}



    }



}
