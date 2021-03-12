using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000047 RID: 71
	[GeneratedCode("wsdl", "4.0.30319.1")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://www.w3.org/2005/08/addressing")]
	[XmlRoot("To", Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
	[Serializable]
	public class To : SoapHeader
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00016ED5 File Offset: 0x000150D5
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00016EDD File Offset: 0x000150DD
		[XmlText]
		public string Text
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

		// Token: 0x040001BE RID: 446
		private string textField;
	}
}
