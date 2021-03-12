using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Update
{
	// Token: 0x02000062 RID: 98
	internal sealed class StsUpdateAgentFactory
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x000122DB File Offset: 0x000104DB
		public StsUpdateAgentFactory()
		{
			Database.Attach();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000122F4 File Offset: 0x000104F4
		public void CreateAgent(SmtpServer server)
		{
			lock (this.syncObject)
			{
				if (this.stsUpdateAgent == null)
				{
					this.stsUpdateAgent = new StsUpdateAgent();
					this.stsUpdateAgent.Startup();
					this.isCreated = true;
				}
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00012354 File Offset: 0x00010554
		public void Close()
		{
			lock (this.syncObject)
			{
				if (this.stsUpdateAgent != null)
				{
					this.stsUpdateAgent.Shutdown();
					this.stsUpdateAgent = null;
					StsUpdateAgent.PerformanceCounters.RemoveCounters();
				}
				else if (this.isCreated)
				{
					throw new InvalidOperationException("Trying to release Update Agent more than once");
				}
			}
		}

		// Token: 0x04000238 RID: 568
		private StsUpdateAgent stsUpdateAgent;

		// Token: 0x04000239 RID: 569
		private bool isCreated;

		// Token: 0x0400023A RID: 570
		private object syncObject = new object();
	}
}
