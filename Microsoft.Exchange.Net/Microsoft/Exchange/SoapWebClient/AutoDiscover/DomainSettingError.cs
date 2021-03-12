using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000113 RID: 275
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class DomainSettingError
	{
		// Token: 0x040005A4 RID: 1444
		public ErrorCode ErrorCode;

		// Token: 0x040005A5 RID: 1445
		[XmlElement(IsNullable = true)]
		public string ErrorMessage;

		// Token: 0x040005A6 RID: 1446
		[XmlElement(IsNullable = true)]
		public string SettingName;
	}
}
