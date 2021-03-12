using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001CA RID: 458
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ProtectionRuleSenderDepartmentsType
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00025503 File Offset: 0x00023703
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0002550B File Offset: 0x0002370B
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

		// Token: 0x04000D82 RID: 3458
		private string[] valueField;
	}
}
