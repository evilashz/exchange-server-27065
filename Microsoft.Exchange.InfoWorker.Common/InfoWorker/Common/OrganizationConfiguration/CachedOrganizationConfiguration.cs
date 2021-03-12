using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.OrganizationConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Classification;
using Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000148 RID: 328
	internal class CachedOrganizationConfiguration
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00026BC1 File Offset: 0x00024DC1
		public OrganizationDomains Domains
		{
			get
			{
				return this.domains;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00026BC9 File Offset: 0x00024DC9
		public PerTenantSmimeSettings SmimeSettings
		{
			get
			{
				return this.smimeSettings;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00026BD1 File Offset: 0x00024DD1
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				return this.transportSettings;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00026BD9 File Offset: 0x00024DD9
		public PerTenantOrganizationConfiguration OrganizationConfiguration
		{
			get
			{
				return this.organizationConfiguration;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00026BE1 File Offset: 0x00024DE1
		public IEnumerable<ClassificationRulePackage> ClassificationDefinitions
		{
			get
			{
				if (this.classificationDefinitions == null)
				{
					return CachedOrganizationConfiguration.EmptyClassificationDefinitionsArray;
				}
				return this.classificationDefinitions.ClassificationDefinitions;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00026BFC File Offset: 0x00024DFC
		public PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules PolicyNudgeRules
		{
			get
			{
				if (this.policyNudgeRules == null)
				{
					return PerTenantPolicyNudgeRulesCollection.PolicyNudgeRules.Empty;
				}
				return this.policyNudgeRules.Rules;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00026C17 File Offset: 0x00024E17
		public IEnumerable<OutlookProtectionRule> ProtectionRules
		{
			get
			{
				if (this.protectionRules == null)
				{
					return CachedOrganizationConfiguration.EmptyOutlookProtectionRulesArray;
				}
				return this.protectionRules.ProtectionRules;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00026C32 File Offset: 0x00024E32
		public GroupsConfiguration GroupsConfiguration
		{
			get
			{
				return this.groupsConfiguration;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00026C3A File Offset: 0x00024E3A
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00026C42 File Offset: 0x00024E42
		public static CachedOrganizationConfiguration GetInstance(OrganizationId organizationId, CachedOrganizationConfiguration.ConfigurationTypes configurationTypes = CachedOrganizationConfiguration.ConfigurationTypes.All)
		{
			return CachedOrganizationConfiguration.GetInstance(organizationId, TimeSpan.Zero, configurationTypes);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00026C50 File Offset: 0x00024E50
		public static CachedOrganizationConfiguration GetInstance(OrganizationId organizationId, TimeSpan timeoutInterval, CachedOrganizationConfiguration.ConfigurationTypes configurationTypes = CachedOrganizationConfiguration.ConfigurationTypes.All)
		{
			CachedOrganizationConfiguration cachedOrganizationConfiguration;
			if (organizationId == OrganizationId.ForestWideOrgId && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				if (CachedOrganizationConfiguration.corporateInstance == null)
				{
					lock (CachedOrganizationConfiguration.corporateInstanceLock)
					{
						if (CachedOrganizationConfiguration.corporateInstance == null)
						{
							cachedOrganizationConfiguration = new CachedOrganizationConfiguration(organizationId, timeoutInterval, CachedOrganizationConfiguration.ConfigurationTypes.All);
							cachedOrganizationConfiguration.Initialize();
							CachedOrganizationConfiguration.corporateInstance = cachedOrganizationConfiguration;
						}
					}
				}
				cachedOrganizationConfiguration = CachedOrganizationConfiguration.corporateInstance;
			}
			else
			{
				cachedOrganizationConfiguration = new CachedOrganizationConfiguration(organizationId, timeoutInterval, configurationTypes);
			}
			cachedOrganizationConfiguration.Initialize();
			return cachedOrganizationConfiguration;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00026CF0 File Offset: 0x00024EF0
		public override string ToString()
		{
			return this.organizationId.ToString();
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00026D00 File Offset: 0x00024F00
		internal CachedOrganizationConfiguration(OrganizationId organizationId, TimeSpan timeoutInterval, CachedOrganizationConfiguration.ConfigurationTypes configurationTypes = CachedOrganizationConfiguration.ConfigurationTypes.All)
		{
			this.organizationId = organizationId;
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.Domains) == CachedOrganizationConfiguration.ConfigurationTypes.Domains)
			{
				this.domains = new OrganizationDomains(organizationId);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.SmimeSettings) == CachedOrganizationConfiguration.ConfigurationTypes.SmimeSettings)
			{
				this.smimeSettings = new PerTenantSmimeSettings(organizationId, timeoutInterval);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.TransportSettings) == CachedOrganizationConfiguration.ConfigurationTypes.TransportSettings)
			{
				this.transportSettings = new PerTenantTransportSettings(organizationId, timeoutInterval);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.OrganizationConfiguration) == CachedOrganizationConfiguration.ConfigurationTypes.OrganizationConfiguration)
			{
				this.organizationConfiguration = new PerTenantOrganizationConfiguration(organizationId);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.ClassificationDefinitions) == CachedOrganizationConfiguration.ConfigurationTypes.ClassificationDefinitions)
			{
				this.classificationDefinitions = new PerTenantClassificationDefinitionCollection(organizationId);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.ProtectionRules) == CachedOrganizationConfiguration.ConfigurationTypes.ProtectionRules)
			{
				this.protectionRules = new PerTenantProtectionRulesCollection(organizationId);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.PolicyNudgeRules) == CachedOrganizationConfiguration.ConfigurationTypes.PolicyNudgeRules)
			{
				this.policyNudgeRules = new PerTenantPolicyNudgeRulesCollection(organizationId);
			}
			if ((configurationTypes & CachedOrganizationConfiguration.ConfigurationTypes.GroupsConfiguration) == CachedOrganizationConfiguration.ConfigurationTypes.GroupsConfiguration)
			{
				this.groupsConfiguration = new GroupsConfiguration(organizationId);
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00026DBC File Offset: 0x00024FBC
		protected void Initialize()
		{
			if (this.domains != null)
			{
				this.domains.Initialize();
			}
			if (this.smimeSettings != null)
			{
				this.smimeSettings.Initialize();
			}
			if (this.transportSettings != null)
			{
				this.transportSettings.Initialize();
			}
			if (this.organizationConfiguration != null)
			{
				this.organizationConfiguration.Initialize();
			}
			if (this.classificationDefinitions != null)
			{
				this.classificationDefinitions.Initialize();
			}
			if (this.protectionRules != null)
			{
				this.protectionRules.Initialize();
			}
			if (this.policyNudgeRules != null)
			{
				this.policyNudgeRules.Initialize();
			}
			if (this.groupsConfiguration != null)
			{
				this.groupsConfiguration.Initialize();
			}
		}

		// Token: 0x040006EE RID: 1774
		private const string EventSource = "MSExchange OrganizationConfiguration";

		// Token: 0x040006EF RID: 1775
		internal static readonly Trace Tracer = ExTraceGlobals.OrganizationConfigurationTracer;

		// Token: 0x040006F0 RID: 1776
		internal static readonly ExEventLog Logger = new ExEventLog(CachedOrganizationConfiguration.Tracer.Category, "MSExchange OrganizationConfiguration");

		// Token: 0x040006F1 RID: 1777
		private static readonly ClassificationRulePackage[] EmptyClassificationDefinitionsArray = new ClassificationRulePackage[0];

		// Token: 0x040006F2 RID: 1778
		private static readonly OutlookProtectionRule[] EmptyOutlookProtectionRulesArray = new OutlookProtectionRule[0];

		// Token: 0x040006F3 RID: 1779
		private static CachedOrganizationConfiguration corporateInstance;

		// Token: 0x040006F4 RID: 1780
		private static object corporateInstanceLock = new object();

		// Token: 0x040006F5 RID: 1781
		private OrganizationDomains domains;

		// Token: 0x040006F6 RID: 1782
		private PerTenantSmimeSettings smimeSettings;

		// Token: 0x040006F7 RID: 1783
		private PerTenantTransportSettings transportSettings;

		// Token: 0x040006F8 RID: 1784
		private PerTenantOrganizationConfiguration organizationConfiguration;

		// Token: 0x040006F9 RID: 1785
		private PerTenantClassificationDefinitionCollection classificationDefinitions;

		// Token: 0x040006FA RID: 1786
		private PerTenantPolicyNudgeRulesCollection policyNudgeRules;

		// Token: 0x040006FB RID: 1787
		private PerTenantProtectionRulesCollection protectionRules;

		// Token: 0x040006FC RID: 1788
		private GroupsConfiguration groupsConfiguration;

		// Token: 0x040006FD RID: 1789
		private OrganizationId organizationId;

		// Token: 0x02000149 RID: 329
		[Flags]
		internal enum ConfigurationTypes
		{
			// Token: 0x040006FF RID: 1791
			Domains = 1,
			// Token: 0x04000700 RID: 1792
			TransportSettings = 2,
			// Token: 0x04000701 RID: 1793
			OrganizationConfiguration = 4,
			// Token: 0x04000702 RID: 1794
			ClassificationDefinitions = 8,
			// Token: 0x04000703 RID: 1795
			PolicyNudgeRules = 16,
			// Token: 0x04000704 RID: 1796
			ProtectionRules = 32,
			// Token: 0x04000705 RID: 1797
			GroupsConfiguration = 64,
			// Token: 0x04000706 RID: 1798
			SmimeSettings = 128,
			// Token: 0x04000707 RID: 1799
			All = 255
		}
	}
}
