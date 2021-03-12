using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A9 RID: 1193
	[XmlType("AddImGroupResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddImGroupResponseMessage : ResponseMessage
	{
		// Token: 0x060023AF RID: 9135 RVA: 0x000A4213 File Offset: 0x000A2413
		public AddImGroupResponseMessage()
		{
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A421B File Offset: 0x000A241B
		internal AddImGroupResponseMessage(ServiceResultCode code, ServiceError error, ImGroup result) : base(code, error)
		{
			this.ImGroup = result;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000A422C File Offset: 0x000A242C
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddImGroupResponseMessage;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000A4230 File Offset: 0x000A2430
		// (set) Token: 0x060023B3 RID: 9139 RVA: 0x000A4238 File Offset: 0x000A2438
		[DataMember]
		[XmlElement]
		public ImGroup ImGroup { get; set; }
	}
}
