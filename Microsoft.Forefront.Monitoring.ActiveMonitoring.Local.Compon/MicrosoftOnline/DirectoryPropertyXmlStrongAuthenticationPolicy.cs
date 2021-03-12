using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000116 RID: 278
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlStrongAuthenticationPolicy : DirectoryPropertyXml
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00020207 File Offset: 0x0001E407
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0002020F File Offset: 0x0001E40F
		[XmlElement("Value")]
		public XmlValueStrongAuthenticationPolicy[] Value
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

		// Token: 0x0400043D RID: 1085
		private XmlValueStrongAuthenticationPolicy[] valueField;
	}
}
