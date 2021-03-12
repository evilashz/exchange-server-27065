using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000430 RID: 1072
	public abstract class NewMailboxPolicyBase<T> : NewMultitenancySystemConfigurationObjectTask<T> where T : MailboxPolicy, new()
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x0009763C File Offset: 0x0009583C
		protected override IConfigurable PrepareDataObject()
		{
			T t = (T)((object)base.PrepareDataObject());
			t.SetId((IConfigurationSession)base.DataSession, base.Name);
			return t;
		}

		// Token: 0x04001D77 RID: 7543
		protected IList<T> existingDefaultPolicies;

		// Token: 0x04001D78 RID: 7544
		protected bool updateExistingDefaultPolicies;
	}
}
