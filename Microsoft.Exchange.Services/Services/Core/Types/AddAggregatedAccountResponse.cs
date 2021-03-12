using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A3 RID: 1187
	[KnownType(typeof(AggregatedAccountType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(AggregatedAccountType))]
	[XmlType("AddAggregatedAccountResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddAggregatedAccountResponse : ResponseMessage
	{
		// Token: 0x06002397 RID: 9111 RVA: 0x000A4137 File Offset: 0x000A2337
		public AddAggregatedAccountResponse()
		{
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A413F File Offset: 0x000A233F
		internal AddAggregatedAccountResponse(ServiceResultCode code, ServiceError error, AggregatedAccountType account) : base(code, error)
		{
			this.Account = account;
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000A4150 File Offset: 0x000A2350
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddAggregatedAccountResponseMessage;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x000A4154 File Offset: 0x000A2354
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x000A415C File Offset: 0x000A235C
		[XmlElement("Account")]
		[DataMember]
		public AggregatedAccountType Account { get; set; }
	}
}
