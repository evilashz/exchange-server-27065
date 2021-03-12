using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000009 RID: 9
	internal static class Strings
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000038DC File Offset: 0x00001ADC
		static Strings()
		{
			Strings.stringIDs.Add(172990383U, "descSkipException");
			Strings.stringIDs.Add(1233540867U, "MailboxPublicFolderFilter");
			Strings.stringIDs.Add(145250360U, "descNo");
			Strings.stringIDs.Add(825595121U, "MailboxInaccessibleFilter");
			Strings.stringIDs.Add(214179510U, "MailboxArchiveFilter");
			Strings.stringIDs.Add(1711506384U, "descTransientException");
			Strings.stringIDs.Add(645888561U, "MailboxMoveDestinationFilter");
			Strings.stringIDs.Add(2834798506U, "MailboxNotUserFilter");
			Strings.stringIDs.Add(384543172U, "descDeadMailboxException");
			Strings.stringIDs.Add(1232667862U, "descYes");
			Strings.stringIDs.Add(2233046866U, "MailboxNoGuidFilter");
			Strings.stringIDs.Add(1323233535U, "MailboxNotInDirectoryFilter");
			Strings.stringIDs.Add(2547632703U, "descDisconnectedMailboxException");
			Strings.stringIDs.Add(2813894235U, "MailboxInDemandJobFilter");
			Strings.stringIDs.Add(1914410581U, "descInvalidLanguageMailboxException");
			Strings.stringIDs.Add(3450913809U, "descLargeNumberSkippedMailboxes");
			Strings.stringIDs.Add(3870763552U, "descMailboxOrDatabaseNotSpecified");
			Strings.stringIDs.Add(3426940714U, "descAmbiguousAliasMailboxException");
			Strings.stringIDs.Add(1802393217U, "driverName");
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003A94 File Offset: 0x00001C94
		public static LocalizedString descSkipException
		{
			get
			{
				return new LocalizedString("descSkipException", "Ex3DE87C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public static LocalizedString descUnknownAssistant(string assistantName)
		{
			return new LocalizedString("descUnknownAssistant", "Ex3696FC", false, true, Strings.ResourceManager, new object[]
			{
				assistantName
			});
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public static LocalizedString MailboxPublicFolderFilter
		{
			get
			{
				return new LocalizedString("MailboxPublicFolderFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003B01 File Offset: 0x00001D01
		public static LocalizedString descNo
		{
			get
			{
				return new LocalizedString("descNo", "ExA39D5F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003B1F File Offset: 0x00001D1F
		public static LocalizedString MailboxInaccessibleFilter
		{
			get
			{
				return new LocalizedString("MailboxInaccessibleFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003B3D File Offset: 0x00001D3D
		public static LocalizedString MailboxArchiveFilter
		{
			get
			{
				return new LocalizedString("MailboxArchiveFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003B5B File Offset: 0x00001D5B
		public static LocalizedString descTransientException
		{
			get
			{
				return new LocalizedString("descTransientException", "Ex51BE76", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003B79 File Offset: 0x00001D79
		public static LocalizedString MailboxMoveDestinationFilter
		{
			get
			{
				return new LocalizedString("MailboxMoveDestinationFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003B97 File Offset: 0x00001D97
		public static LocalizedString MailboxNotUserFilter
		{
			get
			{
				return new LocalizedString("MailboxNotUserFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003BB5 File Offset: 0x00001DB5
		public static LocalizedString descDeadMailboxException
		{
			get
			{
				return new LocalizedString("descDeadMailboxException", "Ex2FC4BE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003BD3 File Offset: 0x00001DD3
		public static LocalizedString descYes
		{
			get
			{
				return new LocalizedString("descYes", "ExC8EBAE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public static LocalizedString MailboxNoGuidFilter
		{
			get
			{
				return new LocalizedString("MailboxNoGuidFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003C0F File Offset: 0x00001E0F
		public static LocalizedString MailboxNotInDirectoryFilter
		{
			get
			{
				return new LocalizedString("MailboxNotInDirectoryFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003C30 File Offset: 0x00001E30
		public static LocalizedString descMissingSystemMailbox(string name)
		{
			return new LocalizedString("descMissingSystemMailbox", "Ex2C26A1", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003C5F File Offset: 0x00001E5F
		public static LocalizedString descDisconnectedMailboxException
		{
			get
			{
				return new LocalizedString("descDisconnectedMailboxException", "ExCF2CBB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003C80 File Offset: 0x00001E80
		public static LocalizedString descUnknownDatabase(string databaseId)
		{
			return new LocalizedString("descUnknownDatabase", "Ex6EF341", false, true, Strings.ResourceManager, new object[]
			{
				databaseId
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003CAF File Offset: 0x00001EAF
		public static LocalizedString MailboxInDemandJobFilter
		{
			get
			{
				return new LocalizedString("MailboxInDemandJobFilter", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003CCD File Offset: 0x00001ECD
		public static LocalizedString descInvalidLanguageMailboxException
		{
			get
			{
				return new LocalizedString("descInvalidLanguageMailboxException", "Ex7F0128", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003CEB File Offset: 0x00001EEB
		public static LocalizedString descLargeNumberSkippedMailboxes
		{
			get
			{
				return new LocalizedString("descLargeNumberSkippedMailboxes", "ExE7E5CA", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003D09 File Offset: 0x00001F09
		public static LocalizedString descMailboxOrDatabaseNotSpecified
		{
			get
			{
				return new LocalizedString("descMailboxOrDatabaseNotSpecified", "Ex00BAFD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003D27 File Offset: 0x00001F27
		public static LocalizedString descAmbiguousAliasMailboxException
		{
			get
			{
				return new LocalizedString("descAmbiguousAliasMailboxException", "Ex8F97A8", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003D45 File Offset: 0x00001F45
		public static LocalizedString driverName
		{
			get
			{
				return new LocalizedString("driverName", "Ex3B873A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003D63 File Offset: 0x00001F63
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000081 RID: 129
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(19);

		// Token: 0x04000082 RID: 130
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Assistants.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000A RID: 10
		public enum IDs : uint
		{
			// Token: 0x04000084 RID: 132
			descSkipException = 172990383U,
			// Token: 0x04000085 RID: 133
			MailboxPublicFolderFilter = 1233540867U,
			// Token: 0x04000086 RID: 134
			descNo = 145250360U,
			// Token: 0x04000087 RID: 135
			MailboxInaccessibleFilter = 825595121U,
			// Token: 0x04000088 RID: 136
			MailboxArchiveFilter = 214179510U,
			// Token: 0x04000089 RID: 137
			descTransientException = 1711506384U,
			// Token: 0x0400008A RID: 138
			MailboxMoveDestinationFilter = 645888561U,
			// Token: 0x0400008B RID: 139
			MailboxNotUserFilter = 2834798506U,
			// Token: 0x0400008C RID: 140
			descDeadMailboxException = 384543172U,
			// Token: 0x0400008D RID: 141
			descYes = 1232667862U,
			// Token: 0x0400008E RID: 142
			MailboxNoGuidFilter = 2233046866U,
			// Token: 0x0400008F RID: 143
			MailboxNotInDirectoryFilter = 1323233535U,
			// Token: 0x04000090 RID: 144
			descDisconnectedMailboxException = 2547632703U,
			// Token: 0x04000091 RID: 145
			MailboxInDemandJobFilter = 2813894235U,
			// Token: 0x04000092 RID: 146
			descInvalidLanguageMailboxException = 1914410581U,
			// Token: 0x04000093 RID: 147
			descLargeNumberSkippedMailboxes = 3450913809U,
			// Token: 0x04000094 RID: 148
			descMailboxOrDatabaseNotSpecified = 3870763552U,
			// Token: 0x04000095 RID: 149
			descAmbiguousAliasMailboxException = 3426940714U,
			// Token: 0x04000096 RID: 150
			driverName = 1802393217U
		}

		// Token: 0x0200000B RID: 11
		private enum ParamIDs
		{
			// Token: 0x04000098 RID: 152
			descUnknownAssistant,
			// Token: 0x04000099 RID: 153
			descMissingSystemMailbox,
			// Token: 0x0400009A RID: 154
			descUnknownDatabase
		}
	}
}
