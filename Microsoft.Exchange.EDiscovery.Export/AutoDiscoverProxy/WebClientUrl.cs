using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000085 RID: 133
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class WebClientUrl
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		[XmlElement(IsNullable = true)]
		public string AuthenticationMethods
		{
			get
			{
				return this.authenticationMethodsField;
			}
			set
			{
				this.authenticationMethodsField = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001F6BD File Offset: 0x0001D8BD
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0001F6C5 File Offset: 0x0001D8C5
		[XmlElement(IsNullable = true)]
		public string Url
		{
			get
			{
				return this.urlField;
			}
			set
			{
				this.urlField = value;
			}
		}

		// Token: 0x0400032A RID: 810
		private string authenticationMethodsField;

		// Token: 0x0400032B RID: 811
		private string urlField;
	}
}
