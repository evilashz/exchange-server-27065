using System;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000013 RID: 19
	internal static class HttpProxySettings
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003995 File Offset: 0x00001B95
		public static string Prefix(string appSettingName)
		{
			return "HttpProxy." + appSettingName;
		}

		// Token: 0x0400006C RID: 108
		public static readonly BoolAppSettingsEntry CafeV2Enabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("CafeV2Enabled"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.CafeV2.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400006D RID: 109
		public static readonly BoolAppSettingsEntry AddressFinderEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("AddressFinderEnabled"), HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400006E RID: 110
		public static readonly bool AnonymousRequestFilterEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("AnonymousRequestFilterEnabled"), true, ExTraceGlobals.VerboseTracer).Value;

		// Token: 0x0400006F RID: 111
		public static readonly BoolAppSettingsEntry ProxyAssistantEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("ProxyAssistantEnabled"), HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000070 RID: 112
		public static readonly BoolAppSettingsEntry NativeProxyEnabled = new BoolAppSettingsEntry("NativeHttpProxy.Enabled", HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000071 RID: 113
		public static readonly BoolAppSettingsEntry RouteSelectorEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RouteSelectorEnabled"), HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000072 RID: 114
		public static readonly BoolAppSettingsEntry DiagnosticsEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("DiagnosticsEnabled"), HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000073 RID: 115
		public static readonly BoolAppSettingsEntry RouteRefresherEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RouteRefresherEnabled"), HttpProxySettings.CafeV2Enabled.Value, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000074 RID: 116
		public static readonly BoolAppSettingsEntry PartitionedRoutingEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("PartitionedRoutingEnabled"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.PartitionedRouting.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000075 RID: 117
		public static readonly EnumAppSettingsEntry<ProtocolType> ProtocolTypeAppSetting = new EnumAppSettingsEntry<ProtocolType>(HttpProxySettings.Prefix("ProtocolType"), ProtocolType.Unknown, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000076 RID: 118
		public static readonly BoolAppSettingsEntry OnlyProxySecureConnectionsAppSetting = new BoolAppSettingsEntry(HttpProxySettings.Prefix("OnlyProxySecureConnections"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000077 RID: 119
		public static readonly BoolAppSettingsEntry NegativeAnchorMailboxCacheEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("NegativeAnchorMailboxCacheEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000078 RID: 120
		public static readonly BoolAppSettingsEntry TrustClientXForwardedFor = new BoolAppSettingsEntry(HttpProxySettings.Prefix("TrustClientXForwardedFor"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.TrustClientXForwardedFor.Enabled, ExTraceGlobals.VerboseTracer);
	}
}
