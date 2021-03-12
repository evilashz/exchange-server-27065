using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200085F RID: 2143
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SubscribedPlan : DirectoryObject
	{
		// Token: 0x06006B4D RID: 27469 RVA: 0x0017376C File Offset: 0x0017196C
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			processor.Process<DirectoryPropertyGuidSingle>(SyncSubscribedPlanSchema.AccountId, ref this.accountIdField);
			processor.Process<DirectoryPropertyXmlAnySingle>(SyncSubscribedPlanSchema.Capability, ref this.capabilityField);
			processor.Process<DirectoryPropertyStringSingleLength1To256>(SyncSubscribedPlanSchema.ServiceType, ref this.serviceTypeField);
			processor.Process<DirectoryPropertyXmlLicenseUnitsDetailSingle>(SyncSubscribedPlanSchema.MaximumOverageUnitsDetail, ref this.maximumOverageUnitsDetailField);
			processor.Process<DirectoryPropertyXmlLicenseUnitsDetailSingle>(SyncSubscribedPlanSchema.PrepaidUnitsDetail, ref this.prepaidUnitsDetailField);
			processor.Process<DirectoryPropertyXmlLicenseUnitsDetailSingle>(SyncSubscribedPlanSchema.TotalTrialUnitsDetail, ref this.totalTrialUnitsDetailField);
		}

		// Token: 0x06006B4E RID: 27470 RVA: 0x001737E0 File Offset: 0x001719E0
		public override string ToString()
		{
			return string.Format("accountIdField={0} capabilityField={1} serviceTypeField={2} maximumOverageUnitsDetailField={3} prepaidUnitsDetailField={4} totalTrialUnitsDetailField={5}", new object[]
			{
				this.accountIdField,
				this.capabilityField,
				this.serviceTypeField,
				this.maximumOverageUnitsDetailField,
				this.prepaidUnitsDetailField,
				this.totalTrialUnitsDetailField
			});
		}

		// Token: 0x1700262C RID: 9772
		// (get) Token: 0x06006B4F RID: 27471 RVA: 0x00173835 File Offset: 0x00171A35
		// (set) Token: 0x06006B50 RID: 27472 RVA: 0x0017383D File Offset: 0x00171A3D
		[XmlElement(Order = 0)]
		public DirectoryPropertyGuidSingle AccountId
		{
			get
			{
				return this.accountIdField;
			}
			set
			{
				this.accountIdField = value;
			}
		}

		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x06006B51 RID: 27473 RVA: 0x00173846 File Offset: 0x00171A46
		// (set) Token: 0x06006B52 RID: 27474 RVA: 0x0017384E File Offset: 0x00171A4E
		[XmlElement(Order = 1)]
		public DirectoryPropertyXmlAnySingle Capability
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

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x06006B53 RID: 27475 RVA: 0x00173857 File Offset: 0x00171A57
		// (set) Token: 0x06006B54 RID: 27476 RVA: 0x0017385F File Offset: 0x00171A5F
		[XmlElement(Order = 2)]
		public DirectoryPropertyInt32SingleMin0 MaximumOverageUnits
		{
			get
			{
				return this.maximumOverageUnitsField;
			}
			set
			{
				this.maximumOverageUnitsField = value;
			}
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x06006B55 RID: 27477 RVA: 0x00173868 File Offset: 0x00171A68
		// (set) Token: 0x06006B56 RID: 27478 RVA: 0x00173870 File Offset: 0x00171A70
		[XmlElement(Order = 3)]
		public DirectoryPropertyXmlLicenseUnitsDetailSingle MaximumOverageUnitsDetail
		{
			get
			{
				return this.maximumOverageUnitsDetailField;
			}
			set
			{
				this.maximumOverageUnitsDetailField = value;
			}
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x06006B57 RID: 27479 RVA: 0x00173879 File Offset: 0x00171A79
		// (set) Token: 0x06006B58 RID: 27480 RVA: 0x00173881 File Offset: 0x00171A81
		[XmlElement(Order = 4)]
		public DirectoryPropertyInt32SingleMin0 PrepaidUnits
		{
			get
			{
				return this.prepaidUnitsField;
			}
			set
			{
				this.prepaidUnitsField = value;
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x06006B59 RID: 27481 RVA: 0x0017388A File Offset: 0x00171A8A
		// (set) Token: 0x06006B5A RID: 27482 RVA: 0x00173892 File Offset: 0x00171A92
		[XmlElement(Order = 5)]
		public DirectoryPropertyXmlLicenseUnitsDetailSingle PrepaidUnitsDetail
		{
			get
			{
				return this.prepaidUnitsDetailField;
			}
			set
			{
				this.prepaidUnitsDetailField = value;
			}
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x06006B5B RID: 27483 RVA: 0x0017389B File Offset: 0x00171A9B
		// (set) Token: 0x06006B5C RID: 27484 RVA: 0x001738A3 File Offset: 0x00171AA3
		[XmlElement(Order = 6)]
		public DirectoryPropertyStringSingleLength1To256 ServiceType
		{
			get
			{
				return this.serviceTypeField;
			}
			set
			{
				this.serviceTypeField = value;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x06006B5D RID: 27485 RVA: 0x001738AC File Offset: 0x00171AAC
		// (set) Token: 0x06006B5E RID: 27486 RVA: 0x001738B4 File Offset: 0x00171AB4
		[XmlElement(Order = 7)]
		public DirectoryPropertyXmlLicenseUnitsDetailSingle TotalTrialUnitsDetail
		{
			get
			{
				return this.totalTrialUnitsDetailField;
			}
			set
			{
				this.totalTrialUnitsDetailField = value;
			}
		}

		// Token: 0x17002634 RID: 9780
		// (get) Token: 0x06006B5F RID: 27487 RVA: 0x001738BD File Offset: 0x00171ABD
		// (set) Token: 0x06006B60 RID: 27488 RVA: 0x001738C5 File Offset: 0x00171AC5
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x040045F1 RID: 17905
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x040045F2 RID: 17906
		private DirectoryPropertyXmlAnySingle capabilityField;

		// Token: 0x040045F3 RID: 17907
		private DirectoryPropertyInt32SingleMin0 maximumOverageUnitsField;

		// Token: 0x040045F4 RID: 17908
		private DirectoryPropertyXmlLicenseUnitsDetailSingle maximumOverageUnitsDetailField;

		// Token: 0x040045F5 RID: 17909
		private DirectoryPropertyInt32SingleMin0 prepaidUnitsField;

		// Token: 0x040045F6 RID: 17910
		private DirectoryPropertyXmlLicenseUnitsDetailSingle prepaidUnitsDetailField;

		// Token: 0x040045F7 RID: 17911
		private DirectoryPropertyStringSingleLength1To256 serviceTypeField;

		// Token: 0x040045F8 RID: 17912
		private DirectoryPropertyXmlLicenseUnitsDetailSingle totalTrialUnitsDetailField;

		// Token: 0x040045F9 RID: 17913
		private XmlAttribute[] anyAttrField;
	}
}
