using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002E1 RID: 737
	internal sealed class PerTenantPerimeterSettings : TenantConfigurationCacheableItem<PerimeterConfig>
	{
		// Token: 0x06002080 RID: 8320 RVA: 0x0007C4C4 File Offset: 0x0007A6C4
		public PerTenantPerimeterSettings()
		{
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0007C4CC File Offset: 0x0007A6CC
		public PerTenantPerimeterSettings(bool routeOutboundViaEhfEnabled, bool routeOutboundViaFfoFrontendEnabled, RoutingDomain partnerRoutingDomain, RoutingDomain partnerConnectorDomain) : base(true)
		{
			this.routeOutboundViaEhfEnabled = routeOutboundViaEhfEnabled;
			this.routeOutboundViaFfoFrontendEnabled = routeOutboundViaFfoFrontendEnabled;
			this.partnerRoutingDomain = partnerRoutingDomain;
			this.partnerConnectorDomain = partnerConnectorDomain;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0007C4F2 File Offset: 0x0007A6F2
		public PerTenantPerimeterSettings(PerimeterConfig perimeterConfig) : base(true)
		{
			this.SetInternalData(perimeterConfig);
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0007C502 File Offset: 0x0007A702
		public bool RouteOutboundViaEhfEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.routeOutboundViaEhfEnabled;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x0007C511 File Offset: 0x0007A711
		public bool MigrationInProgress
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.migrationInProgress;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x0007C520 File Offset: 0x0007A720
		public bool RouteOutboundViaFfoFrontendEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.routeOutboundViaFfoFrontendEnabled;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0007C52F File Offset: 0x0007A72F
		public bool EheEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.eheEnabled;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x0007C53E File Offset: 0x0007A73E
		public bool EheDecryptEnabled
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.eheDecryptEnabled;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0007C54D File Offset: 0x0007A74D
		public RoutingDomain PartnerRoutingDomain
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.partnerRoutingDomain;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0007C55C File Offset: 0x0007A75C
		public RoutingDomain PartnerConnectorDomain
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.partnerConnectorDomain;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0007C56B File Offset: 0x0007A76B
		public ADObjectId MailFlowPartnerId
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				return this.mailFlowPartnerId;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0007C57C File Offset: 0x0007A77C
		public override long ItemSize
		{
			get
			{
				base.ThrowIfNotInitialized(this);
				long num = (long)(3 + 3 * IntPtr.Size + 2 * (this.partnerRoutingDomain.ToString().Length + this.partnerConnectorDomain.ToString().Length));
				if (this.MailFlowPartnerId != null)
				{
					num += (long)(2 * this.MailFlowPartnerId.DistinguishedName.Length + 16);
				}
				return num;
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		public override void ReadData(IConfigurationSession session)
		{
			PerimeterConfig[] array = session.Find<PerimeterConfig>(null, QueryScope.SubTree, null, null, 2);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string>((long)this.GetHashCode(), "Could not find transport settings for {0}", PerTenantPerimeterSettings.GetOrgIdString(session));
				this.routeOutboundViaEhfEnabled = false;
				this.routeOutboundViaFfoFrontendEnabled = true;
				this.partnerRoutingDomain = RoutingDomain.Empty;
				this.eheEnabled = false;
				this.eheDecryptEnabled = false;
				return;
			}
			if (array.Length > 1)
			{
				ExTraceGlobals.ConfigurationTracer.TraceError<string>((long)this.GetHashCode(), "Found more than one transport settings for {0}", PerTenantPerimeterSettings.GetOrgIdString(session));
				throw new PerimeterSettingsAmbiguousException(PerTenantPerimeterSettings.GetOrgIdString(session));
			}
			this.SetInternalData(array[0]);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0007C68C File Offset: 0x0007A88C
		private static string GetOrgIdString(IConfigurationSession session)
		{
			if (!(session.SessionSettings.CurrentOrganizationId != null))
			{
				return "<First Organization>";
			}
			return session.SessionSettings.CurrentOrganizationId.ToString();
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0007C6B8 File Offset: 0x0007A8B8
		private void SetInternalData(PerimeterConfig perimeterConfig)
		{
			this.routeOutboundViaEhfEnabled = perimeterConfig.RouteOutboundViaEhfEnabled;
			this.migrationInProgress = perimeterConfig.MigrationInProgress;
			this.routeOutboundViaFfoFrontendEnabled = perimeterConfig.RouteOutboundViaFfoFrontendEnabled;
			this.eheEnabled = perimeterConfig.EheEnabled;
			this.eheDecryptEnabled = perimeterConfig.EheDecryptEnabled;
			this.mailFlowPartnerId = perimeterConfig.MailFlowPartner;
			if (perimeterConfig.PartnerRoutingDomain == null)
			{
				this.partnerRoutingDomain = RoutingDomain.Empty;
			}
			else
			{
				this.partnerRoutingDomain = new RoutingDomain(perimeterConfig.PartnerRoutingDomain.Domain);
			}
			if (perimeterConfig.PartnerConnectorDomain != null)
			{
				this.partnerConnectorDomain = new RoutingDomain(perimeterConfig.PartnerConnectorDomain.Domain);
				return;
			}
			if (perimeterConfig.PartnerRoutingDomain == null)
			{
				this.partnerConnectorDomain = RoutingDomain.Empty;
				return;
			}
			this.partnerConnectorDomain = PerTenantPerimeterSettings.defaultPartnerConnectorDomain;
		}

		// Token: 0x04001104 RID: 4356
		private const int SizeOfGuid = 16;

		// Token: 0x04001105 RID: 4357
		private static readonly RoutingDomain defaultPartnerConnectorDomain = new RoutingDomain("partner.routing");

		// Token: 0x04001106 RID: 4358
		private bool routeOutboundViaEhfEnabled;

		// Token: 0x04001107 RID: 4359
		private bool migrationInProgress;

		// Token: 0x04001108 RID: 4360
		private bool routeOutboundViaFfoFrontendEnabled;

		// Token: 0x04001109 RID: 4361
		private bool eheEnabled;

		// Token: 0x0400110A RID: 4362
		private bool eheDecryptEnabled;

		// Token: 0x0400110B RID: 4363
		private RoutingDomain partnerRoutingDomain;

		// Token: 0x0400110C RID: 4364
		private RoutingDomain partnerConnectorDomain;

		// Token: 0x0400110D RID: 4365
		private ADObjectId mailFlowPartnerId;
	}
}
