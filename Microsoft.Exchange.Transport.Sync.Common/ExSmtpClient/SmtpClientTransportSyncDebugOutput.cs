using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common.ExSmtpClient
{
	// Token: 0x0200006D RID: 109
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SmtpClientTransportSyncDebugOutput : ISmtpClientDebugOutput
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00007A8D File Offset: 0x00005C8D
		internal SmtpClientTransportSyncDebugOutput(SyncLogSession syncLogSession)
		{
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007A9C File Offset: 0x00005C9C
		public void Output(Trace tracer, object context, string message, params object[] args)
		{
			int num = (context != null) ? context.GetHashCode() : 0;
			this.syncLogSession.LogVerbose((TSLID)9UL, tracer, (long)num, message, args);
		}

		// Token: 0x04000125 RID: 293
		private SyncLogSession syncLogSession;
	}
}
