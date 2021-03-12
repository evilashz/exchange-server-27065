using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000194 RID: 404
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class Scope : DirectoryObject
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00021BAB File Offset: 0x0001FDAB
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00021BB3 File Offset: 0x0001FDB3
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

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00021BBC File Offset: 0x0001FDBC
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00021BC4 File Offset: 0x0001FDC4
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

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00021BCD File Offset: 0x0001FDCD
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00021BD5 File Offset: 0x0001FDD5
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

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00021BDE File Offset: 0x0001FDDE
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00021BE6 File Offset: 0x0001FDE6
		public DirectoryPropertyInt32SingleMin0 ScopeFilterOn
		{
			get
			{
				return this.scopeFilterOnField;
			}
			set
			{
				this.scopeFilterOnField = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00021BEF File Offset: 0x0001FDEF
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00021BF7 File Offset: 0x0001FDF7
		public DirectoryPropertyStringSingleLength1To1024 ScopeFilterValue
		{
			get
			{
				return this.scopeFilterValueField;
			}
			set
			{
				this.scopeFilterValueField = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00021C00 File Offset: 0x0001FE00
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00021C08 File Offset: 0x0001FE08
		public DirectoryPropertyGuidSingle ScopeId
		{
			get
			{
				return this.scopeIdField;
			}
			set
			{
				this.scopeIdField = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00021C11 File Offset: 0x0001FE11
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00021C19 File Offset: 0x0001FE19
		public DirectoryPropertyInt32SingleMin0 ScopeParameterType
		{
			get
			{
				return this.scopeParameterTypeField;
			}
			set
			{
				this.scopeParameterTypeField = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00021C22 File Offset: 0x0001FE22
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00021C2A File Offset: 0x0001FE2A
		public DirectoryPropertyInt64Single ScopeTargetTypes
		{
			get
			{
				return this.scopeTargetTypesField;
			}
			set
			{
				this.scopeTargetTypesField = value;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00021C33 File Offset: 0x0001FE33
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00021C3B File Offset: 0x0001FE3B
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

		// Token: 0x04000583 RID: 1411
		private DirectoryPropertyBooleanSingle builtinField;

		// Token: 0x04000584 RID: 1412
		private DirectoryPropertyStringSingleLength1To1024 descriptionField;

		// Token: 0x04000585 RID: 1413
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x04000586 RID: 1414
		private DirectoryPropertyInt32SingleMin0 scopeFilterOnField;

		// Token: 0x04000587 RID: 1415
		private DirectoryPropertyStringSingleLength1To1024 scopeFilterValueField;

		// Token: 0x04000588 RID: 1416
		private DirectoryPropertyGuidSingle scopeIdField;

		// Token: 0x04000589 RID: 1417
		private DirectoryPropertyInt32SingleMin0 scopeParameterTypeField;

		// Token: 0x0400058A RID: 1418
		private DirectoryPropertyInt64Single scopeTargetTypesField;

		// Token: 0x0400058B RID: 1419
		private XmlAttribute[] anyAttrField;
	}
}
