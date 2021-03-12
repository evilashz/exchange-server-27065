using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000041 RID: 65
	[Flags]
	internal enum ResourceManagerResources
	{
		// Token: 0x040000C8 RID: 200
		None = 0,
		// Token: 0x040000C9 RID: 201
		MailDatabase = 1,
		// Token: 0x040000CA RID: 202
		MailDatabaseLoggingFolder = 2,
		// Token: 0x040000CB RID: 203
		VersionBuckets = 4,
		// Token: 0x040000CC RID: 204
		PrivateBytes = 8,
		// Token: 0x040000CD RID: 205
		TotalBytes = 16,
		// Token: 0x040000CE RID: 206
		SubmissionQueue = 64,
		// Token: 0x040000CF RID: 207
		TempDrive = 128,
		// Token: 0x040000D0 RID: 208
		All = 255
	}
}
