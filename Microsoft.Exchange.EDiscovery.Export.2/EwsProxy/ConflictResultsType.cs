using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000265 RID: 613
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ConflictResultsType
	{
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0002727A File Offset: 0x0002547A
		// (set) Token: 0x06001712 RID: 5906 RVA: 0x00027282 File Offset: 0x00025482
		public int Count
		{
			get
			{
				return this.countField;
			}
			set
			{
				this.countField = value;
			}
		}

		// Token: 0x04000FA2 RID: 4002
		private int countField;
	}
}
