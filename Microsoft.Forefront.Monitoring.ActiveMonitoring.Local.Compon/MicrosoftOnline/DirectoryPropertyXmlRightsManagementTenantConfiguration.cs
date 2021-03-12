using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200011F RID: 287
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementTenantConfigurationSingle))]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementTenantConfiguration : DirectoryPropertyXml
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x000202D7 File Offset: 0x0001E4D7
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x000202DF File Offset: 0x0001E4DF
		[XmlElement("Value")]
		public XmlValueRightsManagementTenantConfiguration[] Value
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

		// Token: 0x04000445 RID: 1093
		private XmlValueRightsManagementTenantConfiguration[] valueField;
	}
}
