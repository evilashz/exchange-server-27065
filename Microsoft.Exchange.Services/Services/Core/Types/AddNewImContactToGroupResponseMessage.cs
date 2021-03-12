using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004AA RID: 1194
	[XmlType("AddNewImContactToGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddNewImContactToGroupResponseMessage : ResponseMessage
	{
		// Token: 0x060023B4 RID: 9140 RVA: 0x000A4241 File Offset: 0x000A2441
		public AddNewImContactToGroupResponseMessage()
		{
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x000A4249 File Offset: 0x000A2449
		internal AddNewImContactToGroupResponseMessage(ServiceResultCode code, ServiceError error, Persona result) : base(code, error)
		{
			this.Persona = result;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x000A425A File Offset: 0x000A245A
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddNewImContactToGroupResponseMessage;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x000A425E File Offset: 0x000A245E
		// (set) Token: 0x060023B8 RID: 9144 RVA: 0x000A4266 File Offset: 0x000A2466
		[XmlElement]
		[DataMember]
		public Persona Persona { get; set; }
	}
}
