using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200020D RID: 525
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class RemoteServerRoundtripCompleteEventArgs : RoundtripCompleteEventArgs
	{
		// Token: 0x060011C6 RID: 4550 RVA: 0x0003A011 File Offset: 0x00038211
		internal RemoteServerRoundtripCompleteEventArgs(string serverName, TimeSpan roundtripTime, bool roundtripSuccessful) : base(roundtripTime, roundtripSuccessful)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("serverName", serverName);
			this.serverName = serverName;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0003A02D File Offset: 0x0003822D
		internal string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040009AC RID: 2476
		private readonly string serverName;
	}
}
