using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000738 RID: 1848
	internal class MailContactSchema : MailEnabledOrgPersonSchema
	{
		// Token: 0x06005969 RID: 22889 RVA: 0x0013BF9A File Offset: 0x0013A19A
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADContactSchema>();
		}

		// Token: 0x04003C09 RID: 15369
		public static readonly ADPropertyDefinition ExternalEmailAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04003C0A RID: 15370
		public static readonly ADPropertyDefinition UsePreferMessageFormat = ADRecipientSchema.UsePreferMessageFormat;

		// Token: 0x04003C0B RID: 15371
		public static readonly ADPropertyDefinition MessageFormat = ADRecipientSchema.MessageFormat;

		// Token: 0x04003C0C RID: 15372
		public static readonly ADPropertyDefinition MessageBodyFormat = ADRecipientSchema.MessageBodyFormat;

		// Token: 0x04003C0D RID: 15373
		public static readonly ADPropertyDefinition MacAttachmentFormat = ADRecipientSchema.MacAttachmentFormat;

		// Token: 0x04003C0E RID: 15374
		public static readonly ADPropertyDefinition MaxRecipientPerMessage = ADRecipientSchema.RecipientLimits;

		// Token: 0x04003C0F RID: 15375
		public static readonly ADPropertyDefinition UseMapiRichTextFormat = ADRecipientSchema.UseMapiRichTextFormat;

		// Token: 0x04003C10 RID: 15376
		public static readonly ADPropertyDefinition UserCertificate = ADRecipientSchema.Certificate;

		// Token: 0x04003C11 RID: 15377
		public static readonly ADPropertyDefinition UserSMimeCertificate = ADRecipientSchema.SMimeCertificate;
	}
}
