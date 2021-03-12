using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A8 RID: 936
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SmtpAddressType
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0002A65D File Offset: 0x0002885D
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x0002A665 File Offset: 0x00028865
		[XmlText]
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

		// Token: 0x0400135C RID: 4956
		private string valueField;
	}
}
