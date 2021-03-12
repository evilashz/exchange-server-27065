using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000083 RID: 131
	public interface IMailboxContext
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D2 RID: 1234
		int MailboxNumber { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004D3 RID: 1235
		bool IsUnifiedMailbox { get; }

		// Token: 0x060004D4 RID: 1236
		string GetDisplayName(Context context);

		// Token: 0x060004D5 RID: 1237
		bool GetCreatedByMove(Context context);

		// Token: 0x060004D6 RID: 1238
		bool GetPreservingMailboxSignature(Context context);

		// Token: 0x060004D7 RID: 1239
		bool GetMRSPreservingMailboxSignature(Context context);

		// Token: 0x060004D8 RID: 1240
		HashSet<ushort> GetDefaultPromotedMessagePropertyIds(Context context);

		// Token: 0x060004D9 RID: 1241
		DateTime GetCreationTime(Context context);
	}
}
