using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000083 RID: 131
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class DocumentSharingLocation
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001F5AE File Offset: 0x0001D7AE
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0001F5B6 File Offset: 0x0001D7B6
		public string ServiceUrl
		{
			get
			{
				return this.serviceUrlField;
			}
			set
			{
				this.serviceUrlField = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001F5BF File Offset: 0x0001D7BF
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0001F5C7 File Offset: 0x0001D7C7
		public string LocationUrl
		{
			get
			{
				return this.locationUrlField;
			}
			set
			{
				this.locationUrlField = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		// (set) Token: 0x0600084B RID: 2123 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		[XmlArrayItem("FileExtension", IsNullable = false)]
		public string[] SupportedFileExtensions
		{
			get
			{
				return this.supportedFileExtensionsField;
			}
			set
			{
				this.supportedFileExtensionsField = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		public bool ExternalAccessAllowed
		{
			get
			{
				return this.externalAccessAllowedField;
			}
			set
			{
				this.externalAccessAllowedField = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001F603 File Offset: 0x0001D803
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001F60B File Offset: 0x0001D80B
		public bool AnonymousAccessAllowed
		{
			get
			{
				return this.anonymousAccessAllowedField;
			}
			set
			{
				this.anonymousAccessAllowedField = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001F614 File Offset: 0x0001D814
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0001F61C File Offset: 0x0001D81C
		public bool CanModifyPermissions
		{
			get
			{
				return this.canModifyPermissionsField;
			}
			set
			{
				this.canModifyPermissionsField = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001F625 File Offset: 0x0001D825
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001F62D File Offset: 0x0001D82D
		public bool IsDefault
		{
			get
			{
				return this.isDefaultField;
			}
			set
			{
				this.isDefaultField = value;
			}
		}

		// Token: 0x0400031C RID: 796
		private string serviceUrlField;

		// Token: 0x0400031D RID: 797
		private string locationUrlField;

		// Token: 0x0400031E RID: 798
		private string displayNameField;

		// Token: 0x0400031F RID: 799
		private string[] supportedFileExtensionsField;

		// Token: 0x04000320 RID: 800
		private bool externalAccessAllowedField;

		// Token: 0x04000321 RID: 801
		private bool anonymousAccessAllowedField;

		// Token: 0x04000322 RID: 802
		private bool canModifyPermissionsField;

		// Token: 0x04000323 RID: 803
		private bool isDefaultField;
	}
}
