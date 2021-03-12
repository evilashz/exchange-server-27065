using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A8 RID: 424
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CDRDataType
	{
		// Token: 0x040009DE RID: 2526
		public DateTime CallStartTime;

		// Token: 0x040009DF RID: 2527
		public string CallType;

		// Token: 0x040009E0 RID: 2528
		public string CallIdentity;

		// Token: 0x040009E1 RID: 2529
		public string ParentCallIdentity;

		// Token: 0x040009E2 RID: 2530
		public string UMServerName;

		// Token: 0x040009E3 RID: 2531
		public string DialPlanGuid;

		// Token: 0x040009E4 RID: 2532
		public string DialPlanName;

		// Token: 0x040009E5 RID: 2533
		public int CallDuration;

		// Token: 0x040009E6 RID: 2534
		public string IPGatewayAddress;

		// Token: 0x040009E7 RID: 2535
		public string IPGatewayName;

		// Token: 0x040009E8 RID: 2536
		public string GatewayGuid;

		// Token: 0x040009E9 RID: 2537
		public string CalledPhoneNumber;

		// Token: 0x040009EA RID: 2538
		public string CallerPhoneNumber;

		// Token: 0x040009EB RID: 2539
		public string OfferResult;

		// Token: 0x040009EC RID: 2540
		public string DropCallReason;

		// Token: 0x040009ED RID: 2541
		public string ReasonForCall;

		// Token: 0x040009EE RID: 2542
		public string TransferredNumber;

		// Token: 0x040009EF RID: 2543
		public string DialedString;

		// Token: 0x040009F0 RID: 2544
		public string CallerMailboxAlias;

		// Token: 0x040009F1 RID: 2545
		public string CalleeMailboxAlias;

		// Token: 0x040009F2 RID: 2546
		public string CallerLegacyExchangeDN;

		// Token: 0x040009F3 RID: 2547
		public string CalleeLegacyExchangeDN;

		// Token: 0x040009F4 RID: 2548
		public string AutoAttendantName;

		// Token: 0x040009F5 RID: 2549
		public AudioQualityType AudioQualityMetrics;

		// Token: 0x040009F6 RID: 2550
		public DateTime CreationTime;
	}
}
