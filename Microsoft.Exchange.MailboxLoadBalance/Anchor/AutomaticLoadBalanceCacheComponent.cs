using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance.Anchor
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutomaticLoadBalanceCacheComponent : CacheProcessorBase
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000035DC File Offset: 0x000017DC
		public AutomaticLoadBalanceCacheComponent(LoadBalanceAnchorContext context, WaitHandle stopEvent) : base(context, stopEvent)
		{
			this.settings = context.Settings;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000035F2 File Offset: 0x000017F2
		internal override string Name
		{
			get
			{
				return "AutomaticLoadBalancingProcessor";
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000035F9 File Offset: 0x000017F9
		internal override bool ShouldProcess()
		{
			return this.settings.AutomaticLoadBalancingEnabled && !this.settings.LoadBalanceBlocked;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003660 File Offset: 0x00001860
		internal override bool Process(JobCache data)
		{
			bool retry = false;
			base.Context.Logger.LogVerbose("Starting automatic load balancing.", new object[0]);
			LoadBalanceAnchorContext context = base.Context as LoadBalanceAnchorContext;
			if (context == null)
			{
				base.Context.Logger.LogError(null, "Context is null or not from an expected type.", new object[0]);
				return false;
			}
			if (!this.settings.AutomaticLoadBalancingEnabled)
			{
				base.Context.Logger.LogWarning("Automatic load balancing is no longer enabled.", new object[0]);
				return false;
			}
			foreach (CacheEntryBase cacheEntryBase in data.Get())
			{
				if (!cacheEntryBase.Validate())
				{
					base.Context.Logger.LogWarning("Invalid cache entry found. Skipped.", new object[0]);
				}
				else if (!cacheEntryBase.IsLocal || !cacheEntryBase.IsActive)
				{
					base.Context.Logger.LogWarning("Inactive or non local cache entry found. Skipped.", new object[0]);
				}
				else if (!this.settings.LoadBalanceBlocked)
				{
					CommonUtils.ProcessKnownExceptions(delegate
					{
						new AutomaticLoadBalancer(context).LoadBalanceForest();
					}, delegate(Exception exception)
					{
						retry = true;
						this.Context.Logger.LogError(exception, "Failed to load balance forest.", new object[0]);
						return true;
					});
				}
			}
			return retry;
		}

		// Token: 0x0400001F RID: 31
		private readonly ILoadBalanceSettings settings;
	}
}
