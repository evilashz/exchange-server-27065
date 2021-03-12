﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000795 RID: 1941
	internal sealed class IdConverter : IIdConverter
	{
		// Token: 0x060039C5 RID: 14789 RVA: 0x000CB977 File Offset: 0x000C9B77
		public IdConverter(CallContext callContext)
		{
			this.callContext = callContext;
			OwsLogRegistry.Register(typeof(GetFolder).Name, typeof(GetFolderMetadata), new Type[0]);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000CB9AA File Offset: 0x000C9BAA
		public static ConcatenatedIdAndChangeKey GetConcatenatedId(StoreId storeId, MailboxId mailboxId, List<AttachmentId> attachmentIds)
		{
			return IdConverter.GetConcatenatedId(storeId, null, mailboxId, attachmentIds);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000CB9B8 File Offset: 0x000C9BB8
		public static ConcatenatedIdAndChangeKey GetConcatenatedId(StoreId storeId, IdAndSession idAndSession, List<AttachmentId> attachmentIds)
		{
			MailboxId mailboxId = null;
			StoreObjectId parentStoreObjectId = null;
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				mailboxId = new MailboxId(mailboxSession);
			}
			else
			{
				parentStoreObjectId = StoreId.GetStoreObjectId(idAndSession.ParentFolderId);
			}
			return IdConverter.GetConcatenatedId(storeId, parentStoreObjectId, mailboxId, attachmentIds);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000CB9F8 File Offset: 0x000C9BF8
		public static FolderId GetFolderIdFromStoreId(StoreId storeId, MailboxId mailboxId)
		{
			FolderId folderId = new FolderId();
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			folderId.Id = concatenatedId.Id;
			folderId.ChangeKey = concatenatedId.ChangeKey;
			return folderId;
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000CBA30 File Offset: 0x000C9C30
		public static ItemId GetItemIdFromStoreId(StoreId storeId, MailboxId mailboxId)
		{
			ItemId itemId = new ItemId();
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			itemId.Id = concatenatedId.Id;
			itemId.ChangeKey = concatenatedId.ChangeKey;
			return itemId;
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000CBA68 File Offset: 0x000C9C68
		public static ConcatenatedIdAndChangeKey GetConcatenatedIdForPublicFolder(StoreId storeId)
		{
			StoreObjectId parentStoreObjectId = null;
			return IdConverter.GetConcatenatedId(storeId, parentStoreObjectId, null, null);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000CBA80 File Offset: 0x000C9C80
		public static ConcatenatedIdAndChangeKey GetConcatenatedIdForPublicFolderItem(StoreId storeId, StoreObjectId parentFolderId, List<AttachmentId> attachmentIds)
		{
			return IdConverter.GetConcatenatedId(storeId, parentFolderId, null, attachmentIds);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000CBA8B File Offset: 0x000C9C8B
		public static StoreObjectId GetParentIdFromItemId(StoreObjectId storeObjectId)
		{
			return IdConverter.GetParentIdFromMessageId(storeObjectId);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000CBA93 File Offset: 0x000C9C93
		public static StoreId CombineStoreObjectIdWithChangeKey(StoreObjectId storeObjectId, byte[] changeKey)
		{
			if (changeKey == null || changeKey.Length == 0)
			{
				return storeObjectId;
			}
			return new VersionedId(storeObjectId, changeKey);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000CBAA6 File Offset: 0x000C9CA6
		public static bool IsDefaultFolderCreateSupported(string distinguishedFolderDisplayName)
		{
			return IdConverter.defaultFoldersWithCreateSupport.Member.Contains(distinguishedFolderDisplayName);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000CBAB8 File Offset: 0x000C9CB8
		public static DefaultFolderType GetDefaultFolderTypeFromDistinguishedFolderIdNameType(string distinguishedFolderDisplayName)
		{
			return IdConverter.displayNameMap.Member[distinguishedFolderDisplayName];
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000CBACA File Offset: 0x000C9CCA
		public static Dictionary<DefaultFolderType, string> GetDefaultFolderTypeToFolderNameMapForMailbox()
		{
			return IdConverter.defaultFolderToNameMapForMailbox.Member;
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000CBAD6 File Offset: 0x000C9CD6
		public static Dictionary<DefaultFolderType, string> GetDefaultFolderTypeToFolderNameMapForArchiveMailbox()
		{
			return IdConverter.defaultFolderToNameMapForArchiveMailbox.Member;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000CBAE4 File Offset: 0x000C9CE4
		public static string ConversationIdToEwsId(Guid mailboxGuid, ConversationId conversationId)
		{
			MailboxId mailboxId = new MailboxId(mailboxGuid);
			IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
			if (conversationId == null)
			{
				byte[] storeIdBytes = new byte[0];
				idHeaderInformation.StoreIdBytes = storeIdBytes;
			}
			else
			{
				idHeaderInformation.StoreIdBytes = conversationId.GetBytes();
			}
			idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Normal;
			idHeaderInformation.IdStorageType = IdStorageType.ConversationIdMailboxGuidBased;
			idHeaderInformation.MailboxId = mailboxId;
			return IdConverter.ConvertToConcatenatedId(idHeaderInformation, null, true);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000CBB3C File Offset: 0x000C9D3C
		public static ConversationId EwsIdToConversationId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return ConversationId.Create(idHeaderInformation.StoreIdBytes);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000CBB5E File Offset: 0x000C9D5E
		public static StoreObjectId EwsIdToFolderId(string ewsId)
		{
			return IdConverter.EwsIdToStoreObjectIdGivenStoreObjectType(ewsId, StoreObjectType.Folder);
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000CBB67 File Offset: 0x000C9D67
		public static StoreObjectId EwsIdToMessageStoreObjectId(string ewsId)
		{
			return IdConverter.EwsIdToStoreObjectIdGivenStoreObjectType(ewsId, StoreObjectType.Message);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000CBB74 File Offset: 0x000C9D74
		public static StoreObjectId EwsIdToStoreObjectIdGivenStoreObjectType(string ewsId, StoreObjectType storeObjectType)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return StoreObjectId.FromProviderSpecificId(idHeaderInformation.StoreIdBytes, storeObjectType);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000CBB98 File Offset: 0x000C9D98
		public static ItemId PersonaIdFromPerson(Person person, Guid mailboxGuid)
		{
			string id = IdConverter.PersonIdToEwsId(mailboxGuid, person.PersonId);
			return new ItemId(id, person.CalculateChangeKey());
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000CBBC0 File Offset: 0x000C9DC0
		public static ItemId PersonaIdFromPersonId(Guid mailboxGuid, PersonId personId)
		{
			string id = IdConverter.PersonIdToEwsId(mailboxGuid, personId);
			return new ItemId(id, null);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000CBBDC File Offset: 0x000C9DDC
		public static ItemId PersonaIdFromADObjectId(Guid adObjectGuid)
		{
			ADObjectId adObjectId = new ADObjectId(adObjectGuid);
			string id = IdConverter.ADObjectIdToEwsId(adObjectId);
			return new ItemId(id, null);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000CBC00 File Offset: 0x000C9E00
		public static ItemId PersonaIdFromStoreId(StoreId storeId, MailboxId mailboxId)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			return new ItemId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000CBC38 File Offset: 0x000C9E38
		public static ItemId PersonaIdFromPublicFolderItemId(StoreId storeId, StoreObjectId parentFolderId)
		{
			ConcatenatedIdAndChangeKey concatenatedIdForPublicFolderItem = IdConverter.GetConcatenatedIdForPublicFolderItem(storeId, parentFolderId, null);
			return new ItemId
			{
				Id = concatenatedIdForPublicFolderItem.Id,
				ChangeKey = concatenatedIdForPublicFolderItem.ChangeKey
			};
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000CBC70 File Offset: 0x000C9E70
		public static string PersonIdToEwsId(Guid mailboxGuid, PersonId personId)
		{
			MailboxId mailboxId = new MailboxId(mailboxGuid);
			IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
			if (personId == null)
			{
				byte[] storeIdBytes = new byte[0];
				idHeaderInformation.StoreIdBytes = storeIdBytes;
			}
			else
			{
				idHeaderInformation.StoreIdBytes = personId.GetBytes();
			}
			idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Normal;
			idHeaderInformation.IdStorageType = IdStorageType.ConversationIdMailboxGuidBased;
			idHeaderInformation.MailboxId = mailboxId;
			return IdConverter.ConvertToConcatenatedId(idHeaderInformation, null, true);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000CBCC8 File Offset: 0x000C9EC8
		public static PersonId EwsIdToPersonId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return PersonId.Create(idHeaderInformation.StoreIdBytes);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000CBCEC File Offset: 0x000C9EEC
		public static string ADObjectIdToEwsId(ADObjectId adObjectId)
		{
			return IdConverter.ConvertToConcatenatedId(new IdHeaderInformation
			{
				IdProcessingInstruction = IdProcessingInstruction.Normal,
				IdStorageType = IdStorageType.ActiveDirectoryObject,
				StoreIdBytes = adObjectId.ObjectGuid.ToByteArray()
			}, null, true);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000CBD2C File Offset: 0x000C9F2C
		public static ADObjectId EwsIdToADObjectId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return new ADObjectId(new Guid(idHeaderInformation.StoreIdBytes));
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000CBD54 File Offset: 0x000C9F54
		public static bool EwsIdIsActiveDirectoryObject(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return idHeaderInformation.IdStorageType == IdStorageType.ActiveDirectoryObject;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000CBD74 File Offset: 0x000C9F74
		public static bool EwsIdIsConversationId(string ewsId)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null, false);
			return idHeaderInformation.IdStorageType == IdStorageType.ConversationIdMailboxGuidBased;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000CBD94 File Offset: 0x000C9F94
		internal static bool IsItemId(StoreObjectId storeObjectId)
		{
			return IdConverter.IsMessageId(storeObjectId);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000CBD9C File Offset: 0x000C9F9C
		public static bool IsFolderObjectType(StoreObjectType objectType)
		{
			switch (objectType)
			{
			case StoreObjectType.Folder:
			case StoreObjectType.CalendarFolder:
			case StoreObjectType.ContactsFolder:
			case StoreObjectType.TasksFolder:
			case StoreObjectType.NotesFolder:
			case StoreObjectType.JournalFolder:
			case StoreObjectType.SearchFolder:
			case StoreObjectType.OutlookSearchFolder:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000CBDD8 File Offset: 0x000C9FD8
		public static bool IsArchiveDistinguishedFolderId(DistinguishedFolderId folderId)
		{
			switch (folderId.Id)
			{
			case DistinguishedFolderIdName.archiveroot:
			case DistinguishedFolderIdName.archivemsgfolderroot:
			case DistinguishedFolderIdName.archivedeleteditems:
			case DistinguishedFolderIdName.archiveinbox:
			case DistinguishedFolderIdName.archiverecoverableitemsroot:
			case DistinguishedFolderIdName.archiverecoverableitemsdeletions:
			case DistinguishedFolderIdName.archiverecoverableitemsversions:
			case DistinguishedFolderIdName.archiverecoverableitemspurges:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000CBE1C File Offset: 0x000CA01C
		public static IdAndSession ConvertDefaultFolderType(CallContext callContext, DefaultFolderType defaultFolderType, MailboxId mailboxId, bool unifiedLogon)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			MailboxSession mailboxSessionByMailboxId = callContext.SessionCache.GetMailboxSessionByMailboxId(mailboxId, unifiedLogon);
			StoreObjectId refreshedDefaultFolderId = mailboxSessionByMailboxId.GetRefreshedDefaultFolderId(defaultFolderType, unifiedLogon);
			IdConverter.VerifyIdRepresentsFolder(refreshedDefaultFolderId);
			return new IdAndSession(refreshedDefaultFolderId, mailboxSessionByMailboxId);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000CBE5C File Offset: 0x000CA05C
		public static IdAndSession ConvertDefaultFolderType(CallContext callContext, DefaultFolderType defaultFolderType, string emailAddress, bool archiveSession)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (callContext.IsExternalUser)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "IDConverter.ConvertDefaultFolderType. DistinguishedFolderId is not supported for external users. SmtpAddress: {0}", emailAddress ?? "<Null>");
				throw new ServiceAccessDeniedException();
			}
			MailboxSession mailboxSession;
			if (string.IsNullOrEmpty(emailAddress))
			{
				mailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession(archiveSession);
			}
			else
			{
				mailboxSession = callContext.SessionCache.GetMailboxSessionBySmtpAddress(emailAddress, archiveSession);
			}
			StoreObjectId refreshedDefaultFolderId = mailboxSession.GetRefreshedDefaultFolderId(defaultFolderType);
			IdConverter.VerifyIdRepresentsFolder(refreshedDefaultFolderId);
			return new IdAndSession(refreshedDefaultFolderId, mailboxSession);
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000CBEDC File Offset: 0x000CA0DC
		public static StoreObjectId GetAsStoreObjectId(StoreId id, out bool wasVersionedId)
		{
			StoreObjectId storeObjectId = id as StoreObjectId;
			if (storeObjectId != null)
			{
				wasVersionedId = false;
				return storeObjectId;
			}
			VersionedId versionedId = id as VersionedId;
			if (versionedId != null)
			{
				wasVersionedId = true;
				return versionedId.ObjectId;
			}
			wasVersionedId = false;
			return null;
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000CBF10 File Offset: 0x000CA110
		public static StoreObjectId GetAsStoreObjectId(StoreId id)
		{
			bool flag;
			return IdConverter.GetAsStoreObjectId(id, out flag);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000CBF28 File Offset: 0x000CA128
		public static FolderId ConvertStoreFolderIdToFolderId(StoreId storeFolderId, StoreSession session)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeFolderId, new IdAndSession(storeFolderId, session), null);
			return new FolderId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000CBF58 File Offset: 0x000CA158
		public static StoreId ConvertItemIdToStoreId(ItemId itemId, BasicTypes expectedType)
		{
			IdConverter.ConvertOption convertOption = IdConverter.ConvertOption.NoBind;
			if (string.IsNullOrEmpty(itemId.ChangeKey))
			{
				convertOption |= IdConverter.ConvertOption.IgnoreChangeKey;
			}
			string changeKey = null;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractIdInformation(itemId, convertOption, expectedType, out changeKey, null);
			Item item = null;
			return IdConverter.CreateAppropriateStoreIdType(null, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000CBF98 File Offset: 0x000CA198
		public static ItemId ConvertStoreItemIdToItemId(StoreId storeItemId, StoreSession session)
		{
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeItemId, new IdAndSession(storeItemId, session), null);
			return new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000CBFC8 File Offset: 0x000CA1C8
		public bool TryGetStoreIdAndMailboxGuidFromItemId(BaseItemId itemId, out StoreId storeId, out Guid mailboxGuid)
		{
			try
			{
				IdAndSession idAndSession = this.ConvertItemIdToIdAndSessionReadOnly(itemId);
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
				if (IdConverter.IsItemId(storeObjectId))
				{
					storeId = idAndSession.Id;
					mailboxGuid = idAndSession.Session.MailboxGuid;
					return true;
				}
			}
			catch (LocalizedException ex)
			{
				if (!IdConverter.ignoreableIdConversionExceptionTypes.Contains(ex.GetType()))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<LocalizedException>(0L, "IdConverter.TryGetItemIdAndMailboxGuidFromXml. Unexpected exception while trying to get ItemId and MailboxGuid from XML: {0}", ex);
				}
			}
			storeId = null;
			mailboxGuid = Guid.Empty;
			return false;
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000CC058 File Offset: 0x000CA258
		public Guid GetMailboxGuidFromFolderId(BaseFolderId folderId)
		{
			try
			{
				IdAndSession idAndSession = this.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey);
				return idAndSession.Session.MailboxGuid;
			}
			catch (LocalizedException ex)
			{
				if (!IdConverter.ignoreableIdConversionExceptionTypes.Contains(ex.GetType()))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<LocalizedException>(0L, "IdConverter.TryGetItemIdAndMailboxGuidFromXml. Unexpected exception while trying to get ItemId and MailboxGuid from XML: {0}", ex);
				}
			}
			return Guid.Empty;
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000CC0BC File Offset: 0x000CA2BC
		public IdAndSession ConvertDefaultFolderType(DefaultFolderType defaultFolderType, string emailAddress)
		{
			bool archiveSession = false;
			return IdConverter.ConvertDefaultFolderType(this.callContext, defaultFolderType, emailAddress, archiveSession);
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000CC0D9 File Offset: 0x000CA2D9
		public IdAndSession ConvertDefaultFolderType(DefaultFolderType defaultFolderType, MailboxId mailboxId, bool unifiedLogon)
		{
			return IdConverter.ConvertDefaultFolderType(this.callContext, defaultFolderType, mailboxId, unifiedLogon);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000CC0E9 File Offset: 0x000CA2E9
		public IdAndSession ConvertItemIdToIdAndSessionReadOnly(BaseItemId baseItemId)
		{
			return this.ConvertItemIdToIdAndSessionReadOnly(baseItemId, BasicTypes.Item);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x000CC0F3 File Offset: 0x000CA2F3
		public IdAndSession ConvertItemIdToIdAndSessionReadOnly(BaseItemId baseItemId, BasicTypes expectedType)
		{
			return this.ConvertItemIdToIdAndSession(baseItemId, IdConverter.ConvertOption.IgnoreChangeKey, expectedType);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000CC0FE File Offset: 0x000CA2FE
		public IdAndSession ConvertItemIdToIdAndSessionReadWrite(BaseItemId itemId)
		{
			return this.ConvertItemIdToIdAndSession(itemId, IdConverter.ConvertOption.None, BasicTypes.Item);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000CC10C File Offset: 0x000CA30C
		public IdAndSession ConvertItemIdToIdAndSessionReadOnly(string itemId)
		{
			IdHeaderInformation headerInformation = IdConverter.ConvertFromConcatenatedId(itemId, BasicTypes.Item, null, false);
			return IdConverter.ConvertId(this.callContext, headerInformation, IdConverter.ConvertOption.None, BasicTypes.Attachment, null, null, this.GetHashCode());
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000CC13C File Offset: 0x000CA33C
		public IdAndSession ConvertAttachmentIdToIdAndSessionReadOnly(string itemId)
		{
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			IdHeaderInformation headerInformation = IdConverter.ConvertFromConcatenatedId(itemId, BasicTypes.Attachment, attachmentIds, false);
			return IdConverter.ConvertId(this.callContext, headerInformation, IdConverter.ConvertOption.None, BasicTypes.Attachment, attachmentIds, null, this.GetHashCode());
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000CC170 File Offset: 0x000CA370
		public IdAndSession ConvertAttachmentIdToIdAndSessionReadOnly(AttachmentIdType attachmentId)
		{
			string id = attachmentId.GetId();
			if (id == null)
			{
				throw new ArgumentException("The given attachmentId does not contain an itemId.");
			}
			return this.ConvertAttachmentIdToIdAndSessionReadOnly(id);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000CC199 File Offset: 0x000CA399
		public IdAndSession ConvertFolderIdToIdAndSession(BaseFolderId folderId, IdConverter.ConvertOption convertOption)
		{
			return this.ConvertFolderIdToIdAndSession(folderId, convertOption, false, Guid.Empty);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000CC1AC File Offset: 0x000CA3AC
		public IdAndSession ConvertFolderIdToIdAndSession(BaseFolderId folderId, IdConverter.ConvertOption convertOption, bool unifiedLogon, Guid mailboxGuid)
		{
			DistinguishedFolderId distinguishedFolderId = folderId as DistinguishedFolderId;
			if (distinguishedFolderId != null)
			{
				string owaExplicitLogonUser = this.callContext.OwaExplicitLogonUser;
				string text = (distinguishedFolderId.Mailbox != null) ? distinguishedFolderId.Mailbox.EmailAddress : owaExplicitLogonUser;
				if (!string.IsNullOrEmpty(text) && this.callContext.AccessingPrincipal != null)
				{
					SmtpAddress primarySmtpAddress = this.callContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress;
					if (string.Equals(distinguishedFolderId.IdString, DistinguishedFolderIdName.calendar.ToString(), StringComparison.OrdinalIgnoreCase) && !primarySmtpAddress.Equals(new SmtpAddress(text)))
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.callContext.ProtocolLog, GetFolderMetadata.FolderType, "SharedCalendar");
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.callContext.ProtocolLog, GetFolderMetadata.Principal, primarySmtpAddress);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.callContext.ProtocolLog, GetFolderMetadata.MailboxTarget, text);
					}
				}
				return IdConverter.ConvertDistinguishedFolderId(this.callContext, distinguishedFolderId.IdString, distinguishedFolderId.ChangeKey, text, convertOption);
			}
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			IdHeaderInformation idHeaderInformation = IdConverter.ConvertFromConcatenatedId(folderId.GetId(), BasicTypes.Folder, attachmentIds, false);
			if (unifiedLogon)
			{
				idHeaderInformation.MailboxId = new MailboxId(mailboxGuid);
			}
			return IdConverter.ConvertId(this.callContext, idHeaderInformation, convertOption, BasicTypes.Folder, attachmentIds, folderId.GetChangeKey(), this.GetHashCode(), unifiedLogon);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000CC2F3 File Offset: 0x000CA4F3
		public IdAndSession ConvertFolderIdToIdAndSessionReadOnly(BaseFolderId folderId)
		{
			return this.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.AllowKnownExternalUsers);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000CC2FD File Offset: 0x000CA4FD
		public IdAndSession ConvertItemIdToIdAndSessionReadOnly(BaseItemId singleId, BasicTypes expectedType, bool deferBind, ref Item cachedItem)
		{
			return this.ConvertItemIdToIdAndSessionReadOnly(singleId, expectedType, deferBind, false, ref cachedItem);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000CC30C File Offset: 0x000CA50C
		public IdAndSession ConvertItemIdToIdAndSessionReadOnly(BaseItemId singleId, BasicTypes expectedType, bool deferBind, bool sessionOnly, ref Item cachedItem)
		{
			IdConverter.ConvertOption convertOption = IdConverter.ConvertOption.IgnoreChangeKey;
			if (deferBind)
			{
				convertOption |= IdConverter.ConvertOption.NoBind;
			}
			if (sessionOnly)
			{
				convertOption |= IdConverter.ConvertOption.SessionOnly;
			}
			return this.ConvertItemIdToIdAndSession(singleId, convertOption, expectedType, ref cachedItem);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000CC336 File Offset: 0x000CA536
		public IdAndSession ConvertTargetFolderIdToIdAndContentSession(BaseFolderId targetFolderId, bool allowKnownExternalUsers)
		{
			return this.ConvertTargetFolderIdToIdAndSession(targetFolderId, allowKnownExternalUsers, false);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000CC341 File Offset: 0x000CA541
		public IdAndSession ConvertTargetFolderIdToIdAndHierarchySession(BaseFolderId targetFolderId, bool allowKnownExternalUsers)
		{
			return this.ConvertTargetFolderIdToIdAndSession(targetFolderId, allowKnownExternalUsers, true);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000CC34C File Offset: 0x000CA54C
		private static bool IsFolderId(StoreObjectId storeObjectId)
		{
			return IdConverter.IsFolderObjectType(storeObjectId.ObjectType);
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000CC35C File Offset: 0x000CA55C
		private static IdHeaderInformation ExtractIdInformation(BaseItemId baseItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType, out string changeKey, List<AttachmentId> attachmentIds)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			IdHeaderInformation result = IdConverter.ConvertFromConcatenatedId(baseItemId.GetId(), expectedType, attachmentIds, false);
			changeKey = baseItemId.GetChangeKey();
			if (!flag)
			{
				if (changeKey == null)
				{
					throw new ChangeKeyRequiredException();
				}
				if (changeKey.Trim().Length == 0)
				{
					throw new InvalidChangeKeyException();
				}
			}
			return result;
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000CC3AC File Offset: 0x000CA5AC
		private static IdHeaderInformation ExtractOccurrenceOrRecurringIdInformation(string idValue, string changeKeyValue, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			IdHeaderInformation result = IdConverter.ConvertFromConcatenatedId(idValue, expectedType, null, false);
			if (!flag)
			{
				if (changeKeyValue == null)
				{
					throw new ChangeKeyRequiredException();
				}
				if (changeKeyValue.Trim().Length == 0)
				{
					throw new InvalidChangeKeyException();
				}
			}
			return result;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000CC3EC File Offset: 0x000CA5EC
		private static IdHeaderInformation ExtractOccurrenceOrRecurringIdInformation(BaseItemId itemId, IdConverter.ConvertOption convertOption, out string changeKey, BasicTypes expectedType, bool isOccurrence)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			string id = itemId.GetId();
			changeKey = (flag ? null : itemId.GetChangeKey());
			return IdConverter.ExtractOccurrenceOrRecurringIdInformation(id, changeKey, convertOption, expectedType);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000CC420 File Offset: 0x000CA620
		private static IdHeaderInformation ExtractOccurrenceIdInformation(OccurrenceItemId itemId, out string changeKey, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractOccurrenceOrRecurringIdInformation(itemId, convertOption, out changeKey, expectedType, true);
			idHeaderInformation.OccurrenceInstanceIndex = itemId.InstanceIndex;
			return idHeaderInformation;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000CC445 File Offset: 0x000CA645
		private static IdHeaderInformation ExtractRecurringMasterIdInformation(RecurringMasterItemId itemId, out string changeKey, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			return IdConverter.ExtractOccurrenceOrRecurringIdInformation(itemId, convertOption, out changeKey, expectedType, false);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000CC454 File Offset: 0x000CA654
		private static StoreId GetSessionSpecificId(StoreId storeId, StoreSession session)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			StoreObjectId sessionSpecificId = session.IdConverter.GetSessionSpecificId(storeObjectId);
			StoreId storeId2 = storeId;
			if (!storeObjectId.Equals(sessionSpecificId))
			{
				using (Folder folder = Folder.Bind(session, sessionSpecificId))
				{
					storeId2 = folder.Id;
					if (storeId is StoreObjectId)
					{
						storeId2 = StoreId.GetStoreObjectId(storeId2);
					}
				}
			}
			return storeId2;
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000CC4BC File Offset: 0x000CA6BC
		private static StoreId CreateAppropriateStoreIdType(StoreSession session, IdHeaderInformation idHeaderInformation, string changeKey, IdConverter.ConvertOption convertOption, BasicTypes expectedType, ref Item cachedItem)
		{
			if (changeKey == null)
			{
				return IdConverter.CreateStoreObjectId(session, idHeaderInformation.StoreIdBytes, StoreObjectType.Unknown, idHeaderInformation.IdProcessingInstruction, expectedType, convertOption, ref cachedItem);
			}
			if (changeKey.Trim().Length == 0)
			{
				throw new InvalidChangeKeyException();
			}
			byte[] array;
			StoreObjectType objectType;
			IdConverter.ParseChangeKeyString(changeKey, convertOption, out array, out objectType);
			if (array != null)
			{
				return IdConverter.CreateVersionedId(idHeaderInformation.StoreIdBytes, array, objectType, idHeaderInformation.IdProcessingInstruction, expectedType);
			}
			return IdConverter.CreateStoreObjectId(session, idHeaderInformation.StoreIdBytes, objectType, idHeaderInformation.IdProcessingInstruction, expectedType, convertOption, ref cachedItem);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000CC534 File Offset: 0x000CA734
		public static ConcatenatedIdAndChangeKey GetConcatenatedId(StoreId storeId, StoreObjectId parentStoreObjectId, MailboxId mailboxId, List<AttachmentId> attachmentIds)
		{
			IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
			byte[] realChangeKey = null;
			VersionedId versionedId = storeId as VersionedId;
			StoreObjectId storeObjectId;
			if (versionedId == null)
			{
				storeObjectId = (StoreObjectId)storeId;
			}
			else
			{
				storeObjectId = versionedId.ObjectId;
				realChangeKey = versionedId.ChangeKeyAsByteArray();
			}
			if (storeObjectId.ObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.GetBytes();
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Recurrence;
			}
			else
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.ProviderLevelItemId;
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Normal;
			}
			if (mailboxId != null)
			{
				if (mailboxId.IsVersionDependent)
				{
					if (ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2007_SP1))
					{
						idHeaderInformation.IdStorageType = IdStorageType.MailboxItemMailboxGuidBased;
					}
					else
					{
						idHeaderInformation.IdStorageType = IdStorageType.MailboxItemSmtpAddressBased;
					}
				}
				else if (mailboxId.MailboxGuid != null)
				{
					idHeaderInformation.IdStorageType = IdStorageType.MailboxItemMailboxGuidBased;
				}
				else if (mailboxId.SmtpAddress != null)
				{
					idHeaderInformation.IdStorageType = IdStorageType.MailboxItemSmtpAddressBased;
				}
				idHeaderInformation.MailboxId = mailboxId;
			}
			else if (IdConverter.IsFolderId(storeObjectId))
			{
				idHeaderInformation.IdStorageType = IdStorageType.PublicFolder;
			}
			else
			{
				idHeaderInformation.FolderIdBytes = parentStoreObjectId.ProviderLevelItemId;
				idHeaderInformation.IdStorageType = IdStorageType.PublicFolderItem;
			}
			string id = IdConverter.ConvertToConcatenatedId(idHeaderInformation, attachmentIds, true);
			StoreObjectType storeObjectType = storeObjectId.ObjectType;
			if (storeObjectType == StoreObjectType.Unknown && ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2007_SP1))
			{
				storeObjectType = StoreObjectType.Message;
			}
			string changeKey = IdConverter.BuildChangeKeyString(realChangeKey, storeObjectType);
			return new ConcatenatedIdAndChangeKey(id, changeKey);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000CC650 File Offset: 0x000CA850
		private static IdAndSession CreateIdAndSessionForItemId(StoreId itemId, StoreId folderId, IdStorageType idStorageType, StoreSession session)
		{
			switch (idStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
				return new IdAndSession(itemId, session);
			case IdStorageType.PublicFolderItem:
				return new IdAndSession(itemId, folderId, session);
			}
			return null;
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000CC689 File Offset: 0x000CA889
		private static void VerifyIdRepresentsItem(StoreObjectId storeObjectId)
		{
			if (IdConverter.IsFolderId(storeObjectId))
			{
				throw new CannotUseFolderIdForItemIdException();
			}
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x000CC699 File Offset: 0x000CA899
		private static void VerifyIdRepresentsFolder(StoreObjectId storeObjectId)
		{
			if (!IdConverter.IsFolderId(storeObjectId))
			{
				throw new CannotUseItemIdForFolderIdException();
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x000CC6AC File Offset: 0x000CA8AC
		public static string BuildChangeKeyString(byte[] realChangeKey, StoreObjectType objectType)
		{
			if (objectType == StoreObjectType.CalendarItemOccurrence)
			{
				objectType = StoreObjectType.CalendarItem;
			}
			int num = (realChangeKey != null) ? realChangeKey.Length : 0;
			int num2 = 4 + ((num == 0) ? 0 : 4) + num;
			byte[] array = new byte[num2];
			int num3 = 0;
			num3 += ExBitConverter.Write((int)objectType, array, num3);
			if (num != 0)
			{
				num3 += ExBitConverter.Write(num, array, num3);
				Array.Copy(realChangeKey, 0, array, num3, num);
				num3 += num;
			}
			return Convert.ToBase64String(array, 0, num2);
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x000CC710 File Offset: 0x000CA910
		private static void ParseChangeKeyString(string changeKey, IdConverter.ConvertOption convertOption, out byte[] realChangeKey, out StoreObjectType objectType)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			try
			{
				byte[] array = Convert.FromBase64String(changeKey);
				int num = array.Length;
				int num2 = 0;
				if (num < 4)
				{
					IdConverter.TraceDebug("[IdConverter::ParseChangeKeyString] change key was less than 4 bytes");
					throw new InvalidChangeKeyException();
				}
				objectType = (StoreObjectType)BitConverter.ToInt32(array, num2);
				num2 += 4;
				if (!EnumValidator.IsValidEnum<StoreObjectType>(objectType))
				{
					IdConverter.TraceDebug("[IdConverter::ParseChangeKeyString] ObjectType of change key was not valid enum value.");
					throw new InvalidChangeKeyException();
				}
				if (num2 == num || flag)
				{
					realChangeKey = null;
				}
				else
				{
					int num3 = BitConverter.ToInt32(array, num2);
					num2 += 4;
					if (num3 <= 0)
					{
						realChangeKey = null;
					}
					else
					{
						if (num3 > 512)
						{
							IdConverter.TraceDebug("[IdConverter::ParseChangeKeyString] change key was too long");
							throw new InvalidChangeKeyException();
						}
						if (num3 != num - num2)
						{
							IdConverter.TraceDebug("[IdConverter::ParseChangeKeyString] change key length did not match available bytes");
							throw new InvalidChangeKeyException();
						}
						realChangeKey = new byte[num3];
						Array.Copy(array, num2, realChangeKey, 0, num3);
						num2 += num3;
					}
				}
			}
			catch (FormatException innerException)
			{
				throw new InvalidChangeKeyException(innerException);
			}
			catch (ArgumentOutOfRangeException innerException2)
			{
				throw new InvalidChangeKeyException(innerException2);
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000CC80C File Offset: 0x000CAA0C
		public static StoreObjectType GetObjectTypeFromChangeKey(string changeKey)
		{
			byte[] array;
			StoreObjectType result;
			IdConverter.ParseChangeKeyString(changeKey, IdConverter.ConvertOption.IgnoreChangeKey, out array, out result);
			return result;
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000CC828 File Offset: 0x000CAA28
		private static string ConvertToConcatenatedId(IdHeaderInformation idHeaderInformation, List<AttachmentId> attachmentIds, bool attemptCompression)
		{
			string result;
			try
			{
				result = ServiceIdConverter.ConvertToConcatenatedId(idHeaderInformation, attachmentIds, attemptCompression);
			}
			catch (InvalidIdMonikerTooLongException innerException)
			{
				throw new InvalidStoreIdException(CoreResources.IDs.ErrorInvalidIdMonikerTooLong, innerException);
			}
			catch (InvalidIdStoreObjectIdTooLongException innerException2)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)2651121857U, innerException2);
			}
			catch (InvalidIdTooManyAttachmentLevelsException innerException3)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3632066599U, innerException3);
			}
			catch (InvalidIdMalformedException innerException4)
			{
				throw new InvalidStoreIdException((CoreResources.IDs)3107705007U, innerException4);
			}
			return result;
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x000CC8C0 File Offset: 0x000CAAC0
		internal static IdHeaderInformation ConvertFromConcatenatedId(string id, BasicTypes expectedType, List<AttachmentId> attachmentIds, bool isConversionMode)
		{
			IdHeaderInformation idHeaderInformation = null;
			try
			{
				idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(id, expectedType, attachmentIds);
			}
			catch (InvalidIdEmptyException exception)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException((CoreResources.IDs)4226852029U, expectedType, exception);
			}
			catch (InvalidIdNotAnItemAttachmentIdException exception2)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException(CoreResources.IDs.ErrorInvalidIdNotAnItemAttachmentId, expectedType, exception2);
			}
			catch (InvalidIdMalformedException exception3)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException((CoreResources.IDs)3107705007U, expectedType, exception3);
			}
			catch (InvalidIdException exception4)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException(CoreResources.IDs.ErrorInvalidId, expectedType, exception4);
			}
			catch (NonExistentMailboxGuidException ex)
			{
				throw new NonExistentMailboxException((CoreResources.IDs)3279543955U, ex.MailboxGuid.ToString());
			}
			if (!isConversionMode)
			{
				if (idHeaderInformation.IdStorageType == IdStorageType.MailboxItemSmtpAddressBased && ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2007_SP1))
				{
					throw new InvalidIdFormatVersionException();
				}
				if (idHeaderInformation.IdStorageType != IdStorageType.MailboxItemSmtpAddressBased && !ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2007_SP1))
				{
					throw new InvalidIdFormatVersionException(CoreResources.IDs.MessageInvalidIdMalformedEwsIdFormat);
				}
			}
			return idHeaderInformation;
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000CC9D4 File Offset: 0x000CABD4
		internal static Exception MapExpectedTypeToInvalidIdException(Enum messageId, BasicTypes expectedType, Exception exception)
		{
			if (expectedType == BasicTypes.Folder)
			{
				if (exception == null)
				{
					return new InvalidFolderIdException(messageId);
				}
				return new InvalidFolderIdException(messageId, exception);
			}
			else
			{
				if (exception == null)
				{
					return new InvalidStoreIdException(messageId);
				}
				return new InvalidStoreIdException(messageId, exception);
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000CC9FC File Offset: 0x000CABFC
		internal static Exception MapExpectedTypeToInvalidIdException(Enum messageId, BasicTypes expectedType)
		{
			return IdConverter.MapExpectedTypeToInvalidIdException(messageId, expectedType, null);
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000CCA06 File Offset: 0x000CAC06
		private static void TraceDebug(string message)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, message);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000CCA18 File Offset: 0x000CAC18
		private static VersionedId CreateVersionedId(byte[] storeObjectIdBytes, byte[] realChangeKeyBytes, StoreObjectType objectType, IdProcessingInstruction idProcessingInstruction, BasicTypes expectedType)
		{
			VersionedId result;
			try
			{
				VersionedId versionedId;
				switch (idProcessingInstruction)
				{
				case IdProcessingInstruction.Normal:
					versionedId = VersionedId.Deserialize(storeObjectIdBytes, realChangeKeyBytes, objectType);
					break;
				case IdProcessingInstruction.Recurrence:
					versionedId = VersionedId.Deserialize(storeObjectIdBytes, realChangeKeyBytes);
					break;
				default:
					versionedId = null;
					break;
				}
				if (expectedType == BasicTypes.Folder)
				{
					IdConverter.VerifyIdRepresentsFolder(versionedId.ObjectId);
				}
				else
				{
					IdConverter.VerifyIdRepresentsItem(versionedId.ObjectId);
				}
				result = versionedId;
			}
			catch (CorruptDataException exception)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException(CoreResources.IDs.ErrorInvalidId, expectedType, exception);
			}
			return result;
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000CCA94 File Offset: 0x000CAC94
		private static StoreObjectId CreateStoreObjectId(StoreSession session, byte[] storeObjectIdBytes, StoreObjectType objectType, IdProcessingInstruction idProcessingInstruction, BasicTypes expectedType, IdConverter.ConvertOption convertOption, ref Item cachedItem)
		{
			StoreObjectId result;
			try
			{
				StoreObjectId storeObjectId = IdConverter.GetAsStoreObjectId(IdConverter.BytesToStoreId(storeObjectIdBytes, idProcessingInstruction, objectType));
				if ((convertOption & IdConverter.ConvertOption.NoBind) != IdConverter.ConvertOption.NoBind)
				{
					storeObjectId = IdConverter.CorrectIdForObjectType(session, storeObjectId, objectType, ref cachedItem);
					if (expectedType == BasicTypes.Folder)
					{
						IdConverter.VerifyIdRepresentsFolder(storeObjectId);
					}
					else
					{
						IdConverter.VerifyIdRepresentsItem(storeObjectId);
					}
				}
				result = storeObjectId;
			}
			catch (ArgumentException exception)
			{
				throw IdConverter.MapExpectedTypeToInvalidIdException(CoreResources.IDs.ErrorInvalidId, expectedType, exception);
			}
			return result;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000CCAFC File Offset: 0x000CACFC
		private static StoreId BytesToStoreId(byte[] bytes, IdProcessingInstruction idProcessingInstruction, StoreObjectType objectType)
		{
			switch (idProcessingInstruction)
			{
			case IdProcessingInstruction.Normal:
				return StoreObjectId.FromProviderSpecificId(bytes, objectType);
			case IdProcessingInstruction.Recurrence:
				return StoreObjectId.Deserialize(bytes);
			default:
				return null;
			}
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000CCB2C File Offset: 0x000CAD2C
		private static BaseServerIdInfo GetPublicFolderServerInfoForAccessingPrincipal(CallContext callContext)
		{
			if (callContext.IsWSSecurityUser)
			{
				IdConverter.TraceDebug("Public folder access (root) not allowed for WS-Security users.");
				return null;
			}
			Guid empty = Guid.Empty;
			if (callContext.AccessingPrincipal != null)
			{
				if (callContext.AccessingPrincipal.MailboxInfo != null)
				{
					PublicFolderSession.TryGetHierarchyMailboxGuidForUser(callContext.AccessingPrincipal.MailboxInfo.OrganizationId, callContext.AccessingPrincipal.MailboxInfo.MailboxGuid, callContext.AccessingPrincipal.DefaultPublicFolderMailbox, out empty);
				}
				else if (callContext.AccessingPrincipal.RecipientType == RecipientType.MailUser && callContext.AccessingPrincipal.RecipientTypeDetails == (RecipientTypeDetails)((ulong)-2147483648))
				{
					if (callContext.HttpContext.Request.Headers != null && RemotePublicFolderOperations.CheckPublicFolderMailboxHeaderGuid(callContext.HttpContext.Request.Headers))
					{
						RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromMailboxHeaders(callContext, out empty);
					}
					else
					{
						RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromAccessingADUser(callContext, out empty);
					}
				}
			}
			if (empty != Guid.Empty)
			{
				return MailboxIdServerInfo.Create(new MailboxId(empty));
			}
			return null;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000CCC18 File Offset: 0x000CAE18
		private static IdHeaderInformation ExtractHeaderInformationFromConversationId(BaseItemId id)
		{
			return IdConverter.ConvertFromConcatenatedId(id.GetId(), BasicTypes.Item, null, false);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000CCC38 File Offset: 0x000CAE38
		private static StoreObjectId CorrectIdForObjectType(StoreSession session, StoreObjectId objectId, StoreObjectType deserializedObjectType, ref Item cachedItem)
		{
			if (deserializedObjectType != StoreObjectType.Unknown)
			{
				return objectId;
			}
			if (objectId.ObjectType == StoreObjectType.Folder)
			{
				using (Folder folder = Folder.Bind(session, objectId, null))
				{
					return IdConverter.GetAsStoreObjectId(folder.Id);
				}
			}
			cachedItem = ServiceCommandBase.GetXsoItem(session, objectId, new PropertyDefinition[0]);
			return IdConverter.GetAsStoreObjectId(cachedItem.Id);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000CCCA4 File Offset: 0x000CAEA4
		internal static IdAndSession ConvertDistinguishedFolderId(CallContext callContext, string displayName, string changeKey, string emailAddress)
		{
			return IdConverter.ConvertDistinguishedFolderId(callContext, displayName, changeKey, emailAddress, IdConverter.ConvertOption.None);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000CCCB0 File Offset: 0x000CAEB0
		internal static IdAndSession ConvertDistinguishedFolderId(CallContext callContext, string displayName, string changeKey, string emailAddress, IdConverter.ConvertOption convertOption)
		{
			IdAndSession idAndSession;
			if (displayName.Equals("publicfoldersroot", StringComparison.Ordinal) || displayName.Equals("internalsubmission", StringComparison.Ordinal))
			{
				if (!string.IsNullOrEmpty(emailAddress))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorMailboxCannotBeSpecifiedForPublicFolderRoot);
				}
				if (callContext.IsWSSecurityUser)
				{
					IdConverter.TraceDebug("[IdConverter::ConvertDistinguishedFolderId] PF support not available to WS-Security users.");
					throw new ServiceAccessDeniedException();
				}
				PublicFolderSession publicFolderSession = IdConverter.GetPublicFolderSession(null, callContext, convertOption);
				StoreId storeId = displayName.Equals("publicfoldersroot", StringComparison.Ordinal) ? publicFolderSession.GetIpmSubtreeFolderId() : publicFolderSession.GetInternalSubmissionFolderId();
				idAndSession = new IdAndSession(storeId, storeId, publicFolderSession);
			}
			else
			{
				if (!IdConverter.displayNameMap.Member.ContainsKey(displayName))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "ConvertDistinguishedFolderId. displayNameMap.Member doesn't contain displayName. displayName: {0}", displayName);
					throw new ServiceInvalidOperationException(CoreResources.IDs.UnrecognizedDistinguishedFolderName);
				}
				idAndSession = IdConverter.ConvertDefaultFolderType(callContext, IdConverter.displayNameMap.Member[displayName], emailAddress, IdConverter.displayNameIsArchive.Member.Contains(displayName));
			}
			if (changeKey != null)
			{
				try
				{
					if (changeKey.Trim().Length == 0)
					{
						throw new InvalidChangeKeyException();
					}
					idAndSession = new IdAndSession(VersionedId.Deserialize(idAndSession.Id.ToBase64String(), changeKey), idAndSession.Session);
				}
				catch (CorruptDataException innerException)
				{
					throw new InvalidChangeKeyException(innerException);
				}
			}
			return idAndSession;
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000CCDE8 File Offset: 0x000CAFE8
		internal static IdAndSession ConvertId(CallContext callContext, IdHeaderInformation headerInformation, IdConverter.ConvertOption convertOption, BasicTypes expectedType, List<AttachmentId> attachmentIds, string changeKey, int hashCode)
		{
			return IdConverter.ConvertId(callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, hashCode, false);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000CCDFC File Offset: 0x000CAFFC
		internal static IdAndSession ConvertId(CallContext callContext, IdHeaderInformation headerInformation, IdConverter.ConvertOption convertOption, BasicTypes expectedType, List<AttachmentId> attachmentIds, string changeKey, int hashCode, bool unifiedLogon)
		{
			Item item = null;
			IdAndSession result;
			try
			{
				result = IdConverter.ConvertId(callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, hashCode, unifiedLogon, ref item);
			}
			finally
			{
				if (item != null)
				{
					IdConverter.TraceDebug("ConvertId: leaked xsoItem, consider caching behavior");
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000CCE48 File Offset: 0x000CB048
		internal static IdAndSession ConvertId(CallContext callContext, IdHeaderInformation headerInformation, IdConverter.ConvertOption convertOption, BasicTypes expectedType, List<AttachmentId> attachmentIds, string changeKey, int hashCode, ref Item cachedItem)
		{
			return IdConverter.ConvertId(callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, hashCode, false, ref cachedItem);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000CCE68 File Offset: 0x000CB068
		internal static IdAndSession ConvertId(CallContext callContext, IdHeaderInformation headerInformation, IdConverter.ConvertOption convertOption, BasicTypes expectedType, List<AttachmentId> attachmentIds, string changeKey, int hashCode, bool unifiedLogon, ref Item cachedItem)
		{
			IdConverterDependencies dependencies = new IdConverterDependencies.FromCallContext(callContext);
			return IdConverter.ConvertId(dependencies, headerInformation, convertOption, expectedType, attachmentIds, changeKey, hashCode, unifiedLogon, ref cachedItem);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000CCE90 File Offset: 0x000CB090
		internal static IdAndSession ConvertId(IdConverterDependencies dependencies, IdHeaderInformation headerInformation, IdConverter.ConvertOption convertOption, BasicTypes expectedType, List<AttachmentId> attachmentIds, string changeKey, int hashCode, bool unifiedLogon, ref Item cachedItem)
		{
			switch (headerInformation.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
			{
				StoreSession storeSession;
				if (dependencies.IsExternalUser)
				{
					if (headerInformation.IdStorageType == IdStorageType.MailboxItemSmtpAddressBased)
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>((long)hashCode, "idConverter.ConvertId. MailboxId for external user doesn't contain MailboxGuid. MailboxId.SmtpAddress: {0}", headerInformation.MailboxId.SmtpAddress ?? "<Null>");
						throw new ServiceAccessDeniedException();
					}
					Guid mailboxGuid = new Guid(headerInformation.MailboxId.MailboxGuid);
					ExchangePrincipal fromCacheByGuid = ExchangePrincipalCache.GetFromCacheByGuid(mailboxGuid, dependencies.ADRecipientSessionContext);
					if (fromCacheByGuid == null)
					{
						throw new ServiceAccessDeniedException();
					}
					if (!DirectoryHelper.HasSharingPartnership(fromCacheByGuid.MailboxInfo.MailboxGuid, fromCacheByGuid.MailboxInfo.IsArchive, dependencies.ExternalId, fromCacheByGuid.MailboxInfo.OrganizationId.ToADSessionSettings().CreateRecipientSession(null)))
					{
						IdConverter.TraceDebug("[IdConverter::ConvertId] External user has never been given access by the mailbox owner.");
						throw new ServiceAccessDeniedException();
					}
					storeSession = dependencies.GetSystemMailboxSession(headerInformation, unifiedLogon);
				}
				else
				{
					storeSession = dependencies.GetMailboxSession(headerInformation, unifiedLogon);
				}
				if ((convertOption & IdConverter.ConvertOption.SessionOnly) == IdConverter.ConvertOption.SessionOnly)
				{
					return new IdAndSession(StoreObjectId.DummyId, storeSession);
				}
				StoreId storeId = IdConverter.CreateAppropriateStoreIdType(storeSession, headerInformation, changeKey, convertOption, expectedType, ref cachedItem);
				if (dependencies.IsExternalUser)
				{
					StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
					StoreObjectId targetFolderId;
					if (!IdConverter.IsFolderId(storeObjectId))
					{
						targetFolderId = IdConverter.GetParentIdFromMessageId(storeObjectId);
					}
					else
					{
						targetFolderId = storeObjectId;
					}
					Permission externalUserPermissions = ExternalUserHandler.GetExternalUserPermissions(dependencies.ExternalId, (MailboxSession)storeSession, targetFolderId, dependencies.UserIdForTracing);
					if (externalUserPermissions == null)
					{
						IdConverter.TraceDebug("[IdConverter::ConvertId] External user has never been given access to the folder.");
						throw new ServiceAccessDeniedException();
					}
					if ((convertOption & IdConverter.ConvertOption.AllowKnownExternalUsers) == IdConverter.ConvertOption.AllowKnownExternalUsers)
					{
						IdConverter.TraceDebug("[IdConverter::ConvertId] External user is known.");
						return new ExternalUserIdAndSession(storeId, storeSession, externalUserPermissions);
					}
					if (!ExternalUserHandler.HasPermission(externalUserPermissions))
					{
						IdConverter.TraceDebug("[IdConverter::ConvertId] External user does not have access to the folder.");
						throw new ServiceAccessDeniedException();
					}
					return new ExternalUserIdAndSession(storeId, storeSession, externalUserPermissions);
				}
				else
				{
					if (attachmentIds == null)
					{
						return new IdAndSession(storeId, storeSession);
					}
					return new IdAndSession(storeId, storeSession, attachmentIds);
				}
				break;
			}
			case IdStorageType.PublicFolder:
			{
				if (dependencies.IsExternalUser || dependencies.IsWSSecurityUser)
				{
					IdConverter.TraceDebug("[IdConverter::ConvertId] Public Folder access is not supported for external or WS-Security users.");
					throw new ServiceAccessDeniedException();
				}
				StoreId storeId2 = StoreObjectId.FromProviderSpecificId(headerInformation.StoreIdBytes, StoreObjectType.Folder);
				if (dependencies.IsOwa)
				{
					storeId2 = StoreObjectId.ToNormalizedPublicFolderId((StoreObjectId)storeId2);
				}
				StoreSession storeSession = dependencies.GetPublicFolderSession(storeId2, convertOption);
				if ((convertOption & IdConverter.ConvertOption.SessionOnly) == IdConverter.ConvertOption.SessionOnly)
				{
					return new IdAndSession(StoreObjectId.DummyId, storeId2, storeSession);
				}
				StoreId storeId = IdConverter.CreateAppropriateStoreIdType(storeSession, headerInformation, changeKey, convertOption, expectedType, ref cachedItem);
				return new IdAndSession(storeId, storeId, storeSession, attachmentIds);
			}
			case IdStorageType.PublicFolderItem:
			{
				if (dependencies.IsExternalUser || dependencies.IsWSSecurityUser)
				{
					IdConverter.TraceDebug("[IdConverter::ConvertId] Public Folder Item access is not supported for external or WS-Security users.");
					throw new ServiceAccessDeniedException();
				}
				StoreId storeId2 = StoreObjectId.FromProviderSpecificId(headerInformation.FolderIdBytes, StoreObjectType.Folder);
				StoreSession storeSession = dependencies.GetPublicFolderSession(storeId2, convertOption);
				if ((convertOption & IdConverter.ConvertOption.SessionOnly) == IdConverter.ConvertOption.SessionOnly)
				{
					return new IdAndSession(StoreObjectId.DummyId, storeId2, storeSession);
				}
				StoreId storeId = IdConverter.CreateAppropriateStoreIdType(storeSession, headerInformation, changeKey, convertOption, expectedType, ref cachedItem);
				return new IdAndSession(storeId, storeId2, storeSession, attachmentIds);
			}
			case IdStorageType.ConversationIdMailboxGuidBased:
				throw new ServiceInvalidOperationException((CoreResources.IDs)3426540703U);
			default:
				return null;
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000CD14C File Offset: 0x000CB34C
		internal static PublicFolderSession GetPublicFolderSession(StoreId folderId, CallContext callContext, IdConverter.ConvertOption convertOption)
		{
			OrganizationId organizationId = (callContext.AccessingADUser != null) ? callContext.AccessingADUser.OrganizationId : OrganizationId.ForestWideOrgId;
			if (PublicFolderSession.CheckIfPublicFolderMailboxLockedForMigration(organizationId))
			{
				throw new MailboxInTransitException(ServerStrings.PublicFoldersCannotBeAccessedDuringCompletion);
			}
			bool flag = false;
			if ((convertOption & IdConverter.ConvertOption.IsHierarchicalOperation) != IdConverter.ConvertOption.IsHierarchicalOperation && folderId != null)
			{
				flag = true;
			}
			Guid empty = Guid.Empty;
			bool flag2;
			if (callContext.AccessingPrincipal is RemoteUserMailboxPrincipal)
			{
				flag2 = RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromAccessingADUser(callContext, out empty);
			}
			else
			{
				flag2 = RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromMailboxHeaders(callContext, out empty);
			}
			if (flag2)
			{
				if (flag)
				{
					callContext.SessionCache.GetPublicFolderSession(empty);
					return callContext.SessionCache.GetPublicFolderSession(folderId);
				}
			}
			else
			{
				if (flag)
				{
					return callContext.SessionCache.GetPublicFolderSession(folderId);
				}
				if (callContext.AccessingPrincipal == null || !PublicFolderSession.TryGetHierarchyMailboxGuidForUser(callContext.AccessingPrincipal.MailboxInfo.OrganizationId, callContext.AccessingPrincipal.MailboxInfo.MailboxGuid, callContext.AccessingPrincipal.DefaultPublicFolderMailbox, out empty))
				{
					throw new NoPublicFolderServerAvailableException();
				}
			}
			return callContext.SessionCache.GetPublicFolderSession(empty);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000CD239 File Offset: 0x000CB439
		internal IdAndSession ConvertItemIdToIdAndSession(BaseItemId baseItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			if (baseItemId is OccurrenceItemId)
			{
				return this.ConvertOccurrenceId(baseItemId as OccurrenceItemId, convertOption, expectedType);
			}
			if (baseItemId is RecurringMasterItemId)
			{
				return this.ConvertRecurringMasterId(baseItemId as RecurringMasterItemId, convertOption, expectedType);
			}
			return this.ConvertId(baseItemId, convertOption, expectedType);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000CD274 File Offset: 0x000CB474
		internal IdAndSession ConvertItemIdToIdAndSession(BaseItemId baseItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType, ref Item cachedItem)
		{
			if (baseItemId is OccurrenceItemId)
			{
				return this.ConvertOccurrenceId(baseItemId as OccurrenceItemId, convertOption, expectedType);
			}
			if (baseItemId is RecurringMasterItemId)
			{
				return this.ConvertRecurringMasterId(baseItemId as RecurringMasterItemId, convertOption, expectedType);
			}
			return this.ConvertId(baseItemId, convertOption, expectedType, ref cachedItem);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000CD2BC File Offset: 0x000CB4BC
		internal static BaseServerIdInfo GetServerInfoForObjectId(CallContext callContext, BasicTypes objectType, ServiceObjectId objectId, bool isHierarchyOperations)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			IdHeaderInformation header = IdConverter.ConvertFromConcatenatedId(objectId.GetId(), objectType, null, false);
			return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header, isHierarchyOperations);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000CD2FC File Offset: 0x000CB4FC
		internal static BaseServerIdInfo GetServerInfoForItemId(CallContext callContext, BaseItemId itemId)
		{
			return IdConverter.GetServerInfoForObjectId(callContext, BasicTypes.Item, itemId, false);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000CD307 File Offset: 0x000CB507
		internal static BaseServerIdInfo GetServerInfoForAttachmentId(CallContext callContext, AttachmentIdType attachmentId)
		{
			return IdConverter.GetServerInfoForObjectId(callContext, BasicTypes.Attachment, attachmentId, false);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000CD312 File Offset: 0x000CB512
		internal static BaseServerIdInfo GetServerInfoForFolderId(CallContext callContext, BaseFolderId folderId, bool isHierarchyOperations)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			return folderId.GetServerInfo(isHierarchyOperations);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000CD32C File Offset: 0x000CB52C
		internal static BaseServerIdInfo GetServerInfoForDistinguishedFolderId(CallContext callContext, DistinguishedFolderId distinguishedFolderId, bool isHierarchyOperations)
		{
			if (distinguishedFolderId.Id == DistinguishedFolderIdName.publicfoldersroot)
			{
				return IdConverter.GetPublicFolderServerInfoForAccessingPrincipal(callContext);
			}
			string text = (distinguishedFolderId.Mailbox != null) ? distinguishedFolderId.Mailbox.EmailAddress : callContext.OwaExplicitLogonUser;
			if (IdConverter.IsArchiveDistinguishedFolderId(distinguishedFolderId))
			{
				ADUser aduser;
				if (!string.IsNullOrEmpty(text))
				{
					ADIdentityInformationCache.Singleton.TryGetADUser(text, callContext.EffectiveCaller.GetADRecipientSessionContext(), out aduser);
				}
				else
				{
					ADIdentityInformationCache.Singleton.TryGetADUser(callContext.EffectiveCallerSid, callContext.EffectiveCaller.GetADRecipientSessionContext(), out aduser);
				}
				if (aduser != null && aduser.ArchiveGuid != Guid.Empty)
				{
					return MailboxIdServerInfo.Create(new MailboxId(aduser.ArchiveGuid, true));
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return MailboxIdServerInfo.Create(text);
			}
			return callContext.GetServerInfoForEffectiveCaller();
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000CD3EB File Offset: 0x000CB5EB
		internal static BaseServerIdInfo GetServerInfoForCallContext(CallContext callContext)
		{
			if (!string.IsNullOrEmpty(callContext.OwaExplicitLogonUser))
			{
				return MailboxIdServerInfo.Create(callContext.OwaExplicitLogonUser);
			}
			return callContext.GetServerInfoForEffectiveCaller();
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000CD40C File Offset: 0x000CB60C
		internal static BaseServerIdInfo ServerInfoFromIdHeaderInformation(CallContext callContext, IdHeaderInformation header, bool isHierarchyOperations)
		{
			switch (header.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
				return MailboxIdServerInfo.Create(header.MailboxId.SmtpAddress);
			case IdStorageType.PublicFolder:
			case IdStorageType.PublicFolderItem:
			{
				Guid folderReplica;
				bool flag;
				if (callContext.AccessingPrincipal is RemoteUserMailboxPrincipal)
				{
					flag = RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromAccessingADUser(callContext, out folderReplica);
				}
				else
				{
					flag = RemotePublicFolderOperations.TryGetPublicFolderMailboxGuidFromMailboxHeaders(callContext, out folderReplica);
				}
				if (!flag && !isHierarchyOperations)
				{
					byte[] array = header.FolderIdBytes ?? header.StoreIdBytes;
					if (array != null)
					{
						folderReplica = RemotePublicFolderOperations.GetFolderReplica(callContext, array);
						if (folderReplica != Guid.Empty)
						{
							RemotePublicFolderOperations.StampPublicFolderMailboxHeader(callContext, folderReplica);
						}
					}
				}
				if (folderReplica != Guid.Empty)
				{
					return MailboxIdServerInfo.Create(new MailboxId(folderReplica));
				}
				return IdConverter.GetPublicFolderServerInfoForAccessingPrincipal(callContext);
			}
			case IdStorageType.MailboxItemMailboxGuidBased:
			case IdStorageType.ConversationIdMailboxGuidBased:
				return MailboxIdServerInfo.Create(header.MailboxId);
			default:
				return null;
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000CD4D4 File Offset: 0x000CB6D4
		public IdAndSession ConvertConversationIdToIdAndSession(BaseItemId id)
		{
			return this.ConvertConversationIdToIdAndSession(id, false);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000CD4E0 File Offset: 0x000CB6E0
		public IdAndSession ConvertConversationIdToIdAndSession(BaseItemId id, bool archiveSession)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractHeaderInformationFromConversationId(id);
			ConversationId storeId = ConversationId.Create(idHeaderInformation.StoreIdBytes);
			if (this.callContext.IsExternalUser || this.callContext.IsWSSecurityUser)
			{
				IdConverter.TraceDebug("[IdConverter::ConvertConversationIdXmlToIdAndSession] Conversation access is not supported for external or WS-Security users.");
				throw new ServiceAccessDeniedException();
			}
			MailboxSession session;
			if (archiveSession)
			{
				session = this.callContext.SessionCache.GetMailboxIdentityMailboxSession(true);
			}
			else
			{
				session = this.callContext.SessionCache.GetMailboxSessionByMailboxId(idHeaderInformation.MailboxId);
			}
			return new IdAndSession(storeId, session);
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000CD564 File Offset: 0x000CB764
		private IdAndSession ConvertTargetFolderIdToIdAndSession(BaseFolderId folderId, bool allowKnownExternalUsers, bool isHierarchicalOperation)
		{
			IdAndSession idAndSession = this.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey | (allowKnownExternalUsers ? IdConverter.ConvertOption.AllowKnownExternalUsers : IdConverter.ConvertOption.None) | (isHierarchicalOperation ? IdConverter.ConvertOption.IsHierarchicalOperation : IdConverter.ConvertOption.None));
			if (!string.IsNullOrEmpty(folderId.GetChangeKey()))
			{
				using (Folder.Bind(idAndSession.Session, idAndSession.Id, null))
				{
				}
			}
			return idAndSession;
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000CD5C8 File Offset: 0x000CB7C8
		private IdAndSession ConvertId(BaseItemId baseItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			string changeKey;
			IdHeaderInformation headerInformation = IdConverter.ExtractIdInformation(baseItemId, convertOption, expectedType, out changeKey, attachmentIds);
			return IdConverter.ConvertId(this.callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, this.GetHashCode());
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000CD600 File Offset: 0x000CB800
		private IdAndSession ConvertId(BaseItemId baseItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType, ref Item cachedItem)
		{
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			string changeKey;
			IdHeaderInformation headerInformation = IdConverter.ExtractIdInformation(baseItemId, convertOption, expectedType, out changeKey, attachmentIds);
			return IdConverter.ConvertId(this.callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, this.GetHashCode(), ref cachedItem);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000CD638 File Offset: 0x000CB838
		private IdAndSession ConvertOccurrenceId(OccurrenceItemId occurrenceItemId, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			string changeKey;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractOccurrenceIdInformation(occurrenceItemId, out changeKey, convertOption, expectedType);
			StoreSession session = null;
			StoreId storeId = null;
			switch (idHeaderInformation.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
				session = this.callContext.SessionCache.GetMailboxSessionByMailboxId(idHeaderInformation.MailboxId);
				goto IL_68;
			case IdStorageType.PublicFolderItem:
				storeId = StoreObjectId.FromProviderSpecificId(idHeaderInformation.FolderIdBytes, StoreObjectType.Folder);
				session = IdConverter.GetPublicFolderSession(storeId, this.callContext, convertOption);
				goto IL_68;
			}
			return null;
			IL_68:
			Item item = null;
			IdAndSession result;
			try
			{
				StoreId id = IdConverter.CreateAppropriateStoreIdType(session, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
				if (idHeaderInformation.OccurrenceInstanceIndex <= 0)
				{
					throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
				}
				if (item == null)
				{
					item = ServiceCommandBase.GetXsoItem(session, id, new PropertyDefinition[0]);
				}
				CalendarItem calendarItem = item as CalendarItem;
				if (calendarItem == null || calendarItem.Recurrence == null)
				{
					throw new CalendarExceptionCannotUseIdForRecurringMasterId();
				}
				OccurrenceInfo occurrenceInfo = calendarItem.Recurrence.GetFirstOccurrence();
				OccurrenceInfo occurrenceInfo2 = (calendarItem.Recurrence.Range is NoEndRecurrenceRange) ? null : calendarItem.Recurrence.GetLastOccurrence();
				ExDateTime[] deletedOccurrences = calendarItem.Recurrence.GetDeletedOccurrences();
				int num = 0;
				bool flag = true;
				bool flag2 = deletedOccurrences != null && deletedOccurrences.Length > 0;
				int num2 = 1;
				bool flag3;
				for (;;)
				{
					if (flag2)
					{
						flag3 = (!flag || deletedOccurrences[num] < occurrenceInfo.OriginalStartTime);
					}
					else
					{
						if (!flag)
						{
							break;
						}
						flag3 = false;
					}
					if (num2 == idHeaderInformation.OccurrenceInstanceIndex)
					{
						goto IL_1C9;
					}
					if (flag2 && flag3)
					{
						num++;
						if (num >= deletedOccurrences.Length)
						{
							flag2 = false;
						}
					}
					else if (occurrenceInfo2 != null && occurrenceInfo.OriginalStartTime == occurrenceInfo2.OriginalStartTime)
					{
						flag = false;
					}
					else
					{
						try
						{
							ExDateTime originalStartTime = occurrenceInfo.OriginalStartTime;
							occurrenceInfo = calendarItem.Recurrence.GetNextOccurrence(occurrenceInfo);
							if (occurrenceInfo.OriginalStartTime == originalStartTime)
							{
								flag = false;
							}
						}
						catch (ArgumentOutOfRangeException)
						{
							throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
						}
					}
					num2++;
				}
				throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
				IL_1C9:
				if (flag3)
				{
					throw new CalendarExceptionOccurrenceIsDeletedFromRecurrence();
				}
				switch (idHeaderInformation.IdStorageType)
				{
				case IdStorageType.MailboxItemSmtpAddressBased:
				case IdStorageType.MailboxItemMailboxGuidBased:
					return new IdAndSession(occurrenceInfo.VersionedId, session);
				case IdStorageType.PublicFolderItem:
					return new IdAndSession(occurrenceInfo.VersionedId, storeId, session);
				}
				result = null;
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000CD8AC File Offset: 0x000CBAAC
		private IdAndSession ConvertRecurringMasterId(RecurringMasterItemId recurringMasterId, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			string changeKey;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractRecurringMasterIdInformation(recurringMasterId, out changeKey, convertOption, expectedType);
			StoreId folderId = null;
			StoreSession storeSessionFromIdHeaderInformation = this.GetStoreSessionFromIdHeaderInformation(idHeaderInformation, out folderId, convertOption);
			Item item = null;
			IdAndSession result;
			try
			{
				StoreId id = IdConverter.CreateAppropriateStoreIdType(storeSessionFromIdHeaderInformation, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
				if (item == null)
				{
					item = ServiceCommandBase.GetXsoItem(storeSessionFromIdHeaderInformation, id, new PropertyDefinition[0]);
				}
				CalendarItemOccurrence calendarItemOccurrence = item as CalendarItemOccurrence;
				if (calendarItemOccurrence == null)
				{
					throw new CalendarExceptionCannotUseIdForOccurrenceId();
				}
				using (CalendarItem master = calendarItemOccurrence.GetMaster())
				{
					result = IdConverter.CreateIdAndSessionForItemId(master.Id, folderId, idHeaderInformation.IdStorageType, storeSessionFromIdHeaderInformation);
				}
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000CD960 File Offset: 0x000CBB60
		private StoreSession GetStoreSessionFromIdHeaderInformation(IdHeaderInformation headerInformation, out StoreId folderId, IdConverter.ConvertOption convertOption)
		{
			StoreSession result = null;
			folderId = null;
			switch (headerInformation.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
				result = this.callContext.SessionCache.GetMailboxSessionByMailboxId(headerInformation.MailboxId);
				break;
			case IdStorageType.PublicFolder:
			case IdStorageType.PublicFolderItem:
				folderId = StoreObjectId.FromProviderSpecificId(headerInformation.FolderIdBytes ?? headerInformation.StoreIdBytes, StoreObjectType.Folder);
				result = IdConverter.GetPublicFolderSession(folderId, this.callContext, convertOption);
				break;
			}
			return result;
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000CD9D2 File Offset: 0x000CBBD2
		public static void CreateStoreIdXml(XmlElement idParentElement, StoreId storeId, IdAndSession idAndSession, string elementName)
		{
			IdConverter.CreateStoreIdXml(idParentElement, storeId, idAndSession, elementName, ServiceXml.DefaultNamespaceUri);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000CD9E4 File Offset: 0x000CBBE4
		public static void CreateStoreIdXml(XmlElement idParentElement, StoreId storeId, IdAndSession idAndSession, string elementName, string namespaceUri)
		{
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				IdConverter.CreateStoreIdXml(idParentElement, storeId, new MailboxId(mailboxSession), elementName, namespaceUri);
				return;
			}
			XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, namespaceUri);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, idAndSession, null);
			IdConverter.AddIdAttributes(idElement, concatenatedId, "Id", "ChangeKey");
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000CDA35 File Offset: 0x000CBC35
		public static void CreateStoreIdXml(XmlElement idParentElement, StoreId storeId, MailboxId mailboxId, string elementName)
		{
			IdConverter.CreateStoreIdXml(idParentElement, storeId, mailboxId, elementName, ServiceXml.DefaultNamespaceUri);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000CDA48 File Offset: 0x000CBC48
		public static void CreateStoreIdXml(XmlElement idParentElement, StoreId storeId, MailboxId mailboxId, string elementName, string namespaceUri)
		{
			XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, namespaceUri);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, mailboxId, null);
			IdConverter.AddIdAttributes(idElement, concatenatedId, "Id", "ChangeKey");
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000CDA7C File Offset: 0x000CBC7C
		public static void CreatePublicFolderItemIdXml(XmlElement idParentElement, StoreObjectId storeObjectId, StoreObjectId parentStoreObjectId, string elementName)
		{
			XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, ServiceXml.DefaultNamespaceUri);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeObjectId, parentStoreObjectId, null, null);
			IdConverter.AddIdAttributes(idElement, concatenatedId, "Id", "ChangeKey");
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000CDAB4 File Offset: 0x000CBCB4
		public static void CreatePublicFolderIdXml(XmlElement idParentElement, StoreObjectId storeObjectId, string elementName)
		{
			XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, ServiceXml.DefaultNamespaceUri);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeObjectId, null, null, null);
			IdConverter.AddIdAttributes(idElement, concatenatedId, "Id", "ChangeKey");
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000CDAE9 File Offset: 0x000CBCE9
		public static void AddIdAttributes(XmlElement idElement, ConcatenatedIdAndChangeKey idAndChangeKey, string idAttributeName, string changeKeyAttributeName)
		{
			ServiceXml.CreateAttribute(idElement, idAttributeName, idAndChangeKey.Id);
			if (!string.IsNullOrEmpty(idAndChangeKey.ChangeKey))
			{
				ServiceXml.CreateAttribute(idElement, changeKeyAttributeName, idAndChangeKey.ChangeKey);
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000CDB18 File Offset: 0x000CBD18
		public static void CreateConversationIdXml(XmlElement idParentElement, ConversationId conversationId, IdAndSession idAndSession, string elementName)
		{
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, ServiceXml.DefaultNamespaceUri);
				string id = IdConverter.ConversationIdToEwsId(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, conversationId);
				ConcatenatedIdAndChangeKey idAndChangeKey = new ConcatenatedIdAndChangeKey(id, null);
				IdConverter.AddIdAttributes(idElement, idAndChangeKey, "Id", "ChangeKey");
			}
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000CDB74 File Offset: 0x000CBD74
		public static void CreatePersonIdXml(XmlElement idParentElement, PersonId personId, IdAndSession idAndSession, string elementName)
		{
			MailboxSession mailboxSession = idAndSession.Session as MailboxSession;
			if (mailboxSession != null)
			{
				XmlElement idElement = IdConverter.AddIdElement(idParentElement, elementName, ServiceXml.DefaultNamespaceUri);
				string id = IdConverter.PersonIdToEwsId(mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, personId);
				ConcatenatedIdAndChangeKey idAndChangeKey = new ConcatenatedIdAndChangeKey(id, null);
				IdConverter.AddIdAttributes(idElement, idAndChangeKey, "Id", "ChangeKey");
			}
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000CDBCE File Offset: 0x000CBDCE
		public static StoreId ConvertXmlToStoreId(XmlElement singleIdXml, MailboxSession session, BasicTypes expectedType)
		{
			return IdConverter.ConvertXmlToStoreId(singleIdXml, session, IdConverter.ConvertOption.IgnoreChangeKey, expectedType);
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000CDBD9 File Offset: 0x000CBDD9
		public static StoreId ConvertXmlToStoreIdNoBind(XmlElement singleIdXml, MailboxSession session, BasicTypes expectedType)
		{
			return IdConverter.ConvertXmlToStoreId(singleIdXml, session, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.NoBind, expectedType);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x000CDBE4 File Offset: 0x000CBDE4
		public static StoreId[] ConvertChildXmlToStoreIds(XmlElement parentOfIdsXml, MailboxSession session, BasicTypes expectedTypes)
		{
			StoreId[] array = new StoreId[parentOfIdsXml.ChildNodes.Count];
			int num = 0;
			foreach (object obj in parentOfIdsXml.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				array[num++] = IdConverter.ConvertXmlToStoreId((XmlElement)xmlNode, session, expectedTypes);
			}
			return array;
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000CDC64 File Offset: 0x000CBE64
		public bool TryGetItemIdAndMailboxGuidFromXml(XmlElement objectIdXml, out StoreId itemId, out Guid mailboxGuid)
		{
			try
			{
				IdAndSession idAndSession = this.ConvertXmlToIdAndSessionReadOnly(objectIdXml, BasicTypes.Item);
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId(idAndSession.Id);
				if (IdConverter.IsItemId(storeObjectId))
				{
					itemId = idAndSession.Id;
					mailboxGuid = idAndSession.Session.MailboxGuid;
					return true;
				}
			}
			catch (LocalizedException ex)
			{
				if (!IdConverter.ignoreableIdConversionExceptionTypes.Contains(ex.GetType()))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<LocalizedException>(0L, "IdConverter.TryGetItemIdAndMailboxGuidFromXml. Unexpected exception while trying to get ItemId and MailboxGuid from XML: {0}", ex);
				}
			}
			itemId = null;
			mailboxGuid = Guid.Empty;
			return false;
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000CDCF8 File Offset: 0x000CBEF8
		public IdAndSession ConvertXmlToIdAndSessionReadOnly(XmlElement singleIdXml, BasicTypes expectedType)
		{
			return this.ConvertXmlToIdAndSessionReadOnly(singleIdXml, expectedType, false);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x000CDD03 File Offset: 0x000CBF03
		public IdAndSession ConvertXmlToIdAndSessionReadOnly(XmlElement singleIdXml, BasicTypes expectedType, bool deferBind)
		{
			return this.ConvertXmlToIdAndSessionReadOnly(singleIdXml, expectedType, deferBind, false);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x000CDD10 File Offset: 0x000CBF10
		public IdAndSession ConvertXmlToIdAndSessionReadOnly(XmlElement singleIdXml, BasicTypes expectedType, bool deferBind, bool sessionOnly)
		{
			IdConverter.ConvertOption convertOption = IdConverter.ConvertOption.IgnoreChangeKey;
			if (deferBind)
			{
				convertOption |= IdConverter.ConvertOption.NoBind;
			}
			if (sessionOnly)
			{
				convertOption |= IdConverter.ConvertOption.SessionOnly;
			}
			return this.ConvertXmlToIdAndSession(singleIdXml, convertOption, expectedType);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x000CDD38 File Offset: 0x000CBF38
		public IdAndSession ConvertXmlToIdAndSessionReadOnly(XmlElement singleIdXml, BasicTypes expectedType, bool deferBind, ref Item cachedItem)
		{
			IdConverter.ConvertOption convertOption = IdConverter.ConvertOption.IgnoreChangeKey;
			if (deferBind)
			{
				convertOption |= IdConverter.ConvertOption.NoBind;
			}
			return this.ConvertXmlToIdAndSession(singleIdXml, convertOption, expectedType, ref cachedItem);
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x000CDD59 File Offset: 0x000CBF59
		private static StoreId ConvertXmlToStoreId(XmlElement singleIdXml, MailboxSession session, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			if (IdConverter.IsDistinguishedFolderIdXml(singleIdXml))
			{
				return IdConverter.ConvertDistinguishedFolderId(singleIdXml, session, convertOption);
			}
			if (IdConverter.IsIdXml(singleIdXml))
			{
				return IdConverter.ConvertId(singleIdXml, session, convertOption, expectedType);
			}
			return null;
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x000CDD7F File Offset: 0x000CBF7F
		private static bool IsDistinguishedFolderIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "DistinguishedFolderId";
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x000CDD94 File Offset: 0x000CBF94
		internal static bool IsIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "FolderId" || idElement.LocalName == "ParentFolderId" || idElement.LocalName == "ItemId" || idElement.LocalName == "ReferenceItemId" || idElement.LocalName == "ParentItemId" || idElement.LocalName == "AttachmentId" || idElement.LocalName == "IdOfFolderToShare" || idElement.LocalName == "SharingFolderId";
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000CDE31 File Offset: 0x000CC031
		private static bool IsUploadIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "Item" && idElement.ChildNodes.Count > 0 && idElement.ChildNodes[0].LocalName == "ParentFolderId";
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x000CDE70 File Offset: 0x000CC070
		private static bool IsConversationIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "ConversationId";
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x000CDE82 File Offset: 0x000CC082
		private static bool IsOccurrenceIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "OccurrenceItemId";
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x000CDE94 File Offset: 0x000CC094
		private static bool IsRecurringMasterIdXml(XmlElement idElement)
		{
			return idElement.LocalName == "RecurringMasterItemId";
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x000CDEA8 File Offset: 0x000CC0A8
		private static void ExtractDistinguishedFolderInformation(XmlElement folderIdElement, IdConverter.ConvertOption convertOption, out string displayName, out string changeKey, out string emailAddress)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			XmlAttribute xmlAttribute = (XmlAttribute)folderIdElement.Attributes.GetNamedItem("Id");
			XmlAttribute xmlAttribute2 = flag ? null : ((XmlAttribute)folderIdElement.Attributes.GetNamedItem("ChangeKey"));
			displayName = xmlAttribute.Value;
			changeKey = ((xmlAttribute2 == null) ? null : xmlAttribute2.Value);
			emailAddress = null;
			if (!flag)
			{
				if (changeKey == null)
				{
					throw new ChangeKeyRequiredException();
				}
				if (changeKey.Trim().Length == 0)
				{
					throw new InvalidChangeKeyException();
				}
			}
			using (XmlNodeList xmlNodeList = folderIdElement.SelectNodes("t:Mailbox/t:EmailAddress[position() = 1]", ServiceXml.NamespaceManager))
			{
				if (xmlNodeList.Count > 0)
				{
					XmlElement textNodeParent = (XmlElement)xmlNodeList[0];
					emailAddress = ServiceXml.GetXmlTextNodeValue(textNodeParent);
				}
				if (string.IsNullOrEmpty(emailAddress) && CallContext.Current != null)
				{
					emailAddress = CallContext.Current.OwaExplicitLogonUser;
				}
			}
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000CDF98 File Offset: 0x000CC198
		internal static IdHeaderInformation ExtractIdInformation(XmlElement idElement, IdConverter.ConvertOption convertOption, BasicTypes expectedType, out string changeKey, List<AttachmentId> attachmentIds)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			XmlAttribute xmlAttribute = (XmlAttribute)idElement.Attributes.GetNamedItem("Id");
			XmlAttribute xmlAttribute2 = (XmlAttribute)idElement.Attributes.GetNamedItem("ChangeKey");
			IdHeaderInformation result = IdConverter.ConvertFromConcatenatedId(xmlAttribute.Value, expectedType, attachmentIds, false);
			changeKey = ((xmlAttribute2 == null) ? null : xmlAttribute2.Value);
			if (!flag)
			{
				if (changeKey == null)
				{
					throw new ChangeKeyRequiredException();
				}
				if (changeKey.Trim().Length == 0)
				{
					throw new InvalidChangeKeyException();
				}
			}
			return result;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x000CE018 File Offset: 0x000CC218
		private static IdHeaderInformation ExtractOccurrenceOrRecurringIdInformation(XmlElement idElement, IdConverter.ConvertOption convertOption, out string changeKey, BasicTypes expectedType, bool isOccurrence)
		{
			bool flag = (convertOption & IdConverter.ConvertOption.IgnoreChangeKey) == IdConverter.ConvertOption.IgnoreChangeKey;
			XmlAttribute xmlAttribute = isOccurrence ? ((XmlAttribute)idElement.Attributes.GetNamedItem("RecurringMasterId")) : ((XmlAttribute)idElement.Attributes.GetNamedItem("OccurrenceId"));
			XmlAttribute xmlAttribute2 = flag ? null : (isOccurrence ? ((XmlAttribute)idElement.Attributes.GetNamedItem("ChangeKey")) : ((XmlAttribute)idElement.Attributes.GetNamedItem("ChangeKey")));
			changeKey = (flag ? null : xmlAttribute2.Value);
			return IdConverter.ExtractOccurrenceOrRecurringIdInformation(xmlAttribute.Value, changeKey, convertOption, expectedType);
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
		private static IdHeaderInformation ExtractOccurrenceIdInformation(XmlElement idElement, out string changeKey, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractOccurrenceOrRecurringIdInformation(idElement, convertOption, out changeKey, expectedType, true);
			XmlAttribute xmlAttribute = (XmlAttribute)idElement.Attributes.GetNamedItem("InstanceIndex");
			idHeaderInformation.OccurrenceInstanceIndex = int.Parse(xmlAttribute.Value, CultureInfo.InvariantCulture);
			return idHeaderInformation;
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000CE0F9 File Offset: 0x000CC2F9
		private static IdHeaderInformation ExtractRecurringMasterIdInformation(XmlElement idElement, out string changeKey, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			return IdConverter.ExtractOccurrenceOrRecurringIdInformation(idElement, convertOption, out changeKey, expectedType, false);
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x000CE108 File Offset: 0x000CC308
		private static StoreId ConvertDistinguishedFolderId(XmlElement folderIdElement, MailboxSession session, IdConverter.ConvertOption convertOption)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			IdConverter.ExtractDistinguishedFolderInformation(folderIdElement, convertOption, out text, out text2, out text3);
			if (!string.IsNullOrEmpty(text3) && !string.Equals(text3, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				throw new EmailAddressMismatchException();
			}
			bool flag = IdConverter.displayNameIsArchive.Member.Contains(text);
			if (flag ^ session.MailboxOwner.MailboxInfo.IsArchive)
			{
				throw new InvalidFolderIdException(ResponseCodeType.ErrorInvalidFolderId, (CoreResources.IDs)3041888687U);
			}
			StoreId storeId = session.GetRefreshedDefaultFolderId(IdConverter.displayNameMap.Member[text]);
			if (text2 != null)
			{
				try
				{
					storeId = VersionedId.Deserialize(storeId.ToBase64String(), text2);
				}
				catch (CorruptDataException innerException)
				{
					throw new InvalidChangeKeyException(innerException);
				}
			}
			return storeId;
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x000CE1E0 File Offset: 0x000CC3E0
		private static StoreId ConvertId(XmlElement idElement, MailboxSession session, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			string changeKey = null;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractIdInformation(idElement, convertOption, expectedType, out changeKey, null);
			if (!ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2007_SP1) && !string.Equals(idHeaderInformation.MailboxId.SmtpAddress, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				throw new EmailAddressMismatchException();
			}
			Item item = null;
			StoreId result;
			try
			{
				result = IdConverter.CreateAppropriateStoreIdType(session, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
			}
			finally
			{
				if (item != null)
				{
					IdConverter.TraceDebug("ConvertId leaked xsoItem; consider caching behavior");
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000CE278 File Offset: 0x000CC478
		private static XmlElement AddIdElement(XmlElement idParentElement, string elementName, string namespaceUri)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(idParentElement.OwnerDocument, elementName, namespaceUri);
			idParentElement.AppendChild(xmlElement);
			return xmlElement;
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000CE29C File Offset: 0x000CC49C
		private static IdAndSession ConvertDistinguishedFolderId(CallContext callContext, XmlElement folderIdElement, IdConverter.ConvertOption convertOption)
		{
			string displayName = null;
			string changeKey = null;
			string text = null;
			IdConverter.ExtractDistinguishedFolderInformation(folderIdElement, convertOption, out displayName, out changeKey, out text);
			if (callContext.IsExternalUser)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "IdConverter.ConvertDistinguishedFolderId. DistinguishedFolderId is not supported for external users. EmailAddress: {0}", text ?? "<Null>");
				throw new ServiceAccessDeniedException();
			}
			return IdConverter.ConvertDistinguishedFolderId(callContext, displayName, changeKey, text, convertOption);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000CE2F0 File Offset: 0x000CC4F0
		private static IdHeaderInformation ExtractHeaderInformationFromConversationId(XmlElement idElement)
		{
			XmlAttribute xmlAttribute = (XmlAttribute)idElement.Attributes.GetNamedItem("Id");
			XmlAttribute xmlAttribute2 = (XmlAttribute)idElement.Attributes.GetNamedItem("ChangeKey");
			return IdConverter.ConvertFromConcatenatedId(xmlAttribute.Value, BasicTypes.Item, null, false);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000CE33C File Offset: 0x000CC53C
		internal IdAndSession ConvertXmlToIdAndSession(XmlElement singleIdXml, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			Item item = null;
			IdAndSession result;
			try
			{
				result = this.ConvertXmlToIdAndSession(singleIdXml, convertOption, expectedType, ref item);
			}
			finally
			{
				if (item != null)
				{
					IdConverter.TraceDebug("ConvertXmlToIdAndSession: leaked xsoItem, consider caching behavior");
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000CE380 File Offset: 0x000CC580
		internal IdAndSession ConvertXmlToIdAndSession(XmlElement singleIdXml, IdConverter.ConvertOption convertOption, BasicTypes expectedType, ref Item cachedItem)
		{
			cachedItem = null;
			if (IdConverter.IsDistinguishedFolderIdXml(singleIdXml))
			{
				return IdConverter.ConvertDistinguishedFolderId(this.callContext, singleIdXml, convertOption);
			}
			if (IdConverter.IsIdXml(singleIdXml))
			{
				return this.ConvertId(singleIdXml, convertOption, expectedType, ref cachedItem);
			}
			if (IdConverter.IsOccurrenceIdXml(singleIdXml))
			{
				return this.ConvertOccurrenceId(singleIdXml, convertOption, expectedType);
			}
			if (IdConverter.IsRecurringMasterIdXml(singleIdXml))
			{
				return this.ConvertRecurringMasterId(singleIdXml, convertOption, expectedType);
			}
			return null;
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000CE3E0 File Offset: 0x000CC5E0
		internal static BaseServerIdInfo GetServerInfoForId(CallContext callContext, XmlElement singleIdXml)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (singleIdXml == null)
			{
				throw new ArgumentNullException("singleIdXml");
			}
			if (IdConverter.IsDistinguishedFolderIdXml(singleIdXml))
			{
				string text;
				string text2;
				string text3;
				IdConverter.ExtractDistinguishedFolderInformation(singleIdXml, IdConverter.ConvertOption.IgnoreChangeKey, out text, out text2, out text3);
				if (text.Equals("publicfoldersroot", StringComparison.Ordinal))
				{
					return IdConverter.GetPublicFolderServerInfoForAccessingPrincipal(callContext);
				}
				if (text3 != null)
				{
					return MailboxIdServerInfo.Create(text3);
				}
				return callContext.GetServerInfoForEffectiveCaller();
			}
			else
			{
				if (IdConverter.IsIdXml(singleIdXml))
				{
					string text4;
					IdHeaderInformation header = IdConverter.ExtractIdInformation(singleIdXml, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Item, out text4, null);
					return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header, false);
				}
				if (IdConverter.IsOccurrenceIdXml(singleIdXml))
				{
					string text5;
					IdHeaderInformation header2 = IdConverter.ExtractOccurrenceIdInformation(singleIdXml, out text5, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Item);
					return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header2, false);
				}
				if (IdConverter.IsRecurringMasterIdXml(singleIdXml))
				{
					string text6;
					IdHeaderInformation header3 = IdConverter.ExtractRecurringMasterIdInformation(singleIdXml, out text6, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Item);
					return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header3, false);
				}
				if (IdConverter.IsConversationIdXml(singleIdXml))
				{
					IdHeaderInformation header4 = IdConverter.ExtractHeaderInformationFromConversationId(singleIdXml);
					return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header4, false);
				}
				if (IdConverter.IsUploadIdXml(singleIdXml))
				{
					XmlElement idElement = (XmlElement)singleIdXml.ChildNodes[0];
					string text7;
					IdHeaderInformation header5 = IdConverter.ExtractIdInformation(idElement, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Item, out text7, null);
					return IdConverter.ServerInfoFromIdHeaderInformation(callContext, header5, false);
				}
				return null;
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
		public IdAndSession ConvertConversationIdXmlToIdAndSession(XmlElement idElement)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractHeaderInformationFromConversationId(idElement);
			ConversationId storeId = ConversationId.Create(idHeaderInformation.StoreIdBytes);
			if (this.callContext.IsExternalUser || this.callContext.IsWSSecurityUser)
			{
				IdConverter.TraceDebug("[IdConverter::ConvertConversationIdXmlToIdAndSession] Conversation access is not supported for external or WS-Security users.");
				throw new ServiceAccessDeniedException();
			}
			MailboxSession mailboxSessionByMailboxId = this.callContext.SessionCache.GetMailboxSessionByMailboxId(idHeaderInformation.MailboxId);
			return new IdAndSession(storeId, mailboxSessionByMailboxId);
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000CE55C File Offset: 0x000CC75C
		public ConversationId ConvertXmlToConversationId(XmlElement idElement)
		{
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractHeaderInformationFromConversationId(idElement);
			return ConversationId.Create(idHeaderInformation.StoreIdBytes);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000CE580 File Offset: 0x000CC780
		private IdAndSession ConvertId(XmlElement idElement, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			Item item = null;
			IdAndSession result;
			try
			{
				result = this.ConvertId(idElement, convertOption, expectedType, ref item);
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000CE5B8 File Offset: 0x000CC7B8
		private IdAndSession ConvertId(XmlElement idElement, IdConverter.ConvertOption convertOption, BasicTypes expectedType, ref Item cachedItem)
		{
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			string changeKey;
			IdHeaderInformation headerInformation = IdConverter.ExtractIdInformation(idElement, convertOption, expectedType, out changeKey, attachmentIds);
			return IdConverter.ConvertId(this.callContext, headerInformation, convertOption, expectedType, attachmentIds, changeKey, this.GetHashCode(), ref cachedItem);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000CE5F0 File Offset: 0x000CC7F0
		private IdAndSession ConvertOccurrenceId(XmlElement idElement, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			string changeKey;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractOccurrenceIdInformation(idElement, out changeKey, convertOption, expectedType);
			StoreSession session = null;
			StoreId storeId = null;
			switch (idHeaderInformation.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
				session = this.callContext.SessionCache.GetMailboxSessionByMailboxId(idHeaderInformation.MailboxId);
				goto IL_68;
			case IdStorageType.PublicFolderItem:
				storeId = StoreObjectId.FromProviderSpecificId(idHeaderInformation.FolderIdBytes, StoreObjectType.Folder);
				session = IdConverter.GetPublicFolderSession(storeId, this.callContext, convertOption);
				goto IL_68;
			}
			return null;
			IL_68:
			Item item = null;
			IdAndSession result;
			try
			{
				StoreId id = IdConverter.CreateAppropriateStoreIdType(session, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
				if (idHeaderInformation.OccurrenceInstanceIndex <= 0)
				{
					throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
				}
				if (item == null)
				{
					item = ServiceCommandBase.GetXsoItem(session, id, new PropertyDefinition[0]);
				}
				CalendarItem calendarItem = item as CalendarItem;
				if (calendarItem == null || calendarItem.Recurrence == null)
				{
					throw new CalendarExceptionCannotUseIdForRecurringMasterId();
				}
				OccurrenceInfo occurrenceInfo = calendarItem.Recurrence.GetFirstOccurrence();
				OccurrenceInfo occurrenceInfo2 = (calendarItem.Recurrence.Range is NoEndRecurrenceRange) ? null : calendarItem.Recurrence.GetLastOccurrence();
				ExDateTime[] deletedOccurrences = calendarItem.Recurrence.GetDeletedOccurrences();
				int num = 0;
				bool flag = true;
				bool flag2 = deletedOccurrences != null && deletedOccurrences.Length > 0;
				int num2 = 1;
				bool flag3;
				for (;;)
				{
					if (flag2)
					{
						flag3 = (!flag || deletedOccurrences[num] < occurrenceInfo.OriginalStartTime);
					}
					else
					{
						if (!flag)
						{
							break;
						}
						flag3 = false;
					}
					if (num2 == idHeaderInformation.OccurrenceInstanceIndex)
					{
						goto IL_1C9;
					}
					if (flag2 && flag3)
					{
						num++;
						if (num >= deletedOccurrences.Length)
						{
							flag2 = false;
						}
					}
					else if (occurrenceInfo2 != null && occurrenceInfo.OriginalStartTime == occurrenceInfo2.OriginalStartTime)
					{
						flag = false;
					}
					else
					{
						try
						{
							ExDateTime originalStartTime = occurrenceInfo.OriginalStartTime;
							occurrenceInfo = calendarItem.Recurrence.GetNextOccurrence(occurrenceInfo);
							if (occurrenceInfo.OriginalStartTime == originalStartTime)
							{
								flag = false;
							}
						}
						catch (ArgumentOutOfRangeException)
						{
							throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
						}
					}
					num2++;
				}
				throw new CalendarExceptionOccurrenceIndexIsOutOfRecurrenceRange();
				IL_1C9:
				if (flag3)
				{
					throw new CalendarExceptionOccurrenceIsDeletedFromRecurrence();
				}
				switch (idHeaderInformation.IdStorageType)
				{
				case IdStorageType.MailboxItemSmtpAddressBased:
				case IdStorageType.MailboxItemMailboxGuidBased:
					return new IdAndSession(occurrenceInfo.VersionedId, session);
				case IdStorageType.PublicFolderItem:
					return new IdAndSession(occurrenceInfo.VersionedId, storeId, session);
				}
				result = null;
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000CE864 File Offset: 0x000CCA64
		private IdAndSession ConvertRecurringMasterId(XmlElement idElement, IdConverter.ConvertOption convertOption, BasicTypes expectedType)
		{
			string changeKey;
			IdHeaderInformation idHeaderInformation = IdConverter.ExtractRecurringMasterIdInformation(idElement, out changeKey, convertOption, expectedType);
			StoreId folderId = null;
			StoreSession storeSessionFromIdHeaderInformation = this.GetStoreSessionFromIdHeaderInformation(idHeaderInformation, out folderId, convertOption);
			Item item = null;
			IdAndSession result;
			try
			{
				StoreId id = IdConverter.CreateAppropriateStoreIdType(storeSessionFromIdHeaderInformation, idHeaderInformation, changeKey, convertOption, expectedType, ref item);
				if (item == null)
				{
					item = ServiceCommandBase.GetXsoItem(storeSessionFromIdHeaderInformation, id, new PropertyDefinition[0]);
				}
				CalendarItemOccurrence calendarItemOccurrence = item as CalendarItemOccurrence;
				if (calendarItemOccurrence == null)
				{
					throw new CalendarExceptionCannotUseIdForOccurrenceId();
				}
				using (CalendarItem master = calendarItemOccurrence.GetMaster())
				{
					result = IdConverter.CreateIdAndSessionForItemId(master.Id, folderId, idHeaderInformation.IdStorageType, storeSessionFromIdHeaderInformation);
				}
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
					item = null;
				}
			}
			return result;
		}

		// Token: 0x04002049 RID: 8265
		private const int MaximumChangeKeyLength = 512;

		// Token: 0x0400204A RID: 8266
		private const string MailboxElementName = "Mailbox";

		// Token: 0x0400204B RID: 8267
		private const string EmailAddressElementName = "EmailAddress";

		// Token: 0x0400204C RID: 8268
		private const string RoutingTypeElementName = "RoutingType";

		// Token: 0x0400204D RID: 8269
		private const string ChangeKeyBytesParamName = "changeKeyByteArray";

		// Token: 0x0400204E RID: 8270
		private const string CalendarFolderName = "calendar";

		// Token: 0x0400204F RID: 8271
		private const string ContactsFolderName = "contacts";

		// Token: 0x04002050 RID: 8272
		private const string DeletedItemsFolderName = "deleteditems";

		// Token: 0x04002051 RID: 8273
		private const string DraftsFolderName = "drafts";

		// Token: 0x04002052 RID: 8274
		private const string InboxFolderName = "inbox";

		// Token: 0x04002053 RID: 8275
		private const string JunkEmailFolderName = "junkemail";

		// Token: 0x04002054 RID: 8276
		private const string JournalFolderName = "journal";

		// Token: 0x04002055 RID: 8277
		private const string NotesFolderName = "notes";

		// Token: 0x04002056 RID: 8278
		private const string OutboxFolderName = "outbox";

		// Token: 0x04002057 RID: 8279
		private const string SentItemsFolderName = "sentitems";

		// Token: 0x04002058 RID: 8280
		private const string TasksFolderName = "tasks";

		// Token: 0x04002059 RID: 8281
		private const string UMVoicemailFolderName = "voicemail";

		// Token: 0x0400205A RID: 8282
		private const string MsgFolderRootFolderName = "msgfolderroot";

		// Token: 0x0400205B RID: 8283
		private const string ConfigurationFolderName = "root";

		// Token: 0x0400205C RID: 8284
		private const string SearchFoldersFolderName = "searchfolders";

		// Token: 0x0400205D RID: 8285
		private const string RecoverableItemsRootFolderName = "recoverableitemsroot";

		// Token: 0x0400205E RID: 8286
		private const string RecoverableItemsDeletionsFolderName = "recoverableitemsdeletions";

		// Token: 0x0400205F RID: 8287
		private const string RecoverableItemsVersionsFolderName = "recoverableitemsversions";

		// Token: 0x04002060 RID: 8288
		private const string RecoverableItemsPurgesFolderName = "recoverableitemspurges";

		// Token: 0x04002061 RID: 8289
		private const string ArchiveConfigurationFolderName = "archiveroot";

		// Token: 0x04002062 RID: 8290
		private const string ArchiveMsgFolderRootFolderName = "archivemsgfolderroot";

		// Token: 0x04002063 RID: 8291
		private const string ArchiveDeletedItemsFolderName = "archivedeleteditems";

		// Token: 0x04002064 RID: 8292
		private const string ArchiveInboxFolderName = "archiveinbox";

		// Token: 0x04002065 RID: 8293
		private const string ArchiveRecoverableItemsRootFolderName = "archiverecoverableitemsroot";

		// Token: 0x04002066 RID: 8294
		private const string ArchiveRecoverableItemsDeletionsFolderName = "archiverecoverableitemsdeletions";

		// Token: 0x04002067 RID: 8295
		private const string ArchiveRecoverableItemsVersionsFolderName = "archiverecoverableitemsversions";

		// Token: 0x04002068 RID: 8296
		private const string ArchiveRecoverableItemsPurgesFolderName = "archiverecoverableitemspurges";

		// Token: 0x04002069 RID: 8297
		private const string SyncIssuesFolderName = "syncissues";

		// Token: 0x0400206A RID: 8298
		private const string ConflictsFolderName = "conflicts";

		// Token: 0x0400206B RID: 8299
		private const string LocalFailuresFolderName = "localfailures";

		// Token: 0x0400206C RID: 8300
		private const string ServerFailuresFolderName = "serverfailures";

		// Token: 0x0400206D RID: 8301
		private const string RecipientCacheFolderName = "recipientcache";

		// Token: 0x0400206E RID: 8302
		private const string QuickContactsFolderName = "quickcontacts";

		// Token: 0x0400206F RID: 8303
		private const string ImContactListFolderName = "imcontactlist";

		// Token: 0x04002070 RID: 8304
		private const string PeopleConnectFolderName = "peopleconnect";

		// Token: 0x04002071 RID: 8305
		private const string FavoritesFolderName = "favorites";

		// Token: 0x04002072 RID: 8306
		private const string ConversationHistoryFolderName = "conversationhistory";

		// Token: 0x04002073 RID: 8307
		private const string AdminAuditLogsFolderName = "adminauditlogs";

		// Token: 0x04002074 RID: 8308
		private const string ToDoSearchFolderName = "todosearch";

		// Token: 0x04002075 RID: 8309
		private const string MyContactsFolderName = "mycontacts";

		// Token: 0x04002076 RID: 8310
		private const string FromFavoriteSendersFolderName = "fromfavoritesenders";

		// Token: 0x04002077 RID: 8311
		private const string ClutterFolderName = "clutter";

		// Token: 0x04002078 RID: 8312
		private const string UnifiedInboxFolderName = "unifiedinbox";

		// Token: 0x04002079 RID: 8313
		private const string WorkingSetFolderName = "workingset";

		// Token: 0x0400207A RID: 8314
		internal const string RootPublicFolderName = "publicfoldersroot";

		// Token: 0x0400207B RID: 8315
		internal const string PublicFolderInternalSubmissionName = "internalsubmission";

		// Token: 0x0400207C RID: 8316
		private static LazyMember<Dictionary<string, DefaultFolderType>> displayNameMap = new LazyMember<Dictionary<string, DefaultFolderType>>(() => new Dictionary<string, DefaultFolderType>
		{
			{
				"calendar",
				DefaultFolderType.Calendar
			},
			{
				"contacts",
				DefaultFolderType.Contacts
			},
			{
				"deleteditems",
				DefaultFolderType.DeletedItems
			},
			{
				"drafts",
				DefaultFolderType.Drafts
			},
			{
				"inbox",
				DefaultFolderType.Inbox
			},
			{
				"junkemail",
				DefaultFolderType.JunkEmail
			},
			{
				"journal",
				DefaultFolderType.Journal
			},
			{
				"notes",
				DefaultFolderType.Notes
			},
			{
				"outbox",
				DefaultFolderType.Outbox
			},
			{
				"sentitems",
				DefaultFolderType.SentItems
			},
			{
				"tasks",
				DefaultFolderType.Tasks
			},
			{
				"voicemail",
				DefaultFolderType.UMVoicemail
			},
			{
				"msgfolderroot",
				DefaultFolderType.Root
			},
			{
				"root",
				DefaultFolderType.Configuration
			},
			{
				"searchfolders",
				DefaultFolderType.SearchFolders
			},
			{
				"recoverableitemsroot",
				DefaultFolderType.RecoverableItemsRoot
			},
			{
				"recoverableitemsdeletions",
				DefaultFolderType.RecoverableItemsDeletions
			},
			{
				"recoverableitemsversions",
				DefaultFolderType.RecoverableItemsVersions
			},
			{
				"recoverableitemspurges",
				DefaultFolderType.RecoverableItemsPurges
			},
			{
				"syncissues",
				DefaultFolderType.SyncIssues
			},
			{
				"conflicts",
				DefaultFolderType.Conflicts
			},
			{
				"localfailures",
				DefaultFolderType.LocalFailures
			},
			{
				"serverfailures",
				DefaultFolderType.ServerFailures
			},
			{
				"recipientcache",
				DefaultFolderType.RecipientCache
			},
			{
				"quickcontacts",
				DefaultFolderType.QuickContacts
			},
			{
				"imcontactlist",
				DefaultFolderType.ImContactList
			},
			{
				"peopleconnect",
				DefaultFolderType.PeopleConnect
			},
			{
				"favorites",
				DefaultFolderType.Favorites
			},
			{
				"archiveroot",
				DefaultFolderType.Configuration
			},
			{
				"archivemsgfolderroot",
				DefaultFolderType.Root
			},
			{
				"archivedeleteditems",
				DefaultFolderType.DeletedItems
			},
			{
				"archiveinbox",
				DefaultFolderType.Inbox
			},
			{
				"archiverecoverableitemsroot",
				DefaultFolderType.RecoverableItemsRoot
			},
			{
				"archiverecoverableitemsdeletions",
				DefaultFolderType.RecoverableItemsDeletions
			},
			{
				"archiverecoverableitemsversions",
				DefaultFolderType.RecoverableItemsVersions
			},
			{
				"archiverecoverableitemspurges",
				DefaultFolderType.RecoverableItemsPurges
			},
			{
				"conversationhistory",
				DefaultFolderType.CommunicatorHistory
			},
			{
				"adminauditlogs",
				DefaultFolderType.AdminAuditLogs
			},
			{
				"todosearch",
				DefaultFolderType.ToDoSearch
			},
			{
				"mycontacts",
				DefaultFolderType.MyContacts
			},
			{
				"fromfavoritesenders",
				DefaultFolderType.FromFavoriteSenders
			},
			{
				"clutter",
				DefaultFolderType.Clutter
			},
			{
				"unifiedinbox",
				DefaultFolderType.UnifiedInbox
			},
			{
				"workingset",
				DefaultFolderType.WorkingSet
			}
		});

		// Token: 0x0400207D RID: 8317
		private static LazyMember<List<string>> displayNameIsArchive = new LazyMember<List<string>>(() => new List<string>
		{
			"archiveroot",
			"archivemsgfolderroot",
			"archivedeleteditems",
			"archiveinbox",
			"archiverecoverableitemsroot",
			"archiverecoverableitemsdeletions",
			"archiverecoverableitemsversions",
			"archiverecoverableitemspurges"
		});

		// Token: 0x0400207E RID: 8318
		private static LazyMember<List<string>> defaultFoldersWithCreateSupport = new LazyMember<List<string>>(() => new List<string>
		{
			"conversationhistory",
			"adminauditlogs",
			"recoverableitemsroot"
		});

		// Token: 0x0400207F RID: 8319
		private static LazyMember<Dictionary<DefaultFolderType, string>> defaultFolderToNameMapForMailbox = new LazyMember<Dictionary<DefaultFolderType, string>>(delegate()
		{
			Dictionary<DefaultFolderType, string> dictionary = new Dictionary<DefaultFolderType, string>();
			foreach (KeyValuePair<string, DefaultFolderType> keyValuePair in IdConverter.displayNameMap.Member)
			{
				if (!IdConverter.displayNameIsArchive.Member.Contains(keyValuePair.Key) && !IdConverter.displayNameIsUnifiedSearchFolder.Member.Contains(keyValuePair.Key))
				{
					dictionary.Add(keyValuePair.Value, keyValuePair.Key);
				}
			}
			return dictionary;
		});

		// Token: 0x04002080 RID: 8320
		private static LazyMember<Dictionary<DefaultFolderType, string>> defaultFolderToNameMapForArchiveMailbox = new LazyMember<Dictionary<DefaultFolderType, string>>(delegate()
		{
			Dictionary<DefaultFolderType, string> dictionary = new Dictionary<DefaultFolderType, string>();
			foreach (KeyValuePair<string, DefaultFolderType> keyValuePair in IdConverter.displayNameMap.Member)
			{
				if (IdConverter.displayNameIsArchive.Member.Contains(keyValuePair.Key))
				{
					dictionary.Add(keyValuePair.Value, keyValuePair.Key);
				}
			}
			return dictionary;
		});

		// Token: 0x04002081 RID: 8321
		private static List<Type> ignoreableIdConversionExceptionTypes = new List<Type>
		{
			typeof(CannotUseFolderIdForItemIdException),
			typeof(InvalidIdEmptyException),
			typeof(InvalidIdFormatVersionException),
			typeof(InvalidSerializedAccessTokenException),
			typeof(InvalidStoreIdException),
			typeof(ObjectNotFoundException)
		};

		// Token: 0x04002082 RID: 8322
		private CallContext callContext;

		// Token: 0x04002083 RID: 8323
		internal static readonly LazyMember<List<string>> displayNameIsUnifiedSearchFolder = new LazyMember<List<string>>(() => new List<string>
		{
			"unifiedinbox"
		});

		// Token: 0x02000796 RID: 1942
		[Flags]
		internal enum ConvertOption
		{
			// Token: 0x0400208B RID: 8331
			None = 0,
			// Token: 0x0400208C RID: 8332
			IgnoreChangeKey = 1,
			// Token: 0x0400208D RID: 8333
			NoBind = 2,
			// Token: 0x0400208E RID: 8334
			AllowKnownExternalUsers = 4,
			// Token: 0x0400208F RID: 8335
			IsHierarchicalOperation = 8,
			// Token: 0x04002090 RID: 8336
			SessionOnly = 16
		}
	}
}
