using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000706 RID: 1798
	[Serializable]
	public class ExchangeServer : ADPresentationObject
	{
		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06005491 RID: 21649 RVA: 0x001321FF File Offset: 0x001303FF
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ExchangeServer.schema;
			}
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x00132206 File Offset: 0x00130406
		public ExchangeServer()
		{
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0013220E File Offset: 0x0013040E
		public ExchangeServer(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06005494 RID: 21652 RVA: 0x00132217 File Offset: 0x00130417
		public new string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x00132229 File Offset: 0x00130429
		public LocalLongFullPath DataPath
		{
			get
			{
				return (LocalLongFullPath)this[ExchangeServerSchema.DataPath];
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06005496 RID: 21654 RVA: 0x0013223B File Offset: 0x0013043B
		public string Domain
		{
			get
			{
				return (string)this[ExchangeServerSchema.Domain];
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x0013224D File Offset: 0x0013044D
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[ExchangeServerSchema.Edition];
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x06005498 RID: 21656 RVA: 0x0013225F File Offset: 0x0013045F
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[ExchangeServerSchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06005499 RID: 21657 RVA: 0x00132271 File Offset: 0x00130471
		public int ExchangeLegacyServerRole
		{
			get
			{
				return (int)this[ExchangeServerSchema.ExchangeLegacyServerRole];
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x00132283 File Offset: 0x00130483
		public string Fqdn
		{
			get
			{
				return (string)this[ExchangeServerSchema.Fqdn];
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x0600549B RID: 21659 RVA: 0x00132295 File Offset: 0x00130495
		// (set) Token: 0x0600549C RID: 21660 RVA: 0x001322A7 File Offset: 0x001304A7
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)this[ExchangeServerSchema.CustomerFeedbackEnabled];
			}
			set
			{
				this[ExchangeServerSchema.CustomerFeedbackEnabled] = value;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x0600549D RID: 21661 RVA: 0x001322BA File Offset: 0x001304BA
		// (set) Token: 0x0600549E RID: 21662 RVA: 0x001322CC File Offset: 0x001304CC
		[Parameter(Mandatory = false)]
		public Uri InternetWebProxy
		{
			get
			{
				return (Uri)this[ExchangeServerSchema.InternetWebProxy];
			}
			set
			{
				this[ExchangeServerSchema.InternetWebProxy] = value;
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x0600549F RID: 21663 RVA: 0x001322DA File Offset: 0x001304DA
		public bool IsHubTransportServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsHubTransportServer];
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x060054A0 RID: 21664 RVA: 0x001322EC File Offset: 0x001304EC
		public bool IsClientAccessServer
		{
			get
			{
				if (!this.IsE15OrLater)
				{
					return (bool)this[ExchangeServerSchema.IsClientAccessServer];
				}
				return (bool)this[ExchangeServerSchema.IsCafeServer];
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x00132317 File Offset: 0x00130517
		public bool IsExchange2007OrLater
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsExchange2007OrLater];
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x060054A2 RID: 21666 RVA: 0x00132329 File Offset: 0x00130529
		public bool IsEdgeServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsEdgeServer];
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x0013233B File Offset: 0x0013053B
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsMailboxServer];
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x0013234D File Offset: 0x0013054D
		public bool IsE14OrLater
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsE14OrLater];
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x0013235F File Offset: 0x0013055F
		public bool IsE15OrLater
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsE15OrLater];
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x00132371 File Offset: 0x00130571
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsProvisionedServer];
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00132383 File Offset: 0x00130583
		public bool IsUnifiedMessagingServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsUnifiedMessagingServer];
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x00132395 File Offset: 0x00130595
		internal bool IsCafeServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsCafeServer];
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x001323A7 File Offset: 0x001305A7
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsFrontendTransportServer];
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x001323B9 File Offset: 0x001305B9
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[ExchangeServerSchema.NetworkAddress];
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x001323CB File Offset: 0x001305CB
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[ExchangeServerSchema.OrganizationalUnit];
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x060054AC RID: 21676 RVA: 0x001323DD File Offset: 0x001305DD
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[ExchangeServerSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x060054AD RID: 21677 RVA: 0x001323EF File Offset: 0x001305EF
		public ADObjectId Site
		{
			get
			{
				return (ADObjectId)this[ExchangeServerSchema.Site];
			}
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x00132404 File Offset: 0x00130604
		public ServerRole ServerRole
		{
			get
			{
				ServerRole serverRole = (ServerRole)this[ExchangeServerSchema.CurrentServerRole];
				if (!this.IsE15OrLater)
				{
					return serverRole;
				}
				return ExchangeServer.ConvertE15ServerRoleToOutput(serverRole);
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x00132432 File Offset: 0x00130632
		// (set) Token: 0x060054B0 RID: 21680 RVA: 0x00132444 File Offset: 0x00130644
		public bool? ErrorReportingEnabled
		{
			get
			{
				return (bool?)this[ServerSchema.ErrorReportingEnabled];
			}
			set
			{
				this[ServerSchema.ErrorReportingEnabled] = value;
			}
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x060054B1 RID: 21681 RVA: 0x00132457 File Offset: 0x00130657
		// (set) Token: 0x060054B2 RID: 21682 RVA: 0x00132469 File Offset: 0x00130669
		[Parameter]
		public MultiValuedProperty<string> StaticDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticDomainControllers];
			}
			set
			{
				this[ServerSchema.StaticDomainControllers] = value;
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x060054B3 RID: 21683 RVA: 0x00132477 File Offset: 0x00130677
		// (set) Token: 0x060054B4 RID: 21684 RVA: 0x00132489 File Offset: 0x00130689
		[Parameter]
		public MultiValuedProperty<string> StaticGlobalCatalogs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticGlobalCatalogs];
			}
			set
			{
				this[ServerSchema.StaticGlobalCatalogs] = value;
			}
		}

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x00132497 File Offset: 0x00130697
		// (set) Token: 0x060054B6 RID: 21686 RVA: 0x001324A9 File Offset: 0x001306A9
		[Parameter]
		public string StaticConfigDomainController
		{
			get
			{
				return (string)this[ServerSchema.StaticConfigDomainController];
			}
			set
			{
				this[ServerSchema.StaticConfigDomainController] = value;
			}
		}

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x060054B7 RID: 21687 RVA: 0x001324B7 File Offset: 0x001306B7
		// (set) Token: 0x060054B8 RID: 21688 RVA: 0x001324C9 File Offset: 0x001306C9
		[Parameter]
		public MultiValuedProperty<string> StaticExcludedDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticExcludedDomainControllers];
			}
			set
			{
				this[ServerSchema.StaticExcludedDomainControllers] = value;
			}
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x001324D7 File Offset: 0x001306D7
		// (set) Token: 0x060054BA RID: 21690 RVA: 0x001324E9 File Offset: 0x001306E9
		[Parameter]
		public string MonitoringGroup
		{
			get
			{
				return (string)this[ExchangeServerSchema.MonitoringGroup];
			}
			set
			{
				this[ExchangeServerSchema.MonitoringGroup] = value;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x060054BB RID: 21691 RVA: 0x001324F7 File Offset: 0x001306F7
		// (set) Token: 0x060054BC RID: 21692 RVA: 0x00132509 File Offset: 0x00130709
		public MultiValuedProperty<string> CurrentDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.CurrentDomainControllers];
			}
			private set
			{
				this[ServerSchema.CurrentDomainControllers] = value;
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x00132517 File Offset: 0x00130717
		// (set) Token: 0x060054BE RID: 21694 RVA: 0x00132529 File Offset: 0x00130729
		public MultiValuedProperty<string> CurrentGlobalCatalogs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.CurrentGlobalCatalogs];
			}
			private set
			{
				this[ServerSchema.CurrentGlobalCatalogs] = value;
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x00132537 File Offset: 0x00130737
		// (set) Token: 0x060054C0 RID: 21696 RVA: 0x00132549 File Offset: 0x00130749
		public string CurrentConfigDomainController
		{
			get
			{
				return (string)this[ServerSchema.CurrentConfigDomainController];
			}
			private set
			{
				this[ServerSchema.CurrentConfigDomainController] = value;
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x00132557 File Offset: 0x00130757
		public string ProductID
		{
			get
			{
				return (string)this[ExchangeServerSchema.ProductID];
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x060054C2 RID: 21698 RVA: 0x00132569 File Offset: 0x00130769
		public bool IsExchangeTrialEdition
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsExchangeTrialEdition];
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x0013257B File Offset: 0x0013077B
		public bool IsExpiredExchangeTrialEdition
		{
			get
			{
				return (bool)this[ExchangeServerSchema.IsExpiredExchangeTrialEdition];
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x0013258D File Offset: 0x0013078D
		public MailboxProvisioningAttributes MailboxProvisioningAttributes
		{
			get
			{
				return this[ServerSchema.MailboxProvisioningAttributes] as MailboxProvisioningAttributes;
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x0013259F File Offset: 0x0013079F
		public EnhancedTimeSpan RemainingTrialPeriod
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.RemainingTrialPeriod];
			}
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x001325B4 File Offset: 0x001307B4
		internal static ServerRole ConvertE15ServerRoleToOutput(ServerRole serverRole)
		{
			if (serverRole == ServerRole.Edge)
			{
				return serverRole;
			}
			bool flag = (serverRole & ServerRole.Cafe) != ServerRole.None;
			bool flag2 = (serverRole & ServerRole.Mailbox) != ServerRole.None;
			ServerRole serverRole2 = ServerRole.Mailbox;
			if (Globals.IsDatacenter)
			{
				if (!flag)
				{
					serverRole2 |= ServerRole.FrontendTransport;
				}
				if (!flag2)
				{
					serverRole2 |= ServerRole.HubTransport;
				}
			}
			serverRole &= serverRole2;
			if (flag)
			{
				serverRole |= ServerRole.ClientAccess;
			}
			return serverRole;
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x00132608 File Offset: 0x00130808
		internal void RefreshDsAccessData()
		{
			if (this.IsEdgeServer)
			{
				this.CurrentDomainControllers = new MultiValuedProperty<string>(new string[]
				{
					this.Fqdn
				});
				this.CurrentGlobalCatalogs = new MultiValuedProperty<string>(new string[]
				{
					this.Fqdn
				});
				this.CurrentConfigDomainController = this.Fqdn;
				return;
			}
			using (ServiceTopologyProvider serviceTopologyProvider = new ServiceTopologyProvider())
			{
				string partitionFqdn = (this.m_Session != null && this.m_Session.SessionSettings.PartitionId != null) ? this.m_Session.SessionSettings.PartitionId.ForestFQDN : TopologyProvider.LocalForestFqdn;
				this.CurrentDomainControllers = new MultiValuedProperty<string>(serviceTopologyProvider.GetCurrentDCs(partitionFqdn));
				this.CurrentGlobalCatalogs = new MultiValuedProperty<string>(serviceTopologyProvider.GetCurrentGCs(partitionFqdn));
				this.CurrentConfigDomainController = (serviceTopologyProvider.GetConfigDC(partitionFqdn, true) ?? string.Empty);
			}
		}

		// Token: 0x040038E7 RID: 14567
		private static ExchangeServerSchema schema = ObjectSchema.GetInstance<ExchangeServerSchema>();
	}
}
