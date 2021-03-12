using System;
using System.Net.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.ManageDelegation1;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x0200033E RID: 830
	internal sealed class ManageDelegation1Client : ManageDelegationClient, IDisposable
	{
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0007E270 File Offset: 0x0007C470
		protected override CustomSoapHttpClientProtocol Client
		{
			get
			{
				return this.manageDelegation;
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0007E278 File Offset: 0x0007C478
		public ManageDelegation1Client(string certificate, WriteVerboseDelegate writeVerbose) : base(LiveConfiguration.GetDomainServicesEpr().ToString(), certificate, writeVerbose)
		{
			this.manageDelegation = new ManageDelegation("ManageDelegation", new RemoteCertificateValidationCallback(ManageDelegationClient.InvalidCertificateHandler));
			this.manageDelegation.ClientCertificates.Add(base.Certificate);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0007E2CA File Offset: 0x0007C4CA
		public void Dispose()
		{
			if (this.manageDelegation != null)
			{
				this.manageDelegation.Dispose();
				this.manageDelegation = null;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0007E314 File Offset: 0x0007C514
		public AppIdInfo CreateAppId(string rawBase64Certificate)
		{
			AppIdInfo appIdInfo = null;
			base.ExecuteAndHandleError(string.Format("CreateAppId(certificate='{0}',properties=[])", rawBase64Certificate), delegate
			{
				appIdInfo = this.manageDelegation.CreateAppId(rawBase64Certificate, new Property[0]);
			});
			return appIdInfo;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0007E390 File Offset: 0x0007C590
		public void UpdateAppIdCertificate(string applicationId, string adminKey, string rawBase64Certificate)
		{
			base.ExecuteAndHandleError(string.Format("UpdateAppIdCertificate(applicationId='{0}',adminKey='****', certificate='{1}')", applicationId, rawBase64Certificate), delegate
			{
				this.manageDelegation.UpdateAppIdCertificate(applicationId, adminKey, rawBase64Certificate);
			});
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0007E410 File Offset: 0x0007C610
		public override void AddUri(string applicationId, string uri)
		{
			base.ExecuteAndHandleError(string.Format("AddUri(applicationId='{0}',uri='{1}')", applicationId, uri), delegate
			{
				this.manageDelegation.AddUri(applicationId, uri);
			});
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0007E488 File Offset: 0x0007C688
		public override void RemoveUri(string applicationId, string uri)
		{
			base.ExecuteAndHandleError(string.Format("RemoveUri(applicationId='{0}',uri='{1}')", applicationId, uri), delegate
			{
				this.manageDelegation.RemoveUri(applicationId, uri);
			});
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0007E504 File Offset: 0x0007C704
		public override void ReserveDomain(string applicationId, string domain, string programId)
		{
			base.ExecuteAndHandleError(string.Format("ReserveDomain(applicationId='{0}',domain='{1}',programId='{2}')", applicationId, domain, programId), delegate
			{
				this.manageDelegation.ReserveDomain(applicationId, domain, programId);
			});
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0007E588 File Offset: 0x0007C788
		public override void ReleaseDomain(string applicationId, string domain)
		{
			base.ExecuteAndHandleError(string.Format("ReleaseDomain(applicationId='{0}',domain='{1}')", applicationId, domain), delegate
			{
				this.manageDelegation.ReleaseDomain(applicationId, domain);
			});
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0007E604 File Offset: 0x0007C804
		public DomainInfo GetDomainInfo(string applicationId, string domain)
		{
			DomainInfo domainInfo = null;
			base.ExecuteAndHandleError(string.Format("GetDomainInfo(applicationId='{0}',domain='{1}')", applicationId, domain), delegate
			{
				domainInfo = this.manageDelegation.GetDomainInfo(applicationId, domain);
			});
			return domainInfo;
		}

		// Token: 0x04001842 RID: 6210
		private const string ComponentId = "ManageDelegation";

		// Token: 0x04001843 RID: 6211
		private ManageDelegation manageDelegation;
	}
}
