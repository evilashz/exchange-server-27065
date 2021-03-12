using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000204 RID: 516
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarFolderType : BaseFolderType
	{
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00025DC9 File Offset: 0x00023FC9
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00025DD1 File Offset: 0x00023FD1
		public CalendarPermissionReadAccessType SharingEffectiveRights
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

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00025DDA File Offset: 0x00023FDA
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x00025DE2 File Offset: 0x00023FE2
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

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00025DEB File Offset: 0x00023FEB
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x00025DF3 File Offset: 0x00023FF3
		public CalendarPermissionSetType PermissionSet
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

		// Token: 0x04000E2C RID: 3628
		private CalendarPermissionReadAccessType sharingEffectiveRightsField;

		// Token: 0x04000E2D RID: 3629
		private bool sharingEffectiveRightsFieldSpecified;

		// Token: 0x04000E2E RID: 3630
		private CalendarPermissionSetType permissionSetField;
	}
}
