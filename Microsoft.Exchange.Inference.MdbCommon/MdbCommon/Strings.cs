using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000021 RID: 33
	internal static class Strings
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005318 File Offset: 0x00003518
		static Strings()
		{
			Strings.stringIDs.Add(3777091915U, "NestedDocumentCountZero");
			Strings.stringIDs.Add(823938420U, "MissingReceivedTime");
			Strings.stringIDs.Add(2612598579U, "InvalidDocumentInTrainingSet");
			Strings.stringIDs.Add(266918677U, "FailedToOpenActivityLog");
			Strings.stringIDs.Add(254348434U, "AbortOnProcessingRequested");
			Strings.stringIDs.Add(662878388U, "MissingConversationId");
			Strings.stringIDs.Add(2549017857U, "AdRecipientNotFound");
			Strings.stringIDs.Add(1492146775U, "InvalidAdRecipient");
			Strings.stringIDs.Add(1292225851U, "SaveWithNoItemError");
			Strings.stringIDs.Add(1800498867U, "MissingSender");
			Strings.stringIDs.Add(3003303052U, "MissingMailboxOwnerProperty");
			Strings.stringIDs.Add(752028432U, "NullDocumentProcessingContext");
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005444 File Offset: 0x00003644
		public static LocalizedString MissingDumpsterIdInFolderList(string mbx)
		{
			return new LocalizedString("MissingDumpsterIdInFolderList", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000546C File Offset: 0x0000366C
		public static LocalizedString NestedDocumentCountZero
		{
			get
			{
				return new LocalizedString("NestedDocumentCountZero", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005483 File Offset: 0x00003683
		public static LocalizedString MissingReceivedTime
		{
			get
			{
				return new LocalizedString("MissingReceivedTime", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000549A File Offset: 0x0000369A
		public static LocalizedString InvalidDocumentInTrainingSet
		{
			get
			{
				return new LocalizedString("InvalidDocumentInTrainingSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000054B4 File Offset: 0x000036B4
		public static LocalizedString MissingFolderId(string mbx)
		{
			return new LocalizedString("MissingFolderId", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000054DC File Offset: 0x000036DC
		public static LocalizedString FailedToOpenActivityLog
		{
			get
			{
				return new LocalizedString("FailedToOpenActivityLog", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000054F4 File Offset: 0x000036F4
		public static LocalizedString ConnectionToMailboxFailed(Guid mbxGuid)
		{
			return new LocalizedString("ConnectionToMailboxFailed", Strings.ResourceManager, new object[]
			{
				mbxGuid
			});
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005524 File Offset: 0x00003724
		public static LocalizedString SetPropertyFailed(string property)
		{
			return new LocalizedString("SetPropertyFailed", Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000554C File Offset: 0x0000374C
		public static LocalizedString AbortOnProcessingRequested
		{
			get
			{
				return new LocalizedString("AbortOnProcessingRequested", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005564 File Offset: 0x00003764
		public static LocalizedString MissingDeletedId(string mbx)
		{
			return new LocalizedString("MissingDeletedId", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000558C File Offset: 0x0000378C
		public static LocalizedString MissingConversationId
		{
			get
			{
				return new LocalizedString("MissingConversationId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000055A4 File Offset: 0x000037A4
		public static LocalizedString PropertyMappingFailed(string property)
		{
			return new LocalizedString("PropertyMappingFailed", Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000055CC File Offset: 0x000037CC
		public static LocalizedString AdRecipientNotFound
		{
			get
			{
				return new LocalizedString("AdRecipientNotFound", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000055E4 File Offset: 0x000037E4
		public static LocalizedString MissingDeletedIdInFolderList(string mbx)
		{
			return new LocalizedString("MissingDeletedIdInFolderList", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000560C File Offset: 0x0000380C
		public static LocalizedString GetPropertyAsStreamFailed(string property)
		{
			return new LocalizedString("GetPropertyAsStreamFailed", Strings.ResourceManager, new object[]
			{
				property
			});
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005634 File Offset: 0x00003834
		public static LocalizedString InvalidAdRecipient
		{
			get
			{
				return new LocalizedString("InvalidAdRecipient", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000564C File Offset: 0x0000384C
		public static LocalizedString MissingInboxId(string mbx)
		{
			return new LocalizedString("MissingInboxId", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005674 File Offset: 0x00003874
		public static LocalizedString MissingInboxIdInFolderList(string mbx)
		{
			return new LocalizedString("MissingInboxIdInFolderList", Strings.ResourceManager, new object[]
			{
				mbx
			});
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000569C File Offset: 0x0000389C
		public static LocalizedString SaveWithNoItemError
		{
			get
			{
				return new LocalizedString("SaveWithNoItemError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000056B3 File Offset: 0x000038B3
		public static LocalizedString MissingSender
		{
			get
			{
				return new LocalizedString("MissingSender", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000056CA File Offset: 0x000038CA
		public static LocalizedString MissingMailboxOwnerProperty
		{
			get
			{
				return new LocalizedString("MissingMailboxOwnerProperty", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000056E1 File Offset: 0x000038E1
		public static LocalizedString NullDocumentProcessingContext
		{
			get
			{
				return new LocalizedString("NullDocumentProcessingContext", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000056F8 File Offset: 0x000038F8
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000054 RID: 84
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(12);

		// Token: 0x04000055 RID: 85
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Inference.MdbCommon.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000022 RID: 34
		public enum IDs : uint
		{
			// Token: 0x04000057 RID: 87
			NestedDocumentCountZero = 3777091915U,
			// Token: 0x04000058 RID: 88
			MissingReceivedTime = 823938420U,
			// Token: 0x04000059 RID: 89
			InvalidDocumentInTrainingSet = 2612598579U,
			// Token: 0x0400005A RID: 90
			FailedToOpenActivityLog = 266918677U,
			// Token: 0x0400005B RID: 91
			AbortOnProcessingRequested = 254348434U,
			// Token: 0x0400005C RID: 92
			MissingConversationId = 662878388U,
			// Token: 0x0400005D RID: 93
			AdRecipientNotFound = 2549017857U,
			// Token: 0x0400005E RID: 94
			InvalidAdRecipient = 1492146775U,
			// Token: 0x0400005F RID: 95
			SaveWithNoItemError = 1292225851U,
			// Token: 0x04000060 RID: 96
			MissingSender = 1800498867U,
			// Token: 0x04000061 RID: 97
			MissingMailboxOwnerProperty = 3003303052U,
			// Token: 0x04000062 RID: 98
			NullDocumentProcessingContext = 752028432U
		}

		// Token: 0x02000023 RID: 35
		private enum ParamIDs
		{
			// Token: 0x04000064 RID: 100
			MissingDumpsterIdInFolderList,
			// Token: 0x04000065 RID: 101
			MissingFolderId,
			// Token: 0x04000066 RID: 102
			ConnectionToMailboxFailed,
			// Token: 0x04000067 RID: 103
			SetPropertyFailed,
			// Token: 0x04000068 RID: 104
			MissingDeletedId,
			// Token: 0x04000069 RID: 105
			PropertyMappingFailed,
			// Token: 0x0400006A RID: 106
			MissingDeletedIdInFolderList,
			// Token: 0x0400006B RID: 107
			GetPropertyAsStreamFailed,
			// Token: 0x0400006C RID: 108
			MissingInboxId,
			// Token: 0x0400006D RID: 109
			MissingInboxIdInFolderList
		}
	}
}
