using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F1 RID: 241
	internal static class AuthZLogger
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BD14 File Offset: 0x00019F14
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
				RpsAuthZLogger rpsAuthZLogger = RequestDetailsLoggerBase<RpsAuthZLogger>.Current;
				return rpsAuthZLogger != null && !rpsAuthZLogger.IsDisposed;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001BD5A File Offset: 0x00019F5A
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
					if (RequestDetailsLoggerBase<RpsAuthZLogger>.Current != null)
					{
						return RequestDetailsLoggerBase<RpsAuthZLogger>.Current.ActivityScope;
					}
					return null;
				}
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001BD94 File Offset: 0x00019F94
		internal static void InitializeRequestLogger()
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsRemotePS)
			{
				IActivityScope activityScope = null;
				if (!ActivityContext.IsStarted)
				{
					ActivityContext.ClearThreadScope();
					activityScope = ActivityContext.Start(null);
				}
				RequestDetailsLoggerBase<RpsAuthZLogger>.InitializeRequestLogger(activityScope ?? ActivityContext.GetCurrentActivityScope());
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001BDD5 File Offset: 0x00019FD5
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
			RequestDetailsLoggerBase<RpsAuthZLogger>.SafeSetLogger(RequestDetailsLoggerBase<RpsAuthZLogger>.Current, key, value);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001BDFF File Offset: 0x00019FFF
		internal static void SafeAppendGenericError(string key, Exception ex, Func<Exception, bool> funcToVerifyException)
		{
			AuthZLogger.SafeAppendGenericError(key, (ex == null) ? null : ex.ToString(), funcToVerifyException(ex));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001BE1C File Offset: 0x0001A01C
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
			RequestDetailsLoggerBase<RpsAuthZLogger>.SafeAppendGenericError(RequestDetailsLoggerBase<RpsAuthZLogger>.Current, genericErrorKey, value);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001BE59 File Offset: 0x0001A059
		internal static void AsyncCommit(bool forceSync)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsRemotePS && RequestDetailsLoggerBase<RpsAuthZLogger>.Current != null)
			{
				RequestDetailsLoggerBase<RpsAuthZLogger>.Current.AsyncCommit(forceSync);
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001BE7C File Offset: 0x0001A07C
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
			RequestDetailsLoggerBase<RpsAuthZLogger>.SafeAppendGenericInfo(RequestDetailsLoggerBase<RpsAuthZLogger>.Current, key, value);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001BEA6 File Offset: 0x0001A0A6
		internal static void UpdateLatency(Enum latencyMetadata, double latencyInMilliseconds)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerSettings.IsPowerShellWebService)
			{
				if (RequestDetailsLoggerBase<PswsLogger>.Current != null)
				{
					RequestDetailsLoggerBase<PswsLogger>.Current.UpdateLatency(latencyMetadata, latencyInMilliseconds);
					return;
				}
			}
			else if (RequestDetailsLoggerBase<RpsAuthZLogger>.Current != null)
			{
				RequestDetailsLoggerBase<RpsAuthZLogger>.Current.UpdateLatency(latencyMetadata, latencyInMilliseconds);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001BEDE File Offset: 0x0001A0DE
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
			RequestDetailsLoggerBase<RpsAuthZLogger>.SafeAppendColumn(RequestDetailsLoggerBase<RpsAuthZLogger>.Current, column, key, value);
		}
	}
}
