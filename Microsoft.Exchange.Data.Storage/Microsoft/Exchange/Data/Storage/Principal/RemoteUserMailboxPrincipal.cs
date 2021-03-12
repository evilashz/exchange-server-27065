using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x0200027A RID: 634
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoteUserMailboxPrincipal : ExchangePrincipal, IUserPrincipal, IExchangePrincipal
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x0007C780 File Offset: 0x0007A980
		public RemoteUserMailboxPrincipal(IGenericADUser adUser, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions) : base(adUser, allMailboxes, mailboxSelector, remotingOptions)
		{
			this.UserPrincipalName = adUser.UserPrincipalName;
			this.WindowsLiveId = adUser.WindowsLiveID;
			this.NetId = adUser.NetId;
			this.PrimarySmtpAddress = adUser.PrimarySmtpAddress;
			this.DisplayName = adUser.DisplayName;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0007C7D4 File Offset: 0x0007A9D4
		private RemoteUserMailboxPrincipal(RemoteUserMailboxPrincipal sourceExchangePrincipal) : base(sourceExchangePrincipal)
		{
			this.UserPrincipalName = sourceExchangePrincipal.UserPrincipalName;
			this.WindowsLiveId = sourceExchangePrincipal.WindowsLiveId;
			this.NetId = sourceExchangePrincipal.NetId;
			this.PrimarySmtpAddress = sourceExchangePrincipal.PrimarySmtpAddress;
			this.DisplayName = sourceExchangePrincipal.DisplayName;
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0007C824 File Offset: 0x0007AA24
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0007C82C File Offset: 0x0007AA2C
		public string UserPrincipalName { get; private set; }

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0007C835 File Offset: 0x0007AA35
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x0007C83D File Offset: 0x0007AA3D
		public SmtpAddress WindowsLiveId { get; private set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0007C846 File Offset: 0x0007AA46
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0007C84E File Offset: 0x0007AA4E
		public NetID NetId { get; private set; }

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0007C857 File Offset: 0x0007AA57
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0007C85F File Offset: 0x0007AA5F
		public SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0007C868 File Offset: 0x0007AA68
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0007C870 File Offset: 0x0007AA70
		public string DisplayName { get; private set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0007C879 File Offset: 0x0007AA79
		public override bool IsMailboxInfoRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x0007C87C File Offset: 0x0007AA7C
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
				if (this.PrimarySmtpAddress.IsValidAddress && this.PrimarySmtpAddress.Local != null)
				{
					return this.PrimarySmtpAddress.Local;
				}
				return string.Empty;
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0007C8E5 File Offset: 0x0007AAE5
		protected override ExchangePrincipal Clone()
		{
			return new RemoteUserMailboxPrincipal(this);
		}
	}
}
