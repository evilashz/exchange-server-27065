using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200018F RID: 399
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUMSubscriberCallAnsweringDataResponseMessageType : ResponseMessageType
	{
		// Token: 0x04000991 RID: 2449
		public bool IsOOF;

		// Token: 0x04000992 RID: 2450
		public UMMailboxTranscriptionEnabledSetting IsTranscriptionEnabledInMailboxConfig;

		// Token: 0x04000993 RID: 2451
		public bool IsMailboxQuotaExceeded;

		// Token: 0x04000994 RID: 2452
		[XmlElement(DataType = "base64Binary")]
		public byte[] Greeting;

		// Token: 0x04000995 RID: 2453
		public string GreetingName;

		// Token: 0x04000996 RID: 2454
		public bool TaskTimedOut;
	}
}
