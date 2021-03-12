using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000171 RID: 369
	[DataContract]
	internal class HierarchyNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000334E1 File Offset: 0x000316E1
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x000334F3 File Offset: 0x000316F3
		[DataMember(Name = "FolderType")]
		public string FolderTypeString
		{
			get
			{
				return this.FolderType.ToString();
			}
			set
			{
				this.FolderType = (StoreObjectType)Enum.Parse(typeof(StoreObjectType), value);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00033510 File Offset: 0x00031710
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x00033518 File Offset: 0x00031718
		public string FolderId
		{
			get
			{
				return this.folderId;
			}
			set
			{
				this.folderId = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00033521 File Offset: 0x00031721
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x00033529 File Offset: 0x00031729
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00033532 File Offset: 0x00031732
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0003353A File Offset: 0x0003173A
		public string ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x00033543 File Offset: 0x00031743
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0003354B File Offset: 0x0003174B
		public long ItemCount
		{
			get
			{
				return this.itemCount;
			}
			set
			{
				this.itemCount = value;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x00033554 File Offset: 0x00031754
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0003355C File Offset: 0x0003175C
		public long UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
			set
			{
				this.unreadCount = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00033565 File Offset: 0x00031765
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0003356D File Offset: 0x0003176D
		[IgnoreDataMember]
		internal StoreObjectType FolderType { get; set; }

		// Token: 0x04000843 RID: 2115
		[DataMember]
		private string folderId;

		// Token: 0x04000844 RID: 2116
		[DataMember]
		private string displayName;

		// Token: 0x04000845 RID: 2117
		[DataMember]
		private string parentFolderId;

		// Token: 0x04000846 RID: 2118
		[DataMember]
		private long itemCount;

		// Token: 0x04000847 RID: 2119
		[DataMember]
		private long unreadCount;
	}
}
