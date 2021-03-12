using System;
using Microsoft.Exchange.Hygiene.Deployment.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200004E RID: 78
	public class ProbeWorkItemLogger : IHygieneLogger, IDisposable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000C62B File Offset: 0x0000A82B
		public ProbeWorkItemLogger(ProbeWorkItem probeWorkItem, bool logVerbose, bool logMessage)
		{
			this.probeWorkItem = probeWorkItem;
			this.logVerbose = logVerbose;
			this.logMessage = logMessage;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C648 File Offset: 0x0000A848
		public void LogMessage(string s)
		{
			if (this.logMessage)
			{
				ProbeResult result = this.probeWorkItem.Result;
				result.ExecutionContext += string.Format("PROBE -- {0}. ", s);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000C678 File Offset: 0x0000A878
		public void LogVerbose(string s)
		{
			if (this.logVerbose)
			{
				ProbeResult result = this.probeWorkItem.Result;
				result.ExecutionContext += string.Format("PROBE -- {0}. ", s);
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public void LogError(string s)
		{
			ProbeResult result = this.probeWorkItem.Result;
			result.ExecutionContext += string.Format("PROBE -- {0}. ", s);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		public void Dispose()
		{
		}

		// Token: 0x0400014A RID: 330
		private readonly bool logVerbose;

		// Token: 0x0400014B RID: 331
		private readonly bool logMessage;

		// Token: 0x0400014C RID: 332
		private ProbeWorkItem probeWorkItem;
	}
}
