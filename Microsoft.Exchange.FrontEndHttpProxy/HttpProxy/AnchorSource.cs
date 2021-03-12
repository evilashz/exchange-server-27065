using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000003 RID: 3
	internal enum AnchorSource
	{
		// Token: 0x04000004 RID: 4
		Smtp,
		// Token: 0x04000005 RID: 5
		Sid,
		// Token: 0x04000006 RID: 6
		Domain,
		// Token: 0x04000007 RID: 7
		DomainAndVersion,
		// Token: 0x04000008 RID: 8
		OrganizationId,
		// Token: 0x04000009 RID: 9
		MailboxGuid,
		// Token: 0x0400000A RID: 10
		DatabaseName,
		// Token: 0x0400000B RID: 11
		DatabaseGuid,
		// Token: 0x0400000C RID: 12
		UserADRawEntry,
		// Token: 0x0400000D RID: 13
		ServerInfo,
		// Token: 0x0400000E RID: 14
		ServerVersion,
		// Token: 0x0400000F RID: 15
		Url,
		// Token: 0x04000010 RID: 16
		Anonymous,
		// Token: 0x04000011 RID: 17
		GenericAnchorHint,
		// Token: 0x04000012 RID: 18
		Puid,
		// Token: 0x04000013 RID: 19
		ExternalDirectoryObjectId,
		// Token: 0x04000014 RID: 20
		OAuthActAsUser,
		// Token: 0x04000015 RID: 21
		LiveIdMemberName
	}
}
