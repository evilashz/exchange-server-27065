using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000039 RID: 57
	internal static class AmExceptionHelper
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ClusterTracer;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C4E1 File Offset: 0x0000A6E1
		public static string GetExceptionMessageOrNoneString(Exception ex)
		{
			return AmExceptionHelper.GetExceptionGenericStringOrNoneString(ex, (Exception exception) => exception.Message);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C50E File Offset: 0x0000A70E
		public static string GetExceptionToStringOrNoneString(Exception ex)
		{
			return AmExceptionHelper.GetExceptionGenericStringOrNoneString(ex, (Exception exception) => exception.ToString());
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000C534 File Offset: 0x0000A734
		public static string GetExceptionGenericStringOrNoneString(Exception ex, Func<Exception, string> stringExtractor)
		{
			string message = (ex != null) ? stringExtractor(ex) : Strings.NoErrorSpecified;
			return AmExceptionHelper.GetMessageOrNoneString(message);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000C560 File Offset: 0x0000A760
		public static string GetMessageOrNoneString(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				message = Strings.NoErrorSpecified;
			}
			return message;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000C577 File Offset: 0x0000A777
		public static uint Win32ErrorCodeFromHresult(uint hresult)
		{
			if ((hresult & 4294901760U) == 2147942400U || (hresult & 4294901760U) == 2147549184U)
			{
				return hresult & 65535U;
			}
			return hresult;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		internal static bool IsKnownClusterException(object task, Exception e)
		{
			if (e is DataSourceTransientException || e is DataSourceOperationException || e is ClusterException || e is TransientException || e is SecurityException || e is UnauthorizedAccessException || e is IOException)
			{
				ExTraceGlobals.CmdletsTracer.TraceError<string>((long)task.GetHashCode(), task.ToString() + " got exception : {0}", e.Message);
				return true;
			}
			return false;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000C60F File Offset: 0x0000A80F
		internal static bool IsImmediateClusRetryException(Exception e)
		{
			return true;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000C614 File Offset: 0x0000A814
		internal static uint Win32ErrorCodeFromComException(COMException comException)
		{
			uint errorCode = (uint)comException.ErrorCode;
			return AmExceptionHelper.Win32ErrorCodeFromHresult(errorCode);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000C62E File Offset: 0x0000A82E
		internal static bool IsRetryableClusterResourceException(Exception e)
		{
			return true;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000C634 File Offset: 0x0000A834
		internal static bool CheckExceptionCode(Exception e, uint errCode)
		{
			if (e == null)
			{
				return false;
			}
			COMException ex = e as COMException;
			if (ex != null && ((ulong)errCode == (ulong)((long)(ex.ErrorCode & 65535)) || errCode == (uint)ex.ErrorCode))
			{
				return true;
			}
			ex = (e.InnerException as COMException);
			if (ex != null && ((ulong)errCode == (ulong)((long)(ex.ErrorCode & 65535)) || errCode == (uint)ex.ErrorCode))
			{
				return true;
			}
			Win32Exception ex2 = e as Win32Exception;
			if (ex2 != null)
			{
				if (ex2.NativeErrorCode == (int)errCode)
				{
					return true;
				}
			}
			else
			{
				ex2 = (e.InnerException as Win32Exception);
				if (ex2 != null && ex2.NativeErrorCode == (int)errCode)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		internal static ClusterApiException ConstructClusterApiException(int errorCode, string methodName, params object[] methodParameters)
		{
			Win32Exception ex = new Win32Exception(errorCode);
			return new ClusterApiException(Strings.ClusterApiErrorMessage(string.Format(methodName, methodParameters), errorCode, ex.Message), ex);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		internal static ClusterApiException ConstructClusterApiExceptionNoErr(string methodName, params object[] methodParameters)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			return AmExceptionHelper.ConstructClusterApiException(lastWin32Error, methodName, methodParameters);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000C718 File Offset: 0x0000A918
		internal static TException HandleSpecificException<TException>(Action operation) where TException : Exception
		{
			TException result = default(TException);
			try
			{
				operation();
			}
			catch (TException ex)
			{
				TException ex2 = (TException)((object)ex);
				result = ex2;
			}
			return result;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C750 File Offset: 0x0000A950
		internal static void HandleRetryIoPendingExceptions(COMException e, ref int count)
		{
			if (AmExceptionHelper.CheckExceptionCode(e, 997U))
			{
				throw new ClusCommonTaskPendingException(e.Message, e);
			}
			AmExceptionHelper.HandleRetryExceptions(e, ref count, null);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C774 File Offset: 0x0000A974
		internal static void HandleRetryExceptions(Exception e, ref int count, IAmCluster cluster)
		{
			AmExceptionHelper.HandleRetryExceptions(e, ref count, false, cluster, null);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000C794 File Offset: 0x0000A994
		internal static void HandleRetryExceptions(Exception e, ref int count, bool dbAccess, IAmCluster cluster)
		{
			AmExceptionHelper.HandleRetryExceptions(e, ref count, dbAccess, cluster, null);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		internal static void HandleRetryExceptions(Exception e, ref int count, bool dbAccess, IAmCluster cluster, int? sleepIntervalMilliSecs)
		{
			if (!AmExceptionHelper.IsImmediateClusRetryException(e))
			{
				if (!AmExceptionHelper.IsRetryableClusterResourceException(e))
				{
					throw new ClusCommonFailException(e.Message, e);
				}
				if (dbAccess)
				{
					throw new ClusterDatabaseTransientException(e.Message, e);
				}
				throw new ClusCommonNonRetryableTransientException(e.Message, e);
			}
			else
			{
				AmExceptionHelper.CheckExceptionCode(e, 2147549448U);
				if (count++ < 3)
				{
					if (sleepIntervalMilliSecs != null)
					{
						Thread.Sleep(sleepIntervalMilliSecs.Value);
					}
					return;
				}
				AmExceptionHelper.Tracer.TraceDebug<string, int>(0L, "HandleRetry throwing error {0}, after {1} retries", e.Message, count);
				if (dbAccess)
				{
					throw new ClusterDatabaseTransientException(e.Message, e);
				}
				throw new ClusCommonRetryableTransientException(e.Message, e);
			}
		}

		// Token: 0x040000C6 RID: 198
		internal const int RetryTimes = 3;
	}
}
