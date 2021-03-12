using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003E6 RID: 998
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "AddressListIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class AddressListId : BaseFolderId
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x0009DBF0 File Offset: 0x0009BDF0
		// (set) Token: 0x06001BFD RID: 7165 RVA: 0x0009DBF8 File Offset: 0x0009BDF8
		[DataMember(IsRequired = true, Order = 1)]
		[XmlAttribute]
		public string Id { get; set; }

		// Token: 0x06001BFE RID: 7166 RVA: 0x0009DC01 File Offset: 0x0009BE01
		public AddressListId()
		{
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0009DC09 File Offset: 0x0009BE09
		public AddressListId(string id)
		{
			this.Id = id;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0009DC18 File Offset: 0x0009BE18
		public override string GetId()
		{
			return this.Id;
		}
	}
}
