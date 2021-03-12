using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000268 RID: 616
	internal class PingProber : IDisposable
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x000633E6 File Offset: 0x000615E6
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.NetworkManagerTracer;
			}
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000633F0 File Offset: 0x000615F0
		public static PingRequest FindRequest(PingRequest[] requests, IPAddress searchForAddr)
		{
			if (requests != null)
			{
				foreach (PingRequest pingRequest in requests)
				{
					if (pingRequest.IPAddress.Equals(searchForAddr))
					{
						return pingRequest;
					}
				}
			}
			return null;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00063429 File Offset: 0x00061629
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00063438 File Offset: 0x00061638
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				lock (this)
				{
					this.m_proberState = ProberState.Disposed;
					if (disposing)
					{
						this.m_sourcePort.Close();
						((IDisposable)this.m_probeCompleteEvent).Dispose();
					}
					this.m_disposed = true;
				}
			}
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0006349C File Offset: 0x0006169C
		public PingProber(IPAddress sourceIp)
		{
			this.m_sourcePort = new PingSource(sourceIp, 3000);
			this.m_probeCompleteEvent = new ManualResetEvent(false);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000634CC File Offset: 0x000616CC
		private void CompletionCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Any, 0);
				EndPoint endPoint = ipendPoint;
				this.m_sourcePort.Socket.EndReceiveFrom(ar, ref endPoint);
				ipendPoint = (IPEndPoint)endPoint;
				PingRequest pingRequest = PingProber.FindRequest(this.m_activeRequests, ipendPoint.Address);
				if (pingRequest == null)
				{
					PingProber.Tracer.TraceError<IPEndPoint>((long)this.GetHashCode(), "Unexpected response from {0}", ipendPoint);
				}
				else
				{
					pingRequest.StopTimeStamp = Win32StopWatch.GetSystemPerformanceCounter();
					PingProber.Tracer.TraceDebug<IPEndPoint>((long)this.GetHashCode(), "Response from {0}.", ipendPoint);
					pingRequest.Success = true;
				}
			}
			catch (SocketException ex2)
			{
				ex = ex2;
			}
			catch (ObjectDisposedException ex3)
			{
				ex = ex3;
			}
			finally
			{
				lock (this)
				{
					this.m_completionCount++;
					if (this.m_completionCount == this.m_sendCount && (this.m_proberState == ProberState.Sent || this.m_proberState == ProberState.Gathering))
					{
						this.m_probeCompleteEvent.Set();
					}
				}
				if (ex != null && this.m_proberState != ProberState.Disposed)
				{
					PingProber.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Ping completion failure: {0}", ex);
				}
			}
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0006361C File Offset: 0x0006181C
		public void SendPings(PingRequest[] targets)
		{
			if (this.m_proberState != ProberState.Idle)
			{
				throw new ArgumentException("user must gather between send intervals");
			}
			try
			{
				this.m_proberState = ProberState.Sending;
				this.m_activeRequests = targets;
				foreach (PingRequest pingRequest in targets)
				{
					IPEndPoint ipendPoint = new IPEndPoint(pingRequest.IPAddress, 0);
					pingRequest.StartTimeStamp = Win32StopWatch.GetSystemPerformanceCounter();
					if (this.m_pingPacket.Length != this.m_sourcePort.Socket.SendTo(this.m_pingPacket, ipendPoint))
					{
						throw new SocketException();
					}
					this.m_sendCount++;
					EndPoint endPoint = ipendPoint;
					this.m_sourcePort.Socket.BeginReceiveFrom(pingRequest.ReplyBuffer, 0, pingRequest.ReplyBuffer.Length, SocketFlags.None, ref endPoint, new AsyncCallback(this.CompletionCallback), null);
				}
			}
			finally
			{
				this.m_proberState = ProberState.Sent;
			}
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00063704 File Offset: 0x00061904
		public void GatherReplies(int maxWaitInMs)
		{
			if (this.m_proberState != ProberState.Sent)
			{
				throw new ArgumentException("user must send befor gathering");
			}
			try
			{
				this.m_proberState = ProberState.Gathering;
				this.m_probeCompleteEvent.WaitOne(maxWaitInMs, false);
			}
			finally
			{
				this.m_proberState = ProberState.Idle;
				PingRequest[] activeRequests = this.m_activeRequests;
				this.m_activeRequests = null;
				Win32StopWatch.GetSystemPerformanceCounter();
				foreach (PingRequest pingRequest in activeRequests)
				{
					if (pingRequest.StopTimeStamp == 0L || pingRequest.StopTimeStamp < pingRequest.StartTimeStamp)
					{
						pingRequest.TimedOut = true;
					}
					else
					{
						pingRequest.LatencyInUSec = Win32StopWatch.ComputeElapsedTimeInUSec(pingRequest.StopTimeStamp, pingRequest.StartTimeStamp);
					}
				}
			}
		}

		// Token: 0x04000995 RID: 2453
		public const int DefaultPingTimeout = 3000;

		// Token: 0x04000996 RID: 2454
		private bool m_disposed;

		// Token: 0x04000997 RID: 2455
		private ProberState m_proberState;

		// Token: 0x04000998 RID: 2456
		private PingSource m_sourcePort;

		// Token: 0x04000999 RID: 2457
		private ManualResetEvent m_probeCompleteEvent;

		// Token: 0x0400099A RID: 2458
		private byte[] m_pingPacket = PingPacket.FormPacket();

		// Token: 0x0400099B RID: 2459
		private PingRequest[] m_activeRequests;

		// Token: 0x0400099C RID: 2460
		private int m_sendCount;

		// Token: 0x0400099D RID: 2461
		private int m_completionCount;
	}
}
