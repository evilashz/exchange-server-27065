using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A73 RID: 2675
	[Serializable]
	public sealed class PublicFolderStatistics : XsoMailboxConfigurationObject
	{
		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x060061BB RID: 25019 RVA: 0x0019D6DF File Offset: 0x0019B8DF
		internal static PublicFolderStatisticsSchema InternalSchema
		{
			get
			{
				return PublicFolderStatistics.schema;
			}
		}

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x0019D6E6 File Offset: 0x0019B8E6
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return PublicFolderStatistics.schema;
			}
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x060061BD RID: 25021 RVA: 0x0019D6ED File Offset: 0x0019B8ED
		public int? AssociatedItemCount
		{
			get
			{
				return (int?)this[PublicFolderStatisticsSchema.AssociatedItemCount];
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x0019D6FF File Offset: 0x0019B8FF
		// (set) Token: 0x060061BF RID: 25023 RVA: 0x0019D707 File Offset: 0x0019B907
		public uint ContactCount
		{
			get
			{
				return this.contactCount;
			}
			internal set
			{
				this.contactCount = value;
			}
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x0019D710 File Offset: 0x0019B910
		public DateTime? CreationTime
		{
			get
			{
				return new DateTime?((DateTime)((ExDateTime)this[PublicFolderStatisticsSchema.CreationTime]));
			}
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x060061C1 RID: 25025 RVA: 0x0019D72C File Offset: 0x0019B92C
		// (set) Token: 0x060061C2 RID: 25026 RVA: 0x0019D734 File Offset: 0x0019B934
		public uint DeletedItemCount
		{
			get
			{
				return this.deletedItemCount;
			}
			internal set
			{
				this.deletedItemCount = value;
			}
		}

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x060061C3 RID: 25027 RVA: 0x0019D740 File Offset: 0x0019B940
		public string EntryId
		{
			get
			{
				StoreObjectId objectId = ((VersionedId)this[PublicFolderStatisticsSchema.EntryId]).ObjectId;
				return objectId.ToHexEntryId();
			}
		}

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x060061C4 RID: 25028 RVA: 0x0019D76C File Offset: 0x0019B96C
		public MapiFolderPath FolderPath
		{
			get
			{
				string text = (string)this[PublicFolderStatisticsSchema.FolderPath];
				if (SuppressingPiiContext.NeedPiiSuppression)
				{
					return new MapiFolderPath('\\' + text);
				}
				return new MapiFolderPath(text);
			}
		}

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x060061C5 RID: 25029 RVA: 0x0019D7AA File Offset: 0x0019B9AA
		public int? ItemCount
		{
			get
			{
				return (int?)this[PublicFolderStatisticsSchema.ItemCount];
			}
		}

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x060061C6 RID: 25030 RVA: 0x0019D7BC File Offset: 0x0019B9BC
		public DateTime? LastModificationTime
		{
			get
			{
				return (DateTime?)((ExDateTime?)this[PublicFolderStatisticsSchema.LastModificationTime]);
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x060061C7 RID: 25031 RVA: 0x0019D7D3 File Offset: 0x0019B9D3
		public string Name
		{
			get
			{
				return (string)this[PublicFolderStatisticsSchema.Name];
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x0019D7E5 File Offset: 0x0019B9E5
		// (set) Token: 0x060061C9 RID: 25033 RVA: 0x0019D7ED File Offset: 0x0019B9ED
		public uint OwnerCount
		{
			get
			{
				return this.ownerCount;
			}
			internal set
			{
				this.ownerCount = value;
			}
		}

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x0019D7F6 File Offset: 0x0019B9F6
		public ByteQuantifiedSize TotalAssociatedItemSize
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(checked((ulong)((long)this[PublicFolderStatisticsSchema.TotalAssociatedItemSize])));
			}
		}

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x060061CB RID: 25035 RVA: 0x0019D80E File Offset: 0x0019BA0E
		// (set) Token: 0x060061CC RID: 25036 RVA: 0x0019D816 File Offset: 0x0019BA16
		public ByteQuantifiedSize TotalDeletedItemSize
		{
			get
			{
				return this.totalDeletedItemSize;
			}
			internal set
			{
				this.totalDeletedItemSize = value;
			}
		}

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x060061CD RID: 25037 RVA: 0x0019D81F File Offset: 0x0019BA1F
		public ByteQuantifiedSize TotalItemSize
		{
			get
			{
				return ByteQuantifiedSize.FromBytes(checked((ulong)((long)this[PublicFolderStatisticsSchema.TotalItemSize])));
			}
		}

		// Token: 0x0400377F RID: 14207
		private static PublicFolderStatisticsSchema schema = ObjectSchema.GetInstance<PublicFolderStatisticsSchema>();

		// Token: 0x04003780 RID: 14208
		private uint ownerCount;

		// Token: 0x04003781 RID: 14209
		private uint contactCount;

		// Token: 0x04003782 RID: 14210
		private uint deletedItemCount;

		// Token: 0x04003783 RID: 14211
		private ByteQuantifiedSize totalDeletedItemSize;
	}
}
