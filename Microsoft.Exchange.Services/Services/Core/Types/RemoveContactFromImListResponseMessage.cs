using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053D RID: 1341
	[XmlType("RemoveContactFromImListResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveContactFromImListResponseMessage : ResponseMessage
	{
		// Token: 0x06002622 RID: 9762 RVA: 0x000A6355 File Offset: 0x000A4555
		public RemoveContactFromImListResponseMessage()
		{
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000A635D File Offset: 0x000A455D
		internal RemoveContactFromImListResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000A6367 File Offset: 0x000A4567
		public override ResponseType GetResponseType()
		{
			return ResponseType.RemoveContactFromImListResponseMessage;
		}
	}
}
