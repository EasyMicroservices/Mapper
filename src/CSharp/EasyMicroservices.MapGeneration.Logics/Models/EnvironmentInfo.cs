using EasyMicroservices.MapGeneration.Options;
using System;
using System.Collections.Generic;

namespace EasyMicroservices.MapGeneration.Models
{
    public class EnvironmentInfo
    {
        public string Name { get; set; }
        public string GenerationPath { get; set; }
        public string BuildPath { get; set; }
        public List<string> NameSpaces { get; set; } = new List<string>();
        public List<GroupMapInfo> Groups { get; set; } = new List<GroupMapInfo>();


        public string GetGenerationPath()
        {
            return GenerationPath.Replace(PathConstants.ExecutionPath, AppDomain.CurrentDomain.BaseDirectory);
        }

        public string GetBuildPath()
        {
            return BuildPath.Replace(PathConstants.ExecutionPath, AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
