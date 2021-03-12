using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000103 RID: 259
	internal static class HttpLogger
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		internal static bool LoggerNotDisposed
		{
			get
			{
				if (!LoggerSettings.LogEnabled)
				{
					return false;
				}
				if (LoggerSettings.IsPowerShellWebService)
				{
					PswsLogger pswsLogger = RequestDetailsLoggerBase<PswsLogger>.Current;
					return pswsLogger != null && !pswsLogger.IsDisposed;
				}
				RpsHttpLogger rpsHttpLogger = RequestDetailsLoggerBase<RpsHttpLogger>.Current;
				return rpsHttpLogger != null && !rpsHttpLogger.IsDisposed;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001DFEA File Offset: 0x0001C1EA
		internal static IActivityScope ActivityScope
		{
			get
			{
				if (!LoggerSettings.LogEnabled)
				{
					return null;
				}
				if (LoggerSettings.IsPowerShellWebService)
				{
					if (RequestDetailsLoggerBase<PswsLogger>.Current != null)
					{
						return RequestDetailsLoggerBase<PswsLogger>.Current.ActivityScope;
					}
					return null;
				}
				else
				{
					if (RequestDetailsLoggerBase<RpsHttpLogger>.Current != null)
					{
						return RequestDetailsLoggerBase<RpsHttpLogger>.Current.ActivityScope;
					}
					return null;
				}
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001E023 File Offset: 0x0001C223
		internal static void InitializeRequestLogger()
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				RequestDetailsLoggerBase<PswsLogger>.InitializeRequestLogger();
				return;
			}
			RequestDetailsLoggerBase<RpsHttpLogger>.InitializeRequestLogger();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001E041 File Offset: 0x0001C241
		internal static void SafeSetLogger(Enum key, object value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeSetLogger(RequestDetailsLoggerBase<PswsLogger>.Current, key, value);
				return;
			}
			RequestDetailsLoggerBase<RpsHttpLogger>.SafeSetLogger(RequestDetailsLoggerBase<RpsHttpLogger>.Current, key, value);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001E06B File Offset: 0x0001C26B
		internal static void SafeAppendGenericError(string key, Exception ex, Func<Exception, bool> funcToVerifyException)
		{
			HttpLogger.SafeAppendGenericError(key, (ex == null) ? null : ex.ToString(), funcToVerifyException(ex));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001E088 File Offset: 0x0001C288
		internal static void SafeAppendGenericError(string key, string value, bool isUnhandledException)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			string genericErrorKey = Diagnostics.GetGenericErrorKey(key, isUnhandledException);
			if (LoggerSettings.IsPowerShellWebService)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendGenericError(RequestDetailsLoggerBase<PswsLogger>.Current, genericErrorKey, value);
				return;
			}
			RequestDetailsLoggerBase<RpsHttpLogger>.SafeAppendGenericError(RequestDetailsLoggerBase<RpsHttpLogger>.Current, genericErrorKey, value);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001E0C5 File Offset: 0x0001C2C5
		internal static void AsyncCommit(bool forceSync)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				if (RequestDetailsLoggerBase<PswsLogger>.Current != null)
				{
					RequestDetailsLoggerBase<PswsLogger>.Current.AsyncCommit(forceSync);
					return;
				}
			}
			else if (RequestDetailsLoggerBase<RpsHttpLogger>.Current != null)
			{
				RequestDetailsLoggerBase<RpsHttpLogger>.Current.AsyncCommit(forceSync);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001E0FB File Offset: 0x0001C2FB
		internal static void SafeAppendGenericInfo(string key, string value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendGenericInfo(RequestDetailsLoggerBase<PswsLogger>.Current, key, value);
				return;
			}
			RequestDetailsLoggerBase<RpsHttpLogger>.SafeAppendGenericInfo(RequestDetailsLoggerBase<RpsHttpLogger>.Current, key, value);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001E125 File Offset: 0x0001C325
		internal static void SafeAppendColumn(Enum column, string key, string value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendColumn(RequestDetailsLoggerBase<PswsLogger>.Current, column, key, value);
				return;
			}
			RequestDetailsLoggerBase<RpsHttpLogger>.SafeAppendColumn(RequestDetailsLoggerBase<RpsHttpLogger>.Current, column, key, value);
		}
	}
}
