using DealGeinieCrmService.Base;
using Microsoft.Xrm.Sdk;
using System;

namespace DealGeinieCrmService.Models
{
    public class FileAttachment
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
    }

    /// <summary>
    /// Represents a CRM annotation entity.
    /// Willie Yao - 02/21/2025
    /// </summary>
    public class CrmAnnotationEntity
    {
        /// <summary>
        /// Entity name stored the crm annotation table name
        /// </summary>
        protected const string EntityName = "annotation";

        /// <summary>
        /// Annotation type.
        /// </summary>
        public AnnotationType AnnotationType { get; set; }

        public string Subject { get; set; }

        public string NoteText { get; set; }

        public bool IsDocument { get; set; }

        public string FileName { get; set; }

        public MimeType MIMEtype { get; set; }

        public string DocumentBody { get; set; }

        public Entity GetEntity()
        {
            return new Entity(EntityName);
        }
    }
}
