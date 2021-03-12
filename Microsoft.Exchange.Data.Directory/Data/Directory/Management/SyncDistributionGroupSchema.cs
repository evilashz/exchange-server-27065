using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200075C RID: 1884
	internal class SyncDistributionGroupSchema : DistributionGroupSchema
	{
		// Token: 0x04003E33 RID: 15923
		public static readonly ADPropertyDefinition BlockedSendersHash = ADRecipientSchema.BlockedSendersHash;

		// Token: 0x04003E34 RID: 15924
		public static readonly ADPropertyDefinition Notes = ADRecipientSchema.Notes;

		// Token: 0x04003E35 RID: 15925
		public static readonly ADPropertyDefinition RecipientDisplayType = ADRecipientSchema.RecipientDisplayType;

		// Token: 0x04003E36 RID: 15926
		public static readonly ADPropertyDefinition SafeRecipientsHash = ADRecipientSchema.SafeRecipientsHash;

		// Token: 0x04003E37 RID: 15927
		public static readonly ADPropertyDefinition SafeSendersHash = ADRecipientSchema.SafeSendersHash;

		// Token: 0x04003E38 RID: 15928
		public static readonly ADPropertyDefinition EndOfList = SyncMailboxSchema.EndOfList;

		// Token: 0x04003E39 RID: 15929
		public static readonly ADPropertyDefinition Cookie = SyncMailboxSchema.Cookie;

		// Token: 0x04003E3A RID: 15930
		public static readonly ADPropertyDefinition DirSyncId = ADRecipientSchema.DirSyncId;

		// Token: 0x04003E3B RID: 15931
		public new static readonly ADPropertyDefinition Members = ADGroupSchema.Members;

		// Token: 0x04003E3C RID: 15932
		public static readonly ADPropertyDefinition SeniorityIndex = ADRecipientSchema.HABSeniorityIndex;

		// Token: 0x04003E3D RID: 15933
		public static readonly ADPropertyDefinition PhoneticDisplayName = ADRecipientSchema.PhoneticDisplayName;

		// Token: 0x04003E3E RID: 15934
		public static readonly ADPropertyDefinition IsHierarchicalGroup = ADGroupSchema.IsOrganizationalGroup;

		// Token: 0x04003E3F RID: 15935
		public static readonly ADPropertyDefinition RawManagedBy = ADGroupSchema.RawManagedBy;

		// Token: 0x04003E40 RID: 15936
		public static readonly ADPropertyDefinition CoManagedBy = ADGroupSchema.CoManagedBy;

		// Token: 0x04003E41 RID: 15937
		public static readonly ADPropertyDefinition OnPremisesObjectId = ADRecipientSchema.OnPremisesObjectId;

		// Token: 0x04003E42 RID: 15938
		public static readonly ADPropertyDefinition IsDirSynced = ADRecipientSchema.IsDirSynced;

		// Token: 0x04003E43 RID: 15939
		public static readonly ADPropertyDefinition DirSyncAuthorityMetadata = ADRecipientSchema.DirSyncAuthorityMetadata;

		// Token: 0x04003E44 RID: 15940
		public static readonly ADPropertyDefinition ExcludedFromBackSync = ADRecipientSchema.ExcludedFromBackSync;
	}
}
