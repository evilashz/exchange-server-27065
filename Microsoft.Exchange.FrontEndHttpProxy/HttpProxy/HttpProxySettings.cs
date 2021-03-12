using System;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000035 RID: 53
	internal static class HttpProxySettings
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000957F File Offset: 0x0000777F
		public static string Prefix(string appSettingName)
		{
			return HttpProxySettings.Prefix(appSettingName);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009588 File Offset: 0x00007788
		private static int GetBufferBoundary(BufferPoolCollection.BufferSize bufferSize)
		{
			int num = 1024;
			string text = bufferSize.ToString();
			char c = text[text.Length - 1];
			if (c == 'M')
			{
				num *= 1024;
			}
			else if (c != 'K')
			{
				throw new ArgumentException(string.Format("BufferSize format for BufferSize value {0} is not supported", bufferSize));
			}
			return Convert.ToInt32(text.Substring(4, text.Length - 5)) * num;
		}

		// Token: 0x040000AE RID: 174
		public static readonly BoolAppSettingsEntry WriteDiagnosticHeaders = new BoolAppSettingsEntry(HttpProxySettings.Prefix("WriteDiagnosticHeaders"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000AF RID: 175
		public static readonly BoolAppSettingsEntry UseDefaultWebProxy = new BoolAppSettingsEntry(HttpProxySettings.Prefix("UseDefaultWebProxy"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B0 RID: 176
		public static readonly BoolAppSettingsEntry UseSmartBufferSizing = new BoolAppSettingsEntry(HttpProxySettings.Prefix("UseSmartBufferSizing"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B1 RID: 177
		public static readonly EnumAppSettingsEntry<BufferPoolCollection.BufferSize> RequestBufferSize = new EnumAppSettingsEntry<BufferPoolCollection.BufferSize>(HttpProxySettings.Prefix("RequestBufferSize"), BufferPoolCollection.BufferSize.Size32K, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B2 RID: 178
		public static readonly EnumAppSettingsEntry<BufferPoolCollection.BufferSize> MinimumRequestBufferSize = new EnumAppSettingsEntry<BufferPoolCollection.BufferSize>(HttpProxySettings.Prefix("MinimumRequestBufferSize"), BufferPoolCollection.BufferSize.Size4K, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B3 RID: 179
		public static readonly EnumAppSettingsEntry<BufferPoolCollection.BufferSize> ResponseBufferSize = new EnumAppSettingsEntry<BufferPoolCollection.BufferSize>(HttpProxySettings.Prefix("ResponseBufferSize"), BufferPoolCollection.BufferSize.Size32K, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B4 RID: 180
		public static readonly EnumAppSettingsEntry<BufferPoolCollection.BufferSize> MinimumResponseBufferSize = new EnumAppSettingsEntry<BufferPoolCollection.BufferSize>(HttpProxySettings.Prefix("MinimumResponseBufferSize"), BufferPoolCollection.BufferSize.Size4K, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B5 RID: 181
		public static readonly LazyMember<long> RequestBufferBoundary = new LazyMember<long>(() => (long)HttpProxySettings.GetBufferBoundary(HttpProxySettings.RequestBufferSize.Value));

		// Token: 0x040000B6 RID: 182
		public static readonly LazyMember<long> ResponseBufferBoundary = new LazyMember<long>(() => (long)HttpProxySettings.GetBufferBoundary(HttpProxySettings.ResponseBufferSize.Value));

		// Token: 0x040000B7 RID: 183
		public static readonly BoolAppSettingsEntry TestBackEndSupportEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("TestBackEndSupportEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B8 RID: 184
		public static readonly BoolAppSettingsEntry DFPOWAVdirProxyEnabled = new BoolAppSettingsEntry("DFPOWAProxyEnabled", false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000B9 RID: 185
		public static readonly StringAppSettingsEntry CaptureResponsesLocation = new StringAppSettingsEntry(HttpProxySettings.Prefix("CaptureResponsesLocation"), string.Empty, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BA RID: 186
		public static readonly IntAppSettingsEntry ServicePointConnectionLimit = new IntAppSettingsEntry(HttpProxySettings.Prefix("ServicePointConnectionLimit"), 65000, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BB RID: 187
		public static readonly IntAppSettingsEntry DelayProbeResponseSeconds = new IntAppSettingsEntry(HttpProxySettings.Prefix("DelayProbeResponseSeconds"), 0, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BC RID: 188
		public static readonly BoolAppSettingsEntry DetailedLatencyTracingEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("DetailedLatencyTracingEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BD RID: 189
		public static readonly IntAppSettingsEntry MaxRetryOnError = new IntAppSettingsEntry(HttpProxySettings.Prefix("MaxRetryOnError"), VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.RetryOnError.Enabled ? 2 : 0, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BE RID: 190
		public static readonly BoolAppSettingsEntry RetryOnConnectivityErrorEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RetryOnConnectivityErrorEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BF RID: 191
		public static readonly IntAppSettingsEntry DelayOnRetryOnError = new IntAppSettingsEntry(HttpProxySettings.Prefix("DelayOnRetryOnError"), 5000, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C0 RID: 192
		public static readonly BoolAppSettingsEntry MailboxServerLocatorSharedCacheEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("MailboxServerLocatorSharedCacheEnabled"), HttpProxyGlobals.IsMultitenant && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.MailboxServerSharedCache.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C1 RID: 193
		public static readonly StringAppSettingsEntry EnableLiveIdBasicBEAuthVersion = new StringAppSettingsEntry("LiveIdBasicAuthModule.EnableBEAuthVersion", string.Empty, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C2 RID: 194
		public static readonly StringAppSettingsEntry EnableOAuthBEAuthVersion = new StringAppSettingsEntry("OAuthHttpModule.EnableBEAuthVersion", string.Empty, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C3 RID: 195
		public static readonly BoolAppSettingsEntry AnchorMailboxSharedCacheEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxSharedCacheEnabled"), HttpProxyGlobals.IsMultitenant && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.AnchorMailboxSharedCache.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C4 RID: 196
		public static readonly IntAppSettingsEntry GlobalSharedCacheRpcTimeout = new IntAppSettingsEntry(HttpProxySettings.Prefix("GlobalSharedCacheRpcTimeout"), 2000, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C5 RID: 197
		public static readonly StringAppSettingsEntry EnableLiveIdCookieBEAuthVersion = new StringAppSettingsEntry("LiveIdCookieAuthModule.EnableBEAuthVersion", string.Empty, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C6 RID: 198
		public static readonly EnumAppSettingsEntry<ProxyRequestHandler.SupportBackEndCookie> SupportBackEndCookie = new EnumAppSettingsEntry<ProxyRequestHandler.SupportBackEndCookie>(HttpProxySettings.Prefix("SupportBackEndCookie"), VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.UseResourceForest.Enabled ? ProxyRequestHandler.SupportBackEndCookie.All : ProxyRequestHandler.SupportBackEndCookie.V1, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C7 RID: 199
		public static readonly BoolAppSettingsEntry CafeV1RUMEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("CafeV1RUMEnabled"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.CafeV1RUM.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000C8 RID: 200
		public static readonly StringAppSettingsEntry EnableRpsTokenBEAuthVersion = new StringAppSettingsEntry("RpsTokenAuthModule.EnableBEAuthVersion", string.Empty, ExTraceGlobals.VerboseTracer);
	}
}
