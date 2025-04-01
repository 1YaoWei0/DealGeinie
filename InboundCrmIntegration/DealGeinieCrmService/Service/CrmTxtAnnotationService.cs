using DealGeinieCrmService.Authentications;
using DealGeinieCrmService.Models;
using System;

namespace DealGeinieCrmService.Services
{
    /// <summary>
    /// This class is responsible for creating a new txt annotation in CRM.
    /// Willie Yao - 02/21/2025
    /// </summary>
    public class CrmTxtAnnotationService : CrmAnnotationService
    {
        /// <summary>
        /// Default constructor.
        /// Willie Yao - 02/21/2025
        /// </summary>
        /// <param name="_crmCredential">CrmDevCredential</param>
        public CrmTxtAnnotationService() : base()
        {
        }

        /// <summary>
        /// Create a new annotation in CRM.
        /// Willie Yao - 02/21/2025
        /// </summary>
        /// <returns></returns>
        public override bool CreateAnnotation(Guid _guid)
        {
            //var annotation = (CrmTxtAnnotationEntity) crmEntity;
            //var note = annotation.GetEntity();

            //try
            //{
            //    note["objectid"] = _guid;
            //    note["objecttypecode"] = annotation.ObjectTypeCode;
            //    note["subject"] = annotation.Subject;
            //    note["notetext"] = annotation.NoteText;

            //    Guid noteId = organizationServiceProxy.Create(note);
            //}
            //catch (Exception ex)
            //{
            //    var errorMsg = ex.Message;

            //    return false;
            //}

            return true;
        }

        public override bool GetOriganizationServiceProxy()
        {
            var crmDevCredential = (CrmDevCredential) crmCredential;
            if (crmDevCredential == null)
            {
                return false;
            }
            organizationServiceProxy = crmDevCredential.OrganizationServiceProxy;
            return organizationServiceProxy != null;
        }
    }
}
