using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B1 RID: 177
	internal static class ServiceDiagnostics
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00016DFC File Offset: 0x00014FFC
		internal static ExEventLog Logger
		{
			get
			{
				return ServiceDiagnostics.logger.Value;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00016E08 File Offset: 0x00015008
		internal static bool LogExceptionWithTrace(ExEventLog.EventTuple tuple, string periodicKey, Microsoft.Exchange.Diagnostics.Trace tagTracer, object thisObject, string traceFormat, Exception exception)
		{
			tagTracer.TraceError<Exception>((long)((thisObject == null) ? 0 : thisObject.GetHashCode()), traceFormat, exception);
			string text = exception.ToString();
			if (text.Length > 32000)
			{
				text = text.Substring(0, 2000) + "...\n" + text.Substring(text.Length - 20000, 20000);
			}
			return ServiceDiagnostics.Logger.LogEvent(tuple, periodicKey, new object[]
			{
				text
			});
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00016E87 File Offset: 0x00015087
		internal static bool LogEventWithTrace(ExEventLog.EventTuple tuple, string periodicKey, Microsoft.Exchange.Diagnostics.Trace tagTracer, object thisObject, string traceFormat, params object[] messageArgs)
		{
			tagTracer.TraceDebug((long)((thisObject == null) ? 0 : thisObject.GetHashCode()), traceFormat, messageArgs);
			return ServiceDiagnostics.Logger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00016EAE File Offset: 0x000150AE
		[Conditional("DEBUG")]
		internal static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00016EB0 File Offset: 0x000150B0
		[Conditional("DEBUG")]
		internal static void Assert(bool condition)
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00016EB2 File Offset: 0x000150B2
		internal static bool IsNonEmptyArray(Array array)
		{
			return array != null && array.Length != 0;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00016EC5 File Offset: 0x000150C5
		internal static object HandleNullObjectTrace(object objectToDisplay)
		{
			return objectToDisplay ?? "Object is null";
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016F50 File Offset: 0x00015150
		internal static void SendWatsonReportOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ServiceDiagnostics.RegisterAdditionalWatsonData();
			try
			{
				ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
				{
					bool flag = ServiceDiagnostics.sendWatsonReportsEnabled;
					Exception ex = exception as Exception;
					if (ex != null)
					{
						ExTraceGlobals.ExceptionTracer.TraceError<Exception>(0L, "Encountered unhandled exception: {0}", ex);
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, ex, "ServiceDiagnostics_SendWatsonReportOnUnhandledException");
						flag = ServiceDiagnostics.IsSendReportValid(ex);
						if (flag)
						{
							ServiceDiagnostics.LogExceptionWithTrace(ServicesEventLogConstants.Tuple_InternalServerError, null, ExTraceGlobals.ExceptionTracer, null, "Encountered unhandled exception: {0}", ex);
							ExWatson.SetWatsonReportAlreadySent(ex);
						}
					}
					ExTraceGlobals.ExceptionTracer.TraceError<bool>(0L, "SendWatsonReportOnUnhandledException isSendReportValid: {0}", flag);
					return flag;
				}, ReportOptions.None);
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
				throw new BailOutException();
			}
			finally
			{
				ExWatson.ClearReportActions(WatsonActionScope.Thread);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00017001 File Offset: 0x00015201
		internal static void TraceErrorOnUnhandledException(ExWatson.MethodDelegate methodDelegate)
		{
			ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
			{
				ExTraceGlobals.ExceptionTracer.TraceError(0L, "Unhandled exception, Exception details: {0}", new object[]
				{
					exception
				});
				Exception ex = exception as Exception;
				if (ex != null)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, ex, "ServiceDiagnostics_TraceErrorOnUnhandledException");
				}
				return false;
			});
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00017100 File Offset: 0x00015300
		internal static void SendWatsonReportOnUnhandledExceptionIfRequired(ExWatson.MethodDelegate methodDelegate)
		{
			ServiceDiagnostics.RegisterAdditionalWatsonData();
			try
			{
				ExWatson.SendReportOnUnhandledException(methodDelegate, delegate(object exception)
				{
					bool flag = ServiceDiagnostics.sendWatsonReportsEnabled;
					Exception ex = exception as Exception;
					if (ex != null)
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, ex, "ServiceDiagnostics_SendWatsonReportOnUnhandledExceptionIfRequired");
						flag = ServiceDiagnostics.IsSendReportValid(ex);
						if (ex.Data != null)
						{
							if (ex.Data.Contains("NeverGenerateWatson"))
							{
								flag = false;
							}
							else
							{
								LocalizedException ex2 = ex as LocalizedException;
								if (ex2 != null)
								{
									if (ex2 is ServicePermanentException)
									{
										flag = false;
									}
									else
									{
										ExceptionMappingBase exceptionMapping = ServiceErrors.GetExceptionMapper().GetExceptionMapping(ex2);
										if (!exceptionMapping.ReportException)
										{
											flag = false;
										}
									}
								}
							}
							ExWatson.SetWatsonReportAlreadySent(ex);
							ex.Data["DisposeServiceCommmandTag"] = null;
						}
						if (flag)
						{
							ServiceDiagnostics.LogExceptionWithTrace(ServicesEventLogConstants.Tuple_InternalServerError, null, ExTraceGlobals.ExceptionTracer, null, "Encountered unhandled exception: {0}", ex);
						}
						ExTraceGlobals.ExceptionTracer.TraceError<bool, string, string>(0L, "[ServiceDiagnostics::SendWatsonReportOnUnhandledExceptionIfRequired] Encountered unhandled exception.  Watson generated? {0}, Class: '{1}', Message: '{2}'", flag, ex.GetType().FullName, ex.Message);
					}
					return flag;
				}, ReportOptions.None);
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
				throw new BailOutException();
			}
			finally
			{
				ExWatson.ClearReportActions(WatsonActionScope.Thread);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001716C File Offset: 0x0001536C
		internal static bool DoesExceptionDataContainTag(Exception exception, string dataTag)
		{
			while (exception != null)
			{
				if (exception.Data != null && exception.Data.Contains(dataTag))
				{
					return true;
				}
				exception = exception.InnerException;
			}
			return false;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00017194 File Offset: 0x00015394
		internal static void InitializeWatsonReporting(bool sendWatsonReports, bool filterExceptionsFromWatsonReporting)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<bool, bool>(0L, "sendWatsonReports: {0} filterExceptionsFromWatsonReporting: {1}", sendWatsonReports, filterExceptionsFromWatsonReporting);
			ExWatson.Register(ExEnvironment.IsTest ? "E12" : "E12IIS");
			ServiceDiagnostics.sendWatsonReportsEnabled = sendWatsonReports;
			ServiceDiagnostics.watsonReportsFiltered = filterExceptionsFromWatsonReporting;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000171D0 File Offset: 0x000153D0
		internal static void ReportException(Exception exception, ExEventLog.EventTuple eventTuple, object eventObject, string traceFormat)
		{
			bool flag = exception is AccessViolationException;
			if (exception != null)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(RequestDetailsLogger.Current, exception, "ServiceDiagnostics_ReportException");
			}
			if (ServiceDiagnostics.IsSendReportValid(exception))
			{
				ServiceDiagnostics.LogExceptionWithTrace(eventTuple, null, ExTraceGlobals.ExceptionTracer, eventObject, traceFormat, exception);
				ExWatson.HandleException(new UnhandledExceptionEventArgs(exception, flag), ReportOptions.None);
				ExWatson.SetWatsonReportAlreadySent(exception);
			}
			else
			{
				ExTraceGlobals.ExceptionTracer.TraceError<Exception>(0L, traceFormat, exception);
			}
			if (flag)
			{
				Environment.FailFast(traceFormat, exception);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001723F File Offset: 0x0001543F
		private static bool IsFaultInjectionException(Exception exception)
		{
			return exception != null && exception.StackTrace != null && exception.StackTrace.Contains("Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException");
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00017260 File Offset: 0x00015460
		private static bool IsSendReportValid(Exception exception)
		{
			if (ServiceDiagnostics.IsFaultInjectionException(exception))
			{
				return false;
			}
			if (ExWatson.IsWatsonReportAlreadySent(exception))
			{
				return false;
			}
			bool flag = ServiceDiagnostics.sendWatsonReportsEnabled;
			if (flag && ServiceDiagnostics.watsonReportsFiltered)
			{
				if (exception is CommunicationException)
				{
					flag = false;
				}
				else if (exception is HttpException)
				{
					flag = false;
				}
				else if (!ServiceDiagnostics.ReportTimeoutException && exception is TimeoutException)
				{
					flag = false;
				}
				else if (exception is BailOutException)
				{
					flag = false;
				}
				else if (exception is System.ServiceModel.QuotaExceededException)
				{
					flag = false;
				}
				else if (exception is DataValidationException)
				{
					flag = false;
				}
				else if (exception is DataSourceOperationException)
				{
					flag = false;
				}
				else if (exception is StoragePermanentException || exception is StorageTransientException)
				{
					flag = false;
				}
				else if (exception is IOException)
				{
					flag = false;
				}
				else if (exception is OutOfMemoryException)
				{
					flag = false;
				}
				else if (exception is ADTransientException)
				{
					flag = false;
				}
				else if (exception.GetType().FullName == "System.ServiceModel.Diagnostics.PlainXmlWriter+MaxSizeExceededException")
				{
					flag = false;
				}
				else if (exception is FaultException)
				{
					FaultException ex = (FaultException)exception;
					if (EWSSettings.IsFromEwsAssemblies(exception.Source))
					{
						flag = false;
					}
					else if (EWSSettings.FaultExceptionDueToAuthorizationManager)
					{
						flag = false;
					}
					else if (ex.Code != null && ex.Code.SubCode != null)
					{
						if (ex.Code.SubCode.Name == "DestinationUnreachable")
						{
							flag = false;
						}
						else if (ex.Code.SubCode.Name == "ActionNotSupported")
						{
							flag = false;
						}
					}
				}
				else if ((exception is UriFormatException || exception is XmlException) && !EWSSettings.IsFromEwsAssemblies(exception.Source))
				{
					flag = false;
				}
			}
			ExTraceGlobals.ExceptionTracer.TraceDebug<bool>(0L, "IsSendReportValid isSendReportValid: {0}", flag);
			return flag;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001741C File Offset: 0x0001561C
		internal static string BuildFullExceptionTrace(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (exception != null)
			{
				stringBuilder.AppendFormat("Exception Class: \"{0}\", Message: \"{1}\"\n", exception.GetType().FullName, exception.Message);
				exception = exception.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00017460 File Offset: 0x00015660
		internal static void RegisterAdditionalWatsonData()
		{
			string watsonExtraData = ServiceDiagnostics.GetWatsonExtraData();
			ExWatson.ClearReportActions(WatsonActionScope.Thread);
			ExWatson.RegisterReportAction(new WatsonExtraDataReportAction(watsonExtraData), WatsonActionScope.Thread);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00017488 File Offset: 0x00015688
		private static string GetWatsonExtraData()
		{
			return string.Format("[TID:{0};Action:{1};RC:{2};Caller:{3}]", new object[]
			{
				EWSSettings.RequestThreadId,
				(CallContext.Current == null || CallContext.Current.SoapAction == null) ? "<NULL>" : CallContext.Current.SoapAction,
				EWSSettings.RequestCorrelation,
				(CallContext.Current == null || CallContext.Current.Budget == null) ? "<NULL>" : CallContext.Current.Budget.Owner.ToString()
			});
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00017519 File Offset: 0x00015719
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00017520 File Offset: 0x00015720
		internal static bool ReportTimeoutException
		{
			get
			{
				return ServiceDiagnostics.reportTimeoutException;
			}
			set
			{
				ServiceDiagnostics.reportTimeoutException = value;
			}
		}

		// Token: 0x0400064F RID: 1615
		private const string DisposeServiceCommmandTag = "DisposeServiceCommmandTag";

		// Token: 0x04000650 RID: 1616
		private const string FaultInjectionFrame = "Microsoft.Exchange.Diagnostics.FaultInjection.FaultInjectionTrace.InjectException";

		// Token: 0x04000651 RID: 1617
		private const string EventSource = "MSExchange Web Services";

		// Token: 0x04000652 RID: 1618
		internal const string NeverGenerateWatsonCommmandTag = "NeverGenerateWatson";

		// Token: 0x04000653 RID: 1619
		private static Lazy<ExEventLog> logger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.PerformanceMonitorTracer.Category, "MSExchange Web Services"));

		// Token: 0x04000654 RID: 1620
		private static bool sendWatsonReportsEnabled = true;

		// Token: 0x04000655 RID: 1621
		private static bool watsonReportsFiltered = true;

		// Token: 0x04000656 RID: 1622
		private static bool reportTimeoutException = true;
	}
}
