using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200004A RID: 74
	internal sealed class UserEwsCallingContext : UserServiceCallingContext<ExchangeServiceBinding>
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x00017234 File Offset: 0x00015434
		public UserEwsCallingContext(ICredentialHandler credentialHandler, string userRole, IDictionary<string, ICredentials> cachedCredentials = null) : base(credentialHandler, cachedCredentials)
		{
			this.userRole = userRole;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00017248 File Offset: 0x00015448
		public override void SetServiceApiContext(ExchangeServiceBinding binding, string mailboxEmailAddress)
		{
			base.SetServiceApiContext(binding, mailboxEmailAddress);
			if (!string.IsNullOrEmpty(this.userRole))
			{
				binding.ManagementRole = new ManagementRoleType
				{
					UserRoles = new string[]
					{
						this.userRole
					}
				};
			}
		}

		// Token: 0x040001C1 RID: 449
		private readonly string userRole;
	}
}
