using System;
using System.ServiceModel;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200000E RID: 14
	public class NotificationListener
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027B4 File Offset: 0x000009B4
		internal TimeSpan RetryDelay
		{
			get
			{
				return this.m_retryDelay;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000027BC File Offset: 0x000009BC
		internal TimeSpan OpenTimeout
		{
			get
			{
				return this.m_openTimeout;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027C4 File Offset: 0x000009C4
		internal TimeSpan SendTimeout
		{
			get
			{
				return this.m_sendTimeout;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000027CC File Offset: 0x000009CC
		internal TimeSpan ReceiveTimeout
		{
			get
			{
				return this.m_receiveTimeout;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000027D4 File Offset: 0x000009D4
		public static NotificationListener Start(INotifyCallback callback, TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			Exception ex = null;
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug(0L, "NotificationListener is starting.");
			NotificationListener notificationListener = new NotificationListener();
			try
			{
				notificationListener.m_retryDelay = retryDelay;
				notificationListener.m_openTimeout = openTimeout;
				notificationListener.m_sendTimeout = sendTimeout;
				notificationListener.m_receiveTimeout = receiveTimeout;
				notificationListener.SetupNotifyHost(callback);
				ReplayCrimsonEvents.TPRNotificationListenerStarted.Log();
				ExTraceGlobals.ThirdPartyClientTracer.TraceDebug(0L, "NotificationListener was started successfully.");
				notificationListener.SendTimeouts();
				return notificationListener;
			}
			catch (CommunicationException ex2)
			{
				ex = ex2;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ReplayCrimsonEvents.TPRNotificationListenerFailedToStart.Log<string>(ex.Message);
				ExTraceGlobals.ThirdPartyClientTracer.TraceError<Exception>(0L, "NotificationListener failed to start: {0}", ex);
				throw ex;
			}
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002898 File Offset: 0x00000A98
		public void Stop()
		{
			ReplayCrimsonEvents.TPRNotificationListenerStopped.Log();
			if (this.m_notifyHost != null)
			{
				this.m_notifyHost.Close();
			}
			if (this.m_service != null)
			{
				this.m_service = null;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028C8 File Offset: 0x00000AC8
		private void SetupNotifyHost(INotifyCallback callback)
		{
			this.m_service = new NotifyService(callback, this);
			EndpointAddress endpointAddress = new EndpointAddress("net.pipe://localhost/Microsoft.Exchange.ThirdPartyReplication.NotifyService");
			this.m_notifyHost = new ServiceHost(this.m_service, new Uri[]
			{
				endpointAddress.Uri
			});
			NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding();
			netNamedPipeBinding.OpenTimeout = this.OpenTimeout;
			netNamedPipeBinding.SendTimeout = this.SendTimeout;
			netNamedPipeBinding.ReceiveTimeout = this.ReceiveTimeout;
			try
			{
				this.m_notifyHost.AddServiceEndpoint(typeof(IInternalNotify), netNamedPipeBinding, string.Empty);
				this.m_notifyHost.Open();
			}
			catch (CommunicationException ex)
			{
				this.m_notifyHost.Abort();
				this.m_notifyHost = null;
				throw ex;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002988 File Offset: 0x00000B88
		private void SendTimeouts()
		{
			lock (this)
			{
				Exception ex = null;
				try
				{
					using (Request request = Request.Open(this.m_openTimeout, this.m_sendTimeout, this.m_receiveTimeout))
					{
						request.AmeIsStarting(this.m_retryDelay, this.m_openTimeout, this.m_sendTimeout, this.m_receiveTimeout);
					}
				}
				catch (FailedCommunicationException ex2)
				{
					ex = ex2;
				}
				catch (Exception ex3)
				{
					ReplayCrimsonEvents.TPRUnexpectedException.Log<string, Exception>(ex3.Message, ex3);
					ex = ex3;
				}
				if (ex != null)
				{
					ExTraceGlobals.ThirdPartyClientTracer.TraceError<Exception>(0L, "NotificationListener failed to SendTimeouts. ActiveManager is expected to contact us later: {0}", ex);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private ServiceHost m_notifyHost;

		// Token: 0x04000007 RID: 7
		private NotifyService m_service;

		// Token: 0x04000008 RID: 8
		private TimeSpan m_retryDelay;

		// Token: 0x04000009 RID: 9
		private TimeSpan m_openTimeout;

		// Token: 0x0400000A RID: 10
		private TimeSpan m_sendTimeout;

		// Token: 0x0400000B RID: 11
		private TimeSpan m_receiveTimeout;
	}
}
