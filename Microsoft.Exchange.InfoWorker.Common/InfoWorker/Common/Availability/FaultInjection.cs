using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F6 RID: 246
	internal static class FaultInjection
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public static Exception Callback(string exceptionType)
		{
			if (exceptionType != null)
			{
				foreach (FaultInjection.NameExceptionPair nameExceptionPair in FaultInjection.exceptions)
				{
					if (nameExceptionPair.Name.Contains(exceptionType))
					{
						return nameExceptionPair.Exception;
					}
				}
			}
			return null;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001D118 File Offset: 0x0001B318
		public static T TraceTest<T>(FaultInjection.LIDs faultLid)
		{
			T result = default(T);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<T>((uint)faultLid, ref result);
			return result;
		}

		// Token: 0x040003EC RID: 1004
		private static FaultInjection.NameExceptionPair[] exceptions = new FaultInjection.NameExceptionPair[]
		{
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(OverBudgetException).FullName,
				Exception = new OverBudgetException()
			},
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(VirusScanInProgressException).FullName,
				Exception = new VirusScanInProgressException(Strings.descVirusDetected("test"))
			},
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(ConnectionFailedTransientException).FullName,
				Exception = new ConnectionFailedTransientException(Strings.descMailboxLogonFailed)
			},
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(ConnectionFailedPermanentException).FullName,
				Exception = new ConnectionFailedPermanentException(Strings.descMailboxLogonFailed)
			},
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(WebException).FullName + "_" + WebExceptionStatus.Timeout,
				Exception = new WebException("test", WebExceptionStatus.Timeout)
			},
			new FaultInjection.NameExceptionPair
			{
				Name = typeof(WebException).FullName + "_" + WebExceptionStatus.Pending,
				Exception = new WebException("test", WebExceptionStatus.Pending)
			}
		};

		// Token: 0x020000F7 RID: 247
		internal enum LIDs : uint
		{
			// Token: 0x040003EE RID: 1006
			UseDestinationUserAddress = 2743479613U
		}

		// Token: 0x020000F8 RID: 248
		private class NameExceptionPair
		{
			// Token: 0x040003EF RID: 1007
			public string Name;

			// Token: 0x040003F0 RID: 1008
			public Exception Exception;
		}
	}
}
