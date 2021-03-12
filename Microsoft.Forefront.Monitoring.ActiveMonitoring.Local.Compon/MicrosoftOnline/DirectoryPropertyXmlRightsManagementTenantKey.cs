using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200011E RID: 286
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementTenantKey : DirectoryPropertyXml
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000202BE File Offset: 0x0001E4BE
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x000202C6 File Offset: 0x0001E4C6
		[XmlElement("Value")]
		public XmlValueRightsManagementTenantKey[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04000444 RID: 1092
		private XmlValueRightsManagementTenantKey[] valueField;
	}
}
