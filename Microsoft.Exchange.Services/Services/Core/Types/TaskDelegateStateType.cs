using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008A0 RID: 2208
	[XmlType(TypeName = "TaskDelegateStateType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TaskDelegateStateType
	{
		// Token: 0x0400241B RID: 9243
		NoMatch,
		// Token: 0x0400241C RID: 9244
		OwnNew,
		// Token: 0x0400241D RID: 9245
		Owned,
		// Token: 0x0400241E RID: 9246
		Accepted,
		// Token: 0x0400241F RID: 9247
		Declined,
		// Token: 0x04002420 RID: 9248
		Max
	}
}
