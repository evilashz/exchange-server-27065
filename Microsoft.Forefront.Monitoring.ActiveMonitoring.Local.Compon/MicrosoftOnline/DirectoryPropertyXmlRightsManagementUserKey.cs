using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200011C RID: 284
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementUserKeySingle))]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementUserKey : DirectoryPropertyXml
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0002029D File Offset: 0x0001E49D
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x000202A5 File Offset: 0x0001E4A5
		[XmlElement("Value")]
		public XmlValueRightsManagementUserKey[] Value
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

		// Token: 0x04000443 RID: 1091
		private XmlValueRightsManagementUserKey[] valueField;
	}
}
