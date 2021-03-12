using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200020F RID: 527
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ContactsFolderType : BaseFolderType
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000260D4 File Offset: 0x000242D4
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x000260DC File Offset: 0x000242DC
		public PermissionReadAccessType SharingEffectiveRights
		{
			get
			{
				return this.sharingEffectiveRightsField;
			}
			set
			{
				this.sharingEffectiveRightsField = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000260E5 File Offset: 0x000242E5
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x000260ED File Offset: 0x000242ED
		[XmlIgnore]
		public bool SharingEffectiveRightsSpecified
		{
			get
			{
				return this.sharingEffectiveRightsFieldSpecified;
			}
			set
			{
				this.sharingEffectiveRightsFieldSpecified = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000260F6 File Offset: 0x000242F6
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x000260FE File Offset: 0x000242FE
		public PermissionSetType PermissionSet
		{
			get
			{
				return this.permissionSetField;
			}
			set
			{
				this.permissionSetField = value;
			}
		}

		// Token: 0x04000E7B RID: 3707
		private PermissionReadAccessType sharingEffectiveRightsField;

		// Token: 0x04000E7C RID: 3708
		private bool sharingEffectiveRightsFieldSpecified;

		// Token: 0x04000E7D RID: 3709
		private PermissionSetType permissionSetField;
	}
}
