using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001F2 RID: 498
	internal class ActivityScopeThreadGuard : DisposeTrackableBase
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003CB1B File Offset: 0x0003AD1B
		public ActivityScopeThreadGuard(IActivityScope scope)
		{
			if (scope != null)
			{
				this.originalScope = ActivityContext.GetCurrentActivityScope();
				if (!object.ReferenceEquals(this.originalScope, scope))
				{
					ActivityContext.SetThreadScope(scope);
				}
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003CB45 File Offset: 0x0003AD45
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityScopeThreadGuard>(this);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003CB4D File Offset: 0x0003AD4D
		protected override void InternalDispose(bool disposing)
		{
			if (!base.IsDisposed && this.originalScope != null)
			{
				ActivityContext.SetThreadScope(this.originalScope);
			}
		}

		// Token: 0x04000A8C RID: 2700
		private IActivityScope originalScope;
	}
}
