using System;
using Microsoft.Exchange.Transport.Sync.Common.SendAsDefaults;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000246 RID: 582
	internal class SendAddressDefaultSetting
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x00078708 File Offset: 0x00076908
		public SendAddressDefaultSetting(UserContext userContext) : this(userContext.UserOptions.SendAddressDefault, userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString())
		{
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00078744 File Offset: 0x00076944
		protected SendAddressDefaultSetting(string settingValue, string userEmailAddress)
		{
			this.settingValue = (settingValue ?? string.Empty);
			this.isUserEmailAddress = this.settingValue.Equals(userEmailAddress);
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0007876E File Offset: 0x0007696E
		public bool IsUserEmailAddress
		{
			get
			{
				return this.isUserEmailAddress;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00078778 File Offset: 0x00076978
		public bool TryGetSubscriptionSendAddressId(out Guid subscriptionSendAddressId)
		{
			SendAsDefaultsManager sendAsDefaultsManager = new SendAsDefaultsManager();
			return sendAsDefaultsManager.TryParseSubscriptionSendAddressId(this.settingValue, out subscriptionSendAddressId);
		}

		// Token: 0x04000D7B RID: 3451
		private string settingValue;

		// Token: 0x04000D7C RID: 3452
		private bool isUserEmailAddress;
	}
}
