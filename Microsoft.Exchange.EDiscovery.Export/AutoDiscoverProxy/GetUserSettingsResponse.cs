using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000094 RID: 148
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUserSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001F900 File Offset: 0x0001DB00
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001F908 File Offset: 0x0001DB08
		[XmlArray(IsNullable = true)]
		public UserResponse[] UserResponses
		{
			get
			{
				return this.userResponsesField;
			}
			set
			{
				this.userResponsesField = value;
			}
		}

		// Token: 0x04000346 RID: 838
		private UserResponse[] userResponsesField;
	}
}
