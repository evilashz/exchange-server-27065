using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200035E RID: 862
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateDelegateType : BaseDelegateType
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000298E0 File Offset: 0x00027AE0
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000298E8 File Offset: 0x00027AE8
		[XmlArrayItem("DelegateUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DelegateUserType[] DelegateUsers
		{
			get
			{
				return this.delegateUsersField;
			}
			set
			{
				this.delegateUsersField = value;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000298F1 File Offset: 0x00027AF1
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x000298F9 File Offset: 0x00027AF9
		public DeliverMeetingRequestsType DeliverMeetingRequests
		{
			get
			{
				return this.deliverMeetingRequestsField;
			}
			set
			{
				this.deliverMeetingRequestsField = value;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00029902 File Offset: 0x00027B02
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0002990A File Offset: 0x00027B0A
		[XmlIgnore]
		public bool DeliverMeetingRequestsSpecified
		{
			get
			{
				return this.deliverMeetingRequestsFieldSpecified;
			}
			set
			{
				this.deliverMeetingRequestsFieldSpecified = value;
			}
		}

		// Token: 0x0400126F RID: 4719
		private DelegateUserType[] delegateUsersField;

		// Token: 0x04001270 RID: 4720
		private DeliverMeetingRequestsType deliverMeetingRequestsField;

		// Token: 0x04001271 RID: 4721
		private bool deliverMeetingRequestsFieldSpecified;
	}
}
