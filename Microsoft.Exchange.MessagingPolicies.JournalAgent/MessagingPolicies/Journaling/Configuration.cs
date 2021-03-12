using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000022 RID: 34
	internal sealed class Configuration
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000B4B6 File Offset: 0x000096B6
		public static Configuration GetConfig(OrganizationId organizationId)
		{
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<OrganizationId>(0L, "Getting config for: {0}", organizationId);
				return Configuration.GetTenantConfig(organizationId);
			}
			ExTraceGlobals.JournalingTracer.TraceDebug(0L, "Getting GetEnterpriseConfig config");
			return Configuration.GetEnterpriseConfig();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000B4F4 File Offset: 0x000096F4
		internal static Configuration GetEnterpriseConfig()
		{
			TransportConfigContainer transportConfigObject = Configuration.TransportConfigObject;
			if (transportConfigObject == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "Transport Settings could not be loaded");
				return null;
			}
			if (!transportConfigObject.JournalingReportNdrTo.IsValidAddress)
			{
				ExTraceGlobals.JournalingTracer.TraceError<SmtpAddress>(0L, "Invalid JournalingReportNdrTo: {0}", transportConfigObject.JournalingReportNdrTo);
				return null;
			}
			MicrosoftExchangeRecipientConfiguration microsoftExchangeRecipientConfiguration = GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance;
			if (microsoftExchangeRecipientConfiguration == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "MER could not be loaded");
				return null;
			}
			JournalingRules config = JournalingRules.GetConfig(OrganizationId.ForestWideOrgId);
			if (config == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "Rules could not be loaded");
				return null;
			}
			ReconcileConfig reconcileConfig = ReconcileConfig.GetInstance(OrganizationId.ForestWideOrgId);
			if (reconcileConfig == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError(0L, "Reconciliation config could not be loaded");
				return null;
			}
			Configuration configuration = Configuration.instance;
			if (configuration == null || configuration.transportSettingsConfiguration != transportConfigObject || configuration.merConfiguration != microsoftExchangeRecipientConfiguration || configuration.rules != config || configuration.reconcileConfiguration != reconcileConfig)
			{
				lock (Configuration.lockObject)
				{
					configuration = Configuration.instance;
					if (configuration == null || configuration.transportSettingsConfiguration != transportConfigObject || configuration.merConfiguration != microsoftExchangeRecipientConfiguration || configuration.rules != config || configuration.reconcileConfiguration != reconcileConfig)
					{
						Configuration.instance = new Configuration(transportConfigObject, microsoftExchangeRecipientConfiguration, config, reconcileConfig, null);
					}
				}
			}
			return Configuration.instance;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000B650 File Offset: 0x00009850
		internal static Configuration GetTenantConfig(OrganizationId organizationId)
		{
			PerTenantTransportSettings settings;
			if (!Components.Configuration.TryGetTransportSettings(organizationId, out settings))
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "Transport Settings could not be loaded for tenant: {0}", organizationId);
				return null;
			}
			MicrosoftExchangeRecipientPerTenantSettings merConfig;
			if (!Components.Configuration.TryGetMicrosoftExchangeRecipient(organizationId, out merConfig))
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "MER config could not be loaded tenant: {0}", organizationId);
				return null;
			}
			JournalingRules config = JournalingRules.GetConfig(organizationId);
			if (config == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "Rules config could not be loaded for tenant: {0}", organizationId);
				return null;
			}
			List<GccRuleEntry> gccConfig = JournalingRules.GetGccConfig();
			if (gccConfig == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "GCC Rules config could not be loaded while processing tenant: {0}", organizationId);
				JournalingGlobals.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingRulesConfigurationLoadError, null, new object[0]);
			}
			ReconcileConfig reconcileConfig = ReconcileConfig.GetInstance(organizationId);
			if (reconcileConfig == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<OrganizationId>(0L, "Reconciliation config was corrupt for tenant: {0}", organizationId);
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<OrganizationId>(0L, "Loaded tenant config: {0}", organizationId);
			return new Configuration(organizationId, settings, merConfig, config, reconcileConfig, gccConfig);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000B731 File Offset: 0x00009931
		internal OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000B73C File Offset: 0x0000993C
		internal RoutingAddress JournalReportNdrTo
		{
			get
			{
				if (null != this.organizationId && this.organizationId != OrganizationId.ForestWideOrgId && this.journalingReportNdrToRoutingAddress.Equals(RoutingAddress.NullReversePath))
				{
					return this.MSExchangeRecipient;
				}
				return this.journalingReportNdrToRoutingAddress;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000B788 File Offset: 0x00009988
		internal RoutingAddress JournalReportNdrToForGcc
		{
			get
			{
				return this.journalingReportNdrToRoutingAddressForGcc;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000B790 File Offset: 0x00009990
		internal RoutingAddress MSExchangeRecipient
		{
			get
			{
				return this.merRoutingAddress;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000B798 File Offset: 0x00009998
		internal bool SkipUMVoicemailMessages
		{
			get
			{
				if (this.organizationId == OrganizationId.ForestWideOrgId)
				{
					return !this.transportSettingsConfiguration.VoicemailJournalingEnabled;
				}
				return !this.perTenantTransportSettings.VoicemailJournalingEnabled;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000B7C9 File Offset: 0x000099C9
		internal bool LegacyJournalingMigrationEnabled
		{
			get
			{
				if (this.organizationId == OrganizationId.ForestWideOrgId)
				{
					return this.transportSettingsConfiguration.LegacyJournalingMigrationEnabled;
				}
				return this.perTenantTransportSettings.LegacyJournalingMigrationEnabled;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000B7F4 File Offset: 0x000099F4
		internal bool LegacyArchiveJournalingEnabled
		{
			get
			{
				return !(this.organizationId == OrganizationId.ForestWideOrgId) && this.perTenantTransportSettings.LegacyArchiveJournalingEnabled;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000B815 File Offset: 0x00009A15
		internal bool JournalReportDLMemberSubstitutionEnabled
		{
			get
			{
				return this.organizationId == OrganizationId.ForestWideOrgId && this.transportSettingsConfiguration.JournalReportDLMemberSubstitutionEnabled;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000B836 File Offset: 0x00009A36
		internal bool LegacyArchiveLiveJournalingEnabled
		{
			get
			{
				return !(this.organizationId == OrganizationId.ForestWideOrgId) && this.perTenantTransportSettings.LegacyArchiveLiveJournalingEnabled;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000B857 File Offset: 0x00009A57
		internal JournalingRules Rules
		{
			get
			{
				return this.rules;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000B85F File Offset: 0x00009A5F
		internal List<GccRuleEntry> GCCRules
		{
			get
			{
				return this.gccRules;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000B867 File Offset: 0x00009A67
		internal ReconcileConfig ReconcileConfig
		{
			get
			{
				return this.reconcileConfiguration;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B870 File Offset: 0x00009A70
		private Configuration(TransportConfigContainer settings, MicrosoftExchangeRecipientConfiguration merConfig, JournalingRules rules, ReconcileConfig reconcileConfig, List<GccRuleEntry> gccRules)
		{
			this.organizationId = OrganizationId.ForestWideOrgId;
			this.transportSettingsConfiguration = settings;
			this.merConfiguration = merConfig;
			this.rules = rules;
			this.reconcileConfiguration = reconcileConfig;
			this.gccRules = gccRules;
			this.merRoutingAddress = RoutingAddress.Parse(merConfig.Address.ToString());
			this.journalingReportNdrToRoutingAddress = RoutingAddress.Parse(settings.JournalingReportNdrTo.ToString());
			this.journalingReportNdrToRoutingAddressForGcc = this.journalingReportNdrToRoutingAddress;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000B900 File Offset: 0x00009B00
		private Configuration(OrganizationId organizationId, PerTenantTransportSettings settings, MicrosoftExchangeRecipientPerTenantSettings merConfig, JournalingRules rules, ReconcileConfig reconcileConfig, List<GccRuleEntry> gccRules)
		{
			this.organizationId = organizationId;
			this.perTenantTransportSettings = settings;
			this.rules = rules;
			this.reconcileConfiguration = reconcileConfig;
			this.gccRules = gccRules;
			this.merRoutingAddress = RoutingAddress.Parse(merConfig.PrimarySmtpAddress.ToString());
			this.journalingReportNdrToRoutingAddress = RoutingAddress.Parse(settings.JournalingReportNdrTo.ToString());
			this.journalingReportNdrToRoutingAddressForGcc = RoutingAddress.NullReversePath;
			TransportConfigContainer transportConfigObject = Configuration.TransportConfigObject;
			if (transportConfigObject != null && transportConfigObject.JournalingReportNdrTo.IsValidAddress && transportConfigObject.JournalingReportNdrTo != SmtpAddress.NullReversePath)
			{
				this.journalingReportNdrToRoutingAddressForGcc = RoutingAddress.Parse(transportConfigObject.JournalingReportNdrTo.ToString());
				return;
			}
			MicrosoftExchangeRecipientConfiguration microsoftExchangeRecipientConfiguration = GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance;
			if (microsoftExchangeRecipientConfiguration != null)
			{
				this.journalingReportNdrToRoutingAddressForGcc = RoutingAddress.Parse(microsoftExchangeRecipientConfiguration.Address.ToString());
			}
		}

		// Token: 0x040000BD RID: 189
		private static object lockObject = new object();

		// Token: 0x040000BE RID: 190
		private static Configuration instance = null;

		// Token: 0x040000BF RID: 191
		private OrganizationId organizationId;

		// Token: 0x040000C0 RID: 192
		private TransportConfigContainer transportSettingsConfiguration;

		// Token: 0x040000C1 RID: 193
		private PerTenantTransportSettings perTenantTransportSettings;

		// Token: 0x040000C2 RID: 194
		private ReconcileConfig reconcileConfiguration;

		// Token: 0x040000C3 RID: 195
		private MicrosoftExchangeRecipientConfiguration merConfiguration;

		// Token: 0x040000C4 RID: 196
		private JournalingRules rules;

		// Token: 0x040000C5 RID: 197
		private List<GccRuleEntry> gccRules;

		// Token: 0x040000C6 RID: 198
		private RoutingAddress journalingReportNdrToRoutingAddress;

		// Token: 0x040000C7 RID: 199
		private RoutingAddress journalingReportNdrToRoutingAddressForGcc;

		// Token: 0x040000C8 RID: 200
		private RoutingAddress merRoutingAddress;
	}
}
