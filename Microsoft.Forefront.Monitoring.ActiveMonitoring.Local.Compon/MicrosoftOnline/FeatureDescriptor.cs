using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000184 RID: 388
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class FeatureDescriptor : DirectoryObject
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000208F9 File Offset: 0x0001EAF9
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x00020901 File Offset: 0x0001EB01
		public DirectoryPropertyStringSingleLength1To1024 Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0002090A File Offset: 0x0001EB0A
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00020912 File Offset: 0x0001EB12
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0002091B File Offset: 0x0001EB1B
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00020923 File Offset: 0x0001EB23
		public DirectoryPropertyGuidSingle FeatureDescriptorId
		{
			get
			{
				return this.featureDescriptorIdField;
			}
			set
			{
				this.featureDescriptorIdField = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002092C File Offset: 0x0001EB2C
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00020934 File Offset: 0x0001EB34
		public DirectoryPropertyInt32SingleMin0 FeatureStatus
		{
			get
			{
				return this.featureStatusField;
			}
			set
			{
				this.featureStatusField = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0002093D File Offset: 0x0001EB3D
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00020945 File Offset: 0x0001EB45
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

		// Token: 0x04000471 RID: 1137
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000472 RID: 1138
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000473 RID: 1139
		private DirectoryPropertyGuidSingle featureDescriptorIdField;

		// Token: 0x04000474 RID: 1140
		private DirectoryPropertyInt32SingleMin0 featureStatusField;

		// Token: 0x04000475 RID: 1141
		private XmlAttribute[] anyAttrField;
	}
}
