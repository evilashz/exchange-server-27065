using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000115 RID: 277
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlStrongAuthenticationMethod : DirectoryPropertyXml
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000201EE File Offset: 0x0001E3EE
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x000201F6 File Offset: 0x0001E3F6
		[XmlElement("Value")]
		public XmlValueStrongAuthenticationMethod[] Value
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

		// Token: 0x0400043C RID: 1084
		private XmlValueStrongAuthenticationMethod[] valueField;
	}
}
