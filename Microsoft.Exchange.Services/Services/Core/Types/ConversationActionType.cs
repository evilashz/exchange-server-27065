using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200072D RID: 1837
	[XmlType(TypeName = "ConversationActionTypeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum ConversationActionType
	{
		// Token: 0x04001EE6 RID: 7910
		AlwaysCategorize,
		// Token: 0x04001EE7 RID: 7911
		AlwaysDelete,
		// Token: 0x04001EE8 RID: 7912
		AlwaysMove,
		// Token: 0x04001EE9 RID: 7913
		Delete,
		// Token: 0x04001EEA RID: 7914
		Move,
		// Token: 0x04001EEB RID: 7915
		Copy,
		// Token: 0x04001EEC RID: 7916
		SetReadState,
		// Token: 0x04001EED RID: 7917
		SetRetentionPolicy,
		// Token: 0x04001EEE RID: 7918
		UpdateAlwaysCategorizeRule,
		// Token: 0x04001EEF RID: 7919
		Flag,
		// Token: 0x04001EF0 RID: 7920
		SetClutterState
	}
}
