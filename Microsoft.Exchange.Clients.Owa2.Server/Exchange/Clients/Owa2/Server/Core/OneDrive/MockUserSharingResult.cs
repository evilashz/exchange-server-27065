using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000028 RID: 40
	public class MockUserSharingResult : IUserSharingResult
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000038E0 File Offset: 0x00001AE0
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000038E8 File Offset: 0x00001AE8
		public string InvitationLink { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000038F1 File Offset: 0x00001AF1
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000038F9 File Offset: 0x00001AF9
		public bool Status { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003902 File Offset: 0x00001B02
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000390A File Offset: 0x00001B0A
		public string User { get; set; }
	}
}
