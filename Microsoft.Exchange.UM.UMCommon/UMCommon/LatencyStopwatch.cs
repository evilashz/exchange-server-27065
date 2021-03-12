using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B9 RID: 185
	internal class LatencyStopwatch
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0001907C File Offset: 0x0001727C
		private void StopSuccess(Stopwatch stopwatch, string operationName)
		{
			stopwatch.Stop();
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "{0} [latency {1}ms]", new object[]
			{
				operationName,
				stopwatch.ElapsedMilliseconds
			});
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000190BC File Offset: 0x000172BC
		private void StopFailure(Stopwatch stopwatch, string operationName, Exception ex)
		{
			stopwatch.Stop();
			CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, null, "{0} [latency {1}ms] (exception {2})", new object[]
			{
				operationName,
				stopwatch.ElapsedMilliseconds,
				ex.GetType().Name + " : " + ex.Message
			});
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00019118 File Offset: 0x00017318
		public T Invoke<T>(string operationName, Func<T> func)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			T result;
			try
			{
				T t = func();
				this.StopSuccess(stopwatch, operationName);
				result = t;
			}
			catch (Exception ex)
			{
				this.StopFailure(stopwatch, operationName, ex);
				throw;
			}
			return result;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001915C File Offset: 0x0001735C
		public void Invoke(string operationName, Action func)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				func();
				this.StopSuccess(stopwatch, operationName);
			}
			catch (Exception ex)
			{
				this.StopFailure(stopwatch, operationName, ex);
				throw;
			}
		}
	}
}
