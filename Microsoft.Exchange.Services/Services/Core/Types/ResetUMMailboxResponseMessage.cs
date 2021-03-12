using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000543 RID: 1347
	[XmlType("ResetUMMailboxResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ResetUMMailboxResponseMessage : ResponseMessage
	{
		// Token: 0x06002633 RID: 9779 RVA: 0x000A63DB File Offset: 0x000A45DB
		public ResetUMMailboxResponseMessage()
		{
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000A63E3 File Offset: 0x000A45E3
		internal ResetUMMailboxResponseMessage(ServiceResultCode code, ServiceError error, ResetUMMailboxResponseMessage response) : base(code, error)
		{
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000A63ED File Offset: 0x000A45ED
		public override ResponseType GetResponseType()
		{
			return ResponseType.ResetUMMailboxResponseMessage;
		}
	}
}
