using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000929 RID: 2345
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueRightsManagementTenantKey
	{
		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x06006F96 RID: 28566 RVA: 0x00176D1E File Offset: 0x00174F1E
		// (set) Token: 0x06006F97 RID: 28567 RVA: 0x00176D26 File Offset: 0x00174F26
		[XmlElement(Order = 0)]
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

		// Token: 0x0400486E RID: 18542
		private RightsManagementTenantKeyValue rightsManagementTenantKeyField;
	}
}
