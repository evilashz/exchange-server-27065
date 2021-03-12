using System;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A6 RID: 166
	[Serializable]
	public class OwaStoreObjectId : ObjectId
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x00013DD6 File Offset: 0x00011FD6
		private OwaStoreObjectId(ConversationId conversationId, StoreObjectId folderId, byte[] instanceKey)
		{
			this.objectIdType = OwaStoreObjectIdType.Conversation;
			this.storeObjectId = conversationId;
			this.containerFolderId = folderId;
			this.instanceKey = instanceKey;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00013DFA File Offset: 0x00011FFA
		private OwaStoreObjectId(ConversationId conversationId, StoreObjectId folderId, string mailboxOwnerLegacyDN, byte[] instanceKey)
		{
			this.objectIdType = OwaStoreObjectIdType.ArchiveConversation;
			this.storeObjectId = conversationId;
			this.containerFolderId = folderId;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
			this.instanceKey = instanceKey;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00013E26 File Offset: 0x00012026
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId) : this(objectIdType, storeObjectId, null, null)
		{
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00013E32 File Offset: 0x00012032
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, string mailboxOwnerLegacyDN) : this(objectIdType, storeObjectId, null, mailboxOwnerLegacyDN)
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00013E3E File Offset: 0x0001203E
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, StoreObjectId containerFolderId) : this(objectIdType, storeObjectId, containerFolderId, null)
		{
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00013E4A File Offset: 0x0001204A
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, StoreObjectId containerFolderId, string mailboxOwnerLegacyDN)
		{
			this.objectIdType = objectIdType;
			this.storeObjectId = storeObjectId;
			this.containerFolderId = containerFolderId;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00013E6F File Offset: 0x0001206F
		internal ConversationId ConversationId
		{
			get
			{
				return this.storeObjectId as ConversationId;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00013E7C File Offset: 0x0001207C
		internal byte[] InstanceKey
		{
			get
			{
				return this.instanceKey;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00013E84 File Offset: 0x00012084
		internal StoreId StoreId
		{
			get
			{
				return this.storeObjectId;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00013E8C File Offset: 0x0001208C
		internal StoreObjectId StoreObjectId
		{
			get
			{
				if (this.IsGSCalendar)
				{
					throw new OwaInvalidOperationException("GS calendar doesn't has a store object id.");
				}
				return this.storeObjectId as StoreObjectId;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00013EAC File Offset: 0x000120AC
		internal bool IsStoreObjectId
		{
			get
			{
				return this.storeObjectId is StoreObjectId;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00013EBC File Offset: 0x000120BC
		internal bool IsConversationId
		{
			get
			{
				return this.storeObjectId is ConversationId;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00013ECC File Offset: 0x000120CC
		internal StoreObjectType StoreObjectType
		{
			get
			{
				StoreObjectId storeObjectId = this.storeObjectId as StoreObjectId;
				if (storeObjectId != null)
				{
					return storeObjectId.ObjectType;
				}
				return StoreObjectType.Unknown;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00013EF0 File Offset: 0x000120F0
		internal OwaStoreObjectIdType OwaStoreObjectIdType
		{
			get
			{
				return this.objectIdType;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00013EF8 File Offset: 0x000120F8
		internal string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00013F00 File Offset: 0x00012100
		internal bool IsPublic
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.PublicStoreFolder || this.objectIdType == OwaStoreObjectIdType.PublicStoreItem;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00013F16 File Offset: 0x00012116
		internal bool IsOtherMailbox
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.OtherUserMailboxObject;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00013F21 File Offset: 0x00012121
		internal bool IsGSCalendar
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.GSCalendar;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00013F2C File Offset: 0x0001212C
		internal bool IsArchive
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.ArchiveConversation || this.objectIdType == OwaStoreObjectIdType.ArchiveMailboxObject;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00013F44 File Offset: 0x00012144
		internal OwaStoreObjectId ProviderLevelItemId
		{
			get
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(this.StoreObjectId.ProviderLevelItemId);
				return new OwaStoreObjectId(this.objectIdType, storeObjectId, this.containerFolderId, this.mailboxOwnerLegacyDN);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00013F7A File Offset: 0x0001217A
		internal StoreObjectId ParentFolderId
		{
			get
			{
				return this.containerFolderId;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00013F84 File Offset: 0x00012184
		private string NormalizedContainerFolderIdBase64String
		{
			get
			{
				byte[] bytes = this.containerFolderId.GetBytes();
				bytes[bytes.Length - 1] = 1;
				return Convert.ToBase64String(bytes);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00013FAC File Offset: 0x000121AC
		private string NormalizedMailboxOwnerLegacyDnBase64String
		{
			get
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.mailboxOwnerLegacyDN);
				return Convert.ToBase64String(bytes);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00013FD0 File Offset: 0x000121D0
		public override byte[] GetBytes()
		{
			if (this.IsGSCalendar)
			{
				return Encoding.Unicode.GetBytes(this.MailboxOwnerLegacyDN);
			}
			return this.StoreId.GetBytes();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00013FF8 File Offset: 0x000121F8
		internal static StoreObjectId[] ConvertToStoreObjectIdArray(params OwaStoreObjectId[] owaStoreObjectIds)
		{
			if (owaStoreObjectIds == null)
			{
				throw new ArgumentNullException("owaStoreObjectIds");
			}
			StoreObjectId[] array = new StoreObjectId[owaStoreObjectIds.Length];
			for (int i = 0; i < owaStoreObjectIds.Length; i++)
			{
				array[i] = owaStoreObjectIds[i].StoreObjectId;
			}
			return array;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00014038 File Offset: 0x00012238
		internal static OwaStoreObjectId CreateFromString(string owaStoreObjectIdString)
		{
			OwaStoreObjectIdType owaStoreObjectIdType = OwaStoreObjectIdType.MailBoxObject;
			if (owaStoreObjectIdString.StartsWith("PSF.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreFolder;
			}
			else if (owaStoreObjectIdString.StartsWith("PSI.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreItem;
			}
			else if (owaStoreObjectIdString.StartsWith("OUM.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
			}
			else if (owaStoreObjectIdString.StartsWith("CID.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.Conversation;
			}
			else if (owaStoreObjectIdString.StartsWith("AMB.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
			}
			else if (owaStoreObjectIdString.StartsWith("ACI.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveConversation;
			}
			else if (owaStoreObjectIdString.StartsWith("GS.", StringComparison.Ordinal))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.GSCalendar;
			}
			StoreObjectId folderStoreObjectId = null;
			StoreObjectId folderStoreObjectId2 = null;
			string text = null;
			switch (owaStoreObjectIdType)
			{
			case OwaStoreObjectIdType.PublicStoreFolder:
				folderStoreObjectId = OwaStoreObjectId.CreateStoreObjectId(owaStoreObjectIdString.Substring("PSF".Length + 1));
				OwaStoreObjectId.ValidateFolderId(folderStoreObjectId);
				break;
			case OwaStoreObjectIdType.PublicStoreItem:
			{
				int num = owaStoreObjectIdString.LastIndexOf(".", StringComparison.Ordinal);
				if (num == "PSI".Length)
				{
					throw new OwaInvalidIdFormatException(string.Format("There should be two separator \"{0}\" in the id of the public item. Invalid Id string: {1}", ".", owaStoreObjectIdString));
				}
				folderStoreObjectId2 = OwaStoreObjectId.CreateStoreObjectId(owaStoreObjectIdString.Substring("PSI".Length + 1, num - "PSI".Length - 1));
				folderStoreObjectId = OwaStoreObjectId.CreateStoreObjectId(owaStoreObjectIdString.Substring(num + 1));
				OwaStoreObjectId.ValidateFolderId(folderStoreObjectId2);
				break;
			}
			case OwaStoreObjectIdType.Conversation:
			{
				string[] array = owaStoreObjectIdString.Split(new char[]
				{
					"."[0]
				});
				OwaStoreObjectId result;
				if (array.Length == 4)
				{
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), string.IsNullOrEmpty(array[2]) ? null : OwaStoreObjectId.CreateStoreObjectId(array[2]), string.IsNullOrEmpty(array[3]) ? null : OwaStoreObjectId.CreateInstanceKey(array[3]));
				}
				else if (array.Length == 3)
				{
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), OwaStoreObjectId.CreateStoreObjectId(array[2]), null);
				}
				else
				{
					if (array.Length != 2)
					{
						throw new OwaInvalidRequestException(string.Format("There should be one or two separator \"{0}\" in the id of the conversation item", "."));
					}
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), null, null);
				}
				return result;
			}
			case OwaStoreObjectIdType.OtherUserMailboxObject:
			{
				int num = owaStoreObjectIdString.LastIndexOf(".", StringComparison.Ordinal);
				folderStoreObjectId = OwaStoreObjectId.CreateStoreObjectIdFromString(owaStoreObjectIdString, "OUM", num);
				text = OwaStoreObjectId.ParseLegacyDnBase64String(owaStoreObjectIdString.Substring(num + 1));
				break;
			}
			case OwaStoreObjectIdType.ArchiveMailboxObject:
			{
				int num = owaStoreObjectIdString.LastIndexOf(".", StringComparison.Ordinal);
				folderStoreObjectId = OwaStoreObjectId.CreateStoreObjectIdFromString(owaStoreObjectIdString, "AMB", num);
				text = OwaStoreObjectId.ParseLegacyDnBase64String(owaStoreObjectIdString.Substring(num + 1));
				break;
			}
			case OwaStoreObjectIdType.ArchiveConversation:
			{
				string[] array = owaStoreObjectIdString.Split(new char[]
				{
					"."[0]
				});
				OwaStoreObjectId result;
				if (array.Length == 5)
				{
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[3]);
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), string.IsNullOrEmpty(array[2]) ? null : OwaStoreObjectId.CreateStoreObjectId(array[2]), text, string.IsNullOrEmpty(array[4]) ? null : OwaStoreObjectId.CreateInstanceKey(array[4]));
				}
				else if (array.Length == 4)
				{
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[3]);
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), OwaStoreObjectId.CreateStoreObjectId(array[2]), text, null);
				}
				else
				{
					if (array.Length != 3)
					{
						throw new OwaInvalidRequestException(string.Format("There should be two or three separator \"{0}\" in the id of the archive conversation item", "."));
					}
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[2]);
					result = new OwaStoreObjectId(ConversationId.Create(array[1]), null, text, null);
				}
				return result;
			}
			case OwaStoreObjectIdType.GSCalendar:
			{
				string[] array = owaStoreObjectIdString.Split(new char[]
				{
					"."[0]
				});
				if (array.Length != 2)
				{
					throw new OwaInvalidRequestException(string.Format("There should be two separator \"{0}\" in the id of the GS calendar item", "."));
				}
				text = OwaStoreObjectId.ParseLegacyDnBase64String(array[1]);
				break;
			}
			default:
				return OwaStoreObjectId.CreateFromStoreObjectId(OwaStoreObjectId.CreateStoreObjectId(owaStoreObjectIdString), null);
			}
			return new OwaStoreObjectId(owaStoreObjectIdType, folderStoreObjectId, folderStoreObjectId2, text);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000143F8 File Offset: 0x000125F8
		internal static OwaStoreObjectId CreateFromStoreObjectId(StoreObjectId storeObjectId, OwaStoreObjectId relatedStoreObjectId)
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			StoreObjectId storeObjectId2 = null;
			OwaStoreObjectIdType owaStoreObjectIdType = OwaStoreObjectIdType.MailBoxObject;
			if (IdConverter.IsFromPublicStore(storeObjectId))
			{
				if (IdConverter.IsMessageId(storeObjectId))
				{
					owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreItem;
					storeObjectId2 = IdConverter.GetParentIdFromMessageId(storeObjectId);
				}
				else
				{
					owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreFolder;
				}
			}
			else if (relatedStoreObjectId != null)
			{
				if (!relatedStoreObjectId.IsConversationId)
				{
					owaStoreObjectIdType = relatedStoreObjectId.OwaStoreObjectIdType;
				}
				else if (relatedStoreObjectId.OwaStoreObjectIdType == OwaStoreObjectIdType.ArchiveConversation)
				{
					owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
				}
			}
			return new OwaStoreObjectId(owaStoreObjectIdType, storeObjectId, storeObjectId2, (relatedStoreObjectId == null) ? null : relatedStoreObjectId.MailboxOwnerLegacyDN);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00014469 File Offset: 0x00012669
		internal string ToBase64String()
		{
			return this.ToString();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00014471 File Offset: 0x00012671
		private static void ValidateFolderId(StoreObjectId folderStoreObjectId)
		{
			if (!Folder.IsFolderId(folderStoreObjectId))
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid folder id: {0}", folderStoreObjectId.ToBase64String()));
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00014494 File Offset: 0x00012694
		private static string ParseLegacyDnBase64String(string legacyDnBase64String)
		{
			string text = null;
			try
			{
				text = Encoding.UTF8.GetString(Convert.FromBase64String(legacyDnBase64String));
			}
			catch (ArgumentException innerException)
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid legacyDN. Invalid Id string: {0}", legacyDnBase64String), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid legacyDN. Invalid Id string: {0}", legacyDnBase64String), innerException2);
			}
			if (string.IsNullOrEmpty(text) || !OwaStoreObjectId.IsValidLegacyDN(text))
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid legacy distinguished name: {0}", (text == null) ? "null" : text));
			}
			return text;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00014524 File Offset: 0x00012724
		private static StoreObjectId CreateStoreObjectIdFromString(string owaStoreObjectIdString, string itemPrefix, int indexOfLastSeparatorChar)
		{
			if (indexOfLastSeparatorChar == itemPrefix.Length)
			{
				throw new OwaInvalidIdFormatException(string.Format("There should be two separators \"{0}\" in the id of the public item. Invalid Id: {1}", ".", owaStoreObjectIdString));
			}
			return OwaStoreObjectId.CreateStoreObjectId(owaStoreObjectIdString.Substring(itemPrefix.Length + 1, indexOfLastSeparatorChar - itemPrefix.Length - 1));
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00014564 File Offset: 0x00012764
		private static StoreObjectId CreateStoreObjectId(string storeObjectIdString)
		{
			if (storeObjectIdString == null)
			{
				throw new ArgumentNullException("storeObjectIdString");
			}
			StoreObjectId result = null;
			try
			{
				result = StoreObjectId.Deserialize(storeObjectIdString);
			}
			catch (ArgumentException innerException)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException);
			}
			catch (FormatException innerException2)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException2);
			}
			catch (CorruptDataException innerException3)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(storeObjectIdString, null, innerException3);
			}
			return result;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000145D4 File Offset: 0x000127D4
		private static byte[] CreateInstanceKey(string instanceKeyString)
		{
			if (instanceKeyString == null)
			{
				throw new ArgumentNullException("instanceKeyString");
			}
			byte[] result = null;
			try
			{
				result = Convert.FromBase64String(instanceKeyString);
			}
			catch (ArgumentException innerException)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(instanceKeyString, null, innerException);
			}
			catch (FormatException innerException2)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(instanceKeyString, null, innerException2);
			}
			catch (CorruptDataException innerException3)
			{
				OwaStoreObjectId.ThrowInvalidIdFormatException(instanceKeyString, null, innerException3);
			}
			return result;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00014644 File Offset: 0x00012844
		private static bool IsValidLegacyDN(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				throw new ArgumentNullException("address");
			}
			LegacyDN legacyDN;
			return LegacyDN.TryParse(address, out legacyDN);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001466C File Offset: 0x0001286C
		private static void ThrowInvalidIdFormatException(string storeObjectId, string changeKey, Exception innerException)
		{
			throw new OwaInvalidIdFormatException(string.Format("Invalid id format. Store object id: {0}. Change key: {1}", (storeObjectId == null) ? "null" : storeObjectId, (changeKey == null) ? "null" : changeKey), innerException);
		}

		// Token: 0x0400039C RID: 924
		private const string PublicStoreFolderPrefix = "PSF";

		// Token: 0x0400039D RID: 925
		private const string PublicStoreItemPrefix = "PSI";

		// Token: 0x0400039E RID: 926
		private const string ConversationIdPrefix = "CID";

		// Token: 0x0400039F RID: 927
		private const string OtherUserMailboxObjectPrefix = "OUM";

		// Token: 0x040003A0 RID: 928
		private const string ArchiveMailBoxObjectPrefix = "AMB";

		// Token: 0x040003A1 RID: 929
		private const string ArchiveConversationIdPrefix = "ACI";

		// Token: 0x040003A2 RID: 930
		private const string GSCalendarPrefix = "GS";

		// Token: 0x040003A3 RID: 931
		private const string SeparatorChar = ".";

		// Token: 0x040003A4 RID: 932
		private readonly string mailboxOwnerLegacyDN;

		// Token: 0x040003A5 RID: 933
		private OwaStoreObjectIdType objectIdType;

		// Token: 0x040003A6 RID: 934
		private StoreId storeObjectId;

		// Token: 0x040003A7 RID: 935
		private StoreObjectId containerFolderId;

		// Token: 0x040003A8 RID: 936
		private byte[] instanceKey;
	}
}
