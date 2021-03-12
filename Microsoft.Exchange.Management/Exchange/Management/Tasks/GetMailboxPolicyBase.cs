﻿using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000435 RID: 1077
	public abstract class GetMailboxPolicyBase<T> : GetDeepSearchMailboxPolicyBase<MailboxPolicyIdParameter, T> where T : MailboxPolicy, new()
	{
	}
}
