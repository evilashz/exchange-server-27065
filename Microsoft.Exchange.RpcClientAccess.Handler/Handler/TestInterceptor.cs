using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class TestInterceptor
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x00011392 File Offset: 0x0000F592
		public static void DisableConditions()
		{
			TestInterceptor.location = TestInterceptorLocation.None;
			TestInterceptor.exceptionToThrow = null;
			TestInterceptor.countCondition = null;
			TestInterceptor.callback = null;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000113AC File Offset: 0x0000F5AC
		public static void EnableThrowOnCondition(TestInterceptorLocation location, Exception exceptionToThrow)
		{
			Util.ThrowOnNullArgument(exceptionToThrow, "exceptionToThrow");
			TestInterceptor.location = location;
			TestInterceptor.exceptionToThrow = exceptionToThrow;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000113C5 File Offset: 0x0000F5C5
		public static void CallbackOnCondition(TestInterceptorLocation location, Action<object[]> callback)
		{
			Util.ThrowOnNullArgument(callback, "callback");
			TestInterceptor.location = location;
			TestInterceptor.callback = callback;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000113DE File Offset: 0x0000F5DE
		public static void OverrideOnCondition<T>(TestInterceptorLocation location, T newValue) where T : class
		{
			Util.ThrowOnNullArgument(newValue, "newValue");
			TestInterceptor.location = location;
			TestInterceptor.newValue = newValue;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00011401 File Offset: 0x0000F601
		public static void WaitForCountCondition(TestInterceptorLocation location, Semaphore countCondition)
		{
			TestInterceptor.location = location;
			TestInterceptor.countCondition = countCondition;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00011410 File Offset: 0x0000F610
		public static void Intercept(TestInterceptorLocation location, params object[] states)
		{
			if ((TestInterceptor.location & location) != location)
			{
				return;
			}
			if (TestInterceptor.exceptionToThrow != null)
			{
				throw TestInterceptor.exceptionToThrow;
			}
			if (TestInterceptor.countCondition != null)
			{
				TestInterceptor.countCondition.WaitOne();
				return;
			}
			if (TestInterceptor.callback != null)
			{
				TestInterceptor.callback(states);
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00011460 File Offset: 0x0000F660
		public static void InterceptValue<T>(TestInterceptorLocation location, ref T overrideValue)
		{
			if ((TestInterceptor.location & location) == location && TestInterceptor.newValue != null)
			{
				overrideValue = (T)((object)TestInterceptor.newValue);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00011483 File Offset: 0x0000F683
		public static Semaphore CountCondition
		{
			get
			{
				return TestInterceptor.countCondition;
			}
		}

		// Token: 0x04000098 RID: 152
		private static Exception exceptionToThrow = null;

		// Token: 0x04000099 RID: 153
		private static Action<object[]> callback = null;

		// Token: 0x0400009A RID: 154
		private static TestInterceptorLocation location;

		// Token: 0x0400009B RID: 155
		private static object newValue = null;

		// Token: 0x0400009C RID: 156
		private static Semaphore countCondition = null;
	}
}
