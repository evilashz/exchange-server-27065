using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FA RID: 1786
	internal class DynamicDistributionGroupSchema : DistributionGroupBaseSchema
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x001314CD File Offset: 0x0012F6CD
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADDynamicGroupSchema>();
		}

		// Token: 0x0400387E RID: 14462
		public static readonly ADPropertyDefinition RecipientFilter = ADDynamicGroupSchema.RecipientFilter;

		// Token: 0x0400387F RID: 14463
		public static readonly ADPropertyDefinition LdapRecipientFilter = ADDynamicGroupSchema.LdapRecipientFilter;

		// Token: 0x04003880 RID: 14464
		public static readonly ADPropertyDefinition RecipientContainer = ADDynamicGroupSchema.RecipientContainer;

		// Token: 0x04003881 RID: 14465
		public static readonly ADPropertyDefinition IncludedRecipients = ADDynamicGroupSchema.IncludedRecipients;

		// Token: 0x04003882 RID: 14466
		public static readonly ADPropertyDefinition ConditionalDepartment = ADDynamicGroupSchema.ConditionalDepartment;

		// Token: 0x04003883 RID: 14467
		public static readonly ADPropertyDefinition ConditionalCompany = ADDynamicGroupSchema.ConditionalCompany;

		// Token: 0x04003884 RID: 14468
		public static readonly ADPropertyDefinition ConditionalStateOrProvince = ADDynamicGroupSchema.ConditionalStateOrProvince;

		// Token: 0x04003885 RID: 14469
		public static readonly ADPropertyDefinition RecipientFilterType = ADDynamicGroupSchema.RecipientFilterType;

		// Token: 0x04003886 RID: 14470
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003887 RID: 14471
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003888 RID: 14472
		public static readonly ADPropertyDefinition ConditionalCustomAttribute1 = ADDynamicGroupSchema.ConditionalCustomAttribute1;

		// Token: 0x04003889 RID: 14473
		public static readonly ADPropertyDefinition ConditionalCustomAttribute2 = ADDynamicGroupSchema.ConditionalCustomAttribute2;

		// Token: 0x0400388A RID: 14474
		public static readonly ADPropertyDefinition ConditionalCustomAttribute3 = ADDynamicGroupSchema.ConditionalCustomAttribute3;

		// Token: 0x0400388B RID: 14475
		public static readonly ADPropertyDefinition ConditionalCustomAttribute4 = ADDynamicGroupSchema.ConditionalCustomAttribute4;

		// Token: 0x0400388C RID: 14476
		public static readonly ADPropertyDefinition ConditionalCustomAttribute5 = ADDynamicGroupSchema.ConditionalCustomAttribute5;

		// Token: 0x0400388D RID: 14477
		public static readonly ADPropertyDefinition ConditionalCustomAttribute6 = ADDynamicGroupSchema.ConditionalCustomAttribute6;

		// Token: 0x0400388E RID: 14478
		public static readonly ADPropertyDefinition ConditionalCustomAttribute7 = ADDynamicGroupSchema.ConditionalCustomAttribute7;

		// Token: 0x0400388F RID: 14479
		public static readonly ADPropertyDefinition ConditionalCustomAttribute8 = ADDynamicGroupSchema.ConditionalCustomAttribute8;

		// Token: 0x04003890 RID: 14480
		public static readonly ADPropertyDefinition ConditionalCustomAttribute9 = ADDynamicGroupSchema.ConditionalCustomAttribute9;

		// Token: 0x04003891 RID: 14481
		public static readonly ADPropertyDefinition ConditionalCustomAttribute10 = ADDynamicGroupSchema.ConditionalCustomAttribute10;

		// Token: 0x04003892 RID: 14482
		public static readonly ADPropertyDefinition ConditionalCustomAttribute11 = ADDynamicGroupSchema.ConditionalCustomAttribute11;

		// Token: 0x04003893 RID: 14483
		public static readonly ADPropertyDefinition ConditionalCustomAttribute12 = ADDynamicGroupSchema.ConditionalCustomAttribute12;

		// Token: 0x04003894 RID: 14484
		public static readonly ADPropertyDefinition ConditionalCustomAttribute13 = ADDynamicGroupSchema.ConditionalCustomAttribute13;

		// Token: 0x04003895 RID: 14485
		public static readonly ADPropertyDefinition ConditionalCustomAttribute14 = ADDynamicGroupSchema.ConditionalCustomAttribute14;

		// Token: 0x04003896 RID: 14486
		public static readonly ADPropertyDefinition ConditionalCustomAttribute15 = ADDynamicGroupSchema.ConditionalCustomAttribute15;

		// Token: 0x04003897 RID: 14487
		public static readonly ADPropertyDefinition ManagedBy = ADDynamicGroupSchema.ManagedBy;

		// Token: 0x04003898 RID: 14488
		public static readonly ADPropertyDefinition FilterOnlyManagedBy = ADDynamicGroupSchema.FilterOnlyManagedBy;
	}
}
