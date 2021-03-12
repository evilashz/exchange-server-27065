using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000557 RID: 1367
	[XmlType(TypeName = "SetHoldOnMailboxesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SetHoldOnMailboxesResponse : ResponseMessage
	{
		// Token: 0x06002666 RID: 9830 RVA: 0x000A65FA File Offset: 0x000A47FA
		public SetHoldOnMailboxesResponse()
		{
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000A6602 File Offset: 0x000A4802
		internal SetHoldOnMailboxesResponse(ServiceResultCode code, ServiceError error, MailboxHoldResult mailboxHoldResult) : base(code, error)
		{
			this.mailboxHoldResult = mailboxHoldResult;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x000A6613 File Offset: 0x000A4813
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x000A661B File Offset: 0x000A481B
		[DataMember(Name = "MailboxHoldResult", IsRequired = false)]
		[XmlElement("MailboxHoldResult")]
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

		// Token: 0x040018A7 RID: 6311
		private MailboxHoldResult mailboxHoldResult;
	}
}
