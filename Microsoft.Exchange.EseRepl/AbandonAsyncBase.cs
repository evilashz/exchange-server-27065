using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Authentication;
using System.Threading;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200003A RID: 58
	internal abstract class AbandonAsyncBase
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00007E18 File Offset: 0x00006018
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
				TcpChannel.Tracer.TraceDebug<string>(0L, "AbandonCompletionCallback ignoring exception {0}", ex.Message);
				return;
			}
			TcpChannel.Tracer.TraceDebug(0L, "AbandonCompletionCallback exits.");
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007EC0 File Offset: 0x000060C0
		public void Start()
		{
			TcpChannel.Tracer.TraceDebug<int>((long)this.GetHashCode(), "RemoteLogSource:Notification timeout after {0}msec", 10);
			this.m_timeActive.Reset();
			this.m_timeActive.Start();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007EF0 File Offset: 0x000060F0
		public void Complete()
		{
			this.m_timeActive.Stop();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007EFD File Offset: 0x000060FD
		public Stopwatch TimeActive
		{
			get
			{
				return this.m_timeActive;
			}
		}

		// Token: 0x06000203 RID: 515
		protected abstract void EndProcessing(IAsyncResult ar);

		// Token: 0x06000204 RID: 516 RVA: 0x00007F05 File Offset: 0x00006105
		private void CompletionProcessing(IAsyncResult ar)
		{
			this.m_timeActive.Stop();
			this.EndProcessing(ar);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007F19 File Offset: 0x00006119
		public void Abandon(IAsyncResult ar)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(AbandonAsyncBase.AbandonCompletionCallback), ar);
		}

		// Token: 0x04000132 RID: 306
		protected Stopwatch m_timeActive = new Stopwatch();
	}
}
