using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200025A RID: 602
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class HighlightTermType
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00026CF4 File Offset: 0x00024EF4
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x00026CFC File Offset: 0x00024EFC
		public string Scope
		{
			get
			{
				return this.scopeField;
			}
			set
			{
				this.scopeField = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00026D05 File Offset: 0x00024F05
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x00026D0D File Offset: 0x00024F0D
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

		// Token: 0x04000F54 RID: 3924
		private string scopeField;

		// Token: 0x04000F55 RID: 3925
		private string valueField;
	}
}
