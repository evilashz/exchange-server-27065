using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000540 RID: 1344
	[XmlType("RemoveImContactFromGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveImContactFromGroupResponseMessage : ResponseMessage
	{
		// Token: 0x0600262A RID: 9770 RVA: 0x000A6396 File Offset: 0x000A4596
		public RemoveImContactFromGroupResponseMessage()
		{
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000A639E File Offset: 0x000A459E
		internal RemoveImContactFromGroupResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x000A63A8 File Offset: 0x000A45A8
		public override ResponseType GetResponseType()
		{
			return ResponseType.RemoveImContactFromGroupResponseMessage;
		}
	}
}
