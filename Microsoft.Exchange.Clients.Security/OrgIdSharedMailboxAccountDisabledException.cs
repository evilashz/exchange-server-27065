using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001B RID: 27
	public class OrgIdSharedMailboxAccountDisabledException : OrgIdMailboxNotFoundException
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003340 File Offset: 0x00001540
		protected override string ErrorMessageFormatString
		{
			get
			{
				return Strings.OrgIdSharedMailboxAccountDisabledErrorMessage;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003347 File Offset: 0x00001547
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				return -2011393914;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000334E File Offset: 0x0000154E
		public override ErrorMode? ErrorMode
		{
			get
			{
				return new ErrorMode?(Microsoft.Exchange.Clients.Common.ErrorMode.SharedMailboxAccountDisabled);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003356 File Offset: 0x00001556
		public OrgIdSharedMailboxAccountDisabledException(string userName, string logoutUrl) : base(userName, logoutUrl)
		{
		}
	}
}
