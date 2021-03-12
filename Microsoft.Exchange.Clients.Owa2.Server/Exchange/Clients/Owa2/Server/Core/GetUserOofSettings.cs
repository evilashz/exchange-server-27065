using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200032E RID: 814
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetUserOofSettings : ServiceCommand<UserOofSettingsType>
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x00066B43 File Offset: 0x00064D43
		public GetUserOofSettings(CallContext callContext, ExTimeZone timeZone) : base(callContext)
		{
			this.timeZone = timeZone;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00066B53 File Offset: 0x00064D53
		protected override UserOofSettingsType InternalExecute()
		{
			return GetUserOofSettings.GetSetting(base.MailboxIdentityMailboxSession, this.timeZone);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00066B68 File Offset: 0x00064D68
		public static UserOofSettingsType GetSetting(MailboxSession mailboxSession, ExTimeZone timeZone)
		{
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(mailboxSession);
			return new UserOofSettingsType(userOofSettings, timeZone);
		}

		// Token: 0x04000F04 RID: 3844
		private readonly ExTimeZone timeZone;
	}
}
