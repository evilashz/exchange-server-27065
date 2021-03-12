using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B0 RID: 176
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetClientIntentResponseMessageType : ResponseMessageType
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001FC95 File Offset: 0x0001DE95
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001FC9D File Offset: 0x0001DE9D
		public ClientIntentType ClientIntent
		{
			get
			{
				return this.clientIntentField;
			}
			set
			{
				this.clientIntentField = value;
			}
		}

		// Token: 0x04000549 RID: 1353
		private ClientIntentType clientIntentField;
	}
}
