using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000270 RID: 624
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class NonIndexableItemDetailType
	{
		// Token: 0x04001025 RID: 4133
		public ItemIdType ItemId;

		// Token: 0x04001026 RID: 4134
		public ItemIndexErrorType ErrorCode;

		// Token: 0x04001027 RID: 4135
		public string ErrorDescription;

		// Token: 0x04001028 RID: 4136
		public bool IsPartiallyIndexed;

		// Token: 0x04001029 RID: 4137
		public bool IsPermanentFailure;

		// Token: 0x0400102A RID: 4138
		public string SortValue;

		// Token: 0x0400102B RID: 4139
		public int AttemptCount;

		// Token: 0x0400102C RID: 4140
		public DateTime LastAttemptTime;

		// Token: 0x0400102D RID: 4141
		[XmlIgnore]
		public bool LastAttemptTimeSpecified;

		// Token: 0x0400102E RID: 4142
		public string AdditionalInfo;
	}
}
