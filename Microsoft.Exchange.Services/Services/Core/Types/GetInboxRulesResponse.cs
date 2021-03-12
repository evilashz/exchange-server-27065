using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FE RID: 1278
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetInboxRulesResponse : ResponseMessage
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000A55A8 File Offset: 0x000A37A8
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x000A55B0 File Offset: 0x000A37B0
		[DataMember(Order = 1)]
		[XmlElement]
		public bool OutlookRuleBlobExists { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000A55B9 File Offset: 0x000A37B9
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x000A55C1 File Offset: 0x000A37C1
		[XmlArrayItem("Rule", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Order = 2)]
		[XmlArray]
		public EwsRule[] InboxRules { get; set; }

		// Token: 0x06002506 RID: 9478 RVA: 0x000A55CA File Offset: 0x000A37CA
		public GetInboxRulesResponse()
		{
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000A55D2 File Offset: 0x000A37D2
		internal GetInboxRulesResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
