using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000123 RID: 291
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlThrottleLimit : DirectoryPropertyXml
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0002032A File Offset: 0x0001E52A
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00020332 File Offset: 0x0001E532
		[XmlElement("Value")]
		public XmlValueThrottleLimit[] Value
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

		// Token: 0x04000448 RID: 1096
		private XmlValueThrottleLimit[] valueField;
	}
}
