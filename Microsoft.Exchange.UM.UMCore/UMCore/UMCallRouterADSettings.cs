using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000266 RID: 614
	internal class UMCallRouterADSettings : UMADSettings
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0005042C File Offset: 0x0004E62C
		public override ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0005042F File Offset: 0x0004E62F
		public override UMStartupMode UMStartupMode
		{
			get
			{
				return this.routerSettings.UMStartupMode;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0005043C File Offset: 0x0004E63C
		public override string UMCertificateThumbprint
		{
			get
			{
				return this.routerSettings.UMCertificateThumbprint;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00050449 File Offset: 0x0004E649
		public override string Fqdn
		{
			get
			{
				return Utils.GetLocalHostFqdn();
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x00050450 File Offset: 0x0004E650
		public override UMSmartHost ExternalServiceFqdn
		{
			get
			{
				return this.routerSettings.ExternalServiceFqdn;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x0005045D File Offset: 0x0004E65D
		public override IPAddressFamily IPAddressFamily
		{
			get
			{
				return this.routerSettings.IPAddressFamily;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x0005046A File Offset: 0x0004E66A
		public override bool IPAddressFamilyConfigurable
		{
			get
			{
				return this.routerSettings.IPAddressFamilyConfigurable;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00050477 File Offset: 0x0004E677
		public override string UMPodRedirectTemplate
		{
			get
			{
				return this.routerSettings.UMPodRedirectTemplate;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00050484 File Offset: 0x0004E684
		public override string UMForwardingAddressTemplate
		{
			get
			{
				return this.routerSettings.UMForwardingAddressTemplate;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00050491 File Offset: 0x0004E691
		public override int SipTcpListeningPort
		{
			get
			{
				return this.routerSettings.SipTcpListeningPort;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0005049E File Offset: 0x0004E69E
		public override int SipTlsListeningPort
		{
			get
			{
				return this.routerSettings.SipTlsListeningPort;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x000504AB File Offset: 0x0004E6AB
		public override ADObjectId Id
		{
			get
			{
				return this.routerSettings.Id;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x000504B8 File Offset: 0x0004E6B8
		public override bool IsSIPAccessServiceNeeded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000504F0 File Offset: 0x0004E6F0
		public UMCallRouterADSettings()
		{
			UMADSettings.ExecuteADOperation(delegate
			{
				ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
				this.routerSettings = adtopologyLookup.GetLocalCallRouterSettings();
				if (this.routerSettings == null)
				{
					throw new ExchangeServerNotFoundException(Utils.GetLocalHostName());
				}
			});
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0005051B File Offset: 0x0004E71B
		internal override void SubscribeToADNotifications(ADNotificationEvent notificationHandler)
		{
			ADNotificationsManager.Instance.CallRouterSettings.ConfigChanged += notificationHandler;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0005052D File Offset: 0x0004E72D
		internal override UMADSettings RefreshFromAD()
		{
			return new UMCallRouterADSettings();
		}

		// Token: 0x04000C00 RID: 3072
		private SIPFEServerConfiguration routerSettings;
	}
}
