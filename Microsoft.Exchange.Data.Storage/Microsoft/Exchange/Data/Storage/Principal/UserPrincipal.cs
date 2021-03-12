using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000268 RID: 616
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserPrincipal : ExchangePrincipal, IUserPrincipal, IExchangePrincipal
	{
		// Token: 0x0600193F RID: 6463 RVA: 0x00079090 File Offset: 0x00077290
		public UserPrincipal(IGenericADUser adUser, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions) : base(adUser, allMailboxes, mailboxSelector, remotingOptions)
		{
			this.UserPrincipalName = adUser.UserPrincipalName;
			this.WindowsLiveId = adUser.WindowsLiveID;
			this.NetId = adUser.NetId;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000790C1 File Offset: 0x000772C1
		private UserPrincipal(UserPrincipal sourceExchangePrincipal) : base(sourceExchangePrincipal)
		{
			this.UserPrincipalName = sourceExchangePrincipal.UserPrincipalName;
			this.WindowsLiveId = sourceExchangePrincipal.WindowsLiveId;
			this.NetId = sourceExchangePrincipal.NetId;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x000790EE File Offset: 0x000772EE
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x000790F6 File Offset: 0x000772F6
		public string UserPrincipalName { get; private set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000790FF File Offset: 0x000772FF
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x00079107 File Offset: 0x00077307
		public SmtpAddress WindowsLiveId { get; private set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00079110 File Offset: 0x00077310
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x00079118 File Offset: 0x00077318
		public NetID NetId { get; private set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00079124 File Offset: 0x00077324
		public override string PrincipalId
		{
			get
			{
				if (!string.IsNullOrEmpty(this.UserPrincipalName))
				{
					return this.UserPrincipalName.Split(new char[]
					{
						'@'
					})[0];
				}
				if (base.MailboxInfo.PrimarySmtpAddress.IsValidAddress && base.MailboxInfo.PrimarySmtpAddress.Local != null)
				{
					return base.MailboxInfo.PrimarySmtpAddress.Local;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0007919C File Offset: 0x0007739C
		protected override ExchangePrincipal Clone()
		{
			return new UserPrincipal(this);
		}
	}
}
