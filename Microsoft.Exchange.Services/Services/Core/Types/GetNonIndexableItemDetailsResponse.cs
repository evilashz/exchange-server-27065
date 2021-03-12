using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000503 RID: 1283
	[XmlType(TypeName = "GetNonIndexableItemDetailsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetNonIndexableItemDetailsResponse : ResponseMessage
	{
		// Token: 0x06002517 RID: 9495 RVA: 0x000A5696 File Offset: 0x000A3896
		public GetNonIndexableItemDetailsResponse()
		{
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000A569E File Offset: 0x000A389E
		internal GetNonIndexableItemDetailsResponse(ServiceResultCode code, ServiceError error, NonIndexableItemDetailResult result) : base(code, error)
		{
			this.NonIndexableItemDetailsResult = result;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000A56AF File Offset: 0x000A38AF
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x000A56B7 File Offset: 0x000A38B7
		[XmlElement("NonIndexableItemDetailsResult")]
		[DataMember(Name = "NonIndexableItemDetailsResult", IsRequired = false)]
		public NonIndexableItemDetailResult NonIndexableItemDetailsResult { get; set; }
	}
}
