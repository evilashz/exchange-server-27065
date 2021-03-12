using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D5 RID: 1237
	[OwaEventStruct("rmInfo")]
	internal sealed class ReminderInfo
	{
		// Token: 0x04002116 RID: 8470
		public const string StructNamespace = "rmInfo";

		// Token: 0x04002117 RID: 8471
		public const string ItemIdName = "id";

		// Token: 0x04002118 RID: 8472
		public const string ChangeKeyName = "ck";

		// Token: 0x04002119 RID: 8473
		[OwaEventField("id", false, "")]
		public StoreObjectId ItemId;

		// Token: 0x0400211A RID: 8474
		[OwaEventField("ck", false, "")]
		public string ChangeKey;
	}
}
