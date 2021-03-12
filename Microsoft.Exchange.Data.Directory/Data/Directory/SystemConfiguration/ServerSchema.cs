using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200057B RID: 1403
	internal class ServerSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04002A7B RID: 10875
		public static readonly ADPropertyDefinition ExchangeLegacyDN = new ADPropertyDefinition("ExchangeLegacyDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A7C RID: 10876
		public static readonly ADPropertyDefinition InternalResponsibleMTA = new ADPropertyDefinition("InternalResponsibleMTA", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchResponsibleMTAServer", ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A7D RID: 10877
		public static readonly ADPropertyDefinition ResponsibleMTA = new ADPropertyDefinition("ResponsibleMTA", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.InternalResponsibleMTA
		}, null, (IPropertyBag propertyBag) => ((ADObjectId)propertyBag[ServerSchema.InternalResponsibleMTA]) ?? ((ADObjectId)propertyBag[ADObjectSchema.Id]).GetChildId("Microsoft MTA"), null, null, null);

		// Token: 0x04002A7E RID: 10878
		public static readonly ADPropertyDefinition DataPath = new ADPropertyDefinition("DataPath", ExchangeObjectVersion.Exchange2003, typeof(LocalLongFullPath), "msExchDataPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002A7F RID: 10879
		public static readonly ADPropertyDefinition InstallPath = new ADPropertyDefinition("InstallPath", ExchangeObjectVersion.Exchange2003, typeof(LocalLongFullPath), "msExchInstallPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002A80 RID: 10880
		public static readonly ADPropertyDefinition Heuristics = new ADPropertyDefinition("Heuristics", ExchangeObjectVersion.Exchange2003, typeof(int), "heuristics", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A81 RID: 10881
		public static readonly ADPropertyDefinition NetworkAddress = new ADPropertyDefinition("NetworkAddress", ExchangeObjectVersion.Exchange2003, typeof(NetworkAddress), "networkAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A82 RID: 10882
		public static readonly ADPropertyDefinition IsPhoneticSupportEnabled = new ADPropertyDefinition("IsPhoneticSupportEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchPhoneticSupport", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A83 RID: 10883
		public static readonly ADPropertyDefinition SerialNumber = new ADPropertyDefinition("SerialNumber", ExchangeObjectVersion.Exchange2003, typeof(string), "serialNumber", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A84 RID: 10884
		public static readonly ADPropertyDefinition ServerRole = new ADPropertyDefinition("ServerRole", ExchangeObjectVersion.Exchange2003, typeof(ServerRole), "serverRole", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.ServerRole.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A85 RID: 10885
		public static readonly ADPropertyDefinition ServerType = new ADPropertyDefinition("ServerType", ExchangeObjectVersion.Exchange2003, typeof(string), "type", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A86 RID: 10886
		public static readonly ADPropertyDefinition VersionNumber = new ADPropertyDefinition("VersionNumber", ExchangeObjectVersion.Exchange2003, typeof(int), "versionNumber", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A87 RID: 10887
		public static readonly ADPropertyDefinition ExchangeLegacyServerRole = new ADPropertyDefinition("ExchangeLegacyServerRole", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchServerRole", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A88 RID: 10888
		public static readonly ADPropertyDefinition CurrentServerRole = new ADPropertyDefinition("CurrentServerRole", ExchangeObjectVersion.Exchange2007, typeof(ServerRole), "msExchCurrentServerRoles", ADPropertyDefinitionFlags.PersistDefaultValue, Microsoft.Exchange.Data.Directory.SystemConfiguration.ServerRole.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A89 RID: 10889
		public static readonly ADPropertyDefinition HomeRoutingGroup = new ADPropertyDefinition("HomeRoutingGroup", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchHomeRoutingGroup", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A8A RID: 10890
		public static readonly ADPropertyDefinition CustomerFeedbackEnabled = new ADPropertyDefinition("CustomerFeedbackEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool?), "msExchCustomerFeedbackEnabled", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A8B RID: 10891
		public static readonly ADPropertyDefinition EdgeSyncCredentials = new ADPropertyDefinition("EdgeSyncCredential", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchEdgeSyncCredential", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A8C RID: 10892
		public static readonly ADPropertyDefinition EdgeSyncStatus = new ADPropertyDefinition("EdgeSyncStatus", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncStatus", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A8D RID: 10893
		public static readonly ADPropertyDefinition InternalTransportCertificate = new ADPropertyDefinition("InternalTransportCertificate", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchServerInternalTLSCert", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A8E RID: 10894
		public static readonly ADPropertyDefinition InternalTransportCertificateThumbprint = new ADPropertyDefinition("InternalTransportCertificateThumbprint", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.InternalTransportCertificate
		}, null, new GetterDelegate(Server.InternalTransportCertificateThumbprintGetter), null, null, null);

		// Token: 0x04002A8F RID: 10895
		public static readonly ADPropertyDefinition EdgeSyncLease = new ADPropertyDefinition("EdgeSyncLease", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchEdgeSyncLease", ADPropertyDefinitionFlags.Binary, null, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 1024)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A90 RID: 10896
		public static readonly ADPropertyDefinition EdgeSyncCookies = SharedPropertyDefinitions.EdgeSyncCookies;

		// Token: 0x04002A91 RID: 10897
		public static readonly ADPropertyDefinition EdgeSyncSourceGuid = new ADPropertyDefinition("EdgeSyncSourceGuid", ExchangeObjectVersion.Exchange2007, typeof(byte[]), "msExchEdgeSyncSourceGuid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A92 RID: 10898
		public static readonly ADPropertyDefinition ProductID = new ADPropertyDefinition("ProductID", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchProductID", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 100)
		}, null, null);

		// Token: 0x04002A93 RID: 10899
		public static readonly ADPropertyDefinition ComponentStates = new ADPropertyDefinition("ComponentStates", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchComponentStates", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A94 RID: 10900
		public static readonly ADPropertyDefinition MonitoringGroup = new ADPropertyDefinition("MonitoringGroup", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchShadowDisplayName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A95 RID: 10901
		public static readonly ADPropertyDefinition Edition = new ADPropertyDefinition("Edition", ExchangeObjectVersion.Exchange2003, typeof(ServerEditionType), null, ADPropertyDefinitionFlags.Calculated, ServerEditionType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.ServerType
		}, null, new GetterDelegate(Server.EditionGetter), new SetterDelegate(Server.EditionSetter), null, null);

		// Token: 0x04002A96 RID: 10902
		public static readonly ADPropertyDefinition Fqdn = new ADPropertyDefinition("Fqdn", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.NetworkAddress
		}, new CustomFilterBuilderDelegate(Server.FqdnFilterBuilder), new GetterDelegate(Server.FqdnGetter), null, null, null);

		// Token: 0x04002A97 RID: 10903
		public static readonly ADPropertyDefinition Domain = new ADPropertyDefinition("Domain", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.NetworkAddress
		}, null, new GetterDelegate(Server.DomainGetter), null, null, null);

		// Token: 0x04002A98 RID: 10904
		public static readonly ADPropertyDefinition OrganizationalUnit = new ADPropertyDefinition("OU", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.NetworkAddress,
			ADObjectSchema.RawName
		}, null, new GetterDelegate(Server.OuGetter), null, null, null);

		// Token: 0x04002A99 RID: 10905
		public static readonly ADPropertyDefinition InternetWebProxy = new ADPropertyDefinition("InternetWebProxy", ExchangeObjectVersion.Exchange2007, typeof(Uri), "msExchInternetWebProxy", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002A9A RID: 10906
		public static readonly ADPropertyDefinition IsPreE12FrontEnd = new ADPropertyDefinition("IsPreE12FrontEnd", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.ServerRole,
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsPreE12FrontEndGetter), null, null, null);

		// Token: 0x04002A9B RID: 10907
		public static readonly ADPropertyDefinition IsPreE12RPCHTTPEnabled = new ADPropertyDefinition("IsPreE12RPCHTTPEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.Heuristics,
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsPreE12RPCHTTPEnabledGetter), null, null, null);

		// Token: 0x04002A9C RID: 10908
		public static readonly ADPropertyDefinition IsExchange2003OrLater = new ADPropertyDefinition("IsExchange2003OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsExchange2003OrLaterGetter), null, null, null);

		// Token: 0x04002A9D RID: 10909
		public static readonly ADPropertyDefinition IsExchange2003Sp1OrLater = new ADPropertyDefinition("IsExchange2003Sp1OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsExchange2003Sp1OrLaterGetter), null, null, null);

		// Token: 0x04002A9E RID: 10910
		public static readonly ADPropertyDefinition IsExchange2003Sp2OrLater = new ADPropertyDefinition("IsExchange2003Sp2OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsExchange2003Sp2OrLaterGetter), null, null, null);

		// Token: 0x04002A9F RID: 10911
		public static readonly ADPropertyDefinition IsExchange2003Sp3OrLater = new ADPropertyDefinition("IsExchange2003Sp3OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsExchange2003Sp3OrLaterGetter), null, null, null);

		// Token: 0x04002AA0 RID: 10912
		public static readonly ADPropertyDefinition IsExchange2007OrLater = new ADPropertyDefinition("IsExchange2007OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsE12OrLaterGetter), null, null, null);

		// Token: 0x04002AA1 RID: 10913
		public static readonly ADPropertyDefinition IsE14OrLater = new ADPropertyDefinition("IsE14OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsE14OrLaterGetter), null, null, null);

		// Token: 0x04002AA2 RID: 10914
		public static readonly ADPropertyDefinition IsE14Sp1OrLater = new ADPropertyDefinition("IsE14Sp1OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsE14Sp1OrLaterGetter), null, null, null);

		// Token: 0x04002AA3 RID: 10915
		public static readonly ADPropertyDefinition IsE15OrLater = new ADPropertyDefinition("IsE15OrLater", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.IsE15OrLaterGetter), null, null, null);

		// Token: 0x04002AA4 RID: 10916
		public static readonly ADPropertyDefinition AdminDisplayVersion = new ADPropertyDefinition("AdminDisplayVersion", ExchangeObjectVersion.Exchange2003, typeof(ServerVersion), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.SerialNumber
		}, null, new GetterDelegate(Server.AdminDisplayVersionGetter), new SetterDelegate(Server.AdminDisplayVersionSetter), null, null);

		// Token: 0x04002AA5 RID: 10917
		public static readonly ADPropertyDefinition MajorVersion = new ADPropertyDefinition("MajorVersion", ExchangeObjectVersion.Exchange2003, typeof(int), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber
		}, null, new GetterDelegate(Server.MajorVersionGetter), null, null, null);

		// Token: 0x04002AA6 RID: 10918
		public static readonly ADPropertyDefinition IsMailboxServer = new ADPropertyDefinition("IsMailboxServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, new CustomFilterBuilderDelegate(Server.MailboxServerRoleFlagFilterBuilder), new GetterDelegate(Server.IsMailboxServerGetter), new SetterDelegate(Server.IsMailboxServerSetter), null, null);

		// Token: 0x04002AA7 RID: 10919
		public static readonly ADPropertyDefinition IsClientAccessServer = new ADPropertyDefinition("IsClientAccessServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsClientAccessServerGetter), new SetterDelegate(Server.IsClientAccessServerSetter), null, null);

		// Token: 0x04002AA8 RID: 10920
		public static readonly ADPropertyDefinition IsUnifiedMessagingServer = new ADPropertyDefinition("IsUnifiedMessagingServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsUnifiedMessagingServerGetter), new SetterDelegate(Server.IsUnifiedMessagingServerSetter), null, null);

		// Token: 0x04002AA9 RID: 10921
		public static readonly ADPropertyDefinition IsHubTransportServer = new ADPropertyDefinition("IsHubTransportServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsHubTransportServerGetter), new SetterDelegate(Server.IsHubTransportServerSetter), null, null);

		// Token: 0x04002AAA RID: 10922
		public static readonly ADPropertyDefinition IsEdgeServer = new ADPropertyDefinition("IsEdgeServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsEdgeServerGetter), new SetterDelegate(Server.IsEdgeServerSetter), null, null);

		// Token: 0x04002AAB RID: 10923
		public static readonly ADPropertyDefinition IsProvisionedServer = new ADPropertyDefinition("IsProvisionedServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsProvisionedServerGetter), new SetterDelegate(Server.IsProvisionedServerSetter), null, null);

		// Token: 0x04002AAC RID: 10924
		public static readonly ADPropertyDefinition IsCafeServer = new ADPropertyDefinition("IsCafeServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, new CustomFilterBuilderDelegate(Server.CafeServerRoleFlagFilterBuilder), new GetterDelegate(Server.IsCafeServerGetter), new SetterDelegate(Server.IsCafeServerSetter), null, null);

		// Token: 0x04002AAD RID: 10925
		public static readonly ADPropertyDefinition IsFrontendTransportServer = new ADPropertyDefinition("IsFrontendTransportServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.IsFrontendTransportServerGetter), new SetterDelegate(Server.IsFrontendTransportServerSetter), null, null);

		// Token: 0x04002AAE RID: 10926
		public static readonly ADPropertyDefinition EmptyDomainAllowed = new ADPropertyDefinition("EmptyDomainAllowed", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.CurrentServerRole
		}, null, new GetterDelegate(Server.EmptyDomainAllowedGetter), null, null, null);

		// Token: 0x04002AAF RID: 10927
		public static readonly ADPropertyDefinition IsExchangeTrialEdition = new ADPropertyDefinition("IsExchangeTrialEdition", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber,
			ServerSchema.ProductID,
			ServerSchema.ServerType
		}, null, new GetterDelegate(Server.IsExchangeTrialEditionGetter), null, null, null);

		// Token: 0x04002AB0 RID: 10928
		public static readonly ADPropertyDefinition IsExpiredExchangeTrialEdition = new ADPropertyDefinition("IsExpiredExchangeTrialEdition", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber,
			ServerSchema.ProductID,
			ServerSchema.ServerType,
			ADObjectSchema.WhenCreatedRaw
		}, null, new GetterDelegate(Server.IsExpiredExchangeTrialEditionGetter), null, null, null);

		// Token: 0x04002AB1 RID: 10929
		public static readonly ADPropertyDefinition RemainingTrialPeriod = new ADPropertyDefinition("RemainingTrialPeriod", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, EnhancedTimeSpan.Zero, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.VersionNumber,
			ServerSchema.ProductID,
			ServerSchema.ServerType,
			ADObjectSchema.WhenCreatedRaw
		}, null, new GetterDelegate(Server.RemainingTrialPeriodGetter), null, null, null);

		// Token: 0x04002AB2 RID: 10930
		public static readonly ADPropertyDefinition ElcSchedule = new ADPropertyDefinition("ElcSchedule", ExchangeObjectVersion.Exchange2007, typeof(ScheduleInterval[]), "msExchELCSchedule", ADPropertyDefinitionFlags.Binary, null, new PropertyDefinitionConstraint[]
		{
			new ElcScheduleIntervalsConstraint()
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB3 RID: 10931
		public static readonly ADPropertyDefinition DelayNotificationTimeout = new ADPropertyDefinition("DelayNotificationTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportDelayNotificationTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(4.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.FromDays(30.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB4 RID: 10932
		public static readonly ADPropertyDefinition MessageExpirationTimeout = new ADPropertyDefinition("MessageExpirationTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMessageExpirationTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(2.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(5.0), EnhancedTimeSpan.FromDays(90.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB5 RID: 10933
		public static readonly ADPropertyDefinition QueueMaxIdleTime = new ADPropertyDefinition("QueueMaxIdleTime", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxQueueIdleTime", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(3.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(5.0), EnhancedTimeSpan.OneHour),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB6 RID: 10934
		public static readonly ADPropertyDefinition MessageRetryInterval = new ADPropertyDefinition("MessageRetryInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMessageRetryInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(15.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.OneDay),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB7 RID: 10935
		public static readonly ADPropertyDefinition TransientFailureRetryInterval = new ADPropertyDefinition("TransientFailureRetryInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportTransientFailureRetryInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(5.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.FromHours(12.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB8 RID: 10936
		public static readonly ADPropertyDefinition TransientFailureRetryCount = new ADPropertyDefinition("TransientFailureRetryCount", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportTransientFailureRetryCount", ADPropertyDefinitionFlags.PersistDefaultValue, 6, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 15)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AB9 RID: 10937
		public static readonly ADPropertyDefinition MaxOutboundConnections = new ADPropertyDefinition("MaxOutboundConnections", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchSmtpMaxOutgoingConnections", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ABA RID: 10938
		public static readonly ADPropertyDefinition MaxPerDomainOutboundConnections = new ADPropertyDefinition("MaxPerDomainOutboundConnections", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchSmtpMaxOutgoingConnectionsPerDomain", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ABB RID: 10939
		public static readonly ADPropertyDefinition MaxConnectionRatePerMinute = new ADPropertyDefinition("MaxConnectionRatePerMinute", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpReceiveMaxConnectionRatePerMinute", ADPropertyDefinitionFlags.PersistDefaultValue, 1200, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ABC RID: 10940
		public static readonly ADPropertyDefinition ReceiveProtocolLogPath = new ADPropertyDefinition("ReceiveProtocolLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportReceiveProtocolLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002ABD RID: 10941
		public static readonly ADPropertyDefinition SendProtocolLogPath = new ADPropertyDefinition("SendProtocolLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportSendProtocolLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002ABE RID: 10942
		public static readonly ADPropertyDefinition OutboundConnectionFailureRetryInterval = new ADPropertyDefinition("OutboundConnectionFailureRetryInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportOutboundConnectionFailureRetryInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(10.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.FromDays(20.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ABF RID: 10943
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxAge = new ADPropertyDefinition("ReceiveProtocolLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxReceiveProtocolLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC0 RID: 10944
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxDirectorySize = new ADPropertyDefinition("ReceiveProtocolLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxReceiveProtocolLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC1 RID: 10945
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxFileSize = new ADPropertyDefinition("ReceiveProtocolLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxReceiveProtocolLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC2 RID: 10946
		public static readonly ADPropertyDefinition SendProtocolLogMaxAge = new ADPropertyDefinition("SendProtocolLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxSendProtocolLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC3 RID: 10947
		public static readonly ADPropertyDefinition SendProtocolLogMaxDirectorySize = new ADPropertyDefinition("SendProtocolLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxSendProtocolLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC4 RID: 10948
		public static readonly ADPropertyDefinition SendProtocolLogMaxFileSize = new ADPropertyDefinition("SendProtocolLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxSendProtocolLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC5 RID: 10949
		public static readonly ADPropertyDefinition InternalDNSAdapterGuid = new ADPropertyDefinition("InternalDNSAdapter", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchTransportInternalDNSAdapterGuid", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC6 RID: 10950
		public static readonly ADPropertyDefinition InternalDNSServers = new ADPropertyDefinition("InternalDNSServers", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTransportInternalDNSServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC7 RID: 10951
		public static readonly ADPropertyDefinition InternalDNSProtocolOption = new ADPropertyDefinition("InternalDNSProtocolOption", ExchangeObjectVersion.Exchange2007, typeof(ProtocolOption), "msExchTransportInternalDNSProtocolOption", ADPropertyDefinitionFlags.None, ProtocolOption.Any, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC8 RID: 10952
		public static readonly ADPropertyDefinition ExternalDNSAdapterGuid = new ADPropertyDefinition("ExternalDNSAdapterGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchTransportExternalDNSAdapterGuid", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AC9 RID: 10953
		public static readonly ADPropertyDefinition ExternalDNSServersStr = new ADPropertyDefinition("ExternalDNSServersStr", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSmtpExternalDNSServers", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ACA RID: 10954
		public static readonly ADPropertyDefinition ExternalDNSServers = new ADPropertyDefinition("ExternalDNSServers", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.ExternalDNSServersStr
		}, null, new GetterDelegate(Server.ExternalDNSServersGetter), new SetterDelegate(Server.ExternalDNSServersSetter), null, null);

		// Token: 0x04002ACB RID: 10955
		public static readonly ADPropertyDefinition ExternalDNSProtocolOption = new ADPropertyDefinition("ExternalDNSProtocolOption", ExchangeObjectVersion.Exchange2007, typeof(ProtocolOption), "msExchTransportExternalDNSProtocolOption", ADPropertyDefinitionFlags.None, ProtocolOption.Any, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ACC RID: 10956
		public static readonly ADPropertyDefinition ExternalIPAddress = new ADPropertyDefinition("ExternalIPAddress", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTransportExternalIPAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ACD RID: 10957
		public static readonly ADPropertyDefinition MaxConcurrentMailboxDeliveries = new ADPropertyDefinition("MaxConcurrentMailboxDeliveries", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxConcurrentMailboxDeliveries", ADPropertyDefinitionFlags.PersistDefaultValue, 20, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 256)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ACE RID: 10958
		public static readonly ADPropertyDefinition PoisonThreshold = new ADPropertyDefinition("PoisonThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportPoisonMessageThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 2, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 10)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ACF RID: 10959
		public static readonly ADPropertyDefinition MessageTrackingLogPath = new ADPropertyDefinition("MessageTrackingLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportMessageTrackingPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AD0 RID: 10960
		public static readonly ADPropertyDefinition MessageTrackingLogMaxAge = new ADPropertyDefinition("MessageTrackingLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxMessageTrackingLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AD1 RID: 10961
		public static readonly ADPropertyDefinition MessageTrackingLogMaxDirectorySize = new ADPropertyDefinition("MessageTrackingLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxMessageTrackingDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AD2 RID: 10962
		public static readonly ADPropertyDefinition MessageTrackingLogMaxFileSize = new ADPropertyDefinition("MessageTrackingLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportMaxMessageTrackingFileSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(4UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002AD3 RID: 10963
		public static readonly ADPropertyDefinition IrmLogPath = new ADPropertyDefinition("IrmLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchIRMLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AD4 RID: 10964
		public static readonly ADPropertyDefinition IrmLogMaxAge = new ADPropertyDefinition("IrmLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchIRMLogMaxAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AD5 RID: 10965
		public static readonly ADPropertyDefinition IrmLogMaxDirectorySize = new ADPropertyDefinition("IrmLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchIRMLogMaxDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AD6 RID: 10966
		public static readonly ADPropertyDefinition IrmLogMaxFileSize = new ADPropertyDefinition("IrmLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchIRMLogMaxFileSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(10UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002AD7 RID: 10967
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogPath = new ADPropertyDefinition("ActiveUserStatisticsLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportRecipientStatisticsPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AD8 RID: 10968
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxAge = new ADPropertyDefinition("ActiveUserStatisticsLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxRecipientStatisticsLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AD9 RID: 10969
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxDirectorySize = new ADPropertyDefinition("ActiveUserStatisticsLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportRecipientStatisticsDirectorySize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(4UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002ADA RID: 10970
		public static readonly ADPropertyDefinition ActiveUserStatisticsLogMaxFileSize = new ADPropertyDefinition("ActiveUserStatisticsLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportRecipientStatisticsFileSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(10UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002ADB RID: 10971
		public static readonly ADPropertyDefinition ServerStatisticsLogPath = new ADPropertyDefinition("ServerStatisticsLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportServerStatisticsPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002ADC RID: 10972
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxAge = new ADPropertyDefinition("ServerStatisticsLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxServerStatisticsLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002ADD RID: 10973
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxDirectorySize = new ADPropertyDefinition("ServerStatisticsLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportServerStatisticsDirectorySize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(4UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002ADE RID: 10974
		public static readonly ADPropertyDefinition ServerStatisticsLogMaxFileSize = new ADPropertyDefinition("ServerStatisticsLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportServerStatisticsFileSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(10UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromGB(4UL))
		}, null, null);

		// Token: 0x04002ADF RID: 10975
		public static readonly ADPropertyDefinition PipelineTracingPath = new ADPropertyDefinition("PipelineTracingPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportPipelineTracingPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AE0 RID: 10976
		public static readonly ADPropertyDefinition PipelineTracingSenderAddress = new ADPropertyDefinition("PipelineTracingSenderAddress", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress?), "msExchTransportPipelineTracingSenderAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 320)
		}, null, null);

		// Token: 0x04002AE1 RID: 10977
		public static readonly ADPropertyDefinition ConnectivityLogPath = new ADPropertyDefinition("ConnectivityLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportConnectivityLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AE2 RID: 10978
		public static readonly ADPropertyDefinition ConnectivityLogMaxAge = new ADPropertyDefinition("ConnectivityLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxConnectivityLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AE3 RID: 10979
		public static readonly ADPropertyDefinition ConnectivityLogMaxDirectorySize = new ADPropertyDefinition("ConnectivityLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportConnectivityLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AE4 RID: 10980
		public static readonly ADPropertyDefinition ConnectivityLogMaxFileSize = new ADPropertyDefinition("ConnectivityLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportConnectivityLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AE5 RID: 10981
		public static readonly ADPropertyDefinition PickupDirectoryPath = new ADPropertyDefinition("PickupDirectoryPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportPickupDirectoryPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AE6 RID: 10982
		public static readonly ADPropertyDefinition ReplayDirectoryPath = new ADPropertyDefinition("ReplayDirectoryPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportReplayDirectoryPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AE7 RID: 10983
		public static readonly ADPropertyDefinition PickupDirectoryMaxMessagesPerMinute = new ADPropertyDefinition("PickupDirectoryMaxMessagesPerMinute", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxPickupDirectoryMessagesPerMinute", ADPropertyDefinitionFlags.PersistDefaultValue, 100, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 20000)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AE8 RID: 10984
		public static readonly ADPropertyDefinition PickupDirectoryMaxHeaderSize = new ADPropertyDefinition("PickupDirectoryMaxHeaderSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchTransportMaxPickupDirectoryHeaderSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(64UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(32768UL), ByteQuantifiedSize.FromBytes(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AE9 RID: 10985
		public static readonly ADPropertyDefinition PickupDirectoryMaxRecipientsPerMessage = new ADPropertyDefinition("PickupDirectoryMaxRecipientsPerMessage", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportMaxPickupDirectoryRecipients", ADPropertyDefinitionFlags.PersistDefaultValue, 100, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 10000)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AEA RID: 10986
		public static readonly ADPropertyDefinition RoutingTableLogPath = new ADPropertyDefinition("RoutingTableLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportRoutingLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002AEB RID: 10987
		public static readonly ADPropertyDefinition RoutingTableLogMaxAge = new ADPropertyDefinition("RoutingTableLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportRoutingLogMaxAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(7.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AEC RID: 10988
		public static readonly ADPropertyDefinition RoutingTableLogMaxDirectorySize = new ADPropertyDefinition("RoutingTableLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportRoutingLogMaxDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AED RID: 10989
		public static readonly ADPropertyDefinition ExternalPostmasterAddress = new ADPropertyDefinition("E12TransportExternalPostmasterAddress", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress?), "msExchTransportExternalPostmasterAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AEE RID: 10990
		public static readonly ADPropertyDefinition IntraOrgConnectorProtocolLoggingLevel = new ADPropertyDefinition("TransportIntraOrgConnectorProtocolLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(ProtocolLoggingLevel), "msExchTransportOutboundProtocolLoggingLevel", ADPropertyDefinitionFlags.None, ProtocolLoggingLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AEF RID: 10991
		public static readonly ADPropertyDefinition InMemoryReceiveConnectorProtocolLoggingLevel = new ADPropertyDefinition("TransportInMemoryReceiveConnectorProtocolLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(ProtocolLoggingLevel), "msExchTransportInboundProtocolLoggingLevel", ADPropertyDefinitionFlags.None, ProtocolLoggingLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002AF0 RID: 10992
		public static readonly ADPropertyDefinition TransportServerFlags = new ADPropertyDefinition("TransportServerFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 17417, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002AF1 RID: 10993
		public static readonly ADPropertyDefinition ConnectivityLogEnabled = new ADPropertyDefinition("ConnectivityLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 8192), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 8192), null, null);

		// Token: 0x04002AF2 RID: 10994
		public static readonly ADPropertyDefinition InternalDNSAdapterDisabled = new ADPropertyDefinition("InternalDNSAdapterDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 4), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 4), null, null);

		// Token: 0x04002AF3 RID: 10995
		public static readonly ADPropertyDefinition InMemoryReceiveConnectorSmtpUtf8Enabled = new ADPropertyDefinition("TransportInMemoryReceiveConnectorSmtpUtf8Enabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 4194304), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 4194304), null, null);

		// Token: 0x04002AF4 RID: 10996
		public static readonly ADPropertyDefinition ExternalDNSAdapterDisabled = new ADPropertyDefinition("ExternalDNSAdapterDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 2), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 2), null, null);

		// Token: 0x04002AF5 RID: 10997
		public static readonly ADPropertyDefinition MessageTrackingLogEnabled = new ADPropertyDefinition("MessageTrackingLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 1), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 1), null, null);

		// Token: 0x04002AF6 RID: 10998
		public static readonly ADPropertyDefinition MessageTrackingLogSubjectLoggingEnabled = new ADPropertyDefinition("MessageTrackingLogSubjectLoggingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 16384), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 16384), null, null);

		// Token: 0x04002AF7 RID: 10999
		public static readonly ADPropertyDefinition PipelineTracingEnabled = new ADPropertyDefinition("PipelineTracingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 32768), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 32768), null, null);

		// Token: 0x04002AF8 RID: 11000
		public static readonly ADPropertyDefinition ContentConversionTracingEnabled = new ADPropertyDefinition("ContentConversionTracingEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 1048576), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 1048576), null, null);

		// Token: 0x04002AF9 RID: 11001
		public static readonly ADPropertyDefinition IrmLogEnabled = new ADPropertyDefinition("IrmLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 1024), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 1024), null, null);

		// Token: 0x04002AFA RID: 11002
		public static readonly ADPropertyDefinition GatewayEdgeSyncSubscribed = new ADPropertyDefinition("GatewayEdgeSyncSubscribed", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 65536), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 65536), null, null);

		// Token: 0x04002AFB RID: 11003
		public static readonly ADPropertyDefinition AntispamUpdatesEnabled = new ADPropertyDefinition("AntispamUpdatesEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 524288), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 524288), null, null);

		// Token: 0x04002AFC RID: 11004
		public static readonly ADPropertyDefinition PoisonMessageDetectionEnabled = new ADPropertyDefinition("PoisonMessageDetectionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 8), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 8), null, null);

		// Token: 0x04002AFD RID: 11005
		public static readonly ADPropertyDefinition RecipientValidationCacheEnabled = new ADPropertyDefinition("RecipientValidationCacheEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 2048), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 2048), null, null);

		// Token: 0x04002AFE RID: 11006
		public static readonly ADPropertyDefinition AntispamAgentsEnabled = new ADPropertyDefinition("AntispamAgentsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportServerFlags, 4096), ADObject.FlagSetterDelegate(ServerSchema.TransportServerFlags, 4096), null, null);

		// Token: 0x04002AFF RID: 11007
		public static readonly ADPropertyDefinition SubmissionServerOverrideList = new ADPropertyDefinition("SubmissionServerOverrideList", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchTransportSubmissionServerOverrideList", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B00 RID: 11008
		public static readonly ADPropertyDefinition Status = new ADPropertyDefinition("Status", ExchangeObjectVersion.Exchange2007, typeof(ServerStatus), "msExchUMServerStatus", ADPropertyDefinitionFlags.None, ServerStatus.Enabled, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ServerStatus))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B01 RID: 11009
		public static readonly ADPropertyDefinition UMStartupMode = new ADPropertyDefinition("UMStartupMode", ExchangeObjectVersion.Exchange2007, typeof(UMStartupMode), "msExchUMStartupMode", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.UMStartupMode.TCP, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(UMStartupMode))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B02 RID: 11010
		public static readonly ADPropertyDefinition ClientAccessArray = new ADPropertyDefinition("ClientAccessArray", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchServerAssociationLink", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B03 RID: 11011
		public static readonly ADPropertyDefinition DatabaseAvailabilityGroup = new ADPropertyDefinition("DatabaseAvailabilityGroup", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchMDBAvailabilityGroupLink", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B04 RID: 11012
		public static readonly ADPropertyDefinition LanguagesTemp = new ADPropertyDefinition("LanguagesTemp", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchUMAvailableLanguages", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1048576)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B05 RID: 11013
		public static readonly ADPropertyDefinition Languages = new ADPropertyDefinition("Languages", ExchangeObjectVersion.Exchange2007, typeof(UMLanguage), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.LanguagesTemp
		}, null, delegate(IPropertyBag propertyBag)
		{
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)propertyBag[ServerSchema.LanguagesTemp];
			MultiValuedProperty<UMLanguage> multiValuedProperty2 = new MultiValuedProperty<UMLanguage>();
			if (multiValuedProperty != null)
			{
				foreach (int lcid in multiValuedProperty)
				{
					try
					{
						UMLanguage item = new UMLanguage(lcid);
						multiValuedProperty2.Add(item);
					}
					catch (ArgumentException)
					{
					}
				}
			}
			return multiValuedProperty2;
		}, delegate(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<UMLanguage> multiValuedProperty = value as MultiValuedProperty<UMLanguage>;
			if (multiValuedProperty != null)
			{
				MultiValuedProperty<int> multiValuedProperty2 = new MultiValuedProperty<int>();
				foreach (UMLanguage umlanguage in multiValuedProperty)
				{
					multiValuedProperty2.Add(umlanguage.LCID);
				}
				propertyBag[ServerSchema.LanguagesTemp] = multiValuedProperty2;
				return;
			}
			propertyBag[ServerSchema.LanguagesTemp] = null;
		}, null, null);

		// Token: 0x04002B06 RID: 11014
		public static readonly ADPropertyDefinition MaxCallsAllowed = new ADPropertyDefinition("MaxCallsAllowed", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchUMMaximumCallsAllowed", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(0, 200)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B07 RID: 11015
		public static readonly ADPropertyDefinition DialPlans = new ADPropertyDefinition("DialPlans", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchUMServerDialPlanLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B08 RID: 11016
		public static readonly ADPropertyDefinition GrammarGenerationSchedule = new ADPropertyDefinition("GrammarGenerationSchedule", ExchangeObjectVersion.Exchange2007, typeof(ScheduleInterval[]), "msExchUMGrammarGenerationSchedule", ADPropertyDefinitionFlags.Binary, new ScheduleInterval[]
		{
			new ScheduleInterval(DayOfWeek.Sunday, 2, 0, DayOfWeek.Sunday, 2, 30),
			new ScheduleInterval(DayOfWeek.Monday, 2, 0, DayOfWeek.Monday, 2, 30),
			new ScheduleInterval(DayOfWeek.Tuesday, 2, 0, DayOfWeek.Tuesday, 2, 30),
			new ScheduleInterval(DayOfWeek.Wednesday, 2, 0, DayOfWeek.Wednesday, 2, 30),
			new ScheduleInterval(DayOfWeek.Thursday, 2, 0, DayOfWeek.Thursday, 2, 30),
			new ScheduleInterval(DayOfWeek.Friday, 2, 0, DayOfWeek.Friday, 2, 30),
			new ScheduleInterval(DayOfWeek.Saturday, 2, 0, DayOfWeek.Saturday, 2, 30)
		}, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B09 RID: 11017
		public static readonly ADPropertyDefinition ExternalHostFqdn = new ADPropertyDefinition("ExternalHostFqdn", ExchangeObjectVersion.Exchange2007, typeof(UMSmartHost), "msExchUMRedirectTarget", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B0A RID: 11018
		public static readonly ADPropertyDefinition UMPodRedirectTemplate = new ADPropertyDefinition("UMPodRedirectTemplate", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMSiteRedirectTarget", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B0B RID: 11019
		public static readonly ADPropertyDefinition UMForwardingAddressTemplate = new ADPropertyDefinition("UMForwardingAddressTemplate", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMForwardingAddressTemplate", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B0C RID: 11020
		public static readonly ADPropertyDefinition InternalFolderAffinityCustom = new ADPropertyDefinition("InternalFolderAffinityCustom", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderReferralOption), "msExchFolderAffinityCustom", ADPropertyDefinitionFlags.DoNotProvisionalClone, PublicFolderReferralOption.ADSite, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B0D RID: 11021
		public static readonly ADPropertyDefinition FolderAffinityCustom = new ADPropertyDefinition("FolderAffinityCustom", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.InternalFolderAffinityCustom
		}, null, (IPropertyBag propertyBag) => (PublicFolderReferralOption)propertyBag[ServerSchema.InternalFolderAffinityCustom] == PublicFolderReferralOption.CustomList, delegate(object value, IPropertyBag propertyBag)
		{
			propertyBag[ServerSchema.InternalFolderAffinityCustom] = (((bool)value) ? PublicFolderReferralOption.CustomList : PublicFolderReferralOption.ADSite);
		}, null, null);

		// Token: 0x04002B0E RID: 11022
		public static readonly ADPropertyDefinition FolderAffinityList = new ADPropertyDefinition("FolderAffinityList", ExchangeObjectVersion.Exchange2003, typeof(ServerCostPair), "msExchFolderAffinityList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B0F RID: 11023
		public static readonly ADPropertyDefinition Locale = new ADPropertyDefinition("Locale", ExchangeObjectVersion.Exchange2003, typeof(CultureInfo), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B10 RID: 11024
		public static readonly ADPropertyDefinition ErrorReportingEnabled = new ADPropertyDefinition("ErrorReportingEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B11 RID: 11025
		public static readonly ADPropertyDefinition StaticDomainControllers = new ADPropertyDefinition("StaticDomainControllers", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B12 RID: 11026
		public static readonly ADPropertyDefinition StaticGlobalCatalogs = new ADPropertyDefinition("StaticGlobalCatalogs", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B13 RID: 11027
		public static readonly ADPropertyDefinition StaticConfigDomainController = new ADPropertyDefinition("StaticConfigDomainController", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B14 RID: 11028
		public static readonly ADPropertyDefinition StaticExcludedDomainControllers = new ADPropertyDefinition("StaticExcludedDomainControllers", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B15 RID: 11029
		public static readonly ADPropertyDefinition CurrentDomainControllers = new ADPropertyDefinition("CurrentDomainControllers", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B16 RID: 11030
		public static readonly ADPropertyDefinition CurrentGlobalCatalogs = new ADPropertyDefinition("CurrentGlobalCatalogs", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B17 RID: 11031
		public static readonly ADPropertyDefinition CurrentConfigDomainController = new ADPropertyDefinition("CurrentConfigDomainController", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B18 RID: 11032
		public new static readonly ADPropertyDefinition SystemFlags = new ADPropertyDefinition("SystemFlags", ExchangeObjectVersion.Exchange2003, typeof(SystemFlagsEnum), "systemFlags", ADPropertyDefinitionFlags.PersistDefaultValue, SystemFlagsEnum.DeleteImmediately | SystemFlagsEnum.Renamable, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B19 RID: 11033
		public static readonly ADPropertyDefinition RootDropDirectoryPath = new ADPropertyDefinition("RootDropDirectoryPath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchTransportRootDropDirectoryPath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B1A RID: 11034
		public static readonly ADPropertyDefinition ServerSite = new ADPropertyDefinition("ServerSite", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchServerSite", ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B1B RID: 11035
		public static readonly ADPropertyDefinition TransportSyncServerFlags = new ADPropertyDefinition("TransportSyncServerFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002B1C RID: 11036
		public static readonly ADPropertyDefinition TransportSyncEnabled = new ADPropertyDefinition("TransportSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 1), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 1), null, null);

		// Token: 0x04002B1D RID: 11037
		public static readonly ADPropertyDefinition TransportSyncPopEnabled = new ADPropertyDefinition("TransportSyncPopEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 4), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 4), null, null);

		// Token: 0x04002B1E RID: 11038
		public static readonly ADPropertyDefinition WindowsLiveHotmailTransportSyncEnabled = new ADPropertyDefinition("WindowsLiveHotmailTransportSyncEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 8), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 8), null, null);

		// Token: 0x04002B1F RID: 11039
		public static readonly ADPropertyDefinition TransportSyncExchangeEnabled = new ADPropertyDefinition("TransportSyncExchangeEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 32), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 32), null, null);

		// Token: 0x04002B20 RID: 11040
		public static readonly ADPropertyDefinition TransportSyncImapEnabled = new ADPropertyDefinition("TransportSyncImapEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 64), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 64), null, null);

		// Token: 0x04002B21 RID: 11041
		public static readonly ADPropertyDefinition TransportSyncFacebookEnabled = new ADPropertyDefinition("TransportSyncFacebookEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 8192), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 8192), null, null);

		// Token: 0x04002B22 RID: 11042
		public static readonly ADPropertyDefinition TransportSyncDispatchEnabled = new ADPropertyDefinition("TransportSyncDispatchEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 512), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 512), null, null);

		// Token: 0x04002B23 RID: 11043
		public static readonly ADPropertyDefinition TransportSyncLinkedInEnabled = new ADPropertyDefinition("TransportSyncLinkedInEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 16384), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 16384), null, null);

		// Token: 0x04002B24 RID: 11044
		public static readonly ADPropertyDefinition MaxNumberOfTransportSyncAttempts = new ADPropertyDefinition("MaxNumberOfTransportSyncAttempts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationMaxNumberOfAttempts", ADPropertyDefinitionFlags.PersistDefaultValue, 3, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B25 RID: 11045
		public static readonly ADPropertyDefinition MaxAcceptedTransportSyncJobsPerProcessor = new ADPropertyDefinition("MaxAcceptedTransportSyncJobsPerProcessor", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationMaxAcceptedJobsPerProcessor", ADPropertyDefinitionFlags.PersistDefaultValue, 64, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B26 RID: 11046
		public static readonly ADPropertyDefinition MaxActiveTransportSyncJobsPerProcessor = new ADPropertyDefinition("MaxActiveTransportSyncJobsPerProcessor", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationMaxActiveJobsPerProcessor", ADPropertyDefinitionFlags.PersistDefaultValue, 16, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B27 RID: 11047
		public static readonly ADPropertyDefinition HttpTransportSyncProxyServer = new ADPropertyDefinition("HttpTransportSyncProxyServer", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchContentAggregationProxyServerURL", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B28 RID: 11048
		public static readonly ADPropertyDefinition HttpProtocolLogEnabled = new ADPropertyDefinition("HttpProtocolLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 128), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 128), null, null);

		// Token: 0x04002B29 RID: 11049
		public static readonly ADPropertyDefinition HttpProtocolLogFilePath = new ADPropertyDefinition("HttpProtocolLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchHTTPProtocolLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002B2A RID: 11050
		public static readonly ADPropertyDefinition HttpProtocolLogMaxAge = new ADPropertyDefinition("HttpProtocolLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchHttpProtocolLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(168.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B2B RID: 11051
		public static readonly ADPropertyDefinition HttpProtocolLogMaxDirectorySize = new ADPropertyDefinition("HttpProtocolLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchHTTPProtocolLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(256000UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B2C RID: 11052
		public static readonly ADPropertyDefinition HttpProtocolLogMaxFileSize = new ADPropertyDefinition("HttpProtocolLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchHTTPProtocolLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(10240UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B2D RID: 11053
		public static readonly ADPropertyDefinition HttpProtocolLogLoggingLevel = new ADPropertyDefinition("HttpProtocolLogLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(ProtocolLoggingLevel), "msExchHTTPProtocolLogLoggingLevel", ADPropertyDefinitionFlags.PersistDefaultValue, ProtocolLoggingLevel.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ProtocolLoggingLevel>(ProtocolLoggingLevel.None, (ProtocolLoggingLevel)2147483647)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B2E RID: 11054
		public static readonly ADPropertyDefinition TransportSyncLogEnabled = new ADPropertyDefinition("TransportSyncLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 256), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 256), null, null);

		// Token: 0x04002B2F RID: 11055
		public static readonly ADPropertyDefinition TransportSyncLogFilePath = new ADPropertyDefinition("TransportSyncLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchSyncLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002B30 RID: 11056
		public static readonly ADPropertyDefinition TransportSyncLogLoggingLevel = new ADPropertyDefinition("TransportSyncLogLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(SyncLoggingLevel), "msExchSyncLogLoggingLevel", ADPropertyDefinitionFlags.PersistDefaultValue, SyncLoggingLevel.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<SyncLoggingLevel>(SyncLoggingLevel.None, (SyncLoggingLevel)2147483647)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B31 RID: 11057
		public static readonly ADPropertyDefinition TransportSyncLogMaxAge = new ADPropertyDefinition("TransportSyncLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchSyncLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(720.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B32 RID: 11058
		public static readonly ADPropertyDefinition TransportSyncLogMaxDirectorySize = new ADPropertyDefinition("TransportSyncLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchSyncLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(10UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B33 RID: 11059
		public static readonly ADPropertyDefinition TransportSyncLogMaxFileSize = new ADPropertyDefinition("TransportSyncLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchSyncLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(10240UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B34 RID: 11060
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonDetectionEnabled = new ADPropertyDefinition("TransportSyncAccountsPoisonDetectionEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 1024), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 1024), null, null);

		// Token: 0x04002B35 RID: 11061
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonAccountThreshold = new ADPropertyDefinition("TransportSyncAccountsPoisonAccountThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncAccountsPoisonAccountThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 2, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04002B36 RID: 11062
		public static readonly ADPropertyDefinition TransportSyncAccountsPoisonItemThreshold = new ADPropertyDefinition("TransportSyncAccountsPoisonItemThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSyncAccountsPoisonItemThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 2, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04002B37 RID: 11063
		public static readonly ADPropertyDefinition TransportSyncRemoteConnectionTimeout = new ADPropertyDefinition("TransportSyncRemoteConnectionTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchContentAggregationRemoteConnectionTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMilliseconds(100000.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B38 RID: 11064
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadSizePerItem = new ADPropertyDefinition("TransportSyncMaxDownloadSizePerItem", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchContentAggregationMaxDownloadSizePerItem", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(36UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B39 RID: 11065
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadSizePerConnection = new ADPropertyDefinition("TransportSyncMaxDownloadSizePerConnection", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchContentAggregationMaxDownloadSizePerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromMB(50UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3A RID: 11066
		public static readonly ADPropertyDefinition TransportSyncMaxDownloadItemsPerConnection = new ADPropertyDefinition("TransportSyncMaxDownloadItemsPerConnection", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationMaxDownloadItemsPerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, 1000, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3B RID: 11067
		public static readonly ADPropertyDefinition DeltaSyncClientCertificateThumbprint = new ADPropertyDefinition("DeltaSyncClientCertificateThumbprint", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchDeltaSyncClientCertificateThumbprint", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3C RID: 11068
		public static readonly ADPropertyDefinition UMCertificateThumbprint = new ADPropertyDefinition("UMCertificateThumbprint", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchUMCertificateThumbprint", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3D RID: 11069
		public static readonly ADPropertyDefinition SIPAccessService = new ADPropertyDefinition("SIPAccessService", ExchangeObjectVersion.Exchange2007, typeof(ProtocolConnectionSettings), "msExchSIPAccessService", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3E RID: 11070
		public static readonly ADPropertyDefinition MaxTransportSyncDispatchers = new ADPropertyDefinition("MaxTransportSyncDispatchers", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchContentAggregationMaxDispatchers", ADPropertyDefinitionFlags.PersistDefaultValue, 5, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B3F RID: 11071
		public static readonly ADPropertyDefinition TransportSyncMailboxLogEnabled = new ADPropertyDefinition("TransportSyncMailboxLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ServerSchema.TransportSyncServerFlags
		}, null, ADObject.FlagGetterDelegate(ServerSchema.TransportSyncServerFlags, 2048), ADObject.FlagSetterDelegate(ServerSchema.TransportSyncServerFlags, 2048), null, null);

		// Token: 0x04002B40 RID: 11072
		public static readonly ADPropertyDefinition TransportSyncMailboxLogLoggingLevel = new ADPropertyDefinition("TransportSyncMailboxLogLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(SyncLoggingLevel), "msExchSyncMailboxLogLoggingLevel", ADPropertyDefinitionFlags.PersistDefaultValue, SyncLoggingLevel.None, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<SyncLoggingLevel>(SyncLoggingLevel.None, (SyncLoggingLevel)2147483647)
		}, null, null);

		// Token: 0x04002B41 RID: 11073
		public static readonly ADPropertyDefinition TransportSyncMailboxLogMaxFileSize = new ADPropertyDefinition("TransportSyncMailboxLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchSyncMailboxLogPerFileSizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(10240UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B42 RID: 11074
		public static readonly ADPropertyDefinition TransportSyncMailboxLogMaxAge = new ADPropertyDefinition("TransportSyncMailboxLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchSyncMailboxLogAgeQuotaInHours", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(720.0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04002B43 RID: 11075
		public static readonly ADPropertyDefinition TransportSyncMailboxLogMaxDirectorySize = new ADPropertyDefinition("TransportSyncMailboxLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "msExchSyncMailboxLogDirectorySizeQuota", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromGB(2UL), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B44 RID: 11076
		public static readonly ADPropertyDefinition TransportSyncMailboxLogFilePath = new ADPropertyDefinition("TransportSyncMailboxLogFilePath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchSyncMailboxLogFilePath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04002B45 RID: 11077
		public static readonly ADPropertyDefinition IntraOrgConnectorSmtpMaxMessagesPerConnection = new ADPropertyDefinition("IntraOrgConnectorSmtpMaxMessagesPerConnection", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpMaxMessagesPerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, 20, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002B46 RID: 11078
		public static readonly ADPropertyDefinition MaxActiveMailboxDatabases = new ADPropertyDefinition("MaxActiveMailboxDatabases", ExchangeObjectVersion.Exchange2007, typeof(int?), "msExchMaxActiveMailboxDatabases", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002B47 RID: 11079
		public static readonly ADPropertyDefinition MalwareFilteringFlags = new ADPropertyDefinition("MalwareFilteringFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 16, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04002B48 RID: 11080
		public static readonly ADPropertyDefinition MalwareFilteringForceRescan = ADObject.BitfieldProperty("MalwareFilteringForceRescan", 1, ServerSchema.MalwareFilteringFlags);

		// Token: 0x04002B49 RID: 11081
		public static readonly ADPropertyDefinition MalwareFilteringBypass = ADObject.BitfieldProperty("MalwareFilteringBypass", 2, ServerSchema.MalwareFilteringFlags);

		// Token: 0x04002B4A RID: 11082
		public static readonly ADPropertyDefinition MalwareFilteringScanErrorAction = ADObject.BitfieldProperty("MalwareFilteringScanErrorAction", 3, 1, ServerSchema.MalwareFilteringFlags);

		// Token: 0x04002B4B RID: 11083
		public static readonly ADPropertyDefinition MinimumSuccessfulEngineScans = ADObject.BitfieldProperty("MinimumSuccessfulEngineScans", 4, 3, ServerSchema.MalwareFilteringFlags);

		// Token: 0x04002B4C RID: 11084
		public static readonly ADPropertyDefinition MalwareFilteringDeferWaitTime = new ADPropertyDefinition("MalwareFilteringDeferWaitTime", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringDeferWaitTime", ADPropertyDefinitionFlags.PersistDefaultValue, 5, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 15)
		}, null, null);

		// Token: 0x04002B4D RID: 11085
		public static readonly ADPropertyDefinition MalwareFilteringDeferAttempts = new ADPropertyDefinition("MalwareFilteringDeferAttempts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringDeferAttempts", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 5)
		}, null, null);

		// Token: 0x04002B4E RID: 11086
		public static readonly ADPropertyDefinition MalwareFilteringUpdateFrequency = new ADPropertyDefinition("MalwareFilteringUpdateFrequency", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringUpdateFrequency", ADPropertyDefinitionFlags.PersistDefaultValue, 30, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 38880)
		}, null, null);

		// Token: 0x04002B4F RID: 11087
		public static readonly ADPropertyDefinition MalwareFilteringUpdateTimeout = new ADPropertyDefinition("MalwareFilteringUpdateTimeout", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringUpdateTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, 150, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(60, 300)
		}, null, null);

		// Token: 0x04002B50 RID: 11088
		public static readonly ADPropertyDefinition MalwareFilteringScanTimeout = new ADPropertyDefinition("MalwareFilteringScanTimeout", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchMalwareFilteringScanTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, 300, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(10, 900)
		}, null, null);

		// Token: 0x04002B51 RID: 11089
		public static readonly ADPropertyDefinition MalwareFilteringPrimaryUpdatePath = new ADPropertyDefinition("MalwareFilteringPrimaryUpdatePath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMalwareFilteringPrimaryUpdatePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B52 RID: 11090
		public static readonly ADPropertyDefinition MalwareFilteringSecondaryUpdatePath = new ADPropertyDefinition("MalwareFilteringSecondaryUpdatePath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchMalwareFilteringSecondaryUpdatePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002B53 RID: 11091
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04002B54 RID: 11092
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<ServerConfigXML>(ServerSchema.ConfigurationXMLRaw);

		// Token: 0x04002B55 RID: 11093
		public static readonly ADPropertyDefinition MaxPreferredActiveDatabases = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, int?>("MaximumPreferredActiveDatabases", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.MaximumPreferredActiveDatabases, delegate(ServerConfigXML configXml, int? value)
		{
			configXml.MaximumPreferredActiveDatabases = value;
		}, null, null);

		// Token: 0x04002B56 RID: 11094
		public static readonly ADPropertyDefinition QueueLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("QueueLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.QueueLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.QueueLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B57 RID: 11095
		public static readonly ADPropertyDefinition QueueLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("QueueLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.QueueLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.QueueLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B58 RID: 11096
		public static readonly ADPropertyDefinition QueueLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("QueueLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.QueueLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.QueueLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B59 RID: 11097
		public static readonly ADPropertyDefinition QueueLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("QueueLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.QueueLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.QueueLog.Path = value;
		}, null, null);

		// Token: 0x04002B5A RID: 11098
		public static readonly ADPropertyDefinition WlmLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("WlmLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.WlmLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.WlmLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B5B RID: 11099
		public static readonly ADPropertyDefinition WlmLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("WlmLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.WlmLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.WlmLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B5C RID: 11100
		public static readonly ADPropertyDefinition WlmLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("WlmLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.WlmLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.WlmLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B5D RID: 11101
		public static readonly ADPropertyDefinition WlmLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("WlmLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.WlmLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.WlmLog.Path = value;
		}, null, null);

		// Token: 0x04002B5E RID: 11102
		public static readonly ADPropertyDefinition AgentLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("AgentLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.AgentLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.AgentLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B5F RID: 11103
		public static readonly ADPropertyDefinition AgentLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("AgentLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.AgentLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AgentLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B60 RID: 11104
		public static readonly ADPropertyDefinition AgentLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("AgentLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.AgentLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AgentLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B61 RID: 11105
		public static readonly ADPropertyDefinition AgentLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("AgentLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.AgentLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.AgentLog.Path = value;
		}, null, null);

		// Token: 0x04002B62 RID: 11106
		public static readonly ADPropertyDefinition AgentLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("AgentLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.AgentLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.AgentLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B63 RID: 11107
		public static readonly ADPropertyDefinition FlowControlLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("FlowControlLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, EnhancedTimeSpan.FromDays(30.0), (ServerConfigXML configXml) => configXml.FlowControlLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.FlowControlLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B64 RID: 11108
		public static readonly ADPropertyDefinition FlowControlLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("FlowControlLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.FlowControlLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.FlowControlLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B65 RID: 11109
		public static readonly ADPropertyDefinition FlowControlLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("FlowControlLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.FlowControlLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.FlowControlLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B66 RID: 11110
		public static readonly ADPropertyDefinition FlowControlLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("FlowControlLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.FlowControlLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.FlowControlLog.Path = value;
		}, null, null);

		// Token: 0x04002B67 RID: 11111
		public static readonly ADPropertyDefinition FlowControlLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("FlowControlLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.FlowControlLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.FlowControlLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B68 RID: 11112
		public static readonly ADPropertyDefinition ProcessingSchedulerLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("ProcessingSchedulerLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.ProcessingSchedulerLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.ProcessingSchedulerLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B69 RID: 11113
		public static readonly ADPropertyDefinition ProcessingSchedulerLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("ProcessingSchedulerLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.ProcessingSchedulerLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ProcessingSchedulerLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B6A RID: 11114
		public static readonly ADPropertyDefinition ProcessingSchedulerLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("ProcessingSchedulerLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.ProcessingSchedulerLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ProcessingSchedulerLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B6B RID: 11115
		public static readonly ADPropertyDefinition ProcessingSchedulerLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("ProcessingSchedulerLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.ProcessingSchedulerLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.ProcessingSchedulerLog.Path = value;
		}, null, null);

		// Token: 0x04002B6C RID: 11116
		public static readonly ADPropertyDefinition ProcessingSchedulerLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("ProcessingSchedulerLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.ProcessingSchedulerLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.ProcessingSchedulerLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B6D RID: 11117
		public static readonly ADPropertyDefinition ResourceLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("ResourceLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.ResourceLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.ResourceLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B6E RID: 11118
		public static readonly ADPropertyDefinition ResourceLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("ResourceLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.ResourceLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ResourceLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B6F RID: 11119
		public static readonly ADPropertyDefinition ResourceLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("ResourceLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.ResourceLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ResourceLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B70 RID: 11120
		public static readonly ADPropertyDefinition ResourceLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("ResourceLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.ResourceLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.ResourceLog.Path = value;
		}, null, null);

		// Token: 0x04002B71 RID: 11121
		public static readonly ADPropertyDefinition ResourceLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("ResourceLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.ResourceLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.ResourceLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B72 RID: 11122
		public static readonly ADPropertyDefinition DnsLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("DnsLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, EnhancedTimeSpan.FromDays(7.0), (ServerConfigXML configXml) => configXml.DnsLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.DnsLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B73 RID: 11123
		public static readonly ADPropertyDefinition DnsLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("DnsLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(100UL), (ServerConfigXML configXml) => configXml.DnsLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.DnsLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B74 RID: 11124
		public static readonly ADPropertyDefinition DnsLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("DnsLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(10UL), (ServerConfigXML configXml) => configXml.DnsLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.DnsLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B75 RID: 11125
		public static readonly ADPropertyDefinition DnsLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("DnsLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.DnsLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.DnsLog.Path = value;
		}, null, null);

		// Token: 0x04002B76 RID: 11126
		public static readonly ADPropertyDefinition DnsLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("DnsLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, false, (ServerConfigXML configXml) => configXml.DnsLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.DnsLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B77 RID: 11127
		public static readonly ADPropertyDefinition MailboxProvisioningAttributes = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, MailboxProvisioningAttributes>("MailboxProvisioningAttributes", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.MailboxProvisioningAttributes, delegate(ServerConfigXML configXml, MailboxProvisioningAttributes value)
		{
			configXml.MailboxProvisioningAttributes = value;
		}, null, null);

		// Token: 0x04002B78 RID: 11128
		public static readonly ADPropertyDefinition JournalLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("JournalLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (ServerConfigXML configXml) => configXml.JournalLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.JournalLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B79 RID: 11129
		public static readonly ADPropertyDefinition JournalLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("JournalLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (ServerConfigXML configXml) => configXml.JournalLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.JournalLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B7A RID: 11130
		public static readonly ADPropertyDefinition JournalLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("JournalLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (ServerConfigXML configXml) => configXml.JournalLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.JournalLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B7B RID: 11131
		public static readonly ADPropertyDefinition JournalLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("JournalLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.JournalLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.JournalLog.Path = value;
		}, null, null);

		// Token: 0x04002B7C RID: 11132
		public static readonly ADPropertyDefinition JournalLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("JournalLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.JournalLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.JournalLog.Enabled = value;
		}, null, null);

		// Token: 0x04002B7D RID: 11133
		public static readonly ADPropertyDefinition TransportMaintenanceLogMaxAge = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, EnhancedTimeSpan>("MaintenanceLogMaxAge", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, EnhancedTimeSpan.FromDays(30.0), (ServerConfigXML configXml) => configXml.TransportMaintenanceLog.MaxAge, delegate(ServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.TransportMaintenanceLog.MaxAge = value;
		}, null, null);

		// Token: 0x04002B7E RID: 11134
		public static readonly ADPropertyDefinition TransportMaintenanceLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("MaintenanceLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(50UL), (ServerConfigXML configXml) => configXml.TransportMaintenanceLog.MaxDirectorySize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.TransportMaintenanceLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04002B7F RID: 11135
		public static readonly ADPropertyDefinition TransportMaintenanceLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, Unlimited<ByteQuantifiedSize>>("MaintenanceLogMaxFileSize", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(1UL), (ServerConfigXML configXml) => configXml.TransportMaintenanceLog.MaxFileSize, delegate(ServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.TransportMaintenanceLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04002B80 RID: 11136
		public static readonly ADPropertyDefinition TransportMaintenanceLogPath = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, LocalLongFullPath>("MaintenanceLogPath", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, null, (ServerConfigXML configXml) => configXml.TransportMaintenanceLog.Path, delegate(ServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.TransportMaintenanceLog.Path = value;
		}, null, null);

		// Token: 0x04002B81 RID: 11137
		public static readonly ADPropertyDefinition TransportMaintenanceLogEnabled = XMLSerializableBase.ConfigXmlProperty<ServerConfigXML, bool>("MaintenanceLogEnabled", ExchangeObjectVersion.Exchange2007, ServerSchema.ConfigurationXML, true, (ServerConfigXML configXml) => configXml.TransportMaintenanceLog.Enabled, delegate(ServerConfigXML configXml, bool value)
		{
			configXml.TransportMaintenanceLog.Enabled = value;
		}, null, null);
	}
}
