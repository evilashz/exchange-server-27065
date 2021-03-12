using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012A RID: 298
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlServiceInfo : DirectoryPropertyXml
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000203D9 File Offset: 0x0001E5D9
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x000203E1 File Offset: 0x0001E5E1
		[XmlElement("Value")]
		public XmlValueServiceInfo[] Value
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

		// Token: 0x0400044F RID: 1103
		private XmlValueServiceInfo[] valueField;
	}
}
