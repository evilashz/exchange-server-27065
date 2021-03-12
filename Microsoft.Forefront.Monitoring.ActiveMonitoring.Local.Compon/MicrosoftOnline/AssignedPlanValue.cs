using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A3 RID: 163
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AssignedPlanValue
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001EDC7 File Offset: 0x0001CFC7
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001EDCF File Offset: 0x0001CFCF
		public XmlElement InitialState
		{
			get
			{
				return this.initialStateField;
			}
			set
			{
				this.initialStateField = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public XmlElement Capability
		{
			get
			{
				return this.capabilityField;
			}
			set
			{
				this.capabilityField = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001EDE9 File Offset: 0x0001CFE9
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0001EDF1 File Offset: 0x0001CFF1
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001EDFA File Offset: 0x0001CFFA
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001EE02 File Offset: 0x0001D002
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001EE0B File Offset: 0x0001D00B
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0001EE13 File Offset: 0x0001D013
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001EE1C File Offset: 0x0001D01C
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0001EE24 File Offset: 0x0001D024
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001EE2D File Offset: 0x0001D02D
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001EE35 File Offset: 0x0001D035
		[XmlAttribute]
		public string ServicePlanId
		{
			get
			{
				return this.servicePlanIdField;
			}
			set
			{
				this.servicePlanIdField = value;
			}
		}

		// Token: 0x040002EE RID: 750
		private XmlElement initialStateField;

		// Token: 0x040002EF RID: 751
		private XmlElement capabilityField;

		// Token: 0x040002F0 RID: 752
		private string subscribedPlanIdField;

		// Token: 0x040002F1 RID: 753
		private string serviceInstanceField;

		// Token: 0x040002F2 RID: 754
		private AssignedCapabilityStatus capabilityStatusField;

		// Token: 0x040002F3 RID: 755
		private DateTime assignedTimestampField;

		// Token: 0x040002F4 RID: 756
		private string servicePlanIdField;
	}
}
