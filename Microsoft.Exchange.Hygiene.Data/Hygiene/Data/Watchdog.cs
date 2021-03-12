using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AB RID: 171
	internal sealed class Watchdog
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00013020 File Offset: 0x00011220
		public static void Invoke(TimeSpan timeout, Action action)
		{
			Watchdog.Invoke<bool>(timeout, delegate()
			{
				action();
				return true;
			});
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001304D File Offset: 0x0001124D
		public static T Invoke<T>(TimeSpan timeout, Func<T> action)
		{
			return action();
		}
	}
}
