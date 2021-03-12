using System;
using System.ComponentModel;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200026A RID: 618
	internal abstract class AbandonAsyncBase
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x00063DB4 File Offset: 0x00061FB4
		protected static void AbandonCompletionCallback(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			AbandonAsyncBase abandonAsyncBase = (AbandonAsyncBase)asyncResult.AsyncState;
			Exception ex = null;
			try
			{
				abandonAsyncBase.CompletionProcessing(asyncResult);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (Win32Exception ex3)
			{
				ex = ex3;
			}
			catch (AuthenticationException ex4)
			{
				ex = ex4;
			}
			catch (ObjectDisposedException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.TcpChannelTracer.TraceDebug<string>(0L, "AbandonCompletionCallback ignoring exception {0}", ex.Message);
				return;
			}
			ExTraceGlobals.TcpChannelTracer.TraceDebug(0L, "AbandonCompletionCallback exits.");
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00063E5C File Offset: 0x0006205C
		public void Start()
		{
			ExTraceGlobals.TcpChannelTracer.TraceDebug<int>((long)this.GetHashCode(), "RemoteLogSource:Notification timeout after {0}msec", 10);
			this.m_timeActive.Reset();
			this.m_timeActive.Start();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00063E8C File Offset: 0x0006208C
		public void Complete()
		{
			this.m_timeActive.Stop();
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00063E99 File Offset: 0x00062099
		public ReplayStopwatch TimeActive
		{
			get
			{
				return this.m_timeActive;
			}
		}

		// Token: 0x06001847 RID: 6215
		protected abstract void EndProcessing(IAsyncResult ar);

		// Token: 0x06001848 RID: 6216 RVA: 0x00063EA1 File Offset: 0x000620A1
		private void CompletionProcessing(IAsyncResult ar)
		{
			this.m_timeActive.Stop();
			this.EndProcessing(ar);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00063EB5 File Offset: 0x000620B5
		public void Abandon(IAsyncResult ar)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(AbandonAsyncBase.AbandonCompletionCallback), ar);
		}

		// Token: 0x040009AB RID: 2475
		protected ReplayStopwatch m_timeActive = new ReplayStopwatch();
	}
}
