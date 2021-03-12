using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security;
using System.Threading;
using System.Transactions;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A0 RID: 160
	internal static class RetryHelper
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0001209C File Offset: 0x0001029C
		public static bool IsSystemFatal(Exception ex)
		{
			return ex is AccessViolationException || ex is AppDomainUnloadedException || ex is BadImageFormatException || ex is DataMisalignedException || ex is InsufficientExecutionStackException || ex is InvalidOperationException || ex is MemberAccessException || ex is OutOfMemoryException || ex is StackOverflowException || ex is TypeInitializationException || ex is TypeLoadException || ex is TypeUnloadedException || ex is UnauthorizedAccessException || ex is ThreadAbortException || ex is SecurityTokenException || ex is InternalBufferOverflowException || ex is SecurityException;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012138 File Offset: 0x00010338
		public static bool IsFatalDefect(Exception ex)
		{
			return ex is ArgumentException || ex is ArithmeticException || ex is FormatException || ex is IndexOutOfRangeException || ex is InvalidCastException || ex is NotImplementedException || ex is NotSupportedException || ex is NullReferenceException;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00012188 File Offset: 0x00010388
		public static T Invoke<T>(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Func<T> action, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException)
		{
			return RetryHelper.Invoke<T>(slowResponseTime, sleepInterval, maxRetryCount, action, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, RetryHelper.DefaultIsStoreUnavailablePredicate, null);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000121B0 File Offset: 0x000103B0
		public static T Invoke<T>(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Func<T> action, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException)
		{
			return RetryHelper.Invoke<T>(slowResponseTime, sleepInterval, maxRetryCount, action, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, isDataStoreUnavailableException, null);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000121D4 File Offset: 0x000103D4
		public static T Invoke<T>(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Func<T> action, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException, Action<TimeSpan> onSuccess)
		{
			int num = 0;
			T result = default(T);
			try
			{
				bool flag;
				do
				{
					flag = false;
					result = RetryHelper.InvokeOnce<T>(ref num, maxRetryCount, slowResponseTime, sleepInterval, action, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out flag, onSuccess);
				}
				while (flag);
			}
			catch (Exception obj)
			{
				onUnhandledException(obj);
				throw;
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012228 File Offset: 0x00010428
		public static void Invoke(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Action retryableAction, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException)
		{
			RetryHelper.Invoke(slowResponseTime, sleepInterval, maxRetryCount, retryableAction, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, RetryHelper.DefaultIsStoreUnavailablePredicate, null);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012250 File Offset: 0x00010450
		public static void Invoke(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Action retryableAction, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException)
		{
			RetryHelper.Invoke(slowResponseTime, sleepInterval, maxRetryCount, retryableAction, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, isDataStoreUnavailableException, null);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012274 File Offset: 0x00010474
		public static void Invoke(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Action retryableAction, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException, Action<TimeSpan> onSuccess)
		{
			int num = 0;
			try
			{
				bool flag;
				do
				{
					flag = false;
					RetryHelper.InvokeOnce(ref num, maxRetryCount, slowResponseTime, sleepInterval, retryableAction, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out flag, onSuccess);
				}
				while (flag);
			}
			catch (Exception obj)
			{
				onUnhandledException(obj);
				throw;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000122DC File Offset: 0x000104DC
		private static T InvokeOnce<T>(ref int retryCount, int maxRetryCount, TimeSpan slowResponseTime, TimeSpan sleepInterval, Func<T> action, Predicate<Exception> isRetriableException, Predicate<Exception> isDataStoreUnavailableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, out bool needsRetry, Action<TimeSpan> onSuccess)
		{
			T result = default(T);
			needsRetry = false;
			RetryHelper.InvokeOnce(ref retryCount, maxRetryCount, slowResponseTime, sleepInterval, delegate()
			{
				result = action();
			}, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out needsRetry, onSuccess);
			return result;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012330 File Offset: 0x00010530
		private static void InvokeOnce(ref int retryCount, int maxRetryCount, TimeSpan slowResponseTime, TimeSpan sleepInterval, Action action, Predicate<Exception> isRetriableException, Predicate<Exception> isDataStoreUnavailableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, out bool needsRetry, Action<TimeSpan> onSuccess)
		{
			needsRetry = false;
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				action();
				stopwatch.Stop();
				if (stopwatch.Elapsed > slowResponseTime)
				{
					onSlowResponse(stopwatch.Elapsed);
				}
				if (onSuccess != null)
				{
					onSuccess(stopwatch.Elapsed);
				}
			}
			catch (Exception ex)
			{
				if (RetryHelper.IsSystemFatal(ex) || RetryHelper.IsFatalDefect(ex))
				{
					throw;
				}
				if (ex is TransientDALException || ex is PermanentDALException)
				{
					throw;
				}
				if (!isRetriableException(ex))
				{
					throw new PermanentDALException(HygieneDataStrings.ErrorPermanentDALException, ex);
				}
				if (Transaction.Current != null)
				{
					throw new TransientDALException(HygieneDataStrings.ErrorTransientDALExceptionAmbientTransaction, ex);
				}
				if (retryCount >= maxRetryCount)
				{
					onMaxRetry(ex, maxRetryCount);
					if (isDataStoreUnavailableException(ex))
					{
						throw new TransientDataProviderUnavailableException(HygieneDataStrings.ErrorDataStoreUnavailable, ex);
					}
					throw new TransientDALException(HygieneDataStrings.ErrorTransientDALExceptionMaxRetries, ex);
				}
				else
				{
					retryCount++;
					onRetry(ex, retryCount);
					Thread.Sleep(sleepInterval);
					needsRetry = true;
				}
			}
		}

		// Token: 0x04000364 RID: 868
		private static readonly Predicate<Exception> DefaultIsStoreUnavailablePredicate = (Exception isDataStoreUnavailable) => false;
	}
}
