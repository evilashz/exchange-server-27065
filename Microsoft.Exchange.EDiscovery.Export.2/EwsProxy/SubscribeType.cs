using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200036C RID: 876
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SubscribeType : BaseRequestType
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00029B79 File Offset: 0x00027D79
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x00029B81 File Offset: 0x00027D81
		[XmlElement("PushSubscriptionRequest", typeof(PushSubscriptionRequestType))]
		[XmlElement("StreamingSubscriptionRequest", typeof(StreamingSubscriptionRequestType))]
		[XmlElement("PullSubscriptionRequest", typeof(PullSubscriptionRequestType))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04001293 RID: 4755
		private object itemField;
	}
}
