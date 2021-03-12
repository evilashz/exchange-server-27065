using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000134 RID: 308
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlDatacenterRedirection : DirectoryPropertyXml
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x000204C2 File Offset: 0x0001E6C2
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x000204CA File Offset: 0x0001E6CA
		[XmlElement("Value")]
		public XmlValueDatacenterRedirection[] Value
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

		// Token: 0x04000458 RID: 1112
		private XmlValueDatacenterRedirection[] valueField;
	}
}
