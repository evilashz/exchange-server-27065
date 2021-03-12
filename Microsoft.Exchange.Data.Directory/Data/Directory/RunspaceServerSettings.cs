using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	internal class RunspaceServerSettings : ADServerSettings, ICloneable, IEquatable<RunspaceServerSettings>
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00042370 File Offset: 0x00040570
		protected RunspaceServerSettings()
		{
			this.ViewEntireForest = false;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000423A0 File Offset: 0x000405A0
		private RunspaceServerSettings(string partitionFqdn, ADServerInfo serverInfo, string token) : this()
		{
			base.Token = token;
			this.partitionFqdn = partitionFqdn;
			if (serverInfo != null)
			{
				this.SetPreferredGlobalCatalog(partitionFqdn, serverInfo);
				this.SetConfigurationDomainController(partitionFqdn, serverInfo);
				this.AddPreferredDC(serverInfo);
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000423D0 File Offset: 0x000405D0
		private RunspaceServerSettings(string partitionFqdn, ADServerInfo gc, ADServerInfo cdc) : this()
		{
			this.partitionFqdn = partitionFqdn;
			if (gc != null)
			{
				this.SetPreferredGlobalCatalog(partitionFqdn, gc);
			}
			if (cdc != null)
			{
				this.SetConfigurationDomainController(partitionFqdn, cdc);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x000423F5 File Offset: 0x000405F5
		protected override bool EnforceIsUpdatableByADSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000423F8 File Offset: 0x000405F8
		internal static string GetTokenForOrganization(OrganizationId organization)
		{
			return organization.OrganizationalUnit.Name.ToLowerInvariant();
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0004240A File Offset: 0x0004060A
		internal static string GetTokenForUser(string userId, OrganizationId organization)
		{
			return string.Format("{0}@{1}", userId, organization.OrganizationalUnit.Name).ToLowerInvariant();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00042427 File Offset: 0x00040627
		internal static RunspaceServerSettings CreateGcOnlyRunspaceServerSettings(string token, bool forestWideAffinityRequested = false)
		{
			return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(token, TopologyProvider.LocalForestFqdn, forestWideAffinityRequested);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00042438 File Offset: 0x00040638
		internal static RunspaceServerSettings CreateGcOnlyRunspaceServerSettings(string token, string partitionFqdn, bool forestWideAffinityRequested = false)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentNullException("token");
			}
			if (string.IsNullOrEmpty(partitionFqdn))
			{
				throw new ArgumentNullException("partitionFqdn");
			}
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string>((long)instance.GetHashCode(), "GetGcOnlyRunspaceServerSettings for token {0}", token);
			bool flag;
			ADServerInfo gcFromToken = instance.GetGcFromToken(partitionFqdn, token, out flag, forestWideAffinityRequested);
			return new RunspaceServerSettings(partitionFqdn, gcFromToken, token)
			{
				ViewEntireForest = true
			};
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000424A5 File Offset: 0x000406A5
		internal static RunspaceServerSettings CreateRunspaceServerSettings(bool forestWideAffinityRequested = false)
		{
			return RunspaceServerSettings.CreateRunspaceServerSettings(TopologyProvider.LocalForestFqdn, forestWideAffinityRequested);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000424B4 File Offset: 0x000406B4
		internal static RunspaceServerSettings CreateRunspaceServerSettings(string partitionFqdn, bool forestWideAffinityRequested = false)
		{
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			bool flag;
			ADServerInfo gcFromToken = instance.GetGcFromToken(partitionFqdn, null, out flag, forestWideAffinityRequested);
			RunspaceServerSettings runspaceServerSettings = new RunspaceServerSettings(partitionFqdn, gcFromToken, instance.GetConfigDc(partitionFqdn));
			if (flag)
			{
				runspaceServerSettings.AddPreferredDC(gcFromToken);
			}
			return runspaceServerSettings;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000424F0 File Offset: 0x000406F0
		internal override Fqdn PreferredGlobalCatalog(string partitionFqdn)
		{
			Fqdn fqdn = this.userPreferredGlobalCatalog;
			if (fqdn != null && ADServerSettings.IsServerNamePartitionSameAsPartitionId(fqdn, partitionFqdn))
			{
				return fqdn;
			}
			return base.PreferredGlobalCatalog(partitionFqdn);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0004251E File Offset: 0x0004071E
		internal Fqdn GetSingleDefaultGlobalCatalog()
		{
			if (!string.IsNullOrEmpty(this.partitionFqdn))
			{
				return this.PreferredGlobalCatalog(this.partitionFqdn);
			}
			return this.PreferredGlobalCatalog(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00042545 File Offset: 0x00040745
		internal Fqdn GetSingleDefaultConfigurationDomainController()
		{
			if (!string.IsNullOrEmpty(this.partitionFqdn))
			{
				return this.ConfigurationDomainController(this.partitionFqdn);
			}
			return this.ConfigurationDomainController(TopologyProvider.LocalForestFqdn);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0004256C File Offset: 0x0004076C
		internal Fqdn UserPreferredGlobalCatalog
		{
			get
			{
				return this.userPreferredGlobalCatalog;
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00042574 File Offset: 0x00040774
		internal void SetUserPreferredGlobalCatalog(Fqdn fqdn)
		{
			ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.GlobalCatalog);
			this.userPreferredGlobalCatalog = fqdn;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0004258A File Offset: 0x0004078A
		internal void ClearUserPreferredGlobalCatalog()
		{
			this.userPreferredGlobalCatalog = null;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00042594 File Offset: 0x00040794
		internal override Fqdn ConfigurationDomainController(string partitionFqdn)
		{
			Fqdn fqdn = this.userConfigurationDomainController;
			if (fqdn != null && ADServerSettings.IsServerNamePartitionSameAsPartitionId(fqdn, partitionFqdn))
			{
				return fqdn;
			}
			return base.ConfigurationDomainController(partitionFqdn);
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x000425C2 File Offset: 0x000407C2
		internal Fqdn UserConfigurationDomainController
		{
			get
			{
				return this.userConfigurationDomainController;
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000425CA File Offset: 0x000407CA
		internal void SetUserConfigurationDomainController(Fqdn fqdn)
		{
			ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.DomainController);
			this.userConfigurationDomainController = fqdn;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000425E0 File Offset: 0x000407E0
		internal void ClearUserConfigurationDomainController()
		{
			this.userConfigurationDomainController = null;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000425E9 File Offset: 0x000407E9
		internal MultiValuedProperty<Fqdn> UserPreferredDomainControllers
		{
			get
			{
				return this.cachedUserPreferredDCList;
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000425F4 File Offset: 0x000407F4
		internal override string GetPreferredDC(ADObjectId domain)
		{
			Fqdn fqdn;
			if (this.userServerPerDomain.TryGetValue(domain, out fqdn))
			{
				return fqdn;
			}
			return base.GetPreferredDC(domain);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00042620 File Offset: 0x00040820
		internal override void AddPreferredDC(ADServerInfo serverInfo)
		{
			ADObjectId key = new ADObjectId(serverInfo.WritableNC);
			if (this.userServerPerDomain.ContainsKey(key))
			{
				return;
			}
			base.AddPreferredDC(serverInfo);
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0004264F File Offset: 0x0004084F
		internal override bool IsUpdatableByADSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00042654 File Offset: 0x00040854
		internal void AddOrReplaceUserPreferredDC(Fqdn fqdn, out ADObjectId domain, out Fqdn replacedDc)
		{
			ADServerInfo serverInfoFromFqdn = ADServerSettings.GetServerInfoFromFqdn(fqdn, ConnectionType.DomainController);
			domain = new ADObjectId(serverInfoFromFqdn.WritableNC);
			replacedDc = null;
			Fqdn fqdn2;
			if (this.userServerPerDomain.TryGetValue(domain, out fqdn2) && string.Equals(fqdn2, serverInfoFromFqdn.Fqdn))
			{
				return;
			}
			lock (this.dictLock)
			{
				Dictionary<ADObjectId, Fqdn> dictionary = new Dictionary<ADObjectId, Fqdn>(this.userServerPerDomain);
				this.userServerPerDomain.TryGetValue(domain, out replacedDc);
				dictionary[domain] = fqdn;
				this.userServerPerDomain = dictionary;
				this.cachedUserPreferredDCList = new MultiValuedProperty<Fqdn>(this.userServerPerDomain.Values);
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00042714 File Offset: 0x00040914
		internal void ClearAllUserPreferredDCs()
		{
			if (this.UserPreferredDomainControllers.Count == 0)
			{
				return;
			}
			lock (this.dictLock)
			{
				if (this.UserPreferredDomainControllers.Count > 0)
				{
					this.userServerPerDomain = new Dictionary<ADObjectId, Fqdn>();
					this.cachedUserPreferredDCList = new MultiValuedProperty<Fqdn>(this.userServerPerDomain.Values);
				}
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0004278C File Offset: 0x0004098C
		internal Dictionary<ADObjectId, Fqdn> UserServerPerDomain
		{
			get
			{
				return this.userServerPerDomain;
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00042794 File Offset: 0x00040994
		public override object Clone()
		{
			RunspaceServerSettings runspaceServerSettings = new RunspaceServerSettings();
			this.CopyTo(runspaceServerSettings);
			return runspaceServerSettings;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000427B0 File Offset: 0x000409B0
		public bool Equals(RunspaceServerSettings other)
		{
			return other != null && (((this.userPreferredGlobalCatalog != null) ? this.userPreferredGlobalCatalog.Equals(other.userPreferredGlobalCatalog) : (other.userPreferredGlobalCatalog == null)) && ((this.userConfigurationDomainController != null) ? this.userConfigurationDomainController.Equals(other.userConfigurationDomainController) : (other.userConfigurationDomainController == null)) && base.Equals(other)) && this.userServerPerDomain.Equals(other.userServerPerDomain);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0004282C File Offset: 0x00040A2C
		protected override void CopyTo(object targetObj)
		{
			ArgumentValidator.ThrowIfNull("targetObj", targetObj);
			ArgumentValidator.ThrowIfTypeInvalid<RunspaceServerSettings>("targetObj", targetObj);
			RunspaceServerSettings runspaceServerSettings = (RunspaceServerSettings)targetObj;
			base.CopyTo(targetObj);
			runspaceServerSettings.userPreferredGlobalCatalog = this.userPreferredGlobalCatalog;
			runspaceServerSettings.userConfigurationDomainController = this.userConfigurationDomainController;
			runspaceServerSettings.userServerPerDomain = new Dictionary<ADObjectId, Fqdn>(this.userServerPerDomain);
			runspaceServerSettings.cachedUserPreferredDCList = new MultiValuedProperty<Fqdn>(runspaceServerSettings.userServerPerDomain.Values);
			runspaceServerSettings.partitionFqdn = this.partitionFqdn;
		}

		// Token: 0x0400074D RID: 1869
		private string partitionFqdn;

		// Token: 0x0400074E RID: 1870
		private Fqdn userPreferredGlobalCatalog;

		// Token: 0x0400074F RID: 1871
		private Fqdn userConfigurationDomainController;

		// Token: 0x04000750 RID: 1872
		private Dictionary<ADObjectId, Fqdn> userServerPerDomain = new Dictionary<ADObjectId, Fqdn>();

		// Token: 0x04000751 RID: 1873
		private object dictLock = new object();

		// Token: 0x04000752 RID: 1874
		private MultiValuedProperty<Fqdn> cachedUserPreferredDCList = new MultiValuedProperty<Fqdn>();
	}
}
