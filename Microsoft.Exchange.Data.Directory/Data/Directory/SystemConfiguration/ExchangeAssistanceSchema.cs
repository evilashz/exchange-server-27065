using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000442 RID: 1090
	internal class ExchangeAssistanceSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002126 RID: 8486
		public static readonly ADPropertyDefinition ExchangeHelpAppOnline = new ADPropertyDefinition("ExchangeHelpAppOnline", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchExchangeHelpAppOnline", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002127 RID: 8487
		public static readonly ADPropertyDefinition ControlPanelHelpURL = new ADPropertyDefinition("ControlPanelHelpURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchControlPanelHelpURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002128 RID: 8488
		public static readonly ADPropertyDefinition ControlPanelFeedbackURL = new ADPropertyDefinition("ControlPanelFeedbackURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchControlPanelFeedbackURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002129 RID: 8489
		public static readonly ADPropertyDefinition ControlPanelFeedbackEnabled = new ADPropertyDefinition("ControlPanelFeedbackEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchControlPanelFeedbackEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400212A RID: 8490
		public static readonly ADPropertyDefinition ManagementConsoleHelpURL = new ADPropertyDefinition("ManagementConsoleHelpURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchManagementConsoleHelpURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x0400212B RID: 8491
		public static readonly ADPropertyDefinition ManagementConsoleFeedbackURL = new ADPropertyDefinition("ManagementConsoleFeedbackURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchManagementConsoleFeedbackURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x0400212C RID: 8492
		public static readonly ADPropertyDefinition ManagementConsoleFeedbackEnabled = new ADPropertyDefinition("ManagementConsoleFeedbackEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchManagementConsoleFeedbackEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400212D RID: 8493
		public static readonly ADPropertyDefinition OWAHelpURL = new ADPropertyDefinition("OWAHelpURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchOWAHelpURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x0400212E RID: 8494
		public static readonly ADPropertyDefinition OWAFeedbackURL = new ADPropertyDefinition("OWAFeedbackURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchOWAFeedbackURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x0400212F RID: 8495
		public static readonly ADPropertyDefinition OWAFeedbackEnabled = new ADPropertyDefinition("OWAFeedbackEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchOWAFeedbackEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002130 RID: 8496
		public static readonly ADPropertyDefinition OWALightHelpURL = new ADPropertyDefinition("OWALightHelpURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchOWALightHelpURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002131 RID: 8497
		public static readonly ADPropertyDefinition OWALightFeedbackURL = new ADPropertyDefinition("OWALightFeedbackURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchOWALightFeedbackURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002132 RID: 8498
		public static readonly ADPropertyDefinition OWALightFeedbackEnabled = new ADPropertyDefinition("OWALightFeedbackEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchOWALightFeedbackEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002133 RID: 8499
		public static readonly ADPropertyDefinition WindowsLiveAccountPageURL = new ADPropertyDefinition("WindowsLiveAccountPageURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchWindowsLiveAccountURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002134 RID: 8500
		public static readonly ADPropertyDefinition WindowsLiveAccountURLEnabled = new ADPropertyDefinition("WindowsLiveAccountURLEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchWindowsLiveAccountURLEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002135 RID: 8501
		public static readonly ADPropertyDefinition PrivacyStatementURL = new ADPropertyDefinition("PrivacyStatementURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchPrivacyStatementURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002136 RID: 8502
		public static readonly ADPropertyDefinition PrivacyLinkDisplayEnabled = new ADPropertyDefinition("PrivacyLinkDisplayEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchPrivacyStatementURLEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002137 RID: 8503
		public static readonly ADPropertyDefinition CommunityURL = new ADPropertyDefinition("CommunityURL", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchCommunityURL", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, null, null);

		// Token: 0x04002138 RID: 8504
		public static readonly ADPropertyDefinition CommunityLinkDisplayEnabled = new ADPropertyDefinition("CommunityLinkDisplayEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchCommunityURLEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
