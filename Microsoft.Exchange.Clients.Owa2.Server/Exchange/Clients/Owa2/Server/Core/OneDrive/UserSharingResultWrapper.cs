using System;
using Microsoft.SharePoint.Client.Sharing;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000027 RID: 39
	public class UserSharingResultWrapper : IUserSharingResult
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000038AA File Offset: 0x00001AAA
		public string InvitationLink
		{
			get
			{
				return this.backingResult.InvitationLink;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000038B7 File Offset: 0x00001AB7
		public bool Status
		{
			get
			{
				return this.backingResult.Status;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000038C4 File Offset: 0x00001AC4
		public string User
		{
			get
			{
				return this.backingResult.User;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000038D1 File Offset: 0x00001AD1
		public UserSharingResultWrapper(UserSharingResult result)
		{
			this.backingResult = result;
		}

		// Token: 0x0400004F RID: 79
		private readonly UserSharingResult backingResult;
	}
}
