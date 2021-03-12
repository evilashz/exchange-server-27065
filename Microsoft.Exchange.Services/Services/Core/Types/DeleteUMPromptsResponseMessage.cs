using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004CB RID: 1227
	[XmlType("DeleteUMPromptsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DeleteUMPromptsResponseMessage : ResponseMessage
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x000A47C2 File Offset: 0x000A29C2
		public DeleteUMPromptsResponseMessage()
		{
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000A47CA File Offset: 0x000A29CA
		internal DeleteUMPromptsResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000A47D4 File Offset: 0x000A29D4
		public override ResponseType GetResponseType()
		{
			return ResponseType.DeleteUMPromptsResponseMessage;
		}
	}
}
