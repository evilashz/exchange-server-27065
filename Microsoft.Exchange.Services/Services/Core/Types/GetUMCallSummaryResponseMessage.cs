using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051C RID: 1308
	[XmlType("GetUMCallSummaryResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUMCallSummaryResponseMessage : ResponseMessage
	{
		// Token: 0x0600258C RID: 9612 RVA: 0x000A5BDD File Offset: 0x000A3DDD
		public GetUMCallSummaryResponseMessage()
		{
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000A5BE5 File Offset: 0x000A3DE5
		internal GetUMCallSummaryResponseMessage(ServiceResultCode code, ServiceError error, GetUMCallSummaryResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.UMReportRawCountersCollection = response.UMReportRawCountersCollection;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000A5BFE File Offset: 0x000A3DFE
		// (set) Token: 0x0600258F RID: 9615 RVA: 0x000A5C06 File Offset: 0x000A3E06
		[XmlArrayItem(ElementName = "UMReportRawCounters", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray(ElementName = "UMReportRawCountersCollection")]
		[DataMember]
		public UMReportRawCounters[] UMReportRawCountersCollection { get; set; }

		// Token: 0x06002590 RID: 9616 RVA: 0x000A5C0F File Offset: 0x000A3E0F
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMCallSummaryResponseMessage;
		}
	}
}
