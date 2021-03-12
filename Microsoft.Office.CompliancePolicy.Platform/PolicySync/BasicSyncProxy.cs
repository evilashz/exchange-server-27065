using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000EA RID: 234
	public sealed class BasicSyncProxy : IPolicySyncWebserviceClient, IDisposable
	{
		// Token: 0x0600065A RID: 1626 RVA: 0x0001401C File Offset: 0x0001221C
		internal BasicSyncProxy(IPolicySyncWebservice webserviceProxy, ChannelFactory<IPolicySyncWebservice> channelFactory, string partnerName)
		{
			if (webserviceProxy == null)
			{
				throw new ArgumentNullException("webserviceProxy");
			}
			if (channelFactory == null)
			{
				throw new ArgumentNullException("channelFactory");
			}
			if (string.IsNullOrWhiteSpace(partnerName))
			{
				throw new ArgumentNullException("partnerName");
			}
			this.webserviceProxy = webserviceProxy;
			this.channelFactory = channelFactory;
			this.partnerName = partnerName;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014074 File Offset: 0x00012274
		public static BasicSyncProxy Create(EndpointAddress endpoint, X509Certificate2 certificate, string partnerName)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException("endpoint");
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (string.IsNullOrWhiteSpace(partnerName))
			{
				throw new ArgumentNullException("partnerName");
			}
			ChannelFactory<IPolicySyncWebservice> channelFactory = new ChannelFactory<IPolicySyncWebservice>(new WSHttpBinding(SecurityMode.Transport)
			{
				MaxReceivedMessageSize = 26214400L,
				Security = 
				{
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.Certificate
					}
				}
			}, endpoint);
			channelFactory.Credentials.ClientCertificate.Certificate = certificate;
			IPolicySyncWebservice policySyncWebservice = channelFactory.CreateChannel(endpoint);
			return new BasicSyncProxy(policySyncWebservice, channelFactory, partnerName);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014104 File Offset: 0x00012304
		public PolicyChange GetSingleTenantChanges(TenantCookie tenantCookie)
		{
			PolicyChange policyChange = null;
			TenantCookieCollection tenantCookieCollection = new TenantCookieCollection(tenantCookie.Workload, tenantCookie.ObjectType);
			tenantCookieCollection[tenantCookie.TenantId] = tenantCookie;
			PolicyChangeBatch changes = this.webserviceProxy.GetChanges(new GetChangesRequest
			{
				CallerContext = SyncCallerContext.Create(this.partnerName),
				TenantCookies = tenantCookieCollection
			});
			if (changes != null)
			{
				policyChange = new PolicyChange();
				policyChange.Changes = changes.Changes;
				TenantCookie newCookie = null;
				if (changes.NewCookies != null)
				{
					changes.NewCookies.TryGetCookie(tenantCookie.TenantId, out newCookie);
				}
				policyChange.NewCookie = newCookie;
			}
			return policyChange;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001419C File Offset: 0x0001239C
		public IAsyncResult BeginGetSingleTenantChanges(TenantCookie tenantCookie, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000141A3 File Offset: 0x000123A3
		public PolicyChange EndGetSingleTenantChanges(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000141AA File Offset: 0x000123AA
		public PolicyChangeBatch GetChanges(GetChangesRequest request)
		{
			return this.webserviceProxy.GetChanges(request);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000141B8 File Offset: 0x000123B8
		public IAsyncResult BeginGetChanges(GetChangesRequest request, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000141BF File Offset: 0x000123BF
		public PolicyChangeBatch EndGetChanges(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000141C6 File Offset: 0x000123C6
		public PolicyConfigurationBase GetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects)
		{
			return this.webserviceProxy.GetObject(callerContext, tenantId, objectType, objectId, includeDeletedObjects);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000141DA File Offset: 0x000123DA
		public IAsyncResult BeginGetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000141E1 File Offset: 0x000123E1
		public PolicyConfigurationBase EndGetObject(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000141E8 File Offset: 0x000123E8
		public void PublishStatus(PublishStatusRequest request)
		{
			this.webserviceProxy.PublishStatus(request);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000141F6 File Offset: 0x000123F6
		public IAsyncResult BeginPublishStatus(PublishStatusRequest request, AsyncCallback userCallback, object stateObject)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000141FD File Offset: 0x000123FD
		public void EndPublishStatus(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00014204 File Offset: 0x00012404
		public void Dispose()
		{
			if (this.channelFactory != null)
			{
				this.channelFactory.Close();
				this.channelFactory = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x040003C0 RID: 960
		private readonly IPolicySyncWebservice webserviceProxy;

		// Token: 0x040003C1 RID: 961
		private readonly string partnerName;

		// Token: 0x040003C2 RID: 962
		private ChannelFactory<IPolicySyncWebservice> channelFactory;
	}
}
