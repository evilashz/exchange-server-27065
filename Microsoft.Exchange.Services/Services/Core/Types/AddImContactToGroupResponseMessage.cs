using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A8 RID: 1192
	[XmlType("AddImContactToGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddImContactToGroupResponseMessage : ResponseMessage
	{
		// Token: 0x060023AC RID: 9132 RVA: 0x000A41FD File Offset: 0x000A23FD
		public AddImContactToGroupResponseMessage()
		{
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000A4205 File Offset: 0x000A2405
		internal AddImContactToGroupResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000A420F File Offset: 0x000A240F
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddImContactToGroupResponseMessage;
		}
	}
}
