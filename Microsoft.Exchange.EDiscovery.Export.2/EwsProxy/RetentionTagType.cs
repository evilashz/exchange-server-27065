using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000186 RID: 390
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class RetentionTagType
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00023DD1 File Offset: 0x00021FD1
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00023DD9 File Offset: 0x00021FD9
		[XmlAttribute]
		public bool IsExplicit
		{
			get
			{
				return this.isExplicitField;
			}
			set
			{
				this.isExplicitField = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00023DE2 File Offset: 0x00021FE2
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x00023DEA File Offset: 0x00021FEA
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

		// Token: 0x04000B6F RID: 2927
		private bool isExplicitField;

		// Token: 0x04000B70 RID: 2928
		private string valueField;
	}
}
