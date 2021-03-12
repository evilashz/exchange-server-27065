using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C1 RID: 1217
	[XmlType("CreateUMPromptResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUMPromptResponseMessage : ResponseMessage
	{
		// Token: 0x060023FE RID: 9214 RVA: 0x000A4685 File Offset: 0x000A2885
		public CreateUMPromptResponseMessage()
		{
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000A468D File Offset: 0x000A288D
		internal CreateUMPromptResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000A4697 File Offset: 0x000A2897
		public override ResponseType GetResponseType()
		{
			return ResponseType.CreateUMPromptResponseMessage;
		}
	}
}
