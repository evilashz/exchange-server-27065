using System;
using System.Diagnostics;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security;
using System.Threading;
using System.Transactions;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000038 RID: 56
	internal static class RetryHelper
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public static bool IsSystemFatal(Exception ex)
		{
			return ex is AccessViolationException || ex is AppDomainUnloadedException || ex is BadImageFormatException || ex is DataMisalignedException || ex is InsufficientExecutionStackException || ex is InvalidOperationException || ex is MemberAccessException || ex is OutOfMemoryException || ex is StackOverflowException || ex is TypeInitializationException || ex is TypeLoadException || ex is TypeUnloadedException || ex is UnauthorizedAccessException || ex is ThreadAbortException || ex is SecurityTokenException || ex is InternalBufferOverflowException || ex is SecurityException;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B980 File Offset: 0x00009B80
		public static bool IsFatalDefect(Exception ex)
		{
			return ex is ArgumentException || ex is ArithmeticException || ex is FormatException || ex is IndexOutOfRangeException || ex is InvalidCastException || ex is NotImplementedException || ex is NotSupportedException || ex is NullReferenceException;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public static T Invoke<T>(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Func<T> action, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException)
		{
			return RetryHelper.Invoke<T>(slowResponseTime, sleepInterval, maxRetryCount, action, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, RetryHelper.DefaultIsStoreUnavailablePredicate);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public static T Invoke<T>(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Func<T> action, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException)
		{
			int num = 0;
			T result = default(T);
			try
			{
				bool flag;
				do
				{
					flag = false;
					result = RetryHelper.InvokeOnce<T>(ref num, maxRetryCount, slowResponseTime, sleepInterval, action, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out flag);
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

		// Token: 0x060002A0 RID: 672 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public static void Invoke(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Action retryableAction, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException)
		{
			RetryHelper.Invoke(slowResponseTime, sleepInterval, maxRetryCount, retryableAction, isRetriableException, onRetry, onMaxRetry, onSlowResponse, onUnhandledException, RetryHelper.DefaultIsStoreUnavailablePredicate);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000BA74 File Offset: 0x00009C74
		public static void Invoke(TimeSpan slowResponseTime, TimeSpan sleepInterval, int maxRetryCount, Action retryableAction, Predicate<Exception> isRetriableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, Action<Exception> onUnhandledException, Predicate<Exception> isDataStoreUnavailableException)
		{
			int num = 0;
			try
			{
				bool flag;
				do
				{
					flag = false;
					RetryHelper.InvokeOnce(ref num, maxRetryCount, slowResponseTime, sleepInterval, retryableAction, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out flag);
				}
				while (flag);
			}
			catch (Exception obj)
			{
				onUnhandledException(obj);
				throw;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		private static T InvokeOnce<T>(ref int retryCount, int maxRetryCount, TimeSpan slowResponseTime, TimeSpan sleepInterval, Func<T> action, Predicate<Exception> isRetriableException, Predicate<Exception> isDataStoreUnavailableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, out bool needsRetry)
		{
			T result = default(T);
			needsRetry = false;
			RetryHelper.InvokeOnce(ref retryCount, maxRetryCount, slowResponseTime, sleepInterval, delegate()
			{
				result = action();
			}, isRetriableException, isDataStoreUnavailableException, onRetry, onMaxRetry, onSlowResponse, out needsRetry);
			return result;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000BB2C File Offset: 0x00009D2C
		private static void InvokeOnce(ref int retryCount, int maxRetryCount, TimeSpan slowResponseTime, TimeSpan sleepInterval, Action action, Predicate<Exception> isRetriableException, Predicate<Exception> isDataStoreUnavailableException, Action<Exception, int> onRetry, Action<Exception, int> onMaxRetry, Action<TimeSpan> onSlowResponse, out bool needsRetry)
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
					throw new PermanentDALException(Strings.ErrorPermanentDALException, ex);
				}
				if (Transaction.Current != null)
				{
					throw new TransientDALException(Strings.ErrorTransientDALExceptionAmbientTransaction, ex);
				}
				if (retryCount >= maxRetryCount)
				{
					onMaxRetry(ex, maxRetryCount);
					if (isDataStoreUnavailableException(ex))
					{
						throw new TransientDataProviderUnavailableException(Strings.ErrorDataStoreUnavailable, ex);
					}
					throw new TransientDALException(Strings.ErrorTransientDALExceptionMaxRetries, ex);
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

		// Token: 0x0400016B RID: 363
		private static readonly Predicate<Exception> DefaultIsStoreUnavailablePredicate = (Exception isDataStoreUnavailable) => false;
	}
}
