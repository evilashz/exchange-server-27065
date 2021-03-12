using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000527 RID: 1319
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("InitUMMailboxResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class InitUMMailboxResponseMessage : ResponseMessage
	{
		// Token: 0x060025CB RID: 9675 RVA: 0x000A5F66 File Offset: 0x000A4166
		public InitUMMailboxResponseMessage()
		{
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000A5F6E File Offset: 0x000A416E
		internal InitUMMailboxResponseMessage(ServiceResultCode code, ServiceError error, InitUMMailboxResponseMessage response) : base(code, error)
		{
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000A5F78 File Offset: 0x000A4178
		public override ResponseType GetResponseType()
		{
			return ResponseType.InitUMMailboxResponseMessage;
		}
	}
}
