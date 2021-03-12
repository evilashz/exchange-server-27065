using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000191 RID: 401
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class ServicePlan : DirectoryObject
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x000219B7 File Offset: 0x0001FBB7
		// (set) Token: 0x06000AE0 RID: 2784 RVA: 0x000219BF File Offset: 0x0001FBBF
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x000219C8 File Offset: 0x0001FBC8
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x000219D0 File Offset: 0x0001FBD0
		public DirectoryPropertyReferenceServicePlan DependsOnServicePlan
		{
			get
			{
				return this.dependsOnServicePlanField;
			}
			set
			{
				this.dependsOnServicePlanField = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000219D9 File Offset: 0x0001FBD9
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x000219E1 File Offset: 0x0001FBE1
		public DirectoryPropertyStringSingleLength1To64 LicenseType
		{
			get
			{
				return this.licenseTypeField;
			}
			set
			{
				this.licenseTypeField = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000219EA File Offset: 0x0001FBEA
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x000219F2 File Offset: 0x0001FBF2
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000219FB File Offset: 0x0001FBFB
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00021A03 File Offset: 0x0001FC03
		public DirectoryPropertyStringLength1To3 ProhibitedUsageLocations
		{
			get
			{
				return this.prohibitedUsageLocationsField;
			}
			set
			{
				this.prohibitedUsageLocationsField = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00021A0C File Offset: 0x0001FC0C
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00021A14 File Offset: 0x0001FC14
		public DirectoryPropertyStringLength1To3 RestrictedUsageLocations
		{
			get
			{
				return this.restrictedUsageLocationsField;
			}
			set
			{
				this.restrictedUsageLocationsField = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00021A1D File Offset: 0x0001FC1D
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00021A25 File Offset: 0x0001FC25
		public DirectoryPropertyStringSingleLength1To256 ServicePlanName
		{
			get
			{
				return this.servicePlanNameField;
			}
			set
			{
				this.servicePlanNameField = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00021A2E File Offset: 0x0001FC2E
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00021A36 File Offset: 0x0001FC36
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00021A3F File Offset: 0x0001FC3F
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x00021A47 File Offset: 0x0001FC47
		public DirectoryPropertyReferenceServicePlan SubsetOf
		{
			get
			{
				return this.subsetOfField;
			}
			set
			{
				this.subsetOfField = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00021A50 File Offset: 0x0001FC50
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x00021A58 File Offset: 0x0001FC58
		public DirectoryPropertyInt32SingleMin0 TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00021A61 File Offset: 0x0001FC61
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00021A69 File Offset: 0x0001FC69
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

		// Token: 0x04000567 RID: 1383
		private DirectoryPropertyXmlAnySingle capabilityField;

		// Token: 0x04000568 RID: 1384
		private DirectoryPropertyReferenceServicePlan dependsOnServicePlanField;

		// Token: 0x04000569 RID: 1385
		private DirectoryPropertyStringSingleLength1To64 licenseTypeField;

		// Token: 0x0400056A RID: 1386
		private DirectoryPropertyGuidSingle planIdField;

		// Token: 0x0400056B RID: 1387
		private DirectoryPropertyStringLength1To3 prohibitedUsageLocationsField;

		// Token: 0x0400056C RID: 1388
		private DirectoryPropertyStringLength1To3 restrictedUsageLocationsField;

		// Token: 0x0400056D RID: 1389
		private DirectoryPropertyStringSingleLength1To256 servicePlanNameField;

		// Token: 0x0400056E RID: 1390
		private DirectoryPropertyStringSingleLength1To256 serviceTypeField;

		// Token: 0x0400056F RID: 1391
		private DirectoryPropertyReferenceServicePlan subsetOfField;

		// Token: 0x04000570 RID: 1392
		private DirectoryPropertyInt32SingleMin0 targetClassField;

		// Token: 0x04000571 RID: 1393
		private XmlAttribute[] anyAttrField;
	}
}
