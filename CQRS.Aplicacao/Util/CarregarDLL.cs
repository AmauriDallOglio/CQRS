using System.Reflection;
using System.Runtime.Loader;

namespace CQRS.Aplicacao.Util
{

    public class CarregarDLL : AssemblyLoadContext
    {
        public IntPtr Carregar(string caminho )
        {


            if (!File.Exists(caminho))
                throw new FileNotFoundException($"DLL nativa não encontrada: {caminho}");

            return LoadUnmanagedDllFromPath(caminho);
        }
 
        protected override Assembly Load(AssemblyName assemblyName)
        {
            return null;
        }




    }



}
