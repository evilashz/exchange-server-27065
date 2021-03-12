using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000541 RID: 1345
	[XmlType("RemoveImGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveImGroupResponseMessage : ResponseMessage
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x000A63AC File Offset: 0x000A45AC
		public RemoveImGroupResponseMessage()
		{
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000A63B4 File Offset: 0x000A45B4
		internal RemoveImGroupResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x000A63BE File Offset: 0x000A45BE
		public override ResponseType GetResponseType()
		{
			return ResponseType.RemoveImGroupResponseMessage;
		}
	}
}
