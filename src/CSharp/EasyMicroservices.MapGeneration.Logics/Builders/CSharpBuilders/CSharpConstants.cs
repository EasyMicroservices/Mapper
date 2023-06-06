using System.Text;

namespace EasyMicroservices.MapGeneration.Builders.CSharpBuilders
{
    public static class CSharpConstants
    {
        public const string TabSpace = "    ";

        public static void AppendTabSpace(this StringBuilder builder,int count)
        {
            for (int i = 0; i < count; i++)
            {
                builder.Append(TabSpace);
            }
        }
    }
}
