using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003C2 RID: 962
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ConversationActionType
	{
		// Token: 0x04001528 RID: 5416
		public ConversationActionTypeType Action;

		// Token: 0x04001529 RID: 5417
		public ItemIdType ConversationId;

		// Token: 0x0400152A RID: 5418
		public TargetFolderIdType ContextFolderId;

		// Token: 0x0400152B RID: 5419
		public DateTime ConversationLastSyncTime;

		// Token: 0x0400152C RID: 5420
		[XmlIgnore]
		public bool ConversationLastSyncTimeSpecified;

		// Token: 0x0400152D RID: 5421
		public bool ProcessRightAway;

		// Token: 0x0400152E RID: 5422
		[XmlIgnore]
		public bool ProcessRightAwaySpecified;

		// Token: 0x0400152F RID: 5423
		public TargetFolderIdType DestinationFolderId;

		// Token: 0x04001530 RID: 5424
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories;

		// Token: 0x04001531 RID: 5425
		public bool EnableAlwaysDelete;

		// Token: 0x04001532 RID: 5426
		[XmlIgnore]
		public bool EnableAlwaysDeleteSpecified;

		// Token: 0x04001533 RID: 5427
		public bool IsRead;

		// Token: 0x04001534 RID: 5428
		[XmlIgnore]
		public bool IsReadSpecified;

		// Token: 0x04001535 RID: 5429
		public DisposalType DeleteType;

		// Token: 0x04001536 RID: 5430
		[XmlIgnore]
		public bool DeleteTypeSpecified;

		// Token: 0x04001537 RID: 5431
		public RetentionType RetentionPolicyType;

		// Token: 0x04001538 RID: 5432
		[XmlIgnore]
		public bool RetentionPolicyTypeSpecified;

		// Token: 0x04001539 RID: 5433
		public string RetentionPolicyTagId;

		// Token: 0x0400153A RID: 5434
		public FlagType Flag;

		// Token: 0x0400153B RID: 5435
		public bool SuppressReadReceipts;

		// Token: 0x0400153C RID: 5436
		[XmlIgnore]
		public bool SuppressReadReceiptsSpecified;
	}
}
