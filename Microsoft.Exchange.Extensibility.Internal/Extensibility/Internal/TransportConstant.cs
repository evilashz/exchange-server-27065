using System;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000059 RID: 89
	internal static class TransportConstant
	{
		// Token: 0x04000361 RID: 865
		internal const string DirectoryPrefix = "Microsoft.Exchange.Transport.DirectoryData.";

		// Token: 0x04000362 RID: 866
		public const string RecipientType = "Microsoft.Exchange.Transport.DirectoryData.RecipientType";

		// Token: 0x04000363 RID: 867
		public const string IsResource = "Microsoft.Exchange.Transport.DirectoryData.IsResource";

		// Token: 0x04000364 RID: 868
		public const string RecipientTypeDetailsRaw = "Microsoft.Exchange.Transport.DirectoryData.RecipientTypeDetailsRaw";

		// Token: 0x04000365 RID: 869
		public const string ExternalEmailAddress = "Microsoft.Exchange.Transport.DirectoryData.ExternalEmailAddress";

		// Token: 0x04000366 RID: 870
		public const string ForwardingSmtpAddress = "Microsoft.Exchange.Transport.DirectoryData.ForwardingSmtpAddress";

		// Token: 0x04000367 RID: 871
		public const string DeliverToMailboxAndForward = "Microsoft.Exchange.Transport.DirectoryData.DeliverToMailboxAndForward";

		// Token: 0x04000368 RID: 872
		public const string Database = "Microsoft.Exchange.Transport.DirectoryData.Database";

		// Token: 0x04000369 RID: 873
		public const string MailboxGuid = "Microsoft.Exchange.Transport.DirectoryData.ExchangeGuid";

		// Token: 0x0400036A RID: 874
		public const string ThrottlingPolicy = "Microsoft.Exchange.Transport.DirectoryData.ThrottlingPolicy";

		// Token: 0x0400036B RID: 875
		public const string RedirectHosts = "Microsoft.Exchange.Transport.DirectoryData.RedirectHosts";

		// Token: 0x0400036C RID: 876
		public const string JournalRecipientList = "Microsoft.Exchange.JournalRecipientList";

		// Token: 0x0400036D RID: 877
		public const string OriginalP1RecipientList = "Microsoft.Exchange.OriginalP1RecipientList";

		// Token: 0x0400036E RID: 878
		public const string EnvelopeJournalRecipientList = "Microsoft.Exchange.EnvelopeJournalRecipientList";

		// Token: 0x0400036F RID: 879
		public const string EnvelopeJournalRecipientType = "Microsoft.Exchange.EnvelopeJournalRecipientType";

		// Token: 0x04000370 RID: 880
		public const string EnvelopeJournalExpansionHistory = "Microsoft.Exchange.EnvelopeJournalExpansionHistory";

		// Token: 0x04000371 RID: 881
		public const string LegacyJournalReport = "Microsoft.Exchange.LegacyJournalReport";

		// Token: 0x04000372 RID: 882
		public const string IsRemoteRecipient = "Microsoft.Exchange.Transport.IsRemoteRecipient";

		// Token: 0x04000373 RID: 883
		public const string RecipientP2TypeProperty = "Microsoft.Exchange.Transport.RecipientP2Type";

		// Token: 0x04000374 RID: 884
		public const string ElcJournalReport = "Microsoft.Exchange.Transport.ElcJournalReport";

		// Token: 0x04000375 RID: 885
		public const string OpenDomainRoutingDisabled = "Microsoft.Exchange.Transport.OpenDomainRoutingDisabled";

		// Token: 0x04000376 RID: 886
		public const string ResentMapiMessage = "Microsoft.Exchange.Transport.ResentMapiMessage";

		// Token: 0x04000377 RID: 887
		public const string ResentMapiP2ToRecipients = "Microsoft.Exchange.Transport.ResentMapiP2ToRecipients";

		// Token: 0x04000378 RID: 888
		public const string ResentMapiP2CcRecipients = "Microsoft.Exchange.Transport.ResentMapiP2CcRecipients";

		// Token: 0x04000379 RID: 889
		public const string ContentIdentifierProperty = "Microsoft.Exchange.ContentIdentifier";

		// Token: 0x0400037A RID: 890
		public const string SmtpMuaSubmission = "Microsoft.Exchange.SmtpMuaSubmission";

		// Token: 0x0400037B RID: 891
		public const string TlsDomain = "Microsoft.Exchange.Transport.MailRecipient.TlsDomain";

		// Token: 0x0400037C RID: 892
		public const string TenantInboundConnectorId = "Microsoft.Exchange.Hygiene.TenantInboundConnectorId";

		// Token: 0x0400037D RID: 893
		public const string TenantOutboundConnectorId = "Microsoft.Exchange.Hygiene.TenantOutboundConnectorId";

		// Token: 0x0400037E RID: 894
		public const string TenantOutboundConnectorCustomData = "Microsoft.Exchange.Hygiene.TenantOutboundConnectorCustomData";

		// Token: 0x0400037F RID: 895
		public const string EffectiveTlsAuthLevel = "Microsoft.Exchange.Transport.MailRecipient.EffectiveTlsAuthLevel";

		// Token: 0x04000380 RID: 896
		public const string RequiredTlsAuthLevel = "Microsoft.Exchange.Transport.MailRecipient.RequiredTlsAuthLevel";

		// Token: 0x04000381 RID: 897
		public const string OutboundIPPoolLabel = "Microsoft.Exchange.Transport.MailRecipient.OutboundIPPool";

		// Token: 0x04000382 RID: 898
		public const string OriginatorOrganization = "Microsoft.Exchange.Transport.MailRecipient.OriginatorOrganization";

		// Token: 0x04000383 RID: 899
		public const string OrganizationScope = "Microsoft.Exchange.Transport.MailRecipient.OrganizationScope";

		// Token: 0x04000384 RID: 900
		public const string NetworkMessageId = "Microsoft.Exchange.Transport.MailRecipient.NetworkMessageId";

		// Token: 0x04000385 RID: 901
		public const string AddressBookPolicy = "Microsoft.Exchange.Transport.MailRecipient.AddressBookPolicy";

		// Token: 0x04000386 RID: 902
		public const string DisplayName = "Microsoft.Exchange.Transport.MailRecipient.DisplayName";

		// Token: 0x04000387 RID: 903
		public const string MailFlowPartnerInternalMailContentType = "Microsoft.Exchange.Transport.Agent.OpenDomainRouting.MailFlowPartnerSettings.InternalMailContentType";

		// Token: 0x04000388 RID: 904
		public const string RecipientDiagnosticInfo = "Microsoft.Exchange.Transport.RecipientDiagnosticInfo";

		// Token: 0x04000389 RID: 905
		public const string TransportDecrypted = "Microsoft.Exchange.RightsManagement.TransportDecrypted";

		// Token: 0x0400038A RID: 906
		public const string TransportDecryptionPL = "Microsoft.Exchange.RightsManagement.TransportDecryptionPL";

		// Token: 0x0400038B RID: 907
		public const string TransportDecryptionUL = "Microsoft.Exchange.RightsManagement.TransportDecryptionUL";

		// Token: 0x0400038C RID: 908
		public const string TransportDecryptionLicenseUri = "Microsoft.Exchange.RightsManagement.TransportDecryptionLicenseUri";

		// Token: 0x0400038D RID: 909
		public const string TransportE4ERpmsg = "Microsoft.Exchange.Encryption.TransportRpmsg";

		// Token: 0x0400038E RID: 910
		public const string TransportE4EDecryptionPL = "Microsoft.Exchange.Encryption.TransportDecryptionPL";

		// Token: 0x0400038F RID: 911
		public const string TransportE4EDecryptionUL = "Microsoft.Exchange.Encryption.TransportDecryptionUL";

		// Token: 0x04000390 RID: 912
		public const string TransportE4EDecryptionLicenseUri = "Microsoft.Exchange.Encryption.TransportDecryptionLicenseUri";

		// Token: 0x04000391 RID: 913
		public const string DecryptionTokenRecipient = "Microsoft.Exchange.RightsManagement.DecryptionTokenRecipient";

		// Token: 0x04000392 RID: 914
		public const string DecryptionE4ETokenRecipient = "Microsoft.Exchange.Encryption.DecryptionTokenRecipient";

		// Token: 0x04000393 RID: 915
		public const string RecipientEUL = "Microsoft.Exchange.RightsManagement.DRMLicense";

		// Token: 0x04000394 RID: 916
		public const string RecipientDrmRights = "Microsoft.Exchange.RightsManagement.DRMRights";

		// Token: 0x04000395 RID: 917
		public const string RecipientDrmExpiryTime = "Microsoft.Exchange.RightsManagement.DRMExpiryTime";

		// Token: 0x04000396 RID: 918
		public const string RecipientDrmPropsSignature = "Microsoft.Exchange.RightsManagement.DRMPropsSignature";

		// Token: 0x04000397 RID: 919
		public const string RecipientDrmFailure = "Microsoft.Exchange.RightsManagement.DRMFailure";

		// Token: 0x04000398 RID: 920
		public const string RecipientB2BEul = "Microsoft.Exchange.RightsManagement.B2BDRMLicense";

		// Token: 0x04000399 RID: 921
		internal const string SystemMessageContentIdentifier = "ExSysMessage";

		// Token: 0x0400039A RID: 922
		internal const string VoicemailMessageClass = "IPM.Note.Microsoft.Voicemail.UM";

		// Token: 0x0400039B RID: 923
		internal const string VoicemailMessageClassCA = "IPM.Note.Microsoft.Voicemail.UM.CA";

		// Token: 0x0400039C RID: 924
		internal const string MissedCallMessageClass = "IPM.Note.Microsoft.Missed.Voice";

		// Token: 0x0400039D RID: 925
		public const string QuarantineMessageMarkerHeader = "X-MS-Exchange-Organization-Quarantine";

		// Token: 0x0400039E RID: 926
		public const string SmtpServicePrincipalName = "SmtpSvc";

		// Token: 0x0400039F RID: 927
		public const string AttachmentRemovedLabel = "a4bb0cb2-4395-4d18-9799-1f904b20fe92";

		// Token: 0x040003A0 RID: 928
		public const string MExConfigFilePath = "TransportRoles\\Shared\\agents.config";

		// Token: 0x040003A1 RID: 929
		public const string MExFrontEndConfigFilePath = "TransportRoles\\Shared\\fetagents.config";

		// Token: 0x040003A2 RID: 930
		public const string ModerationOriginalMessageFileName = "OriginalMessage";

		// Token: 0x040003A3 RID: 931
		public const string ModerationReplayXHeaderFileName = "ReplayXHeaders";

		// Token: 0x040003A4 RID: 932
		public const string ModerationFireWalledHeadersFileName = "FireWalledHeaders";

		// Token: 0x040003A5 RID: 933
		public const string InboundTrustEnabled = "Microsoft.Exchange.Transport.InboundTrustEnabled";

		// Token: 0x040003A6 RID: 934
		public const string MailboxRuleDelegateAccessRuleName = ".DelegateAccess";

		// Token: 0x040003A7 RID: 935
		public const string GeneratedByMailboxRule = "Microsoft.Exchange.Transport.GeneratedByMailboxRule";

		// Token: 0x040003A8 RID: 936
		public const string AddedByTransportRule = "Microsoft.Exchange.Transport.AddedByTransportRule";

		// Token: 0x040003A9 RID: 937
		public const string ModeratedByTransportRule = "Microsoft.Exchange.Transport.ModeratedByTransportRule";

		// Token: 0x040003AA RID: 938
		public const string PFNoReplicaRoutingOverride = "Microsoft.Exchange.Transport.RoutingOverride";

		// Token: 0x040003AB RID: 939
		public const string SmtpInSessionId = "Microsoft.Exchange.Transport.SmtpInSessionId";

		// Token: 0x040003AC RID: 940
		public const string NextHopFqdn = "Microsoft.Exchange.Transport.Proxy.NextHopFqdn";

		// Token: 0x040003AD RID: 941
		public const string IsNextHopInternal = "Microsoft.Exchange.Transport.Proxy.IsNextHopInternal";

		// Token: 0x040003AE RID: 942
		public const string RetryOnDuplicateDelivery = "Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ";

		// Token: 0x040003AF RID: 943
		public const string InternalMessageId = "Microsoft.Exchange.Transport.MailboxTransport.InternalMessageId";

		// Token: 0x040003B0 RID: 944
		public const string MailboxTransportSmtpInClientHostname = "Microsoft.Exchange.Transport.MailboxTransport.SmtpInClientHostname";

		// Token: 0x040003B1 RID: 945
		public const string DeliveryQueueMailboxSubComponent = "Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponent";

		// Token: 0x040003B2 RID: 946
		public const string DeliveryQueueMailboxSubComponentLatency = "Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponentLatency";

		// Token: 0x040003B3 RID: 947
		public const string SmtpReceiveAgentsDiagnosticsComponentName = "SmtpReceiveAgents";

		// Token: 0x040003B4 RID: 948
		public const string RoutingAgentsDiagnosticsComponentName = "RoutingAgents";

		// Token: 0x040003B5 RID: 949
		public const string DeliveryAgentsDiagnosticsComponentName = "DeliveryAgents";

		// Token: 0x040003B6 RID: 950
		public const string StorageAgentsDiagnosticsComponentName = "StorageAgents";

		// Token: 0x040003B7 RID: 951
		public const string SmtpReceiveAgentsMExRuntimeAgentType = "SmtpReceiveAgent";

		// Token: 0x040003B8 RID: 952
		public const string RoutingAgentsMExRuntimeAgentType = "RoutingAgent";

		// Token: 0x040003B9 RID: 953
		public const string DeliveryAgentsMExRuntimeAgentType = "DeliveryAgent";

		// Token: 0x040003BA RID: 954
		public const string StorageAgentsMExRuntimeAgentType = "StorageAgent";

		// Token: 0x040003BB RID: 955
		public const string CategorizerDiagnosticsComponentName = "Categorizer";

		// Token: 0x040003BC RID: 956
		public const string DeliveryDiagnosticsComponentName = "RemoteDelivery";

		// Token: 0x040003BD RID: 957
		public const string ShadowRedundancyDiagnosticsComponentName = "ShadowRedundancy";

		// Token: 0x040003BE RID: 958
		public const string PreserveCrossPremisesHeaders = "PreserveCrossPremisesHeaders";

		// Token: 0x040003BF RID: 959
		public const string CrossPremisesHeadersBlockedHeader = "X-MS-Exchange-Organization-Cross-Premises-Headers-Blocked";

		// Token: 0x040003C0 RID: 960
		public const string CrossPremisesHeadersPromotedHeader = "X-MS-Exchange-Organization-Cross-Premises-Headers-Promoted";

		// Token: 0x040003C1 RID: 961
		public const string ConnectorRedirectPropertyName = "ConnectorRedirect";

		// Token: 0x040003C2 RID: 962
		public const string IsProbePropertyName = "IsProbe";

		// Token: 0x040003C3 RID: 963
		public const string ProbeTypePropertyName = "ProbeType";

		// Token: 0x040003C4 RID: 964
		public const string PersistProbeTracePropertyName = "PersistProbeTrace";

		// Token: 0x040003C5 RID: 965
		public const string ConsolidateAdvancedRoutingAppConfigKey = "ConsolidateAdvancedRouting";

		// Token: 0x040003C6 RID: 966
		internal const string VoltageEncryptedHeaderName = "X-Voltage-Encrypted";

		// Token: 0x040003C7 RID: 967
		internal const string VoltageEncryptedHeaderValue = "Encrypted";

		// Token: 0x040003C8 RID: 968
		internal const string VoltageDecryptedHeaderName = "X-Voltage-Decrypted";

		// Token: 0x040003C9 RID: 969
		internal const string VoltageDecryptedHeaderValue = "Decrypted";

		// Token: 0x040003CA RID: 970
		internal const string HeaderNameForVoltageEncryptAction = "X-Voltage-Encrypt";

		// Token: 0x040003CB RID: 971
		internal const string HeaderValueForVoltageEncryptAction = "Encrypt";

		// Token: 0x040003CC RID: 972
		internal const string HeaderNameForVoltageDecryptAction = "X-Voltage-Decrypt";

		// Token: 0x040003CD RID: 973
		internal const string HeaderValueForVoltageDecryptAction = "Decrypt";

		// Token: 0x0200005A RID: 90
		internal class ComponentCost
		{
			// Token: 0x040003CE RID: 974
			internal const string PropertyName = "CompCost";

			// Token: 0x040003CF RID: 975
			internal const string TransportRules = "ETR";

			// Token: 0x040003D0 RID: 976
			internal const string Antimalware = "AMA";

			// Token: 0x040003D1 RID: 977
			internal const string SpamFilter = "SFA";
		}
	}
}
