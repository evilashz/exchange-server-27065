using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000034 RID: 52
	internal static class Strings
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		static Strings()
		{
			Strings.stringIDs.Add(330076707U, "DocumentProcessingFailed");
			Strings.stringIDs.Add(3296324356U, "FailedToOpenAdminRpcConnection");
			Strings.stringIDs.Add(1985214299U, "FailedToRegisterDatabaseChangeNotification");
			Strings.stringIDs.Add(2036468886U, "FailedToGetLocalServer");
			Strings.stringIDs.Add(151923492U, "FailedToGetMailboxDatabases");
			Strings.stringIDs.Add(3211562794U, "FailedToGetDatabasesContainerId");
			Strings.stringIDs.Add(3857521097U, "FailedToShutdownFeeder");
			Strings.stringIDs.Add(2230516668U, "InvalidDocument");
			Strings.stringIDs.Add(2968057743U, "FailedToCrawlMailbox");
			Strings.stringIDs.Add(3667120730U, "FailedToUnRegisterDatabaseChangeNotification");
			Strings.stringIDs.Add(3549756491U, "RetryFailed");
			Strings.stringIDs.Add(3018223464U, "ErrorAccessingStateStorage");
			Strings.stringIDs.Add(1020230779U, "AdOperationFailed");
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		public static LocalizedString DocumentProcessingFailed
		{
			get
			{
				return new LocalizedString("DocumentProcessingFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000CF10 File Offset: 0x0000B110
		public static LocalizedString FailedToGetActiveServer(Guid mdbGuid)
		{
			return new LocalizedString("FailedToGetActiveServer", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000CF3D File Offset: 0x0000B13D
		public static LocalizedString FailedToOpenAdminRpcConnection
		{
			get
			{
				return new LocalizedString("FailedToOpenAdminRpcConnection", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000CF54 File Offset: 0x0000B154
		public static LocalizedString MailboxLoginFailed(StoreSessionCacheKey key)
		{
			return new LocalizedString("MailboxLoginFailed", Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public static LocalizedString MdbMailboxQueryFailed(Guid mdbGuid)
		{
			return new LocalizedString("MdbMailboxQueryFailed", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public static LocalizedString FailedToReadNotifications(Guid mdbGuid)
		{
			return new LocalizedString("FailedToReadNotifications", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
		public static LocalizedString FailedToRegisterDatabaseChangeNotification
		{
			get
			{
				return new LocalizedString("FailedToRegisterDatabaseChangeNotification", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public static LocalizedString FailedToGetLocalServer
		{
			get
			{
				return new LocalizedString("FailedToGetLocalServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000D008 File Offset: 0x0000B208
		public static LocalizedString MailboxQuarantined(StoreSessionCacheKey key)
		{
			return new LocalizedString("MailboxQuarantined", Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000D030 File Offset: 0x0000B230
		public static LocalizedString FailedToGetServer(string fqdn)
		{
			return new LocalizedString("FailedToGetServer", Strings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000D058 File Offset: 0x0000B258
		public static LocalizedString FailedToGetMailboxDatabases
		{
			get
			{
				return new LocalizedString("FailedToGetMailboxDatabases", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000D06F File Offset: 0x0000B26F
		public static LocalizedString FailedToGetDatabasesContainerId
		{
			get
			{
				return new LocalizedString("FailedToGetDatabasesContainerId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D088 File Offset: 0x0000B288
		public static LocalizedString ConnectionToMailboxFailed(Guid mbxGuid)
		{
			return new LocalizedString("ConnectionToMailboxFailed", Strings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		public static LocalizedString ServerNotFound(string fqdn)
		{
			return new LocalizedString("ServerNotFound", Strings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		public static LocalizedString FailedToShutdownFeeder
		{
			get
			{
				return new LocalizedString("FailedToShutdownFeeder", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000D0F7 File Offset: 0x0000B2F7
		public static LocalizedString InvalidDocument
		{
			get
			{
				return new LocalizedString("InvalidDocument", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000D10E File Offset: 0x0000B30E
		public static LocalizedString FailedToCrawlMailbox
		{
			get
			{
				return new LocalizedString("FailedToCrawlMailbox", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000D125 File Offset: 0x0000B325
		public static LocalizedString FailedToUnRegisterDatabaseChangeNotification
		{
			get
			{
				return new LocalizedString("FailedToUnRegisterDatabaseChangeNotification", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000D13C File Offset: 0x0000B33C
		public static LocalizedString FailedCreateEventManager(Guid mdbGuid)
		{
			return new LocalizedString("FailedCreateEventManager", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000D169 File Offset: 0x0000B369
		public static LocalizedString RetryFailed
		{
			get
			{
				return new LocalizedString("RetryFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D180 File Offset: 0x0000B380
		public static LocalizedString ExceptionOccurred(Guid mdbGuid)
		{
			return new LocalizedString("ExceptionOccurred", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public static LocalizedString ErrorAccessingStateStorage
		{
			get
			{
				return new LocalizedString("ErrorAccessingStateStorage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static LocalizedString AdOperationFailed
		{
			get
			{
				return new LocalizedString("AdOperationFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public static LocalizedString FailedToFindSystemMailbox(Guid mdbGuid)
		{
			return new LocalizedString("FailedToFindSystemMailbox", Strings.ResourceManager, new object[]
			{
				mdbGuid
			});
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D20C File Offset: 0x0000B40C
		public static LocalizedString MailboxLocked(StoreSessionCacheKey key)
		{
			return new LocalizedString("MailboxLocked", Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D234 File Offset: 0x0000B434
		public static LocalizedString UnavailableSession(StoreSessionCacheKey key)
		{
			return new LocalizedString("UnavailableSession", Strings.ResourceManager, new object[]
			{
				key
			});
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D25C File Offset: 0x0000B45C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000117 RID: 279
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(13);

		// Token: 0x04000118 RID: 280
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Search.Mdb.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000035 RID: 53
		public enum IDs : uint
		{
			// Token: 0x0400011A RID: 282
			DocumentProcessingFailed = 330076707U,
			// Token: 0x0400011B RID: 283
			FailedToOpenAdminRpcConnection = 3296324356U,
			// Token: 0x0400011C RID: 284
			FailedToRegisterDatabaseChangeNotification = 1985214299U,
			// Token: 0x0400011D RID: 285
			FailedToGetLocalServer = 2036468886U,
			// Token: 0x0400011E RID: 286
			FailedToGetMailboxDatabases = 151923492U,
			// Token: 0x0400011F RID: 287
			FailedToGetDatabasesContainerId = 3211562794U,
			// Token: 0x04000120 RID: 288
			FailedToShutdownFeeder = 3857521097U,
			// Token: 0x04000121 RID: 289
			InvalidDocument = 2230516668U,
			// Token: 0x04000122 RID: 290
			FailedToCrawlMailbox = 2968057743U,
			// Token: 0x04000123 RID: 291
			FailedToUnRegisterDatabaseChangeNotification = 3667120730U,
			// Token: 0x04000124 RID: 292
			RetryFailed = 3549756491U,
			// Token: 0x04000125 RID: 293
			ErrorAccessingStateStorage = 3018223464U,
			// Token: 0x04000126 RID: 294
			AdOperationFailed = 1020230779U
		}

		// Token: 0x02000036 RID: 54
		private enum ParamIDs
		{
			// Token: 0x04000128 RID: 296
			FailedToGetActiveServer,
			// Token: 0x04000129 RID: 297
			MailboxLoginFailed,
			// Token: 0x0400012A RID: 298
			MdbMailboxQueryFailed,
			// Token: 0x0400012B RID: 299
			FailedToReadNotifications,
			// Token: 0x0400012C RID: 300
			MailboxQuarantined,
			// Token: 0x0400012D RID: 301
			FailedToGetServer,
			// Token: 0x0400012E RID: 302
			ConnectionToMailboxFailed,
			// Token: 0x0400012F RID: 303
			ServerNotFound,
			// Token: 0x04000130 RID: 304
			FailedCreateEventManager,
			// Token: 0x04000131 RID: 305
			ExceptionOccurred,
			// Token: 0x04000132 RID: 306
			FailedToFindSystemMailbox,
			// Token: 0x04000133 RID: 307
			MailboxLocked,
			// Token: 0x04000134 RID: 308
			UnavailableSession
		}
	}
}
