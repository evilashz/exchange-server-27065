using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066E RID: 1646
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SyncFolderItemsDeleteType : SyncFolderItemsChangeTypeBase
	{
		// Token: 0x06003256 RID: 12886 RVA: 0x000B7A7C File Offset: 0x000B5C7C
		public SyncFolderItemsDeleteType()
		{
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000B7A84 File Offset: 0x000B5C84
		public SyncFolderItemsDeleteType(ItemId itemId)
		{
			this.ItemId = itemId;
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003258 RID: 12888 RVA: 0x000B7A93 File Offset: 0x000B5C93
		// (set) Token: 0x06003259 RID: 12889 RVA: 0x000B7A9B File Offset: 0x000B5C9B
		[DataMember(EmitDefaultValue = false)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000B7AA4 File Offset: 0x000B5CA4
		public override SyncFolderItemsChangesEnum ChangeType
		{
			get
			{
				return SyncFolderItemsChangesEnum.Delete;
			}
		}
	}
}
