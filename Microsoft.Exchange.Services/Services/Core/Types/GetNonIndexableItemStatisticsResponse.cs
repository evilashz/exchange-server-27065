using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000504 RID: 1284
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetNonIndexableItemStatisticsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetNonIndexableItemStatisticsResponse : ResponseMessage
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x000A56C0 File Offset: 0x000A38C0
		public GetNonIndexableItemStatisticsResponse()
		{
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x000A56C8 File Offset: 0x000A38C8
		internal GetNonIndexableItemStatisticsResponse(ServiceResultCode code, ServiceError error, NonIndexableItemStatisticResult[] results) : base(code, error)
		{
			this.NonIndexableItemStatisticsResult = results;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000A56D9 File Offset: 0x000A38D9
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000A56E1 File Offset: 0x000A38E1
		[DataMember(Name = "NonIndexableItemStatistics", IsRequired = false)]
		[XmlArrayItem(ElementName = "NonIndexableItemStatistic", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(NonIndexableItemStatisticResult))]
		[XmlArray(ElementName = "NonIndexableItemStatistics", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public NonIndexableItemStatisticResult[] NonIndexableItemStatisticsResult { get; set; }
	}
}
