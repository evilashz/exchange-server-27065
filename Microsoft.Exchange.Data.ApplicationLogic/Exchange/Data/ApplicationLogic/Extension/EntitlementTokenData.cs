using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F3 RID: 243
	public class EntitlementTokenData
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x00026FF0 File Offset: 0x000251F0
		public EntitlementTokenData(string licensePurchaser, LicenseType licenseType, int seatsPurchased, DateTime etokenExpirationDate)
		{
			this.LicenseType = licenseType;
			this.LicensePurchaser = licensePurchaser;
			this.SeatsPurchased = seatsPurchased;
			this.EtokenExpirationDate = etokenExpirationDate;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00027015 File Offset: 0x00025215
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0002701D File Offset: 0x0002521D
		public string LicensePurchaser { get; private set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00027026 File Offset: 0x00025226
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0002702E File Offset: 0x0002522E
		public LicenseType LicenseType { get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00027037 File Offset: 0x00025237
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0002703F File Offset: 0x0002523F
		public int SeatsPurchased { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00027048 File Offset: 0x00025248
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00027050 File Offset: 0x00025250
		public DateTime EtokenExpirationDate { get; private set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002705C File Offset: 0x0002525C
		public bool IsRenewalNeeded
		{
			get
			{
				return this.EtokenExpirationDate < DateTime.UtcNow.AddDays(2.0);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002708A File Offset: 0x0002528A
		public bool IsExpired
		{
			get
			{
				return this.EtokenExpirationDate < DateTime.UtcNow;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002709C File Offset: 0x0002529C
		internal static void UpdatePaidAppSourceLocation(string urlElementName, ExtensionData extensionData)
		{
			if (string.IsNullOrWhiteSpace(extensionData.Etoken))
			{
				return;
			}
			Exception ex;
			extensionData.TryUpdateSourceLocation(null, urlElementName, out ex, new ExtensionDataHelper.TryModifySourceLocationDelegate(EntitlementTokenData.TryModifySourceLocation));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000270D0 File Offset: 0x000252D0
		private static bool TryModifySourceLocation(IExchangePrincipal exchangePrincipal, XmlAttribute xmlAttribute, ExtensionData extensionData, out Exception exception)
		{
			exception = null;
			if (string.IsNullOrWhiteSpace(xmlAttribute.Value))
			{
				return false;
			}
			string str = (xmlAttribute.Value.IndexOf('?') > 0) ? "&" : "?";
			xmlAttribute.Value = xmlAttribute.Value + str + "et=" + extensionData.Etoken;
			return true;
		}

		// Token: 0x040004BA RID: 1210
		internal const string ScenarioProcessEntitlementToken = "ProcessEntitlementToken";

		// Token: 0x040004BB RID: 1211
		internal const string TokenResponseTagName = "r";

		// Token: 0x040004BC RID: 1212
		internal const string TokenTagName = "t";

		// Token: 0x040004BD RID: 1213
		internal const string AssetIdTagName = "aid";

		// Token: 0x040004BE RID: 1214
		internal const string LicensePurchaserTagName = "cid";

		// Token: 0x040004BF RID: 1215
		internal const string SiteLicenseTagName = "sl";

		// Token: 0x040004C0 RID: 1216
		internal const string DeploymentIdTagName = "did";

		// Token: 0x040004C1 RID: 1217
		internal const string EtokenExpirationDateTagName = "te";

		// Token: 0x040004C2 RID: 1218
		internal const string SeatsPurchasedTagName = "ts";

		// Token: 0x040004C3 RID: 1219
		internal const string LicenseTypeTagName = "et";

		// Token: 0x040004C4 RID: 1220
		internal const string EtokenParameter = "et=";

		// Token: 0x040004C5 RID: 1221
		internal const int DaysToRenewBeforeExpiry = 2;
	}
}
