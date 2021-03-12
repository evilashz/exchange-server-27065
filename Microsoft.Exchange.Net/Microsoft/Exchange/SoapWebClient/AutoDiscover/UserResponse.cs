using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x02000125 RID: 293
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UserResponse : AutodiscoverResponse
	{
		// Token: 0x040005DA RID: 1498
		[XmlElement(IsNullable = true)]
		public string RedirectTarget;

		// Token: 0x040005DB RID: 1499
		[XmlArray(IsNullable = true)]
		public UserSettingError[] UserSettingErrors;

		// Token: 0x040005DC RID: 1500
		[XmlArray(IsNullable = true)]
		public UserSetting[] UserSettings;
	}
}
