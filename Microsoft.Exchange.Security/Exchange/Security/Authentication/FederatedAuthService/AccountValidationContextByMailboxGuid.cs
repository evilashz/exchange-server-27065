using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200004E RID: 78
	internal class AccountValidationContextByMailboxGuid : AccountValidationContextBase
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000DC43 File Offset: 0x0000BE43
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000DC4B File Offset: 0x0000BE4B
		public Guid MailboxGuid { get; protected set; }

		// Token: 0x06000218 RID: 536 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public AccountValidationContextByMailboxGuid(Guid mailboxGuid, ExDateTime accountAuthTime) : this(mailboxGuid, null, accountAuthTime, null)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public AccountValidationContextByMailboxGuid(Guid mailboxGuid, ExDateTime accountAuthTime, string appName) : this(mailboxGuid, null, accountAuthTime, appName)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public AccountValidationContextByMailboxGuid(Guid mailboxGuid, OrganizationId orgId, ExDateTime accountAuthTime, string appName) : base(orgId, accountAuthTime, appName)
		{
			this.MailboxGuid = mailboxGuid;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000DC7F File Offset: 0x0000BE7F
		public override AccountState CheckAccount()
		{
			return base.CheckAccount();
		}
	}
}
