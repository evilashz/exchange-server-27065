using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000788 RID: 1928
	[XmlType(TypeName = "FlaggedForActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum FlaggedForAction
	{
		// Token: 0x04002009 RID: 8201
		Any,
		// Token: 0x0400200A RID: 8202
		Call,
		// Token: 0x0400200B RID: 8203
		DoNotForward,
		// Token: 0x0400200C RID: 8204
		FollowUp,
		// Token: 0x0400200D RID: 8205
		Forward = 5,
		// Token: 0x0400200E RID: 8206
		FYI = 4,
		// Token: 0x0400200F RID: 8207
		NoResponseNecessary = 6,
		// Token: 0x04002010 RID: 8208
		Read,
		// Token: 0x04002011 RID: 8209
		Reply,
		// Token: 0x04002012 RID: 8210
		ReplyToAll,
		// Token: 0x04002013 RID: 8211
		Review
	}
}
