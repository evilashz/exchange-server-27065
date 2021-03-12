using System;
using System.Threading;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200002E RID: 46
	internal class ApnsFeedbackScheduler
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00007568 File Offset: 0x00005768
		public virtual void ScheduleOnce(Action action, int dueTime)
		{
			Timer timer = null;
			timer = new Timer(delegate(object state)
			{
				action();
				timer.Dispose();
			});
			timer.Change(dueTime, -1);
		}

		// Token: 0x040000B5 RID: 181
		public static readonly ApnsFeedbackScheduler DefaultScheduler = new ApnsFeedbackScheduler();
	}
}
