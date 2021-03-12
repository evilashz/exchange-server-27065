using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EdgeSync.Common.Internal
{
	// Token: 0x02000057 RID: 87
	internal static class Strings
	{
		// Token: 0x0600022E RID: 558 RVA: 0x0000AA4C File Offset: 0x00008C4C
		static Strings()
		{
			Strings.stringIDs.Add(39187813U, "NoSubscriptions");
			Strings.stringIDs.Add(137354313U, "CertErrorDuringConnect");
			Strings.stringIDs.Add(1462089994U, "SeeEventLogForSyncFailures");
			Strings.stringIDs.Add(836513619U, "CannotGetAdminGroupsContainer");
			Strings.stringIDs.Add(380956288U, "ServersEnumerate");
			Strings.stringIDs.Add(468239512U, "CouldNotSaveHub");
			Strings.stringIDs.Add(1997022922U, "CannotGetOrgContainer");
			Strings.stringIDs.Add(2249628033U, "CannotGetLocalSite");
			Strings.stringIDs.Add(2143632979U, "UnableToLoadSD");
			Strings.stringIDs.Add(1724752590U, "EdgeNotFoundInDNS");
			Strings.stringIDs.Add(2846323109U, "LocalHubNotFound");
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000AB64 File Offset: 0x00008D64
		public static LocalizedString NoSubscriptions
		{
			get
			{
				return new LocalizedString("NoSubscriptions", "ExAFFA7E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AB84 File Offset: 0x00008D84
		public static LocalizedString EdgeSyncNormal(string service)
		{
			return new LocalizedString("EdgeSyncNormal", "Ex622C6F", false, true, Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public static LocalizedString CertErrorDuringConnect
		{
			get
			{
				return new LocalizedString("CertErrorDuringConnect", "Ex3A7E44", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000ABD4 File Offset: 0x00008DD4
		public static LocalizedString EdgeSyncServiceConfigNotFoundException(string site, string dn)
		{
			return new LocalizedString("EdgeSyncServiceConfigNotFoundException", "", false, false, Strings.ResourceManager, new object[]
			{
				site,
				dn
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AC08 File Offset: 0x00008E08
		public static LocalizedString ProviderNull(string name)
		{
			return new LocalizedString("ProviderNull", "Ex5F9541", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AC38 File Offset: 0x00008E38
		public static LocalizedString LoadedADAMObjectCount(int objectCount)
		{
			return new LocalizedString("LoadedADAMObjectCount", "Ex750831", false, true, Strings.ResourceManager, new object[]
			{
				objectCount
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public static LocalizedString EdgeSyncNotConfigured(string service)
		{
			return new LocalizedString("EdgeSyncNotConfigured", "Ex0C9239", false, true, Strings.ResourceManager, new object[]
			{
				service
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public static LocalizedString CouldNotCreateProvider(string name)
		{
			return new LocalizedString("CouldNotCreateProvider", "Ex5D9A4C", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000ACCB File Offset: 0x00008ECB
		public static LocalizedString SeeEventLogForSyncFailures
		{
			get
			{
				return new LocalizedString("SeeEventLogForSyncFailures", "ExE31A64", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		public static LocalizedString CannotGetAdminGroupsContainer
		{
			get
			{
				return new LocalizedString("CannotGetAdminGroupsContainer", "ExE6C23A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AD08 File Offset: 0x00008F08
		public static LocalizedString EdgeSyncFailedUrgent(string service, string taskName)
		{
			return new LocalizedString("EdgeSyncFailedUrgent", "", false, false, Strings.ResourceManager, new object[]
			{
				service,
				taskName
			});
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public static LocalizedString ServersEnumerate
		{
			get
			{
				return new LocalizedString("ServersEnumerate", "ExAA8622", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000AD59 File Offset: 0x00008F59
		public static LocalizedString CouldNotSaveHub
		{
			get
			{
				return new LocalizedString("CouldNotSaveHub", "Ex5E8A8B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000AD77 File Offset: 0x00008F77
		public static LocalizedString CannotGetOrgContainer
		{
			get
			{
				return new LocalizedString("CannotGetOrgContainer", "ExFC412D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000AD98 File Offset: 0x00008F98
		public static LocalizedString EdgeSyncFailed(string service, string taskName)
		{
			return new LocalizedString("EdgeSyncFailed", "Ex383E75", false, true, Strings.ResourceManager, new object[]
			{
				service,
				taskName
			});
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public static LocalizedString LoadedADObjectCount(int objectCount)
		{
			return new LocalizedString("LoadedADObjectCount", "ExFC8325", false, true, Strings.ResourceManager, new object[]
			{
				objectCount
			});
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000AE00 File Offset: 0x00009000
		public static LocalizedString CannotGetLocalSite
		{
			get
			{
				return new LocalizedString("CannotGetLocalSite", "Ex3C890F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AE20 File Offset: 0x00009020
		public static LocalizedString LoadingADAMComparisonList(string objectType, string serverName)
		{
			return new LocalizedString("LoadingADAMComparisonList", "ExA08E15", false, true, Strings.ResourceManager, new object[]
			{
				objectType,
				serverName
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AE54 File Offset: 0x00009054
		public static LocalizedString TargetEdgeNotFound(string server)
		{
			return new LocalizedString("TargetEdgeNotFound", "Ex5553EC", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000AE83 File Offset: 0x00009083
		public static LocalizedString UnableToLoadSD
		{
			get
			{
				return new LocalizedString("UnableToLoadSD", "ExCDE63F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public static LocalizedString InvalidProviderPath(string name, string path)
		{
			return new LocalizedString("InvalidProviderPath", "Ex55004B", false, true, Strings.ResourceManager, new object[]
			{
				name,
				path
			});
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000AED7 File Offset: 0x000090D7
		public static LocalizedString EdgeNotFoundInDNS
		{
			get
			{
				return new LocalizedString("EdgeNotFoundInDNS", "Ex2F4B16", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public static LocalizedString EdgeSyncAbnormal(string service, string taskName)
		{
			return new LocalizedString("EdgeSyncAbnormal", "Ex511308", false, true, Strings.ResourceManager, new object[]
			{
				service,
				taskName
			});
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AF2C File Offset: 0x0000912C
		public static LocalizedString NoCredentialsFound(string serverName)
		{
			return new LocalizedString("NoCredentialsFound", "Ex81B410", false, true, Strings.ResourceManager, new object[]
			{
				serverName
			});
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000AF5B File Offset: 0x0000915B
		public static LocalizedString LocalHubNotFound
		{
			get
			{
				return new LocalizedString("LocalHubNotFound", "ExE2E5D2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AF7C File Offset: 0x0000917C
		public static LocalizedString EdgeSyncInconclusive(string service, string taskName)
		{
			return new LocalizedString("EdgeSyncInconclusive", "ExFC6C9A", false, true, Strings.ResourceManager, new object[]
			{
				service,
				taskName
			});
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public static LocalizedString NoSiteAttributeFound(string servername)
		{
			return new LocalizedString("NoSiteAttributeFound", "Ex12C90A", false, true, Strings.ResourceManager, new object[]
			{
				servername
			});
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public static LocalizedString CannotLoadDefaultCertificate(string server)
		{
			return new LocalizedString("CannotLoadDefaultCertificate", "Ex2B8ADD", false, true, Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B010 File Offset: 0x00009210
		public static LocalizedString LoadingADComparisonList(string objectType)
		{
			return new LocalizedString("LoadingADComparisonList", "Ex70819E", false, true, Strings.ResourceManager, new object[]
			{
				objectType
			});
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B03F File Offset: 0x0000923F
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040001BF RID: 447
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(11);

		// Token: 0x040001C0 RID: 448
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.EdgeSync.Common.Internal.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000058 RID: 88
		public enum IDs : uint
		{
			// Token: 0x040001C2 RID: 450
			NoSubscriptions = 39187813U,
			// Token: 0x040001C3 RID: 451
			CertErrorDuringConnect = 137354313U,
			// Token: 0x040001C4 RID: 452
			SeeEventLogForSyncFailures = 1462089994U,
			// Token: 0x040001C5 RID: 453
			CannotGetAdminGroupsContainer = 836513619U,
			// Token: 0x040001C6 RID: 454
			ServersEnumerate = 380956288U,
			// Token: 0x040001C7 RID: 455
			CouldNotSaveHub = 468239512U,
			// Token: 0x040001C8 RID: 456
			CannotGetOrgContainer = 1997022922U,
			// Token: 0x040001C9 RID: 457
			CannotGetLocalSite = 2249628033U,
			// Token: 0x040001CA RID: 458
			UnableToLoadSD = 2143632979U,
			// Token: 0x040001CB RID: 459
			EdgeNotFoundInDNS = 1724752590U,
			// Token: 0x040001CC RID: 460
			LocalHubNotFound = 2846323109U
		}

		// Token: 0x02000059 RID: 89
		private enum ParamIDs
		{
			// Token: 0x040001CE RID: 462
			EdgeSyncNormal,
			// Token: 0x040001CF RID: 463
			EdgeSyncServiceConfigNotFoundException,
			// Token: 0x040001D0 RID: 464
			ProviderNull,
			// Token: 0x040001D1 RID: 465
			LoadedADAMObjectCount,
			// Token: 0x040001D2 RID: 466
			EdgeSyncNotConfigured,
			// Token: 0x040001D3 RID: 467
			CouldNotCreateProvider,
			// Token: 0x040001D4 RID: 468
			EdgeSyncFailedUrgent,
			// Token: 0x040001D5 RID: 469
			EdgeSyncFailed,
			// Token: 0x040001D6 RID: 470
			LoadedADObjectCount,
			// Token: 0x040001D7 RID: 471
			LoadingADAMComparisonList,
			// Token: 0x040001D8 RID: 472
			TargetEdgeNotFound,
			// Token: 0x040001D9 RID: 473
			InvalidProviderPath,
			// Token: 0x040001DA RID: 474
			EdgeSyncAbnormal,
			// Token: 0x040001DB RID: 475
			NoCredentialsFound,
			// Token: 0x040001DC RID: 476
			EdgeSyncInconclusive,
			// Token: 0x040001DD RID: 477
			NoSiteAttributeFound,
			// Token: 0x040001DE RID: 478
			CannotLoadDefaultCertificate,
			// Token: 0x040001DF RID: 479
			LoadingADComparisonList
		}
	}
}
