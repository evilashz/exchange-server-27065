using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000129 RID: 297
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlServiceInstanceInfo : DirectoryPropertyXml
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x000203C0 File Offset: 0x0001E5C0
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x000203C8 File Offset: 0x0001E5C8
		public XmlValueServiceInstanceInfo Value
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

		// Token: 0x0400044E RID: 1102
		private XmlValueServiceInstanceInfo valueField;
	}
}
