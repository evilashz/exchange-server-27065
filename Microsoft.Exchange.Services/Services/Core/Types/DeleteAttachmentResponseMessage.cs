using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C7 RID: 1223
	[XmlType("DeleteAttachmentResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteAttachmentResponseMessage : ResponseMessage
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x000A472C File Offset: 0x000A292C
		public DeleteAttachmentResponseMessage()
		{
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000A4734 File Offset: 0x000A2934
		internal DeleteAttachmentResponseMessage(ServiceResultCode code, ServiceError error, RootItemIdType rootItemId) : base(code, error)
		{
			this.RootItemId = rootItemId;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000A4745 File Offset: 0x000A2945
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x000A474D File Offset: 0x000A294D
		[DataMember]
		[XmlElement("RootItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public RootItemIdType RootItemId { get; set; }
	}
}
