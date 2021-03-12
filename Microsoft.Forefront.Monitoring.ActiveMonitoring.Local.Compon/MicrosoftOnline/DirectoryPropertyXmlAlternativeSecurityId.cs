using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000117 RID: 279
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[Serializable]
	public class DirectoryPropertyXmlAlternativeSecurityId : DirectoryPropertyXml
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00020220 File Offset: 0x0001E420
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00020228 File Offset: 0x0001E428
		[XmlElement("Value")]
		public XmlValueAlternativeSecurityId[] Value
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

		// Token: 0x0400043E RID: 1086
		private XmlValueAlternativeSecurityId[] valueField;
	}
}
