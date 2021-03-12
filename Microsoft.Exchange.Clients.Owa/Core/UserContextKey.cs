using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200027C RID: 636
	internal sealed class UserContextKey
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00083845 File Offset: 0x00081A45
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x0008384D File Offset: 0x00081A4D
		public Canary Canary { get; private set; }

		// Token: 0x0600165B RID: 5723 RVA: 0x00083856 File Offset: 0x00081A56
		private UserContextKey(Guid userContextId, string logonUniqueKey, string mailboxUniqueKey)
		{
			this.Canary = new Canary(userContextId, logonUniqueKey);
			this.mailboxUniqueKey = mailboxUniqueKey;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00083872 File Offset: 0x00081A72
		private UserContextKey(Canary canary, string mailboxUniqueKey)
		{
			this.Canary = canary;
			this.mailboxUniqueKey = mailboxUniqueKey;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00083888 File Offset: 0x00081A88
		internal UserContextKey CloneWithRenewedCanary()
		{
			return new UserContextKey(this.Canary.CloneRenewed(), this.mailboxUniqueKey);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x000838A0 File Offset: 0x00081AA0
		internal static UserContextKey Create(Guid userContextIdGuid, string logonUniqueKey, string mailboxUniqueKey)
		{
			return new UserContextKey(userContextIdGuid, logonUniqueKey, mailboxUniqueKey);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000838AA File Offset: 0x00081AAA
		internal static UserContextKey CreateFromCookie(UserContextCookie userContextCookie)
		{
			return new UserContextKey(userContextCookie.ContextCanary, userContextCookie.MailboxUniqueKey);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000838C0 File Offset: 0x00081AC0
		internal static UserContextKey CreateNew(OwaContext owaContext)
		{
			string uniqueId = owaContext.LogonIdentity.UniqueId;
			string text = owaContext.IsDifferentMailbox ? owaContext.MailboxIdentity.UniqueId : null;
			return UserContextKey.Create(Guid.NewGuid(), uniqueId, text);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000838FC File Offset: 0x00081AFC
		public override string ToString()
		{
			if (this.keyString == null)
			{
				this.keyString = this.UserContextId + ":" + this.Canary.LogonUniqueKey;
				if (this.mailboxUniqueKey != null)
				{
					this.keyString = this.keyString + ":" + this.mailboxUniqueKey;
				}
			}
			return this.keyString;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x0008395C File Offset: 0x00081B5C
		internal string UserContextId
		{
			get
			{
				return this.Canary.UserContextId;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00083969 File Offset: 0x00081B69
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x00083971 File Offset: 0x00081B71
		internal string MailboxUniqueKey
		{
			get
			{
				return this.mailboxUniqueKey;
			}
			set
			{
				this.mailboxUniqueKey = value;
			}
		}

		// Token: 0x0400115D RID: 4445
		private string mailboxUniqueKey;

		// Token: 0x0400115E RID: 4446
		private string keyString;
	}
}
