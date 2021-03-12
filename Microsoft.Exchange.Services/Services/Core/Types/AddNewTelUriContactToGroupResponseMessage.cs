using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004AB RID: 1195
	[XmlType("AddNewTelUriContactToGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddNewTelUriContactToGroupResponseMessage : ResponseMessage
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x000A426F File Offset: 0x000A246F
		public AddNewTelUriContactToGroupResponseMessage()
		{
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x000A4277 File Offset: 0x000A2477
		internal AddNewTelUriContactToGroupResponseMessage(ServiceResultCode code, ServiceError error, Persona result) : base(code, error)
		{
			this.Persona = result;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000A4288 File Offset: 0x000A2488
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddNewTelUriContactToGroupResponseMessage;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000A428F File Offset: 0x000A248F
		// (set) Token: 0x060023BD RID: 9149 RVA: 0x000A4297 File Offset: 0x000A2497
		[DataMember]
		[XmlElement]
		public Persona Persona { get; set; }
	}
}
