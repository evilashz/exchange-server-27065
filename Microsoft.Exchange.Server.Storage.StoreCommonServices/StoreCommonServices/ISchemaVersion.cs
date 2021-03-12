using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000085 RID: 133
	public interface ISchemaVersion
	{
		// Token: 0x060004DD RID: 1245
		ComponentVersion GetCurrentSchemaVersion(Context context);

		// Token: 0x060004DE RID: 1246
		void SetCurrentSchemaVersion(Context context, ComponentVersion version);

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004DF RID: 1247
		string Identifier { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004E0 RID: 1248
		int MailboxNumber { get; }
	}
}
