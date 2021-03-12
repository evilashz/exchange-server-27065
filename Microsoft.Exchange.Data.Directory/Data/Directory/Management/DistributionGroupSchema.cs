using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F4 RID: 1780
	internal class DistributionGroupSchema : DistributionGroupBaseSchema
	{
		// Token: 0x0600535E RID: 21342 RVA: 0x00130AFF File Offset: 0x0012ECFF
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADGroupSchema>();
		}

		// Token: 0x04003875 RID: 14453
		public static readonly ADPropertyDefinition GroupType = ADGroupSchema.GroupType;

		// Token: 0x04003876 RID: 14454
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003877 RID: 14455
		public static readonly ADPropertyDefinition BypassNestedModerationEnabled = ADRecipientSchema.BypassNestedModerationEnabled;

		// Token: 0x04003878 RID: 14456
		public static readonly ADPropertyDefinition ManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x04003879 RID: 14457
		public static readonly ADPropertyDefinition MemberJoinRestriction = ADGroupSchema.MemberJoinRestriction;

		// Token: 0x0400387A RID: 14458
		public static readonly ADPropertyDefinition MemberDepartRestriction = ADGroupSchema.MemberDepartRestriction;

		// Token: 0x0400387B RID: 14459
		public static readonly ADPropertyDefinition Members = ADGroupSchema.Members;
	}
}
