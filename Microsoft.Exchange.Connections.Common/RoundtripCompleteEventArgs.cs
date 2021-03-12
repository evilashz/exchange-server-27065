using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RoundtripCompleteEventArgs : EventArgs
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000279A File Offset: 0x0000099A
		public RoundtripCompleteEventArgs(TimeSpan roundtripTime, bool roundtripSuccessful)
		{
			this.RoundtripTime = roundtripTime;
			this.RoundtripSuccessful = roundtripSuccessful;
			this.ServerName = string.Empty;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000027BB File Offset: 0x000009BB
		public RoundtripCompleteEventArgs(TimeSpan roundtripTime, bool roundtripSuccessful, string serverName) : this(roundtripTime, roundtripSuccessful)
		{
			this.ServerName = serverName;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000027CC File Offset: 0x000009CC
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000027D4 File Offset: 0x000009D4
		public TimeSpan RoundtripTime { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000027DD File Offset: 0x000009DD
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000027E5 File Offset: 0x000009E5
		internal bool RoundtripSuccessful { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000027EE File Offset: 0x000009EE
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000027F6 File Offset: 0x000009F6
		internal string ServerName { get; private set; }
	}
}
