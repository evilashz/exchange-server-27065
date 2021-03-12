using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000E7 RID: 231
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class ProvisionedPlanValue
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x0001FADC File Offset: 0x0001DCDC
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001FAE5 File Offset: 0x0001DCE5
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x0001FAED File Offset: 0x0001DCED
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001FAF6 File Offset: 0x0001DCF6
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0001FAFE File Offset: 0x0001DCFE
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001FB07 File Offset: 0x0001DD07
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0001FB0F File Offset: 0x0001DD0F
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0001FB18 File Offset: 0x0001DD18
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x0001FB20 File Offset: 0x0001DD20
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001FB29 File Offset: 0x0001DD29
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0001FB31 File Offset: 0x0001DD31
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001FB3A File Offset: 0x0001DD3A
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001FB42 File Offset: 0x0001DD42
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001FB4B File Offset: 0x0001DD4B
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001FB53 File Offset: 0x0001DD53
		[XmlAttribute]
		public DateTime ReceivedTimestamp
		{
			get
			{
				return this.receivedTimestampField;
			}
			set
			{
				this.receivedTimestampField = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001FB5C File Offset: 0x0001DD5C
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0001FB64 File Offset: 0x0001DD64
		[XmlIgnore]
		public bool ReceivedTimestampSpecified
		{
			get
			{
				return this.receivedTimestampFieldSpecified;
			}
			set
			{
				this.receivedTimestampFieldSpecified = value;
			}
		}

		// Token: 0x040003B2 RID: 946
		private XmlElement errorDetailField;

		// Token: 0x040003B3 RID: 947
		private string subscribedPlanIdField;

		// Token: 0x040003B4 RID: 948
		private string serviceInstanceField;

		// Token: 0x040003B5 RID: 949
		private AssignedCapabilityStatus capabilityStatusField;

		// Token: 0x040003B6 RID: 950
		private DateTime assignedTimestampField;

		// Token: 0x040003B7 RID: 951
		private ProvisioningStatus provisioningStatusField;

		// Token: 0x040003B8 RID: 952
		private DateTime provisionedTimestampField;

		// Token: 0x040003B9 RID: 953
		private DateTime receivedTimestampField;

		// Token: 0x040003BA RID: 954
		private bool receivedTimestampFieldSpecified;
	}
}
