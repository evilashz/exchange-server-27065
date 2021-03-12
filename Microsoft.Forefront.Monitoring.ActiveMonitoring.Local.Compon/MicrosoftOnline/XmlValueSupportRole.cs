using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000CC RID: 204
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class XmlValueSupportRole
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001F3C6 File Offset: 0x0001D5C6
		public SupportRoleValue SupportRole
		{
			get
			{
				return this.supportRoleField;
			}
			set
			{
				this.supportRoleField = value;
			}
		}

		// Token: 0x04000352 RID: 850
		private SupportRoleValue supportRoleField;
	}
}
