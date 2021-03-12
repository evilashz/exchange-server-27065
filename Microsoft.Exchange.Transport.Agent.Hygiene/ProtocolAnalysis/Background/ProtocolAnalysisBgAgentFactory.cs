using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200004F RID: 79
	internal sealed class ProtocolAnalysisBgAgentFactory
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		public ProtocolAnalysisBgAgentFactory()
		{
			Database.Attach();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public void CreateAgent(SmtpServer server)
		{
			lock (this.syncObject)
			{
				if (this.PaBgAgent == null)
				{
					this.PaBgAgent = new ProtocolAnalysisBgAgent(server);
					this.isCreated = true;
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public void Close()
		{
			lock (this.syncObject)
			{
				if (this.PaBgAgent != null)
				{
					this.PaBgAgent.Shutdown();
					this.PaBgAgent = null;
					ProtocolAnalysisBgAgent.PerformanceCounters.RemoveCounters();
				}
				else if (this.isCreated)
				{
					throw new InvalidOperationException("Trying to destroy ProtocolAnalysisBgAgent more than once.");
				}
			}
		}

		// Token: 0x040001B4 RID: 436
		internal ProtocolAnalysisBgAgent PaBgAgent;

		// Token: 0x040001B5 RID: 437
		private bool isCreated;

		// Token: 0x040001B6 RID: 438
		private object syncObject = new object();
	}
}
