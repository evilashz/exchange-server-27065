using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FB RID: 1275
	[XmlType(TypeName = "GetHoldOnMailboxesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetHoldOnMailboxesResponse : ResponseMessage
	{
		// Token: 0x060024F4 RID: 9460 RVA: 0x000A5522 File Offset: 0x000A3722
		public GetHoldOnMailboxesResponse()
		{
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x000A552A File Offset: 0x000A372A
		internal GetHoldOnMailboxesResponse(ServiceResultCode code, ServiceError error, MailboxHoldResult mailboxHoldResult) : base(code, error)
		{
			this.mailboxHoldResult = mailboxHoldResult;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000A553B File Offset: 0x000A373B
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x000A5543 File Offset: 0x000A3743
		[XmlElement("MailboxHoldResult")]
		[DataMember(Name = "MailboxHoldResult", IsRequired = false)]
		public MailboxHoldResult MailboxHoldResult
		{
			get
			{
				return this.mailboxHoldResult;
			}
			set
			{
				this.mailboxHoldResult = value;
			}
		}

		// Token: 0x040015A1 RID: 5537
		private MailboxHoldResult mailboxHoldResult;
	}
}
