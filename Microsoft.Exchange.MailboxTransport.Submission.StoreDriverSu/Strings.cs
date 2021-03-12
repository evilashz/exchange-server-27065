using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200004B RID: 75
	internal static class Strings
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000E408 File Offset: 0x0000C608
		static Strings()
		{
			Strings.stringIDs.Add(2999405978U, "QuarantineLoadRegistryAccessFailed");
			Strings.stringIDs.Add(1182810610U, "descExplination");
			Strings.stringIDs.Add(204157539U, "MeetingForwardNotificationSubject");
			Strings.stringIDs.Add(2422042322U, "UnexpectedException");
			Strings.stringIDs.Add(3518739609U, "NoSubject");
			Strings.stringIDs.Add(1075460866U, "descCredit");
			Strings.stringIDs.Add(1630794581U, "descArialGreySmallFontTag");
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000E584 File Offset: 0x0000C784
		public static LocalizedString QuarantineLoadRegistryAccessFailed
		{
			get
			{
				return new LocalizedString("QuarantineLoadRegistryAccessFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000E59B File Offset: 0x0000C79B
		public static LocalizedString descExplination
		{
			get
			{
				return new LocalizedString("descExplination", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		public static LocalizedString MeetingForwardNotificationSubject
		{
			get
			{
				return new LocalizedString("MeetingForwardNotificationSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000E5C9 File Offset: 0x0000C7C9
		public static LocalizedString UnexpectedException
		{
			get
			{
				return new LocalizedString("UnexpectedException", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		public static LocalizedString NoSubject
		{
			get
			{
				return new LocalizedString("NoSubject", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000E5F7 File Offset: 0x0000C7F7
		public static LocalizedString descCredit
		{
			get
			{
				return new LocalizedString("descCredit", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000E60E File Offset: 0x0000C80E
		public static LocalizedString descArialGreySmallFontTag
		{
			get
			{
				return new LocalizedString("descArialGreySmallFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000E625 File Offset: 0x0000C825
		public static LocalizedString NoStartTime
		{
			get
			{
				return new LocalizedString("NoStartTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000E63C File Offset: 0x0000C83C
		public static LocalizedString descTahomaBlackMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaBlackMediumFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000E653 File Offset: 0x0000C853
		public static LocalizedString descMeetingTimeLabel
		{
			get
			{
				return new LocalizedString("descMeetingTimeLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000E66A File Offset: 0x0000C86A
		public static LocalizedString descTahomaGreyMediumFontTag
		{
			get
			{
				return new LocalizedString("descTahomaGreyMediumFontTag", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000E681 File Offset: 0x0000C881
		public static LocalizedString descTitle
		{
			get
			{
				return new LocalizedString("descTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000E698 File Offset: 0x0000C898
		public static LocalizedString descMeetingSubjectLabel
		{
			get
			{
				return new LocalizedString("descMeetingSubjectLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000E6AF File Offset: 0x0000C8AF
		public static LocalizedString descRecipientsLabel
		{
			get
			{
				return new LocalizedString("descRecipientsLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000E6C6 File Offset: 0x0000C8C6
		public static LocalizedString descTimeZoneInfo
		{
			get
			{
				return new LocalizedString("descTimeZoneInfo", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000E6DD File Offset: 0x0000C8DD
		public static LocalizedString PoisonMessageRegistryAccessFailed
		{
			get
			{
				return new LocalizedString("PoisonMessageRegistryAccessFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040001A2 RID: 418
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(16);

		// Token: 0x040001A3 RID: 419
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200004C RID: 76
		public enum IDs : uint
		{
			// Token: 0x040001A5 RID: 421
			QuarantineLoadRegistryAccessFailed = 2999405978U,
			// Token: 0x040001A6 RID: 422
			descExplination = 1182810610U,
			// Token: 0x040001A7 RID: 423
			MeetingForwardNotificationSubject = 204157539U,
			// Token: 0x040001A8 RID: 424
			UnexpectedException = 2422042322U,
			// Token: 0x040001A9 RID: 425
			NoSubject = 3518739609U,
			// Token: 0x040001AA RID: 426
			descCredit = 1075460866U,
			// Token: 0x040001AB RID: 427
			descArialGreySmallFontTag = 1630794581U,
			// Token: 0x040001AC RID: 428
			NoStartTime = 869936810U,
			// Token: 0x040001AD RID: 429
			descTahomaBlackMediumFontTag = 3779075844U,
			// Token: 0x040001AE RID: 430
			descMeetingTimeLabel = 445569887U,
			// Token: 0x040001AF RID: 431
			descTahomaGreyMediumFontTag = 3244013810U,
			// Token: 0x040001B0 RID: 432
			descTitle = 3488657717U,
			// Token: 0x040001B1 RID: 433
			descMeetingSubjectLabel = 3493819938U,
			// Token: 0x040001B2 RID: 434
			descRecipientsLabel = 3885561947U,
			// Token: 0x040001B3 RID: 435
			descTimeZoneInfo = 4274878084U,
			// Token: 0x040001B4 RID: 436
			PoisonMessageRegistryAccessFailed = 1004476481U
		}
	}
}
