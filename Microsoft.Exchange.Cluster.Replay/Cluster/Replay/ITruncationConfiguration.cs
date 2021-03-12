using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200011A RID: 282
	internal interface ITruncationConfiguration
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000ABA RID: 2746
		string SourceMachine { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000ABB RID: 2747
		string ServerName { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000ABC RID: 2748
		Guid IdentityGuid { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000ABD RID: 2749
		string LogFilePrefix { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000ABE RID: 2750
		string DestinationLogPath { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000ABF RID: 2751
		bool CircularLoggingEnabled { get; }
	}
}
