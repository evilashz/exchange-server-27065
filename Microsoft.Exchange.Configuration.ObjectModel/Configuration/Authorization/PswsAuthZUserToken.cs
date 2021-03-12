using System;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000214 RID: 532
	internal class PswsAuthZUserToken : AuthZPluginUserToken
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0003BF11 File Offset: 0x0003A111
		internal PswsAuthZUserToken(DelegatedPrincipal delegatedPrincipal, ADRawEntry userEntry, Microsoft.Exchange.Configuration.Core.AuthenticationType authenticationType, string defaultUserName, string executingUserName) : base(delegatedPrincipal, userEntry, authenticationType, defaultUserName)
		{
			ExAssert.RetailAssert(!string.IsNullOrWhiteSpace(executingUserName), "The executingUserName should not be null or white space.");
			this.ExecutingUserName = executingUserName;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0003BF3A File Offset: 0x0003A13A
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x0003BF42 File Offset: 0x0003A142
		internal string ExecutingUserName { get; private set; }
	}
}
