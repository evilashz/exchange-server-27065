using System;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200002E RID: 46
	internal abstract class ThrottleGovernor : Governor
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00007373 File Offset: 0x00005573
		public ThrottleGovernor(Governor parentGovernor, Throttle throttle) : base(parentGovernor)
		{
			this.throttle = throttle;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007383 File Offset: 0x00005583
		public Throttle Throttle
		{
			get
			{
				return this.throttle;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000738C File Offset: 0x0000558C
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableThrottleGovernor queryableThrottleGovernor = queryableObject as QueryableThrottleGovernor;
			if (queryableThrottleGovernor != null)
			{
				QueryableThrottle queryableObject2 = new QueryableThrottle();
				this.throttle.ExportToQueryableObject(queryableObject2);
				queryableThrottleGovernor.Throttle = queryableObject2;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000073C3 File Offset: 0x000055C3
		protected override void Run()
		{
			this.throttle.OpenThrottle();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000073D0 File Offset: 0x000055D0
		protected override void Retry()
		{
			this.throttle.SetThrottle(1);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000073DE File Offset: 0x000055DE
		protected override void OnFailure()
		{
			this.throttle.CloseThrottle();
		}

		// Token: 0x04000145 RID: 325
		private Throttle throttle;
	}
}
