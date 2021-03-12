using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003EA RID: 1002
	[KnownType(typeof(StoreObjectId))]
	[KnownType(typeof(VersionedId))]
	[KnownType(typeof(ConversationId))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[KnownType(typeof(PersonId))]
	[Serializable]
	internal abstract class StoreId : ObjectId, IEquatable<StoreId>
	{
		// Token: 0x06002DBA RID: 11706 RVA: 0x000BC6DF File Offset: 0x000BA8DF
		internal StoreId()
		{
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000BC6E8 File Offset: 0x000BA8E8
		public static StoreObjectId GetStoreObjectId(StoreId id)
		{
			if (id == null)
			{
				return null;
			}
			VersionedId versionedId = id as VersionedId;
			if (versionedId != null)
			{
				return versionedId.ObjectId;
			}
			return id as StoreObjectId;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000BC714 File Offset: 0x000BA914
		public static string StoreIdToEwsId(Guid mailboxGuid, StoreId storeId)
		{
			Util.ThrowOnNullArgument(storeId, "storeId");
			MailboxId mailboxId = new MailboxId(mailboxGuid);
			IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			if (storeObjectId.ObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.GetBytes();
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Recurrence;
			}
			else if (storeObjectId.ObjectType == StoreObjectType.CalendarItemSeries)
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.ProviderLevelItemId;
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Series;
			}
			else
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.ProviderLevelItemId;
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Normal;
			}
			idHeaderInformation.IdStorageType = IdStorageType.MailboxItemMailboxGuidBased;
			idHeaderInformation.MailboxId = mailboxId;
			return ServiceIdConverter.ConvertToConcatenatedId(idHeaderInformation, null, true);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000BC7A8 File Offset: 0x000BA9A8
		public static string PublicFolderStoreIdToEwsId(StoreId storeId, StoreId parentFolderId)
		{
			Util.ThrowOnNullArgument(storeId, "storeId");
			IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			if (storeObjectId.ObjectType == StoreObjectType.CalendarItemOccurrence)
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.GetBytes();
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Recurrence;
			}
			else if (storeObjectId.ObjectType == StoreObjectType.CalendarItemSeries)
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.ProviderLevelItemId;
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Series;
			}
			else
			{
				idHeaderInformation.StoreIdBytes = storeObjectId.ProviderLevelItemId;
				idHeaderInformation.IdProcessingInstruction = IdProcessingInstruction.Normal;
			}
			if (Folder.IsFolderId(storeObjectId))
			{
				idHeaderInformation.IdStorageType = IdStorageType.PublicFolder;
			}
			else
			{
				Util.ThrowOnNullArgument(parentFolderId, "parentFolderId");
				StoreObjectId storeObjectId2 = StoreId.GetStoreObjectId(parentFolderId);
				idHeaderInformation.FolderIdBytes = storeObjectId2.ProviderLevelItemId;
				idHeaderInformation.IdStorageType = IdStorageType.PublicFolderItem;
			}
			return ServiceIdConverter.ConvertToConcatenatedId(idHeaderInformation, null, true);
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000BC85C File Offset: 0x000BAA5C
		public static StoreObjectId EwsIdToFolderStoreObjectId(string id)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(id, BasicTypes.Folder, null);
			return idHeaderInformation.ToStoreObjectId();
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000BC878 File Offset: 0x000BAA78
		public static StoreObjectId EwsIdToStoreObjectId(string id)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(id, BasicTypes.Item, null);
			return idHeaderInformation.ToStoreObjectId();
		}

		// Token: 0x06002DC0 RID: 11712
		public abstract string ToBase64String();

		// Token: 0x06002DC1 RID: 11713
		public abstract bool Equals(StoreId id);

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000BC894 File Offset: 0x000BAA94
		internal static byte[][] StoreIdsToEntryIds(params StoreId[] itemIds)
		{
			byte[][] array = new byte[itemIds.Length][];
			for (int i = 0; i < itemIds.Length; i++)
			{
				if (itemIds[i] == null)
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, "ItemId::ItemIdsToEntryIds. The in itemId cannot be null.");
					throw new ArgumentException(ServerStrings.ExNullItemIdParameter(i));
				}
				array[i] = StoreId.StoreIdToEntryId(itemIds[i], i);
			}
			return array;
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000BC8EC File Offset: 0x000BAAEC
		internal static void SplitStoreObjectIdAndChangeKey(StoreId id, out StoreObjectId storeObjectId, out byte[] changeKey)
		{
			VersionedId versionedId = id as VersionedId;
			if (versionedId != null)
			{
				changeKey = versionedId.ChangeKeyAsByteArray();
				storeObjectId = versionedId.ObjectId;
				return;
			}
			changeKey = null;
			storeObjectId = (StoreObjectId)id;
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000BC920 File Offset: 0x000BAB20
		internal static byte[] Base64ToByteArray(string base64String)
		{
			byte[] result;
			try
			{
				result = Convert.FromBase64String(base64String);
			}
			catch (FormatException innerException)
			{
				throw new CorruptDataException(ServerStrings.InvalidBase64String, innerException);
			}
			return result;
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000BC954 File Offset: 0x000BAB54
		private static byte[] StoreIdToEntryId(StoreId id, int index)
		{
			if (id == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>(0L, "Folder::IItemIdToEntryId. The element cannot be null. Index = {0}.", index);
				throw new ArgumentException(ServerStrings.ExNullItemIdParameter(index));
			}
			return StoreId.GetStoreObjectId(id).ProviderLevelItemId;
		}

		// Token: 0x0400190B RID: 6411
		public static readonly IEqualityComparer<StoreId> EqualityComparer = new StoreId.Comparer();

		// Token: 0x020003EB RID: 1003
		private sealed class Comparer : IEqualityComparer<StoreId>
		{
			// Token: 0x06002DC7 RID: 11719 RVA: 0x000BC993 File Offset: 0x000BAB93
			public bool Equals(StoreId a, StoreId b)
			{
				return object.ReferenceEquals(a, b) || (a != null && b != null && a.Equals(b));
			}

			// Token: 0x06002DC8 RID: 11720 RVA: 0x000BC9AF File Offset: 0x000BABAF
			public int GetHashCode(StoreId storeId)
			{
				return storeId.GetHashCode();
			}
		}
	}
}
