using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000F5 RID: 245
	internal static class CmdletLogger
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001C0E5 File Offset: 0x0001A2E5
		internal static void SafeSetLogger(Enum key, object value)
		{
			CmdletLogger.SafeSetLogger(Guid.Empty, key, value);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		internal static void SafeSetLogger(Guid cmdletUniqueId, Enum key, object value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerHelper.IsPswsNormalRequest)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeSetLogger(RequestDetailsLoggerBase<PswsLogger>.Current, key, value);
				return;
			}
			RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
			if (rpsCmdletLoggerBuffer != null)
			{
				rpsCmdletLoggerBuffer.AddMetadataLog(key, value);
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001C12F File Offset: 0x0001A32F
		internal static void SafeAppendGenericError(string key, Exception ex, Func<Exception, bool> funcToVerifyException)
		{
			CmdletLogger.SafeAppendGenericError(Guid.Empty, key, ex, funcToVerifyException);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001C13E File Offset: 0x0001A33E
		internal static void SafeAppendGenericError(Guid cmdletUniqueId, string key, Exception ex, Func<Exception, bool> funcToVerifyException)
		{
			CmdletLogger.SafeAppendGenericError(cmdletUniqueId, key, (ex == null) ? null : ex.ToString(), funcToVerifyException(ex));
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001C15A File Offset: 0x0001A35A
		internal static void SafeAppendGenericError(string key, string value, bool isUnhandledException)
		{
			CmdletLogger.SafeAppendGenericError(Guid.Empty, key, value, isUnhandledException);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001C16C File Offset: 0x0001A36C
		internal static void SafeAppendGenericError(Guid cmdletUniqueId, string key, string value, bool isUnhandledException)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			string genericErrorKey = Diagnostics.GetGenericErrorKey(key, isUnhandledException);
			if (LoggerHelper.IsPswsNormalRequest)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendGenericError(RequestDetailsLoggerBase<PswsLogger>.Current, genericErrorKey, value);
				return;
			}
			RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
			if (rpsCmdletLoggerBuffer != null)
			{
				rpsCmdletLoggerBuffer.AppendGenericError(genericErrorKey, value);
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001C1AF File Offset: 0x0001A3AF
		internal static void SafeAppendGenericInfo(string key, string value)
		{
			CmdletLogger.SafeAppendGenericInfo(Guid.Empty, key, value);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		internal static void SafeAppendGenericInfo(Guid cmdletUniqueId, string key, string value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerHelper.IsPswsNormalRequest)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendGenericInfo(RequestDetailsLoggerBase<PswsLogger>.Current, key, value);
				return;
			}
			RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
			if (rpsCmdletLoggerBuffer != null)
			{
				rpsCmdletLoggerBuffer.AppendGenericInfo(key, value);
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001C1FB File Offset: 0x0001A3FB
		internal static void UpdateLatency(Enum latencyMetadata, double latencyInMilliseconds)
		{
			CmdletLogger.UpdateLatency(Guid.Empty, latencyMetadata, latencyInMilliseconds);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001C20C File Offset: 0x0001A40C
		internal static void UpdateLatency(Guid cmdletUniqueId, Enum latencyMetadata, double latencyInMilliseconds)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerHelper.IsPswsNormalRequest)
			{
				if (RequestDetailsLoggerBase<PswsLogger>.Current != null)
				{
					RequestDetailsLoggerBase<PswsLogger>.Current.UpdateLatency(latencyMetadata, latencyInMilliseconds);
					return;
				}
			}
			else
			{
				RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
				if (rpsCmdletLoggerBuffer != null)
				{
					rpsCmdletLoggerBuffer.UpdateLatency(latencyMetadata, latencyInMilliseconds);
				}
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001C24E File Offset: 0x0001A44E
		internal static void SafeAppendColumn(Enum column, string key, string value)
		{
			CmdletLogger.SafeAppendColumn(Guid.Empty, column, key, value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001C260 File Offset: 0x0001A460
		internal static void SafeAppendColumn(Guid cmdletUniqueId, Enum column, string key, string value)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (LoggerHelper.IsPswsNormalRequest)
			{
				RequestDetailsLoggerBase<PswsLogger>.SafeAppendColumn(RequestDetailsLoggerBase<PswsLogger>.Current, column, key, value);
				return;
			}
			RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
			if (rpsCmdletLoggerBuffer != null)
			{
				rpsCmdletLoggerBuffer.AppendColumn(column, key, value);
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
		internal static void AsyncCommit(Guid cmdletUniqueId, bool forceSync)
		{
			if (!LoggerSettings.LogEnabled)
			{
				return;
			}
			if (!LoggerHelper.IsPswsNormalRequest)
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope == null)
				{
					throw new ArgumentException("activityScope is null.");
				}
				RequestDetailsLoggerBase<RpsCmdletLogger>.InitializeRequestLogger(currentActivityScope);
				RpsCmdletLogger rpsCmdletLogger = RequestDetailsLoggerBase<RpsCmdletLogger>.Current;
				if (rpsCmdletLogger == null)
				{
					return;
				}
				rpsCmdletLogger.EndActivityContext = false;
				RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer = RpsCmdletLoggerBuffer.Get(cmdletUniqueId);
				foreach (KeyValuePair<Enum, object> keyValuePair in rpsCmdletLoggerBuffer.MetadataLogCache)
				{
					RequestDetailsLoggerBase<RpsCmdletLogger>.SafeSetLogger(rpsCmdletLogger, keyValuePair.Key, keyValuePair.Value);
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in rpsCmdletLoggerBuffer.GenericInfoLogCache)
				{
					RequestDetailsLoggerBase<RpsCmdletLogger>.SafeAppendColumn(rpsCmdletLogger, RpsCmdletMetadata.GenericInfo, keyValuePair2.Key, keyValuePair2.Value);
				}
				foreach (KeyValuePair<string, string> keyValuePair3 in rpsCmdletLoggerBuffer.GenericErrorLogCache)
				{
					RequestDetailsLoggerBase<RpsCmdletLogger>.SafeAppendColumn(rpsCmdletLogger, RpsCmdletMetadata.GenericErrors, keyValuePair3.Key, keyValuePair3.Value);
				}
				foreach (KeyValuePair<Enum, Dictionary<string, string>> keyValuePair4 in rpsCmdletLoggerBuffer.GenericColumnLogCache)
				{
					foreach (KeyValuePair<string, string> keyValuePair5 in keyValuePair4.Value)
					{
						RequestDetailsLoggerBase<RpsCmdletLogger>.SafeAppendColumn(rpsCmdletLogger, keyValuePair4.Key, keyValuePair5.Key, keyValuePair5.Value);
					}
				}
				foreach (KeyValuePair<Enum, double> keyValuePair6 in rpsCmdletLoggerBuffer.LatencyLogCache)
				{
					rpsCmdletLogger.UpdateLatency(keyValuePair6.Key, keyValuePair6.Value);
				}
				rpsCmdletLogger.AsyncCommit(forceSync);
				foreach (KeyValuePair<Enum, object> keyValuePair7 in rpsCmdletLoggerBuffer.MetadataLogCache)
				{
					currentActivityScope.SetProperty(keyValuePair7.Key, null);
				}
				foreach (KeyValuePair<Enum, double> keyValuePair8 in rpsCmdletLoggerBuffer.LatencyLogCache)
				{
					currentActivityScope.SetProperty(keyValuePair8.Key, null);
				}
				foreach (KeyValuePair<Enum, Dictionary<string, string>> keyValuePair9 in rpsCmdletLoggerBuffer.GenericColumnLogCache)
				{
					currentActivityScope.SetProperty(keyValuePair9.Key, null);
				}
				currentActivityScope.SetProperty(RpsCmdletMetadata.GenericInfo, null);
				currentActivityScope.SetProperty(RpsCmdletMetadata.GenericErrors, null);
				rpsCmdletLoggerBuffer.Reset();
				CmdletStaticDataWithUniqueId<RpsCmdletLoggerBuffer>.Remove(cmdletUniqueId);
			}
		}
	}
}
