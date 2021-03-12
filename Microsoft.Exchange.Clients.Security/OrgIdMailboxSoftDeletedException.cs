using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000019 RID: 25
	public class OrgIdMailboxSoftDeletedException : OrgIdMailboxNotFoundException
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003300 File Offset: 0x00001500
		protected override string ErrorMessageFormatString
		{
			get
			{
				return Strings.MailboxSoftDeletedErrorMessage;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003307 File Offset: 0x00001507
		public override ErrorMode? ErrorMode
		{
			get
			{
				return new ErrorMode?(Microsoft.Exchange.Clients.Common.ErrorMode.MailboxSoftDeleted);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000330F File Offset: 0x0000150F
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				return 637041586;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003316 File Offset: 0x00001516
		public OrgIdMailboxSoftDeletedException(string userName, string logoutUrl) : base(userName, logoutUrl)
		{
		}
	}
}
