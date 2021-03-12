using System;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x02000014 RID: 20
	internal class MailboxAdminSyncUser : AdminSyncUser
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00006C1A File Offset: 0x00004E1A
		public MailboxAdminSyncUser(string wlid, Guid objectGuid, string distinguishedName) : base(distinguishedName, objectGuid)
		{
			this.windowsLiveId = wlid;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006C2B File Offset: 0x00004E2B
		public string WindowsLiveId
		{
			get
			{
				return this.windowsLiveId;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006C33 File Offset: 0x00004E33
		public override string ToString()
		{
			return this.windowsLiveId;
		}

		// Token: 0x0400004A RID: 74
		private string windowsLiveId;
	}
}
