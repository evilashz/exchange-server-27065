using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000559 RID: 1369
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactChangeLogger : StorageLoggerBase
	{
		// Token: 0x060039C4 RID: 14788 RVA: 0x000ECB07 File Offset: 0x000EAD07
		public ContactChangeLogger(StoreSession storeSession) : this(ContactChangeLogger.Logger.Member, storeSession)
		{
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000ECB1C File Offset: 0x000EAD1C
		public ContactChangeLogger(IExtensibleLogger logger, StoreSession storeSession) : base(logger)
		{
			ArgumentValidator.ThrowIfNull("storeSession", storeSession);
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession != null && mailboxSession.MailboxOwner.MailboxInfo.OrganizationId != null && mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit != null)
			{
				this.tenantName = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit.ToString();
			}
			this.mailboxGuid = storeSession.MailboxGuid;
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000ECBA0 File Offset: 0x000EADA0
		protected override string TenantName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000ECBA8 File Offset: 0x000EADA8
		protected override Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x04001EE4 RID: 7908
		private static readonly LazyMember<ExtensibleLogger> Logger = new LazyMember<ExtensibleLogger>(() => new ExtensibleLogger(ContactChangeLogConfiguration.Default));

		// Token: 0x04001EE5 RID: 7909
		private readonly string tenantName;

		// Token: 0x04001EE6 RID: 7910
		private readonly Guid mailboxGuid;
	}
}
