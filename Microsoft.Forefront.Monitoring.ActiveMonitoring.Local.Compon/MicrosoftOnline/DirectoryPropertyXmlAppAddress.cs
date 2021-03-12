using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000121 RID: 289
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlAppAddress : DirectoryPropertyXml
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x000202F8 File Offset: 0x0001E4F8
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00020300 File Offset: 0x0001E500
		[XmlElement("Value")]
		public XmlValueAppAddress[] Value
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

		// Token: 0x04000446 RID: 1094
		private XmlValueAppAddress[] valueField;
	}
}
