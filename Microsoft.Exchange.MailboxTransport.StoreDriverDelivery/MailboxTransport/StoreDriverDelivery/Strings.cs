using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000051 RID: 81
	internal static class Strings
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		static Strings()
		{
			Strings.stringIDs.Add(475229721U, "AutoGroupRequestBody");
			Strings.stringIDs.Add(1743625299U, "Null");
			Strings.stringIDs.Add(1182810610U, "descExplination");
			Strings.stringIDs.Add(2422042322U, "UnexpectedException");
			Strings.stringIDs.Add(1075460866U, "descCredit");
			Strings.stringIDs.Add(1751084259U, "StoreDriverAgentTransientExceptionEmail");
			Strings.stringIDs.Add(1630794581U, "descArialGreySmallFontTag");
			Strings.stringIDs.Add(651309707U, "RetryException");
			Strings.stringIDs.Add(869936810U, "NoStartTime");
			Strings.stringIDs.Add(3779075844U, "descTahomaBlackMediumFontTag");
			Strings.stringIDs.Add(445569887U, "descMeetingTimeLabel");
			Strings.stringIDs.Add(3244013810U, "descTahomaGreyMediumFontTag");
			Strings.stringIDs.Add(3488657717U, "descTitle");
			Strings.stringIDs.Add(3493819938U, "descMeetingSubjectLabel");
			Strings.stringIDs.Add(3885561947U, "descRecipientsLabel");
			Strings.stringIDs.Add(4274878084U, "descTimeZoneInfo");
			Strings.stringIDs.Add(1004476481U, "PoisonMessageRegistryAccessFailed");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public static LocalizedString AutoGroupRequestHeader(string requestor, string group)
		{
			return new LocalizedString("AutoGroupRequestHeader", Strings.ResourceManager, new object[]
			{
				requestor,
				group
			});
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000ED28 File Offset: 0x0000CF28
		public static LocalizedString AutoGroupRequestBody
		{
			get
			{
				return new LocalizedString("AutoGroupRequestBody", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000ED40 File Offset: 0x0000CF40
		public static LocalizedString InboxRuleErrorInvalidGroup(string group)
		{
			return new LocalizedString("InboxRuleErrorInvalidGroup", Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public static LocalizedString Null
		{
			get
			{
				return new LocalizedString("Null", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		public static LocalizedString descExplination
		{
			get
			{
				return new LocalizedString("descExplination", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000ED96 File Offset: 0x0000CF96
		public static LocalizedString UnexpectedException
		{
			get
			{
				return new LocalizedString("UnexpectedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000EDAD File Offset: 0x0000CFAD
		public static LocalizedString descCredit
		{
			get
			{
				return new LocalizedString("descCredit", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		public static LocalizedString StoreDriverAgentTransientExceptionEmail
		{
			get
			{
				return new LocalizedString("StoreDriverAgentTransientExceptionEmail", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000EDDB File Offset: 0x0000CFDB
		public static LocalizedString descArialGreySmallFontTag
		{
			get
			{
				return new LocalizedString("descArialGreySmallFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		public static LocalizedString RetryException
		{
			get
			{
				return new LocalizedString("RetryException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000EE09 File Offset: 0x0000D009
		public static LocalizedString NoStartTime
		{
			get
			{
				return new LocalizedString("NoStartTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000EE20 File Offset: 0x0000D020
		public static LocalizedString descTahomaBlackMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaBlackMediumFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000EE37 File Offset: 0x0000D037
		public static LocalizedString descMeetingTimeLabel
		{
			get
			{
				return new LocalizedString("descMeetingTimeLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000EE4E File Offset: 0x0000D04E
		public static LocalizedString descTahomaGreyMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaGreyMediumFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000EE65 File Offset: 0x0000D065
		public static LocalizedString descTitle
		{
			get
			{
				return new LocalizedString("descTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public static LocalizedString descMeetingSubjectLabel
		{
			get
			{
				return new LocalizedString("descMeetingSubjectLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000EE93 File Offset: 0x0000D093
		public static LocalizedString descRecipientsLabel
		{
			get
			{
				return new LocalizedString("descRecipientsLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000EEAA File Offset: 0x0000D0AA
		public static LocalizedString descTimeZoneInfo
		{
			get
			{
				return new LocalizedString("descTimeZoneInfo", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
		public static LocalizedString PoisonMessageRegistryAccessFailed
		{
			get
			{
				return new LocalizedString("PoisonMessageRegistryAccessFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040001AB RID: 427
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(17);

		// Token: 0x040001AC RID: 428
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000052 RID: 82
		public enum IDs : uint
		{
			// Token: 0x040001AE RID: 430
			AutoGroupRequestBody = 475229721U,
			// Token: 0x040001AF RID: 431
			Null = 1743625299U,
			// Token: 0x040001B0 RID: 432
			descExplination = 1182810610U,
			// Token: 0x040001B1 RID: 433
			UnexpectedException = 2422042322U,
			// Token: 0x040001B2 RID: 434
			descCredit = 1075460866U,
			// Token: 0x040001B3 RID: 435
			StoreDriverAgentTransientExceptionEmail = 1751084259U,
			// Token: 0x040001B4 RID: 436
			descArialGreySmallFontTag = 1630794581U,
			// Token: 0x040001B5 RID: 437
			RetryException = 651309707U,
			// Token: 0x040001B6 RID: 438
			NoStartTime = 869936810U,
			// Token: 0x040001B7 RID: 439
			descTahomaBlackMediumFontTag = 3779075844U,
			// Token: 0x040001B8 RID: 440
			descMeetingTimeLabel = 445569887U,
			// Token: 0x040001B9 RID: 441
			descTahomaGreyMediumFontTag = 3244013810U,
			// Token: 0x040001BA RID: 442
			descTitle = 3488657717U,
			// Token: 0x040001BB RID: 443
			descMeetingSubjectLabel = 3493819938U,
			// Token: 0x040001BC RID: 444
			descRecipientsLabel = 3885561947U,
			// Token: 0x040001BD RID: 445
			descTimeZoneInfo = 4274878084U,
			// Token: 0x040001BE RID: 446
			PoisonMessageRegistryAccessFailed = 1004476481U
		}

		// Token: 0x02000053 RID: 83
		private enum ParamIDs
		{
			// Token: 0x040001C0 RID: 448
			AutoGroupRequestHeader,
			// Token: 0x040001C1 RID: 449
			InboxRuleErrorInvalidGroup
		}
	}
}
