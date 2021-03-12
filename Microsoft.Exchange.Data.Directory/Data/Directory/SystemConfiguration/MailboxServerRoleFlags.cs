using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000586 RID: 1414
	[Flags]
	internal enum MailboxServerRoleFlags
	{
		// Token: 0x04002CBA RID: 11450
		None = 0,
		// Token: 0x04002CBB RID: 11451
		MAPIEncryptionRequired = 1,
		// Token: 0x04002CBC RID: 11452
		ExpirationAuditLogEnabled = 2,
		// Token: 0x04002CBD RID: 11453
		AutocopyAuditLogEnabled = 4,
		// Token: 0x04002CBE RID: 11454
		FolderAuditLogEnabled = 8,
		// Token: 0x04002CBF RID: 11455
		ElcSubjectLoggingEnabled = 16
	}
}
