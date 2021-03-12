using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036B RID: 875
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class UnsubscribeType : BaseRequestType
	{
		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00029B60 File Offset: 0x00027D60
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x00029B68 File Offset: 0x00027D68
		public string SubscriptionId
		{
			get
			{
				return this.subscriptionIdField;
			}
			set
			{
				this.subscriptionIdField = value;
			}
		}

		// Token: 0x04001292 RID: 4754
		private string subscriptionIdField;
	}
}
