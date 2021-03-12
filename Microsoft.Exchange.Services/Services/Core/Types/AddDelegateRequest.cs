using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F1 RID: 1009
	[XmlType("AddDelegateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddDelegateRequest : BaseDelegateRequest
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0009E29E File Offset: 0x0009C49E
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0009E2A6 File Offset: 0x0009C4A6
		[XmlArrayItem("DelegateUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DelegateUserType[] DelegateUsers
		{
			get
			{
				return this.delegateUsers;
			}
			set
			{
				this.delegateUsers = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x0009E2AF File Offset: 0x0009C4AF
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x0009E2B7 File Offset: 0x0009C4B7
		[XmlElement("DeliverMeetingRequests")]
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

		// Token: 0x06001C53 RID: 7251 RVA: 0x0009E2C0 File Offset: 0x0009C4C0
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddDelegate(callContext, this);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0009E2C9 File Offset: 0x0009C4C9
		public AddDelegateRequest() : base(true)
		{
		}

		// Token: 0x040012BB RID: 4795
		private DelegateUserType[] delegateUsers;

		// Token: 0x040012BC RID: 4796
		private DeliverMeetingRequestsType deliverMeetingRequest;
	}
}
