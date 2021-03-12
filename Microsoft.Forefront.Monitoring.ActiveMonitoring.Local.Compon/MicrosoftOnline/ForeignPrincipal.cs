using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000199 RID: 409
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ForeignPrincipal : DirectoryObject
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00022277 File Offset: 0x00020477
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0002227F File Offset: 0x0002047F
		public DirectoryPropertyBooleanSingle Builtin
		{
			get
			{
				return this.builtinField;
			}
			set
			{
				this.builtinField = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00022288 File Offset: 0x00020488
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00022290 File Offset: 0x00020490
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00022299 File Offset: 0x00020499
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x000222A1 File Offset: 0x000204A1
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

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x000222AA File Offset: 0x000204AA
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x000222B2 File Offset: 0x000204B2
		public DirectoryPropertyGuidSingle ForeignContextId
		{
			get
			{
				return this.foreignContextIdField;
			}
			set
			{
				this.foreignContextIdField = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x000222BB File Offset: 0x000204BB
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x000222C3 File Offset: 0x000204C3
		public DirectoryPropertyGuidSingle ForeignPrincipalId
		{
			get
			{
				return this.foreignPrincipalIdField;
			}
			set
			{
				this.foreignPrincipalIdField = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x000222CC File Offset: 0x000204CC
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x000222D4 File Offset: 0x000204D4
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

		// Token: 0x040005E7 RID: 1511
		private DirectoryPropertyBooleanSingle builtinField;

		// Token: 0x040005E8 RID: 1512
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x040005E9 RID: 1513
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040005EA RID: 1514
		private DirectoryPropertyGuidSingle foreignContextIdField;

		// Token: 0x040005EB RID: 1515
		private DirectoryPropertyGuidSingle foreignPrincipalIdField;

		// Token: 0x040005EC RID: 1516
		private XmlAttribute[] anyAttrField;
	}
}
