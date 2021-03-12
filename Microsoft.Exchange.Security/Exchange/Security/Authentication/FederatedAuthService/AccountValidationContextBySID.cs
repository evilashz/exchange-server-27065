using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000050 RID: 80
	internal class AccountValidationContextBySID : AccountValidationContextBase
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public SecurityIdentifier SID { get; protected set; }

		// Token: 0x06000224 RID: 548 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		public AccountValidationContextBySID(SecurityIdentifier sid, ExDateTime accountAuthTime) : this(sid, null, accountAuthTime, null)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		public AccountValidationContextBySID(SecurityIdentifier sid, ExDateTime accountAuthTime, string appName) : this(sid, null, accountAuthTime, appName)
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DD07 File Offset: 0x0000BF07
		public AccountValidationContextBySID(SecurityIdentifier sid, OrganizationId orgId, ExDateTime accountAuthTime, string appName) : base(orgId, accountAuthTime, appName)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("SID");
			}
			this.SID = sid;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000DD2E File Offset: 0x0000BF2E
		public override AccountState CheckAccount()
		{
			return base.CheckAccount();
		}
	}
}
