using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200056F RID: 1391
	[XmlType("UnpinTeamMailboxResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UnpinTeamMailboxResponseMessage : ResponseMessage
	{
		// Token: 0x060026E6 RID: 9958 RVA: 0x000A6BAA File Offset: 0x000A4DAA
		public UnpinTeamMailboxResponseMessage()
		{
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000A6BB2 File Offset: 0x000A4DB2
		public override ResponseType GetResponseType()
		{
			return ResponseType.UnpinTeamMailboxResponseMessage;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000A6BB6 File Offset: 0x000A4DB6
		internal UnpinTeamMailboxResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
