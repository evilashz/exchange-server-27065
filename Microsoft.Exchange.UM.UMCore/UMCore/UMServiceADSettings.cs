using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000265 RID: 613
	internal class UMServiceADSettings : UMADSettings
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00050304 File Offset: 0x0004E504
		public override ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return this.umServer.SIPAccessService;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00050311 File Offset: 0x0004E511
		public override UMStartupMode UMStartupMode
		{
			get
			{
				return this.umServer.UMStartupMode;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0005031E File Offset: 0x0004E51E
		public override string UMCertificateThumbprint
		{
			get
			{
				return this.umServer.UMCertificateThumbprint;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0005032B File Offset: 0x0004E52B
		public override string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00050333 File Offset: 0x0004E533
		public override UMSmartHost ExternalServiceFqdn
		{
			get
			{
				return this.umServer.ExternalServiceFqdn;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00050340 File Offset: 0x0004E540
		public override IPAddressFamily IPAddressFamily
		{
			get
			{
				return this.umServer.IPAddressFamily;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0005034D File Offset: 0x0004E54D
		public override bool IPAddressFamilyConfigurable
		{
			get
			{
				return this.umServer.IPAddressFamilyConfigurable;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0005035A File Offset: 0x0004E55A
		public override string UMPodRedirectTemplate
		{
			get
			{
				return this.umServer.UMPodRedirectTemplate;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00050367 File Offset: 0x0004E567
		public override string UMForwardingAddressTemplate
		{
			get
			{
				return this.umServer.UMForwardingAddressTemplate;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00050374 File Offset: 0x0004E574
		public override int SipTcpListeningPort
		{
			get
			{
				return this.umServer.SipTcpListeningPort;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00050381 File Offset: 0x0004E581
		public override int SipTlsListeningPort
		{
			get
			{
				return this.umServer.SipTlsListeningPort;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0005038E File Offset: 0x0004E58E
		public override ADObjectId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00050396 File Offset: 0x0004E596
		public override bool IsSIPAccessServiceNeeded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000503E8 File Offset: 0x0004E5E8
		public UMServiceADSettings()
		{
			UMADSettings.ExecuteADOperation(delegate
			{
				ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
				Server localServer = adtopologyLookup.GetLocalServer();
				if (localServer == null)
				{
					throw new ExchangeServerNotFoundException(Utils.GetLocalHostName());
				}
				this.umServer = new UMServer(localServer);
				this.fqdn = localServer.Fqdn;
				this.id = localServer.Id;
			});
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00050413 File Offset: 0x0004E613
		internal override void SubscribeToADNotifications(ADNotificationEvent notificationHandler)
		{
			ADNotificationsManager.Instance.Server.ConfigChanged += notificationHandler;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00050425 File Offset: 0x0004E625
		internal override UMADSettings RefreshFromAD()
		{
			return new UMServiceADSettings();
		}

		// Token: 0x04000BFD RID: 3069
		private string fqdn;

		// Token: 0x04000BFE RID: 3070
		private UMServer umServer;

		// Token: 0x04000BFF RID: 3071
		private ADObjectId id;
	}
}
