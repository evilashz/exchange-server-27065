using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007B RID: 123
	internal sealed class Starter : Base
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00011554 File Offset: 0x0000F754
		public Starter(Init init)
		{
			this.init = init;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000115A4 File Offset: 0x0000F7A4
		public void Stop()
		{
			this.serviceStoppingEvent.Set();
			if (!this.startupThread.Join(this.waitingThreadTerminationTime))
			{
				this.startupThread.Abort();
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000115DD File Offset: 0x0000F7DD
		public void Start()
		{
			this.serviceStoppingEvent.Reset();
			this.startupThread = new Thread(new ThreadStart(this.InitCallback));
			this.startupThread.Start();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011610 File Offset: 0x0000F810
		private void InitCallback()
		{
			try
			{
				IL_00:
				this.init();
			}
			catch (TransientServerException arg)
			{
				ExTraceGlobals.DatabaseManagerTracer.TraceError<Starter, TransientServerException>((long)this.GetHashCode(), "{0}: Transient failure during startup. Will retry in a 10 seconds. Exception: {1}", this, arg);
				if (!this.serviceStoppingEvent.WaitOne(this.sleepStartingThread))
				{
					goto IL_00;
				}
			}
		}

		// Token: 0x04000201 RID: 513
		private Init init;

		// Token: 0x04000202 RID: 514
		private Thread startupThread;

		// Token: 0x04000203 RID: 515
		private ManualResetEvent serviceStoppingEvent = new ManualResetEvent(false);

		// Token: 0x04000204 RID: 516
		private readonly TimeSpan waitingThreadTerminationTime = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000205 RID: 517
		private readonly TimeSpan sleepStartingThread = TimeSpan.FromSeconds(10.0);
	}
}
