using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200033F RID: 831
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetHoldOnMailboxesType : BaseRequestType
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x000291C0 File Offset: 0x000273C0
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x000291C8 File Offset: 0x000273C8
		public string HoldId
		{
			get
			{
				return this.holdIdField;
			}
			set
			{
				this.holdIdField = value;
			}
		}

		// Token: 0x040011E8 RID: 4584
		private string holdIdField;
	}
}
