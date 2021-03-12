using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036A RID: 874
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetEventsType : BaseRequestType
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00029B36 File Offset: 0x00027D36
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x00029B3E File Offset: 0x00027D3E
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00029B47 File Offset: 0x00027D47
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x00029B4F File Offset: 0x00027D4F
		public string Watermark
		{
			get
			{
				return this.watermarkField;
			}
			set
			{
				this.watermarkField = value;
			}
		}

		// Token: 0x04001290 RID: 4752
		private string subscriptionIdField;

		// Token: 0x04001291 RID: 4753
		private string watermarkField;
	}
}
