using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004A7 RID: 1191
	[XmlType("AddDistributionGroupToImListResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddDistributionGroupToImListResponseMessage : ResponseMessage
	{
		// Token: 0x060023A7 RID: 9127 RVA: 0x000A41CF File Offset: 0x000A23CF
		public AddDistributionGroupToImListResponseMessage()
		{
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x000A41D7 File Offset: 0x000A23D7
		internal AddDistributionGroupToImListResponseMessage(ServiceResultCode code, ServiceError error, ImGroup result) : base(code, error)
		{
			this.ImGroup = result;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x000A41E8 File Offset: 0x000A23E8
		public override ResponseType GetResponseType()
		{
			return ResponseType.AddDistributionGroupToImListResponseMessage;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000A41EC File Offset: 0x000A23EC
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x000A41F4 File Offset: 0x000A23F4
		[DataMember]
		[XmlElement]
		public ImGroup ImGroup { get; set; }
	}
}
