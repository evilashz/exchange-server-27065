using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000713 RID: 1811
	internal class FrontendTransportServerADSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x06005515 RID: 21781 RVA: 0x001335E4 File Offset: 0x001317E4
		private static object AdminDisplayVersionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[FrontendTransportServerADSchema.SerialNumber];
			if (string.IsNullOrEmpty(text))
			{
				InvalidOperationException ex = new InvalidOperationException(DirectoryStrings.SerialNumberMissing);
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdminDisplayVersion", ex.Message), FrontendTransportServerADSchema.AdminDisplayVersion, string.Empty), ex);
			}
			object result;
			try
			{
				result = ServerVersion.ParseFromSerialNumber(text);
			}
			catch (FormatException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdminDisplayVersion", ex2.Message), FrontendTransportServerADSchema.AdminDisplayVersion, propertyBag[FrontendTransportServerADSchema.SerialNumber]), ex2);
			}
			return result;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x00133688 File Offset: 0x00131888
		private static void AdminDisplayVersionSetter(object value, IPropertyBag propertyBag)
		{
			ServerVersion serverVersion = (ServerVersion)value;
			propertyBag[FrontendTransportServerADSchema.SerialNumber] = serverVersion.ToString(true);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001336B0 File Offset: 0x001318B0
		private static object EditionGetter(IPropertyBag propertyBag)
		{
			string serverTypeInAD = (string)propertyBag[FrontendTransportServerADSchema.ServerType];
			return ServerEdition.DecryptServerEdition(serverTypeInAD);
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x001336DC File Offset: 0x001318DC
		private static void EditionSetter(object value, IPropertyBag propertyBag)
		{
			ServerEditionType edition = (ServerEditionType)value;
			propertyBag[FrontendTransportServerADSchema.ServerType] = ServerEdition.EncryptServerEdition(edition);
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00133704 File Offset: 0x00131904
		private static object ExternalDNSServersGetter(IPropertyBag propertyBag)
		{
			List<IPAddress> list = Server.ParseStringForAddresses((string)propertyBag[FrontendTransportServerADSchema.ExternalDNSServersStr]);
			if (list.Count > 0)
			{
				return new MultiValuedProperty<IPAddress>(false, null, list);
			}
			return new MultiValuedProperty<IPAddress>();
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0013373E File Offset: 0x0013193E
		private static void ExternalDNSServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[FrontendTransportServerADSchema.ExternalDNSServersStr] = Server.FormatAddressesToString((MultiValuedProperty<IPAddress>)value);
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x00133758 File Offset: 0x00131958
		private static object IsFrontendTransportServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole];
			return (serverRole & ServerRole.FrontendTransport) == ServerRole.FrontendTransport;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0013378C File Offset: 0x0013198C
		private static void IsFrontendTransportServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[FrontendTransportServerADSchema.CurrentServerRole] = ((ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[FrontendTransportServerADSchema.CurrentServerRole] = ((ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole] | ServerRole.FrontendTransport);
				return;
			}
			propertyBag[FrontendTransportServerADSchema.CurrentServerRole] = ((ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole] & ~ServerRole.FrontendTransport);
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00133814 File Offset: 0x00131A14
		private static object IsProvisionedServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole];
			return (serverRole & ServerRole.ProvisionedServer) == ServerRole.ProvisionedServer;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00133848 File Offset: 0x00131A48
		private static void IsProvisionedServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[FrontendTransportServerADSchema.CurrentServerRole] = ((ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole] | ServerRole.ProvisionedServer);
				return;
			}
			propertyBag[FrontendTransportServerADSchema.CurrentServerRole] = ((ServerRole)propertyBag[FrontendTransportServerADSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
		}

		// Token: 0x0400391C RID: 14620
		public static readonly ADPropertyDefinition ConnectivityLogMaxAge = new ADPropertyDefinition("ConnectivityLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxConnectivityLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400391D RID: 14621
		public static readonly ADPropertyDefinition ConnectivityLogMaxDirectorySize = new ADPropertyDefinition("ConnectivityLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportConnectivityLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400391E RID: 14622
		public static readonly ADPropertyDefinition ConnectivityLogMaxFileSize = new ADPropertyDefinition("ConnectivityLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportConnectivityLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400391F RID: 14623
		public static readonly ADPropertyDefinition ConnectivityLogPath = new ADPropertyDefinition("ConnectivityLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportConnectivityLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04003920 RID: 14624
		public static readonly ADPropertyDefinition CurrentServerRole = new ADPropertyDefinition("CurrentServerRole", ExchangeObjectVersion.Exchange2007, typeof(ServerRole), "msExchCurrentServerRoles", ADPropertyDefinitionFlags.PersistDefaultValue, ServerRole.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003921 RID: 14625
		public static readonly ADPropertyDefinition ExchangeLegacyDN = new ADPropertyDefinition("ExchangeLegacyDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003922 RID: 14626
		public static readonly ADPropertyDefinition ExternalDNSAdapterGuid = new ADPropertyDefinition("ExternalDNSAdapterGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchTransportExternalDNSAdapterGuid", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003923 RID: 14627
		public static readonly ADPropertyDefinition ExternalDNSServersStr = new ADPropertyDefinition("ExternalDNSServersStr", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSmtpExternalDNSServers", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003924 RID: 14628
		public static readonly ADPropertyDefinition ExternalDNSProtocolOption = new ADPropertyDefinition("ExternalDNSProtocolOption", ExchangeObjectVersion.Exchange2007, typeof(ProtocolOption), "msExchTransportExternalDNSProtocolOption", ADPropertyDefinitionFlags.None, ProtocolOption.Any, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003925 RID: 14629
		public static readonly ADPropertyDefinition ExternalIPAddress = new ADPropertyDefinition("ExternalIPAddress", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTransportExternalIPAddress", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003926 RID: 14630
		public static readonly ADPropertyDefinition InternalDNSAdapterGuid = new ADPropertyDefinition("InternalDNSAdapter", ExchangeObjectVersion.Exchange2007, typeof(Guid), "msExchTransportInternalDNSAdapterGuid", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003927 RID: 14631
		public static readonly ADPropertyDefinition InternalDNSProtocolOption = new ADPropertyDefinition("InternalDNSProtocolOption", ExchangeObjectVersion.Exchange2007, typeof(ProtocolOption), "msExchTransportInternalDNSProtocolOption", ADPropertyDefinitionFlags.None, ProtocolOption.Any, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003928 RID: 14632
		public static readonly ADPropertyDefinition InternalDNSServers = new ADPropertyDefinition("InternalDNSServers", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), "msExchTransportInternalDNSServers", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003929 RID: 14633
		public static readonly ADPropertyDefinition IntraOrgConnectorProtocolLoggingLevel = new ADPropertyDefinition("TransportIntraOrgConnectorProtocolLoggingLevel", ExchangeObjectVersion.Exchange2007, typeof(ProtocolLoggingLevel), "msExchTransportOutboundProtocolLoggingLevel", ADPropertyDefinitionFlags.None, ProtocolLoggingLevel.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400392A RID: 14634
		public static readonly ADPropertyDefinition FrontendTransportServerFlags = new ADPropertyDefinition("TransportServerFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x0400392B RID: 14635
		public static readonly ADPropertyDefinition IntraOrgConnectorSmtpMaxMessagesPerConnection = new ADPropertyDefinition("IntraOrgConnectorSmtpMaxMessagesPerConnection", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpMaxMessagesPerConnection", ADPropertyDefinitionFlags.PersistDefaultValue, 20, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x0400392C RID: 14636
		public static readonly ADPropertyDefinition MaxConnectionRatePerMinute = new ADPropertyDefinition("MaxConnectionRatePerMinute", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSmtpReceiveMaxConnectionRatePerMinute", ADPropertyDefinitionFlags.PersistDefaultValue, 1200, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400392D RID: 14637
		public static readonly ADPropertyDefinition MaxOutboundConnections = new ADPropertyDefinition("MaxOutboundConnections", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchSmtpMaxOutgoingConnections", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400392E RID: 14638
		public static readonly ADPropertyDefinition MaxPerDomainOutboundConnections = new ADPropertyDefinition("MaxPerDomainOutboundConnections", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<int>), "msExchSmtpMaxOutgoingConnectionsPerDomain", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(1, int.MaxValue)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400392F RID: 14639
		public static readonly ADPropertyDefinition NetworkAddress = new ADPropertyDefinition("NetworkAddress", ExchangeObjectVersion.Exchange2003, typeof(NetworkAddress), "networkAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003930 RID: 14640
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxAge = new ADPropertyDefinition("ReceiveProtocolLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxReceiveProtocolLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003931 RID: 14641
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxDirectorySize = new ADPropertyDefinition("ReceiveProtocolLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxReceiveProtocolLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003932 RID: 14642
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxFileSize = new ADPropertyDefinition("ReceiveProtocolLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxReceiveProtocolLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003933 RID: 14643
		public static readonly ADPropertyDefinition ReceiveProtocolLogPath = new ADPropertyDefinition("ReceiveProtocolLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportReceiveProtocolLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04003934 RID: 14644
		public static readonly ADPropertyDefinition SendProtocolLogMaxAge = new ADPropertyDefinition("SendProtocolLogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportMaxSendProtocolLogAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003935 RID: 14645
		public static readonly ADPropertyDefinition SendProtocolLogMaxDirectorySize = new ADPropertyDefinition("SendProtocolLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxSendProtocolLogDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003936 RID: 14646
		public static readonly ADPropertyDefinition SendProtocolLogMaxFileSize = new ADPropertyDefinition("SendProtocolLogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchTransportMaxSendProtocolLogFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003937 RID: 14647
		public static readonly ADPropertyDefinition SendProtocolLogPath = new ADPropertyDefinition("SendProtocolLogPath", ExchangeObjectVersion.Exchange2007, typeof(LocalLongFullPath), "msExchTransportSendProtocolLogPath", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			LocalLongFullPathLengthConstraint.LocalLongFullDirectoryPathLengthConstraint
		}, null, null);

		// Token: 0x04003938 RID: 14648
		public static readonly ADPropertyDefinition SerialNumber = new ADPropertyDefinition("SerialNumber", ExchangeObjectVersion.Exchange2003, typeof(string), "serialNumber", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003939 RID: 14649
		public static readonly ADPropertyDefinition ServerType = new ADPropertyDefinition("ServerType", ExchangeObjectVersion.Exchange2003, typeof(string), "type", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400393A RID: 14650
		public new static readonly ADPropertyDefinition SystemFlags = new ADPropertyDefinition("SystemFlags", ExchangeObjectVersion.Exchange2003, typeof(SystemFlagsEnum), "systemFlags", ADPropertyDefinitionFlags.PersistDefaultValue, SystemFlagsEnum.DeleteImmediately | SystemFlagsEnum.Renamable, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400393B RID: 14651
		public static readonly ADPropertyDefinition TransientFailureRetryCount = new ADPropertyDefinition("TransientFailureRetryCount", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchTransportTransientFailureRetryCount", ADPropertyDefinitionFlags.PersistDefaultValue, 6, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 15)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400393C RID: 14652
		public static readonly ADPropertyDefinition TransientFailureRetryInterval = new ADPropertyDefinition("TransientFailureRetryInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchTransportTransientFailureRetryInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(5.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.FromHours(12.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400393D RID: 14653
		public static readonly ADPropertyDefinition VersionNumber = new ADPropertyDefinition("VersionNumber", ExchangeObjectVersion.Exchange2003, typeof(int), "versionNumber", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400393E RID: 14654
		public static readonly ADPropertyDefinition AdminDisplayVersion = new ADPropertyDefinition("AdminDisplayVersion", ExchangeObjectVersion.Exchange2003, typeof(ServerVersion), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.SerialNumber
		}, null, new GetterDelegate(FrontendTransportServerADSchema.AdminDisplayVersionGetter), new SetterDelegate(FrontendTransportServerADSchema.AdminDisplayVersionSetter), null, null);

		// Token: 0x0400393F RID: 14655
		public static readonly ADPropertyDefinition AntispamAgentsEnabled = new ADPropertyDefinition("AntispamAgentsEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.FrontendTransportServerFlags
		}, null, ADObject.FlagGetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 1), ADObject.FlagSetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 1), null, null);

		// Token: 0x04003940 RID: 14656
		public static readonly ADPropertyDefinition ConnectivityLogEnabled = new ADPropertyDefinition("ConnectivityLogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.FrontendTransportServerFlags
		}, null, ADObject.FlagGetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 2), ADObject.FlagSetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 2), null, null);

		// Token: 0x04003941 RID: 14657
		public static readonly ADPropertyDefinition ExternalDNSAdapterDisabled = new ADPropertyDefinition("ExternalDNSAdapterDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.FrontendTransportServerFlags
		}, null, ADObject.FlagGetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 4), ADObject.FlagSetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 4), null, null);

		// Token: 0x04003942 RID: 14658
		public static readonly ADPropertyDefinition Edition = new ADPropertyDefinition("Edition", ExchangeObjectVersion.Exchange2003, typeof(ServerEditionType), null, ADPropertyDefinitionFlags.Calculated, ServerEditionType.Unknown, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.ServerType
		}, null, new GetterDelegate(FrontendTransportServerADSchema.EditionGetter), new SetterDelegate(FrontendTransportServerADSchema.EditionSetter), null, null);

		// Token: 0x04003943 RID: 14659
		public static readonly ADPropertyDefinition ExternalDNSServers = new ADPropertyDefinition("ExternalDNSServers", ExchangeObjectVersion.Exchange2007, typeof(IPAddress), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.ExternalDNSServersStr
		}, null, new GetterDelegate(FrontendTransportServerADSchema.ExternalDNSServersGetter), new SetterDelegate(FrontendTransportServerADSchema.ExternalDNSServersSetter), null, null);

		// Token: 0x04003944 RID: 14660
		public static readonly ADPropertyDefinition InternalDNSAdapterDisabled = new ADPropertyDefinition("InternalDNSAdapterDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.FrontendTransportServerFlags
		}, null, ADObject.FlagGetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 8), ADObject.FlagSetterDelegate(FrontendTransportServerADSchema.FrontendTransportServerFlags, 8), null, null);

		// Token: 0x04003945 RID: 14661
		public static readonly ADPropertyDefinition IsFrontendTransportServer = new ADPropertyDefinition("IsFrontendTransportServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.CurrentServerRole
		}, null, new GetterDelegate(FrontendTransportServerADSchema.IsFrontendTransportServerGetter), new SetterDelegate(FrontendTransportServerADSchema.IsFrontendTransportServerSetter), null, null);

		// Token: 0x04003946 RID: 14662
		public static readonly ADPropertyDefinition IsProvisionedServer = new ADPropertyDefinition("IsProvisionedServer", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			FrontendTransportServerADSchema.CurrentServerRole
		}, null, new GetterDelegate(FrontendTransportServerADSchema.IsProvisionedServerGetter), new SetterDelegate(FrontendTransportServerADSchema.IsProvisionedServerSetter), null, null);

		// Token: 0x04003947 RID: 14663
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x04003948 RID: 14664
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<FrontendTransportServerConfigXML>(FrontendTransportServerADSchema.ConfigurationXMLRaw);

		// Token: 0x04003949 RID: 14665
		public static readonly ADPropertyDefinition AgentLogEnabled = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, bool>("AgentLogEnabled", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, true, (FrontendTransportServerConfigXML configXml) => configXml.AgentLog.Enabled, delegate(FrontendTransportServerConfigXML configXml, bool value)
		{
			configXml.AgentLog.Enabled = value;
		}, null, null);

		// Token: 0x0400394A RID: 14666
		public static readonly ADPropertyDefinition AgentLogMaxAge = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, EnhancedTimeSpan>("AgentLogMaxAge", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (FrontendTransportServerConfigXML configXml) => configXml.AgentLog.MaxAge, delegate(FrontendTransportServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.AgentLog.MaxAge = value;
		}, null, null);

		// Token: 0x0400394B RID: 14667
		public static readonly ADPropertyDefinition AgentLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("AgentLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (FrontendTransportServerConfigXML configXml) => configXml.AgentLog.MaxDirectorySize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AgentLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x0400394C RID: 14668
		public static readonly ADPropertyDefinition AgentLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("AgentLogMaxFileSize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (FrontendTransportServerConfigXML configXml) => configXml.AgentLog.MaxFileSize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AgentLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x0400394D RID: 14669
		public static readonly ADPropertyDefinition AgentLogPath = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, LocalLongFullPath>("AgentLogPath", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, null, (FrontendTransportServerConfigXML configXml) => configXml.AgentLog.Path, delegate(FrontendTransportServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.AgentLog.Path = value;
		}, null, null);

		// Token: 0x0400394E RID: 14670
		public static readonly ADPropertyDefinition DnsLogEnabled = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, bool>("DnsLogEnabled", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, false, (FrontendTransportServerConfigXML configXml) => configXml.DnsLog.Enabled, delegate(FrontendTransportServerConfigXML configXml, bool value)
		{
			configXml.DnsLog.Enabled = value;
		}, null, null);

		// Token: 0x0400394F RID: 14671
		public static readonly ADPropertyDefinition DnsLogMaxAge = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, EnhancedTimeSpan>("DnsLogMaxAge", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, EnhancedTimeSpan.FromDays(7.0), (FrontendTransportServerConfigXML configXml) => configXml.DnsLog.MaxAge, delegate(FrontendTransportServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.DnsLog.MaxAge = value;
		}, null, null);

		// Token: 0x04003950 RID: 14672
		public static readonly ADPropertyDefinition DnsLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("DnsLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(100UL), (FrontendTransportServerConfigXML configXml) => configXml.DnsLog.MaxDirectorySize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.DnsLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04003951 RID: 14673
		public static readonly ADPropertyDefinition DnsLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("DnsLogMaxFileSize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(10UL), (FrontendTransportServerConfigXML configXml) => configXml.DnsLog.MaxFileSize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.DnsLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04003952 RID: 14674
		public static readonly ADPropertyDefinition DnsLogPath = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, LocalLongFullPath>("DnsLogPath", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, null, (FrontendTransportServerConfigXML configXml) => configXml.DnsLog.Path, delegate(FrontendTransportServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.DnsLog.Path = value;
		}, null, null);

		// Token: 0x04003953 RID: 14675
		public static readonly ADPropertyDefinition ResourceLogEnabled = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, bool>("ResourceLogEnabled", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, true, (FrontendTransportServerConfigXML configXml) => configXml.ResourceLog.Enabled, delegate(FrontendTransportServerConfigXML configXml, bool value)
		{
			configXml.ResourceLog.Enabled = value;
		}, null, null);

		// Token: 0x04003954 RID: 14676
		public static readonly ADPropertyDefinition ResourceLogMaxAge = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, EnhancedTimeSpan>("ResourceLogMaxAge", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, EnhancedTimeSpan.FromDays(7.0), (FrontendTransportServerConfigXML configXml) => configXml.ResourceLog.MaxAge, delegate(FrontendTransportServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.ResourceLog.MaxAge = value;
		}, null, null);

		// Token: 0x04003955 RID: 14677
		public static readonly ADPropertyDefinition ResourceLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("ResourceLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(100UL), (FrontendTransportServerConfigXML configXml) => configXml.ResourceLog.MaxDirectorySize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ResourceLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x04003956 RID: 14678
		public static readonly ADPropertyDefinition ResourceLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("ResourceLogMaxFileSize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, ByteQuantifiedSize.FromMB(10UL), (FrontendTransportServerConfigXML configXml) => configXml.ResourceLog.MaxFileSize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.ResourceLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x04003957 RID: 14679
		public static readonly ADPropertyDefinition ResourceLogPath = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, LocalLongFullPath>("ResourceLogPath", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, null, (FrontendTransportServerConfigXML configXml) => configXml.ResourceLog.Path, delegate(FrontendTransportServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.ResourceLog.Path = value;
		}, null, null);

		// Token: 0x04003958 RID: 14680
		public static readonly ADPropertyDefinition AttributionLogEnabled = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, bool>("AttributionLogEnabled", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, false, (FrontendTransportServerConfigXML configXml) => configXml.AttributionLog.Enabled, delegate(FrontendTransportServerConfigXML configXml, bool value)
		{
			configXml.AttributionLog.Enabled = value;
		}, null, null);

		// Token: 0x04003959 RID: 14681
		public static readonly ADPropertyDefinition AttributionLogMaxAge = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, EnhancedTimeSpan>("AttributionLogMaxAge", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxAge, (FrontendTransportServerConfigXML configXml) => configXml.AttributionLog.MaxAge, delegate(FrontendTransportServerConfigXML configXml, EnhancedTimeSpan value)
		{
			configXml.AttributionLog.MaxAge = value;
		}, null, null);

		// Token: 0x0400395A RID: 14682
		public static readonly ADPropertyDefinition AttributionLogMaxDirectorySize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("AttributionLogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxDirectorySize, (FrontendTransportServerConfigXML configXml) => configXml.AttributionLog.MaxDirectorySize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AttributionLog.MaxDirectorySize = value;
		}, null, null);

		// Token: 0x0400395B RID: 14683
		public static readonly ADPropertyDefinition AttributionLogMaxFileSize = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, Unlimited<ByteQuantifiedSize>>("AttributionLogMaxFileSize", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, LogConfigXML.DefaultMaxFileSize, (FrontendTransportServerConfigXML configXml) => configXml.AttributionLog.MaxFileSize, delegate(FrontendTransportServerConfigXML configXml, Unlimited<ByteQuantifiedSize> value)
		{
			configXml.AttributionLog.MaxFileSize = value;
		}, null, null);

		// Token: 0x0400395C RID: 14684
		public static readonly ADPropertyDefinition AttributionLogPath = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, LocalLongFullPath>("AttributionLogPath", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, null, (FrontendTransportServerConfigXML configXml) => configXml.AttributionLog.Path, delegate(FrontendTransportServerConfigXML configXml, LocalLongFullPath value)
		{
			configXml.AttributionLog.Path = value;
		}, null, null);

		// Token: 0x0400395D RID: 14685
		public static readonly ADPropertyDefinition MaxReceiveTlsRatePerMinute = XMLSerializableBase.ConfigXmlProperty<FrontendTransportServerConfigXML, int>("MaxReceiveTlsRatePerMinute", ExchangeObjectVersion.Exchange2007, FrontendTransportServerADSchema.ConfigurationXML, 6000, (FrontendTransportServerConfigXML configXml) => configXml.MaxReceiveTlsRatePerMinute, delegate(FrontendTransportServerConfigXML configXml, int value)
		{
			configXml.MaxReceiveTlsRatePerMinute = value;
		}, null, null);
	}
}
