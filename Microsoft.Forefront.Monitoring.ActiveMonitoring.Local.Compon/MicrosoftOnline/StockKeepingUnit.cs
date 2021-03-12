using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000190 RID: 400
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class StockKeepingUnit : DirectoryObject
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0002195A File Offset: 0x0001FB5A
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00021962 File Offset: 0x0001FB62
		public DirectoryPropertyStringSingleLength1To256 PartNumber
		{
			get
			{
				return this.partNumberField;
			}
			set
			{
				this.partNumberField = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0002196B File Offset: 0x0001FB6B
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x00021973 File Offset: 0x0001FB73
		public DirectoryPropertyReferenceServicePlan ServicePlanGranted
		{
			get
			{
				return this.servicePlanGrantedField;
			}
			set
			{
				this.servicePlanGrantedField = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0002197C File Offset: 0x0001FB7C
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00021984 File Offset: 0x0001FB84
		public DirectoryPropertyGuidSingle SkuId
		{
			get
			{
				return this.skuIdField;
			}
			set
			{
				this.skuIdField = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002198D File Offset: 0x0001FB8D
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00021995 File Offset: 0x0001FB95
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

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002199E File Offset: 0x0001FB9E
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000219A6 File Offset: 0x0001FBA6
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

		// Token: 0x04000562 RID: 1378
		private DirectoryPropertyStringSingleLength1To256 partNumberField;

		// Token: 0x04000563 RID: 1379
		private DirectoryPropertyReferenceServicePlan servicePlanGrantedField;

		// Token: 0x04000564 RID: 1380
		private DirectoryPropertyGuidSingle skuIdField;

		// Token: 0x04000565 RID: 1381
		private DirectoryPropertyInt32SingleMin0 targetClassField;

		// Token: 0x04000566 RID: 1382
		private XmlAttribute[] anyAttrField;
	}
}
