using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000204 RID: 516
	[Serializable]
	internal class OwaStoreObjectId : ObjectId
	{
		// Token: 0x06001132 RID: 4402 RVA: 0x00068408 File Offset: 0x00066608
		private OwaStoreObjectId(ConversationId conversationId, StoreObjectId folderId, byte[] instanceKey)
		{
			this.objectIdType = OwaStoreObjectIdType.Conversation;
			this.storeObjectId = conversationId;
			this.containerFolderId = folderId;
			this.instanceKey = instanceKey;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0006842C File Offset: 0x0006662C
		private OwaStoreObjectId(ConversationId conversationId, StoreObjectId folderId, string mailboxOwnerLegacyDN, byte[] instanceKey)
		{
			this.objectIdType = OwaStoreObjectIdType.ArchiveConversation;
			this.storeObjectId = conversationId;
			this.containerFolderId = folderId;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
			this.instanceKey = instanceKey;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00068458 File Offset: 0x00066658
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId) : this(objectIdType, storeObjectId, null, null)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00068464 File Offset: 0x00066664
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, string mailboxOwnerLegacyDN) : this(objectIdType, storeObjectId, null, mailboxOwnerLegacyDN)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00068470 File Offset: 0x00066670
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, StoreObjectId containerFolderId) : this(objectIdType, storeObjectId, containerFolderId, null)
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0006847C File Offset: 0x0006667C
		private OwaStoreObjectId(OwaStoreObjectIdType objectIdType, StoreObjectId storeObjectId, StoreObjectId containerFolderId, string mailboxOwnerLegacyDN)
		{
			this.objectIdType = objectIdType;
			this.storeObjectId = storeObjectId;
			this.containerFolderId = containerFolderId;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000684A4 File Offset: 0x000666A4
		public static StoreObjectId[] ConvertToStoreObjectIdArray(params OwaStoreObjectId[] owaStoreObjectIds)
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

		// Token: 0x06001139 RID: 4409 RVA: 0x000684E2 File Offset: 0x000666E2
		public static OwaStoreObjectId CreateFromItemId(StoreObjectId itemStoreObjectId, OwaStoreObjectId containerFolderId)
		{
			if (itemStoreObjectId == null)
			{
				throw new ArgumentNullException("itemStoreObjectId");
			}
			if (containerFolderId == null)
			{
				throw new ArgumentNullException("containerFolderId");
			}
			return OwaStoreObjectId.CreateFromItemId(itemStoreObjectId, containerFolderId.StoreObjectId, containerFolderId.OwaStoreObjectIdType, containerFolderId.MailboxOwnerLegacyDN);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00068518 File Offset: 0x00066718
		public static OwaStoreObjectId CreateFromStoreObjectId(StoreObjectId storeObjectId, OwaStoreObjectId relatedStoreObjectId)
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

		// Token: 0x0600113B RID: 4411 RVA: 0x0006858C File Offset: 0x0006678C
		public static OwaStoreObjectId CreateFromItemId(StoreObjectId itemStoreObjectId, Folder containerFolder)
		{
			if (itemStoreObjectId == null)
			{
				throw new ArgumentNullException("itemStoreObjectId");
			}
			if (containerFolder == null)
			{
				throw new ArgumentNullException("containerFolder");
			}
			OwaStoreObjectIdType owaStoreObjectIdType = OwaStoreObjectIdType.MailBoxObject;
			string legacyDN = null;
			if (Utilities.IsPublic(containerFolder))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreItem;
			}
			else if (Utilities.IsOtherMailbox(containerFolder))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
				legacyDN = Utilities.GetMailboxSessionLegacyDN(containerFolder);
			}
			else if (Utilities.IsInArchiveMailbox(containerFolder))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
				legacyDN = Utilities.GetMailboxSessionLegacyDN(containerFolder);
			}
			return OwaStoreObjectId.CreateFromItemId(itemStoreObjectId, (containerFolder.Id == null) ? null : containerFolder.Id.ObjectId, owaStoreObjectIdType, legacyDN);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00068607 File Offset: 0x00066807
		public static OwaStoreObjectId CreateFromMailboxItemId(StoreObjectId mailboxItemStoreObjectId)
		{
			if (mailboxItemStoreObjectId == null)
			{
				throw new ArgumentNullException("mailboxItemStoreObjectId");
			}
			return new OwaStoreObjectId(OwaStoreObjectIdType.MailBoxObject, mailboxItemStoreObjectId);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00068620 File Offset: 0x00066820
		public static OwaStoreObjectId CreateFromStoreObject(StoreObject storeObject)
		{
			if (storeObject == null)
			{
				throw new ArgumentNullException("storeObject");
			}
			if (storeObject.Id == null)
			{
				throw new ArgumentException("storeObject.Id must not be null");
			}
			OwaStoreObjectIdType owaStoreObjectIdType = OwaStoreObjectIdType.MailBoxObject;
			string legacyDN = null;
			if (Utilities.IsOtherMailbox(storeObject))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
				legacyDN = Utilities.GetMailboxSessionLegacyDN(storeObject);
			}
			else if (Utilities.IsInArchiveMailbox(storeObject))
			{
				owaStoreObjectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
				legacyDN = Utilities.GetMailboxSessionLegacyDN(storeObject);
			}
			if (storeObject is Item)
			{
				if (Utilities.IsPublic(storeObject))
				{
					owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreItem;
				}
				return OwaStoreObjectId.CreateFromItemId(storeObject.Id.ObjectId, (owaStoreObjectIdType == OwaStoreObjectIdType.OtherUserMailboxObject) ? null : storeObject.ParentId, owaStoreObjectIdType, legacyDN);
			}
			if (storeObject is Folder)
			{
				if (Utilities.IsPublic(storeObject))
				{
					owaStoreObjectIdType = OwaStoreObjectIdType.PublicStoreFolder;
				}
				return OwaStoreObjectId.CreateFromFolderId(storeObject.Id.ObjectId, owaStoreObjectIdType, legacyDN);
			}
			string message = string.Format(CultureInfo.InvariantCulture, "OwaStoreObjectId.CreateOwaStoreObjectId(StoreObject) only support item or folder as input, but the input is an {0}", new object[]
			{
				storeObject.GetType().ToString()
			});
			throw new ArgumentOutOfRangeException("storeObject", message);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00068700 File Offset: 0x00066900
		public static OwaStoreObjectId CreateFromSessionFolderId(UserContext userContext, StoreSession session, StoreObjectId folderId)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (userContext.IsMyMailbox(session))
			{
				return OwaStoreObjectId.CreateFromSessionFolderId(OwaStoreObjectIdType.MailBoxObject, null, folderId);
			}
			if (userContext.IsOtherMailbox(session))
			{
				return OwaStoreObjectId.CreateFromSessionFolderId(OwaStoreObjectIdType.OtherUserMailboxObject, ((MailboxSession)session).MailboxOwner.LegacyDn, folderId);
			}
			if (Utilities.IsArchiveMailbox(session))
			{
				return OwaStoreObjectId.CreateFromSessionFolderId(OwaStoreObjectIdType.ArchiveMailboxObject, ((MailboxSession)session).MailboxOwnerLegacyDN, folderId);
			}
			if (session is PublicFolderSession)
			{
				return OwaStoreObjectId.CreateFromSessionFolderId(OwaStoreObjectIdType.PublicStoreFolder, null, folderId);
			}
			throw new ArgumentException("The type of session is unknown");
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000687A0 File Offset: 0x000669A0
		public static OwaStoreObjectId CreateFromSessionFolderId(OwaStoreObjectIdType owaStoreObjectIdType, string legacyDN, StoreObjectId folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			switch (owaStoreObjectIdType)
			{
			case OwaStoreObjectIdType.MailBoxObject:
				return OwaStoreObjectId.CreateFromMailboxFolderId(folderId);
			case OwaStoreObjectIdType.PublicStoreFolder:
				return OwaStoreObjectId.CreateFromPublicFolderId(folderId);
			case OwaStoreObjectIdType.OtherUserMailboxObject:
				return OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(folderId, legacyDN);
			case OwaStoreObjectIdType.ArchiveMailboxObject:
				return OwaStoreObjectId.CreateFromArchiveMailboxFolderId(folderId, legacyDN);
			}
			throw new ArgumentException("mailbox session type is unknown");
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00068808 File Offset: 0x00066A08
		public static OwaStoreObjectIdType GetOwaStoreObjectIdType(UserContext userContext, StoreSession session, out string mailboxOwnerLegacyDN)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			mailboxOwnerLegacyDN = string.Empty;
			if (userContext.IsMyMailbox(session))
			{
				return OwaStoreObjectIdType.MailBoxObject;
			}
			if (userContext.IsOtherMailbox(session))
			{
				mailboxOwnerLegacyDN = ((MailboxSession)session).MailboxOwner.LegacyDn;
				return OwaStoreObjectIdType.OtherUserMailboxObject;
			}
			if (Utilities.IsArchiveMailbox(session))
			{
				mailboxOwnerLegacyDN = ((MailboxSession)session).MailboxOwnerLegacyDN;
				return OwaStoreObjectIdType.ArchiveMailboxObject;
			}
			if (session is PublicFolderSession)
			{
				return OwaStoreObjectIdType.PublicStoreFolder;
			}
			throw new ArgumentException("The type of session is unknown");
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0006888C File Offset: 0x00066A8C
		public static OwaStoreObjectId CreateFromNavigationNodeFolder(UserContext userContext, NavigationNodeFolder nodeFolder)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (nodeFolder == null)
			{
				throw new ArgumentNullException("nodeFolder");
			}
			if (!nodeFolder.IsValid)
			{
				throw new ArgumentException("Not valid navigation node folder.");
			}
			if (!nodeFolder.IsGSCalendar && nodeFolder.FolderId == null)
			{
				throw new NotSupportedException("Doesn't support this kind of node folder");
			}
			if (nodeFolder.IsFolderInSpecificMailboxSession(userContext.MailboxSession))
			{
				return OwaStoreObjectId.CreateFromMailboxFolderId(nodeFolder.FolderId);
			}
			if (nodeFolder.IsGSCalendar)
			{
				return OwaStoreObjectId.CreateFromGSCalendarLegacyDN(nodeFolder.MailboxLegacyDN);
			}
			ExchangePrincipal exchangePrincipal;
			if (userContext.DelegateSessionManager.TryGetExchangePrincipal(nodeFolder.MailboxLegacyDN, out exchangePrincipal) && exchangePrincipal.MailboxInfo.IsArchive)
			{
				return OwaStoreObjectId.CreateFromArchiveMailboxFolderId(nodeFolder.FolderId, nodeFolder.MailboxLegacyDN);
			}
			return OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(nodeFolder.FolderId, nodeFolder.MailboxLegacyDN);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00068958 File Offset: 0x00066B58
		public static OwaStoreObjectId CreateFromString(string owaStoreObjectIdString)
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
				folderStoreObjectId = Utilities.CreateStoreObjectId(owaStoreObjectIdString.Substring("PSF".Length + 1));
				OwaStoreObjectId.ValidateFolderId(folderStoreObjectId);
				break;
			case OwaStoreObjectIdType.PublicStoreItem:
			{
				int num = owaStoreObjectIdString.LastIndexOf(".", StringComparison.Ordinal);
				if (num == "PSI".Length)
				{
					throw new OwaInvalidIdFormatException(string.Format("There should be two separator \"{0}\" in the id of the public item. Invalid Id string: {1}", ".", owaStoreObjectIdString));
				}
				folderStoreObjectId2 = Utilities.CreateStoreObjectId(owaStoreObjectIdString.Substring("PSI".Length + 1, num - "PSI".Length - 1));
				folderStoreObjectId = Utilities.CreateStoreObjectId(owaStoreObjectIdString.Substring(num + 1));
				OwaStoreObjectId.ValidateFolderId(folderStoreObjectId2);
				break;
			}
			case OwaStoreObjectIdType.Conversation:
			{
				string[] array = owaStoreObjectIdString.Split(new char[]
				{
					"."[0]
				});
				OwaStoreObjectId owaStoreObjectId;
				if (array.Length == 4)
				{
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), string.IsNullOrEmpty(array[2]) ? null : Utilities.CreateStoreObjectId(array[2]), string.IsNullOrEmpty(array[3]) ? null : Utilities.CreateInstanceKey(array[3]));
				}
				else if (array.Length == 3)
				{
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), Utilities.CreateStoreObjectId(array[2]), null);
				}
				else
				{
					if (array.Length != 2)
					{
						throw new OwaInvalidRequestException(string.Format("There should be one or two separator \"{0}\" in the id of the conversation item", "."));
					}
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), null, null);
				}
				owaStoreObjectId.bufferedIdString = owaStoreObjectIdString;
				return owaStoreObjectId;
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
				OwaStoreObjectId owaStoreObjectId;
				if (array.Length == 5)
				{
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[3]);
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), string.IsNullOrEmpty(array[2]) ? null : Utilities.CreateStoreObjectId(array[2]), text, string.IsNullOrEmpty(array[4]) ? null : Utilities.CreateInstanceKey(array[4]));
				}
				else if (array.Length == 4)
				{
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[3]);
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), Utilities.CreateStoreObjectId(array[2]), text, null);
				}
				else
				{
					if (array.Length != 3)
					{
						throw new OwaInvalidRequestException(string.Format("There should be two or three separator \"{0}\" in the id of the archive conversation item", "."));
					}
					text = OwaStoreObjectId.ParseLegacyDnBase64String(array[2]);
					owaStoreObjectId = new OwaStoreObjectId(ConversationId.Create(array[1]), null, text, null);
				}
				owaStoreObjectId.bufferedIdString = owaStoreObjectIdString;
				return owaStoreObjectId;
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
				return OwaStoreObjectId.CreateFromStoreObjectId(Utilities.CreateStoreObjectId(owaStoreObjectIdString), null);
			}
			return new OwaStoreObjectId(owaStoreObjectIdType, folderStoreObjectId, folderStoreObjectId2, text)
			{
				bufferedIdString = owaStoreObjectIdString
			};
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00068D2C File Offset: 0x00066F2C
		public static OwaStoreObjectId CreateFromItemId(StoreObjectId itemStoreObjectId, StoreObjectId containerFolderId, OwaStoreObjectIdType objectIdType, string legacyDN)
		{
			if (itemStoreObjectId == null)
			{
				throw new ArgumentNullException("itemStoreObjectId");
			}
			if (objectIdType == OwaStoreObjectIdType.PublicStoreItem && containerFolderId == null)
			{
				throw new ArgumentNullException("containerFolderId");
			}
			if (objectIdType == OwaStoreObjectIdType.OtherUserMailboxObject && string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			if (objectIdType == OwaStoreObjectIdType.ArchiveMailboxObject && string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			return new OwaStoreObjectId(objectIdType, itemStoreObjectId, containerFolderId, legacyDN);
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00068D90 File Offset: 0x00066F90
		public ConversationId ConversationId
		{
			get
			{
				return this.storeObjectId as ConversationId;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00068D9D File Offset: 0x00066F9D
		public byte[] InstanceKey
		{
			get
			{
				return this.instanceKey;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00068DA5 File Offset: 0x00066FA5
		public StoreId StoreId
		{
			get
			{
				return this.storeObjectId;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00068DAD File Offset: 0x00066FAD
		public StoreObjectId StoreObjectId
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

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00068DCD File Offset: 0x00066FCD
		public bool IsStoreObjectId
		{
			get
			{
				return this.storeObjectId is StoreObjectId;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00068DDD File Offset: 0x00066FDD
		public bool IsConversationId
		{
			get
			{
				return this.storeObjectId is ConversationId;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00068DF0 File Offset: 0x00066FF0
		public StoreObjectType StoreObjectType
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00068E14 File Offset: 0x00067014
		public OwaStoreObjectIdType OwaStoreObjectIdType
		{
			get
			{
				return this.objectIdType;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00068E1C File Offset: 0x0006701C
		public string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00068E24 File Offset: 0x00067024
		public bool IsPublic
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.PublicStoreFolder || this.objectIdType == OwaStoreObjectIdType.PublicStoreItem;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00068E3A File Offset: 0x0006703A
		public bool IsOtherMailbox
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.OtherUserMailboxObject;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00068E45 File Offset: 0x00067045
		public bool IsGSCalendar
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.GSCalendar;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00068E50 File Offset: 0x00067050
		public bool IsArchive
		{
			get
			{
				return this.objectIdType == OwaStoreObjectIdType.ArchiveConversation || this.objectIdType == OwaStoreObjectIdType.ArchiveMailboxObject;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00068E68 File Offset: 0x00067068
		public OwaStoreObjectId ProviderLevelItemId
		{
			get
			{
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(this.StoreObjectId.ProviderLevelItemId);
				return new OwaStoreObjectId(this.objectIdType, storeObjectId, this.containerFolderId, this.mailboxOwnerLegacyDN);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00068E9E File Offset: 0x0006709E
		public StoreObjectId ParentFolderId
		{
			get
			{
				return this.containerFolderId;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00068EA8 File Offset: 0x000670A8
		public StoreSession GetSession(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(this, userContext);
			StoreSession result;
			try
			{
				StoreSession session = owaStoreObjectIdSessionHandle.Session;
				userContext.AddSessionHandle(owaStoreObjectIdSessionHandle);
				result = session;
			}
			catch (Exception)
			{
				owaStoreObjectIdSessionHandle.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00068EF8 File Offset: 0x000670F8
		public StoreSession GetSessionForFolderContent(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			OwaStoreObjectIdSessionHandle owaStoreObjectIdSessionHandle = new OwaStoreObjectIdSessionHandle(this, userContext);
			StoreSession result;
			try
			{
				StoreSession sessionForFolderContent = owaStoreObjectIdSessionHandle.SessionForFolderContent;
				userContext.AddSessionHandle(owaStoreObjectIdSessionHandle);
				result = sessionForFolderContent;
			}
			catch (Exception)
			{
				owaStoreObjectIdSessionHandle.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00068F48 File Offset: 0x00067148
		public string ToBase64String()
		{
			return this.ToString();
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00068F50 File Offset: 0x00067150
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			OwaStoreObjectId owaStoreObjectId = obj as OwaStoreObjectId;
			if (owaStoreObjectId == null)
			{
				return false;
			}
			if (this.objectIdType != owaStoreObjectId.objectIdType)
			{
				return false;
			}
			if (this.objectIdType == OwaStoreObjectIdType.PublicStoreItem)
			{
				return this.StoreObjectId.Equals(owaStoreObjectId.StoreObjectId) && this.containerFolderId.Equals(owaStoreObjectId.containerFolderId);
			}
			if (this.objectIdType == OwaStoreObjectIdType.OtherUserMailboxObject || this.objectIdType == OwaStoreObjectIdType.ArchiveMailboxObject)
			{
				return this.StoreObjectId.Equals(owaStoreObjectId.StoreObjectId) && this.mailboxOwnerLegacyDN.Equals(owaStoreObjectId.MailboxOwnerLegacyDN, StringComparison.OrdinalIgnoreCase);
			}
			if (this.objectIdType == OwaStoreObjectIdType.GSCalendar)
			{
				return this.mailboxOwnerLegacyDN.Equals(owaStoreObjectId.MailboxOwnerLegacyDN, StringComparison.OrdinalIgnoreCase);
			}
			return this.StoreObjectId.Equals(owaStoreObjectId.StoreObjectId);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00069015 File Offset: 0x00067215
		public override int GetHashCode()
		{
			if (this.IsGSCalendar)
			{
				return this.MailboxOwnerLegacyDN.ToLowerInvariant().GetHashCode();
			}
			return this.StoreId.GetHashCode();
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0006903B File Offset: 0x0006723B
		public override byte[] GetBytes()
		{
			if (this.IsGSCalendar)
			{
				return Encoding.Unicode.GetBytes(this.MailboxOwnerLegacyDN);
			}
			return this.StoreId.GetBytes();
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00069064 File Offset: 0x00067264
		public override string ToString()
		{
			if (this.bufferedIdString == null)
			{
				switch (this.objectIdType)
				{
				case OwaStoreObjectIdType.MailBoxObject:
					this.bufferedIdString = this.storeObjectId.ToBase64String();
					break;
				case OwaStoreObjectIdType.PublicStoreFolder:
					this.bufferedIdString = this.CreatePublicStoreFolderId();
					break;
				case OwaStoreObjectIdType.PublicStoreItem:
					this.bufferedIdString = this.CreatePublicStoreItemId();
					break;
				case OwaStoreObjectIdType.Conversation:
					this.bufferedIdString = this.CreateConversationId();
					break;
				case OwaStoreObjectIdType.OtherUserMailboxObject:
					this.bufferedIdString = this.CreateOtherUserMailboxStoreItemId();
					break;
				case OwaStoreObjectIdType.ArchiveMailboxObject:
					this.bufferedIdString = this.CreateArchiveMailboxStoreObjectId();
					break;
				case OwaStoreObjectIdType.ArchiveConversation:
					this.bufferedIdString = this.CreateArchiveConversationId();
					break;
				case OwaStoreObjectIdType.GSCalendar:
					this.bufferedIdString = this.CreateGSCalendarId();
					break;
				}
			}
			return this.bufferedIdString;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00069124 File Offset: 0x00067324
		internal static OwaStoreObjectId CreateFromConversationId(ConversationId conversationId, StoreObject folder)
		{
			return OwaStoreObjectId.CreateFromConversationId(conversationId, folder, null);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00069130 File Offset: 0x00067330
		internal static OwaStoreObjectId CreateFromConversationId(ConversationId conversationId, StoreObject folder, byte[] instanceKey)
		{
			if (conversationId == null)
			{
				throw new ArgumentNullException("conversationId");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			if (Utilities.IsOtherMailbox(folder))
			{
				throw new NotSupportedException("conversation should not from other user's folder");
			}
			if (Utilities.IsInArchiveMailbox(folder))
			{
				return new OwaStoreObjectId(conversationId, folder.Id.ObjectId, Utilities.GetMailboxSessionLegacyDN(folder), instanceKey);
			}
			return new OwaStoreObjectId(conversationId, folder.Id.ObjectId, instanceKey);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0006919F File Offset: 0x0006739F
		internal static OwaStoreObjectId CreateFromConversationIdForListViewNotification(ConversationId conversationId, StoreObjectId folderId, byte[] instanceKey)
		{
			if (conversationId == null)
			{
				throw new ArgumentNullException("conversationId");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folder");
			}
			return new OwaStoreObjectId(conversationId, folderId, instanceKey);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000691C5 File Offset: 0x000673C5
		internal static OwaStoreObjectId CreateFromPublicFolderId(StoreObjectId publicStoreFolderId)
		{
			if (publicStoreFolderId == null)
			{
				throw new ArgumentNullException("publicStoreFolderId");
			}
			return OwaStoreObjectId.CreateFromFolderId(publicStoreFolderId, OwaStoreObjectIdType.PublicStoreFolder);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000691DC File Offset: 0x000673DC
		internal static OwaStoreObjectId CreateFromPublicStoreItemId(StoreObjectId publicStoreItemId, StoreObjectId publicStoreFolderId)
		{
			if (publicStoreItemId == null)
			{
				throw new ArgumentNullException("publicStoreItemId");
			}
			if (publicStoreFolderId == null)
			{
				throw new ArgumentNullException("publicStoreFolderId");
			}
			return OwaStoreObjectId.CreateFromItemId(publicStoreItemId, publicStoreFolderId, OwaStoreObjectIdType.PublicStoreItem, null);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00069203 File Offset: 0x00067403
		internal static OwaStoreObjectId CreateFromMailboxFolderId(StoreObjectId mailboxFolderStoreObjectId)
		{
			if (mailboxFolderStoreObjectId == null)
			{
				throw new ArgumentNullException("mailboxFolderStoreObjectId");
			}
			return OwaStoreObjectId.CreateFromFolderId(mailboxFolderStoreObjectId, OwaStoreObjectIdType.MailBoxObject);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0006921A File Offset: 0x0006741A
		internal static OwaStoreObjectId CreateFromOtherUserMailboxFolderId(StoreObjectId otherMailboxFolderStoreObjectId, string legacyDN)
		{
			if (otherMailboxFolderStoreObjectId == null)
			{
				throw new ArgumentNullException("otherMailboxFolderStoreObjectId");
			}
			return OwaStoreObjectId.CreateFromFolderId(otherMailboxFolderStoreObjectId, OwaStoreObjectIdType.OtherUserMailboxObject, legacyDN);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00069232 File Offset: 0x00067432
		internal static OwaStoreObjectId CreateFromArchiveMailboxFolderId(StoreObjectId archiveMailboxFolderStoreObjectId, string legacyDN)
		{
			if (archiveMailboxFolderStoreObjectId == null)
			{
				throw new ArgumentNullException("archiveMailboxFolderStoreObjectId");
			}
			if (string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			return OwaStoreObjectId.CreateFromFolderId(archiveMailboxFolderStoreObjectId, OwaStoreObjectIdType.ArchiveMailboxObject, legacyDN);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0006925D File Offset: 0x0006745D
		internal static OwaStoreObjectId CreateFromGSCalendarLegacyDN(string legacyDN)
		{
			if (string.IsNullOrEmpty(legacyDN))
			{
				throw new ArgumentNullException("legacyDN");
			}
			return new OwaStoreObjectId(OwaStoreObjectIdType.GSCalendar, null, legacyDN);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0006927A File Offset: 0x0006747A
		internal static OwaStoreObjectId CreateFromFolderId(StoreObjectId folderStoreObjectId, OwaStoreObjectIdType objectIdType, string legacyDN)
		{
			if (folderStoreObjectId == null)
			{
				throw new ArgumentNullException("folderStoreObjectId");
			}
			return new OwaStoreObjectId(objectIdType, folderStoreObjectId, legacyDN);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00069292 File Offset: 0x00067492
		internal static OwaStoreObjectId CreateFromFolderId(StoreObjectId folderStoreObjectId, OwaStoreObjectIdType objectIdType)
		{
			if (folderStoreObjectId == null)
			{
				throw new ArgumentNullException("folderStoreObjectId");
			}
			if (objectIdType == OwaStoreObjectIdType.OtherUserMailboxObject)
			{
				throw new OwaInvalidOperationException("Mailbox legacy DN is required for other user mailbox");
			}
			return new OwaStoreObjectId(objectIdType, folderStoreObjectId);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000692B8 File Offset: 0x000674B8
		internal static bool IsDummyArchiveFolder(string owaStoreObjectIdString)
		{
			if (!owaStoreObjectIdString.StartsWith("AMB.", StringComparison.Ordinal))
			{
				return false;
			}
			int num = owaStoreObjectIdString.LastIndexOf(".", StringComparison.Ordinal);
			string strA = owaStoreObjectIdString.Substring("AMB".Length + 1, num - "AMB".Length - 1);
			return string.Compare(strA, StoreObjectId.DummyId.ToString(), StringComparison.Ordinal) == 0;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00069317 File Offset: 0x00067517
		private static StoreObjectId CreateStoreObjectIdFromString(string owaStoreObjectIdString, string itemPrefix, int indexOfLastSeparatorChar)
		{
			if (indexOfLastSeparatorChar == itemPrefix.Length)
			{
				throw new OwaInvalidIdFormatException(string.Format("There should be two separators \"{0}\" in the id of the public item. Invalid Id: {1}", ".", owaStoreObjectIdString));
			}
			return Utilities.CreateStoreObjectId(owaStoreObjectIdString.Substring(itemPrefix.Length + 1, indexOfLastSeparatorChar - itemPrefix.Length - 1));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00069355 File Offset: 0x00067555
		private static void ValidateFolderId(StoreObjectId folderStoreObjectId)
		{
			if (!Folder.IsFolderId(folderStoreObjectId))
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid folder id: {0}", folderStoreObjectId.ToBase64String()));
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00069378 File Offset: 0x00067578
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
			if (string.IsNullOrEmpty(text) || !Utilities.IsValidLegacyDN(text))
			{
				throw new OwaInvalidIdFormatException(string.Format("Invalid legacy distinguished name: {0}", (text == null) ? "null" : text));
			}
			return text;
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00069408 File Offset: 0x00067608
		private string NormalizedContainerFolderIdBase64String
		{
			get
			{
				byte[] bytes = this.containerFolderId.GetBytes();
				bytes[bytes.Length - 1] = 1;
				return Convert.ToBase64String(bytes);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00069430 File Offset: 0x00067630
		private string NormalizedMailboxOwnerLegacyDnBase64String
		{
			get
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.mailboxOwnerLegacyDN);
				return Convert.ToBase64String(bytes);
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00069454 File Offset: 0x00067654
		private string CreatePublicStoreFolderId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PSF");
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			return stringBuilder.ToString();
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00069498 File Offset: 0x00067698
		private string CreatePublicStoreItemId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PSI");
			stringBuilder.Append(".");
			stringBuilder.Append(this.NormalizedContainerFolderIdBase64String);
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			return stringBuilder.ToString();
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000694F4 File Offset: 0x000676F4
		private string CreateConversationId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CID");
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			stringBuilder.Append(".");
			if (this.containerFolderId != null)
			{
				stringBuilder.Append(this.NormalizedContainerFolderIdBase64String);
			}
			stringBuilder.Append(".");
			if (this.instanceKey != null)
			{
				stringBuilder.Append(Convert.ToBase64String(this.instanceKey));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00069580 File Offset: 0x00067780
		private string CreateOtherUserMailboxStoreItemId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OUM");
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			stringBuilder.Append(".");
			stringBuilder.Append(this.NormalizedMailboxOwnerLegacyDnBase64String);
			return stringBuilder.ToString();
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000695DC File Offset: 0x000677DC
		private string CreateArchiveMailboxStoreObjectId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("AMB");
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			stringBuilder.Append(".");
			stringBuilder.Append(this.NormalizedMailboxOwnerLegacyDnBase64String);
			return stringBuilder.ToString();
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00069638 File Offset: 0x00067838
		private string CreateArchiveConversationId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ACI");
			stringBuilder.Append(".");
			stringBuilder.Append(this.storeObjectId.ToBase64String());
			stringBuilder.Append(".");
			if (this.containerFolderId != null)
			{
				stringBuilder.Append(this.NormalizedContainerFolderIdBase64String);
			}
			stringBuilder.Append(".");
			stringBuilder.Append(this.NormalizedMailboxOwnerLegacyDnBase64String);
			stringBuilder.Append(".");
			if (this.instanceKey != null)
			{
				stringBuilder.Append(Convert.ToBase64String(this.instanceKey));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000696DC File Offset: 0x000678DC
		private string CreateGSCalendarId()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("GS");
			stringBuilder.Append(".");
			stringBuilder.Append(this.NormalizedMailboxOwnerLegacyDnBase64String);
			return stringBuilder.ToString();
		}

		// Token: 0x04000B9F RID: 2975
		private const string PublicStoreFolderPrefix = "PSF";

		// Token: 0x04000BA0 RID: 2976
		private const string PublicStoreItemPrefix = "PSI";

		// Token: 0x04000BA1 RID: 2977
		private const string ConversationIdPrefix = "CID";

		// Token: 0x04000BA2 RID: 2978
		private const string OtherUserMailboxObjectPrefix = "OUM";

		// Token: 0x04000BA3 RID: 2979
		private const string ArchiveMailBoxObjectPrefix = "AMB";

		// Token: 0x04000BA4 RID: 2980
		private const string ArchiveConversationIdPrefix = "ACI";

		// Token: 0x04000BA5 RID: 2981
		private const string GSCalendarPrefix = "GS";

		// Token: 0x04000BA6 RID: 2982
		private const string SeparatorChar = ".";

		// Token: 0x04000BA7 RID: 2983
		private OwaStoreObjectIdType objectIdType;

		// Token: 0x04000BA8 RID: 2984
		private StoreId storeObjectId;

		// Token: 0x04000BA9 RID: 2985
		private StoreObjectId containerFolderId;

		// Token: 0x04000BAA RID: 2986
		private byte[] instanceKey;

		// Token: 0x04000BAB RID: 2987
		private string bufferedIdString;

		// Token: 0x04000BAC RID: 2988
		private string mailboxOwnerLegacyDN;
	}
}
