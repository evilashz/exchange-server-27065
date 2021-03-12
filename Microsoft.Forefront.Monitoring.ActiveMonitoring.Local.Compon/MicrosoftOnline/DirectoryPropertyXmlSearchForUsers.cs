using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200012C RID: 300
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlSearchForUsers : DirectoryPropertyXml
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002040B File Offset: 0x0001E60B
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00020413 File Offset: 0x0001E613
		[XmlElement("Value")]
		public XmlValueSearchForUsers[] Value
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

		// Token: 0x04000451 RID: 1105
		private XmlValueSearchForUsers[] valueField;
	}
}
