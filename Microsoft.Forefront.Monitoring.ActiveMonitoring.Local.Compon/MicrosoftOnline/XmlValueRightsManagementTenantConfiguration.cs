using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000C0 RID: 192
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueRightsManagementTenantConfiguration
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0001F223 File Offset: 0x0001D423
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0001F22B File Offset: 0x0001D42B
		public RightsManagementTenantConfigurationValue RightsManagementTenantConfiguration
		{
			get
			{
				return this.rightsManagementTenantConfigurationField;
			}
			set
			{
				this.rightsManagementTenantConfigurationField = value;
			}
		}

		// Token: 0x0400033B RID: 827
		private RightsManagementTenantConfigurationValue rightsManagementTenantConfigurationField;
	}
}
