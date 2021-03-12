using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200004F RID: 79
	internal class AccountValidationContextByPUID : AccountValidationContextBase
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000DC87 File Offset: 0x0000BE87
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000DC8F File Offset: 0x0000BE8F
		public string PUID { get; protected set; }

		// Token: 0x0600021E RID: 542 RVA: 0x0000DC98 File Offset: 0x0000BE98
		public AccountValidationContextByPUID(string puid, ExDateTime accountAuthTime) : this(puid, null, accountAuthTime, null)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		public AccountValidationContextByPUID(string puid, ExDateTime accountAuthTime, string appName) : this(puid, null, accountAuthTime, appName)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
		public AccountValidationContextByPUID(string puid, OrganizationId orgId, ExDateTime accountAuthTime, string appName) : base(orgId, accountAuthTime, appName)
		{
			if (string.IsNullOrEmpty(puid))
			{
				throw new ArgumentNullException("PUID");
			}
			this.PUID = puid;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		public override AccountState CheckAccount()
		{
			return base.CheckAccount();
		}
	}
}
