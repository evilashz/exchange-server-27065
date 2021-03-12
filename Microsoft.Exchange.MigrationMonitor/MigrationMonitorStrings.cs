using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationMonitorStrings
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00008A28 File Offset: 0x00006C28
		static MigrationMonitorStrings()
		{
			MigrationMonitorStrings.stringIDs.Add(1815070528U, "ErrorLookUpServerId");
			MigrationMonitorStrings.stringIDs.Add(2311404879U, "ErrorUploadDatabaseInfoInBatch");
			MigrationMonitorStrings.stringIDs.Add(2787859741U, "ErrorUploadMrsAvailabilityLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(2842050225U, "ErrorUploadJobPickupResultsLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(779663980U, "ErrorUploadMrsLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(3175692275U, "ErrorUploadMigrationJobDataInBatch");
			MigrationMonitorStrings.stringIDs.Add(1616066425U, "ErrorUploadWLMResourceStatsLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(1288218702U, "ErrorUploadMigrationJobItemDataInBatch");
			MigrationMonitorStrings.stringIDs.Add(3126953968U, "ErrorUploadQueueStatsLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(2399146322U, "ErrorLookUpStringId");
			MigrationMonitorStrings.stringIDs.Add(2743222918U, "ErrorUploadMrsDrumTestingLogInBatch");
			MigrationMonitorStrings.stringIDs.Add(648032374U, "ErrorLookUpEndpointId");
			MigrationMonitorStrings.stringIDs.Add(774821795U, "ErrorLookUpWatsonId");
			MigrationMonitorStrings.stringIDs.Add(2500238689U, "ErrorUploadMailboxStatsInBatch");
			MigrationMonitorStrings.stringIDs.Add(2952266827U, "ErrorUploadMigrationEndpointDataInBatch");
			MigrationMonitorStrings.stringIDs.Add(4180397884U, "ErrorHealthStatusPublishFailureException");
			MigrationMonitorStrings.stringIDs.Add(2876186303U, "ErrorLookUpTenantId");
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008BB8 File Offset: 0x00006DB8
		public static LocalizedString ErrorLookUpServerId
		{
			get
			{
				return new LocalizedString("ErrorLookUpServerId", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00008BCF File Offset: 0x00006DCF
		public static LocalizedString ErrorUploadDatabaseInfoInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadDatabaseInfoInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public static LocalizedString ErrorLogFileRead(string fileName)
		{
			return new LocalizedString("ErrorLogFileRead", MigrationMonitorStrings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008C10 File Offset: 0x00006E10
		public static LocalizedString ErrorUploadMrsAvailabilityLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMrsAvailabilityLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008C27 File Offset: 0x00006E27
		public static LocalizedString ErrorUploadJobPickupResultsLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadJobPickupResultsLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00008C40 File Offset: 0x00006E40
		public static LocalizedString ErrorSqlServerUnreachableException(string connectionString)
		{
			return new LocalizedString("ErrorSqlServerUnreachableException", MigrationMonitorStrings.ResourceManager, new object[]
			{
				connectionString
			});
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008C68 File Offset: 0x00006E68
		public static LocalizedString ErrorDirectoryNotExist(string dirName)
		{
			return new LocalizedString("ErrorDirectoryNotExist", MigrationMonitorStrings.ResourceManager, new object[]
			{
				dirName
			});
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00008C90 File Offset: 0x00006E90
		public static LocalizedString ErrorUploadMrsLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMrsLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008CA8 File Offset: 0x00006EA8
		public static LocalizedString ErrorUnexpectedNullFromSproc(string msg)
		{
			return new LocalizedString("ErrorUnexpectedNullFromSproc", MigrationMonitorStrings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public static LocalizedString ErrorUploadMigrationJobDataInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMigrationJobDataInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008CE7 File Offset: 0x00006EE7
		public static LocalizedString ErrorUploadWLMResourceStatsLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadWLMResourceStatsLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00008CFE File Offset: 0x00006EFE
		public static LocalizedString ErrorUploadMigrationJobItemDataInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMigrationJobItemDataInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008D15 File Offset: 0x00006F15
		public static LocalizedString ErrorUploadQueueStatsLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadQueueStatsLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008D2C File Offset: 0x00006F2C
		public static LocalizedString ErrorLogFileLoad(string dirName)
		{
			return new LocalizedString("ErrorLogFileLoad", MigrationMonitorStrings.ResourceManager, new object[]
			{
				dirName
			});
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008D54 File Offset: 0x00006F54
		public static LocalizedString ErrorSqlConnectionString(string connection)
		{
			return new LocalizedString("ErrorSqlConnectionString", MigrationMonitorStrings.ResourceManager, new object[]
			{
				connection
			});
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008D7C File Offset: 0x00006F7C
		public static LocalizedString ErrorLookUpStringId
		{
			get
			{
				return new LocalizedString("ErrorLookUpStringId", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008D93 File Offset: 0x00006F93
		public static LocalizedString ErrorUploadMrsDrumTestingLogInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMrsDrumTestingLogInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008DAA File Offset: 0x00006FAA
		public static LocalizedString ErrorLookUpEndpointId
		{
			get
			{
				return new LocalizedString("ErrorLookUpEndpointId", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008DC4 File Offset: 0x00006FC4
		public static LocalizedString ErrorSqlServerTimeout(string sprocName)
		{
			return new LocalizedString("ErrorSqlServerTimeout", MigrationMonitorStrings.ResourceManager, new object[]
			{
				sprocName
			});
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008DEC File Offset: 0x00006FEC
		public static LocalizedString ErrorSqlQueryFailed(string sprocName)
		{
			return new LocalizedString("ErrorSqlQueryFailed", MigrationMonitorStrings.ResourceManager, new object[]
			{
				sprocName
			});
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008E14 File Offset: 0x00007014
		public static LocalizedString ErrorLookUpWatsonId
		{
			get
			{
				return new LocalizedString("ErrorLookUpWatsonId", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00008E2B File Offset: 0x0000702B
		public static LocalizedString ErrorUploadMailboxStatsInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMailboxStatsInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008E42 File Offset: 0x00007042
		public static LocalizedString ErrorUploadMigrationEndpointDataInBatch
		{
			get
			{
				return new LocalizedString("ErrorUploadMigrationEndpointDataInBatch", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008E59 File Offset: 0x00007059
		public static LocalizedString ErrorHealthStatusPublishFailureException
		{
			get
			{
				return new LocalizedString("ErrorHealthStatusPublishFailureException", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008E70 File Offset: 0x00007070
		public static LocalizedString ErrorLookUpTenantId
		{
			get
			{
				return new LocalizedString("ErrorLookUpTenantId", MigrationMonitorStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008E87 File Offset: 0x00007087
		public static LocalizedString GetLocalizedString(MigrationMonitorStrings.IDs key)
		{
			return new LocalizedString(MigrationMonitorStrings.stringIDs[(uint)key], MigrationMonitorStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400013E RID: 318
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(17);

		// Token: 0x0400013F RID: 319
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Servicelets.MigrationMonitor.Strings", typeof(MigrationMonitorStrings).GetTypeInfo().Assembly);

		// Token: 0x02000033 RID: 51
		public enum IDs : uint
		{
			// Token: 0x04000141 RID: 321
			ErrorLookUpServerId = 1815070528U,
			// Token: 0x04000142 RID: 322
			ErrorUploadDatabaseInfoInBatch = 2311404879U,
			// Token: 0x04000143 RID: 323
			ErrorUploadMrsAvailabilityLogInBatch = 2787859741U,
			// Token: 0x04000144 RID: 324
			ErrorUploadJobPickupResultsLogInBatch = 2842050225U,
			// Token: 0x04000145 RID: 325
			ErrorUploadMrsLogInBatch = 779663980U,
			// Token: 0x04000146 RID: 326
			ErrorUploadMigrationJobDataInBatch = 3175692275U,
			// Token: 0x04000147 RID: 327
			ErrorUploadWLMResourceStatsLogInBatch = 1616066425U,
			// Token: 0x04000148 RID: 328
			ErrorUploadMigrationJobItemDataInBatch = 1288218702U,
			// Token: 0x04000149 RID: 329
			ErrorUploadQueueStatsLogInBatch = 3126953968U,
			// Token: 0x0400014A RID: 330
			ErrorLookUpStringId = 2399146322U,
			// Token: 0x0400014B RID: 331
			ErrorUploadMrsDrumTestingLogInBatch = 2743222918U,
			// Token: 0x0400014C RID: 332
			ErrorLookUpEndpointId = 648032374U,
			// Token: 0x0400014D RID: 333
			ErrorLookUpWatsonId = 774821795U,
			// Token: 0x0400014E RID: 334
			ErrorUploadMailboxStatsInBatch = 2500238689U,
			// Token: 0x0400014F RID: 335
			ErrorUploadMigrationEndpointDataInBatch = 2952266827U,
			// Token: 0x04000150 RID: 336
			ErrorHealthStatusPublishFailureException = 4180397884U,
			// Token: 0x04000151 RID: 337
			ErrorLookUpTenantId = 2876186303U
		}

		// Token: 0x02000034 RID: 52
		private enum ParamIDs
		{
			// Token: 0x04000153 RID: 339
			ErrorLogFileRead,
			// Token: 0x04000154 RID: 340
			ErrorSqlServerUnreachableException,
			// Token: 0x04000155 RID: 341
			ErrorDirectoryNotExist,
			// Token: 0x04000156 RID: 342
			ErrorUnexpectedNullFromSproc,
			// Token: 0x04000157 RID: 343
			ErrorLogFileLoad,
			// Token: 0x04000158 RID: 344
			ErrorSqlConnectionString,
			// Token: 0x04000159 RID: 345
			ErrorSqlServerTimeout,
			// Token: 0x0400015A RID: 346
			ErrorSqlQueryFailed
		}
	}
}
