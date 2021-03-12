using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004E4 RID: 1252
	[XmlType("GetAggregatedAccountResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetAggregatedAccountResponse : ResponseMessage
	{
		// Token: 0x06002491 RID: 9361 RVA: 0x000A4FE0 File Offset: 0x000A31E0
		public GetAggregatedAccountResponse()
		{
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000A4FE8 File Offset: 0x000A31E8
		internal GetAggregatedAccountResponse(ServiceResultCode code, ServiceError error, AggregatedAccountType[] aggregatedAccounts) : base(code, error)
		{
			this.AggregatedAccounts = aggregatedAccounts;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000A4FF9 File Offset: 0x000A31F9
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetAggregatedAccountResponseMessage;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x000A4FFD File Offset: 0x000A31FD
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x000A5005 File Offset: 0x000A3205
		[XmlArrayItem("AggregatedAccounts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("AggregatedAccounts", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember]
		public AggregatedAccountType[] AggregatedAccounts { get; set; }
	}
}
