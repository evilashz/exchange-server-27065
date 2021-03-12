using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200066F RID: 1647
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SyncFolderItemsReadFlagType : SyncFolderItemsChangeTypeBase
	{
		// Token: 0x0600325B RID: 12891 RVA: 0x000B7AA7 File Offset: 0x000B5CA7
		public SyncFolderItemsReadFlagType()
		{
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000B7AAF File Offset: 0x000B5CAF
		public SyncFolderItemsReadFlagType(ItemId itemId, bool isRead)
		{
			this.ItemId = itemId;
			this.IsRead = isRead;
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000B7AC5 File Offset: 0x000B5CC5
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000B7ACD File Offset: 0x000B5CCD
		[DataMember(IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000B7AD6 File Offset: 0x000B5CD6
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x000B7ADE File Offset: 0x000B5CDE
		[DataMember(EmitDefaultValue = true, IsRequired = true)]
		public bool IsRead { get; set; }

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000B7AE7 File Offset: 0x000B5CE7
		public override SyncFolderItemsChangesEnum ChangeType
		{
			get
			{
				return SyncFolderItemsChangesEnum.ReadFlagChange;
			}
		}
	}
}
