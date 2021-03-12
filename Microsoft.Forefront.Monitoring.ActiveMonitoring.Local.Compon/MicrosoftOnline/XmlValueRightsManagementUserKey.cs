using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000BC RID: 188
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueRightsManagementUserKey
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001F18C File Offset: 0x0001D38C
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0001F194 File Offset: 0x0001D394
		public RightsManagementUserKeyValue RightsManagementUserKey
		{
			get
			{
				return this.rightsManagementUserKeyField;
			}
			set
			{
				this.rightsManagementUserKeyField = value;
			}
		}

		// Token: 0x04000334 RID: 820
		private RightsManagementUserKeyValue rightsManagementUserKeyField;
	}
}
