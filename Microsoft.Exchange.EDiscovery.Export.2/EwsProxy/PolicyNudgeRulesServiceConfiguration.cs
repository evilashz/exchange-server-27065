using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001D1 RID: 465
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PolicyNudgeRulesServiceConfiguration
	{
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0002559A File Offset: 0x0002379A
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x000255A2 File Offset: 0x000237A2
		[XmlAnyElement]
		public XmlElement[] Any
		{
			get
			{
				return this.anyField;
			}
			set
			{
				this.anyField = value;
			}
		}

		// Token: 0x04000D97 RID: 3479
		private XmlElement[] anyField;
	}
}
