using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E1 RID: 225
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class AssignedPlanFilterValue
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001F7BF File Offset: 0x0001D9BF
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001F7C7 File Offset: 0x0001D9C7
		[XmlAttribute]
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstanceField;
			}
			set
			{
				this.serviceInstanceField = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001F7D0 File Offset: 0x0001D9D0
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		[XmlAttribute]
		public string SubscribedPlanId
		{
			get
			{
				return this.subscribedPlanIdField;
			}
			set
			{
				this.subscribedPlanIdField = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001F7E1 File Offset: 0x0001D9E1
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001F7E9 File Offset: 0x0001D9E9
		[XmlAttribute]
		public AssignedCapabilityStatus CapabilityStatus
		{
			get
			{
				return this.capabilityStatusField;
			}
			set
			{
				this.capabilityStatusField = value;
			}
		}

		// Token: 0x04000384 RID: 900
		private string serviceInstanceField;

		// Token: 0x04000385 RID: 901
		private string subscribedPlanIdField;

		// Token: 0x04000386 RID: 902
		private AssignedCapabilityStatus capabilityStatusField;
	}
}
