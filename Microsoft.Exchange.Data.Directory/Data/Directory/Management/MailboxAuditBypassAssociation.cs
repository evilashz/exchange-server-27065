using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072B RID: 1835
	[Serializable]
	public class MailboxAuditBypassAssociation : ADPresentationObject
	{
		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x060057D6 RID: 22486 RVA: 0x001398C9 File Offset: 0x00137AC9
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailboxAuditBypassAssociation.schema;
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x001398D0 File Offset: 0x00137AD0
		public MailboxAuditBypassAssociation()
		{
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x001398D8 File Offset: 0x00137AD8
		public MailboxAuditBypassAssociation(ADRecipient dataObject) : base(dataObject)
		{
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x001398E1 File Offset: 0x00137AE1
		internal static MailboxAuditBypassAssociation FromDataObject(ADRecipient dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new MailboxAuditBypassAssociation(dataObject);
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x060057DA RID: 22490 RVA: 0x001398EE File Offset: 0x00137AEE
		public ADObjectId ObjectId
		{
			get
			{
				return (ADObjectId)this[MailboxAuditBypassAssociationSchema.ObjectId];
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x00139900 File Offset: 0x00137B00
		public bool AuditBypassEnabled
		{
			get
			{
				return (bool)this[MailboxAuditBypassAssociationSchema.AuditBypassEnabled];
			}
		}

		// Token: 0x17001DC4 RID: 7620
		// (get) Token: 0x060057DC RID: 22492 RVA: 0x00139912 File Offset: 0x00137B12
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x04003B44 RID: 15172
		private static MailboxAuditBypassAssociationSchema schema = ObjectSchema.GetInstance<MailboxAuditBypassAssociationSchema>();
	}
}
