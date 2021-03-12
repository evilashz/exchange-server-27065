using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000126 RID: 294
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlServiceInstanceMap : DirectoryPropertyXml
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00020375 File Offset: 0x0001E575
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0002037D File Offset: 0x0001E57D
		public XmlValueServiceInstanceMap Value
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

		// Token: 0x0400044B RID: 1099
		private XmlValueServiceInstanceMap valueField;
	}
}
