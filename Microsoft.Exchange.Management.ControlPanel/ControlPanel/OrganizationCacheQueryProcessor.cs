using System;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200025B RID: 603
	internal class OrganizationCacheQueryProcessor<T> : RbacQuery.RbacQueryProcessor, INamedQueryProcessor
	{
		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x00080C84 File Offset: 0x0007EE84
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x00080C8C File Offset: 0x0007EE8C
		public string Name { get; private set; }

		// Token: 0x060028DB RID: 10459 RVA: 0x00080C98 File Offset: 0x0007EE98
		public OrganizationCacheQueryProcessor(string roleName, string key, Func<T, bool> predicate = null)
		{
			if (string.IsNullOrEmpty(roleName))
			{
				throw new ArgumentNullException("roleName");
			}
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			this.Name = roleName;
			this.key = key;
			this.predicate = predicate;
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x060028DC RID: 10460 RVA: 0x00080CE6 File Offset: 0x0007EEE6
		public sealed override bool CanCache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00080CEC File Offset: 0x0007EEEC
		public sealed override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			T t;
			bool flag = OrganizationCache.TryGetValue<T>(this.key, out t);
			bool value = flag && ((this.predicate != null) ? this.predicate(t) : ((bool)((object)t)));
			return new bool?(value);
		}

		// Token: 0x0400209A RID: 8346
		public static readonly OrganizationCacheQueryProcessor<bool> XPremiseEnt = new OrganizationCacheQueryProcessor<bool>("XPremiseEnt", "EntHasTargetDeliveryDomain", null);

		// Token: 0x0400209B RID: 8347
		public static readonly OrganizationCacheQueryProcessor<bool> XPremiseDC = new OrganizationCacheQueryProcessor<bool>("XPremiseDC", "DCIsDirSyncRunning", null);

		// Token: 0x0400209C RID: 8348
		private readonly string key;

		// Token: 0x0400209D RID: 8349
		private readonly Func<T, bool> predicate;
	}
}
