using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000132 RID: 306
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlGeographicLocation : DirectoryPropertyXml
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00020490 File Offset: 0x0001E690
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00020498 File Offset: 0x0001E698
		public XmlValueGeographicLocation Value
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

		// Token: 0x04000456 RID: 1110
		private XmlValueGeographicLocation valueField;
	}
}
