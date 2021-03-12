using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F5 RID: 245
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PersonaAttributionType
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00020F10 File Offset: 0x0001F110
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x00020F18 File Offset: 0x0001F118
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00020F21 File Offset: 0x0001F121
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00020F29 File Offset: 0x0001F129
		public ItemIdType SourceId
		{
			get
			{
				return this.sourceIdField;
			}
			set
			{
				this.sourceIdField = value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00020F32 File Offset: 0x0001F132
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00020F3A File Offset: 0x0001F13A
		public string DisplayName
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00020F43 File Offset: 0x0001F143
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00020F4B File Offset: 0x0001F14B
		public bool IsWritable
		{
			get
			{
				return this.isWritableField;
			}
			set
			{
				this.isWritableField = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00020F54 File Offset: 0x0001F154
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00020F5C File Offset: 0x0001F15C
		[XmlIgnore]
		public bool IsWritableSpecified
		{
			get
			{
				return this.isWritableFieldSpecified;
			}
			set
			{
				this.isWritableFieldSpecified = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00020F65 File Offset: 0x0001F165
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00020F6D File Offset: 0x0001F16D
		public bool IsQuickContact
		{
			get
			{
				return this.isQuickContactField;
			}
			set
			{
				this.isQuickContactField = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00020F76 File Offset: 0x0001F176
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00020F7E File Offset: 0x0001F17E
		[XmlIgnore]
		public bool IsQuickContactSpecified
		{
			get
			{
				return this.isQuickContactFieldSpecified;
			}
			set
			{
				this.isQuickContactFieldSpecified = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00020F87 File Offset: 0x0001F187
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00020F8F File Offset: 0x0001F18F
		public bool IsHidden
		{
			get
			{
				return this.isHiddenField;
			}
			set
			{
				this.isHiddenField = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00020F98 File Offset: 0x0001F198
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x00020FA0 File Offset: 0x0001F1A0
		[XmlIgnore]
		public bool IsHiddenSpecified
		{
			get
			{
				return this.isHiddenFieldSpecified;
			}
			set
			{
				this.isHiddenFieldSpecified = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00020FA9 File Offset: 0x0001F1A9
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x00020FB1 File Offset: 0x0001F1B1
		public FolderIdType FolderId
		{
			get
			{
				return this.folderIdField;
			}
			set
			{
				this.folderIdField = value;
			}
		}

		// Token: 0x04000830 RID: 2096
		private string idField;

		// Token: 0x04000831 RID: 2097
		private ItemIdType sourceIdField;

		// Token: 0x04000832 RID: 2098
		private string displayNameField;

		// Token: 0x04000833 RID: 2099
		private bool isWritableField;

		// Token: 0x04000834 RID: 2100
		private bool isWritableFieldSpecified;

		// Token: 0x04000835 RID: 2101
		private bool isQuickContactField;

		// Token: 0x04000836 RID: 2102
		private bool isQuickContactFieldSpecified;

		// Token: 0x04000837 RID: 2103
		private bool isHiddenField;

		// Token: 0x04000838 RID: 2104
		private bool isHiddenFieldSpecified;

		// Token: 0x04000839 RID: 2105
		private FolderIdType folderIdField;
	}
}
