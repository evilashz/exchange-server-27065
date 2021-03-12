using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000768 RID: 1896
	internal class WindowsGroupSchema : ADPresentationSchema
	{
		// Token: 0x06005D2E RID: 23854 RVA: 0x00142020 File Offset: 0x00140220
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADGroupSchema>();
		}

		// Token: 0x04003F04 RID: 16132
		public static readonly ADPropertyDefinition GroupType = ADGroupSchema.GroupType;

		// Token: 0x04003F05 RID: 16133
		public static readonly ADPropertyDefinition ManagedBy = ADGroupSchema.ManagedBy;

		// Token: 0x04003F06 RID: 16134
		public static readonly ADPropertyDefinition RecipientType = ADRecipientSchema.RecipientType;

		// Token: 0x04003F07 RID: 16135
		public static readonly ADPropertyDefinition RecipientTypeDetails = ADRecipientSchema.RecipientTypeDetails;

		// Token: 0x04003F08 RID: 16136
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003F09 RID: 16137
		public static readonly ADPropertyDefinition Sid = ADMailboxRecipientSchema.Sid;

		// Token: 0x04003F0A RID: 16138
		public static readonly ADPropertyDefinition SidHistory = ADMailboxRecipientSchema.SidHistory;

		// Token: 0x04003F0B RID: 16139
		public static readonly ADPropertyDefinition SimpleDisplayName = ADRecipientSchema.SimpleDisplayName;

		// Token: 0x04003F0C RID: 16140
		public static readonly ADPropertyDefinition WindowsEmailAddress = ADRecipientSchema.WindowsEmailAddress;

		// Token: 0x04003F0D RID: 16141
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04003F0E RID: 16142
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003F0F RID: 16143
		public static readonly ADPropertyDefinition Members = ADGroupSchema.Members;

		// Token: 0x04003F10 RID: 16144
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003F11 RID: 16145
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x04003F12 RID: 16146
		public static readonly ADPropertyDefinition OrganizationalUnit = ADRecipientSchema.OrganizationalUnit;

		// Token: 0x04003F13 RID: 16147
		public static readonly ADPropertyDefinition SeniorityIndex = ADRecipientSchema.HABSeniorityIndex;

		// Token: 0x04003F14 RID: 16148
		public static readonly ADPropertyDefinition IsHierarchicalGroup = ADGroupSchema.IsOrganizationalGroup;
	}
}
