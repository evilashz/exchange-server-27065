using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CA RID: 1226
	[XmlType(TypeName = "DeleteItemResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class DeleteItemResponseMessage : ResponseMessage
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x000A479F File Offset: 0x000A299F
		public DeleteItemResponseMessage()
		{
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000A47A7 File Offset: 0x000A29A7
		internal DeleteItemResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000A47B1 File Offset: 0x000A29B1
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x000A47B9 File Offset: 0x000A29B9
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Name = "MovedItemId")]
		public ItemId MovedItemId { get; set; }
	}
}
