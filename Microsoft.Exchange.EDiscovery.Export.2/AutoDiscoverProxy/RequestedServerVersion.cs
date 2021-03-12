using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200009C RID: 156
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RequestedServerVersion : SoapHeader
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001F9E2 File Offset: 0x0001DBE2
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0001F9EA File Offset: 0x0001DBEA
		[XmlText]
		public string[] Text
		{
			get
			{
				return this.textField;
			}
			set
			{
				this.textField = value;
			}
		}

		// Token: 0x04000356 RID: 854
		private string[] textField;
	}
}
