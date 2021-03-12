using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000617 RID: 1559
	internal abstract class PopImapAdConfigurationSchema : ADEmailTransportSchema
	{
		// Token: 0x04003330 RID: 13104
		public static readonly ADPropertyDefinition PopImapProtocolFlags = new ADPropertyDefinition("PopImapProtocolFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchPopImapProtocolFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003331 RID: 13105
		public static readonly ADPropertyDefinition ProtocolLogEnabled = ADObject.BitfieldProperty("ProtocolLogEnabled", 0, PopImapAdConfigurationSchema.PopImapProtocolFlags);

		// Token: 0x04003332 RID: 13106
		public static readonly ADPropertyDefinition LogFileLocation = new ADPropertyDefinition("LogFileLocation", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchPopImapLogFilePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003333 RID: 13107
		public static readonly ADPropertyDefinition LogFileRollOverSettings = new ADPropertyDefinition("LogFileRollOverSettings", ExchangeObjectVersion.Exchange2010, typeof(LogFileRollOver), "msExchPopImapLogFileRolloverFrequency", ADPropertyDefinitionFlags.None, LogFileRollOver.Daily, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003334 RID: 13108
		public static readonly ADPropertyDefinition LogPerFileSizeQuota = new ADPropertyDefinition("LogPerFileSizeQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<ByteQuantifiedSize>), "msExchPopImapPerLogFileSizeQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003335 RID: 13109
		public static readonly ADPropertyDefinition AllowCrossSiteSessions = ADObject.BitfieldProperty("AllowCrossSiteSessions", 1, PopImapAdConfigurationSchema.PopImapProtocolFlags);

		// Token: 0x04003336 RID: 13110
		public static readonly ADPropertyDefinition EnforceCertificateErrors = ADObject.BitfieldProperty("EnforceCertificateErrors", 2, PopImapAdConfigurationSchema.PopImapProtocolFlags);

		// Token: 0x04003337 RID: 13111
		public static readonly ADPropertyDefinition UnencryptedOrTLSBindings = new ADPropertyDefinition("UnencryptedOrTLSBindings", ExchangeObjectVersion.Exchange2003, typeof(IPBinding), "msExchServerBindings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003338 RID: 13112
		public static readonly ADPropertyDefinition SSLBindings = new ADPropertyDefinition("SSLBindings", ExchangeObjectVersion.Exchange2003, typeof(IPBinding), "msExchSecureBindings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003339 RID: 13113
		public static readonly ADPropertyDefinition InternalConnectionSettings = new ADPropertyDefinition("InternalConnectionSettings", ExchangeObjectVersion.Exchange2010, typeof(ProtocolConnectionSettings), "msExchPOPIMAPInternalConnectionSettings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400333A RID: 13114
		public static readonly ADPropertyDefinition ExternalConnectionSettings = new ADPropertyDefinition("ExternalConnectionSettings", ExchangeObjectVersion.Exchange2010, typeof(ProtocolConnectionSettings), "msExchPOPIMAPExternalConnectionSettings", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400333B RID: 13115
		public static readonly ADPropertyDefinition X509CertificateName = new ADPropertyDefinition("X509CertificateName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchPopImapX509CertificateName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x0400333C RID: 13116
		public static readonly ADPropertyDefinition Banner = new ADPropertyDefinition("Banner", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchPopImapBanner", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new UIImpactStringLengthConstraint(0, 1024)
		}, null, null);

		// Token: 0x0400333D RID: 13117
		public static readonly ADPropertyDefinition LoginType = new ADPropertyDefinition("LoginType", ExchangeObjectVersion.Exchange2003, typeof(LoginOptions), "msExchAuthenticationFlags", ADPropertyDefinitionFlags.None, LoginOptions.SecureLogin, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400333E RID: 13118
		public static readonly ADPropertyDefinition AuthenticatedConnectionTimeout = new ADPropertyDefinition("AuthenticatedConnectionTimeout", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchIncomingConnectionTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(1800.0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(30.0), EnhancedTimeSpan.FromSeconds(86400.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x0400333F RID: 13119
		public static readonly ADPropertyDefinition PreAuthenticatedConnectionTimeout = new ADPropertyDefinition("PreAuthenticatedConnectionTimeout", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchPopImapIncomingPreauthConnectionTimeout", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(60.0), PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromSeconds(30.0), EnhancedTimeSpan.FromSeconds(3600.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04003340 RID: 13120
		public static readonly ADPropertyDefinition MaxConnections = new ADPropertyDefinition("MaxConnections", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchMaxIncomingConnections", ADPropertyDefinitionFlags.PersistDefaultValue, int.MaxValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04003341 RID: 13121
		public static readonly ADPropertyDefinition MaxConnectionFromSingleIP = new ADPropertyDefinition("MaxConnectionFromSingleIP", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPopImapMaxIncomingConnectionFromSingleSource", ADPropertyDefinitionFlags.PersistDefaultValue, int.MaxValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04003342 RID: 13122
		public static readonly ADPropertyDefinition MaxConnectionsPerUser = new ADPropertyDefinition("MaxConnectionsPerUser", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPopImapMaxIncomingConnectionPerUser", ADPropertyDefinitionFlags.PersistDefaultValue, 16, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04003343 RID: 13123
		public static readonly ADPropertyDefinition MessageRetrievalMimeFormat = new ADPropertyDefinition("MessageRetrievalMimeFormat", ExchangeObjectVersion.Exchange2007, typeof(MimeTextFormat), "contentType", ADPropertyDefinitionFlags.None, MimeTextFormat.BestBodyFormat, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003344 RID: 13124
		public static readonly ADPropertyDefinition ProxyTargetPort = new ADPropertyDefinition("ProxyTargetPort", ExchangeObjectVersion.Exchange2007, typeof(int), "portNumber", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 65535)
		}, null, null);

		// Token: 0x04003345 RID: 13125
		public static readonly ADPropertyDefinition CalendarItemRetrievalOption = new ADPropertyDefinition("CalendarItemRetrievalOption", ExchangeObjectVersion.Exchange2007, typeof(CalendarItemRetrievalOptions), "msExchPopImapCalendarItemRetrievalOption", ADPropertyDefinitionFlags.None, CalendarItemRetrievalOptions.iCalendar, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003346 RID: 13126
		public static readonly ADPropertyDefinition OwaServerUrl = new ADPropertyDefinition("OwaServerUrl", ExchangeObjectVersion.Exchange2007, typeof(Uri), "oWAServer", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04003347 RID: 13127
		public static readonly ADPropertyDefinition PopImapFlags = new ADPropertyDefinition("PopImapFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchPopImapFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04003348 RID: 13128
		public static readonly ADPropertyDefinition EnableExactRFC822Size = ADObject.BitfieldProperty("EnableExactRFC822Size", 1, PopImapAdConfigurationSchema.PopImapFlags);

		// Token: 0x04003349 RID: 13129
		public static readonly ADPropertyDefinition LiveIdBasicAuthReplacement = ADObject.BitfieldProperty("LiveIdBasicAuthReplacement", 2, PopImapAdConfigurationSchema.PopImapFlags);

		// Token: 0x0400334A RID: 13130
		public static readonly ADPropertyDefinition SuppressReadReceipt = ADObject.BitfieldProperty("SuppressReadReceipt", 4, PopImapAdConfigurationSchema.PopImapFlags);

		// Token: 0x0400334B RID: 13131
		public static readonly ADPropertyDefinition ExtendedProtectionPolicy = new ADPropertyDefinition("ExtendedProtectionPolicy", ExchangeObjectVersion.Exchange2007, typeof(ExtendedProtectionTokenCheckingMode), "msExchPopImapExtendedProtectionPolicy", ADPropertyDefinitionFlags.None, ExtendedProtectionTokenCheckingMode.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400334C RID: 13132
		public static readonly ADPropertyDefinition EnableGSSAPIAndNTLMAuth = ADObject.BitfieldProperty("EnableGSSAPIAndNTLMAuth", 8, PopImapAdConfigurationSchema.PopImapFlags);
	}
}
