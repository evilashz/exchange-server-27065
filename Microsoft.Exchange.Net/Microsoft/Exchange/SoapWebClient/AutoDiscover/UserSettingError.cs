using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200011F RID: 287
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class UserSettingError
	{
		// Token: 0x040005CC RID: 1484
		public ErrorCode ErrorCode;

		// Token: 0x040005CD RID: 1485
		[XmlElement(IsNullable = true)]
		public string ErrorMessage;

		// Token: 0x040005CE RID: 1486
		[XmlElement(IsNullable = true)]
		public string SettingName;
	}
}
