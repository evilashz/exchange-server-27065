using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000034 RID: 52
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.1")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://www.w3.org/2005/08/addressing")]
	[XmlRoot("Action", Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
	[Serializable]
	public class Action : SoapHeader
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000672A File Offset: 0x0000492A
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006732 File Offset: 0x00004932
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

		// Token: 0x040000B3 RID: 179
		private string textField;
	}
}
