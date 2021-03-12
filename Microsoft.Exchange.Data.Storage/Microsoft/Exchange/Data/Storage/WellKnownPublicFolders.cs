using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000958 RID: 2392
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WellKnownPublicFolders
	{
		// Token: 0x060058C5 RID: 22725 RVA: 0x0016CFF4 File Offset: 0x0016B1F4
		public WellKnownPublicFolders(PropValue[] propValues)
		{
			this.folderIdMappings = new Dictionary<WellKnownPublicFolders.FolderType, byte[]>(propValues.Length);
			this.longTermIdMappings = new Dictionary<byte[], WellKnownPublicFolders.FolderType?>(propValues.Length, ArrayComparer<byte>.Comparer);
			foreach (PropValue propValue in propValues)
			{
				WellKnownPublicFolders.FolderType? folderType = WellKnownPublicFolders.GetFolderType(propValue.PropTag);
				if (folderType != null)
				{
					byte[] bytes = propValue.GetBytes();
					this.folderIdMappings.Add(folderType.Value, bytes);
					StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(bytes, StoreObjectType.Folder);
					this.longTermIdMappings.Add(storeObjectId.LongTermFolderId, new WellKnownPublicFolders.FolderType?(folderType.Value));
				}
			}
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0016D0A4 File Offset: 0x0016B2A4
		public static WellKnownPublicFolders.FolderType? GetFolderType(PropTag propTag)
		{
			EnumValidator.ThrowIfInvalid<PropTag>(propTag, "propTag");
			if (propTag <= PropTag.IpmOutboxEntryId)
			{
				if (propTag <= PropTag.IpmSubtreeEntryId)
				{
					if (propTag == PropTag.RootEntryId)
					{
						return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.Root);
					}
					if (propTag == PropTag.IpmSubtreeEntryId)
					{
						return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.EFormsRegistry);
					}
				}
				else
				{
					if (propTag == PropTag.IpmInboxEntryId)
					{
						return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.DumpsterRoot);
					}
					if (propTag == PropTag.IpmOutboxEntryId)
					{
						return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.TombstoneRoot);
					}
				}
			}
			else if (propTag <= PropTag.IpmSentMailEntryId)
			{
				if (propTag == PropTag.IpmWasteBasketEntryId)
				{
					return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.InternalSubmission);
				}
				if (propTag == PropTag.IpmSentMailEntryId)
				{
					return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.AsyncDeleteState);
				}
			}
			else
			{
				if (propTag == PropTag.SpoolerQueueEntryId)
				{
					return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.NonIpmSubtree);
				}
				if (propTag == PropTag.DeferredActionFolderEntryID)
				{
					return new WellKnownPublicFolders.FolderType?(WellKnownPublicFolders.FolderType.IpmSubtree);
				}
			}
			return null;
		}

		// Token: 0x060058C7 RID: 22727 RVA: 0x0016D160 File Offset: 0x0016B360
		public bool GetFolderType(byte[] folderId, out WellKnownPublicFolders.FolderType? folderType)
		{
			folderType = null;
			foreach (KeyValuePair<WellKnownPublicFolders.FolderType, byte[]> keyValuePair in this.folderIdMappings)
			{
				if (ArrayComparer<byte>.Comparer.Equals(keyValuePair.Value, folderId))
				{
					folderType = new WellKnownPublicFolders.FolderType?(keyValuePair.Key);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060058C8 RID: 22728 RVA: 0x0016D1E0 File Offset: 0x0016B3E0
		public bool TryGetFolderTypeFromLongTermId(byte[] folderId, out WellKnownPublicFolders.FolderType? folderType)
		{
			ArgumentValidator.ThrowIfNull("folerId", folderId);
			ArgumentValidator.ThrowIfOutOfRange<int>("folerId.Length", folderId.Length, 22, 22);
			return this.longTermIdMappings.TryGetValue(folderId, out folderType);
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x0016D20B File Offset: 0x0016B40B
		public byte[] GetFolderId(WellKnownPublicFolders.FolderType folderType)
		{
			EnumValidator.ThrowIfInvalid<WellKnownPublicFolders.FolderType>(folderType, "folderType");
			return this.folderIdMappings[folderType];
		}

		// Token: 0x04003082 RID: 12418
		public const PropTag RootEntryIdPropTag = PropTag.RootEntryId;

		// Token: 0x04003083 RID: 12419
		public const PropTag IpmSubtreeEntryIdPropTag = PropTag.DeferredActionFolderEntryID;

		// Token: 0x04003084 RID: 12420
		public const PropTag NonIpmSubtreeEntryIdPropTag = PropTag.SpoolerQueueEntryId;

		// Token: 0x04003085 RID: 12421
		public const PropTag EFormsRegistryEntryIdPropTag = PropTag.IpmSubtreeEntryId;

		// Token: 0x04003086 RID: 12422
		public const PropTag DumpsterRootEntryIdPropTag = PropTag.IpmInboxEntryId;

		// Token: 0x04003087 RID: 12423
		public const PropTag AsyncDeleteStateEntryIdPropTag = PropTag.IpmSentMailEntryId;

		// Token: 0x04003088 RID: 12424
		public const PropTag InternalSubmissionEntryIdPropTag = PropTag.IpmWasteBasketEntryId;

		// Token: 0x04003089 RID: 12425
		public const PropTag TombstonesRootEntryIdPropTag = PropTag.IpmOutboxEntryId;

		// Token: 0x0400308A RID: 12426
		public static PropTag[] EntryIdPropTags = new PropTag[]
		{
			PropTag.RootEntryId,
			PropTag.DeferredActionFolderEntryID,
			PropTag.SpoolerQueueEntryId,
			PropTag.IpmSubtreeEntryId,
			PropTag.IpmInboxEntryId,
			PropTag.IpmSentMailEntryId,
			PropTag.IpmWasteBasketEntryId,
			PropTag.IpmOutboxEntryId
		};

		// Token: 0x0400308B RID: 12427
		private readonly Dictionary<WellKnownPublicFolders.FolderType, byte[]> folderIdMappings;

		// Token: 0x0400308C RID: 12428
		private readonly Dictionary<byte[], WellKnownPublicFolders.FolderType?> longTermIdMappings;

		// Token: 0x02000959 RID: 2393
		internal enum FolderType
		{
			// Token: 0x0400308E RID: 12430
			Root,
			// Token: 0x0400308F RID: 12431
			IpmSubtree,
			// Token: 0x04003090 RID: 12432
			NonIpmSubtree,
			// Token: 0x04003091 RID: 12433
			EFormsRegistry,
			// Token: 0x04003092 RID: 12434
			DumpsterRoot,
			// Token: 0x04003093 RID: 12435
			AsyncDeleteState,
			// Token: 0x04003094 RID: 12436
			InternalSubmission,
			// Token: 0x04003095 RID: 12437
			TombstoneRoot
		}
	}
}
