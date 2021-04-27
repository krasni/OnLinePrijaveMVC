using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnLinePrijaveMVC.HelperClasses
{
    public class Attachment
    {
        public Attachment(string fileName, byte[] content)
        {
            Content = content;
            FileName = fileName;
        }

        public MemoryStream ContentToStream()
        {
            var stream = new MemoryStream(Content)
            {
                Position = 0
            };
            return stream;
        }

        public byte[] Content { get; set; }
        public string FileName { get; set; }
    }
}