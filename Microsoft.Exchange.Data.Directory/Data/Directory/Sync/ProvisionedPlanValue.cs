using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200093F RID: 2367
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class ProvisionedPlanValue
	{
		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x06006FF0 RID: 28656 RVA: 0x00177018 File Offset: 0x00175218
		// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x00177020 File Offset: 0x00175220
		[XmlElement(Order = 0)]
		public XmlElement ErrorDetail
		{
			get
			{
				return this.errorDetailField;
			}
			set
			{
				this.errorDetailField = value;
			}
		}

		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x06006FF2 RID: 28658 RVA: 0x00177029 File Offset: 0x00175229
		// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x00177031 File Offset: 0x00175231
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

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06006FF4 RID: 28660 RVA: 0x0017703A File Offset: 0x0017523A
		// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x00177042 File Offset: 0x00175242
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

		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x06006FF6 RID: 28662 RVA: 0x0017704B File Offset: 0x0017524B
		// (set) Token: 0x06006FF7 RID: 28663 RVA: 0x00177053 File Offset: 0x00175253
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

		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06006FF8 RID: 28664 RVA: 0x0017705C File Offset: 0x0017525C
		// (set) Token: 0x06006FF9 RID: 28665 RVA: 0x00177064 File Offset: 0x00175264
		[XmlAttribute]
		public DateTime AssignedTimestamp
		{
			get
			{
				return this.assignedTimestampField;
			}
			set
			{
				this.assignedTimestampField = value;
			}
		}

		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x06006FFA RID: 28666 RVA: 0x0017706D File Offset: 0x0017526D
		// (set) Token: 0x06006FFB RID: 28667 RVA: 0x00177075 File Offset: 0x00175275
		[XmlAttribute]
		public ProvisioningStatus ProvisioningStatus
		{
			get
			{
				return this.provisioningStatusField;
			}
			set
			{
				this.provisioningStatusField = value;
			}
		}

		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x06006FFC RID: 28668 RVA: 0x0017707E File Offset: 0x0017527E
		// (set) Token: 0x06006FFD RID: 28669 RVA: 0x00177086 File Offset: 0x00175286
		[XmlAttribute]
		public DateTime ProvisionedTimestamp
		{
			get
			{
				return this.provisionedTimestampField;
			}
			set
			{
				this.provisionedTimestampField = value;
			}
		}

		// Token: 0x0400489F RID: 18591
		private XmlElement errorDetailField;

		// Token: 0x040048A0 RID: 18592
		private string subscribedPlanIdField;

		// Token: 0x040048A1 RID: 18593
		private string serviceInstanceField;

		// Token: 0x040048A2 RID: 18594
		private AssignedCapabilityStatus capabilityStatusField;

		// Token: 0x040048A3 RID: 18595
		private DateTime assignedTimestampField;

		// Token: 0x040048A4 RID: 18596
		private ProvisioningStatus provisioningStatusField;

		// Token: 0x040048A5 RID: 18597
		private DateTime provisionedTimestampField;
	}
}
