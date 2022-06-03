using System.Collections.Generic;

namespace backend.References
{
    public class FileTypes
    {
        public const string CSV = "csv";
        public const string XLSX = "xlsx";

        public static readonly Dictionary<string, string> FileTypeToContentTypeMap = new Dictionary<string, string>
        {
            { CSV, ContentTypes.Csv },
            { XLSX, ContentTypes.Excel }
        };
    }
}