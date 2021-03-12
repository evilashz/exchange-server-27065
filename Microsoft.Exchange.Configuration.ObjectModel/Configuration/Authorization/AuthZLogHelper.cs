using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.Core.EventLog;
using Microsoft.Exchange.Configuration.ObjectModel.EventLog;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200021E RID: 542
	internal static class AuthZLogHelper
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x0003D37C File Offset: 0x0003B57C
		public static IScopedPerformanceMonitor[] AuthZPerfMonitors
		{
			get
			{
				return new IScopedPerformanceMonitor[]
				{
					new LatencyMonitor(AuthZLogHelper.latencyTracker)
				};
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0003D39E File Offset: 0x0003B59E
		public static LatencyTracker LantencyTracker
		{
			get
			{
				return AuthZLogHelper.latencyTracker;
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0003D3BC File Offset: 0x0003B5BC
		internal static void StartAndEndLoging(string funcName, Action action)
		{
			AuthZLogHelper.StartAndEndLoging<bool>(funcName, delegate()
			{
				action();
				return true;
			});
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0003D3EC File Offset: 0x0003B5EC
		internal static T StartAndEndLoging<T>(string funcName, Func<T> func)
		{
			bool flag = false;
			bool flag2 = false;
			T result;
			try
			{
				flag2 = AuthZLogHelper.StartLogging(funcName, out flag);
				result = func();
			}
			finally
			{
				if (flag2)
				{
					AuthZLogHelper.EndLogging(true);
				}
				else if (flag)
				{
					AuthZLogHelper.EndLogging(false);
				}
			}
			return result;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0003D450 File Offset: 0x0003B650
		internal static void ExecuteWSManPluginAPI(string funcName, bool throwException, bool trackLatency, Action action)
		{
			AuthZLogHelper.ExecuteWSManPluginAPI<bool>(funcName, throwException, trackLatency, false, delegate()
			{
				action();
				return false;
			});
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003D4C8 File Offset: 0x0003B6C8
		internal static T ExecuteWSManPluginAPI<T>(string funcName, bool throwException, bool trackLatency, T defaultReturnValue, Func<T> func)
		{
			ExWatson.IsExceptionInteresting isExceptionInteresting = null;
			T result;
			try
			{
				AuthZLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, funcName, DateTime.UtcNow.ToString());
				string funcName2 = funcName;
				bool throwException2 = throwException;
				LatencyTracker latencyTracker = trackLatency ? AuthZLogHelper.latencyTracker : null;
				ExEventLog rbacEventLogger = AuthZLogHelper.RbacEventLogger;
				ExEventLog.EventTuple tuple_RemotePSPublicAPIFailed = Microsoft.Exchange.Configuration.ObjectModel.EventLog.TaskEventLogConstants.Tuple_RemotePSPublicAPIFailed;
				Trace publicPluginAPITracer = Microsoft.Exchange.Diagnostics.Components.Authorization.ExTraceGlobals.PublicPluginAPITracer;
				if (isExceptionInteresting == null)
				{
					isExceptionInteresting = ((object ex) => AuthZPluginHelper.IsFatalException(ex as Exception));
				}
				result = Diagnostics.ExecuteAndLog<T>(funcName2, throwException2, latencyTracker, rbacEventLogger, tuple_RemotePSPublicAPIFailed, publicPluginAPITracer, isExceptionInteresting, delegate(Exception ex)
				{
					AuthZLogHelper.LogException(ex, funcName, throwException);
				}, defaultReturnValue, () => AuthZLogHelper.HandleExceptionAndRetry<T>(funcName, func, throwException, defaultReturnValue));
			}
			catch (Exception ex)
			{
				string arg = (AuthZLogger.ActivityScope != null) ? AuthZLogger.ActivityScope.ActivityId.ToString() : null;
				AuthZLogHelper.EndLogging(true);
				Exception ex3;
				string str = string.Format("[FailureCategory={0}] ", FailureCategory.AuthZ + "-" + ex3.GetType().Name);
				string str2 = string.Format("[AuthZRequestId={0}]", arg);
				LocalizedString message = new LocalizedString(str2 + str + ex3.Message);
				AuthorizationException ex2 = new AuthorizationException(message, ex3);
				throw ex2;
			}
			return result;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003D634 File Offset: 0x0003B834
		internal static bool StartLogging(string funcName)
		{
			bool flag = false;
			return AuthZLogHelper.StartLogging(funcName, out flag);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003D670 File Offset: 0x0003B870
		internal static bool StartLogging(string funcName, out bool latencyTrackerStartedByMe)
		{
			AuthZLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, funcName, DateTime.UtcNow.ToString());
			latencyTrackerStartedByMe = false;
			if (AuthZLogHelper.latencyTracker == null)
			{
				Diagnostics.ExecuteAndLog("AuthZLogHelper.StartLatencyTracker", false, null, Constants.CoreEventLogger, Microsoft.Exchange.Configuration.Core.EventLog.TaskEventLogConstants.Tuple_NonCrashingException, Microsoft.Exchange.Diagnostics.Components.Configuration.Core.ExTraceGlobals.InstrumentationTracer, null, delegate(Exception ex)
				{
					AuthZLogHelper.LogException(ex, "AuthZLogHelper.StartLatencyTracker", false);
				}, delegate()
				{
					AuthZLogHelper.StartLatencyTracker(funcName);
				});
				latencyTrackerStartedByMe = (AuthZLogHelper.latencyTracker != null);
			}
			if (AuthZLogger.LoggerNotDisposed)
			{
				return false;
			}
			InitializeLoggerSettingsHelper.InitLoggerSettings();
			AuthZLogger.InitializeRequestLogger();
			AuthZLogger.SafeSetLogger(RpsAuthZMetadata.Function, funcName);
			return true;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0003D736 File Offset: 0x0003B936
		internal static void EndLogging()
		{
			AuthZLogHelper.EndLogging(true);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0003D75C File Offset: 0x0003B95C
		internal static void EndLogging(bool shouldCommit)
		{
			Microsoft.Exchange.Diagnostics.Components.Configuration.Core.ExTraceGlobals.InstrumentationTracer.TraceDebug(0L, "[AuthZLogHelper.EndLogging] End logging.");
			try
			{
				if (AuthZLogHelper.latencyTracker != null)
				{
					long num = Diagnostics.ExecuteAndLog<long>("AuthZLogHelper.StopLatencyTracker", false, null, Constants.CoreEventLogger, Microsoft.Exchange.Configuration.Core.EventLog.TaskEventLogConstants.Tuple_NonCrashingException, Microsoft.Exchange.Diagnostics.Components.Configuration.Core.ExTraceGlobals.InstrumentationTracer, null, delegate(Exception ex)
					{
						AuthZLogHelper.LogException(ex, "AuthZLogHelper.StopLatencyTracker", false);
					}, -1L, new Func<long>(AuthZLogHelper.StopLatencyTracker));
					AuthZLogger.SafeSetLogger(ConfigurationCoreMetadata.TotalTime, num);
					AuthZLogHelper.latencyTracker.PushLatencyDetailsToLog(AuthZLogHelper.funcNameToLogMetadataDic, new Action<Enum, double>(AuthZLogger.UpdateLatency), delegate(string funcName, string totalLatency)
					{
						AuthZLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, funcName, totalLatency);
					});
				}
				else
				{
					AuthZLogger.SafeAppendColumn(RpsCommonMetadata.GenericLatency, "LatencyMissed", "AuthZLogHelper.latencyTracker is null");
				}
			}
			finally
			{
				try
				{
					if (shouldCommit)
					{
						AuthZLogger.AsyncCommit(true);
					}
				}
				finally
				{
					AuthZLogHelper.latencyTracker = null;
				}
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0003D867 File Offset: 0x0003BA67
		internal static void StartLatencyTracker(string funcName)
		{
			if (AuthZLogHelper.latencyTracker == null)
			{
				AuthZLogHelper.latencyTracker = new LatencyTracker(funcName, () => AuthZLogger.ActivityScope);
				AuthZLogHelper.latencyTracker.Start();
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003D8A2 File Offset: 0x0003BAA2
		internal static bool StartInternalTracking(string groupName, string funcName)
		{
			return AuthZLogHelper.latencyTracker != null && AuthZLogHelper.latencyTracker.StartInternalTracking(groupName, funcName, false);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003D8BA File Offset: 0x0003BABA
		internal static bool StartInternalTracking(string funcName)
		{
			return AuthZLogHelper.latencyTracker != null && AuthZLogHelper.latencyTracker.StartInternalTracking(funcName);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		internal static void EndInternalTracking(string groupName, string funcName)
		{
			if (AuthZLogHelper.latencyTracker == null)
			{
				return;
			}
			AuthZLogHelper.latencyTracker.EndInternalTracking(groupName, funcName);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0003D8E6 File Offset: 0x0003BAE6
		internal static void EndInternalTracking(string funcName)
		{
			if (AuthZLogHelper.latencyTracker == null)
			{
				return;
			}
			AuthZLogHelper.latencyTracker.EndInternalTracking(funcName);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0003D8FC File Offset: 0x0003BAFC
		internal static void LogAuthZUserToken(AuthZPluginUserToken userToken)
		{
			if (userToken == null)
			{
				AuthZLogger.SafeSetLogger(RpsAuthZMetadata.IsAuthorized, false);
				return;
			}
			AuthZLogger.SafeSetLogger(RpsAuthZMetadata.IsAuthorized, true);
			AuthZLogger.SafeSetLogger(ServiceCommonMetadata.AuthenticatedUser, userToken.UserNameForLogging);
			AuthZLogger.SafeSetLogger(ActivityStandardMetadata.AuthenticationType, userToken.AuthenticationType);
			AuthZLogger.SafeSetLogger(ActivityStandardMetadata.TenantId, userToken.OrgIdInString);
			if (userToken.DelegatedPrincipal != null)
			{
				AuthZLogger.SafeSetLogger(ConfigurationCoreMetadata.ManagedOrganization, "Delegate:" + userToken.DelegatedPrincipal.DelegatedOrganization);
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0003D98F File Offset: 0x0003BB8F
		private static void LogException(Exception ex, string funcName, bool throwException)
		{
			if (throwException)
			{
				AuthZLogger.SafeSetLogger(RpsAuthZMetadata.IsAuthorized, false);
			}
			AuthZLogger.SafeAppendGenericError(funcName, ex, new Func<Exception, bool>(KnownException.IsUnhandledException));
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003D9B8 File Offset: 0x0003BBB8
		private static long StopLatencyTracker()
		{
			long result = -1L;
			if (AuthZLogHelper.latencyTracker != null)
			{
				result = AuthZLogHelper.latencyTracker.Stop();
			}
			return result;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		private static T HandleExceptionAndRetry<T>(string methodName, Func<T> func, bool throwException, T defaultReturnValue)
		{
			for (int i = 0; i < 2; i++)
			{
				try
				{
					Microsoft.Exchange.Diagnostics.Components.Authorization.ExTraceGlobals.PublicPluginAPITracer.TraceDebug<string, int>(0L, "Retry function {0} the {1} times.", methodName, i);
					return func();
				}
				catch (Exception ex)
				{
					bool flag = ex is TransientException;
					bool flag2 = AuthZPluginHelper.IsFatalException(ex);
					bool flag3 = flag2 || AuthZLogHelper.ExceptionNoNeedToRetry(ex);
					Microsoft.Exchange.Diagnostics.Components.Authorization.ExTraceGlobals.PublicPluginAPITracer.TraceDebug(0L, "{0} caught Exception {1}. IsTransientException = {2}. IsFatalException = {3}. NoNeedToRetry = {4}.", new object[]
					{
						methodName,
						ex,
						flag,
						flag2,
						flag3
					});
					ExEventLog.EventTuple eventInfo = Microsoft.Exchange.Configuration.ObjectModel.EventLog.TaskEventLogConstants.Tuple_RBACUnavailable_UnknownError;
					if (flag)
					{
						eventInfo = Microsoft.Exchange.Configuration.ObjectModel.EventLog.TaskEventLogConstants.Tuple_RBACUnavailable_TransientError;
					}
					else if (flag2)
					{
						eventInfo = Microsoft.Exchange.Configuration.ObjectModel.EventLog.TaskEventLogConstants.Tuple_RBACUnavailable_FatalError;
					}
					TaskLogger.LogRbacEvent(eventInfo, null, new object[]
					{
						methodName,
						ex
					});
					if (flag3 || i == 1)
					{
						if (!(ex is ADTransientException) && (flag2 || throwException))
						{
							throw;
						}
						AuthZLogHelper.LogException(ex, methodName, false);
						break;
					}
					else
					{
						AuthZLogger.SafeAppendGenericInfo(methodName + "-" + ex.GetType().Name + "-Retried", ex.Message);
					}
				}
			}
			Microsoft.Exchange.Diagnostics.Components.Authorization.ExTraceGlobals.PublicPluginAPITracer.TraceError<string, T>(0L, "{0} returns default value {1}.", methodName, defaultReturnValue);
			return defaultReturnValue;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0003DB2C File Offset: 0x0003BD2C
		private static bool ExceptionNoNeedToRetry(Exception ex)
		{
			return ex is AuthorizationException;
		}

		// Token: 0x040004B4 RID: 1204
		private const int MaxRetryForADTransient = 2;

		// Token: 0x040004B5 RID: 1205
		public const string ExchangeRunspaceConfigurationGroupName = "ExchangeRunspaceConfiguration";

		// Token: 0x040004B6 RID: 1206
		public const string InitialSessionStateBuilderGroupName = "InitialSessionStateBuilder";

		// Token: 0x040004B7 RID: 1207
		[ThreadStatic]
		private static LatencyTracker latencyTracker;

		// Token: 0x040004B8 RID: 1208
		private static readonly ExEventLog RbacEventLogger = new ExEventLog(Microsoft.Exchange.Diagnostics.Components.Tasks.ExTraceGlobals.LogTracer.Category, "MSExchange RBAC");

		// Token: 0x040004B9 RID: 1209
		private static readonly Dictionary<string, Enum> funcNameToLogMetadataDic = new Dictionary<string, Enum>
		{
			{
				"AuthorizeUser",
				RpsAuthZMetadata.AuthorizeUser
			},
			{
				"AuthorizeOperation",
				RpsAuthZMetadata.AuthorizeOperation
			},
			{
				"GetQuota",
				RpsAuthZMetadata.GetQuota
			},
			{
				"WSManOperationComplete",
				RpsAuthZMetadata.WSManOperationComplete
			},
			{
				"WSManUserComplete",
				RpsAuthZMetadata.WSManUserComplete
			},
			{
				"WSManQuotaComplete",
				RpsAuthZMetadata.WSManQuotaComplete
			},
			{
				"ValidateConnectionLimit",
				RpsAuthZMetadata.ValidateConnectionLimit
			},
			{
				"GetApplicationPrivateData",
				RpsAuthZMetadata.GetApplicationPrivateData
			},
			{
				"GetInitialSessionState",
				RpsAuthZMetadata.GetInitialSessionState
			}
		};
	}
}
