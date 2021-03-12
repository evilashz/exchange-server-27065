using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000714 RID: 1812
	internal class FrontendTransportServerSchema : ADPresentationSchema
	{
		// Token: 0x0600554B RID: 21835 RVA: 0x00134F5C File Offset: 0x0013315C
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<FrontendTransportServerADSchema>();
		}

		// Token: 0x04003988 RID: 14728
		public static readonly ADPropertyDefinition AdminDisplayVersion = FrontendTransportServerADSchema.AdminDisplayVersion;

		// Token: 0x04003989 RID: 14729
		public static readonly ADPropertyDefinition Edition = FrontendTransportServerADSchema.Edition;

		// Token: 0x0400398A RID: 14730
		public static readonly ADPropertyDefinition ExchangeLegacyDN = FrontendTransportServerADSchema.ExchangeLegacyDN;

		// Token: 0x0400398B RID: 14731
		public static readonly ADPropertyDefinition NetworkAddress = FrontendTransportServerADSchema.NetworkAddress;

		// Token: 0x0400398C RID: 14732
		public static readonly ADPropertyDefinition VersionNumber = FrontendTransportServerADSchema.VersionNumber;

		// Token: 0x0400398D RID: 14733
		public static readonly ADPropertyDefinition IsFrontendTransportServer = FrontendTransportServerADSchema.IsFrontendTransportServer;

		// Token: 0x0400398E RID: 14734
		public static readonly ADPropertyDefinition IsProvisionedServer = FrontendTransportServerADSchema.IsProvisionedServer;

		// Token: 0x0400398F RID: 14735
		public static readonly ADPropertyDefinition IntraOrgConnectorSmtpMaxMessagesPerConnection = FrontendTransportServerADSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection;

		// Token: 0x04003990 RID: 14736
		public static readonly ADPropertyDefinition MaxPerDomainOutboundConnections = FrontendTransportServerADSchema.MaxPerDomainOutboundConnections;

		// Token: 0x04003991 RID: 14737
		public static readonly ADPropertyDefinition MaxOutboundConnections = FrontendTransportServerADSchema.MaxOutboundConnections;

		// Token: 0x04003992 RID: 14738
		public static readonly ADPropertyDefinition TransientFailureRetryInterval = FrontendTransportServerADSchema.TransientFailureRetryInterval;

		// Token: 0x04003993 RID: 14739
		public static readonly ADPropertyDefinition TransientFailureRetryCount = FrontendTransportServerADSchema.TransientFailureRetryCount;

		// Token: 0x04003994 RID: 14740
		public static readonly ADPropertyDefinition ReceiveProtocolLogPath = FrontendTransportServerADSchema.ReceiveProtocolLogPath;

		// Token: 0x04003995 RID: 14741
		public static readonly ADPropertyDefinition SendProtocolLogPath = FrontendTransportServerADSchema.SendProtocolLogPath;

		// Token: 0x04003996 RID: 14742
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxAge = FrontendTransportServerADSchema.ReceiveProtocolLogMaxAge;

		// Token: 0x04003997 RID: 14743
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxDirectorySize = FrontendTransportServerADSchema.ReceiveProtocolLogMaxDirectorySize;

		// Token: 0x04003998 RID: 14744
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxFileSize = FrontendTransportServerADSchema.ReceiveProtocolLogMaxFileSize;

		// Token: 0x04003999 RID: 14745
		public static readonly ADPropertyDefinition SendProtocolLogMaxAge = FrontendTransportServerADSchema.SendProtocolLogMaxAge;

		// Token: 0x0400399A RID: 14746
		public static readonly ADPropertyDefinition SendProtocolLogMaxDirectorySize = FrontendTransportServerADSchema.SendProtocolLogMaxDirectorySize;

		// Token: 0x0400399B RID: 14747
		public static readonly ADPropertyDefinition SendProtocolLogMaxFileSize = FrontendTransportServerADSchema.SendProtocolLogMaxFileSize;

		// Token: 0x0400399C RID: 14748
		public static readonly ADPropertyDefinition InternalDNSAdapterDisabled = FrontendTransportServerADSchema.InternalDNSAdapterDisabled;

		// Token: 0x0400399D RID: 14749
		public static readonly ADPropertyDefinition InternalDNSAdapterGuid = FrontendTransportServerADSchema.InternalDNSAdapterGuid;

		// Token: 0x0400399E RID: 14750
		public static readonly ADPropertyDefinition InternalDNSServers = FrontendTransportServerADSchema.InternalDNSServers;

		// Token: 0x0400399F RID: 14751
		public static readonly ADPropertyDefinition InternalDNSProtocolOption = FrontendTransportServerADSchema.InternalDNSProtocolOption;

		// Token: 0x040039A0 RID: 14752
		public static readonly ADPropertyDefinition IntraOrgConnectorProtocolLoggingLevel = FrontendTransportServerADSchema.IntraOrgConnectorProtocolLoggingLevel;

		// Token: 0x040039A1 RID: 14753
		public static readonly ADPropertyDefinition ExternalDNSAdapterDisabled = FrontendTransportServerADSchema.ExternalDNSAdapterDisabled;

		// Token: 0x040039A2 RID: 14754
		public static readonly ADPropertyDefinition ExternalDNSAdapterGuid = FrontendTransportServerADSchema.ExternalDNSAdapterGuid;

		// Token: 0x040039A3 RID: 14755
		public static readonly ADPropertyDefinition ExternalDNSServers = FrontendTransportServerADSchema.ExternalDNSServers;

		// Token: 0x040039A4 RID: 14756
		public static readonly ADPropertyDefinition ExternalDNSProtocolOption = FrontendTransportServerADSchema.ExternalDNSProtocolOption;

		// Token: 0x040039A5 RID: 14757
		public static readonly ADPropertyDefinition ExternalIPAddress = FrontendTransportServerADSchema.ExternalIPAddress;

		// Token: 0x040039A6 RID: 14758
		public static readonly ADPropertyDefinition ConnectivityLogEnabled = FrontendTransportServerADSchema.ConnectivityLogEnabled;

		// Token: 0x040039A7 RID: 14759
		public static readonly ADPropertyDefinition ConnectivityLogPath = FrontendTransportServerADSchema.ConnectivityLogPath;

		// Token: 0x040039A8 RID: 14760
		public static readonly ADPropertyDefinition ConnectivityLogMaxAge = FrontendTransportServerADSchema.ConnectivityLogMaxAge;

		// Token: 0x040039A9 RID: 14761
		public static readonly ADPropertyDefinition ConnectivityLogMaxDirectorySize = FrontendTransportServerADSchema.ConnectivityLogMaxDirectorySize;

		// Token: 0x040039AA RID: 14762
		public static readonly ADPropertyDefinition ConnectivityLogMaxFileSize = FrontendTransportServerADSchema.ConnectivityLogMaxFileSize;

		// Token: 0x040039AB RID: 14763
		public static readonly ADPropertyDefinition AntispamAgentsEnabled = FrontendTransportServerADSchema.AntispamAgentsEnabled;

		// Token: 0x040039AC RID: 14764
		public static readonly ADPropertyDefinition CurrentServerRole = FrontendTransportServerADSchema.CurrentServerRole;

		// Token: 0x040039AD RID: 14765
		public static readonly ADPropertyDefinition MaxConnectionRatePerMinute = FrontendTransportServerADSchema.MaxConnectionRatePerMinute;

		// Token: 0x040039AE RID: 14766
		public static readonly ADPropertyDefinition AgentLogEnabled = FrontendTransportServerADSchema.AgentLogEnabled;

		// Token: 0x040039AF RID: 14767
		public static readonly ADPropertyDefinition AgentLogMaxAge = FrontendTransportServerADSchema.AgentLogMaxAge;

		// Token: 0x040039B0 RID: 14768
		public static readonly ADPropertyDefinition AgentLogMaxDirectorySize = FrontendTransportServerADSchema.AgentLogMaxDirectorySize;

		// Token: 0x040039B1 RID: 14769
		public static readonly ADPropertyDefinition AgentLogMaxFileSize = FrontendTransportServerADSchema.AgentLogMaxFileSize;

		// Token: 0x040039B2 RID: 14770
		public static readonly ADPropertyDefinition AgentLogPath = FrontendTransportServerADSchema.AgentLogPath;

		// Token: 0x040039B3 RID: 14771
		public static readonly ADPropertyDefinition DnsLogEnabled = FrontendTransportServerADSchema.DnsLogEnabled;

		// Token: 0x040039B4 RID: 14772
		public static readonly ADPropertyDefinition DnsLogMaxAge = FrontendTransportServerADSchema.DnsLogMaxAge;

		// Token: 0x040039B5 RID: 14773
		public static readonly ADPropertyDefinition DnsLogMaxDirectorySize = FrontendTransportServerADSchema.DnsLogMaxDirectorySize;

		// Token: 0x040039B6 RID: 14774
		public static readonly ADPropertyDefinition DnsLogMaxFileSize = FrontendTransportServerADSchema.DnsLogMaxFileSize;

		// Token: 0x040039B7 RID: 14775
		public static readonly ADPropertyDefinition DnsLogPath = FrontendTransportServerADSchema.DnsLogPath;

		// Token: 0x040039B8 RID: 14776
		public static readonly ADPropertyDefinition ResourceLogEnabled = FrontendTransportServerADSchema.ResourceLogEnabled;

		// Token: 0x040039B9 RID: 14777
		public static readonly ADPropertyDefinition ResourceLogMaxAge = FrontendTransportServerADSchema.ResourceLogMaxAge;

		// Token: 0x040039BA RID: 14778
		public static readonly ADPropertyDefinition ResourceLogMaxDirectorySize = FrontendTransportServerADSchema.ResourceLogMaxDirectorySize;

		// Token: 0x040039BB RID: 14779
		public static readonly ADPropertyDefinition ResourceLogMaxFileSize = FrontendTransportServerADSchema.ResourceLogMaxFileSize;

		// Token: 0x040039BC RID: 14780
		public static readonly ADPropertyDefinition ResourceLogPath = FrontendTransportServerADSchema.ResourceLogPath;

		// Token: 0x040039BD RID: 14781
		public static readonly ADPropertyDefinition AttributionLogEnabled = FrontendTransportServerADSchema.AttributionLogEnabled;

		// Token: 0x040039BE RID: 14782
		public static readonly ADPropertyDefinition AttributionLogMaxAge = FrontendTransportServerADSchema.AttributionLogMaxAge;

		// Token: 0x040039BF RID: 14783
		public static readonly ADPropertyDefinition AttributionLogMaxDirectorySize = FrontendTransportServerADSchema.AttributionLogMaxDirectorySize;

		// Token: 0x040039C0 RID: 14784
		public static readonly ADPropertyDefinition AttributionLogMaxFileSize = FrontendTransportServerADSchema.AttributionLogMaxFileSize;

		// Token: 0x040039C1 RID: 14785
		public static readonly ADPropertyDefinition AttributionLogPath = FrontendTransportServerADSchema.AttributionLogPath;

		// Token: 0x040039C2 RID: 14786
		public static readonly ADPropertyDefinition MaxReceiveTlsRatePerMinute = FrontendTransportServerADSchema.MaxReceiveTlsRatePerMinute;
	}
}
