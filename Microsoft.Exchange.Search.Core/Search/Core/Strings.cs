using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000BB RID: 187
	internal static class Strings
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x000125B8 File Offset: 0x000107B8
		static Strings()
		{
			Strings.stringIDs.Add(1350420100U, "IndexNotEnabled");
			Strings.stringIDs.Add(1862033255U, "EvaluationErrorsNoSupport");
			Strings.stringIDs.Add(2980931297U, "EvaluationErrorsAnnotationTokenError");
			Strings.stringIDs.Add(800591795U, "DocumentFailure");
			Strings.stringIDs.Add(3359953177U, "CatalogExcluded");
			Strings.stringIDs.Add(1803150643U, "AcceptedDomainRetrievalFailure");
			Strings.stringIDs.Add(3613842499U, "CatalogReseed");
			Strings.stringIDs.Add(4204989999U, "EvaluationErrorsMailboxLocked");
			Strings.stringIDs.Add(1780900744U, "CannotProcessDoc");
			Strings.stringIDs.Add(77711424U, "EvaluationErrorsMailboxOffline");
			Strings.stringIDs.Add(1144276406U, "EvaluationErrorsTextConversionFailure");
			Strings.stringIDs.Add(1946519939U, "ComponentFailure");
			Strings.stringIDs.Add(935218638U, "EvaluationErrorsTimeout");
			Strings.stringIDs.Add(2717588701U, "EvaluationErrorsAttachmentLimitReached");
			Strings.stringIDs.Add(2107982464U, "SeedingCatalog");
			Strings.stringIDs.Add(1663626573U, "EvaluationErrorsLoginFailed");
			Strings.stringIDs.Add(4239111926U, "EvaluationErrorsGenericError");
			Strings.stringIDs.Add(427251555U, "MapiNetworkError");
			Strings.stringIDs.Add(485396995U, "RpcEndpointFailedToRegister");
			Strings.stringIDs.Add(3251639695U, "IndexStatusTimestampTooOld");
			Strings.stringIDs.Add(498323155U, "EvaluationErrorsMailboxQuarantined");
			Strings.stringIDs.Add(1798012184U, "IndexStatusRegistryNotFound");
			Strings.stringIDs.Add(3214313948U, "CatalogCorruption");
			Strings.stringIDs.Add(3880225324U, "EvaluationErrorsStaleEvent");
			Strings.stringIDs.Add(2285368354U, "CatalogSuspended");
			Strings.stringIDs.Add(3376753831U, "ActivationPreferenceSkipped");
			Strings.stringIDs.Add(3459498499U, "LagCopySkipped");
			Strings.stringIDs.Add(3063759980U, "RecoveryDatabaseSkipped");
			Strings.stringIDs.Add(1388966758U, "ComponentCriticalFailure");
			Strings.stringIDs.Add(3194642979U, "InternalError");
			Strings.stringIDs.Add(1686554670U, "EvaluationErrorsMarsWriterTruncation");
			Strings.stringIDs.Add(4083887973U, "OperationFailure");
			Strings.stringIDs.Add(2780255030U, "CrawlingDatabase");
			Strings.stringIDs.Add(3710103617U, "EvaluationErrorsSessionUnavailable");
			Strings.stringIDs.Add(3704567193U, "EvaluationErrorsRightsManagementFailure");
			Strings.stringIDs.Add(584722949U, "EvaluationErrorsDocumentParserFailure");
			Strings.stringIDs.Add(1400615166U, "EvaluationErrorsPoisonDocument");
			Strings.stringIDs.Add(3608246202U, "DatabaseOffline");
			Strings.stringIDs.Add(2735780538U, "DocProcessCanceled");
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00012900 File Offset: 0x00010B00
		public static LocalizedString IndexNotEnabled
		{
			get
			{
				return new LocalizedString("IndexNotEnabled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012918 File Offset: 0x00010B18
		public static LocalizedString FastServiceNotRunning(string server)
		{
			return new LocalizedString("FastServiceNotRunning", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00012940 File Offset: 0x00010B40
		public static LocalizedString EvaluationErrorsNoSupport
		{
			get
			{
				return new LocalizedString("EvaluationErrorsNoSupport", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00012957 File Offset: 0x00010B57
		public static LocalizedString EvaluationErrorsAnnotationTokenError
		{
			get
			{
				return new LocalizedString("EvaluationErrorsAnnotationTokenError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0001296E File Offset: 0x00010B6E
		public static LocalizedString DocumentFailure
		{
			get
			{
				return new LocalizedString("DocumentFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00012985 File Offset: 0x00010B85
		public static LocalizedString CatalogExcluded
		{
			get
			{
				return new LocalizedString("CatalogExcluded", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0001299C File Offset: 0x00010B9C
		public static LocalizedString AcceptedDomainRetrievalFailure
		{
			get
			{
				return new LocalizedString("AcceptedDomainRetrievalFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000129B3 File Offset: 0x00010BB3
		public static LocalizedString CatalogReseed
		{
			get
			{
				return new LocalizedString("CatalogReseed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000129CA File Offset: 0x00010BCA
		public static LocalizedString EvaluationErrorsMailboxLocked
		{
			get
			{
				return new LocalizedString("EvaluationErrorsMailboxLocked", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000129E4 File Offset: 0x00010BE4
		public static LocalizedString IndexStatusException(string error)
		{
			return new LocalizedString("IndexStatusException", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00012A0C File Offset: 0x00010C0C
		public static LocalizedString CannotProcessDoc
		{
			get
			{
				return new LocalizedString("CannotProcessDoc", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00012A23 File Offset: 0x00010C23
		public static LocalizedString EvaluationErrorsMailboxOffline
		{
			get
			{
				return new LocalizedString("EvaluationErrorsMailboxOffline", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00012A3A File Offset: 0x00010C3A
		public static LocalizedString EvaluationErrorsTextConversionFailure
		{
			get
			{
				return new LocalizedString("EvaluationErrorsTextConversionFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00012A51 File Offset: 0x00010C51
		public static LocalizedString ComponentFailure
		{
			get
			{
				return new LocalizedString("ComponentFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00012A68 File Offset: 0x00010C68
		public static LocalizedString EvaluationErrorsTimeout
		{
			get
			{
				return new LocalizedString("EvaluationErrorsTimeout", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012A80 File Offset: 0x00010C80
		public static LocalizedString DocumentValidationFailure(string msg)
		{
			return new LocalizedString("DocumentValidationFailure", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00012AA8 File Offset: 0x00010CA8
		public static LocalizedString EvaluationErrorsAttachmentLimitReached
		{
			get
			{
				return new LocalizedString("EvaluationErrorsAttachmentLimitReached", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00012ABF File Offset: 0x00010CBF
		public static LocalizedString SeedingCatalog
		{
			get
			{
				return new LocalizedString("SeedingCatalog", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00012AD6 File Offset: 0x00010CD6
		public static LocalizedString EvaluationErrorsLoginFailed
		{
			get
			{
				return new LocalizedString("EvaluationErrorsLoginFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00012AED File Offset: 0x00010CED
		public static LocalizedString EvaluationErrorsGenericError
		{
			get
			{
				return new LocalizedString("EvaluationErrorsGenericError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x00012B04 File Offset: 0x00010D04
		public static LocalizedString MapiNetworkError
		{
			get
			{
				return new LocalizedString("MapiNetworkError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00012B1C File Offset: 0x00010D1C
		public static LocalizedString SearchServiceNotRunning(string server)
		{
			return new LocalizedString("SearchServiceNotRunning", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00012B44 File Offset: 0x00010D44
		public static LocalizedString RpcEndpointFailedToRegister
		{
			get
			{
				return new LocalizedString("RpcEndpointFailedToRegister", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00012B5B File Offset: 0x00010D5B
		public static LocalizedString IndexStatusTimestampTooOld
		{
			get
			{
				return new LocalizedString("IndexStatusTimestampTooOld", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00012B72 File Offset: 0x00010D72
		public static LocalizedString EvaluationErrorsMailboxQuarantined
		{
			get
			{
				return new LocalizedString("EvaluationErrorsMailboxQuarantined", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00012B89 File Offset: 0x00010D89
		public static LocalizedString IndexStatusRegistryNotFound
		{
			get
			{
				return new LocalizedString("IndexStatusRegistryNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00012BA0 File Offset: 0x00010DA0
		public static LocalizedString IndexStatusInvalidProperty(string property, string value)
		{
			return new LocalizedString("IndexStatusInvalidProperty", Strings.ResourceManager, new object[]
			{
				property,
				value
			});
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00012BCC File Offset: 0x00010DCC
		public static LocalizedString CatalogCorruption
		{
			get
			{
				return new LocalizedString("CatalogCorruption", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00012BE3 File Offset: 0x00010DE3
		public static LocalizedString EvaluationErrorsStaleEvent
		{
			get
			{
				return new LocalizedString("EvaluationErrorsStaleEvent", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00012BFA File Offset: 0x00010DFA
		public static LocalizedString CatalogSuspended
		{
			get
			{
				return new LocalizedString("CatalogSuspended", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00012C11 File Offset: 0x00010E11
		public static LocalizedString ActivationPreferenceSkipped
		{
			get
			{
				return new LocalizedString("ActivationPreferenceSkipped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00012C28 File Offset: 0x00010E28
		public static LocalizedString LagCopySkipped
		{
			get
			{
				return new LocalizedString("LagCopySkipped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00012C3F File Offset: 0x00010E3F
		public static LocalizedString RecoveryDatabaseSkipped
		{
			get
			{
				return new LocalizedString("RecoveryDatabaseSkipped", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00012C56 File Offset: 0x00010E56
		public static LocalizedString ComponentCriticalFailure
		{
			get
			{
				return new LocalizedString("ComponentCriticalFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00012C6D File Offset: 0x00010E6D
		public static LocalizedString InternalError
		{
			get
			{
				return new LocalizedString("InternalError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012C84 File Offset: 0x00010E84
		public static LocalizedString IndexStatusInvalid(string value)
		{
			return new LocalizedString("IndexStatusInvalid", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00012CAC File Offset: 0x00010EAC
		public static LocalizedString EvaluationErrorsMarsWriterTruncation
		{
			get
			{
				return new LocalizedString("EvaluationErrorsMarsWriterTruncation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00012CC3 File Offset: 0x00010EC3
		public static LocalizedString OperationFailure
		{
			get
			{
				return new LocalizedString("OperationFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00012CDA File Offset: 0x00010EDA
		public static LocalizedString CrawlingDatabase
		{
			get
			{
				return new LocalizedString("CrawlingDatabase", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00012CF1 File Offset: 0x00010EF1
		public static LocalizedString EvaluationErrorsSessionUnavailable
		{
			get
			{
				return new LocalizedString("EvaluationErrorsSessionUnavailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00012D08 File Offset: 0x00010F08
		public static LocalizedString EvaluationErrorsRightsManagementFailure
		{
			get
			{
				return new LocalizedString("EvaluationErrorsRightsManagementFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00012D1F File Offset: 0x00010F1F
		public static LocalizedString EvaluationErrorsDocumentParserFailure
		{
			get
			{
				return new LocalizedString("EvaluationErrorsDocumentParserFailure", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00012D36 File Offset: 0x00010F36
		public static LocalizedString EvaluationErrorsPoisonDocument
		{
			get
			{
				return new LocalizedString("EvaluationErrorsPoisonDocument", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00012D50 File Offset: 0x00010F50
		public static LocalizedString IndexStatusNotFound(string database)
		{
			return new LocalizedString("IndexStatusNotFound", Strings.ResourceManager, new object[]
			{
				database
			});
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00012D78 File Offset: 0x00010F78
		public static LocalizedString PropertyTypeError(string property)
		{
			return new LocalizedString("PropertyTypeError", Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00012DA0 File Offset: 0x00010FA0
		public static LocalizedString DatabaseOffline
		{
			get
			{
				return new LocalizedString("DatabaseOffline", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00012DB7 File Offset: 0x00010FB7
		public static LocalizedString DocProcessCanceled
		{
			get
			{
				return new LocalizedString("DocProcessCanceled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00012DD0 File Offset: 0x00010FD0
		public static LocalizedString PropertyError(string property)
		{
			return new LocalizedString("PropertyError", Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00012DF8 File Offset: 0x00010FF8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000296 RID: 662
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(39);

		// Token: 0x04000297 RID: 663
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Search.Core.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x020000BC RID: 188
		public enum IDs : uint
		{
			// Token: 0x04000299 RID: 665
			IndexNotEnabled = 1350420100U,
			// Token: 0x0400029A RID: 666
			EvaluationErrorsNoSupport = 1862033255U,
			// Token: 0x0400029B RID: 667
			EvaluationErrorsAnnotationTokenError = 2980931297U,
			// Token: 0x0400029C RID: 668
			DocumentFailure = 800591795U,
			// Token: 0x0400029D RID: 669
			CatalogExcluded = 3359953177U,
			// Token: 0x0400029E RID: 670
			AcceptedDomainRetrievalFailure = 1803150643U,
			// Token: 0x0400029F RID: 671
			CatalogReseed = 3613842499U,
			// Token: 0x040002A0 RID: 672
			EvaluationErrorsMailboxLocked = 4204989999U,
			// Token: 0x040002A1 RID: 673
			CannotProcessDoc = 1780900744U,
			// Token: 0x040002A2 RID: 674
			EvaluationErrorsMailboxOffline = 77711424U,
			// Token: 0x040002A3 RID: 675
			EvaluationErrorsTextConversionFailure = 1144276406U,
			// Token: 0x040002A4 RID: 676
			ComponentFailure = 1946519939U,
			// Token: 0x040002A5 RID: 677
			EvaluationErrorsTimeout = 935218638U,
			// Token: 0x040002A6 RID: 678
			EvaluationErrorsAttachmentLimitReached = 2717588701U,
			// Token: 0x040002A7 RID: 679
			SeedingCatalog = 2107982464U,
			// Token: 0x040002A8 RID: 680
			EvaluationErrorsLoginFailed = 1663626573U,
			// Token: 0x040002A9 RID: 681
			EvaluationErrorsGenericError = 4239111926U,
			// Token: 0x040002AA RID: 682
			MapiNetworkError = 427251555U,
			// Token: 0x040002AB RID: 683
			RpcEndpointFailedToRegister = 485396995U,
			// Token: 0x040002AC RID: 684
			IndexStatusTimestampTooOld = 3251639695U,
			// Token: 0x040002AD RID: 685
			EvaluationErrorsMailboxQuarantined = 498323155U,
			// Token: 0x040002AE RID: 686
			IndexStatusRegistryNotFound = 1798012184U,
			// Token: 0x040002AF RID: 687
			CatalogCorruption = 3214313948U,
			// Token: 0x040002B0 RID: 688
			EvaluationErrorsStaleEvent = 3880225324U,
			// Token: 0x040002B1 RID: 689
			CatalogSuspended = 2285368354U,
			// Token: 0x040002B2 RID: 690
			ActivationPreferenceSkipped = 3376753831U,
			// Token: 0x040002B3 RID: 691
			LagCopySkipped = 3459498499U,
			// Token: 0x040002B4 RID: 692
			RecoveryDatabaseSkipped = 3063759980U,
			// Token: 0x040002B5 RID: 693
			ComponentCriticalFailure = 1388966758U,
			// Token: 0x040002B6 RID: 694
			InternalError = 3194642979U,
			// Token: 0x040002B7 RID: 695
			EvaluationErrorsMarsWriterTruncation = 1686554670U,
			// Token: 0x040002B8 RID: 696
			OperationFailure = 4083887973U,
			// Token: 0x040002B9 RID: 697
			CrawlingDatabase = 2780255030U,
			// Token: 0x040002BA RID: 698
			EvaluationErrorsSessionUnavailable = 3710103617U,
			// Token: 0x040002BB RID: 699
			EvaluationErrorsRightsManagementFailure = 3704567193U,
			// Token: 0x040002BC RID: 700
			EvaluationErrorsDocumentParserFailure = 584722949U,
			// Token: 0x040002BD RID: 701
			EvaluationErrorsPoisonDocument = 1400615166U,
			// Token: 0x040002BE RID: 702
			DatabaseOffline = 3608246202U,
			// Token: 0x040002BF RID: 703
			DocProcessCanceled = 2735780538U
		}

		// Token: 0x020000BD RID: 189
		private enum ParamIDs
		{
			// Token: 0x040002C1 RID: 705
			FastServiceNotRunning,
			// Token: 0x040002C2 RID: 706
			IndexStatusException,
			// Token: 0x040002C3 RID: 707
			DocumentValidationFailure,
			// Token: 0x040002C4 RID: 708
			SearchServiceNotRunning,
			// Token: 0x040002C5 RID: 709
			IndexStatusInvalidProperty,
			// Token: 0x040002C6 RID: 710
			IndexStatusInvalid,
			// Token: 0x040002C7 RID: 711
			IndexStatusNotFound,
			// Token: 0x040002C8 RID: 712
			PropertyTypeError,
			// Token: 0x040002C9 RID: 713
			PropertyError
		}
	}
}
