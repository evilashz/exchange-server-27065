using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000555 RID: 1365
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SetClutterStateResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class SetClutterStateResponse : ResponseMessage
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000A65B7 File Offset: 0x000A47B7
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x000A65BF File Offset: 0x000A47BF
		[XmlElement(ElementName = "ClutterState")]
		[DataMember]
		public ClutterState ClutterState { get; set; }

		// Token: 0x06002660 RID: 9824 RVA: 0x000A65C8 File Offset: 0x000A47C8
		public SetClutterStateResponse()
		{
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000A65D0 File Offset: 0x000A47D0
		internal SetClutterStateResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000A65DA File Offset: 0x000A47DA
		public override ResponseType GetResponseType()
		{
			return ResponseType.SetClutterStateResponseMessage;
		}
	}
}
