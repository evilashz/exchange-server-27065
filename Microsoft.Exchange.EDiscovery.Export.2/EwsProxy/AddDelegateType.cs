using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000360 RID: 864
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AddDelegateType : BaseDelegateType
	{
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00029934 File Offset: 0x00027B34
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0002993C File Offset: 0x00027B3C
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

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x00029945 File Offset: 0x00027B45
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x0002994D File Offset: 0x00027B4D
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

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00029956 File Offset: 0x00027B56
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x0002995E File Offset: 0x00027B5E
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

		// Token: 0x04001273 RID: 4723
		private DelegateUserType[] delegateUsersField;

		// Token: 0x04001274 RID: 4724
		private DeliverMeetingRequestsType deliverMeetingRequestsField;

		// Token: 0x04001275 RID: 4725
		private bool deliverMeetingRequestsFieldSpecified;
	}
}
