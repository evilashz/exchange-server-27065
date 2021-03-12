using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.ContentAggregation;
using Microsoft.Exchange.Net.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000061 RID: 97
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeltaSyncClientFactory
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00016BA9 File Offset: 0x00014DA9
		protected DeltaSyncClientFactory()
		{
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00016BB1 File Offset: 0x00014DB1
		public static DeltaSyncClientFactory Instance
		{
			get
			{
				return DeltaSyncClientFactory.instance;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016BB8 File Offset: 0x00014DB8
		public virtual IDeltaSyncClient Create(DeltaSyncUserAccount userAccount, int remoteConnectionTimeout, IWebProxy proxy, long maxDownloadSizePerMessage, ProtocolLog httpProtocolLog, SyncLogSession syncLogSession, EventHandler<RoundtripCompleteEventArgs> roundtripCompleteEventHandler)
		{
			return new DeltaSyncClient(userAccount, remoteConnectionTimeout, proxy, maxDownloadSizePerMessage, httpProtocolLog, syncLogSession, roundtripCompleteEventHandler);
		}

		// Token: 0x0400024B RID: 587
		private static readonly DeltaSyncClientFactory instance = new DeltaSyncClientFactory();
	}
}
