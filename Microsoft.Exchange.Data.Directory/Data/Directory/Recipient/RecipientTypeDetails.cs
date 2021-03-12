using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000217 RID: 535
	[Flags]
	public enum RecipientTypeDetails : long
	{
		// Token: 0x04000BF5 RID: 3061
		[LocDescription(DirectoryStrings.IDs.UndefinedRecipientTypeDetails)]
		None = 0L,
		// Token: 0x04000BF6 RID: 3062
		[LocDescription(DirectoryStrings.IDs.MailboxUserRecipientTypeDetails)]
		UserMailbox = 1L,
		// Token: 0x04000BF7 RID: 3063
		[LocDescription(DirectoryStrings.IDs.LinkedMailboxRecipientTypeDetails)]
		LinkedMailbox = 2L,
		// Token: 0x04000BF8 RID: 3064
		[LocDescription(DirectoryStrings.IDs.SharedMailboxRecipientTypeDetails)]
		SharedMailbox = 4L,
		// Token: 0x04000BF9 RID: 3065
		[LocDescription(DirectoryStrings.IDs.LegacyMailboxRecipientTypeDetails)]
		LegacyMailbox = 8L,
		// Token: 0x04000BFA RID: 3066
		[LocDescription(DirectoryStrings.IDs.ConferenceRoomMailboxRecipientTypeDetails)]
		RoomMailbox = 16L,
		// Token: 0x04000BFB RID: 3067
		[LocDescription(DirectoryStrings.IDs.EquipmentMailboxRecipientTypeDetails)]
		EquipmentMailbox = 32L,
		// Token: 0x04000BFC RID: 3068
		[LocDescription(DirectoryStrings.IDs.MailEnabledContactRecipientTypeDetails)]
		MailContact = 64L,
		// Token: 0x04000BFD RID: 3069
		[LocDescription(DirectoryStrings.IDs.MailEnabledUserRecipientTypeDetails)]
		MailUser = 128L,
		// Token: 0x04000BFE RID: 3070
		[LocDescription(DirectoryStrings.IDs.MailEnabledUniversalDistributionGroupRecipientTypeDetails)]
		MailUniversalDistributionGroup = 256L,
		// Token: 0x04000BFF RID: 3071
		[LocDescription(DirectoryStrings.IDs.MailEnabledNonUniversalGroupRecipientTypeDetails)]
		MailNonUniversalGroup = 512L,
		// Token: 0x04000C00 RID: 3072
		[LocDescription(DirectoryStrings.IDs.MailEnabledUniversalSecurityGroupRecipientTypeDetails)]
		MailUniversalSecurityGroup = 1024L,
		// Token: 0x04000C01 RID: 3073
		[LocDescription(DirectoryStrings.IDs.MailEnabledDynamicDistributionGroupRecipientTypeDetails)]
		DynamicDistributionGroup = 2048L,
		// Token: 0x04000C02 RID: 3074
		[LocDescription(DirectoryStrings.IDs.PublicFolderRecipientTypeDetails)]
		PublicFolder = 4096L,
		// Token: 0x04000C03 RID: 3075
		[LocDescription(DirectoryStrings.IDs.SystemAttendantMailboxRecipientTypeDetails)]
		SystemAttendantMailbox = 8192L,
		// Token: 0x04000C04 RID: 3076
		[LocDescription(DirectoryStrings.IDs.SystemMailboxRecipientTypeDetails)]
		SystemMailbox = 16384L,
		// Token: 0x04000C05 RID: 3077
		[LocDescription(DirectoryStrings.IDs.MailEnabledForestContactRecipientTypeDetails)]
		MailForestContact = 32768L,
		// Token: 0x04000C06 RID: 3078
		[LocDescription(DirectoryStrings.IDs.UserRecipientTypeDetails)]
		User = 65536L,
		// Token: 0x04000C07 RID: 3079
		[LocDescription(DirectoryStrings.IDs.ContactRecipientTypeDetails)]
		Contact = 131072L,
		// Token: 0x04000C08 RID: 3080
		[LocDescription(DirectoryStrings.IDs.UniversalDistributionGroupRecipientTypeDetails)]
		UniversalDistributionGroup = 262144L,
		// Token: 0x04000C09 RID: 3081
		[LocDescription(DirectoryStrings.IDs.UniversalSecurityGroupRecipientTypeDetails)]
		UniversalSecurityGroup = 524288L,
		// Token: 0x04000C0A RID: 3082
		[LocDescription(DirectoryStrings.IDs.NonUniversalGroupRecipientTypeDetails)]
		NonUniversalGroup = 1048576L,
		// Token: 0x04000C0B RID: 3083
		[LocDescription(DirectoryStrings.IDs.DisabledUserRecipientTypeDetails)]
		DisabledUser = 2097152L,
		// Token: 0x04000C0C RID: 3084
		[LocDescription(DirectoryStrings.IDs.MicrosoftExchangeRecipientTypeDetails)]
		MicrosoftExchange = 4194304L,
		// Token: 0x04000C0D RID: 3085
		[LocDescription(DirectoryStrings.IDs.ArbitrationMailboxTypeDetails)]
		ArbitrationMailbox = 8388608L,
		// Token: 0x04000C0E RID: 3086
		[LocDescription(DirectoryStrings.IDs.MailboxPlanTypeDetails)]
		MailboxPlan = 16777216L,
		// Token: 0x04000C0F RID: 3087
		[LocDescription(DirectoryStrings.IDs.LinkedUserTypeDetails)]
		LinkedUser = 33554432L,
		// Token: 0x04000C10 RID: 3088
		[LocDescription(DirectoryStrings.IDs.RoomListGroupTypeDetails)]
		RoomList = 268435456L,
		// Token: 0x04000C11 RID: 3089
		[LocDescription(DirectoryStrings.IDs.DiscoveryMailboxTypeDetails)]
		DiscoveryMailbox = 536870912L,
		// Token: 0x04000C12 RID: 3090
		[LocDescription(DirectoryStrings.IDs.RoleGroupTypeDetails)]
		RoleGroup = 1073741824L,
		// Token: 0x04000C13 RID: 3091
		[LocDescription(DirectoryStrings.IDs.RemoteUserMailboxTypeDetails)]
		RemoteUserMailbox = 2147483648L,
		// Token: 0x04000C14 RID: 3092
		[LocDescription(DirectoryStrings.IDs.ComputerRecipientTypeDetails)]
		Computer = 4294967296L,
		// Token: 0x04000C15 RID: 3093
		[LocDescription(DirectoryStrings.IDs.RemoteRoomMailboxTypeDetails)]
		RemoteRoomMailbox = 8589934592L,
		// Token: 0x04000C16 RID: 3094
		[LocDescription(DirectoryStrings.IDs.RemoteEquipmentMailboxTypeDetails)]
		RemoteEquipmentMailbox = 17179869184L,
		// Token: 0x04000C17 RID: 3095
		[LocDescription(DirectoryStrings.IDs.RemoteSharedMailboxTypeDetails)]
		RemoteSharedMailbox = 34359738368L,
		// Token: 0x04000C18 RID: 3096
		[LocDescription(DirectoryStrings.IDs.PublicFolderMailboxRecipientTypeDetails)]
		PublicFolderMailbox = 68719476736L,
		// Token: 0x04000C19 RID: 3097
		[LocDescription(DirectoryStrings.IDs.TeamMailboxRecipientTypeDetails)]
		TeamMailbox = 137438953472L,
		// Token: 0x04000C1A RID: 3098
		[LocDescription(DirectoryStrings.IDs.RemoteTeamMailboxRecipientTypeDetails)]
		RemoteTeamMailbox = 274877906944L,
		// Token: 0x04000C1B RID: 3099
		[LocDescription(DirectoryStrings.IDs.MonitoringMailboxRecipientTypeDetails)]
		MonitoringMailbox = 549755813888L,
		// Token: 0x04000C1C RID: 3100
		[LocDescription(DirectoryStrings.IDs.GroupMailboxRecipientTypeDetails)]
		GroupMailbox = 1099511627776L,
		// Token: 0x04000C1D RID: 3101
		[LocDescription(DirectoryStrings.IDs.LinkedRoomMailboxRecipientTypeDetails)]
		LinkedRoomMailbox = 2199023255552L,
		// Token: 0x04000C1E RID: 3102
		[LocDescription(DirectoryStrings.IDs.AuditLogMailboxRecipientTypeDetails)]
		AuditLogMailbox = 4398046511104L,
		// Token: 0x04000C1F RID: 3103
		[LocDescription(DirectoryStrings.IDs.RemoteGroupMailboxRecipientTypeDetails)]
		RemoteGroupMailbox = 8796093022208L,
		// Token: 0x04000C20 RID: 3104
		AllUniqueRecipientTypes = 17592186044415L
	}
}
