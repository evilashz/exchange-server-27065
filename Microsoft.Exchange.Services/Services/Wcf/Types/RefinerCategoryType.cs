using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B34 RID: 2868
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RefinerCategoryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RefinerCategoryType
	{
		// Token: 0x04002D7C RID: 11644
		DateTimeReceived = 1,
		// Token: 0x04002D7D RID: 11645
		SearchRecipients,
		// Token: 0x04002D7E RID: 11646
		From,
		// Token: 0x04002D7F RID: 11647
		HasAttachment,
		// Token: 0x04002D80 RID: 11648
		FolderEntryId
	}
}
