using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000821 RID: 2081
	[XmlType(TypeName = "ItemIndexErrorType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ItemIndexError
	{
		// Token: 0x0400214B RID: 8523
		None,
		// Token: 0x0400214C RID: 8524
		GenericError,
		// Token: 0x0400214D RID: 8525
		Timeout,
		// Token: 0x0400214E RID: 8526
		StaleEvent,
		// Token: 0x0400214F RID: 8527
		MailboxOffline,
		// Token: 0x04002150 RID: 8528
		AttachmentLimitReached,
		// Token: 0x04002151 RID: 8529
		MarsWriterTruncation,
		// Token: 0x04002152 RID: 8530
		DocumentParserFailure
	}
}
