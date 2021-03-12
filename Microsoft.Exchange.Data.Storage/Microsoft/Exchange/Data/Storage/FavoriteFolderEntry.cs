using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.OutlookClassIds;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007A2 RID: 1954
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FavoriteFolderEntry : FolderTreeData
	{
		// Token: 0x060049C0 RID: 18880 RVA: 0x00134C34 File Offset: 0x00132E34
		internal FavoriteFolderEntry(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this[StoreObjectSchema.ItemClass] = "IPM.Microsoft.WunderBar.Link";
			}
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00134C55 File Offset: 0x00132E55
		public static FavoriteFolderEntry Create(MailboxSession session, StoreObjectId folderId, FolderTreeDataType dataType)
		{
			return FavoriteFolderEntry.Create(session, folderId, dataType, FavoriteFolderType.Mail);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x00134C60 File Offset: 0x00132E60
		public static FavoriteFolderEntry Create(MailboxSession session, StoreObjectId folderId, FolderTreeDataType dataType, FavoriteFolderType favoriteFolderType)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(session, "folderId");
			EnumValidator.ThrowIfInvalid<FolderTreeDataType>(dataType, "dataType");
			EnumValidator.ThrowIfInvalid<FavoriteFolderType>(favoriteFolderType, "favoriteFolderType");
			FolderTreeDataSection groupSection = FavoriteFolderEntry.FavoritesGroupSectionByFolderType[favoriteFolderType];
			byte[] favoritesClassIdValue = FavoriteFolderEntry.FavoritesClassIdValueByFolderType[favoriteFolderType];
			FavoriteFolderEntry favoriteFolderEntry = ItemBuilder.CreateNewItem<FavoriteFolderEntry>(session, session.GetDefaultFolderId(DefaultFolderType.CommonViews), ItemCreateInfo.FavoriteFolderEntryInfo, CreateMessageType.Associated);
			byte[] favoritesParentGroupClassIdValue = (FavoriteFolderType.Contact == favoriteFolderType || FavoriteFolderType.Calendar == favoriteFolderType) ? NavigationNodeParentGroup.OtherFoldersClassId.AsBytes : null;
			FolderTreeDataFlags additionalFolderTreeDataFlags = FolderTreeDataFlags.None;
			if (folderId.IsLegacyPublicFolderType())
			{
				additionalFolderTreeDataFlags = FavoriteFolderEntry.PublicFolderTreeDataFlagsByFolderType[favoriteFolderType];
			}
			favoriteFolderEntry.SetPropertiesOfFavoriteFolderEntry(session, folderId, dataType, groupSection, favoritesClassIdValue, favoritesParentGroupClassIdValue, additionalFolderTreeDataFlags);
			return favoriteFolderEntry;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x00134D04 File Offset: 0x00132F04
		private void SetPropertiesOfFavoriteFolderEntry(MailboxSession session, StoreObjectId folderId, FolderTreeDataType dataType, FolderTreeDataSection groupSection, byte[] favoritesClassIdValue, byte[] favoritesParentGroupClassIdValue, FolderTreeDataFlags additionalFolderTreeDataFlags)
		{
			if (folderId.ObjectType != StoreObjectType.Folder && folderId.ObjectType != StoreObjectType.SearchFolder)
			{
				throw new NotSupportedException("Only folder and search folder types can be added to favorites.");
			}
			this[FolderTreeDataSchema.GroupSection] = groupSection;
			if (favoritesClassIdValue != null)
			{
				this[FolderTreeDataSchema.ClassId] = favoritesClassIdValue;
			}
			if (favoritesParentGroupClassIdValue != null)
			{
				this[FolderTreeDataSchema.ParentGroupClassId] = favoritesParentGroupClassIdValue;
			}
			base.MailboxSession = session;
			this.NodeEntryId = folderId.ProviderLevelItemId;
			this.StoreEntryId = Microsoft.Exchange.Data.Storage.StoreEntryId.ToProviderStoreEntryId(session.MailboxOwner, folderId.IsLegacyPublicFolderType());
			if (additionalFolderTreeDataFlags != FolderTreeDataFlags.None)
			{
				base.FolderTreeDataFlags |= additionalFolderTreeDataFlags;
			}
			if (folderId.IsLegacyPublicFolderType())
			{
				byte[] providerLevelItemId = folderId.ProviderLevelItemId;
				providerLevelItemId[0] = 239;
				providerLevelItemId[20] = 2;
				this.NavigationNodeRecordKey = providerLevelItemId;
			}
			base.FolderTreeDataType = dataType;
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x00134DCC File Offset: 0x00132FCC
		public static FavoriteFolderEntry Bind(MailboxSession session, StoreId storeId)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			FavoriteFolderEntry favoriteFolderEntry = ItemBuilder.ItemBind<FavoriteFolderEntry>(session, storeId, FavoriteFolderEntrySchema.Instance, null);
			favoriteFolderEntry.MailboxSession = session;
			return favoriteFolderEntry;
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x00134E05 File Offset: 0x00133005
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return FavoriteFolderEntrySchema.Instance;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x00134E17 File Offset: 0x00133017
		// (set) Token: 0x060049C7 RID: 18887 RVA: 0x00134E2F File Offset: 0x0013302F
		public byte[] NodeEntryId
		{
			get
			{
				this.CheckDisposed("NodeEntryId::get");
				return base.GetValueOrDefault<byte[]>(FavoriteFolderEntrySchema.NodeEntryId);
			}
			set
			{
				this.CheckDisposed("NodeEntryId::set");
				this[FavoriteFolderEntrySchema.NodeEntryId] = value;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x00134E48 File Offset: 0x00133048
		// (set) Token: 0x060049C9 RID: 18889 RVA: 0x00134E60 File Offset: 0x00133060
		public byte[] StoreEntryId
		{
			get
			{
				this.CheckDisposed("StoreEntryId::get");
				return base.GetValueOrDefault<byte[]>(FavoriteFolderEntrySchema.StoreEntryId);
			}
			set
			{
				this.CheckDisposed("StoreEntryId::set");
				this[FavoriteFolderEntrySchema.StoreEntryId] = value;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x00134E79 File Offset: 0x00133079
		// (set) Token: 0x060049CB RID: 18891 RVA: 0x00134E91 File Offset: 0x00133091
		public byte[] NavigationNodeRecordKey
		{
			get
			{
				this.CheckDisposed("RecordKey::get");
				return base.GetValueOrDefault<byte[]>(FavoriteFolderEntrySchema.NavigationNodeRecordKey);
			}
			set
			{
				this.CheckDisposed("RecordKey::set");
				this[FavoriteFolderEntrySchema.NavigationNodeRecordKey] = value;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x00134EAA File Offset: 0x001330AA
		// (set) Token: 0x060049CD RID: 18893 RVA: 0x00134EC2 File Offset: 0x001330C2
		public string FolderDisplayName
		{
			get
			{
				this.CheckDisposed("FolderDisplayName::get");
				return base.GetValueOrDefault<string>(FavoriteFolderEntrySchema.FolderName);
			}
			set
			{
				this.CheckDisposed("FolderDisplayName::set");
				this[FavoriteFolderEntrySchema.FolderName] = value;
			}
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x00134EDB File Offset: 0x001330DB
		public override void SetNodeOrdinal(byte[] nodeBefore, byte[] nodeAfter)
		{
			this.CheckDisposed("SetNodeOrdinal");
			base.SetNodeOrdinalInternal(nodeBefore, nodeAfter);
		}

		// Token: 0x040027B8 RID: 10168
		private const int PublicFolderFavByte = 239;

		// Token: 0x040027B9 RID: 10169
		private const int PublicFolderFavByteIndex = 0;

		// Token: 0x040027BA RID: 10170
		private const int PublicFolderFavoriteType = 2;

		// Token: 0x040027BB RID: 10171
		private const int PublicFolderFavoriteTypeIndex = 20;

		// Token: 0x040027BC RID: 10172
		private static readonly Dictionary<FavoriteFolderType, byte[]> FavoritesClassIdValueByFolderType = new Dictionary<FavoriteFolderType, byte[]>
		{
			{
				FavoriteFolderType.Mail,
				NavigationNode.MailFolderFavoriteClassId.AsBytes
			},
			{
				FavoriteFolderType.Calendar,
				NavigationNode.CalendarFolderFavoriteClassId.AsBytes
			},
			{
				FavoriteFolderType.Contact,
				NavigationNode.ContactFolderFavoriteClassId.AsBytes
			}
		};

		// Token: 0x040027BD RID: 10173
		private static readonly Dictionary<FavoriteFolderType, FolderTreeDataSection> FavoritesGroupSectionByFolderType = new Dictionary<FavoriteFolderType, FolderTreeDataSection>
		{
			{
				FavoriteFolderType.Mail,
				FolderTreeDataSection.First
			},
			{
				FavoriteFolderType.Calendar,
				FolderTreeDataSection.Calendar
			},
			{
				FavoriteFolderType.Contact,
				FolderTreeDataSection.Contacts
			}
		};

		// Token: 0x040027BE RID: 10174
		private static readonly Dictionary<FavoriteFolderType, FolderTreeDataFlags> PublicFolderTreeDataFlagsByFolderType = new Dictionary<FavoriteFolderType, FolderTreeDataFlags>
		{
			{
				FavoriteFolderType.Mail,
				FolderTreeDataFlags.PublicFolder | FolderTreeDataFlags.PublicFolderFavorite | FolderTreeDataFlags.IpfNote
			},
			{
				FavoriteFolderType.Calendar,
				FolderTreeDataFlags.PublicFolder | FolderTreeDataFlags.PublicFolderFavorite | FolderTreeDataFlags.SharedOut
			},
			{
				FavoriteFolderType.Contact,
				FolderTreeDataFlags.PublicFolder | FolderTreeDataFlags.PublicFolderFavorite | FolderTreeDataFlags.SharedOut
			}
		};
	}
}
