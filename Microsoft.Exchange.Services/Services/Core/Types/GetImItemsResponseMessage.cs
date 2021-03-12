using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FD RID: 1277
	[XmlType("GetImItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetImItemsResponseMessage : ResponseMessage
	{
		// Token: 0x060024FD RID: 9469 RVA: 0x000A557A File Offset: 0x000A377A
		public GetImItemsResponseMessage()
		{
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000A5582 File Offset: 0x000A3782
		internal GetImItemsResponseMessage(ServiceResultCode code, ServiceError error, ImItemList result) : base(code, error)
		{
			this.ImItemList = result;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000A5593 File Offset: 0x000A3793
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetImItemsResponseMessage;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000A5597 File Offset: 0x000A3797
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x000A559F File Offset: 0x000A379F
		[XmlElement]
		[DataMember]
		public ImItemList ImItemList { get; set; }
	}
}
