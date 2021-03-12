using System;
using System.Net.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.ManageDelegation2;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000340 RID: 832
	internal sealed class ManageDelegation2Client : ManageDelegationClient, IDisposable
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x0007F258 File Offset: 0x0007D458
		protected override CustomSoapHttpClientProtocol Client
		{
			get
			{
				return this.manageDelegation;
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0007F260 File Offset: 0x0007D460
		public ManageDelegation2Client(string domain, string signingDomain, string certificateThumbprint, WriteVerboseDelegate writeVerbose) : base(LiveConfiguration.GetDomainServices2Epr().ToString(), certificateThumbprint, writeVerbose)
		{
			this.manageDelegation = new ManageDelegation2("ManageDelegation2", new RemoteCertificateValidationCallback(ManageDelegationClient.InvalidCertificateHandler));
			this.manageDelegation.Authenticator = SoapHttpClientAuthenticator.Create(base.Certificate);
			this.manageDelegation.DomainOwnershipProofHeaderValue = new DomainOwnershipProofHeader
			{
				Domain = domain,
				HashAlgorithm = "SHA-512",
				Signature = Convert.ToBase64String(FederatedDomainProofAlgorithm.GetSignature(base.Certificate, signingDomain))
			};
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0007F2ED File Offset: 0x0007D4ED
		public void Dispose()
		{
			if (this.manageDelegation != null)
			{
				this.manageDelegation.Dispose();
				this.manageDelegation = null;
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0007F338 File Offset: 0x0007D538
		public AppIdInfo CreateAppId(string uri)
		{
			AppIdInfo appIdInfo = null;
			base.ExecuteAndHandleError(string.Format("CreateAppId(uri='{0}',properties=[0])", uri), delegate
			{
				appIdInfo = this.manageDelegation.CreateAppId(uri, new Property[0]);
			});
			return appIdInfo;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0007F3B0 File Offset: 0x0007D5B0
		public void UpdateAppIdCertificate(string applicationId, string rawBase64Certificate)
		{
			base.ExecuteAndHandleError(string.Format("UpdateAppIdCertificate(applicationId='{0}',certificate='{1}')", applicationId, rawBase64Certificate), delegate
			{
				this.manageDelegation.UpdateAppIdCertificate(applicationId, rawBase64Certificate);
			});
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0007F428 File Offset: 0x0007D628
		public override void AddUri(string applicationId, string uri)
		{
			base.ExecuteAndHandleError(string.Format("AddUri(applicationId='{0}',uri='{1}')", applicationId, uri), delegate
			{
				this.manageDelegation.AddUri(applicationId, uri);
			});
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0007F4A0 File Offset: 0x0007D6A0
		public override void RemoveUri(string applicationId, string uri)
		{
			base.ExecuteAndHandleError(string.Format("RemoveUri(applicationId='{0}',uri='{1}')", applicationId, uri), delegate
			{
				this.manageDelegation.RemoveUri(applicationId, uri);
			});
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0007F51C File Offset: 0x0007D71C
		public override void ReserveDomain(string applicationId, string domain, string programId)
		{
			base.ExecuteAndHandleError(string.Format("ReserveDomain(applicationId='{0}',domain='{1}',programId='{2}')", applicationId, domain, programId), delegate
			{
				this.manageDelegation.ReserveDomain(applicationId, domain, programId);
			});
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0007F5A0 File Offset: 0x0007D7A0
		public override void ReleaseDomain(string applicationId, string domain)
		{
			base.ExecuteAndHandleError(string.Format("ReleaseDomain(applicationId='{0}',domain='{1}')", applicationId, domain), delegate
			{
				this.manageDelegation.ReleaseDomain(applicationId, domain);
			});
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0007F61C File Offset: 0x0007D81C
		public DomainInfo GetDomainInfo(string applicationId, string domain)
		{
			DomainInfo domainInfo = null;
			base.ExecuteAndHandleError(string.Format("GetDomainInfo(applicationId='{0}',domain='{1}')", applicationId, domain), delegate
			{
				domainInfo = this.manageDelegation.GetDomainInfo(applicationId, domain);
			});
			return domainInfo;
		}

		// Token: 0x04001858 RID: 6232
		private const string ComponentId = "ManageDelegation2";

		// Token: 0x04001859 RID: 6233
		private ManageDelegation2 manageDelegation;
	}
}
