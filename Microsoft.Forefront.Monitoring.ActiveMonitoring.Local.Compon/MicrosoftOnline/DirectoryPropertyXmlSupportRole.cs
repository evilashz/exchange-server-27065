using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000125 RID: 293
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlSupportRole : DirectoryPropertyXml
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0002035C File Offset: 0x0001E55C
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00020364 File Offset: 0x0001E564
		[XmlElement("Value")]
		public XmlValueSupportRole[] Value
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

		// Token: 0x0400044A RID: 1098
		private XmlValueSupportRole[] valueField;
	}
}
