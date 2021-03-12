using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000775 RID: 1909
	internal class ThrottlingPolicyAssociationSchema : ADPresentationSchema
	{
		// Token: 0x06005DF8 RID: 24056 RVA: 0x00143850 File Offset: 0x00141A50
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADRecipientSchema>();
		}

		// Token: 0x04003F88 RID: 16264
		public static readonly ADPropertyDefinition ObjectId = ADObjectSchema.Id;

		// Token: 0x04003F89 RID: 16265
		public static readonly ADPropertyDefinition ThrottlingPolicyId = ADRecipientSchema.ThrottlingPolicy;
	}
}
