using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000140 RID: 320
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlInclude(typeof(DirectoryPropertyXmlAnySingle))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAny : DirectoryPropertyXml
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000205BB File Offset: 0x0001E7BB
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x000205C3 File Offset: 0x0001E7C3
		[XmlElement("Value")]
		public XmlElement[] Value
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

		// Token: 0x04000461 RID: 1121
		private XmlElement[] valueField;
	}
}
