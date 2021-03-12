using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000237 RID: 567
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class SubscribeResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00026547 File Offset: 0x00024747
		// (set) Token: 0x06001582 RID: 5506 RVA: 0x0002654F File Offset: 0x0002474F
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

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00026558 File Offset: 0x00024758
		// (set) Token: 0x06001584 RID: 5508 RVA: 0x00026560 File Offset: 0x00024760
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

		// Token: 0x04000ED0 RID: 3792
		private string subscriptionIdField;

		// Token: 0x04000ED1 RID: 3793
		private string watermarkField;
	}
}
