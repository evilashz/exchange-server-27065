using System;
using System.Globalization;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000051 RID: 81
	public class WacConfiguration
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00008E98 File Offset: 0x00007098
		private WacConfiguration()
		{
			this.BlockWacViewingThroughUI = new BoolAppSettingsEntry("BlockWacViewingThroughUI", false, ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.AccessTokenExpirationDuration = new TimeSpanAppSettingsEntry("WacAccessTokenExpirationInMinutes", TimeSpanUnit.Minutes, new TimeSpan(8, 0, 0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.AccessTokenCacheTime = new TimeSpanAppSettingsEntry("WacAccessTokenCacheTimeInMinutes", TimeSpanUnit.Minutes, new TimeSpan(0, 30, 0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.UseHttpsForWacUrl = new BoolAppSettingsEntry("UseHttpsForWacUrl", true, ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.DiscoveryDataRefreshInterval = new TimeSpanAppSettingsEntry("WacDiscoveryDataRefreshIntervalInMinutes", TimeSpanUnit.Minutes, TimeSpan.FromHours(8.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.DiscoveryDataRetrievalErrorBaseRefreshInterval = new TimeSpanAppSettingsEntry("WacDiscoveryDataRetrievalErrorBaseRefreshIntervalInMinutes", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(5.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.MdbCacheInterval = new TimeSpanAppSettingsEntry("WacMdbCacheIntervalInMinutes", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(30.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.CobaltStoreCleanupInterval = new TimeSpanAppSettingsEntry("WacCobaltStoreCleanupIntervalInHours", TimeSpanUnit.Hours, TimeSpan.FromHours(24.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.CobaltStoreExpirationInterval = new TimeSpanAppSettingsEntry("WacCobaltStoreExpirationIntervalInHours", TimeSpanUnit.Hours, TimeSpan.FromHours(12.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.AutoSaveInterval = new TimeSpanAppSettingsEntry("WacAutoSaveIntervalInSeconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(180.0), ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.DiagnosticsEnabled = new BoolAppSettingsEntry("WacDiagnosticsEnabled", false, ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.EditingEnabled = new BoolAppSettingsEntry("WacEditingEnabled", true, ExTraceGlobals.AttachmentHandlingTracer).Value;
			this.BlobStoreMemoryBudget = new IntAppSettingsEntry("WacCobaltBlobStoreMemoryBudget", 524288, ExTraceGlobals.AttachmentHandlingTracer).Value;
			if (this.BlockWacViewingThroughUI)
			{
				this.WacDiscoveryEndPoint = null;
				string text = "BlockWacViewingThroughUI is set to true";
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_WacConfigurationSetupSuccessful, string.Empty, new object[]
				{
					text
				});
				OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text));
				return;
			}
			string text2 = new StringAppSettingsEntry("WacUrlHostName", null, ExTraceGlobals.AttachmentHandlingTracer).Value;
			bool flag = false;
			if (string.IsNullOrEmpty(text2) && !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.WacConfigurationFromOrgConfig.Enabled)
			{
				flag = true;
				text2 = WacConfiguration.ReadFromOrganizationConfig();
			}
			if (string.IsNullOrEmpty(text2) || !Uri.TryCreate(text2, UriKind.Absolute, out this.WacDiscoveryEndPoint))
			{
				this.BlockWacViewingThroughUI = true;
				string text3 = string.Format("The WacUrlHostName was invalid. Expected a valid Uri. Actual value was '{0}'. Value read from '{1}'", text2, flag ? "OrganizationConfig" : "web.config");
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_WacConfigurationSetupError, string.Empty, new object[]
				{
					text3
				});
				OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text3));
				return;
			}
			string value = new StringAppSettingsEntry("WacDataCenterPrefix", null, ExTraceGlobals.AttachmentHandlingTracer).Value;
			if (!string.IsNullOrEmpty(value))
			{
				this.WacDiscoveryEndPoint = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}?dcPrefix={1}", new object[]
				{
					this.WacDiscoveryEndPoint,
					value
				}));
			}
			string text4 = string.Format("Wac enabled and configured with {0} as the endpoint. Value was read from {1}.", this.WacDiscoveryEndPoint, flag ? "OrganizationConfig" : "web.config");
			OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_WacConfigurationSetupSuccessful, string.Empty, new object[]
			{
				text4
			});
			text4 += string.Format(" DiagnosticsEnabled={0}, EditingEnabled={1}", this.DiagnosticsEnabled, this.EditingEnabled);
			OwaServerTraceLogger.AppendToLog(new WacAttachmentLogEvent(text4));
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000922C File Offset: 0x0000742C
		public static WacConfiguration Instance
		{
			get
			{
				if (WacConfiguration.soleInstance == null)
				{
					WacConfiguration wacConfiguration = new WacConfiguration();
					WacConfiguration.soleInstance = wacConfiguration;
				}
				return WacConfiguration.soleInstance;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009251 File Offset: 0x00007451
		public string WacUrlScheme
		{
			get
			{
				if (!this.UseHttpsForWacUrl)
				{
					return "http";
				}
				return "https";
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009268 File Offset: 0x00007468
		private static string ReadFromOrganizationConfig()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 321, "ReadFromOrganizationConfig", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\attachment\\WacConfiguration.cs");
			Organization orgContainer = topologyConfigurationSession.GetOrgContainer();
			return orgContainer.WACDiscoveryEndpoint;
		}

		// Token: 0x04000117 RID: 279
		public const int DefaultBlobStoreMemoryBudget = 524288;

		// Token: 0x04000118 RID: 280
		public readonly TimeSpan AccessTokenExpirationDuration;

		// Token: 0x04000119 RID: 281
		public readonly TimeSpan AccessTokenCacheTime;

		// Token: 0x0400011A RID: 282
		public readonly TimeSpan DiscoveryDataRefreshInterval;

		// Token: 0x0400011B RID: 283
		public readonly TimeSpan DiscoveryDataRetrievalErrorBaseRefreshInterval;

		// Token: 0x0400011C RID: 284
		public readonly TimeSpan MdbCacheInterval;

		// Token: 0x0400011D RID: 285
		public readonly TimeSpan AutoSaveInterval;

		// Token: 0x0400011E RID: 286
		public readonly TimeSpan CobaltStoreCleanupInterval;

		// Token: 0x0400011F RID: 287
		public readonly TimeSpan CobaltStoreExpirationInterval;

		// Token: 0x04000120 RID: 288
		public readonly bool BlockWacViewingThroughUI;

		// Token: 0x04000121 RID: 289
		public readonly bool UseHttpsForWacUrl;

		// Token: 0x04000122 RID: 290
		public readonly Uri WacDiscoveryEndPoint;

		// Token: 0x04000123 RID: 291
		public readonly bool DiagnosticsEnabled;

		// Token: 0x04000124 RID: 292
		public readonly bool EditingEnabled;

		// Token: 0x04000125 RID: 293
		public readonly int BlobStoreMemoryBudget;

		// Token: 0x04000126 RID: 294
		private static WacConfiguration soleInstance;
	}
}
