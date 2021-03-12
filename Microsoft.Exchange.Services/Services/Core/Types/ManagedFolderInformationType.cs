using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E8 RID: 1512
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ManagedFolderInformationType
	{
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000B23BA File Offset: 0x000B05BA
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x000B23C2 File Offset: 0x000B05C2
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public bool CanDelete
		{
			get
			{
				return this.canDelete;
			}
			set
			{
				this.CanDeleteSpecified = true;
				this.canDelete = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000B23D2 File Offset: 0x000B05D2
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000B23DA File Offset: 0x000B05DA
		[IgnoreDataMember]
		[XmlIgnore]
		public bool CanDeleteSpecified { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000B23E3 File Offset: 0x000B05E3
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x000B23EB File Offset: 0x000B05EB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public bool CanRenameOrMove
		{
			get
			{
				return this.canRenameOrMove;
			}
			set
			{
				this.CanRenameOrMoveSpecified = true;
				this.canRenameOrMove = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000B23FB File Offset: 0x000B05FB
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000B2403 File Offset: 0x000B0603
		[IgnoreDataMember]
		[XmlIgnore]
		public bool CanRenameOrMoveSpecified { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000B240C File Offset: 0x000B060C
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000B2414 File Offset: 0x000B0614
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public bool MustDisplayComment
		{
			get
			{
				return this.mustDisplayComment;
			}
			set
			{
				this.MustDisplayCommentSpecified = true;
				this.mustDisplayComment = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000B2424 File Offset: 0x000B0624
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x000B242C File Offset: 0x000B062C
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MustDisplayCommentSpecified { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000B2435 File Offset: 0x000B0635
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x000B243D File Offset: 0x000B063D
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public bool HasQuota
		{
			get
			{
				return this.hasQuota;
			}
			set
			{
				this.HasQuotaSpecified = true;
				this.hasQuota = value;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000B244D File Offset: 0x000B064D
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x000B2455 File Offset: 0x000B0655
		[IgnoreDataMember]
		[XmlIgnore]
		public bool HasQuotaSpecified { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x000B245E File Offset: 0x000B065E
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x000B2466 File Offset: 0x000B0666
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public bool IsManagedFoldersRoot
		{
			get
			{
				return this.isManagedFoldersRoot;
			}
			set
			{
				this.IsManagedFoldersRootSpecified = true;
				this.isManagedFoldersRoot = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x000B2476 File Offset: 0x000B0676
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x000B247E File Offset: 0x000B067E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsManagedFoldersRootSpecified { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x000B2487 File Offset: 0x000B0687
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x000B248F File Offset: 0x000B068F
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string ManagedFolderId { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000B2498 File Offset: 0x000B0698
		// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x000B24A0 File Offset: 0x000B06A0
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string Comment { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x000B24A9 File Offset: 0x000B06A9
		// (set) Token: 0x06002DA7 RID: 11687 RVA: 0x000B24B1 File Offset: 0x000B06B1
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public int StorageQuota
		{
			get
			{
				return this.storageQuota;
			}
			set
			{
				this.StorageQuotaSpecified = true;
				this.storageQuota = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000B24C1 File Offset: 0x000B06C1
		// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x000B24C9 File Offset: 0x000B06C9
		[IgnoreDataMember]
		[XmlIgnore]
		public bool StorageQuotaSpecified { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x000B24D2 File Offset: 0x000B06D2
		// (set) Token: 0x06002DAB RID: 11691 RVA: 0x000B24DA File Offset: 0x000B06DA
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public int FolderSize
		{
			get
			{
				return this.folderSize;
			}
			set
			{
				this.FolderSizeSpecified = true;
				this.folderSize = value;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000B24EA File Offset: 0x000B06EA
		// (set) Token: 0x06002DAD RID: 11693 RVA: 0x000B24F2 File Offset: 0x000B06F2
		[XmlIgnore]
		[IgnoreDataMember]
		public bool FolderSizeSpecified { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x000B24FB File Offset: 0x000B06FB
		// (set) Token: 0x06002DAF RID: 11695 RVA: 0x000B2503 File Offset: 0x000B0703
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string HomePage { get; set; }

		// Token: 0x04001B3D RID: 6973
		private bool canDelete;

		// Token: 0x04001B3E RID: 6974
		private bool canRenameOrMove;

		// Token: 0x04001B3F RID: 6975
		private bool mustDisplayComment;

		// Token: 0x04001B40 RID: 6976
		private bool hasQuota;

		// Token: 0x04001B41 RID: 6977
		private bool isManagedFoldersRoot;

		// Token: 0x04001B42 RID: 6978
		private int storageQuota;

		// Token: 0x04001B43 RID: 6979
		private int folderSize;
	}
}
