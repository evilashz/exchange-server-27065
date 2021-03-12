using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FC RID: 1276
	[XmlType("GetImItemListResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetImItemListResponseMessage : ResponseMessage
	{
		// Token: 0x060024F8 RID: 9464 RVA: 0x000A554C File Offset: 0x000A374C
		public GetImItemListResponseMessage()
		{
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000A5554 File Offset: 0x000A3754
		internal GetImItemListResponseMessage(ServiceResultCode code, ServiceError error, ImItemList result) : base(code, error)
		{
			this.ImItemList = result;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x000A5565 File Offset: 0x000A3765
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetImItemListResponseMessage;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000A5569 File Offset: 0x000A3769
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x000A5571 File Offset: 0x000A3771
		[XmlElement]
		[DataMember]
		public ImItemList ImItemList { get; set; }
	}
}
