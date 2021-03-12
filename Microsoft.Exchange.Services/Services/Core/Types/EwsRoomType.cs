using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000635 RID: 1589
	[XmlType(TypeName = "RoomType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "RoomType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class EwsRoomType
	{
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000B7379 File Offset: 0x000B5579
		// (set) Token: 0x060031A1 RID: 12705 RVA: 0x000B7381 File Offset: 0x000B5581
		[DataMember(Name = "Id")]
		[XmlElement("Id")]
		public EmailAddressWrapper Mailbox { get; set; }
	}
}
