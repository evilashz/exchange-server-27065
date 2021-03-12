using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BE RID: 190
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class XmlValueRightsManagementTenantKey
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		public RightsManagementTenantKeyValue RightsManagementTenantKey
		{
			get
			{
				return this.rightsManagementTenantKeyField;
			}
			set
			{
				this.rightsManagementTenantKeyField = value;
			}
		}

		// Token: 0x04000338 RID: 824
		private RightsManagementTenantKeyValue rightsManagementTenantKeyField;
	}
}
