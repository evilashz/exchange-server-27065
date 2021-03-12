using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000494 RID: 1172
	[XmlType("UpdateDelegateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateDelegateRequest : BaseDelegateRequest
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x000A365B File Offset: 0x000A185B
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x000A3663 File Offset: 0x000A1863
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

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x000A366C File Offset: 0x000A186C
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x000A3674 File Offset: 0x000A1874
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

		// Token: 0x060022F0 RID: 8944 RVA: 0x000A367D File Offset: 0x000A187D
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateDelegate(callContext, this);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000A3686 File Offset: 0x000A1886
		public UpdateDelegateRequest() : base(true)
		{
		}

		// Token: 0x0400151C RID: 5404
		private DelegateUserType[] delegateUsers;

		// Token: 0x0400151D RID: 5405
		private DeliverMeetingRequestsType deliverMeetingRequest;
	}
}
