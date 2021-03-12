using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200002F RID: 47
	internal sealed class ProtocolLog
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00005C4C File Offset: 0x00003E4C
		private ProtocolLog()
		{
			ProtocolLogConfiguration protocolLogConfiguration = Configuration.ProtocolLogConfiguration;
			this.schema = new LogSchema(protocolLogConfiguration.SoftwareName, protocolLogConfiguration.SoftwareVersion, protocolLogConfiguration.LogTypeName, ProtocolLog.GetColumnArray());
			this.log = new Log(protocolLogConfiguration.LogFilePrefix, new LogHeaderFormatter(this.schema), protocolLogConfiguration.LogComponent);
			this.Refresh();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005CAF File Offset: 0x00003EAF
		public static void Initialize()
		{
			ProtocolLog.instance = new ProtocolLog();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005CBB File Offset: 0x00003EBB
		public static void Shutdown()
		{
			if (ProtocolLog.instance == null)
			{
				return;
			}
			ProtocolLog.instance.Close();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005CCF File Offset: 0x00003ECF
		public static void Referesh()
		{
			if (ProtocolLog.instance == null)
			{
				return;
			}
			ProtocolLog.instance.Refresh();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005CE3 File Offset: 0x00003EE3
		internal static ProtocolLogSession CreateNewSession()
		{
			return ProtocolLog.CreateNewSession(false);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005CEB File Offset: 0x00003EEB
		internal static ProtocolLogSession CreateNewSession(bool lazyInitializeRpcTimers)
		{
			if (ProtocolLog.instance == null)
			{
				return null;
			}
			return ProtocolLog.instance.InternalCreateNewSession(lazyInitializeRpcTimers);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005D01 File Offset: 0x00003F01
		internal LogRowFormatter CreateRowFormatter()
		{
			return new LogRowFormatter(this.schema);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005D0E File Offset: 0x00003F0E
		internal void Append(LogRowFormatter row)
		{
			if (!this.isEnabled)
			{
				return;
			}
			this.log.Append(row, 0);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005D26 File Offset: 0x00003F26
		public static void SetConnectionParameters(int connectionId, string user, MapiVersion version, IPAddress clientIpAddress, IPAddress serverIpAddress, string protocolSequence)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetConnectionParameters(connectionId, user, version, clientIpAddress, serverIpAddress, protocolSequence);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005D42 File Offset: 0x00003F42
		public static void SetHttpParameters(IList<string> sessionCookies, IList<string> requestIds)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetHttpParameters(sessionCookies, requestIds);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005D58 File Offset: 0x00003F58
		public static void SetApplicationParameters(ClientMode clientMode, string clientProcessName)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetApplicationParameters(clientMode, clientProcessName);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005D6E File Offset: 0x00003F6E
		public static void SetOrganizationInfo(string organizationInfo)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetOrganizationInfo(organizationInfo);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005D83 File Offset: 0x00003F83
		public static void SetClientConnectionInfo(string connectionInfo)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetClientConnectionInfo(connectionInfo);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005D98 File Offset: 0x00003F98
		public static void SetClientIpAddress(IPAddress clientIpAddress)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetClientIpAddress(clientIpAddress);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005DAD File Offset: 0x00003FAD
		public static void SetActivityData()
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalSetActivityData();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005DC1 File Offset: 0x00003FC1
		public static void LogConnect(SecurityIdentifier sid, ConnectionFlags connectionFlags)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogConnect(sid, connectionFlags);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public static void LogDisconnect(DisconnectReason disconnectReason)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogDisconnect(disconnectReason);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005DEC File Offset: 0x00003FEC
		public static void LogLogonPending(ProtocolLogLogonType logonType, ExchangePrincipal targetMailbox, string applicationId)
		{
			bool flag = ProtocolLog.IsLogEnabled || ExTraceGlobals.LogonTracer.IsTraceEnabled(TraceType.InfoTrace);
			string text = null;
			if (flag)
			{
				text = ProtocolLogSession.GenerateLogonOperationSpecificData(logonType, targetMailbox);
			}
			ExTraceGlobals.LogonTracer.TraceInformation(0, Activity.TraceId, text);
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogLogon(logonType, text, applicationId);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005E42 File Offset: 0x00004042
		public static void LogLogonSuccess(int logonId)
		{
			ExTraceGlobals.LogonTracer.TraceInformation<int>(0, Activity.TraceId, "Logon successful. LogonId = {0}", logonId);
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogLogonSuccess(logonId);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005E6D File Offset: 0x0000406D
		public static void LogLogonRedirect(string reason, string suggestedNewServer)
		{
			ExTraceGlobals.LogonTracer.TraceWarning<string, string>(0, Activity.TraceId, "Redirecting a client: {0}, suggested new server: {1}", reason, suggestedNewServer);
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogLogonRedirect(reason, suggestedNewServer);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005E9A File Offset: 0x0000409A
		public static void LogLogoff(ProtocolLogLogonType logonType, int logonId)
		{
			ExTraceGlobals.LogonTracer.TraceInformation<int>(0, Activity.TraceId, "Logon has been released. LogonId = {0}", logonId);
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogLogoff(logonType, logonId);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005EC6 File Offset: 0x000040C6
		public static void LogNewCall()
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogNewCall();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005EDA File Offset: 0x000040DA
		public static void LogInputRops(IEnumerable<RopId> rops)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogInputRops(rops);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005EEF File Offset: 0x000040EF
		public static void LogOutputRop(RopId ropId, ErrorCode errorCode)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogOutputRop(ropId, errorCode);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005F08 File Offset: 0x00004108
		public static void LogRopFailure(RopId ropId, bool isWarning, bool shouldLog, ErrorCode errorCode, Exception exception)
		{
			if (isWarning)
			{
				ExTraceGlobals.FailedRopTracer.TraceWarning<RopId, ErrorCode, string>(0, Activity.TraceId, "{0} | {1} | {2}", ropId, errorCode, (exception != null) ? exception.Message : null);
				if (shouldLog && ProtocolLog.IsLogEnabled)
				{
					ProtocolLog.LogWarning("{0} succeeded with warning ec={1} \"{2}\"", new object[]
					{
						ropId,
						errorCode,
						exception
					});
				}
			}
			else
			{
				ExTraceGlobals.FailedRopTracer.TraceError<RopId, ErrorCode, string>(0, Activity.TraceId, "{0} | {1} | {2}", ropId, errorCode, (exception != null) ? exception.Message : null);
				if (shouldLog && ProtocolLog.IsLogEnabled)
				{
					ProtocolLog.instance.InternalLogFailure(ProtocolLogFailureLevel.RopHandler, string.Format("{0} (rop::{1})", (int)errorCode, errorCode), ropId, exception);
				}
			}
			if (exception != null)
			{
				ExTraceGlobals.FailedRopTracer.TraceDebug<Exception>(0, Activity.TraceId, "{0}", exception);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005FE4 File Offset: 0x000041E4
		public static void LogRpcFailure(bool shouldLog, RpcErrorCode errorCode, Exception exception)
		{
			ExTraceGlobals.FailedRpcTracer.TraceError<RpcErrorCode, string>(0, Activity.TraceId, "{0} | {1}", errorCode, (exception != null) ? exception.Message : "(no exception)");
			if (exception != null)
			{
				ExTraceGlobals.FailedRpcTracer.TraceDebug<Exception>(0, Activity.TraceId, "RPC Error: {0}", exception);
			}
			if (shouldLog && ProtocolLog.IsLogEnabled)
			{
				ProtocolLog.instance.InternalLogFailure(ProtocolLogFailureLevel.RpcDispatch, string.Format("{0} (rpc::{1})", (uint)errorCode, errorCode), RopId.None, exception);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006060 File Offset: 0x00004260
		public static void LogRpcException(RpcServiceException exception)
		{
			ExTraceGlobals.FailedRpcTracer.TraceError<string, int>(0L, "Raising an RPC exception: {0}. Status = {1:X4}", exception.Message, exception.RpcStatus);
			if (exception != null)
			{
				ExTraceGlobals.FailedRpcTracer.TraceDebug<RpcServiceException>(0, Activity.TraceId, "RPC Exception: {0}", exception);
			}
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogFailure(ProtocolLogFailureLevel.RpcEndPoint, string.Format("0x{0:X} (rpc::Exception)", exception.RpcStatus), RopId.None, exception);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000060D0 File Offset: 0x000042D0
		public static void LogWebServiceFailure(string status, string message, Exception exception, string emailAddress, string organizationInfo, string protocolSequence, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			ProtocolLog.LogProtocolFailure(ProtocolLogFailureLevel.WebServiceEndPoint, null, null, status, message, exception, emailAddress, organizationInfo, protocolSequence, null, trace);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000060F0 File Offset: 0x000042F0
		public static void LogMapiHttpProtocolFailure(IList<string> requestIds, IList<string> cookies, string status, string message, Exception exception, string emailAddress, string organizationInfo, string protocolSequence, string clientAddress, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			ProtocolLog.LogProtocolFailure(ProtocolLogFailureLevel.MapiHttpEndPoint, requestIds, cookies, status, message, exception, emailAddress, organizationInfo, protocolSequence, clientAddress, trace);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006113 File Offset: 0x00004313
		public static void LogWatsonFailure(bool isFatal, Exception exception)
		{
			ExTraceGlobals.UnhandledExceptionTracer.TraceError<bool, Exception>(0, Activity.TraceId, "Unhandled exception. Create watson report. Recycle process = {0}.\r\n{1}", isFatal, exception);
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogFailure(ProtocolLogFailureLevel.Watson, "fault", RopId.None, exception);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006146 File Offset: 0x00004346
		public static void LogWarning(string message, params object[] args)
		{
			if (!ProtocolLog.IsLogEnabled || (ProtocolLog.instance.EnabledTags & ProtocolLoggingTag.Warnings) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogWarning(string.Format(message, args));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006178 File Offset: 0x00004378
		public static void LogThrottlingStatistics(float lowestBudgetBalance, uint throttleCount)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogThrottlingStatistics(lowestBudgetBalance, throttleCount);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000618E File Offset: 0x0000438E
		public static void LogThrottlingSnapshot(IBudget budgetToLog)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogThrottlingSnapshot(budgetToLog);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000061A3 File Offset: 0x000043A3
		public static void LogMicroDelay(DelayEnforcementResults delayinfo)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogMicroDelay(delayinfo);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000061B8 File Offset: 0x000043B8
		public static void LogCriticalResourceHealth(ResourceUnhealthyException unhealthyException)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogCriticalResourceHealth(unhealthyException.ResourceKey.ToString());
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000061D7 File Offset: 0x000043D7
		public static void LogThrottlingOverBudget(string policy, int backoffTime)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogThrottlingOverBudget(policy, backoffTime);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000061ED File Offset: 0x000043ED
		public static void LogThrottlingConnectionLimitHit()
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogThrottlingConnectionLimitHit();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006201 File Offset: 0x00004401
		public static void LogConnectionRpcProcessingTime()
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogConnectionRpcProcessingTime();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006215 File Offset: 0x00004415
		public static void UpdateClientRpcLatency(TimeSpan clientLatency)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalUpdateClientRpcLatency(clientLatency);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000622A File Offset: 0x0000442A
		public static void UpdateClientRpcFailureData(ExDateTime timeStamp, FailureCounterData failureCounterData)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalUpdateClientRpcFailureData(timeStamp, failureCounterData);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006240 File Offset: 0x00004440
		public static void UpdateClientRpcAttemptsData(ExDateTime timeStamp, IRpcCounterData attemptedCounterData)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalUpdateClientRpcAttemptsData(timeStamp, attemptedCounterData);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006256 File Offset: 0x00004456
		public static void UpdateMailboxServerRpcProcessingTime(TimeSpan serverLatency)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalUpdateMailboxServerRpcProcessingTime(serverLatency);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000626B File Offset: 0x0000446B
		public static void LogData(bool flushRow, string value)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogData(flushRow, value);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006281 File Offset: 0x00004481
		public static void LogData<TArg0>(bool flushRow, string format, TArg0 arg0)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogData<TArg0>(flushRow, format, arg0);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006298 File Offset: 0x00004498
		public static void LogData<TArg0, TArg1>(bool flushRow, string format, TArg0 arg0, TArg1 arg1)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogData<TArg0, TArg1>(flushRow, format, arg0, arg1);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000062B0 File Offset: 0x000044B0
		public static void LogData<TArg0, TArg1, TArg2>(bool flushRow, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			if (!ProtocolLog.IsLogEnabled)
			{
				return;
			}
			ProtocolLog.instance.InternalLogData<TArg0, TArg1, TArg2>(flushRow, format, arg0, arg1, arg2);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000062CC File Offset: 0x000044CC
		[Conditional("DEBUG")]
		private static void ValidateFieldDefinition()
		{
			for (int i = 0; i < ProtocolLog.Fields.Length; i++)
			{
				ProtocolLog.Field field = ProtocolLog.Fields[i].Field;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006304 File Offset: 0x00004504
		private static string[] GetColumnArray()
		{
			string[] array = new string[ProtocolLog.Fields.Length];
			for (int i = 0; i < ProtocolLog.Fields.Length; i++)
			{
				array[i] = ProtocolLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006349 File Offset: 0x00004549
		private static bool IsLogEnabled
		{
			get
			{
				return ProtocolLog.instance != null && ProtocolLog.instance.isEnabled && Activity.Current != null;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000636C File Offset: 0x0000456C
		private static bool IsRepeatingBackoffException(Exception ex)
		{
			while (ex != null)
			{
				ClientBackoffException ex2 = ex as ClientBackoffException;
				if (ex2 != null && ex2.IsRepeatingBackoff)
				{
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000639C File Offset: 0x0000459C
		private static void LogProtocolFailure(ProtocolLogFailureLevel protocolFailureLevel, IList<string> requestIds, IList<string> cookies, string status, string message, Exception exception, string emailAddress, string organizationInfo, string protocolSequence, string clientAddress, Microsoft.Exchange.Diagnostics.Trace trace)
		{
			trace.TraceError<string, string, string>(0L, "[{0}] {1} | {2}", status, message ?? "(no message)", (exception != null) ? exception.Message : "(no exception)");
			if (ProtocolLog.instance == null || !ProtocolLog.instance.isEnabled)
			{
				return;
			}
			if ((ProtocolLog.instance.EnabledTags & ProtocolLoggingTag.Failures) == ProtocolLoggingTag.Failures)
			{
				ProtocolLogSession protocolLogSession = ProtocolLog.CreateNewSession(true);
				if (protocolLogSession != null)
				{
					try
					{
						IPAddress clientIpAddress = null;
						if (!string.IsNullOrEmpty(clientAddress) && !IPAddress.TryParse(clientAddress, out clientIpAddress))
						{
							clientIpAddress = null;
						}
						protocolLogSession.OnActivityResume();
						protocolLogSession.SetConnectionParameters(emailAddress, clientIpAddress, protocolSequence);
						protocolLogSession.SetOrganizationInfo(organizationInfo);
						protocolLogSession.SetHttpParameters(cookies, requestIds);
						protocolLogSession.LogFailure(protocolFailureLevel, status, RopId.None, exception, message);
					}
					finally
					{
						protocolLogSession.OnActivityPause();
					}
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006464 File Offset: 0x00004664
		private ProtocolLoggingTag EnabledTags
		{
			get
			{
				return Configuration.ProtocolLogConfiguration.EnabledTags;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006470 File Offset: 0x00004670
		private ProtocolLogSession InternalCreateNewSession(bool lazyInitializeRpcTimers)
		{
			return new ProtocolLogSession(this, lazyInitializeRpcTimers);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006479 File Offset: 0x00004679
		private void InternalSetConnectionParameters(int connectionId, string user, MapiVersion version, IPAddress clientIpAddress, IPAddress serverIpAddress, string protocolSequence)
		{
			if (this.EnabledTags == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetConnectionParameters(connectionId, user, version, clientIpAddress, serverIpAddress, protocolSequence);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000649C File Offset: 0x0000469C
		private void InternalSetHttpParameters(IList<string> sessionCookies, IList<string> requestIds)
		{
			if (this.EnabledTags == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetHttpParameters(sessionCookies, requestIds);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000064B8 File Offset: 0x000046B8
		private void InternalSetApplicationParameters(ClientMode clientMode, string clientProcessName)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.ApplicationData) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetApplicationParameters(clientMode, clientProcessName);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000064D6 File Offset: 0x000046D6
		private void InternalSetOrganizationInfo(string organizationInfo)
		{
			if (this.EnabledTags == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetOrganizationInfo(organizationInfo);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000064F1 File Offset: 0x000046F1
		private void InternalSetClientConnectionInfo(string connectionInfo)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.ConnectDisconnect) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetClientConnectionInfo(connectionInfo);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000650E File Offset: 0x0000470E
		private void InternalSetClientIpAddress(IPAddress clientIpAddress)
		{
			if (this.EnabledTags == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetClientIpAddress(clientIpAddress);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006529 File Offset: 0x00004729
		private void InternalSetActivityData()
		{
			if ((this.EnabledTags & ProtocolLoggingTag.ConnectDisconnect) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.SetActivityData();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006545 File Offset: 0x00004745
		private void InternalLogConnect(SecurityIdentifier sid, ConnectionFlags connectionFlags)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.ConnectDisconnect) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogConnect(sid, connectionFlags);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006563 File Offset: 0x00004763
		private void InternalLogDisconnect(DisconnectReason disconnectReason)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.ConnectDisconnect) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogDisconnect(disconnectReason);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006580 File Offset: 0x00004780
		private void InternalLogLogon(ProtocolLogLogonType logonType, string operationSpecificData, string applicationId)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Logon) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogLogonPending(logonType, applicationId);
			Activity.Current.ProtocolLogSession.LogOperationSpecificData(false, operationSpecificData);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000065B0 File Offset: 0x000047B0
		private void InternalLogLogonSuccess(int logonId)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Logon) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogLogonSuccess(logonId);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000065CE File Offset: 0x000047CE
		private void InternalLogLogonRedirect(string reason, string suggestedNewServer)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Logon) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogLogonRedirect(reason, suggestedNewServer);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000065ED File Offset: 0x000047ED
		private void InternalLogLogoff(ProtocolLogLogonType logonType, int logonId)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Logon) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogLogoff(logonType, logonId);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000660C File Offset: 0x0000480C
		private void InternalLogNewCall()
		{
			Activity.Current.ProtocolLogSession.LogNewCall();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006620 File Offset: 0x00004820
		private void InternalLogInputRops(IEnumerable<RopId> rops)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Rops) == ProtocolLoggingTag.None)
			{
				return;
			}
			foreach (RopId ropId in rops)
			{
				Activity.Current.ProtocolLogSession.LogInputRop(ropId);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000667C File Offset: 0x0000487C
		private void InternalLogOutputRop(RopId ropId, ErrorCode errorCode)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Rops) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogOutputRop(ropId, errorCode);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000669A File Offset: 0x0000489A
		private void InternalLogFailure(ProtocolLogFailureLevel failureLevel, string status, RopId ropId, Exception exception)
		{
			if (ProtocolLog.IsRepeatingBackoffException(exception))
			{
				Activity.Current.ProtocolLogSession.IgnorePendingData();
				return;
			}
			if ((this.EnabledTags & ProtocolLoggingTag.Failures) == ProtocolLoggingTag.Failures)
			{
				Activity.Current.ProtocolLogSession.LogFailure(failureLevel, status, ropId, exception);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000066D8 File Offset: 0x000048D8
		private void InternalLogThrottlingStatistics(float lowestBudgetBalance, uint throttleCount)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None)
			{
				return;
			}
			string text = string.Format("Lowest Budget Balance: {0},Session Throttled Count = {1}", lowestBudgetBalance, throttleCount);
			Activity.Current.ProtocolLogSession.LogThrottling(text);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006718 File Offset: 0x00004918
		private void InternalLogThrottlingOverBudget(string policyPartViolated, int backoffTime)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None)
			{
				return;
			}
			string text = string.Format("Over budget for: {0}  Backoff time: {1}", policyPartViolated, backoffTime);
			Activity.Current.ProtocolLogSession.LogThrottling(text);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006754 File Offset: 0x00004954
		private void InternalLogCriticalResourceHealth(string resourceIdentity)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None)
			{
				return;
			}
			string text = string.Format("Critical resource={0}", resourceIdentity);
			Activity.Current.ProtocolLogSession.LogThrottling(text);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000678C File Offset: 0x0000498C
		private void InternalLogThrottlingSnapshot(IBudget budgetToLog)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None || budgetToLog == null)
			{
				return;
			}
			string text = string.Format("BS={0}", budgetToLog);
			Activity.Current.ProtocolLogSession.LogThrottling(text);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000067C4 File Offset: 0x000049C4
		private void InternalLogMicroDelay(DelayEnforcementResults delayInfo)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None || delayInfo.DelayInfo == DelayInfo.NoDelay)
			{
				return;
			}
			string text;
			if (!string.IsNullOrEmpty(delayInfo.NotEnforcedReason))
			{
				text = string.Format("NotEnforcedMicroDelay:{0}, NotEnforcedReason:{1}", delayInfo.DelayInfo.Delay, delayInfo.NotEnforcedReason);
			}
			else
			{
				text = string.Format("MicroDelay:{0}", delayInfo.DelayedAmount);
			}
			Activity.Current.ProtocolLogSession.LogThrottling(text);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006842 File Offset: 0x00004A42
		private void InternalLogThrottlingConnectionLimitHit()
		{
			if ((this.EnabledTags & ProtocolLoggingTag.Throttling) == ProtocolLoggingTag.None)
			{
				return;
			}
			Activity.Current.ProtocolLogSession.LogThrottling("Connection Limit Exceeded");
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006864 File Offset: 0x00004A64
		private void InternalLogConnectionRpcProcessingTime()
		{
			Activity.Current.ProtocolLogSession.LogConnectionRpcProcessingTime();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006875 File Offset: 0x00004A75
		private void InternalUpdateClientRpcLatency(TimeSpan clientLatency)
		{
			Activity.Current.ProtocolLogSession.UpdateClientRpcLatency(clientLatency);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006887 File Offset: 0x00004A87
		private void InternalUpdateClientRpcFailureData(ExDateTime timeStamp, FailureCounterData failureCounterData)
		{
			Activity.Current.ProtocolLogSession.UpdateClientRpcFailureData(timeStamp, failureCounterData);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000689A File Offset: 0x00004A9A
		private void InternalUpdateClientRpcAttemptsData(ExDateTime timeStamp, IRpcCounterData attemptedCounterData)
		{
			Activity.Current.ProtocolLogSession.UpdateClientRpcAttemptsData(timeStamp, attemptedCounterData);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000068AD File Offset: 0x00004AAD
		private void InternalUpdateMailboxServerRpcProcessingTime(TimeSpan serverLatency)
		{
			Activity.Current.ProtocolLogSession.UpdateMailboxServerRpcProcessingTime(serverLatency);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000068BF File Offset: 0x00004ABF
		private void InternalLogData(bool flushRow, string format)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.OperationSpecific) == ProtocolLoggingTag.None)
			{
				return;
			}
			this.LogOperationSpecificData(flushRow, format);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000068D4 File Offset: 0x00004AD4
		private void InternalLogData<TArg0>(bool flushRow, string format, TArg0 arg0)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.OperationSpecific) == ProtocolLoggingTag.None)
			{
				return;
			}
			this.LogOperationSpecificData(flushRow, string.Format(format, arg0));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000068F4 File Offset: 0x00004AF4
		private void InternalLogData<TArg0, TArg1>(bool flushRow, string format, TArg0 arg0, TArg1 arg1)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.OperationSpecific) == ProtocolLoggingTag.None)
			{
				return;
			}
			this.LogOperationSpecificData(flushRow, string.Format(format, arg0, arg1));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000691B File Offset: 0x00004B1B
		private void InternalLogData<TArg0, TArg1, TArg2>(bool flushRow, string format, TArg0 arg0, TArg1 arg1, TArg2 arg2)
		{
			if ((this.EnabledTags & ProtocolLoggingTag.OperationSpecific) == ProtocolLoggingTag.None)
			{
				return;
			}
			this.LogOperationSpecificData(flushRow, string.Format(format, arg0, arg1, arg2));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006949 File Offset: 0x00004B49
		private void LogOperationSpecificData(bool flushRow, string data)
		{
			Activity.Current.ProtocolLogSession.LogOperationSpecificData(flushRow, data);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000695C File Offset: 0x00004B5C
		private void Refresh()
		{
			ProtocolLogConfiguration protocolLogConfiguration = Configuration.ProtocolLogConfiguration;
			this.isEnabled = protocolLogConfiguration.IsEnabled;
			this.log.Configure(protocolLogConfiguration.LogFilePath, protocolLogConfiguration.AgeQuota, protocolLogConfiguration.DirectorySizeQuota, protocolLogConfiguration.PerFileSizeQuota, protocolLogConfiguration.ApplyHourPrecision);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000069A4 File Offset: 0x00004BA4
		private void Close()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
			this.isEnabled = false;
		}

		// Token: 0x04000154 RID: 340
		private readonly Log log;

		// Token: 0x04000155 RID: 341
		private readonly LogSchema schema;

		// Token: 0x04000156 RID: 342
		internal static readonly ProtocolLog.FieldInfo[] Fields = new ProtocolLog.FieldInfo[]
		{
			new ProtocolLog.FieldInfo(ProtocolLog.Field.DateTime, "date-time", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.SessionId, "session-id", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.SequenceNumber, "seq-number", ProtocolLog.FieldScope.Call),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientName, "client-name", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.OrganizationInfo, "organization-info", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientSoftware, "client-software", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientSoftwareVersion, "client-software-version", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientMode, "client-mode", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientIpAddress, "client-ip", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ClientConnectionInfo, "client-connection-info", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ServerIpAddress, "server-ip", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Protocol, "protocol", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ApplicationId, "application-id", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.RequestIds, "request-ids", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.SessionCookie, "session-cookie", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Operation, "operation", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Status, "rpc-status", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ProcessingTime, "processing-time", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.OperationSpecificData, "operation-specific", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Failures, "failures", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.PerformanceData, "performance-data", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.ActivityContextData, "activity-context-data", ProtocolLog.FieldScope.Record),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.UserEmail, "user-email", ProtocolLog.FieldScope.Session),
			new ProtocolLog.FieldInfo(ProtocolLog.Field.Puid, "passport-unique-id", ProtocolLog.FieldScope.Session)
		};

		// Token: 0x04000157 RID: 343
		private static ProtocolLog instance;

		// Token: 0x04000158 RID: 344
		private bool isEnabled;

		// Token: 0x02000030 RID: 48
		internal struct FieldInfo
		{
			// Token: 0x060001E1 RID: 481 RVA: 0x00006C39 File Offset: 0x00004E39
			public FieldInfo(ProtocolLog.Field field, string columnName, ProtocolLog.FieldScope scope)
			{
				this.Field = field;
				this.ColumnName = columnName;
				this.Scope = scope;
			}

			// Token: 0x04000159 RID: 345
			public readonly ProtocolLog.Field Field;

			// Token: 0x0400015A RID: 346
			public readonly string ColumnName;

			// Token: 0x0400015B RID: 347
			public readonly ProtocolLog.FieldScope Scope;
		}

		// Token: 0x02000031 RID: 49
		internal enum Field
		{
			// Token: 0x0400015D RID: 349
			DateTime,
			// Token: 0x0400015E RID: 350
			SessionId,
			// Token: 0x0400015F RID: 351
			SequenceNumber,
			// Token: 0x04000160 RID: 352
			ClientName,
			// Token: 0x04000161 RID: 353
			OrganizationInfo,
			// Token: 0x04000162 RID: 354
			ClientSoftware,
			// Token: 0x04000163 RID: 355
			ClientSoftwareVersion,
			// Token: 0x04000164 RID: 356
			ClientMode,
			// Token: 0x04000165 RID: 357
			ClientIpAddress,
			// Token: 0x04000166 RID: 358
			ClientConnectionInfo,
			// Token: 0x04000167 RID: 359
			ServerIpAddress,
			// Token: 0x04000168 RID: 360
			Protocol,
			// Token: 0x04000169 RID: 361
			ApplicationId,
			// Token: 0x0400016A RID: 362
			RequestIds,
			// Token: 0x0400016B RID: 363
			SessionCookie,
			// Token: 0x0400016C RID: 364
			Operation,
			// Token: 0x0400016D RID: 365
			Status,
			// Token: 0x0400016E RID: 366
			ProcessingTime,
			// Token: 0x0400016F RID: 367
			OperationSpecificData,
			// Token: 0x04000170 RID: 368
			Failures,
			// Token: 0x04000171 RID: 369
			PerformanceData,
			// Token: 0x04000172 RID: 370
			ActivityContextData,
			// Token: 0x04000173 RID: 371
			UserEmail,
			// Token: 0x04000174 RID: 372
			Puid,
			// Token: 0x04000175 RID: 373
			NumberOfFields
		}

		// Token: 0x02000032 RID: 50
		internal enum FieldScope
		{
			// Token: 0x04000177 RID: 375
			Session,
			// Token: 0x04000178 RID: 376
			Call,
			// Token: 0x04000179 RID: 377
			Record
		}
	}
}
