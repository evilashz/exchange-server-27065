using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000121 RID: 289
	internal sealed class MailTipsPermission
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00024353 File Offset: 0x00022553
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0002435B File Offset: 0x0002255B
		internal bool AccessEnabled { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00024364 File Offset: 0x00022564
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002436C File Offset: 0x0002256C
		internal MailTipsAccessLevel AccessLevel { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00024375 File Offset: 0x00022575
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002437D File Offset: 0x0002257D
		internal bool InAccessScope { get; private set; }

		// Token: 0x060007FC RID: 2044 RVA: 0x00024386 File Offset: 0x00022586
		internal MailTipsPermission(bool mailTipsAccessEnabled, MailTipsAccessLevel mailTipsAccessLevel, bool requesterInAccessScope)
		{
			this.AccessEnabled = mailTipsAccessEnabled;
			this.AccessLevel = mailTipsAccessLevel;
			this.InAccessScope = requesterInAccessScope;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000243A3 File Offset: 0x000225A3
		internal bool CanAccessAMailTip()
		{
			return this.InAccessScope && this.AccessEnabled && MailTipsAccessLevel.None != this.AccessLevel;
		}

		// Token: 0x040004BA RID: 1210
		internal static readonly MailTipsPermission AllAccess = new MailTipsPermission(true, MailTipsAccessLevel.All, true);

		// Token: 0x040004BB RID: 1211
		internal static MailTipsPermission NoAccess = new MailTipsPermission(false, MailTipsAccessLevel.None, false);
	}
}
