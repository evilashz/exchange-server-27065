using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200042C RID: 1068
	[Serializable]
	public class MailboxFolderConfiguration : ConfigurableObject
	{
		// Token: 0x0600253C RID: 9532 RVA: 0x000966E8 File Offset: 0x000948E8
		public MailboxFolderConfiguration() : base(new MapiFolderConfigurationPropertyBag())
		{
			this.DeletedItemsInFolder = 0;
			this.DeletedItemsInFolderAndSubfolders = 0;
			this.ItemsInFolder = 0;
			this.ItemsInFolderAndSubfolders = 0;
			this.FolderAndSubfolderSize = ByteQuantifiedSize.FromBytes(0UL);
			this.FolderSize = ByteQuantifiedSize.FromBytes(0UL);
			this.Date = DateTime.MinValue;
			this.NewestDeletedItemReceivedDate = null;
			this.NewestItemReceivedDate = null;
			this.OldestDeletedItemReceivedDate = null;
			this.OldestItemReceivedDate = null;
			this.NewestDeletedItemLastModifiedDate = null;
			this.NewestItemLastModifiedDate = null;
			this.OldestDeletedItemLastModifiedDate = null;
			this.OldestItemLastModifiedDate = null;
			this.FolderId = null;
			this.FolderPath = null;
			this.FolderType = null;
			this.Name = null;
			this.ManagedFolder = null;
			this.DeletePolicy = null;
			this.ArchivePolicy = null;
			this.TopSubjectCount = 0;
			this.TopSubjectSize = ByteQuantifiedSize.FromBytes(0UL);
			this.TopClientInfoForSubject = string.Empty;
			this.TopClientInfoCountForSubject = 0;
			this.TopSubjectPath = string.Empty;
			this.TopSubject = string.Empty;
			this.TopSubjectReceivedTime = null;
			this.TopSubjectFrom = string.Empty;
			this.TopSubjectClass = string.Empty;
			this.SearchFolders = null;
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x00096857 File Offset: 0x00094A57
		// (set) Token: 0x0600253E RID: 9534 RVA: 0x0009686E File Offset: 0x00094A6E
		public DateTime Date
		{
			get
			{
				return (DateTime)this.propertyBag[MapiFolderConfigurationSchema.Date];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.Date] = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600253F RID: 9535 RVA: 0x00096886 File Offset: 0x00094A86
		// (set) Token: 0x06002540 RID: 9536 RVA: 0x00096898 File Offset: 0x00094A98
		public string Name
		{
			get
			{
				return (string)this[MapiFolderConfigurationSchema.Name];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.Name] = value;
			}
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000968A6 File Offset: 0x00094AA6
		internal void SetIdentity(MailboxFolderId value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("Identity_set");
			}
			this.propertyBag[MapiFolderConfigurationSchema.Identity] = value;
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x000968C7 File Offset: 0x00094AC7
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x000968D9 File Offset: 0x00094AD9
		public string FolderPath
		{
			get
			{
				return (string)this[MapiFolderConfigurationSchema.FolderPath];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.FolderPath] = value;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000968E7 File Offset: 0x00094AE7
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x000968FE File Offset: 0x00094AFE
		public string FolderId
		{
			get
			{
				return (string)this.propertyBag[MapiFolderConfigurationSchema.FolderId];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.FolderId] = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x00096911 File Offset: 0x00094B11
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x00096928 File Offset: 0x00094B28
		public string FolderType
		{
			get
			{
				return (string)this.propertyBag[MapiFolderConfigurationSchema.FolderType];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.FolderType] = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0009693B File Offset: 0x00094B3B
		// (set) Token: 0x06002549 RID: 9545 RVA: 0x00096952 File Offset: 0x00094B52
		public int ItemsInFolder
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.ItemsInFolder];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.ItemsInFolder] = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x0009696A File Offset: 0x00094B6A
		// (set) Token: 0x0600254B RID: 9547 RVA: 0x00096981 File Offset: 0x00094B81
		public int DeletedItemsInFolder
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.DeletedItemsInFolder];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.DeletedItemsInFolder] = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x00096999 File Offset: 0x00094B99
		// (set) Token: 0x0600254D RID: 9549 RVA: 0x000969B0 File Offset: 0x00094BB0
		public ByteQuantifiedSize FolderSize
		{
			get
			{
				return (ByteQuantifiedSize)this.propertyBag[MapiFolderConfigurationSchema.FolderSize];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.FolderSize] = value;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x000969C8 File Offset: 0x00094BC8
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x000969DF File Offset: 0x00094BDF
		public int ItemsInFolderAndSubfolders
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.ItemsInFolderAndSubfolders];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.ItemsInFolderAndSubfolders] = value;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x000969F7 File Offset: 0x00094BF7
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x00096A0E File Offset: 0x00094C0E
		public int DeletedItemsInFolderAndSubfolders
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.DeletedItemsInFolderAndSubfolders];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.DeletedItemsInFolderAndSubfolders] = value;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00096A26 File Offset: 0x00094C26
		// (set) Token: 0x06002553 RID: 9555 RVA: 0x00096A3D File Offset: 0x00094C3D
		public ByteQuantifiedSize FolderAndSubfolderSize
		{
			get
			{
				return (ByteQuantifiedSize)this.propertyBag[MapiFolderConfigurationSchema.FolderAndSubfolderSize];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.FolderAndSubfolderSize] = value;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x00096A55 File Offset: 0x00094C55
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x00096A6C File Offset: 0x00094C6C
		public DateTime? OldestItemReceivedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.OldestItemReceivedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.OldestItemReceivedDate] = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00096A84 File Offset: 0x00094C84
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x00096A9B File Offset: 0x00094C9B
		public DateTime? NewestItemReceivedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.NewestItemReceivedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.NewestItemReceivedDate] = value;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x00096AB3 File Offset: 0x00094CB3
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x00096ACA File Offset: 0x00094CCA
		public DateTime? OldestDeletedItemReceivedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.OldestDeletedItemReceivedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.OldestDeletedItemReceivedDate] = value;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x00096AE2 File Offset: 0x00094CE2
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x00096AF9 File Offset: 0x00094CF9
		public DateTime? NewestDeletedItemReceivedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.NewestDeletedItemReceivedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.NewestDeletedItemReceivedDate] = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x00096B11 File Offset: 0x00094D11
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x00096B28 File Offset: 0x00094D28
		public DateTime? OldestItemLastModifiedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.OldestItemLastModifiedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.OldestItemLastModifiedDate] = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x00096B40 File Offset: 0x00094D40
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x00096B57 File Offset: 0x00094D57
		public DateTime? NewestItemLastModifiedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.NewestItemLastModifiedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.NewestItemLastModifiedDate] = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x00096B6F File Offset: 0x00094D6F
		// (set) Token: 0x06002561 RID: 9569 RVA: 0x00096B86 File Offset: 0x00094D86
		public DateTime? OldestDeletedItemLastModifiedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.OldestDeletedItemLastModifiedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.OldestDeletedItemLastModifiedDate] = value;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00096B9E File Offset: 0x00094D9E
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x00096BB5 File Offset: 0x00094DB5
		public DateTime? NewestDeletedItemLastModifiedDate
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.NewestDeletedItemLastModifiedDate];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.NewestDeletedItemLastModifiedDate] = value;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00096BCD File Offset: 0x00094DCD
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x00096BE4 File Offset: 0x00094DE4
		public ELCFolderIdParameter ManagedFolder
		{
			get
			{
				return (ELCFolderIdParameter)this.propertyBag[MapiFolderConfigurationSchema.ManagedFolder];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.ManagedFolder] = value;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00096BF7 File Offset: 0x00094DF7
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00096C0E File Offset: 0x00094E0E
		public RetentionPolicyTagIdParameter DeletePolicy
		{
			get
			{
				return (RetentionPolicyTagIdParameter)this.propertyBag[MapiFolderConfigurationSchema.DeletePolicy];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.DeletePolicy] = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x00096C21 File Offset: 0x00094E21
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x00096C38 File Offset: 0x00094E38
		public RetentionPolicyTagIdParameter ArchivePolicy
		{
			get
			{
				return (RetentionPolicyTagIdParameter)this.propertyBag[MapiFolderConfigurationSchema.ArchivePolicy];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.ArchivePolicy] = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600256A RID: 9578 RVA: 0x00096C4B File Offset: 0x00094E4B
		// (set) Token: 0x0600256B RID: 9579 RVA: 0x00096C5D File Offset: 0x00094E5D
		public string TopSubject
		{
			get
			{
				return (string)this[MapiFolderConfigurationSchema.TopSubject];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.TopSubject] = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x00096C6B File Offset: 0x00094E6B
		// (set) Token: 0x0600256D RID: 9581 RVA: 0x00096C82 File Offset: 0x00094E82
		public ByteQuantifiedSize TopSubjectSize
		{
			get
			{
				return (ByteQuantifiedSize)this.propertyBag[MapiFolderConfigurationSchema.TopSubjectSize];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopSubjectSize] = value;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x00096C9A File Offset: 0x00094E9A
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x00096CB1 File Offset: 0x00094EB1
		public int TopSubjectCount
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.TopSubjectCount];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopSubjectCount] = value;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x00096CC9 File Offset: 0x00094EC9
		// (set) Token: 0x06002571 RID: 9585 RVA: 0x00096CE0 File Offset: 0x00094EE0
		public string TopSubjectClass
		{
			get
			{
				return (string)this.propertyBag[MapiFolderConfigurationSchema.TopSubjectClass];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopSubjectClass] = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x00096CF3 File Offset: 0x00094EF3
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x00096D05 File Offset: 0x00094F05
		public string TopSubjectPath
		{
			get
			{
				return (string)this[MapiFolderConfigurationSchema.TopSubjectPath];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.TopSubjectPath] = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x00096D13 File Offset: 0x00094F13
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x00096D2A File Offset: 0x00094F2A
		public DateTime? TopSubjectReceivedTime
		{
			get
			{
				return (DateTime?)this.propertyBag[MapiFolderConfigurationSchema.TopSubjectReceivedTime];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopSubjectReceivedTime] = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x00096D42 File Offset: 0x00094F42
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x00096D54 File Offset: 0x00094F54
		public string TopSubjectFrom
		{
			get
			{
				return (string)this[MapiFolderConfigurationSchema.TopSubjectFrom];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.TopSubjectFrom] = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x00096D62 File Offset: 0x00094F62
		// (set) Token: 0x06002579 RID: 9593 RVA: 0x00096D79 File Offset: 0x00094F79
		public string TopClientInfoForSubject
		{
			get
			{
				return (string)this.propertyBag[MapiFolderConfigurationSchema.TopClientInfoForSubject];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopClientInfoForSubject] = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x00096D8C File Offset: 0x00094F8C
		// (set) Token: 0x0600257B RID: 9595 RVA: 0x00096DA3 File Offset: 0x00094FA3
		public int TopClientInfoCountForSubject
		{
			get
			{
				return (int)this.propertyBag[MapiFolderConfigurationSchema.TopClientInfoCountForSubject];
			}
			internal set
			{
				this.propertyBag[MapiFolderConfigurationSchema.TopClientInfoCountForSubject] = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00096DBB File Offset: 0x00094FBB
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x00096DCD File Offset: 0x00094FCD
		public string[] SearchFolders
		{
			get
			{
				return (string[])this[MapiFolderConfigurationSchema.SearchFolders];
			}
			internal set
			{
				this[MapiFolderConfigurationSchema.SearchFolders] = value;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00096DDC File Offset: 0x00094FDC
		public override ObjectId Identity
		{
			get
			{
				ObjectId objectId = (ObjectId)this[MapiFolderConfigurationSchema.Identity];
				if (SuppressingPiiContext.NeedPiiSuppression && objectId is MailboxFolderId)
				{
					string text = objectId.ToString();
					int num = text.IndexOf('\\');
					if (num <= 0 || num >= text.Length - 1)
					{
						return objectId;
					}
					string value = text.Substring(0, num);
					string value2 = text.Substring(num);
					objectId = new MailboxFolderId(SuppressingPiiData.Redact(value), SuppressingPiiData.Redact(value2));
				}
				return objectId;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x00096E50 File Offset: 0x00095050
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxFolderConfiguration.schema;
			}
		}

		// Token: 0x04001D51 RID: 7505
		private static MapiFolderConfigurationSchema schema = new MapiFolderConfigurationSchema();
	}
}
