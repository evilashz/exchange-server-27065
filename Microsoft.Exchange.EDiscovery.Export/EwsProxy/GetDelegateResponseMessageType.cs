using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F2 RID: 498
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetDelegateResponseMessageType : BaseDelegateResponseMessageType
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00025A87 File Offset: 0x00023C87
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x00025A8F File Offset: 0x00023C8F
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

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00025A98 File Offset: 0x00023C98
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00025AA0 File Offset: 0x00023CA0
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

		// Token: 0x04000DF1 RID: 3569
		private DeliverMeetingRequestsType deliverMeetingRequestsField;

		// Token: 0x04000DF2 RID: 3570
		private bool deliverMeetingRequestsFieldSpecified;
	}
}
