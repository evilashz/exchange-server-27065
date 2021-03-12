using System;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200003A RID: 58
	internal class UcmaAsyncTimer : BaseUMAsyncTimer
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000AF06 File Offset: 0x00009106
		public UcmaAsyncTimer(BaseUMCallSession session, BaseUMAsyncTimer.UMTimerCallback callback, int dueTime) : base(session, callback, dueTime)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AF14 File Offset: 0x00009114
		internal override void TimerExpired(object state)
		{
			UcmaCallSession session = (UcmaCallSession)state;
			if (base.IsActive)
			{
				base.Callback(session);
			}
		}
	}
}
