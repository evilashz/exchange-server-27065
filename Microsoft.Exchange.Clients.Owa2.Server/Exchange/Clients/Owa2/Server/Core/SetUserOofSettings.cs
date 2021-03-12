using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000359 RID: 857
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SetUserOofSettings : ServiceCommand<bool>
	{
		// Token: 0x06001BCA RID: 7114 RVA: 0x0006B194 File Offset: 0x00069394
		public SetUserOofSettings(CallContext callContext, UserOofSettingsType userOofSettings) : base(callContext)
		{
			this.userOofSettings = userOofSettings;
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0006B1A4 File Offset: 0x000693A4
		protected override bool InternalExecute()
		{
			MailboxSession mailboxIdentityMailboxSession = base.CallContext.SessionCache.GetMailboxIdentityMailboxSession();
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(mailboxIdentityMailboxSession);
			userOofSettings.OofState = (this.userOofSettings.IsOofOn ? OofState.Enabled : OofState.Disabled);
			userOofSettings.ExternalAudience = this.userOofSettings.ExternalAudience;
			userOofSettings.InternalReply.Message = UserOofSettingsType.ConvertTextToHtml(this.userOofSettings.InternalReply);
			userOofSettings.ExternalReply.Message = UserOofSettingsType.ConvertTextToHtml(this.userOofSettings.ExternalReply);
			userOofSettings.Save(mailboxIdentityMailboxSession);
			return true;
		}

		// Token: 0x04000FC5 RID: 4037
		private UserOofSettingsType userOofSettings;
	}
}
