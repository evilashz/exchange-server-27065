using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050C RID: 1292
	public static class MailboxPlanResolverExtensions
	{
		// Token: 0x06003E09 RID: 15881 RVA: 0x000BA624 File Offset: 0x000B8824
		public static MailboxPlanResolverRow ResolveMailboxPlan(ADObjectId mailboxPlan)
		{
			List<ADObjectId> list = new List<ADObjectId>();
			list.Add(mailboxPlan);
			return MailboxPlanResolver.Instance.ResolveObjects(list).FirstOrDefault<MailboxPlanResolverRow>();
		}
	}
}
