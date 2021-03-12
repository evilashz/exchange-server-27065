using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C3 RID: 1219
	[XmlType("CreateUnifiedMailboxResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateUnifiedMailboxResponseMessage : ResponseMessage
	{
		// Token: 0x06002406 RID: 9222 RVA: 0x000A46C8 File Offset: 0x000A28C8
		public CreateUnifiedMailboxResponseMessage()
		{
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A46D0 File Offset: 0x000A28D0
		internal CreateUnifiedMailboxResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
