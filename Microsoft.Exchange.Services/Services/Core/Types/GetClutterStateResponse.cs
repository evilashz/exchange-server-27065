using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004EF RID: 1263
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetClutterStateResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetClutterStateResponse : ResponseMessage
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060024BF RID: 9407 RVA: 0x000A521C File Offset: 0x000A341C
		// (set) Token: 0x060024C0 RID: 9408 RVA: 0x000A5224 File Offset: 0x000A3424
		[DataMember]
		[XmlElement(ElementName = "ClutterState")]
		public ClutterState ClutterState { get; set; }

		// Token: 0x060024C1 RID: 9409 RVA: 0x000A522D File Offset: 0x000A342D
		public GetClutterStateResponse()
		{
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000A5235 File Offset: 0x000A3435
		internal GetClutterStateResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000A523F File Offset: 0x000A343F
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetClutterStateResponseMessage;
		}
	}
}
