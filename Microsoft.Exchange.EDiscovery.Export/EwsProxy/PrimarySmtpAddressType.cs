using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A9 RID: 937
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class PrimarySmtpAddressType
	{
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0002A676 File Offset: 0x00028876
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0002A67E File Offset: 0x0002887E
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

		// Token: 0x0400135D RID: 4957
		private string valueField;
	}
}
