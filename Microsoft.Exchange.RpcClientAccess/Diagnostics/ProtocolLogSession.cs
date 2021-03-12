using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.Messages;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Mapi;
using Microsoft.Office365.DataInsights.Uploader;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x02000036 RID: 54
	internal class ProtocolLogSession
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00006F6A File Offset: 0x0000516A
		internal ProtocolLogSession(ProtocolLog protocolLog) : this(protocolLog, false)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006F74 File Offset: 0x00005174
		internal ProtocolLogSession(ProtocolLog protocolLog, bool lazyInitializeRpcTimers)
		{
			this.protocolLog = protocolLog;
			this.contextValues = new string[24];
			if (!lazyInitializeRpcTimers)
			{
				this.connectionProcessingTime = new ProtocolLogSession.RpcProcessingTime();
				this.failureData = new ProtocolLogSession.RpcFailureData();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006FD4 File Offset: 0x000051D4
		internal static BatchingUploader<MoMTRawData> StreamInsightUploader
		{
			get
			{
				if (ProtocolLogSession.uploader == null)
				{
					DataContractSerializerEncoder<MoMTRawData> dataContractSerializerEncoder = new DataContractSerializerEncoder<MoMTRawData>();
					ProtocolLogSession.uploader = new BatchingUploader<MoMTRawData>(dataContractSerializerEncoder, ServiceConfiguration.StreamInsightEngineURI, ServiceConfiguration.StreamInsightUploaderQueueSize, TimeSpan.FromSeconds(30.0), 1000, 1, 3, true, "", false, null, null, null, null, false);
				}
				return ProtocolLogSession.uploader;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000702C File Offset: 0x0000522C
		public static string GenerateActivityScopeReport(bool statisticsOnly = false)
		{
			ReferencedActivityScope referencedActivityScope = ReferencedActivityScope.Current;
			if (referencedActivityScope == null)
			{
				return string.Empty;
			}
			return LogRowFormatter.FormatCollection(statisticsOnly ? referencedActivityScope.ActivityScope.GetFormattableStatistics() : referencedActivityScope.ActivityScope.GetFormattableMetadata().Concat(referencedActivityScope.ActivityScope.GetFormattableStatistics()));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007078 File Offset: 0x00005278
		internal static string GenerateLogonOperationSpecificData(ProtocolLogLogonType logonType, ExchangePrincipal targetMailbox)
		{
			object obj = (targetMailbox.MailboxInfo.MailboxDatabase != null) ? targetMailbox.MailboxInfo.MailboxDatabase.Name : null;
			string legacyDn = targetMailbox.LegacyDn;
			IMailboxLocation location = targetMailbox.MailboxInfo.Location;
			if (location != null)
			{
				return string.Format("Logon: {0}, {1} in database {2} last mounted on {3}", new object[]
				{
					logonType,
					legacyDn,
					obj,
					location.ServerFqdn
				});
			}
			return string.Format("Logon: {0}{1}, {2} in database {3}", new object[]
			{
				logonType,
				targetMailbox.MailboxInfo.IsRemote ? "-Remote" : string.Empty,
				legacyDn,
				obj
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000712E File Offset: 0x0000532E
		internal void IgnorePendingData()
		{
			ProtocolLogSession.tlsData.ClearPerRecordData();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000713A File Offset: 0x0000533A
		internal void OnClientActivityResume()
		{
			this.OnActivityResume();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007142 File Offset: 0x00005342
		internal void OnClientActivityPause()
		{
			this.ConnectionProcessingTime.UpdateClientAccessServerRpcProcessingTime(ProtocolLogSession.tlsData.ElapsedTime);
			this.OnActivityPause();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000715F File Offset: 0x0000535F
		internal void OnActivityResume()
		{
			if (ProtocolLogSession.tlsData == null)
			{
				ProtocolLogSession.tlsData = new ProtocolLogSession.ThreadLocalData(this.protocolLog);
			}
			ProtocolLogSession.tlsData.ClearPendingData();
			ProtocolLogSession.tlsData.CurrentCallStartTickCount = new int?(Environment.TickCount);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007196 File Offset: 0x00005396
		internal void OnActivityPause()
		{
			this.FlushRow();
			this.ClearPersistentData();
			ProtocolLogSession.tlsData.ClearPendingData();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000071B0 File Offset: 0x000053B0
		internal void SetConnectionParameters(int connectionId, string user, MapiVersion version, IPAddress clientIpAddress, IPAddress serverIpAddress, string protocolSequence)
		{
			this.SetColumn(ProtocolLog.Field.SessionId, connectionId.ToString());
			this.SetColumn(ProtocolLog.Field.ClientName, user);
			this.SetColumn(ProtocolLog.Field.UserEmail, this.GetUserEmail());
			this.SetColumn(ProtocolLog.Field.Puid, this.GetPuid());
			this.SetColumn(ProtocolLog.Field.ClientSoftwareVersion, version.ToString());
			this.SetColumn(ProtocolLog.Field.ClientIpAddress, clientIpAddress.ToString());
			this.SetColumn(ProtocolLog.Field.ServerIpAddress, serverIpAddress.ToString());
			this.SetColumn(ProtocolLog.Field.Protocol, protocolSequence);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000722C File Offset: 0x0000542C
		internal void SetHttpParameters(IList<string> sessionCookies, IList<string> requestIds)
		{
			if (sessionCookies != null)
			{
				string value = string.Join("|", sessionCookies);
				ProtocolLogSession.tlsData.PersistentData[ProtocolLog.Field.SessionCookie] = value;
			}
			if (requestIds != null)
			{
				string value2 = string.Join("|", requestIds);
				ProtocolLogSession.tlsData.PersistentData[ProtocolLog.Field.RequestIds] = value2;
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000727B File Offset: 0x0000547B
		internal void SetConnectionParameters(string user, IPAddress clientIpAddress, string protocolSequence)
		{
			this.SetColumn(ProtocolLog.Field.ClientName, user);
			this.SetColumn(ProtocolLog.Field.Protocol, protocolSequence);
			if (clientIpAddress != null)
			{
				this.SetColumn(ProtocolLog.Field.ClientIpAddress, clientIpAddress.ToString());
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000729E File Offset: 0x0000549E
		internal void SetClientIpAddress(IPAddress clientIpAddress)
		{
			if (clientIpAddress != null)
			{
				this.SetColumn(ProtocolLog.Field.ClientIpAddress, clientIpAddress.ToString(), false);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000072B1 File Offset: 0x000054B1
		internal void SetApplicationParameters(ClientMode clientMode, string clientProcessName)
		{
			this.SetColumn(ProtocolLog.Field.ClientMode, ProtocolLogSession.clientModeFormatter.Format(clientMode));
			this.SetColumn(ProtocolLog.Field.ClientSoftware, clientProcessName);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000072CD File Offset: 0x000054CD
		internal void SetOrganizationInfo(string organizationInfo)
		{
			this.organizationInfo = organizationInfo;
			this.SetColumn(ProtocolLog.Field.OrganizationInfo, organizationInfo);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000072DE File Offset: 0x000054DE
		internal void SetClientConnectionInfo(string connectionInfo)
		{
			this.SetColumn(ProtocolLog.Field.ClientConnectionInfo, connectionInfo);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000072E9 File Offset: 0x000054E9
		internal void SetActivityData()
		{
			this.SetColumn(ProtocolLog.Field.ActivityContextData, ProtocolLogSession.GenerateActivityScopeReport(false));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000072F9 File Offset: 0x000054F9
		internal void LogConnect(SecurityIdentifier sid, ConnectionFlags connectionFlags)
		{
			this.LogOperationPending(ProtocolLogSession.OperationConnectEx);
			this.LogOperationSpecificData(false, string.Format("SID={0}, Flags={1}", sid, connectionFlags));
			this.startTickCount = Environment.TickCount;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000732C File Offset: 0x0000552C
		internal void LogDisconnect(DisconnectReason disconnectReason)
		{
			switch (disconnectReason)
			{
			case DisconnectReason.ClientDisconnect:
				this.LogOperationPending(ProtocolLogSession.OperationDisconnect);
				break;
			case DisconnectReason.ServerDropped:
				this.LogOperationSpecificData(false, ProtocolLogSession.operationDataSessionDropped);
				break;
			case DisconnectReason.NetworkRundown:
				this.LogOperationPending(ProtocolLogSession.OperationNetworkRundown);
				break;
			default:
				throw new InvalidOperationException(string.Format("Invalid DisconnectReason; disconnectReason={0}", disconnectReason));
			}
			this.SetColumn(ProtocolLog.Field.PerformanceData, string.Format("{0};{1}", this.ConnectionProcessingTime.DetachRpcProcessingTimeLogLine(), this.FailureData.DetachClientRpcStatisticsLogLine()));
			this.SetColumn(ProtocolLog.Field.ActivityContextData, ProtocolLogSession.GenerateActivityScopeReport(false));
			this.SetColumn(ProtocolLog.Field.OrganizationInfo, this.organizationInfo);
			ProtocolLogSession.tlsData.CurrentCallStartTickCount = new int?(this.startTickCount);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000073E8 File Offset: 0x000055E8
		internal void LogConnectionRpcProcessingTime()
		{
			if (ProtocolLogSession.tlsData == null)
			{
				ProtocolLogSession.tlsData = new ProtocolLogSession.ThreadLocalData(this.protocolLog);
			}
			this.SetColumn(ProtocolLog.Field.Operation, ProtocolLogSession.OperationSystemLogging);
			this.SetColumn(ProtocolLog.Field.PerformanceData, string.Format("{0};{1}", this.ConnectionProcessingTime.DetachRpcProcessingTimeLogLine(), this.FailureData.DetachClientRpcStatisticsLogLine()));
			this.SetColumn(ProtocolLog.Field.ActivityContextData, ProtocolLogSession.GenerateActivityScopeReport(false));
			this.SetColumn(ProtocolLog.Field.OrganizationInfo, this.organizationInfo);
			this.FlushRow();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007462 File Offset: 0x00005662
		internal void UpdateClientRpcLatency(TimeSpan clientLatency)
		{
			this.ConnectionProcessingTime.UpdateClientRpcLatency(clientLatency);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007470 File Offset: 0x00005670
		internal void UpdateClientRpcFailureData(ExDateTime timeStamp, FailureCounterData failureCounterData)
		{
			this.FailureData.UpdateClientRpcFailureData(timeStamp, failureCounterData);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000747F File Offset: 0x0000567F
		internal void UpdateClientRpcAttemptsData(ExDateTime timeStamp, IRpcCounterData attemptedCounterData)
		{
			this.FailureData.UpdateClientRpcAttemptsData(timeStamp, attemptedCounterData);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000748E File Offset: 0x0000568E
		internal void UpdateMailboxServerRpcProcessingTime(TimeSpan serverLatency)
		{
			this.connectionProcessingTime.UpdateMailboxServerRpcProcessingTime(serverLatency);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000749C File Offset: 0x0000569C
		internal void LogLogonPending(ProtocolLogLogonType logonType, string applicationId)
		{
			this.LogOperationPending(logonType.ToString() + ProtocolLogSession.OperationSuffixLogon);
			this.SetColumn(ProtocolLog.Field.ApplicationId, applicationId);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000074C2 File Offset: 0x000056C2
		internal void LogLogonSuccess(int logonId)
		{
			this.LogLogonId(logonId);
			this.FlushRow();
			this.ClearPersistentData();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000074D7 File Offset: 0x000056D7
		internal void LogLogonRedirect(string reason, string suggestedNewServer)
		{
			this.LogOperationSpecificData(false, string.Format("Redirected: {0}, suggested new server: {1}", reason, suggestedNewServer));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000074EC File Offset: 0x000056EC
		internal void LogLogoff(ProtocolLogLogonType logonType, int logonId)
		{
			this.LogOperationPending(logonType.ToString() + ProtocolLogSession.OperationSuffixLogoff);
			this.LogLogonId(logonId);
			this.FlushRow();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007518 File Offset: 0x00005718
		internal void LogNewCall()
		{
			this.SetColumn(ProtocolLog.Field.Status, "0", false);
			this.SetColumn(ProtocolLog.Field.SequenceNumber, (Interlocked.Increment(ref this.rpcRequestCount) - 1).ToString(), false);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007550 File Offset: 0x00005750
		internal void LogInputRop(RopId ropId)
		{
			this.LogOperationSpecificData(false, ProtocolLogSession.inputRopFormatter.Format(ropId));
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007564 File Offset: 0x00005764
		internal void LogOutputRop(RopId ropId, ErrorCode errorCode)
		{
			if (errorCode == ErrorCode.None)
			{
				this.LogOperationSpecificData(false, ProtocolLogSession.outputRopFormatter.Format(ropId));
				return;
			}
			this.LogOperationSpecificData(false, string.Format(ProtocolLogSession.outputRopWithErrorFormatter.Format(ropId), errorCode));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007599 File Offset: 0x00005799
		internal void LogFailure(ProtocolLogFailureLevel failureLevel, string status, RopId ropId, Exception exception)
		{
			this.LogFailure(failureLevel, status, ropId, exception, null);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000075A8 File Offset: 0x000057A8
		internal void LogFailure(ProtocolLogFailureLevel failureLevel, string status, RopId ropId, Exception exception, string message)
		{
			this.SetColumn(ProtocolLog.Field.Status, status);
			StringBuilder stringBuilder = new StringBuilder(ProtocolLogSession.logFailureLevelFormatter.Format(failureLevel));
			stringBuilder.Append(": ");
			if (!string.IsNullOrEmpty(message))
			{
				stringBuilder.Append("[");
				stringBuilder.Append(message);
				stringBuilder.Append("] ");
			}
			if (failureLevel == ProtocolLogFailureLevel.RopHandler)
			{
				stringBuilder.Append(ProtocolLogSession.genericRopFormatter.Format(ropId));
				stringBuilder.Append(": ");
			}
			string text = null;
			bool flag = false;
			Exception ex = exception;
			while (exception != null)
			{
				if (ex != exception)
				{
					stringBuilder.Append(" -> ");
				}
				string text2 = exception.Message;
				string text3 = null;
				MapiRetryableException ex2 = exception as MapiRetryableException;
				MapiPermanentException ex3 = exception as MapiPermanentException;
				if (text2 != null && (ex2 != null || ex3 != null))
				{
					string text4 = exception.GetType().Name + ": ";
					int num = text2.StartsWith(text4) ? text4.Length : 0;
					int num2 = text2.IndexOf(DiagnosticContext.MessageHeader);
					if (num != 0 || num2 != -1)
					{
						if (num2 == -1)
						{
							text2 = text2.Substring(num);
						}
						else
						{
							text2 = text2.Substring(num, num2 - num);
						}
					}
					if (ex2 != null && ex2.DiagCtx != null)
					{
						text3 = Convert.ToBase64String(ex2.DiagCtx.ToByteArray());
					}
					else if (ex3 != null && ex3.DiagCtx != null)
					{
						text3 = Convert.ToBase64String(ex3.DiagCtx.ToByteArray());
					}
				}
				stringBuilder.AppendFormat("[{0}] {1}", exception.GetType().Name, text2);
				if (text3 != null)
				{
					stringBuilder.AppendFormat(" [diag::{0}]", text3);
				}
				if (!flag)
				{
					flag = true;
					if (!ProtocolLogSession.IsCommonlyKnownException(exception))
					{
						string stackTrace = exception.StackTrace;
						if (!string.IsNullOrEmpty(stackTrace))
						{
							text = stackTrace;
						}
					}
				}
				exception = exception.InnerException;
			}
			stringBuilder.Replace('\r', ' ').Replace('\n', ' ');
			if (!string.IsNullOrEmpty(text))
			{
				text = ProtocolLogSession.AbbreviateStackTrace(text);
				int num3 = 1500 - stringBuilder.Length - 1;
				if (text.Length < num3)
				{
					stringBuilder.Append(text);
				}
				else if (num3 > 0)
				{
					stringBuilder.Append(text.Substring(0, num3));
				}
			}
			this.SetColumn(ProtocolLog.Field.Failures, stringBuilder.ToString());
			this.FlushRow();
			this.ClearPersistentData();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000077F4 File Offset: 0x000059F4
		private static string AbbreviateStackTrace(string fullStackTrace)
		{
			string text = fullStackTrace;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace('\r', ' ').Replace('\n', ' ');
				text = text.Replace("\"", "'");
				text = text.Replace("Microsoft.", "M.");
				text = text.Replace(".Exchange.", ".E.");
				text = text.Replace(".RpcClientAccess.", ".R.");
				text = text.Replace(".Data.", ".D.");
				text = text.Replace(".Directory.", ".Dir.");
				text = text.Replace(".Storage.", ".S.");
				text = text.Replace(".Handler.", ".H.");
				text = text.Replace(".Parser.", ".P.");
				text = text.Replace(".Common.", ".C.");
			}
			return text;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000078E8 File Offset: 0x00005AE8
		private static bool IsCommonlyKnownException(Exception exception)
		{
			while (exception != null)
			{
				if (ProtocolLogSession.KnownExceptions.Contains(exception.GetType()))
				{
					return true;
				}
				if (!string.IsNullOrEmpty(exception.Message))
				{
					if (ProtocolLogSession.KnownErrors.Any((string str) => exception.Message.Contains(str)))
					{
						return true;
					}
				}
				exception = exception.InnerException;
			}
			return false;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000796A File Offset: 0x00005B6A
		internal void LogThrottling(string text)
		{
			this.LogOperationSpecificData(false, text);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007974 File Offset: 0x00005B74
		internal void LogWarning(string text)
		{
			this.LogOperationSpecificData(false, text);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000797E File Offset: 0x00005B7E
		internal void LogOperationSpecificData(bool flushRow, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				ProtocolLogSession.tlsData.HasInterestingDataToFlush = true;
				ProtocolLogSession.tlsData.OperationSpecificData.Append(value);
			}
			if (flushRow)
			{
				this.FlushRow();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000079AC File Offset: 0x00005BAC
		private void SetColumn(ProtocolLog.Field field, string value)
		{
			this.SetColumn(field, value, true);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000079B8 File Offset: 0x00005BB8
		private void SetColumn(ProtocolLog.Field field, string value, bool markRowAsDirty)
		{
			if (markRowAsDirty)
			{
				ProtocolLogSession.tlsData.HasInterestingDataToFlush = true;
			}
			if (ProtocolLog.Fields[(int)field].Scope == ProtocolLog.FieldScope.Session)
			{
				lock (this.contextValues)
				{
					this.contextValues[(int)field] = value;
					return;
				}
			}
			ProtocolLogSession.tlsData.Row[(int)field] = value;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007A34 File Offset: 0x00005C34
		private void LogLogonId(int logonId)
		{
			this.LogOperationSpecificData(false, string.Format("LogonId: {0}", logonId));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007A4D File Offset: 0x00005C4D
		private void LogOperationPending(string operation)
		{
			this.FlushRow();
			this.SetColumn(ProtocolLog.Field.Operation, operation);
			this.SetColumn(ProtocolLog.Field.Status, "0", false);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007A6C File Offset: 0x00005C6C
		private void FlushRow()
		{
			if (ProtocolLogSession.tlsData.HasInterestingDataToFlush)
			{
				foreach (KeyValuePair<ProtocolLog.Field, string> keyValuePair in ProtocolLogSession.tlsData.PersistentData)
				{
					this.SetColumn(keyValuePair.Key, keyValuePair.Value, false);
				}
				this.SetColumn(ProtocolLog.Field.OperationSpecificData, ProtocolLogSession.tlsData.OperationSpecificData.Detach());
				this.SetColumn(ProtocolLog.Field.ProcessingTime, ProtocolLogSession.tlsData.ElapsedTime.ToString());
				lock (this.contextValues)
				{
					for (int i = 0; i < this.contextValues.Length; i++)
					{
						if (this.contextValues[i] != null)
						{
							ProtocolLogSession.tlsData.Row[i] = this.contextValues[i];
						}
					}
				}
				this.protocolLog.Append(ProtocolLogSession.tlsData.Row);
			}
			if (ServiceConfiguration.StreamInsightUploaderEnabled)
			{
				object obj2 = ProtocolLogSession.tlsData.Row[15];
				object obj3 = ProtocolLogSession.tlsData.Row[5];
				if (obj2 != null && obj3 != null && ((string)obj2).Equals("OwnerLogon", StringComparison.OrdinalIgnoreCase) && !((string)obj3).Equals("Microsoft.Exchange.RpcClientAccess.Monitoring.dll", StringComparison.OrdinalIgnoreCase))
				{
					this.UploadToStreamInsight();
				}
			}
			ProtocolLogSession.tlsData.ClearPerRecordData();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007BF8 File Offset: 0x00005DF8
		private void ClearPersistentData()
		{
			ProtocolLogSession.tlsData.PersistentData.Clear();
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007C0C File Offset: 0x00005E0C
		private void PopulateStreamInsightDataObject(out MoMTRawData momtRawData)
		{
			momtRawData = new MoMTRawData();
			momtRawData.DateTimeUtc = ((DateTime)ProtocolLogSession.tlsData.Row[0]).ToString("o");
			momtRawData.ClientName = this.contextValues[3];
			momtRawData.OrganizationInfo = this.organizationInfo;
			momtRawData.Failures = (string)ProtocolLogSession.tlsData.Row[19];
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007C84 File Offset: 0x00005E84
		private void UploadToStreamInsight()
		{
			try
			{
				MoMTRawData moMTRawData;
				this.PopulateStreamInsightDataObject(out moMTRawData);
				if (!ProtocolLogSession.StreamInsightUploader.TryEnqueueItem(moMTRawData))
				{
					this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_StreamInsightsDataUploadFailed, "RpcClientAccess_StreamInsightsDataUploadFailed", new object[]
					{
						ServiceConfiguration.Component,
						ServiceConfiguration.StreamInsightEngineURI
					});
				}
			}
			catch (Exception ex)
			{
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_StreamInsightsDataUploadExceptionThrown, "RpcClientAccess_StreamInsightsDataUploadExceptionThrown", new object[]
				{
					ServiceConfiguration.Component,
					ex.ToString()
				});
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00007D20 File Offset: 0x00005F20
		private ProtocolLogSession.RpcProcessingTime ConnectionProcessingTime
		{
			get
			{
				if (this.connectionProcessingTime == null)
				{
					this.connectionProcessingTime = new ProtocolLogSession.RpcProcessingTime();
				}
				return this.connectionProcessingTime;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007D3C File Offset: 0x00005F3C
		private string GetUserEmail()
		{
			ReferencedActivityScope referencedActivityScope = ReferencedActivityScope.Current;
			if (referencedActivityScope == null)
			{
				return string.Empty;
			}
			return referencedActivityScope.UserEmail;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007D60 File Offset: 0x00005F60
		private string GetPuid()
		{
			ReferencedActivityScope referencedActivityScope = ReferencedActivityScope.Current;
			if (referencedActivityScope == null || referencedActivityScope.Puid == null)
			{
				return string.Empty;
			}
			return referencedActivityScope.Puid;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007D8A File Offset: 0x00005F8A
		private ProtocolLogSession.RpcFailureData FailureData
		{
			get
			{
				if (this.failureData == null)
				{
					this.failureData = new ProtocolLogSession.RpcFailureData();
				}
				return this.failureData;
			}
		}

		// Token: 0x0400019C RID: 412
		private const int LogFailureMaxLength = 1500;

		// Token: 0x0400019D RID: 413
		internal static readonly string OperationConnectEx = "Connect";

		// Token: 0x0400019E RID: 414
		internal static readonly string OperationDisconnect = "Disconnect";

		// Token: 0x0400019F RID: 415
		internal static readonly string OperationSuffixLogon = "Logon";

		// Token: 0x040001A0 RID: 416
		internal static readonly string OperationSuffixLogoff = "Logoff";

		// Token: 0x040001A1 RID: 417
		internal static readonly string OperationSystemLogging = "SystemLogging";

		// Token: 0x040001A2 RID: 418
		internal static readonly string OperationNetworkRundown = "NetworkRundown";

		// Token: 0x040001A3 RID: 419
		private static readonly string operationDataSessionDropped = "SessionDropped";

		// Token: 0x040001A4 RID: 420
		private static readonly EnumFormatter<RopId> genericRopFormatter = new EnumFormatter<RopId>("{0}", (RopId v) => (int)v);

		// Token: 0x040001A5 RID: 421
		private static readonly EnumFormatter<RopId> inputRopFormatter = new EnumFormatter<RopId>(">{0}", (RopId v) => (int)v);

		// Token: 0x040001A6 RID: 422
		private static readonly EnumFormatter<RopId> outputRopFormatter = new EnumFormatter<RopId>("<{0}", (RopId v) => (int)v);

		// Token: 0x040001A7 RID: 423
		private static readonly EnumFormatter<RopId> outputRopWithErrorFormatter = new EnumFormatter<RopId>("<{0}({{0}})", (RopId v) => (int)v);

		// Token: 0x040001A8 RID: 424
		private static readonly EnumFormatter<ClientMode> clientModeFormatter = new EnumFormatter<ClientMode>("{0}", (ClientMode v) => (int)v);

		// Token: 0x040001A9 RID: 425
		private static readonly EnumFormatter<ProtocolLogFailureLevel> logFailureLevelFormatter = new EnumFormatter<ProtocolLogFailureLevel>("{0}", (ProtocolLogFailureLevel v) => (int)v);

		// Token: 0x040001AA RID: 426
		private static readonly EnumFormatter<DatabaseLocationInfoResult> databaseLocationInfoFormatter = new EnumFormatter<DatabaseLocationInfoResult>("{0}", (DatabaseLocationInfoResult v) => (int)v).OverrideFormat(DatabaseLocationInfoResult.Success, "Mounted");

		// Token: 0x040001AB RID: 427
		private static BatchingUploader<MoMTRawData> uploader;

		// Token: 0x040001AC RID: 428
		[ThreadStatic]
		private static ProtocolLogSession.ThreadLocalData tlsData;

		// Token: 0x040001AD RID: 429
		private readonly ProtocolLog protocolLog;

		// Token: 0x040001AE RID: 430
		private readonly string[] contextValues;

		// Token: 0x040001AF RID: 431
		private int rpcRequestCount;

		// Token: 0x040001B0 RID: 432
		private int startTickCount;

		// Token: 0x040001B1 RID: 433
		private ProtocolLogSession.RpcProcessingTime connectionProcessingTime;

		// Token: 0x040001B2 RID: 434
		private ProtocolLogSession.RpcFailureData failureData;

		// Token: 0x040001B3 RID: 435
		private string organizationInfo = string.Empty;

		// Token: 0x040001B4 RID: 436
		private ExEventLog eventLog = new ExEventLog(ServiceConfiguration.ComponentGuid, "MSExchangeRPC");

		// Token: 0x040001B5 RID: 437
		private static readonly string[] KnownErrors = new string[]
		{
			ErrorCode.MdbOffline.ToString()
		};

		// Token: 0x040001B6 RID: 438
		private static readonly Type[] KnownExceptions = new Type[]
		{
			typeof(MapiExceptionIllegalCrossServerConnection),
			typeof(WrongServerException),
			typeof(SessionDeadException)
		};

		// Token: 0x02000037 RID: 55
		internal class RpcFailureData
		{
			// Token: 0x06000232 RID: 562 RVA: 0x00007FA1 File Offset: 0x000061A1
			public RpcFailureData()
			{
				this.rpcFailureDataLock = new object();
				this.timeBucketedFailureCounters = new RpcTimeIntervalCounterGroups<RpcFailureCounters>();
				this.timeBucketedRpcAttemptedCounters = new RpcTimeIntervalCounterGroups<RpcAttemptedCounters>();
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00007FCC File Offset: 0x000061CC
			public void UpdateClientRpcFailureData(ExDateTime timeStamp, FailureCounterData failureCounterData)
			{
				lock (this.rpcFailureDataLock)
				{
					this.timeBucketedFailureCounters.IncrementCounter(timeStamp, failureCounterData);
				}
			}

			// Token: 0x06000234 RID: 564 RVA: 0x00008014 File Offset: 0x00006214
			public void UpdateClientRpcAttemptsData(ExDateTime timeStamp, IRpcCounterData attemptedCounterData)
			{
				lock (this.rpcFailureDataLock)
				{
					this.timeBucketedRpcAttemptedCounters.IncrementCounter(timeStamp, attemptedCounterData);
				}
			}

			// Token: 0x06000235 RID: 565 RVA: 0x0000805C File Offset: 0x0000625C
			internal string DetachClientRpcStatisticsLogLine()
			{
				string result;
				lock (this.rpcFailureDataLock)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ExDateTime exDateTime in this.timeBucketedFailureCounters.GetTimeIntervals())
					{
						stringBuilder.AppendFormat("[{0}|{1};{2}]", exDateTime, this.timeBucketedRpcAttemptedCounters.GetFormattedCounterDataForInterval(exDateTime), this.timeBucketedFailureCounters.GetFormattedCounterDataForInterval(exDateTime));
					}
					this.Reset();
					result = string.Format("ClientRpcFailureData:{0}", stringBuilder);
				}
				return result;
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000811C File Offset: 0x0000631C
			private void Reset()
			{
				lock (this.rpcFailureDataLock)
				{
					this.timeBucketedFailureCounters.ResetCounters();
					this.timeBucketedRpcAttemptedCounters.ResetCounters();
				}
			}

			// Token: 0x040001BE RID: 446
			private const string LogLineFormat = "ClientRpcFailureData:{0}";

			// Token: 0x040001BF RID: 447
			private readonly RpcTimeIntervalCounterGroups<RpcFailureCounters> timeBucketedFailureCounters;

			// Token: 0x040001C0 RID: 448
			private readonly RpcTimeIntervalCounterGroups<RpcAttemptedCounters> timeBucketedRpcAttemptedCounters;

			// Token: 0x040001C1 RID: 449
			private readonly object rpcFailureDataLock;
		}

		// Token: 0x02000038 RID: 56
		internal class RpcProcessingTime
		{
			// Token: 0x06000237 RID: 567 RVA: 0x0000816C File Offset: 0x0000636C
			internal string DetachRpcProcessingTimeLogLine()
			{
				string result;
				lock (this.rpcProcessingTimeLock)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("ClientRPCCount={0};AvgClientLatency={1};ServerRPCCount={2};AvgCasRPCProcessingTime={3};AvgMbxProcessingTime={4};MaxCasRPCProcessingTime={5}", new object[]
					{
						this.clientRpcCount,
						(this.clientRpcCount > 1) ? TimeSpan.FromTicks(this.totalClientRPCLatency.Ticks / (long)this.clientRpcCount) : this.totalClientRPCLatency,
						this.serverRpcCount,
						(this.serverRpcCount > 1) ? TimeSpan.FromTicks(this.totalCasRPCProcessingTime.Ticks / (long)this.serverRpcCount) : this.totalCasRPCProcessingTime,
						(this.serverRpcCount > 1) ? TimeSpan.FromTicks(this.totalMbxRPCProcessingTime.Ticks / (long)this.serverRpcCount) : this.totalMbxRPCProcessingTime,
						this.maxRpcProcessingTime
					});
					StringBuilder stringBuilder2 = new StringBuilder();
					StringBuilder stringBuilder3 = new StringBuilder();
					StringBuilder stringBuilder4 = new StringBuilder();
					for (int i = 0; i < ProtocolLogSession.RpcProcessingTime.Percentiles.Length; i++)
					{
						stringBuilder2.AppendFormat(";{0}%ClientPercentile={1}", ProtocolLogSession.RpcProcessingTime.Percentiles[i], this.clientPercentileCounter.PercentileQuery(ProtocolLogSession.RpcProcessingTime.Percentiles[i]) + 10L);
						stringBuilder3.AppendFormat(";{0}%CasPercentile={1}", ProtocolLogSession.RpcProcessingTime.Percentiles[i], this.casPercentileCounter.PercentileQuery(ProtocolLogSession.RpcProcessingTime.Percentiles[i]) + 10L);
						stringBuilder4.AppendFormat(";{0}%MbxPercentile={1}", ProtocolLogSession.RpcProcessingTime.Percentiles[i], this.mbxPercentileCounter.PercentileQuery(ProtocolLogSession.RpcProcessingTime.Percentiles[i]) + 10L);
					}
					stringBuilder.AppendFormat("{0}{1}{2}", stringBuilder2.ToString(), stringBuilder3.ToString(), stringBuilder4.ToString());
					this.Reset();
					result = stringBuilder.ToString();
				}
				return result;
			}

			// Token: 0x06000238 RID: 568 RVA: 0x00008390 File Offset: 0x00006590
			internal void UpdateClientAccessServerRpcProcessingTime(TimeSpan requestProcessingTime)
			{
				lock (this.rpcProcessingTimeLock)
				{
					if (requestProcessingTime > this.maxRpcProcessingTime)
					{
						this.maxRpcProcessingTime = requestProcessingTime;
					}
					this.totalCasRPCProcessingTime += requestProcessingTime;
					this.serverRpcCount++;
					this.casPercentileCounter.AddValue((long)requestProcessingTime.Milliseconds);
				}
			}

			// Token: 0x06000239 RID: 569 RVA: 0x00008414 File Offset: 0x00006614
			internal void UpdateMailboxServerRpcProcessingTime(TimeSpan requestProcessingTime)
			{
				lock (this.rpcProcessingTimeLock)
				{
					this.totalMbxRPCProcessingTime += requestProcessingTime;
					this.mbxPercentileCounter.AddValue((long)requestProcessingTime.Milliseconds);
				}
			}

			// Token: 0x0600023A RID: 570 RVA: 0x00008474 File Offset: 0x00006674
			internal void UpdateClientRpcLatency(TimeSpan clientLatency)
			{
				lock (this.rpcProcessingTimeLock)
				{
					this.totalClientRPCLatency += clientLatency;
					this.clientRpcCount++;
					this.clientPercentileCounter.AddValue((long)clientLatency.Milliseconds);
				}
			}

			// Token: 0x0600023B RID: 571 RVA: 0x000084E4 File Offset: 0x000066E4
			private void Reset()
			{
				this.serverRpcCount = 0;
				this.clientRpcCount = 0;
				this.maxRpcProcessingTime = TimeSpan.Zero;
				this.totalClientRPCLatency = TimeSpan.Zero;
				this.totalCasRPCProcessingTime = TimeSpan.Zero;
				this.totalMbxRPCProcessingTime = TimeSpan.Zero;
				this.clientPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);
				this.casPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);
				this.mbxPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);
			}

			// Token: 0x040001C2 RID: 450
			internal const long MaxValue = 2000L;

			// Token: 0x040001C3 RID: 451
			internal const long BinSize = 10L;

			// Token: 0x040001C4 RID: 452
			private const string LogLineFormat = "ClientRPCCount={0};AvgClientLatency={1};ServerRPCCount={2};AvgCasRPCProcessingTime={3};AvgMbxProcessingTime={4};MaxCasRPCProcessingTime={5}";

			// Token: 0x040001C5 RID: 453
			private static readonly double[] Percentiles = new double[]
			{
				20.0,
				40.0,
				60.0,
				80.0,
				90.0,
				95.0,
				99.0,
				100.0
			};

			// Token: 0x040001C6 RID: 454
			private readonly object rpcProcessingTimeLock = new object();

			// Token: 0x040001C7 RID: 455
			private int serverRpcCount;

			// Token: 0x040001C8 RID: 456
			private int clientRpcCount;

			// Token: 0x040001C9 RID: 457
			private TimeSpan maxRpcProcessingTime = TimeSpan.Zero;

			// Token: 0x040001CA RID: 458
			private TimeSpan totalClientRPCLatency = TimeSpan.Zero;

			// Token: 0x040001CB RID: 459
			private TimeSpan totalCasRPCProcessingTime = TimeSpan.Zero;

			// Token: 0x040001CC RID: 460
			private TimeSpan totalMbxRPCProcessingTime = TimeSpan.Zero;

			// Token: 0x040001CD RID: 461
			private PercentileCounter clientPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);

			// Token: 0x040001CE RID: 462
			private PercentileCounter casPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);

			// Token: 0x040001CF RID: 463
			private PercentileCounter mbxPercentileCounter = new PercentileCounter(TimeSpan.MaxValue, TimeSpan.MaxValue, 10L, 2000L);
		}

		// Token: 0x02000039 RID: 57
		private class ThreadLocalData
		{
			// Token: 0x0600023E RID: 574 RVA: 0x00008684 File Offset: 0x00006884
			internal ThreadLocalData(ProtocolLog protocolLog)
			{
				this.Row = protocolLog.CreateRowFormatter();
				this.PersistentData = new Dictionary<ProtocolLog.Field, string>();
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600023F RID: 575 RVA: 0x000086AF File Offset: 0x000068AF
			public TimeSpan ElapsedTime
			{
				get
				{
					if (this.CurrentCallStartTickCount == null)
					{
						throw new InvalidOperationException("CurrentCallStartTickCount is not set");
					}
					return TimeSpan.FromMilliseconds(Environment.TickCount - this.CurrentCallStartTickCount.Value);
				}
			}

			// Token: 0x06000240 RID: 576 RVA: 0x000086E4 File Offset: 0x000068E4
			public void ClearPendingData()
			{
				this.ClearPerRecordData();
				for (int i = 0; i < 24; i++)
				{
					this.Row[i] = null;
				}
				this.CurrentCallStartTickCount = null;
			}

			// Token: 0x06000241 RID: 577 RVA: 0x00008720 File Offset: 0x00006920
			public void ClearPerRecordData()
			{
				for (int i = 0; i < 24; i++)
				{
					if (ProtocolLog.Fields[i].Scope == ProtocolLog.FieldScope.Record)
					{
						this.Row[i] = null;
					}
				}
				this.OperationSpecificData.Clear();
				this.HasInterestingDataToFlush = false;
			}

			// Token: 0x040001D0 RID: 464
			internal readonly LogRowFormatter Row;

			// Token: 0x040001D1 RID: 465
			internal readonly Dictionary<ProtocolLog.Field, string> PersistentData;

			// Token: 0x040001D2 RID: 466
			internal int? CurrentCallStartTickCount = null;

			// Token: 0x040001D3 RID: 467
			internal ProtocolLogSession.ThreadLocalData.DataAccumulator OperationSpecificData;

			// Token: 0x040001D4 RID: 468
			internal bool HasInterestingDataToFlush;

			// Token: 0x0200003A RID: 58
			internal struct DataAccumulator
			{
				// Token: 0x06000242 RID: 578 RVA: 0x00008771 File Offset: 0x00006971
				internal void Append(string value)
				{
					if (this.data == null)
					{
						this.data = new StringBuilder();
					}
					if (this.data.Length != 0)
					{
						this.data.Append("; ");
					}
					this.data.Append(value);
				}

				// Token: 0x06000243 RID: 579 RVA: 0x000087B4 File Offset: 0x000069B4
				internal string Detach()
				{
					string result = null;
					if (this.data != null && this.data.Length != 0)
					{
						result = this.data.ToString();
					}
					this.Clear();
					return result;
				}

				// Token: 0x06000244 RID: 580 RVA: 0x000087EB File Offset: 0x000069EB
				internal void Clear()
				{
					if (this.data != null)
					{
						this.data.Length = 0;
					}
				}

				// Token: 0x040001D5 RID: 469
				private const string DataDelimiter = "; ";

				// Token: 0x040001D6 RID: 470
				private StringBuilder data;
			}
		}
	}
}
