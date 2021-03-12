using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000191 RID: 401
	[Flags]
	public enum ReportEntryFlags
	{
		// Token: 0x0400086B RID: 2155
		None = 0,
		// Token: 0x0400086C RID: 2156
		BadItem = 1,
		// Token: 0x0400086D RID: 2157
		Failure = 2,
		// Token: 0x0400086E RID: 2158
		ConfigObject = 4,
		// Token: 0x0400086F RID: 2159
		MailboxSize = 8,
		// Token: 0x04000870 RID: 2160
		Fatal = 16,
		// Token: 0x04000871 RID: 2161
		Cleanup = 32,
		// Token: 0x04000872 RID: 2162
		Primary = 256,
		// Token: 0x04000873 RID: 2163
		Archive = 512,
		// Token: 0x04000874 RID: 2164
		Source = 1024,
		// Token: 0x04000875 RID: 2165
		Target = 2048,
		// Token: 0x04000876 RID: 2166
		Before = 4096,
		// Token: 0x04000877 RID: 2167
		After = 8192,
		// Token: 0x04000878 RID: 2168
		MailboxVerificationResults = 16384,
		// Token: 0x04000879 RID: 2169
		TargetThrottleDurations = 32768,
		// Token: 0x0400087A RID: 2170
		SourceThrottleDurations = 65536,
		// Token: 0x0400087B RID: 2171
		SessionStatistics = 131072
	}
}
