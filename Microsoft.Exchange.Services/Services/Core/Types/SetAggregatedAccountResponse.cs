using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000552 RID: 1362
	[KnownType(typeof(AggregatedAccountType))]
	[XmlInclude(typeof(AggregatedAccountType))]
	[XmlType("SetAggregatedAccountResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetAggregatedAccountResponse : ResponseMessage
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x000A656A File Offset: 0x000A476A
		public SetAggregatedAccountResponse()
		{
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000A6572 File Offset: 0x000A4772
		internal SetAggregatedAccountResponse(ServiceResultCode code, ServiceError error, AggregatedAccountType account) : base(code, error)
		{
			this.Account = account;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000A6583 File Offset: 0x000A4783
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetAggregatedAccountResponseMessage;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000A6587 File Offset: 0x000A4787
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x000A658F File Offset: 0x000A478F
		[XmlElement("Account")]
		public AggregatedAccountType Account { get; set; }
	}
}
