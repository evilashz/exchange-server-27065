using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000037 RID: 55
	internal static class Strings
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		static Strings()
		{
			Strings.stringIDs.Add(3963885811U, "AccessDenied");
			Strings.stringIDs.Add(4235889287U, "DiscoveryTimeoutOrCancelled");
			Strings.stringIDs.Add(2768002519U, "LocalServerNotInSiteVerbose");
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000EE60 File Offset: 0x0000D060
		public static LocalizedString ErrorFlushingKerberosTicketForAccount(string account)
		{
			return new LocalizedString("ErrorFlushingKerberosTicketForAccount", Strings.ResourceManager, new object[]
			{
				account
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000EE88 File Offset: 0x0000D088
		public static LocalizedString NoSuitableDirectoryServersInSite(string forest, string site)
		{
			return new LocalizedString("NoSuitableDirectoryServersInSite", Strings.ResourceManager, new object[]
			{
				forest,
				site
			});
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public static LocalizedString ErrorAdjustingPrivileges(string error)
		{
			return new LocalizedString("ErrorAdjustingPrivileges", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		public static LocalizedString NoDirectoryServersInForestError(string forest)
		{
			return new LocalizedString("NoDirectoryServersInForestError", Strings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000EF04 File Offset: 0x0000D104
		public static LocalizedString NoRequiredSuitableDirectoryServersInSiteAndConnectedSites(string forest, string site)
		{
			return new LocalizedString("NoRequiredSuitableDirectoryServersInSiteAndConnectedSites", Strings.ResourceManager, new object[]
			{
				forest,
				site
			});
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000EF30 File Offset: 0x0000D130
		public static LocalizedString AccessDenied
		{
			get
			{
				return new LocalizedString("AccessDenied", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000EF47 File Offset: 0x0000D147
		public static LocalizedString DiscoveryTimeoutOrCancelled
		{
			get
			{
				return new LocalizedString("DiscoveryTimeoutOrCancelled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000EF5E File Offset: 0x0000D15E
		public static LocalizedString LocalServerNotInSiteVerbose
		{
			get
			{
				return new LocalizedString("LocalServerNotInSiteVerbose", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000EF78 File Offset: 0x0000D178
		public static LocalizedString ForestNotFoundOrNotDiscovered(string forest)
		{
			return new LocalizedString("ForestNotFoundOrNotDiscovered", Strings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public static LocalizedString ForestDiscoveryTimeout(string forest, double seconds)
		{
			return new LocalizedString("ForestDiscoveryTimeout", Strings.ResourceManager, new object[]
			{
				forest,
				seconds
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public static LocalizedString ComputerNameNotFoundInAD(string computerName)
		{
			return new LocalizedString("ComputerNameNotFoundInAD", Strings.ResourceManager, new object[]
			{
				computerName
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public static LocalizedString NoSitesInForest(string forest)
		{
			return new LocalizedString("NoSitesInForest", Strings.ResourceManager, new object[]
			{
				forest
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000F024 File Offset: 0x0000D224
		public static LocalizedString NoSuitableDirectoryServersInSiteAndConnectedSites(string forest, string site)
		{
			return new LocalizedString("NoSuitableDirectoryServersInSiteAndConnectedSites", Strings.ResourceManager, new object[]
			{
				forest,
				site
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000F050 File Offset: 0x0000D250
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000179 RID: 377
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x0400017A RID: 378
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Directory.TopologyService.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000038 RID: 56
		public enum IDs : uint
		{
			// Token: 0x0400017C RID: 380
			AccessDenied = 3963885811U,
			// Token: 0x0400017D RID: 381
			DiscoveryTimeoutOrCancelled = 4235889287U,
			// Token: 0x0400017E RID: 382
			LocalServerNotInSiteVerbose = 2768002519U
		}

		// Token: 0x02000039 RID: 57
		private enum ParamIDs
		{
			// Token: 0x04000180 RID: 384
			ErrorFlushingKerberosTicketForAccount,
			// Token: 0x04000181 RID: 385
			NoSuitableDirectoryServersInSite,
			// Token: 0x04000182 RID: 386
			ErrorAdjustingPrivileges,
			// Token: 0x04000183 RID: 387
			NoDirectoryServersInForestError,
			// Token: 0x04000184 RID: 388
			NoRequiredSuitableDirectoryServersInSiteAndConnectedSites,
			// Token: 0x04000185 RID: 389
			ForestNotFoundOrNotDiscovered,
			// Token: 0x04000186 RID: 390
			ForestDiscoveryTimeout,
			// Token: 0x04000187 RID: 391
			ComputerNameNotFoundInAD,
			// Token: 0x04000188 RID: 392
			NoSitesInForest,
			// Token: 0x04000189 RID: 393
			NoSuitableDirectoryServersInSiteAndConnectedSites
		}
	}
}
