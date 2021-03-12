using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200041E RID: 1054
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class SetHoldOnMailboxesType : BaseRequestType
	{
		// Token: 0x0400162B RID: 5675
		public HoldActionType ActionType;

		// Token: 0x0400162C RID: 5676
		public string HoldId;

		// Token: 0x0400162D RID: 5677
		public string Query;

		// Token: 0x0400162E RID: 5678
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Mailboxes;

		// Token: 0x0400162F RID: 5679
		public string Language;

		// Token: 0x04001630 RID: 5680
		public bool IncludeNonIndexableItems;

		// Token: 0x04001631 RID: 5681
		[XmlIgnore]
		public bool IncludeNonIndexableItemsSpecified;

		// Token: 0x04001632 RID: 5682
		public bool Deduplication;

		// Token: 0x04001633 RID: 5683
		[XmlIgnore]
		public bool DeduplicationSpecified;

		// Token: 0x04001634 RID: 5684
		public string InPlaceHoldIdentity;

		// Token: 0x04001635 RID: 5685
		public string ItemHoldPeriod;
	}
}
