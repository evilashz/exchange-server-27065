using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000004 RID: 4
	internal class Notify : IDisposable
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public static Notify Open(TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			Notify notify = new Notify();
			EndpointAddress remoteAddress = new EndpointAddress("net.pipe://localhost/Microsoft.Exchange.ThirdPartyReplication.NotifyService");
			notify.m_wcfClient = new InternalNotifyClient(ClientServices.SetupBinding(openTimeout, sendTimeout, receiveTimeout), remoteAddress);
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug((long)notify.GetHashCode(), "Notify.Open successful");
			return notify;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002195 File Offset: 0x00000395
		public void Dispose()
		{
			if (this.m_wcfClient != null)
			{
				ExTraceGlobals.ThirdPartyClientTracer.TraceDebug((long)this.GetHashCode(), "Notify.Dispose invoked");
				this.m_wcfClient.Abort();
				this.m_wcfClient = null;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C7 File Offset: 0x000003C7
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021DE File Offset: 0x000003DE
		internal TimeSpan SendTimeout
		{
			get
			{
				return this.m_wcfClient.Endpoint.Binding.SendTimeout;
			}
			set
			{
				this.m_wcfClient.Endpoint.Binding.SendTimeout = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021F6 File Offset: 0x000003F6
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000220D File Offset: 0x0000040D
		internal TimeSpan ReceiveTimeout
		{
			get
			{
				return this.m_wcfClient.Endpoint.Binding.ReceiveTimeout;
			}
			set
			{
				this.m_wcfClient.Endpoint.Binding.ReceiveTimeout = value;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002232 File Offset: 0x00000432
		public void BecomePame()
		{
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug((long)this.GetHashCode(), "Notify.BecomePame attempted");
			ClientServices.CallService(delegate
			{
				this.m_wcfClient.BecomePame();
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002268 File Offset: 0x00000468
		public void RevokePame()
		{
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug((long)this.GetHashCode(), "Notify.RevokePame attempted");
			ClientServices.CallService(delegate
			{
				this.m_wcfClient.RevokePame();
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022C4 File Offset: 0x000004C4
		public NotificationResponse DatabaseMoveNeeded(Guid dbId, string currentActiveFqdn, bool mountDesired)
		{
			NotificationResponse rc = NotificationResponse.Incomplete;
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug<Guid, string, bool>((long)this.GetHashCode(), "Notify.DatabaseMoveNeeded({0},{1},{2}) attempted", dbId, currentActiveFqdn, mountDesired);
			ClientServices.CallService(delegate
			{
				rc = this.m_wcfClient.DatabaseMoveNeeded(dbId, currentActiveFqdn, mountDesired);
			});
			return rc;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000233C File Offset: 0x0000053C
		internal void GetTimeouts(out TimeSpan retryDelay, out TimeSpan openTimeout, out TimeSpan sendTimeout, out TimeSpan receiveTimeout)
		{
			try
			{
				this.m_wcfClient.GetTimeouts(out retryDelay, out openTimeout, out sendTimeout, out receiveTimeout);
			}
			catch (Exception ex)
			{
				throw new FailedCommunicationException(ex.Message, ex);
			}
		}

		// Token: 0x04000001 RID: 1
		private InternalNotifyClient m_wcfClient;
	}
}
