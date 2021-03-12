using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000026 RID: 38
	public interface IUserSharingResult
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CE RID: 206
		string InvitationLink { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CF RID: 207
		bool Status { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D0 RID: 208
		string User { get; }
	}
}
