using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200010C RID: 268
	internal class DatacenterSipPeerManager : SipPeerManager
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001E86B File Offset: 0x0001CA6B
		internal override UMSipPeer AVAuthenticationService
		{
			get
			{
				return this.avAuthenticationService;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001E873 File Offset: 0x0001CA73
		internal override UMSipPeer SIPAccessService
		{
			get
			{
				return this.sipAccessService;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001E87B File Offset: 0x0001CA7B
		internal override bool IsHeuristicBasedSIPAccessService
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001E87E File Offset: 0x0001CA7E
		internal override UMSipPeer SBCService
		{
			get
			{
				return this.sbcService;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001E888 File Offset: 0x0001CA88
		internal DatacenterSipPeerManager(UMADSettings adSettings) : base(adSettings)
		{
			this.sipPeerList = new List<UMSipPeer>();
			Organization organization = this.ReadForestWideSettings();
			ProtocolConnectionSettings sipaccessService = organization.SIPAccessService;
			if (sipaccessService != null)
			{
				this.sipAccessService = new UMSipPeer(new UMSmartHost(sipaccessService.Hostname.HostnameString), sipaccessService.Port, true, true, true, IPAddressFamily.Any);
				this.sipPeerList.Add(this.sipAccessService);
			}
			ProtocolConnectionSettings avauthenticationService = organization.AVAuthenticationService;
			if (avauthenticationService != null)
			{
				this.avAuthenticationService = new UMSipPeer(new UMSmartHost(avauthenticationService.Hostname.HostnameString), avauthenticationService.Port, true, true, IPAddressFamily.Any);
			}
			ProtocolConnectionSettings sipsessionBorderController = organization.SIPSessionBorderController;
			if (sipsessionBorderController != null)
			{
				this.sbcService = new UMSipPeer(new UMSmartHost(sipsessionBorderController.Hostname.HostnameString), sipsessionBorderController.Port, true, true, IPAddressFamily.Any);
				this.sipPeerList.Add(this.sbcService);
			}
			UMSipPeer item = new UMSipPeer(new UMSmartHost(Utils.GetOwnerHostFqdn()), base.ServiceADSettings.SipTlsListeningPort, true, true, IPAddressFamily.Any);
			this.sipPeerList.Add(item);
			UMSipPeer sipPeerFromUMCertificateSubjectName = base.GetSipPeerFromUMCertificateSubjectName();
			if (sipPeerFromUMCertificateSubjectName != null)
			{
				this.sipPeerList.Add(sipPeerFromUMCertificateSubjectName);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		internal override List<UMSipPeer> GetSecuredSipPeers()
		{
			return this.sipPeerList;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		internal override bool IsAccessProxy(string matchedFqdn)
		{
			return DatacenterSipPeerManager.IsMatchedFqdnForSipPeer(matchedFqdn, this.SIPAccessService);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		internal override bool IsAccessProxyWithOrgTestHook(string matchedFqdn, string orgParameter)
		{
			bool flag = this.IsAccessProxy(matchedFqdn);
			if (flag && Utils.RunningInTestMode && string.IsNullOrEmpty(orgParameter))
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001E9E2 File Offset: 0x0001CBE2
		internal override bool IsSIPSBC(string matchedFqdn)
		{
			return DatacenterSipPeerManager.IsMatchedFqdnForSipPeer(matchedFqdn, this.SBCService);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		protected override void LogUnknownTcpGateway(string remoteEndPoint)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRejected, null, new object[]
			{
				(CallId.Id == null) ? "<null>" : CallId.Id,
				Strings.CallFromUnknownTcpGateway(remoteEndPoint).ToString()
			});
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001EA44 File Offset: 0x0001CC44
		protected override void LogUnknownTlsGateway(X509Certificate2 certificate, string matchedFqdn, string remoteEndPoint)
		{
			string text = Strings.CallFromUnknownTlsGateway(remoteEndPoint, certificate.Thumbprint, TlsCertificateInfo.GetFQDNs(certificate).ToString(","));
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_CallRejected, null, new object[]
			{
				(CallId.Id == null) ? "<null>" : CallId.Id,
				text
			});
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001EAA6 File Offset: 0x0001CCA6
		protected override void OnLocalServerUpdated()
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		protected override UMIPGateway FindGatewayForTlsEndPoint(X509Certificate2 certificate, ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, PlatformSipUri requestUri, Guid tenantGuid, ref string matchedFqdn)
		{
			base.DebugTrace("DatacenterSipPeerManager::FindGatewayForTlsEndPoint.", new object[0]);
			UMIPGateway umipgateway = null;
			List<string> fqdns = null;
			bool isMonitoring = false;
			string orgParameter = (requestUri != null) ? requestUri.FindParameter("ms-organization") : string.Empty;
			if (this.IsAccessProxyWithOrgTestHook(matchedFqdn, orgParameter))
			{
				base.DebugTrace("Inbound call from Access Proxy", new object[0]);
				umipgateway = this.GetSipAccessServiceGateway(tenantGuid);
			}
			else if (this.IsSIPSBC(matchedFqdn))
			{
				base.DebugTrace("Inbound call from SBC", new object[0]);
				if (DatacenterSipPeerManager.CheckIfCertificateHeadersPresent(sipHeaders, out fqdns, out isMonitoring))
				{
					umipgateway = base.GetUMIPGateway(fqdns, tenantGuid, isMonitoring);
				}
				else
				{
					base.DebugTrace("Received a TLS call from SBC but no remote certificate fqdn(s) were passed in the header", new object[0]);
				}
			}
			else
			{
				base.DebugTrace("Inbound call from an unknown source", new object[0]);
			}
			if (umipgateway != null)
			{
				string text = umipgateway.Address.ToString();
				base.DebugTrace("Populating matched FQDN. Old Value={0}, New Value={1}", new object[]
				{
					matchedFqdn,
					text
				});
				matchedFqdn = text;
			}
			return umipgateway;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		private static bool CheckIfCertificateHeadersPresent(ReadOnlyCollection<PlatformSignalingHeader> sipHeaders, out List<string> certFqdns, out bool isMonitoring)
		{
			certFqdns = new List<string>();
			isMonitoring = false;
			if (sipHeaders != null)
			{
				foreach (PlatformSignalingHeader platformSignalingHeader in sipHeaders)
				{
					if (string.Equals(platformSignalingHeader.Name, "P-Certificate-Subject-Common-Name", StringComparison.InvariantCultureIgnoreCase) || string.Equals(platformSignalingHeader.Name, "P-Certificate-Subject-Alternative-Name", StringComparison.InvariantCultureIgnoreCase))
					{
						certFqdns.Add(platformSignalingHeader.Value);
						if (string.Equals(platformSignalingHeader.Value, "um.o365.exchangemon.net", StringComparison.InvariantCultureIgnoreCase))
						{
							isMonitoring = true;
						}
					}
				}
			}
			return certFqdns.Count > 0;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001EC40 File Offset: 0x0001CE40
		private static bool IsMatchedFqdnForSipPeer(string matchedFqdn, UMSipPeer peer)
		{
			return peer != null && string.Equals(peer.Address.ToString(), matchedFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001EC5C File Offset: 0x0001CE5C
		private Organization ReadForestWideSettings()
		{
			CachedOrganizationConfiguration instance = CachedOrganizationConfiguration.GetInstance(OrganizationId.ForestWideOrgId, CachedOrganizationConfiguration.ConfigurationTypes.OrganizationConfiguration);
			return instance.OrganizationConfiguration.Configuration;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001EC80 File Offset: 0x0001CE80
		private UMIPGateway GetSipAccessServiceGateway(Guid tenantGuid)
		{
			base.DebugTrace("DatacenterSipPeerManager.GetSipAccessServiceGateway tenantGuid={0}", new object[]
			{
				tenantGuid
			});
			UMIPGateway result = null;
			try
			{
				ADSessionSettings adsessionSettings = ADSessionSettings.FromExternalDirectoryOrganizationId(tenantGuid);
				result = this.SIPAccessService.ToUMIPGateway(adsessionSettings.CurrentOrganizationId);
			}
			catch (Exception ex)
			{
				base.DebugTrace("DatacenterSipPeerManager.GetSipAccessServiceGateway -  An exception occurred {0}", new object[]
				{
					ex.Message
				});
				if (!base.HandleADException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x04000836 RID: 2102
		private readonly List<UMSipPeer> sipPeerList;

		// Token: 0x04000837 RID: 2103
		private UMSipPeer avAuthenticationService;

		// Token: 0x04000838 RID: 2104
		private UMSipPeer sipAccessService;

		// Token: 0x04000839 RID: 2105
		private UMSipPeer sbcService;
	}
}
