using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200011B RID: 283
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlKeyDescription : DirectoryPropertyXml
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00020284 File Offset: 0x0001E484
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0002028C File Offset: 0x0001E48C
		[XmlElement("Value")]
		public XmlValueKeyDescription[] Value
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

		// Token: 0x04000442 RID: 1090
		private XmlValueKeyDescription[] valueField;
	}
}
