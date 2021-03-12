using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200036B RID: 875
	internal class LogStreamResetOnMount
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000A45D7 File Offset: 0x000A27D7
		public LogStreamResetOnMount(IReplayConfiguration config)
		{
			this.m_config = config;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000A45E6 File Offset: 0x000A27E6
		public bool ResetPending
		{
			get
			{
				return this.m_logResetPending;
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000A45EE File Offset: 0x000A27EE
		public void ResetLogStream()
		{
			this.m_logResetPending = true;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000A45F8 File Offset: 0x000A27F8
		public void ConfirmLogReset()
		{
			if (this.m_logResetPending)
			{
				Exception ex = null;
				try
				{
					long num = ShipControl.HighestGenerationInDirectory(new DirectoryInfo(this.m_config.DestinationLogPath), this.m_config.LogFilePrefix, "." + this.m_config.LogExtension);
					ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string, long>((long)this.GetHashCode(), "LogStreamResetOnMount: {0}: log stream reset confirmed at 0x{1:X}", this.m_config.Name, num);
					this.m_logResetPending = false;
					this.m_config.UpdateLastLogGeneratedAndEndOfLogInfo(num);
				}
				catch (IOException ex2)
				{
					ex = ex2;
				}
				catch (SecurityException ex3)
				{
					ex = ex3;
				}
				catch (TransientException ex4)
				{
					ex = ex4;
				}
				catch (AmServerException ex5)
				{
					ex = ex5;
				}
				if (ex != null)
				{
					ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, Exception>((long)this.GetHashCode(), "LogStreamResetOnMount: {0}: log stream reset confirmation failed with exception: {1}", this.m_config.Name, ex);
					return;
				}
			}
			else
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "LogStreamResetOnMount: {0}: normal mount: no log stream reset was pending", this.m_config.Name);
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000A4714 File Offset: 0x000A2914
		public void CancelLogReset()
		{
			this.m_logResetPending = false;
			ExTraceGlobals.ReplicaInstanceTracer.TraceDebug<string>((long)this.GetHashCode(), "LogStreamResetOnMount: {0}: mount failed, so no log stream reset was possible", this.m_config.Name);
		}

		// Token: 0x04000EDE RID: 3806
		private IReplayConfiguration m_config;

		// Token: 0x04000EDF RID: 3807
		private bool m_logResetPending;
	}
}
