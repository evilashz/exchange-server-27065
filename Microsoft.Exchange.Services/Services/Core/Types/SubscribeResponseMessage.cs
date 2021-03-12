using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055D RID: 1373
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SubscribeResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SubscribeResponseMessage : ResponseMessage
	{
		// Token: 0x06002679 RID: 9849 RVA: 0x000A66A4 File Offset: 0x000A48A4
		public SubscribeResponseMessage()
		{
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000A66AC File Offset: 0x000A48AC
		internal SubscribeResponseMessage(ServiceResultCode code, ServiceError error, SubscriptionBase value) : base(code, error)
		{
			if (value != null)
			{
				this.SubscriptionId = value.SubscriptionId;
				if (value.UseWatermarks)
				{
					this.Watermark = value.LastWatermarkSent;
				}
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000A66D9 File Offset: 0x000A48D9
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x000A66E1 File Offset: 0x000A48E1
		[DataMember(Name = "SubscriptionId", EmitDefaultValue = false)]
		[XmlElement("SubscriptionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SubscriptionId { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x000A66EA File Offset: 0x000A48EA
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x000A66F2 File Offset: 0x000A48F2
		[DataMember(Name = "Watermark", EmitDefaultValue = false)]
		[XmlElement("Watermark", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string Watermark { get; set; }
	}
}
