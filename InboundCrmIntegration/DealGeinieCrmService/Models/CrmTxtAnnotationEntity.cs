using Microsoft.Xrm.Sdk;
using System;

namespace DealGeinieCrmService.Models
{
    /// <summary>
    /// Represents a CRM annotation entity.
    /// Willie Yao - 02/21/2025
    /// </summary>
    public class CrmTxtAnnotationEntity : ICrmEntity
    {
        public static string EntityType = "annotation";

        public Guid ObjectID { get; set; }

        public string ObjectTypeCode { get; set; }

        public string Subject { get; set; }

        public string NoteText { get; set; }

        /// <summary>
        /// Default constructor.
        /// Willie Yao - 02/21/2025
        /// </summary>
        /// <param name="objectID">objectID</param>
        /// <param name="objectTypeCode">objectTypeCode</param>
        /// <param name="subject">subject</param>
        /// <param name="noteText">noteText</param>
        public CrmTxtAnnotationEntity(Guid objectID, string objectTypeCode, string subject, string noteText)
        {
            ObjectID = objectID;
            ObjectTypeCode = objectTypeCode;
            Subject = subject;
            NoteText = noteText;
        }

        public Entity GetEntity()
        {
            return new Entity(EntityType);
        }
    }
}
