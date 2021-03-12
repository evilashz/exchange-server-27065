using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200019A RID: 410
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class Contract : DirectoryObject
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000222E5 File Offset: 0x000204E5
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x000222ED File Offset: 0x000204ED
		public DirectoryPropertyGuidSingle ContractId
		{
			get
			{
				return this.contractIdField;
			}
			set
			{
				this.contractIdField = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x000222F6 File Offset: 0x000204F6
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x000222FE File Offset: 0x000204FE
		public DirectoryPropertyInt32SingleMin0 ContractType
		{
			get
			{
				return this.contractTypeField;
			}
			set
			{
				this.contractTypeField = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00022307 File Offset: 0x00020507
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0002230F File Offset: 0x0002050F
		public DirectoryPropertyStringSingleLength1To128 DefaultGeography
		{
			get
			{
				return this.defaultGeographyField;
			}
			set
			{
				this.defaultGeographyField = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00022318 File Offset: 0x00020518
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00022320 File Offset: 0x00020520
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00022329 File Offset: 0x00020529
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00022331 File Offset: 0x00020531
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002233A File Offset: 0x0002053A
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00022342 File Offset: 0x00020542
		public DirectoryPropertyXmlSupportRole SupportRole
		{
			get
			{
				return this.supportRoleField;
			}
			set
			{
				this.supportRoleField = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002234B File Offset: 0x0002054B
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00022353 File Offset: 0x00020553
		public DirectoryPropertyGuidSingle TargetContextId
		{
			get
			{
				return this.targetContextIdField;
			}
			set
			{
				this.targetContextIdField = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002235C File Offset: 0x0002055C
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00022364 File Offset: 0x00020564
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

		// Token: 0x040005ED RID: 1517
		private DirectoryPropertyGuidSingle contractIdField;

		// Token: 0x040005EE RID: 1518
		private DirectoryPropertyInt32SingleMin0 contractTypeField;

		// Token: 0x040005EF RID: 1519
		private DirectoryPropertyStringSingleLength1To128 defaultGeographyField;

		// Token: 0x040005F0 RID: 1520
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040005F1 RID: 1521
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040005F2 RID: 1522
		private DirectoryPropertyXmlSupportRole supportRoleField;

		// Token: 0x040005F3 RID: 1523
		private DirectoryPropertyGuidSingle targetContextIdField;

		// Token: 0x040005F4 RID: 1524
		private XmlAttribute[] anyAttrField;
	}
}
