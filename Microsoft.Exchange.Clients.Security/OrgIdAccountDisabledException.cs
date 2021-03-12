using System;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200001A RID: 26
	public class OrgIdAccountDisabledException : OrgIdMailboxNotFoundException
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003320 File Offset: 0x00001520
		protected override string ErrorMessageFormatString
		{
			get
			{
				return Strings.OrgIdAccountDisabledErrorMessage;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003327 File Offset: 0x00001527
		public override Strings.IDs ErrorMessageStringId
		{
			get
			{
				return -106213327;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000332E File Offset: 0x0000152E
		public override ErrorMode? ErrorMode
		{
			get
			{
				return new ErrorMode?(Microsoft.Exchange.Clients.Common.ErrorMode.AccountDisabled);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003336 File Offset: 0x00001536
		public OrgIdAccountDisabledException(string userName, string logoutUrl) : base(userName, logoutUrl)
		{
		}
	}
}
