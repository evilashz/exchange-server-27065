using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Imap
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class ImapConstants
	{
		// Token: 0x04000058 RID: 88
		internal const string InboxFolderName = "INBOX";

		// Token: 0x04000059 RID: 89
		internal const int DefaultPort = 143;

		// Token: 0x0400005A RID: 90
		internal const int SslPort = 993;

		// Token: 0x0400005B RID: 91
		internal const char DefaultHierarchySeparator = '/';

		// Token: 0x0400005C RID: 92
		internal const string ImapVersionString = "imap4rev1";

		// Token: 0x0400005D RID: 93
		internal const string ImapComponentId = "IMAP";

		// Token: 0x0400005E RID: 94
		internal const int MaxFolderLevelDepth = 20;

		// Token: 0x0400005F RID: 95
		internal const int MaxLinesToLog = 10;

		// Token: 0x04000060 RID: 96
		internal const string NullString = "Null";

		// Token: 0x04000061 RID: 97
		internal const string NilValue = "NIL";
	}
}
