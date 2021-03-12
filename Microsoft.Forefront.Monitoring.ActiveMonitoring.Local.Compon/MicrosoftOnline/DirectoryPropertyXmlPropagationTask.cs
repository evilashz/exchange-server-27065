using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012E RID: 302
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlPropagationTask : DirectoryPropertyXml
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0002043D File Offset: 0x0001E63D
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x00020445 File Offset: 0x0001E645
		[XmlElement("Value")]
		public XmlValuePropagationTask[] Value
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

		// Token: 0x04000453 RID: 1107
		private XmlValuePropagationTask[] valueField;
	}
}
