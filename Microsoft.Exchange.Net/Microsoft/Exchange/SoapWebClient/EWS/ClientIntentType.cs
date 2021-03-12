using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000192 RID: 402
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ClientIntentType
	{
		// Token: 0x0400099C RID: 2460
		public ItemIdType ItemId;

		// Token: 0x0400099D RID: 2461
		public int Intent;

		// Token: 0x0400099E RID: 2462
		public int ItemVersion;

		// Token: 0x0400099F RID: 2463
		public bool WouldRepair;

		// Token: 0x040009A0 RID: 2464
		public ClientIntentMeetingInquiryActionType PredictedAction;

		// Token: 0x040009A1 RID: 2465
		[XmlIgnore]
		public bool PredictedActionSpecified;
	}
}
