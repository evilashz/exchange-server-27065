using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000822 RID: 2082
	[DataContract(Name = "NonIndexableItemDetailResult", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "NonIndexableItemDetailResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NonIndexableItemDetailResult
	{
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000D5B03 File Offset: 0x000D3D03
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x000D5B0B File Offset: 0x000D3D0B
		[XmlArray(ElementName = "Items", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "NonIndexableItemDetail", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(NonIndexableItemDetail))]
		[DataMember(Name = "Items", EmitDefaultValue = false, IsRequired = false)]
		public NonIndexableItemDetail[] Items { get; set; }

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x000D5B14 File Offset: 0x000D3D14
		// (set) Token: 0x06003C45 RID: 15429 RVA: 0x000D5B1C File Offset: 0x000D3D1C
		[DataMember(Name = "FailedMailboxes", EmitDefaultValue = false, IsRequired = false)]
		[XmlArray(ElementName = "FailedMailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "FailedMailbox", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(FailedSearchMailbox))]
		public FailedSearchMailbox[] FailedMailboxes { get; set; }
	}
}
