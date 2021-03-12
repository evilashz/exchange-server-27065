using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002F7 RID: 759
	internal class TransportSettingsConfiguration
	{
		// Token: 0x0600217F RID: 8575 RVA: 0x0007F0CD File Offset: 0x0007D2CD
		private TransportSettingsConfiguration(TransportConfigContainer settings, Guid organizationGuid)
		{
			this.settings = settings;
			this.perTenantSettings = new PerTenantTransportSettings(settings);
			this.organizationGuid = organizationGuid;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007F0EF File Offset: 0x0007D2EF
		public TransportConfigContainer TransportSettings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0007F0F7 File Offset: 0x0007D2F7
		public PerTenantTransportSettings PerTenantTransportSettings
		{
			get
			{
				return this.perTenantSettings;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007F0FF File Offset: 0x0007D2FF
		public Guid OrganizationGuid
		{
			get
			{
				return this.organizationGuid;
			}
		}

		// Token: 0x0400119A RID: 4506
		private TransportConfigContainer settings;

		// Token: 0x0400119B RID: 4507
		private PerTenantTransportSettings perTenantSettings;

		// Token: 0x0400119C RID: 4508
		private Guid organizationGuid;

		// Token: 0x020002F8 RID: 760
		public class Builder : ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder>.SimpleBuilder<TransportConfigContainer>
		{
			// Token: 0x06002183 RID: 8579 RVA: 0x0007F108 File Offset: 0x0007D308
			public override void Register()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 90, "Register", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\TransportSettingsConfiguration.cs");
				base.RootId = tenantOrTopologyConfigurationSession.GetOrgContainerId();
				base.Register();
			}

			// Token: 0x06002184 RID: 8580 RVA: 0x0007F144 File Offset: 0x0007D344
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				base.RootId = session.GetOrgContainerId();
				base.LoadData(session, QueryScope.OneLevel);
			}

			// Token: 0x06002185 RID: 8581 RVA: 0x0007F15A File Offset: 0x0007D35A
			protected override TransportSettingsConfiguration BuildCache(List<TransportConfigContainer> configObjects)
			{
				if (configObjects.Count != 1)
				{
					base.FailureMessage = Strings.ReadTransportConfigConfigFailed;
					return null;
				}
				return new TransportSettingsConfiguration(configObjects[0], base.RootId.ObjectGuid);
			}
		}
	}
}
