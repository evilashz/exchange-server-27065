using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200015A RID: 346
	internal class HeavyBlockingOperation : IUMHeavyBlockingOperation
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0002BA99 File Offset: 0x00029C99
		internal HeavyBlockingOperation(ActivityManager manager, BaseUMCallSession vo, FsmAction action, TransitionBase originalTransition)
		{
			this.manager = manager;
			this.vo = vo;
			this.action = action;
			this.originalTransition = originalTransition;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002BAC0 File Offset: 0x00029CC0
		public void Execute()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Starting heavy blocking operation: {0}...", new object[]
			{
				this.action
			});
			this.autoEvent = this.action.Execute(this.manager, this.vo);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Completed heavy blocking operation: {0}.", new object[]
			{
				this.action
			});
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002BB2C File Offset: 0x00029D2C
		internal void CompleteHeavyBlockingOperation()
		{
			this.originalTransition.ProcessAutoEvent(this.manager, this.vo, this.autoEvent);
		}

		// Token: 0x04000948 RID: 2376
		private ActivityManager manager;

		// Token: 0x04000949 RID: 2377
		private BaseUMCallSession vo;

		// Token: 0x0400094A RID: 2378
		private FsmAction action;

		// Token: 0x0400094B RID: 2379
		private TransitionBase originalTransition;

		// Token: 0x0400094C RID: 2380
		private TransitionBase autoEvent;
	}
}
