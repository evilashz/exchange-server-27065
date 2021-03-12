using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200071E RID: 1822
	internal class MailboxTransportServerSchema : ADPresentationSchema
	{
		// Token: 0x06005622 RID: 22050 RVA: 0x00137350 File Offset: 0x00135550
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<MailboxTransportServerADSchema>();
		}

		// Token: 0x04003A4F RID: 14927
		public static readonly ADPropertyDefinition AdminDisplayVersion = MailboxTransportServerADSchema.AdminDisplayVersion;

		// Token: 0x04003A50 RID: 14928
		public static readonly ADPropertyDefinition ConnectivityLogEnabled = MailboxTransportServerADSchema.ConnectivityLogEnabled;

		// Token: 0x04003A51 RID: 14929
		public static readonly ADPropertyDefinition ConnectivityLogPath = MailboxTransportServerADSchema.ConnectivityLogPath;

		// Token: 0x04003A52 RID: 14930
		public static readonly ADPropertyDefinition ConnectivityLogMaxAge = MailboxTransportServerADSchema.ConnectivityLogMaxAge;

		// Token: 0x04003A53 RID: 14931
		public static readonly ADPropertyDefinition ConnectivityLogMaxDirectorySize = MailboxTransportServerADSchema.ConnectivityLogMaxDirectorySize;

		// Token: 0x04003A54 RID: 14932
		public static readonly ADPropertyDefinition ConnectivityLogMaxFileSize = MailboxTransportServerADSchema.ConnectivityLogMaxFileSize;

		// Token: 0x04003A55 RID: 14933
		public static readonly ADPropertyDefinition ContentConversionTracingEnabled = MailboxTransportServerADSchema.ContentConversionTracingEnabled;

		// Token: 0x04003A56 RID: 14934
		public static readonly ADPropertyDefinition CurrentServerRole = MailboxTransportServerADSchema.CurrentServerRole;

		// Token: 0x04003A57 RID: 14935
		public static readonly ADPropertyDefinition Edition = MailboxTransportServerADSchema.Edition;

		// Token: 0x04003A58 RID: 14936
		public static readonly ADPropertyDefinition ExchangeLegacyDN = MailboxTransportServerADSchema.ExchangeLegacyDN;

		// Token: 0x04003A59 RID: 14937
		public static readonly ADPropertyDefinition IsMailboxServer = MailboxTransportServerADSchema.IsMailboxServer;

		// Token: 0x04003A5A RID: 14938
		public static readonly ADPropertyDefinition IsProvisionedServer = MailboxTransportServerADSchema.IsProvisionedServer;

		// Token: 0x04003A5B RID: 14939
		public static readonly ADPropertyDefinition InMemoryReceiveConnectorProtocolLoggingLevel = MailboxTransportServerADSchema.InMemoryReceiveConnectorProtocolLoggingLevel;

		// Token: 0x04003A5C RID: 14940
		public static readonly ADPropertyDefinition InMemoryReceiveConnectorSmtpUtf8Enabled = MailboxTransportServerADSchema.InMemoryReceiveConnectorSmtpUtf8Enabled;

		// Token: 0x04003A5D RID: 14941
		public static readonly ADPropertyDefinition MailboxDeliveryAgentLogEnabled = MailboxTransportServerADSchema.MailboxDeliveryAgentLogEnabled;

		// Token: 0x04003A5E RID: 14942
		public static readonly ADPropertyDefinition MailboxDeliveryAgentLogMaxAge = MailboxTransportServerADSchema.MailboxDeliveryAgentLogMaxAge;

		// Token: 0x04003A5F RID: 14943
		public static readonly ADPropertyDefinition MailboxDeliveryAgentLogMaxDirectorySize = MailboxTransportServerADSchema.MailboxDeliveryAgentLogMaxDirectorySize;

		// Token: 0x04003A60 RID: 14944
		public static readonly ADPropertyDefinition MailboxDeliveryAgentLogMaxFileSize = MailboxTransportServerADSchema.MailboxDeliveryAgentLogMaxFileSize;

		// Token: 0x04003A61 RID: 14945
		public static readonly ADPropertyDefinition MailboxDeliveryAgentLogPath = MailboxTransportServerADSchema.MailboxDeliveryAgentLogPath;

		// Token: 0x04003A62 RID: 14946
		public static readonly ADPropertyDefinition MailboxDeliveryThrottlingLogEnabled = MailboxTransportServerADSchema.MailboxDeliveryThrottlingLogEnabled;

		// Token: 0x04003A63 RID: 14947
		public static readonly ADPropertyDefinition MailboxDeliveryThrottlingLogMaxAge = MailboxTransportServerADSchema.MailboxDeliveryThrottlingLogMaxAge;

		// Token: 0x04003A64 RID: 14948
		public static readonly ADPropertyDefinition MailboxDeliveryThrottlingLogMaxDirectorySize = MailboxTransportServerADSchema.MailboxDeliveryThrottlingLogMaxDirectorySize;

		// Token: 0x04003A65 RID: 14949
		public static readonly ADPropertyDefinition MailboxDeliveryThrottlingLogMaxFileSize = MailboxTransportServerADSchema.MailboxDeliveryThrottlingLogMaxFileSize;

		// Token: 0x04003A66 RID: 14950
		public static readonly ADPropertyDefinition MailboxDeliveryThrottlingLogPath = MailboxTransportServerADSchema.MailboxDeliveryThrottlingLogPath;

		// Token: 0x04003A67 RID: 14951
		public static readonly ADPropertyDefinition MailboxDeliveryConnectorProtocolLoggingLevel = MailboxTransportServerADSchema.InMemoryReceiveConnectorProtocolLoggingLevel;

		// Token: 0x04003A68 RID: 14952
		public static readonly ADPropertyDefinition MailboxDeliveryConnectorSmtpUtf8Enabled = MailboxTransportServerADSchema.InMemoryReceiveConnectorSmtpUtf8Enabled;

		// Token: 0x04003A69 RID: 14953
		public static readonly ADPropertyDefinition MailboxSubmissionAgentLogEnabled = MailboxTransportServerADSchema.MailboxSubmissionAgentLogEnabled;

		// Token: 0x04003A6A RID: 14954
		public static readonly ADPropertyDefinition MailboxSubmissionAgentLogMaxAge = MailboxTransportServerADSchema.MailboxSubmissionAgentLogMaxAge;

		// Token: 0x04003A6B RID: 14955
		public static readonly ADPropertyDefinition MailboxSubmissionAgentLogMaxDirectorySize = MailboxTransportServerADSchema.MailboxSubmissionAgentLogMaxDirectorySize;

		// Token: 0x04003A6C RID: 14956
		public static readonly ADPropertyDefinition MailboxSubmissionAgentLogMaxFileSize = MailboxTransportServerADSchema.MailboxSubmissionAgentLogMaxFileSize;

		// Token: 0x04003A6D RID: 14957
		public static readonly ADPropertyDefinition MailboxSubmissionAgentLogPath = MailboxTransportServerADSchema.MailboxSubmissionAgentLogPath;

		// Token: 0x04003A6E RID: 14958
		public static readonly ADPropertyDefinition MaxConcurrentMailboxSubmissions = MailboxTransportServerADSchema.MaxConcurrentMailboxSubmissions;

		// Token: 0x04003A6F RID: 14959
		public static readonly ADPropertyDefinition MaxConcurrentMailboxDeliveries = MailboxTransportServerADSchema.MaxConcurrentMailboxDeliveries;

		// Token: 0x04003A70 RID: 14960
		public static readonly ADPropertyDefinition NetworkAddress = MailboxTransportServerADSchema.NetworkAddress;

		// Token: 0x04003A71 RID: 14961
		public static readonly ADPropertyDefinition PipelineTracingEnabled = MailboxTransportServerADSchema.PipelineTracingEnabled;

		// Token: 0x04003A72 RID: 14962
		public static readonly ADPropertyDefinition PipelineTracingPath = MailboxTransportServerADSchema.PipelineTracingPath;

		// Token: 0x04003A73 RID: 14963
		public static readonly ADPropertyDefinition PipelineTracingSenderAddress = MailboxTransportServerADSchema.PipelineTracingSenderAddress;

		// Token: 0x04003A74 RID: 14964
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxAge = MailboxTransportServerADSchema.ReceiveProtocolLogMaxAge;

		// Token: 0x04003A75 RID: 14965
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxDirectorySize = MailboxTransportServerADSchema.ReceiveProtocolLogMaxDirectorySize;

		// Token: 0x04003A76 RID: 14966
		public static readonly ADPropertyDefinition ReceiveProtocolLogMaxFileSize = MailboxTransportServerADSchema.ReceiveProtocolLogMaxFileSize;

		// Token: 0x04003A77 RID: 14967
		public static readonly ADPropertyDefinition ReceiveProtocolLogPath = MailboxTransportServerADSchema.ReceiveProtocolLogPath;

		// Token: 0x04003A78 RID: 14968
		public static readonly ADPropertyDefinition SendProtocolLogMaxAge = MailboxTransportServerADSchema.SendProtocolLogMaxAge;

		// Token: 0x04003A79 RID: 14969
		public static readonly ADPropertyDefinition SendProtocolLogMaxDirectorySize = MailboxTransportServerADSchema.SendProtocolLogMaxDirectorySize;

		// Token: 0x04003A7A RID: 14970
		public static readonly ADPropertyDefinition SendProtocolLogMaxFileSize = MailboxTransportServerADSchema.SendProtocolLogMaxFileSize;

		// Token: 0x04003A7B RID: 14971
		public static readonly ADPropertyDefinition SendProtocolLogPath = MailboxTransportServerADSchema.SendProtocolLogPath;

		// Token: 0x04003A7C RID: 14972
		public static readonly ADPropertyDefinition VersionNumber = MailboxTransportServerADSchema.VersionNumber;
	}
}
