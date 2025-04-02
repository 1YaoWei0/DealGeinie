using DealGeinieCrmService.Authentications;
using DealGeinieCrmService.Base;
using DealGeinieCrmService.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.IO;
using System.Web.Services.Description;

namespace DealGeinieCrmService.Services
{
    /// <summary>
    /// This class is responsible for creating a new txt annotation in CRM.
    /// Willie Yao - 02/21/2025
    /// </summary>
    public class CrmTxtAnnotationService
    {
        /// <summary>
        /// Create a new annotation in CRM.
        /// Willie Yao - 02/21/2025
        /// </summary>
        /// <returns></returns>
        public Entity ConstructAnnotation(
            Guid guid, 
            string entityName,
            CrmAnnotationEntity txtEntity)
        {
            var note = txtEntity.GetEntity();

            try
            {
                note["objectid"] = new EntityReference(entityName, guid);
                note["subject"] = txtEntity.Subject;
                note["notetext"] = txtEntity.NoteText;
                note["isdocument"] = txtEntity.IsDocument;

                if (txtEntity.IsDocument)
                {
                    note["filename"] = txtEntity.FileName;
                    note["documentbody"] = txtEntity.DocumentBody;
                    note["mimetype"] = MimeTypeHelper.GetMimeString(txtEntity.MIMEtype);                   
                }
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
            }

            return note;
        }
    }
}
