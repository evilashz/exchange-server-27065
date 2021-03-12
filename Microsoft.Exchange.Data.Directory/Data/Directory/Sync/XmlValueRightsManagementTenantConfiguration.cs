using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200092B RID: 2347
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueRightsManagementTenantConfiguration
	{
		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x00176D72 File Offset: 0x00174F72
		// (set) Token: 0x06006FA1 RID: 28577 RVA: 0x00176D7A File Offset: 0x00174F7A
		[XmlElement(Order = 0)]
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

		// Token: 0x04004872 RID: 18546
		private RightsManagementTenantConfigurationValue rightsManagementTenantConfigurationField;
	}
}
