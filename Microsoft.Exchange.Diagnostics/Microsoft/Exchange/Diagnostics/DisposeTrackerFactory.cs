using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000EF RID: 239
	public static class DisposeTrackerFactory
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001BCED File Offset: 0x00019EED
		public static void Register(DisposeTrackerFactory.DisposeTrackerFactoryDelegate factoryDelegate)
		{
			DisposeTrackerFactory.factoryDelegate = factoryDelegate;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001BCF5 File Offset: 0x00019EF5
		public static DisposeTracker Get(IDisposable obj)
		{
			if (DisposeTrackerFactory.factoryDelegate == null)
			{
				return DisposeTrackerNullObject.Instance;
			}
			return DisposeTrackerFactory.factoryDelegate(obj);
		}

		// Token: 0x04000483 RID: 1155
		private static DisposeTrackerFactory.DisposeTrackerFactoryDelegate factoryDelegate;

		// Token: 0x020000F0 RID: 240
		// (Invoke) Token: 0x060006C7 RID: 1735
		public delegate DisposeTracker DisposeTrackerFactoryDelegate(IDisposable obj);
	}
}
