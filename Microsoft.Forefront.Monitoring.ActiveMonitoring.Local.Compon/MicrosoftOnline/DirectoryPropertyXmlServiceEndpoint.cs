using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012B RID: 299
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlServiceEndpoint : DirectoryPropertyXml
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x000203F2 File Offset: 0x0001E5F2
		// (set) Token: 0x0600084C RID: 2124 RVA: 0x000203FA File Offset: 0x0001E5FA
		[XmlElement("Value")]
		public XmlValueServiceEndpoint[] Value
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

		// Token: 0x04000450 RID: 1104
		private XmlValueServiceEndpoint[] valueField;
	}
}
