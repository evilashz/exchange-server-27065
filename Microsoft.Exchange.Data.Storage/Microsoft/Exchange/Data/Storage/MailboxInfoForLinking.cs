using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004F9 RID: 1273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxInfoForLinking
	{
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000DE018 File Offset: 0x000DC218
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x000DE020 File Offset: 0x000DC220
		internal string TenantName { get; set; }

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600374F RID: 14159 RVA: 0x000DE029 File Offset: 0x000DC229
		// (set) Token: 0x06003750 RID: 14160 RVA: 0x000DE031 File Offset: 0x000DC231
		internal Guid MailboxGuid { get; set; }

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06003751 RID: 14161 RVA: 0x000DE03A File Offset: 0x000DC23A
		// (set) Token: 0x06003752 RID: 14162 RVA: 0x000DE042 File Offset: 0x000DC242
		internal CultureInfo PreferredCulture { get; set; }

		// Token: 0x06003753 RID: 14163 RVA: 0x000DE04B File Offset: 0x000DC24B
		internal MailboxInfoForLinking()
		{
			this.TenantName = string.Empty;
			this.MailboxGuid = Guid.NewGuid();
			this.PreferredCulture = CultureInfo.InvariantCulture;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000DE074 File Offset: 0x000DC274
		public static MailboxInfoForLinking CreateFromMailboxSession(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (mailboxSession.MailboxOwner == null)
			{
				throw new ArgumentNullException("mailboxSession.MailboxOwner");
			}
			string tenantName = string.Empty;
			if (mailboxSession.MailboxOwner.MailboxInfo.OrganizationId != null && mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit != null)
			{
				tenantName = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit.ToString();
			}
			CultureInfo preferredCulture = CultureInfo.InvariantCulture;
			if (mailboxSession.Capabilities.CanHaveCulture && mailboxSession.PreferedCulture != null)
			{
				preferredCulture = mailboxSession.PreferedCulture;
			}
			return new MailboxInfoForLinking
			{
				TenantName = tenantName,
				MailboxGuid = mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid,
				PreferredCulture = preferredCulture
			};
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000DE141 File Offset: 0x000DC341
		public override string ToString()
		{
			return string.Format("TenantName: {0}, MailboxGuid: {1}", this.TenantName, this.MailboxGuid);
		}
	}
}
