using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200018F RID: 399
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class SubscribedPlan : DirectoryObject
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00021886 File Offset: 0x0001FA86
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0002188E File Offset: 0x0001FA8E
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

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00021897 File Offset: 0x0001FA97
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0002189F File Offset: 0x0001FA9F
		public DirectoryPropertyStringLength1To256 AuthorizedServiceInstance
		{
			get
			{
				return this.authorizedServiceInstanceField;
			}
			set
			{
				this.authorizedServiceInstanceField = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000218A8 File Offset: 0x0001FAA8
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public DirectoryPropertyBooleanSingle BelongsToFirstLoginObjectSet
		{
			get
			{
				return this.belongsToFirstLoginObjectSetField;
			}
			set
			{
				this.belongsToFirstLoginObjectSetField = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000218B9 File Offset: 0x0001FAB9
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x000218C1 File Offset: 0x0001FAC1
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x000218CA File Offset: 0x0001FACA
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x000218D2 File Offset: 0x0001FAD2
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

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000218DB File Offset: 0x0001FADB
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000218E3 File Offset: 0x0001FAE3
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

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000218EC File Offset: 0x0001FAEC
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000218F4 File Offset: 0x0001FAF4
		public DirectoryPropertyGuidSingle PlanId
		{
			get
			{
				return this.planIdField;
			}
			set
			{
				this.planIdField = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000218FD File Offset: 0x0001FAFD
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x00021905 File Offset: 0x0001FB05
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002190E File Offset: 0x0001FB0E
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x00021916 File Offset: 0x0001FB16
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002191F File Offset: 0x0001FB1F
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x00021927 File Offset: 0x0001FB27
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00021930 File Offset: 0x0001FB30
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x00021938 File Offset: 0x0001FB38
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00021941 File Offset: 0x0001FB41
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00021949 File Offset: 0x0001FB49
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

		// Token: 0x04000556 RID: 1366
		private DirectoryPropertyGuidSingle accountIdField;

		// Token: 0x04000557 RID: 1367
		private DirectoryPropertyStringLength1To256 authorizedServiceInstanceField;

		// Token: 0x04000558 RID: 1368
		private DirectoryPropertyBooleanSingle belongsToFirstLoginObjectSetField;

		// Token: 0x04000559 RID: 1369
		private DirectoryPropertyXmlAnySingle capabilityField;

		// Token: 0x0400055A RID: 1370
		private DirectoryPropertyInt32SingleMin0 maximumOverageUnitsField;

		// Token: 0x0400055B RID: 1371
		private DirectoryPropertyXmlLicenseUnitsDetailSingle maximumOverageUnitsDetailField;

		// Token: 0x0400055C RID: 1372
		private DirectoryPropertyGuidSingle planIdField;

		// Token: 0x0400055D RID: 1373
		private DirectoryPropertyInt32SingleMin0 prepaidUnitsField;

		// Token: 0x0400055E RID: 1374
		private DirectoryPropertyXmlLicenseUnitsDetailSingle prepaidUnitsDetailField;

		// Token: 0x0400055F RID: 1375
		private DirectoryPropertyStringSingleLength1To256 serviceTypeField;

		// Token: 0x04000560 RID: 1376
		private DirectoryPropertyXmlLicenseUnitsDetailSingle totalTrialUnitsDetailField;

		// Token: 0x04000561 RID: 1377
		private XmlAttribute[] anyAttrField;
	}
}
