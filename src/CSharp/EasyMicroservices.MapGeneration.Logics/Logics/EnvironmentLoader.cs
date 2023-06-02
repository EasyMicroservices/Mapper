using EasyMicroservices.FileManager.Interfaces;
using EasyMicroservices.MapGeneration.Models;
using EasyMicroservices.Serialization.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.MapGeneration.Logics
{
    public class EnvironmentLoader
    {
        ITextSerialization _textSerialization;
        IFileManagerProvider _fileManagerProvider;

        public MapperAppData AppData { get; set; } = new MapperAppData();

        public EnvironmentLoader(ITextSerialization textSerialization, IFileManagerProvider fileManagerProvider)
        {
            _textSerialization = textSerialization;
            _fileManagerProvider = fileManagerProvider;
        }

        public async Task Load(string filePath)
        {
            var json = await _fileManagerProvider.ReadAllTextAsync(filePath, Encoding.UTF8);
            try
            {
                AppData = _textSerialization.Deserialize<MapperAppData>(json);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
