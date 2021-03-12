using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055A RID: 1370
	[XmlType("SetTeamMailboxResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetTeamMailboxResponseMessage : ResponseMessage
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x000A6653 File Offset: 0x000A4853
		public SetTeamMailboxResponseMessage()
		{
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000A665B File Offset: 0x000A485B
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetTeamMailboxResponseMessage;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000A665F File Offset: 0x000A485F
		internal SetTeamMailboxResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
