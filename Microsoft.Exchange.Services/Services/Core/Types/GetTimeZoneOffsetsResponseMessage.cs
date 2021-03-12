using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051A RID: 1306
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetTimeZoneOffsetsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetTimeZoneOffsetsResponseMessage : ResponseMessage
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x000A5B7A File Offset: 0x000A3D7A
		public GetTimeZoneOffsetsResponseMessage()
		{
		}

		// Token: 0x06002583 RID: 9603 RVA: 0x000A5B82 File Offset: 0x000A3D82
		internal GetTimeZoneOffsetsResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000A5B8C File Offset: 0x000A3D8C
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetTimeZoneOffsetsResponseMessage;
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x000A5B93 File Offset: 0x000A3D93
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x000A5B9B File Offset: 0x000A3D9B
		[XmlArray(ElementName = "TimeZones")]
		[XmlArrayItem(ElementName = "TimeZone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public TimeZoneOffsetsType[] TimeZones { get; set; }
	}
}
