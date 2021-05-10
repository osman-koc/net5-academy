using System;
using System.IO;

namespace NET5Academy.Services.PhotoStock.Application.Dtos
{
    public class PhotoDto
    {
        public Guid Id { get; protected set; }
        public string FullPath { get; protected set; }
        public string FileName { get; protected set; }
        public string OriginalName { get; protected set; }

        public PhotoDto(string fileName)
        {
            Id = Guid.NewGuid();
            OriginalName = fileName;
            FileName = Id.ToString() + Path.GetExtension(fileName);
            FullPath = "photos/" + FileName;
        }
    }
}
