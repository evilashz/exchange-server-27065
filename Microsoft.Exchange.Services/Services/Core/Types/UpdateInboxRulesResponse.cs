using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000575 RID: 1397
	[XmlType("UpdateInboxRulesResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class UpdateInboxRulesResponse : ResponseMessage
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000A6C0F File Offset: 0x000A4E0F
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x000A6C17 File Offset: 0x000A4E17
		[XmlArrayItem("RuleOperationError", Type = typeof(RuleOperationError), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray]
		public RuleOperationError[] RuleOperationErrors { get; set; }

		// Token: 0x060026F3 RID: 9971 RVA: 0x000A6C20 File Offset: 0x000A4E20
		public UpdateInboxRulesResponse()
		{
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000A6C28 File Offset: 0x000A4E28
		internal UpdateInboxRulesResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
