using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001CE RID: 462
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRuleArgumentType
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00025546 File Offset: 0x00023746
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x0002554E File Offset: 0x0002374E
		[XmlAttribute]
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

		// Token: 0x04000D91 RID: 3473
		private string valueField;
	}
}
