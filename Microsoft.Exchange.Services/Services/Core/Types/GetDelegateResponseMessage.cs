using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F5 RID: 1269
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetDelegateResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetDelegateResponseMessage : DelegateResponseMessage
	{
		// Token: 0x060024D7 RID: 9431 RVA: 0x000A535E File Offset: 0x000A355E
		public GetDelegateResponseMessage()
		{
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000A5366 File Offset: 0x000A3566
		internal GetDelegateResponseMessage(ServiceResultCode code, ServiceError error, DelegateUserResponseMessageType[] delegateUsers, DeliverMeetingRequestsType deliverMeetingRequest) : base(code, error, delegateUsers, ResponseType.GetDelegateResponseMessage)
		{
			this.deliverMeetingRequest = deliverMeetingRequest;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000A537B File Offset: 0x000A357B
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x000A5383 File Offset: 0x000A3583
		[XmlElement("DeliverMeetingRequests")]
		[DefaultValue(DeliverMeetingRequestsType.None)]
		public DeliverMeetingRequestsType DeliverMeetingRequests
		{
			get
			{
				return this.deliverMeetingRequest;
			}
			set
			{
				this.deliverMeetingRequest = value;
			}
		}

		// Token: 0x04001599 RID: 5529
		private DeliverMeetingRequestsType deliverMeetingRequest;
	}
}
