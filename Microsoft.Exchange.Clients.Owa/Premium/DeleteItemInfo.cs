using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004BA RID: 1210
	[OwaEventStruct("ItmInf")]
	internal sealed class DeleteItemInfo
	{
		// Token: 0x04001F9E RID: 8094
		public const string StructNamespace = "ItmInf";

		// Token: 0x04001F9F RID: 8095
		public const string StoreObjectIdName = "ID";

		// Token: 0x04001FA0 RID: 8096
		public const string IsMeetingMessageName = "MM";

		// Token: 0x04001FA1 RID: 8097
		[OwaEventField("ID", false, null)]
		public OwaStoreObjectId OwaStoreObjectId;

		// Token: 0x04001FA2 RID: 8098
		[OwaEventField("MM", true, false)]
		public bool IsMeetingMessage;
	}
}
