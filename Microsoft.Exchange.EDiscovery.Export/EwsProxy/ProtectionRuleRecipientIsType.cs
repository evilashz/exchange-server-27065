using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001C9 RID: 457
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtectionRuleRecipientIsType
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000254EA File Offset: 0x000236EA
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x000254F2 File Offset: 0x000236F2
		[XmlElement("Value")]
		public string[] Value
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

		// Token: 0x04000D81 RID: 3457
		private string[] valueField;
	}
}
