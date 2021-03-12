using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003EA RID: 1002
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "TargetFolderIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class TargetFolderId
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0009DD13 File Offset: 0x0009BF13
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0009DD1B File Offset: 0x0009BF1B
		[XmlElement("AddressListId", typeof(AddressListId))]
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderId))]
		[DataMember(IsRequired = true)]
		[XmlElement("FolderId", typeof(FolderId))]
		public BaseFolderId BaseFolderId { get; set; }

		// Token: 0x06001C19 RID: 7193 RVA: 0x0009DD24 File Offset: 0x0009BF24
		public TargetFolderId()
		{
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0009DD2C File Offset: 0x0009BF2C
		public TargetFolderId(BaseFolderId baseFolderId)
		{
			this.BaseFolderId = baseFolderId;
		}
	}
}
