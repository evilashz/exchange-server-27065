using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003F8 RID: 1016
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetUMCallDataRecordsType : BaseRequestType
	{
		// Token: 0x040015BD RID: 5565
		public DateTime StartDateTime;

		// Token: 0x040015BE RID: 5566
		[XmlIgnore]
		public bool StartDateTimeSpecified;

		// Token: 0x040015BF RID: 5567
		public DateTime EndDateTime;

		// Token: 0x040015C0 RID: 5568
		[XmlIgnore]
		public bool EndDateTimeSpecified;

		// Token: 0x040015C1 RID: 5569
		public int Offset;

		// Token: 0x040015C2 RID: 5570
		[XmlIgnore]
		public bool OffsetSpecified;

		// Token: 0x040015C3 RID: 5571
		public int NumberOfRecords;

		// Token: 0x040015C4 RID: 5572
		[XmlIgnore]
		public bool NumberOfRecordsSpecified;

		// Token: 0x040015C5 RID: 5573
		public string UserLegacyExchangeDN;

		// Token: 0x040015C6 RID: 5574
		public UMCDRFilterByType FilterBy;
	}
}
