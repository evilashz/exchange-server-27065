using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000C1 RID: 193
	internal sealed class RequestDispatcher : BaseRequestDispatcher
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00015BDC File Offset: 0x00013DDC
		public RequestDispatcher(RequestLogger requestLogger)
		{
			this.requestLogger = requestLogger;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015C24 File Offset: 0x00013E24
		public bool Execute(DateTime deadline, HttpResponse httpResponse)
		{
			this.ThrowIfClientDisconnected(httpResponse);
			this.requestLogger.CaptureRequestStage("RequestDispatcher.PreQuery");
			RequestDispatcher.RequestRoutingTracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: Dispatching all {1} requests", TraceContext.Get(), base.Requests.Count);
			bool flag = false;
			using (ManualResetEvent done = new ManualResetEvent(false))
			{
				this.BeginInvoke(delegate(AsyncTask task)
				{
					try
					{
						done.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				});
				this.requestLogger.CaptureRequestStage("RequestDispatcher.BeginInvoke");
				RequestDispatcher.RequestRoutingTracer.TraceDebug<object, DateTime>((long)this.GetHashCode(), "{0}: Waiting for requests to complete by {1}", TraceContext.Get(), deadline);
				do
				{
					this.ThrowIfClientDisconnected(httpResponse);
					flag = done.WaitOne(RequestDispatcher.DisconnectCheckInterval, false);
				}
				while (DateTime.UtcNow < deadline && !flag);
			}
			this.requestLogger.CaptureRequestStage("RequestDispatcher.Complete");
			if (!flag)
			{
				try
				{
					RequestDispatcher.RequestRoutingTracer.TraceError<object, DateTime>((long)this.GetHashCode(), "{0}: Not all requests did not complete by {1}. Aborting now.", TraceContext.Get(), deadline);
				}
				finally
				{
					this.Abort();
				}
				this.requestLogger.CaptureRequestStage("RequestDispatcher.Abort");
			}
			else
			{
				RequestDispatcher.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: All requests completed in time", new object[]
				{
					TraceContext.Get()
				});
			}
			return flag;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015D94 File Offset: 0x00013F94
		private void ThrowIfClientDisconnected(HttpResponse httpResponse)
		{
			if (httpResponse != null && !httpResponse.IsClientConnected)
			{
				RequestDispatcher.RequestRoutingTracer.TraceDebug((long)this.GetHashCode(), "{0}: Client has disconnected. Aborting now.", new object[]
				{
					TraceContext.Get()
				});
				throw new ClientDisconnectedException();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public override string ToString()
		{
			return "RequestDispatcher for " + base.Requests.Count + " requests";
		}

		// Token: 0x040002CD RID: 717
		private RequestLogger requestLogger;

		// Token: 0x040002CE RID: 718
		private static readonly TimeSpan DisconnectCheckInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x040002CF RID: 719
		private static readonly Trace RequestRoutingTracer = ExTraceGlobals.RequestRoutingTracer;
	}
}
