using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.FederationProvisioning;
using Microsoft.Exchange.Management.ManageDelegation2;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009ED RID: 2541
	internal sealed class LiveFederationProvision2 : LiveFederationProvision
	{
		// Token: 0x06005AAB RID: 23211 RVA: 0x0017BC9D File Offset: 0x00179E9D
		public LiveFederationProvision2(string certificateThumbprint, string applicationIdentifier, Task task) : base(certificateThumbprint, applicationIdentifier, task)
		{
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x0017BCA8 File Offset: 0x00179EA8
		public override void OnNewFederationTrust(FederationTrust federationTrust)
		{
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x0017BCD8 File Offset: 0x00179ED8
		public override void OnSetFederatedOrganizationIdentifier(FederationTrust federationTrust, SmtpDomain accountNamespace)
		{
			string text = accountNamespace.ToString();
			string wkgDomain = FederatedOrganizationId.AddHybridConfigurationWellKnownSubDomain(text);
			AppIdInfo appIdInfo = null;
			ManageDelegation2Client client = this.GetManageDelegation(wkgDomain);
			try
			{
				appIdInfo = client.CreateAppId(wkgDomain);
			}
			catch (LiveDomainServicesException ex)
			{
				if (ex.DomainError != null && ex.DomainError.Value == DomainError.ProofOfOwnershipNotValid)
				{
					throw new DomainProofOwnershipException(Strings.ErrorManageDelegation2ProofDomainOwnership, ex);
				}
				throw new ProvisioningFederatedExchangeException(ex.LocalizedString, ex);
			}
			if (string.IsNullOrEmpty(federationTrust.ApplicationIdentifier))
			{
				if (appIdInfo == null || string.IsNullOrEmpty(appIdInfo.AppId))
				{
					throw new LiveDomainServicesException(Strings.ErrorLiveDomainServicesUnexpectedResult(Strings.ErrorInvalidApplicationId));
				}
				base.WriteVerbose(Strings.NewFederationTrustSuccessAppId(FederationTrust.PartnerSTSType.LiveId.ToString(), appIdInfo.AppId));
				federationTrust.ApplicationIdentifier = appIdInfo.AppId.Trim();
			}
			base.ReserveDomain(wkgDomain, federationTrust.ApplicationIdentifier, client, Strings.ErrorManageDelegation2ProofDomainOwnership, () => LiveFederationProvision2.GetDomainStateFromDomainInfo(client.GetDomainInfo(federationTrust.ApplicationIdentifier, wkgDomain)));
			using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(text))
			{
				manageDelegation.AddUri(appIdInfo.AppId, text);
			}
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x0017BE5C File Offset: 0x0017A05C
		public override void OnAddFederatedDomain(SmtpDomain smtpDomain)
		{
			string domain = smtpDomain.ToString();
			using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(domain))
			{
				base.AddUri(domain, base.ApplicationIdentifier, manageDelegation, Strings.ErrorManageDelegation2ProofDomainOwnership);
			}
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x0017BEE8 File Offset: 0x0017A0E8
		public override void OnRemoveAccountNamespace(SmtpDomain smtpDomain, bool force)
		{
			LiveFederationProvision2.<>c__DisplayClass4 CS$<>8__locals1 = new LiveFederationProvision2.<>c__DisplayClass4();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.domain = smtpDomain.ToString();
			if (FederatedOrganizationId.ContainsHybridConfigurationWellKnownSubDomain(CS$<>8__locals1.domain))
			{
				string text = FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(CS$<>8__locals1.domain);
				using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(text))
				{
					base.RemoveUri(manageDelegation, text, force);
				}
			}
			using (ManageDelegation2Client client = this.GetManageDelegation(CS$<>8__locals1.domain))
			{
				base.RemoveUri(client, CS$<>8__locals1.domain, force);
				base.ReleaseDomain(CS$<>8__locals1.domain, base.ApplicationIdentifier, force, client, () => LiveFederationProvision2.GetDomainStateFromDomainInfo(client.GetDomainInfo(CS$<>8__locals1.<>4__this.ApplicationIdentifier, CS$<>8__locals1.domain)));
			}
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x0017BFE0 File Offset: 0x0017A1E0
		public override void OnRemoveFederatedDomain(SmtpDomain smtpDomain, bool force)
		{
			string text = smtpDomain.ToString();
			using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(text))
			{
				base.RemoveUri(manageDelegation, text, force);
			}
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x0017C024 File Offset: 0x0017A224
		public override void OnPublishFederationCertificate(FederationTrust federationTrust)
		{
			X509Certificate2 x509Certificate = FederationCertificate.LoadCertificateWithPrivateKey(federationTrust.OrgNextPrivCertificate, base.WriteVerbose);
			string rawBase64Certificate = Convert.ToBase64String(x509Certificate.GetRawCertData());
			using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(federationTrust.ApplicationUri.OriginalString))
			{
				manageDelegation.UpdateAppIdCertificate(federationTrust.ApplicationIdentifier, rawBase64Certificate);
			}
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0017C08C File Offset: 0x0017A28C
		public override Microsoft.Exchange.Data.Directory.Management.DomainState GetDomainState(string domain)
		{
			Microsoft.Exchange.Data.Directory.Management.DomainState domainStateFromDomainInfo;
			using (ManageDelegation2Client manageDelegation = this.GetManageDelegation(domain))
			{
				domainStateFromDomainInfo = LiveFederationProvision2.GetDomainStateFromDomainInfo(manageDelegation.GetDomainInfo(base.ApplicationIdentifier, domain));
			}
			return domainStateFromDomainInfo;
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0017C0D4 File Offset: 0x0017A2D4
		private static Microsoft.Exchange.Data.Directory.Management.DomainState GetDomainStateFromDomainInfo(DomainInfo domainInfo)
		{
			if (domainInfo != null)
			{
				switch (domainInfo.DomainState)
				{
				case Microsoft.Exchange.Management.ManageDelegation2.DomainState.PendingActivation:
					return Microsoft.Exchange.Data.Directory.Management.DomainState.PendingActivation;
				case Microsoft.Exchange.Management.ManageDelegation2.DomainState.Active:
					return Microsoft.Exchange.Data.Directory.Management.DomainState.Active;
				case Microsoft.Exchange.Management.ManageDelegation2.DomainState.PendingRelease:
					return Microsoft.Exchange.Data.Directory.Management.DomainState.PendingRelease;
				}
			}
			return Microsoft.Exchange.Data.Directory.Management.DomainState.Unknown;
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x0017C108 File Offset: 0x0017A308
		private ManageDelegation2Client GetManageDelegation(string domain)
		{
			string signingDomain = FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(domain);
			return new ManageDelegation2Client(domain, signingDomain, base.CertificateThumbprint, base.WriteVerbose);
		}
	}
}
