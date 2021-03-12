using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000813 RID: 2067
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class AssignedPlanValue
	{
		// Token: 0x06006628 RID: 26152 RVA: 0x001693CC File Offset: 0x001675CC
		public override string ToString()
		{
			return string.Format("subscribedPlanIdField={0} serviceInstanceField={1} assignedTimestampField={2} capabilityStatusField={3} servicePlanIdField={4} Capability={5}", new object[]
			{
				this.subscribedPlanIdField,
				this.serviceInstanceField,
				this.assignedTimestampField,
				this.capabilityStatusField,
				this.servicePlanIdField,
				this.Capability.OuterXml
			});
		}

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x06006629 RID: 26153 RVA: 0x00169430 File Offset: 0x00167630
		// (set) Token: 0x0600662A RID: 26154 RVA: 0x00169438 File Offset: 0x00167638
		[XmlElement(Order = 0)]
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

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x0600662B RID: 26155 RVA: 0x00169441 File Offset: 0x00167641
		// (set) Token: 0x0600662C RID: 26156 RVA: 0x00169449 File Offset: 0x00167649
		[XmlElement(Order = 1)]
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

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x0600662D RID: 26157 RVA: 0x00169452 File Offset: 0x00167652
		// (set) Token: 0x0600662E RID: 26158 RVA: 0x0016945A File Offset: 0x0016765A
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

		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x0600662F RID: 26159 RVA: 0x00169463 File Offset: 0x00167663
		// (set) Token: 0x06006630 RID: 26160 RVA: 0x0016946B File Offset: 0x0016766B
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

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x06006631 RID: 26161 RVA: 0x00169474 File Offset: 0x00167674
		// (set) Token: 0x06006632 RID: 26162 RVA: 0x0016947C File Offset: 0x0016767C
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

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x06006633 RID: 26163 RVA: 0x00169485 File Offset: 0x00167685
		// (set) Token: 0x06006634 RID: 26164 RVA: 0x0016948D File Offset: 0x0016768D
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

		// Token: 0x1700240D RID: 9229
		// (get) Token: 0x06006635 RID: 26165 RVA: 0x00169496 File Offset: 0x00167696
		// (set) Token: 0x06006636 RID: 26166 RVA: 0x0016949E File Offset: 0x0016769E
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

		// Token: 0x0400438F RID: 17295
		private XmlElement initialStateField;

		// Token: 0x04004390 RID: 17296
		private XmlElement capabilityField;

		// Token: 0x04004391 RID: 17297
		private string subscribedPlanIdField;

		// Token: 0x04004392 RID: 17298
		private string serviceInstanceField;

		// Token: 0x04004393 RID: 17299
		private AssignedCapabilityStatus capabilityStatusField;

		// Token: 0x04004394 RID: 17300
		private DateTime assignedTimestampField;

		// Token: 0x04004395 RID: 17301
		private string servicePlanIdField;
	}
}
