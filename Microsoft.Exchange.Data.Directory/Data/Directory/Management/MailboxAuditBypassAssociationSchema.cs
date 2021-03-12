using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072A RID: 1834
	internal class MailboxAuditBypassAssociationSchema : ADPresentationSchema
	{
		// Token: 0x060057D3 RID: 22483 RVA: 0x001398A4 File Offset: 0x00137AA4
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADRecipientSchema>();
		}

		// Token: 0x04003B42 RID: 15170
		public static readonly ADPropertyDefinition ObjectId = ADObjectSchema.Id;

		// Token: 0x04003B43 RID: 15171
		public static readonly ADPropertyDefinition AuditBypassEnabled = ADRecipientSchema.AuditBypassEnabled;
	}
}
