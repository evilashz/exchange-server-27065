using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000A5 RID: 165
	[XmlRoot("DateTimePrecision", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class DateTimePrecisionType : SoapHeader
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0001FA9B File Offset: 0x0001DC9B
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x0001FAA3 File Offset: 0x0001DCA3
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

		// Token: 0x0400035B RID: 859
		private string[] textField;
	}
}
