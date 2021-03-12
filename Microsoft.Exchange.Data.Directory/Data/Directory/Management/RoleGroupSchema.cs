using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200074E RID: 1870
	internal class RoleGroupSchema : ADPresentationSchema
	{
		// Token: 0x06005ADF RID: 23263 RVA: 0x0013E3DA File Offset: 0x0013C5DA
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADGroupSchema>();
		}

		// Token: 0x04003D20 RID: 15648
		public static readonly ADPropertyDefinition RoleAssignments = ADGroupSchema.RoleAssignments;

		// Token: 0x04003D21 RID: 15649
		public static readonly ADPropertyDefinition Roles = ADGroupSchema.Roles;

		// Token: 0x04003D22 RID: 15650
		public static readonly ADPropertyDefinition ManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x04003D23 RID: 15651
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003D24 RID: 15652
		public static readonly ADPropertyDefinition Members = ADGroupSchema.Members;

		// Token: 0x04003D25 RID: 15653
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04003D26 RID: 15654
		public static readonly ADPropertyDefinition ForeignGroupSid = ADGroupSchema.ForeignGroupSid;

		// Token: 0x04003D27 RID: 15655
		public static readonly ADPropertyDefinition LinkedGroup = ADGroupSchema.LinkedGroup;

		// Token: 0x04003D28 RID: 15656
		public static readonly ADPropertyDefinition RoleGroupType = ADGroupSchema.RoleGroupType;

		// Token: 0x04003D29 RID: 15657
		public static readonly ADPropertyDefinition RawDescription = ADRecipientSchema.Description;

		// Token: 0x04003D2A RID: 15658
		public static readonly ADPropertyDefinition Description = ADGroupSchema.RoleGroupDescription;

		// Token: 0x04003D2B RID: 15659
		public static readonly ADPropertyDefinition RoleGroupTypeId = ADGroupSchema.RoleGroupTypeId;

		// Token: 0x04003D2C RID: 15660
		public static readonly ADPropertyDefinition LocalizationFlags = ADGroupSchema.LocalizationFlags;

		// Token: 0x04003D2D RID: 15661
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04003D2E RID: 15662
		public static readonly ADPropertyDefinition LinkedPartnerGroupId = ADGroupSchema.LinkedPartnerGroupId;

		// Token: 0x04003D2F RID: 15663
		public static readonly ADPropertyDefinition LinkedPartnerOrganizationId = ADGroupSchema.LinkedPartnerOrganizationId;

		// Token: 0x04003D30 RID: 15664
		public static readonly ADPropertyDefinition RawCapabilities = ADRecipientSchema.RawCapabilities;

		// Token: 0x04003D31 RID: 15665
		public static readonly ADPropertyDefinition Capabilities = ADRecipientSchema.Capabilities;

		// Token: 0x04003D32 RID: 15666
		public static readonly ADPropertyDefinition UsnCreated = ADRecipientSchema.UsnCreated;
	}
}
