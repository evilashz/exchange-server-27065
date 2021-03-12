using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000005 RID: 5
	public class Request : IDisposable
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002384 File Offset: 0x00000584
		public static Request Open(TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			Request request = new Request();
			EndpointAddress remoteAddress = new EndpointAddress("net.pipe://localhost/Microsoft.Exchange.ThirdPartyReplication.RequestService");
			request.m_wcfClient = new InternalRequestClient(ClientServices.SetupBinding(openTimeout, sendTimeout, receiveTimeout), remoteAddress);
			return request;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023B7 File Offset: 0x000005B7
		public void Dispose()
		{
			if (this.m_wcfClient != null)
			{
				ExTraceGlobals.ThirdPartyClientTracer.TraceDebug((long)this.GetHashCode(), "Request.Dispose invoked");
				this.m_wcfClient.Abort();
				this.m_wcfClient = null;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002410 File Offset: 0x00000610
		public string GetPrimaryActiveManager()
		{
			byte[] exBytes = null;
			string pam = null;
			ClientServices.CallService(delegate
			{
				pam = this.m_wcfClient.GetPrimaryActiveManager(out exBytes);
			});
			if (exBytes != null)
			{
				Exception ex = (Exception)Serialization.BytesToObject(exBytes);
				ExTraceGlobals.ThirdPartyClientTracer.TraceError<Exception>((long)this.GetHashCode(), "GetPAM fails: {0}", ex);
				throw ex;
			}
			ExTraceGlobals.ThirdPartyClientTracer.TraceDebug<string>((long)this.GetHashCode(), "GetPAM returns: {0}", pam);
			return pam;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024F8 File Offset: 0x000006F8
		public void ChangeActiveServer(Guid dbId, string newNode)
		{
			Exception serverEx = null;
			byte[] exBytes = null;
			ClientServices.CallService(delegate
			{
				exBytes = this.m_wcfClient.ChangeActiveServer(dbId, newNode);
				if (exBytes != null)
				{
					serverEx = (Exception)Serialization.BytesToObject(exBytes);
				}
			});
			if (serverEx != null)
			{
				ExTraceGlobals.ThirdPartyClientTracer.TraceError<Guid, string, Exception>((long)this.GetHashCode(), "ChangeActiveServer({0},{1}) fails: {2}", dbId, newNode, serverEx);
				throw serverEx;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025BC File Offset: 0x000007BC
		public void ImmediateDismountMailboxDatabase(Guid dbId)
		{
			Exception serverEx = null;
			byte[] exBytes = null;
			ClientServices.CallService(delegate
			{
				exBytes = this.m_wcfClient.ImmediateDismountMailboxDatabase(dbId);
				if (exBytes != null)
				{
					serverEx = (Exception)Serialization.BytesToObject(exBytes);
				}
			});
			if (serverEx != null)
			{
				ExTraceGlobals.ThirdPartyClientTracer.TraceError<Guid, Exception>((long)this.GetHashCode(), "ImmediateDismountMailboxDatabase({0}) fails: {1}", dbId, serverEx);
				throw serverEx;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002660 File Offset: 0x00000860
		internal void AmeIsStarting(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			ClientServices.CallService(delegate
			{
				this.m_wcfClient.AmeIsStarting(retryDelay, openTimeout, sendTimeout, receiveTimeout);
			});
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026B5 File Offset: 0x000008B5
		internal void AmeIsStopping()
		{
			ClientServices.CallService(delegate
			{
				this.m_wcfClient.AmeIsStopping();
			});
		}

		// Token: 0x04000002 RID: 2
		private InternalRequestClient m_wcfClient;
	}
}
