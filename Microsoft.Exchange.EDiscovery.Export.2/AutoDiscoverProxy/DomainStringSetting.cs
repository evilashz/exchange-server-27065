using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000080 RID: 128
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class DomainStringSetting : DomainSetting
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001F55A File Offset: 0x0001D75A
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0001F562 File Offset: 0x0001D762
		[XmlElement(IsNullable = true)]
		public string Value
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

		// Token: 0x0400030C RID: 780
		private string valueField;
	}
}
