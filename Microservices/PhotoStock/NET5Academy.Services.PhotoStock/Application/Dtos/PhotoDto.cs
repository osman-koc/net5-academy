using System;
using System.IO;

namespace NET5Academy.Services.PhotoStock.Application.Dtos
{
    public class PhotoDto
    {
        public Guid Id { get; protected set; }
        public string NewPath { get; protected set; }
        public string FileName { get; protected set; }

        public PhotoDto(string fileName)
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            NewPath = $"photos/{Id}.{Path.GetExtension(fileName)}";
        }
    }
}
