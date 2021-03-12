using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x02000215 RID: 533
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SkipPhotoHandlerQueryStringParameters
	{
		// Token: 0x04000AC1 RID: 2753
		internal const string FileSystem = "skipfs";

		// Token: 0x04000AC2 RID: 2754
		internal const string FileSystemWithLeadingAmpersand = "&skipfs=1";

		// Token: 0x04000AC3 RID: 2755
		internal const string Mailbox = "skipmbx";

		// Token: 0x04000AC4 RID: 2756
		internal const string MailboxWithLeadingAmpersand = "&skipmbx=1";

		// Token: 0x04000AC5 RID: 2757
		internal const string ActiveDirectory = "skipad";

		// Token: 0x04000AC6 RID: 2758
		internal const string ActiveDirectoryWithLeadingAmpersand = "&skipad=1";

		// Token: 0x04000AC7 RID: 2759
		internal const string Caching = "skipcaching";

		// Token: 0x04000AC8 RID: 2760
		internal const string CachingWithLeadingAmpersand = "&skipcaching=1";

		// Token: 0x04000AC9 RID: 2761
		internal const string Http = "skiphttp";

		// Token: 0x04000ACA RID: 2762
		internal const string HttpWithLeadingAmpersand = "&skiphttp=1";

		// Token: 0x04000ACB RID: 2763
		internal const string Private = "skipprv";

		// Token: 0x04000ACC RID: 2764
		internal const string PrivateWithLeadingAmpersand = "&skipprv=1";

		// Token: 0x04000ACD RID: 2765
		internal const string RemoteForest = "skiprf";

		// Token: 0x04000ACE RID: 2766
		internal const string RemoteForestWithLeadingAmpersand = "&skiprf=1";
	}
}
