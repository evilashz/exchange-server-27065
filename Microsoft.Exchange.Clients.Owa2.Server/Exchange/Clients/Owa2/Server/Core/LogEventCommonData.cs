using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200009A RID: 154
	internal sealed class LogEventCommonData
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00010EFB File Offset: 0x0000F0FB
		internal static LogEventCommonData NullInstance
		{
			get
			{
				return new LogEventCommonData();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00010F02 File Offset: 0x0000F102
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00010F0A File Offset: 0x0000F10A
		internal string Platform { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00010F13 File Offset: 0x0000F113
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00010F1B File Offset: 0x0000F11B
		internal string DeviceModel { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00010F24 File Offset: 0x0000F124
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x00010F2C File Offset: 0x0000F12C
		internal string Browser { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00010F35 File Offset: 0x0000F135
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x00010F3D File Offset: 0x0000F13D
		internal string BrowserVersion { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00010F46 File Offset: 0x0000F146
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x00010F4E File Offset: 0x0000F14E
		internal string OperatingSystem { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00010F57 File Offset: 0x0000F157
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00010F5F File Offset: 0x0000F15F
		internal string OperatingSystemVersion { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00010F68 File Offset: 0x0000F168
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00010F70 File Offset: 0x0000F170
		internal string TenantDomain { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00010F79 File Offset: 0x0000F179
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00010F81 File Offset: 0x0000F181
		internal string Flights { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00010F8A File Offset: 0x0000F18A
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00010F92 File Offset: 0x0000F192
		internal string Features { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00010F9B File Offset: 0x0000F19B
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00010FA3 File Offset: 0x0000F1A3
		internal string Layout { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00010FAC File Offset: 0x0000F1AC
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		internal string Culture { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00010FBD File Offset: 0x0000F1BD
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00010FC5 File Offset: 0x0000F1C5
		internal string TimeZone { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00010FCE File Offset: 0x0000F1CE
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		internal Guid DatabaseGuid { get; private set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00010FDF File Offset: 0x0000F1DF
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00010FE7 File Offset: 0x0000F1E7
		internal bool ClientDataInitialized { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00010FF8 File Offset: 0x0000F1F8
		internal string OfflineEnabled { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00011001 File Offset: 0x0000F201
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00011009 File Offset: 0x0000F209
		internal string PalBuild { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00011012 File Offset: 0x0000F212
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0001101A File Offset: 0x0000F21A
		internal string ClientBuild { get; private set; }

		// Token: 0x060005E4 RID: 1508 RVA: 0x00011024 File Offset: 0x0000F224
		internal LogEventCommonData(UserContext userContext)
		{
			ArgumentValidator.ThrowIfNull("userContext cannot be null", userContext);
			if (userContext.FeaturesManager != null)
			{
				this.Flights = this.GetFlights(userContext.FeaturesManager);
				this.Features = this.GetFeatures(userContext.FeaturesManager);
			}
			if (userContext.ExchangePrincipal != null)
			{
				this.TenantDomain = this.GetTenantDomain(userContext.ExchangePrincipal);
				this.DatabaseGuid = userContext.ExchangePrincipal.MailboxInfo.GetDatabaseGuid();
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000110A0 File Offset: 0x0000F2A0
		private LogEventCommonData()
		{
			this.Flights = (this.Features = (this.TenantDomain = string.Empty));
			this.DatabaseGuid = Guid.Empty;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000110DC File Offset: 0x0000F2DC
		internal void UpdateClientData(Dictionary<string, object> sessionInfoData)
		{
			if (!this.ClientDataInitialized)
			{
				this.Platform = this.SafeGetStringValue(sessionInfoData, "pl");
				this.Browser = this.SafeGetStringValue(sessionInfoData, "brn");
				this.BrowserVersion = this.SafeGetStringValue(sessionInfoData, "brv");
				this.OperatingSystem = this.SafeGetStringValue(sessionInfoData, "osn");
				this.OperatingSystemVersion = this.SafeGetStringValue(sessionInfoData, "osv");
				this.DeviceModel = this.SafeGetStringValue(sessionInfoData, "dm");
				this.PalBuild = this.SafeGetStringValue(sessionInfoData, "pbld");
				this.ClientDataInitialized = true;
			}
			this.Layout = this.SafeGetStringValue(sessionInfoData, "l");
			this.Culture = this.SafeGetStringValue(sessionInfoData, "clg");
			this.TimeZone = this.SafeGetStringValue(sessionInfoData, "tz");
			this.OfflineEnabled = this.SafeGetStringValue(sessionInfoData, "oe");
			this.ClientBuild = this.SafeGetStringValue(sessionInfoData, "cbld");
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000111D4 File Offset: 0x0000F3D4
		private string SafeGetStringValue(Dictionary<string, object> sessionInfoData, string key)
		{
			object obj;
			if (!sessionInfoData.TryGetValue(key, out obj))
			{
				return null;
			}
			if (obj is string)
			{
				return (string)obj;
			}
			return null;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00011200 File Offset: 0x0000F400
		private string GetTenantDomain(ExchangePrincipal exchangePrincipal)
		{
			string result = string.Empty;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				try
				{
					result = SmtpAddress.Parse(exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()).Domain;
				}
				catch (FormatException)
				{
				}
			}
			return result;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00011274 File Offset: 0x0000F474
		private string GetFeatures(FeaturesManager featuresManager)
		{
			HashSet<string> enabledFlightedFeatures = featuresManager.GetEnabledFlightedFeatures(FlightedFeatureScope.Any);
			IOrderedEnumerable<string> values = from feature in enabledFlightedFeatures
			orderby feature
			select feature;
			return string.Join(",", values);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000112BC File Offset: 0x0000F4BC
		private string GetFlights(FeaturesManager featuresManager)
		{
			string result = null;
			if (featuresManager.ConfigurationSnapshot != null)
			{
				string[] flights = featuresManager.ConfigurationSnapshot.Flights;
				Array.Sort<string>(flights);
				result = string.Join(",", flights);
			}
			return result;
		}

		// Token: 0x04000337 RID: 823
		internal const string PlatformKey = "pl";

		// Token: 0x04000338 RID: 824
		internal const string DeviceModelKey = "dm";

		// Token: 0x04000339 RID: 825
		internal const string BrowserKey = "brn";

		// Token: 0x0400033A RID: 826
		internal const string BrowserVersionKey = "brv";

		// Token: 0x0400033B RID: 827
		internal const string OperatingSystemKey = "osn";

		// Token: 0x0400033C RID: 828
		internal const string OperatingSystemVersionKey = "osv";

		// Token: 0x0400033D RID: 829
		internal const string TenantDomainKey = "dom";

		// Token: 0x0400033E RID: 830
		internal const string ServicePlanKey = "sku";

		// Token: 0x0400033F RID: 831
		internal const string DatabaseGuidKey = "db";

		// Token: 0x04000340 RID: 832
		internal const string FeaturesKey = "ftr";

		// Token: 0x04000341 RID: 833
		internal const string FlightsKey = "flt";

		// Token: 0x04000342 RID: 834
		internal const string LayoutKey = "l";

		// Token: 0x04000343 RID: 835
		internal const string CultureKey = "clg";

		// Token: 0x04000344 RID: 836
		internal const string TimeZoneKey = "tz";

		// Token: 0x04000345 RID: 837
		internal const string OfflineEnabledKey = "oe";

		// Token: 0x04000346 RID: 838
		internal const string PalBuildKey = "pbld";

		// Token: 0x04000347 RID: 839
		internal const string ClientBuildKey = "cbld";
	}
}
