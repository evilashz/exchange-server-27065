using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.Logging.ConnectionLog
{
	// Token: 0x02000091 RID: 145
	internal class ConnectionLog
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00014FDE File Offset: 0x000131DE
		private static string[] PriorityNamesForActiveQueueCounts
		{
			get
			{
				if (!ConnectionLog.priorityNamesForQueueCountsInitialized)
				{
					ConnectionLog.InitializePriorityNamesForQueueCounts();
				}
				return ConnectionLog.priorityNamesForActiveQueueCounts;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00014FF1 File Offset: 0x000131F1
		private static string[] PriorityNamesForRetryQueueCounts
		{
			get
			{
				if (!ConnectionLog.priorityNamesForQueueCountsInitialized)
				{
					ConnectionLog.InitializePriorityNamesForQueueCounts();
				}
				return ConnectionLog.priorityNamesForRetryQueueCounts;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00015004 File Offset: 0x00013204
		internal static LogSchema Schema
		{
			get
			{
				if (ConnectionLog.schema == null)
				{
					LogSchema value = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Transport Connectivity Log", ConnectionLog.Row.Headers);
					Interlocked.CompareExchange<LogSchema>(ref ConnectionLog.schema, value, null);
				}
				return ConnectionLog.schema;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015053 File Offset: 0x00013253
		public static void Start()
		{
			ConnectionLog.CreateLog();
			ConnectionLog.Configure(Components.Configuration.LocalServer);
			ConnectionLog.RegisterConfigurationChangeHandlers();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001506E File Offset: 0x0001326E
		public static void Stop()
		{
			ConnectionLog.UnregisterConfigurationChangeHandlers();
			if (ConnectionLog.log != null)
			{
				ConnectionLog.log.Close();
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00015086 File Offset: 0x00013286
		public static void FlushBuffer()
		{
			if (ConnectionLog.log != null)
			{
				ConnectionLog.log.Flush();
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001509C File Offset: 0x0001329C
		public static void SmtpConnectionStart(ulong id, NextHopSolutionKey nextHop, int totalCount, int[] activeCountsPerPriority, int[] retryCountsPerPriority, string logMessage = null)
		{
			ConnectionLog.SmtpConnectionStart(id, nextHop, string.Format(CultureInfo.InvariantCulture, "{0} {1};QueueLength={2}. {3}", new object[]
			{
				nextHop.NextHopType,
				nextHop.NextHopConnector,
				ConnectionLog.GetFormattedQueueCountsForConnectionLog(totalCount, activeCountsPerPriority, retryCountsPerPriority),
				logMessage ?? string.Empty
			}));
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00015100 File Offset: 0x00013300
		public static void SmtpConnectionFailover(ulong newSessionid, ulong previousSessionId, string host, SessionSetupFailureReason reason)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			ConnectionLog.Row row = new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(newSessionid),
				Destination = (host ?? string.Empty),
				Direction = "*",
				Source = "SMTP",
				Description = string.Format("Session Failover; previous session id = {0}; reason = {1}", ConnectionLog.SessionIdToString(previousSessionId), reason)
			};
			row.Log("SmtpConnectionFailover");
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00015178 File Offset: 0x00013378
		public static void SmtpConnectionStart(ulong id, NextHopSolutionKey nextHop, string description)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = nextHop.NextHopDomain,
				Direction = "+",
				Source = "SMTP",
				Description = description
			}.Log("SmtpConnectionStart");
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000151D4 File Offset: 0x000133D4
		public static void SmtpConnectionStartCacheHit(ulong cachedSessionID, ulong oldSessionID, NextHopSolutionKey nextHop, int totalCount, int[] activeCountsPerPriority, int[] retryCountsPerPriority, string logMessage)
		{
			string description = string.Format("{0} {1} CachedSession Replacing {2};{3}. {4}", new object[]
			{
				nextHop.NextHopType.ToString(),
				nextHop.NextHopConnector.ToString(),
				ConnectionLog.SessionIdToString(oldSessionID),
				ConnectionLog.GetFormattedQueueCountsForConnectionLog(totalCount, activeCountsPerPriority, retryCountsPerPriority),
				logMessage
			});
			ConnectionLog.SmtpConnectionStart(cachedSessionID, nextHop, description);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00015248 File Offset: 0x00013448
		public static void SmtpHostResolved(ulong id, string host, TargetHost[] hosts, bool hostIsOutboundProxy)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			ConnectionLog.Row row = new ConnectionLog.Row();
			row.Session = ConnectionLog.SessionIdToString(id);
			row.Destination = host;
			row.Direction = ">";
			row.Source = "SMTP";
			int num = 256;
			StringBuilder stringBuilder = new StringBuilder(num);
			if (hostIsOutboundProxy)
			{
				stringBuilder.Append("Outbound proxy via ");
			}
			for (int i = 0; i < hosts.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(hosts[i].Name);
				stringBuilder.Append("[");
				int num2 = 0;
				foreach (IPAddress ipaddress in hosts[i].IPAddresses)
				{
					if (num2++ != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(ipaddress.ToString());
				}
				stringBuilder.Append("]");
				if (stringBuilder.Length > num)
				{
					stringBuilder.Length = num - 3;
					stringBuilder.Append("...");
					break;
				}
			}
			row.Description = stringBuilder.ToString();
			row.Log("SmtpHostResolved");
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00015394 File Offset: 0x00013594
		public static void SmtpHostResolutionFailed(ulong id, string host, IPAddress reportingServer, string reason, string diagnosticInfo)
		{
			ConnectionLog.SmtpHostResolutionFailed(id, host, reportingServer, reason, diagnosticInfo, false);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000153A2 File Offset: 0x000135A2
		public static void SmtpHostResolutionFailedForOutboundProxyFrontend(ulong id, string host, IPAddress reportingServer, string reason, string diagnosticInfo)
		{
			ConnectionLog.SmtpHostResolutionFailed(id, host, reportingServer, reason, diagnosticInfo, true);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000153B0 File Offset: 0x000135B0
		public static void SmtpConnected(ulong id, string host, IPAddress address)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "SMTP",
				Description = "Established connection to " + address.ToString()
			}.Log("SmtpConnected");
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015415 File Offset: 0x00013615
		public static void SmtpConnectionFailed(ulong id, string host, IPAddress address, ushort port, SocketException ex)
		{
			ConnectionLog.SmtpConnectionFailed(id, host, address, port, ex, null);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015424 File Offset: 0x00013624
		public static void SmtpConnectionFailed(ulong id, string host, IPAddress address, string targetHostName, ushort port, bool ipAddressMarkedUnhealthy, ExDateTime ipAddressNextRetryTime, int currentIpAddressConnectionCount, int currentIpAddressFailureCount, bool targetHostMarkedUnhealthy, ExDateTime targetHostRetryTime, int currentTargetHostConnectionCount, int currentTargetHostFailureCount, SocketException ex)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			string str = ConnectionLog.FormatTargetFailureStatus("TargetHost", targetHostName, port, targetHostMarkedUnhealthy, currentTargetHostConnectionCount, currentTargetHostFailureCount, targetHostRetryTime);
			string str2 = ConnectionLog.FormatTargetFailureStatus("TargetIPAddress", address.ToString(), port, ipAddressMarkedUnhealthy, currentIpAddressConnectionCount, currentIpAddressFailureCount, ipAddressNextRetryTime);
			ConnectionLog.SmtpConnectionFailed(id, host, address, port, ex, str + str2);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015480 File Offset: 0x00013680
		public static void SmtpConnectionStop(ulong id, string host, string description, ulong count, ulong bytes, ulong discardIds)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			ConnectionLog.Row row = new ConnectionLog.Row();
			row.Session = ConnectionLog.SessionIdToString(id);
			row.Destination = host;
			row.Direction = "-";
			row.Source = "SMTP";
			string text = string.Format(CultureInfo.InvariantCulture, "Messages: {0} Bytes: {1}", new object[]
			{
				count.ToString(NumberFormatInfo.InvariantInfo),
				bytes.ToString(NumberFormatInfo.InvariantInfo)
			});
			if (discardIds != 0UL)
			{
				text += string.Format(CultureInfo.InvariantCulture, " DiscardIDs: {0}", new object[]
				{
					discardIds.ToString(NumberFormatInfo.InvariantInfo)
				});
			}
			if (description != null)
			{
				text += string.Format(CultureInfo.InvariantCulture, " ({0})", new object[]
				{
					description
				});
			}
			row.Description = text;
			row.Log("SmtpConnectionStop");
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015568 File Offset: 0x00013768
		public static void SmtpConnectionStopDueToCacheHit(ulong sessionID, ulong cachedSessionID, string host)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(sessionID),
				Destination = host,
				Direction = "-",
				Source = "SMTP",
				Description = string.Format("Using Cached Session {0}", ConnectionLog.SessionIdToString(cachedSessionID))
			}.Log("SmtpConnectionStop");
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000155D0 File Offset: 0x000137D0
		public static void SmtpConnectionAborted(ulong id, string host, IPAddress address)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = "-",
				Source = "SMTP",
				Description = "Aborted connection to " + address.ToString()
			}.Log("SmtpConnectionAborted");
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015635 File Offset: 0x00013835
		public static void MapiDeliveryConnectionStart(ulong id, string mdb, string description)
		{
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, "+", description, "MapiDeliveryConnectionStart");
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00015649 File Offset: 0x00013849
		public static void MapiDeliveryConnectionStartingDelivery(ulong id, string mdb)
		{
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, ">", "Starting delivery", "MapiDeliveryConnectionStartingDelivery");
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00015664 File Offset: 0x00013864
		public static void MapiDeliveryConnectionServerThreadLimitReached(string mdb, string serverFqdn, int limit)
		{
			ConnectionLog.MapiDeliveryConnectionLog(0UL, mdb, "*", string.Concat(new object[]
			{
				"Throttled delivery due to server limit for ",
				serverFqdn,
				" with threshold ",
				limit
			}), "MapiDeliveryConnectionServerThreadLimitReached");
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000156AD File Offset: 0x000138AD
		public static void MapiDeliveryConnectionServerConnect(ulong id, string mdb, string serverFqdn, string sessionType)
		{
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, ">", "Connecting to server " + serverFqdn + " session type " + sessionType, "MapiDeliveryConnectionServerConnect");
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000156D1 File Offset: 0x000138D1
		public static void MapiDeliveryConnectionMdbThreadLimitReached(string mdb, int limit)
		{
			ConnectionLog.MapiDeliveryConnectionLog(0UL, mdb, "*", "Throttled delivery due to MDB limit " + limit, "MapiDeliveryConnectionMdbThreadLimitReached");
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000156F5 File Offset: 0x000138F5
		public static void MapiDeliveryConnectionRecipientThreadLimitReached(ulong sessionId, RoutingAddress routingAddress, string mdb, int limit)
		{
			ConnectionLog.MapiDeliveryConnectionLog(sessionId, mdb, ">", string.Format("Throttled delivery for recipient {0} due to concurrency limit {1}", routingAddress.ToString(), limit), "MapiDeliveryConnectionRecipientThreadLimitReached");
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00015725 File Offset: 0x00013925
		public static void MapiDeliveryConnectionServerNotFound(string mdb, string message)
		{
			ConnectionLog.MapiDeliveryConnectionLog(0UL, mdb, "*", "Failed to locate server: " + message, "MapiDeliveryConnectionServerNotFound");
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015744 File Offset: 0x00013944
		public static void MapiDeliveryConnectionServerConnectFailed(ulong id, string mdb, string serverFqdn)
		{
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, ">", "Failed to connect to server " + serverFqdn, "MapiDeliveryConnectionServerConnectFailed");
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00015762 File Offset: 0x00013962
		public static void MapiDeliveryConnectionRetired(ulong id, string mdb)
		{
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, "-", "Retired", "MapiDeliveryConnectionRetired");
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001577C File Offset: 0x0001397C
		public static void MapiDeliveryConnectionStop(ulong id, string mdb, ulong count, ulong bytes, ulong recipientCount)
		{
			string description = string.Concat(new string[]
			{
				"Messages: ",
				count.ToString(NumberFormatInfo.InvariantInfo),
				" Bytes: ",
				bytes.ToString(NumberFormatInfo.InvariantInfo),
				" Recipients: ",
				recipientCount.ToString(NumberFormatInfo.InvariantInfo)
			});
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, "-", description, "MapiDeliveryConnectionStop");
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000157F0 File Offset: 0x000139F0
		public static void MapiDeliveryConnectionLost(ulong id, string mdb, string description, ulong count, ulong bytes, ulong recipientCount)
		{
			description = string.Concat(new string[]
			{
				"Messages: ",
				count.ToString(NumberFormatInfo.InvariantInfo),
				" Bytes: ",
				bytes.ToString(NumberFormatInfo.InvariantInfo),
				" Recipients: ",
				recipientCount.ToString(NumberFormatInfo.InvariantInfo),
				" ",
				description
			});
			ConnectionLog.MapiDeliveryConnectionLog(id, mdb, "-", description, "MapiDeliveryConnectionLost");
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015870 File Offset: 0x00013A70
		public static void MapiDeliveryConnectionDeliveryQueueStats(Dictionary<QueueStatus, ConnectionLog.AggregateQueueStats> queueStats)
		{
			string text = "Queues: ";
			foreach (KeyValuePair<QueueStatus, ConnectionLog.AggregateQueueStats> keyValuePair in queueStats)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					keyValuePair.Key,
					":ActiveMsgs=",
					keyValuePair.Value.ActiveMessageCount,
					"/RetryMsgs=",
					keyValuePair.Value.RetryMessageCount,
					"/Queues=",
					keyValuePair.Value.QueueCount,
					";"
				});
			}
			ConnectionLog.MapiDeliveryConnectionLog(0UL, string.Empty, "*", text, "MapiDeliveryConnectionDeliveryQueueStats");
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00015968 File Offset: 0x00013B68
		public static void MapiSubmissionConnectionStart(ulong id, string mdb, string description)
		{
			ConnectionLog.MapiSubmissionConnectionLog(id, mdb, "+", description, "MapiSubmissionConnectionStart");
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001597C File Offset: 0x00013B7C
		public static void MapiSubmissionAborted(ulong id, string mdb, string description)
		{
			ConnectionLog.MapiSubmissionConnectionLog(id, mdb, ">", "Aborted; " + description, "MapiSubmissionConnectionAborted");
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001599A File Offset: 0x00013B9A
		public static void MapiSubmissionFailed(ulong id, string mdb, string description)
		{
			ConnectionLog.MapiSubmissionConnectionLog(id, mdb, ">", "Failed; " + description, "MapiSubmissionConnectionFailed");
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000159B8 File Offset: 0x00013BB8
		public static void MapiSubmissionConnectionStop(ulong id, string mdb, long totalSubmissions, long shadowSubmissions, long bytes, long recipientCount, int failures, bool reachedLimit, bool idle)
		{
			string description = string.Format(CultureInfo.InvariantCulture, "RegularSubmissions: {0} ShadowSubmissions: {1} Bytes: {2} Recipients: {3} Failures: {4} ReachedLimit: {5} Idle: {6}", new object[]
			{
				totalSubmissions,
				shadowSubmissions,
				bytes,
				recipientCount,
				failures,
				reachedLimit,
				idle
			});
			ConnectionLog.MapiSubmissionConnectionLog(id, mdb, "-", description, "MapiSubmissionConnectionStop");
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00015A34 File Offset: 0x00013C34
		public static void AggregationConnectionStart(string sourceComponent, string sessionId, string destination, Guid subscription)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = sessionId,
				Destination = destination,
				Direction = "+",
				Source = sourceComponent,
				Description = subscription.ToString()
			}.Log("AggregationConnectionStart");
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015A90 File Offset: 0x00013C90
		public static void AggregationConnectionStop(string sourceComponent, string sessionId, string destination, ulong countMessages)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = sessionId,
				Destination = destination,
				Direction = "-",
				Source = sourceComponent,
				Description = "Messages: " + countMessages.ToString(NumberFormatInfo.InvariantInfo)
			}.Log("AggregationConnectionStop");
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public static void DeliveryAgentStart(ulong id, string agentName, NextHopSolutionKey nextHop)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = nextHop.NextHopDomain,
				Direction = "+",
				Source = "DeliveryAgent",
				Description = string.Format("DeliveryAgent {0} invoked for connector {1}", agentName, nextHop.NextHopConnector)
			}.Log("DeliveryAgentStart");
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015B68 File Offset: 0x00013D68
		public static void DeliveryAgentConnected(ulong id, string remoteHost, SmtpResponse smtpResponse)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = remoteHost,
				Direction = ">",
				Source = "DeliveryAgent",
				Description = string.Format("Connection to {0} has succeeded with status {1}", remoteHost, smtpResponse)
			}.Log("DeliveryAgentConnected");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public static void DeliveryAgentConnectionFailed(ulong id, string host)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "DeliveryAgent",
				Description = "Failed to create a connection"
			}.Log("DeliveryAgentConnectionFailed");
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015C2C File Offset: 0x00013E2C
		public static void DeliveryAgentPermanentFailure(ulong id, string host, SmtpResponse smtpResponse)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "DeliveryAgent",
				Description = string.Format("Connection with remote host encountered a permanent failure with status {0}", smtpResponse)
			}.Log("DeliveryAgentPermanentFailure");
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015C94 File Offset: 0x00013E94
		public static void DeliveryAgentQueueRetry(ulong id, string host, SmtpResponse smtpResponse)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "DeliveryAgent",
				Description = string.Format("Connection with remote host encountered a transient failure with status {0}", smtpResponse)
			}.Log("DeliveryAgentQueueRetry");
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00015CFC File Offset: 0x00013EFC
		public static void DeliveryAgentDisconnected(ulong id, string host, SmtpResponse smtpResponse)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "DeliveryAgent",
				Description = string.Format("Connection ended with status {0}", smtpResponse)
			}.Log("DeliveryAgentDisconnected");
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015D64 File Offset: 0x00013F64
		public static void DeliveryAgentStop(ulong id, string host, int messages, long bytes)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = "-",
				Source = "DeliveryAgent",
				Description = string.Format("Messages:{0} Bytes:{1}", messages.ToString(NumberFormatInfo.InvariantInfo), bytes.ToString(NumberFormatInfo.InvariantInfo))
			}.Log("DeliveryAgentStop");
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00015DDC File Offset: 0x00013FDC
		public static void TransportStart(int maxConcurrentSubmissions, int maxConcurrentDeliveries, string maxSmtpOutConnections)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Direction = "*",
				Source = "Transport",
				Description = string.Concat(new object[]
				{
					"service started; MaxConcurrentSubmissions=",
					maxConcurrentSubmissions,
					"; MaxConcurrentDeliveries=",
					maxConcurrentDeliveries,
					"; MaxSmtpOutConnections=",
					maxSmtpOutConnections
				})
			}.Log("TransportServiceStart");
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015E5C File Offset: 0x0001405C
		public static void TransportStop()
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Direction = "*",
				Source = "Transport",
				Description = "service stopped"
			}.Log("TransportServiceStop");
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015EA3 File Offset: 0x000140A3
		internal static void StartTest(string directory)
		{
			ConnectionLog.CreateLog();
			ConnectionLog.enabled = true;
			ConnectionLog.log.Configure(directory, TimeSpan.FromDays(1.0), 0L, 0L, 0, TimeSpan.MaxValue, TimeSpan.FromSeconds(1.0));
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015EE1 File Offset: 0x000140E1
		internal static string SessionIdToString(ulong sessionId)
		{
			return sessionId.ToString("X16", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015EF4 File Offset: 0x000140F4
		private static void InitializePriorityNamesForQueueCounts()
		{
			ConnectionLog.priorityNamesForActiveQueueCounts = new string[QueueManager.PriorityToInstanceIndexMap.Count];
			ConnectionLog.priorityNamesForRetryQueueCounts = new string[QueueManager.PriorityToInstanceIndexMap.Count];
			foreach (DeliveryPriority key in QueueManager.PriorityToInstanceIndexMap.Keys)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				switch (key)
				{
				case DeliveryPriority.High:
					text = "AH";
					text2 = "RH";
					break;
				case DeliveryPriority.Normal:
					text = "AN";
					text2 = "RN";
					break;
				case DeliveryPriority.Low:
					text = "AL";
					text2 = "RL";
					break;
				case DeliveryPriority.None:
					text = "ANo";
					text2 = "RNo";
					break;
				}
				ConnectionLog.priorityNamesForActiveQueueCounts[QueueManager.PriorityToInstanceIndexMap[key]] = text;
				ConnectionLog.priorityNamesForRetryQueueCounts[QueueManager.PriorityToInstanceIndexMap[key]] = text2;
			}
			ConnectionLog.priorityNamesForQueueCountsInitialized = true;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015FFC File Offset: 0x000141FC
		private static string GetFormattedQueueCountsForConnectionLog(int totalCount, int[] activeCountsPerPriority, int[] retryCountsPerPriority)
		{
			if (activeCountsPerPriority == null || retryCountsPerPriority == null)
			{
				return "<no priority counts>";
			}
			if (ConnectionLog.PriorityNamesForActiveQueueCounts.Length != activeCountsPerPriority.Length || ConnectionLog.PriorityNamesForRetryQueueCounts.Length != retryCountsPerPriority.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("length of PriorityNamesForActiveQueueCounts:{0} don't match that of activeCountsPerPriority:{1} or length of PriorityNamesForRetryQueueCounts:{2} don't match that of retryCountsPerPriority:{3}", new object[]
				{
					ConnectionLog.PriorityNamesForActiveQueueCounts.Length,
					activeCountsPerPriority.Length,
					ConnectionLog.PriorityNamesForRetryQueueCounts.Length,
					retryCountsPerPriority.Length
				}));
			}
			StringBuilder stringBuilder = new StringBuilder(string.Format("TQ={0};", totalCount));
			stringBuilder.Append(ConnectionLog.GetFormattedIndividualQueueCounts(activeCountsPerPriority, ConnectionLog.PriorityNamesForActiveQueueCounts));
			stringBuilder.Append(ConnectionLog.GetFormattedIndividualQueueCounts(retryCountsPerPriority, ConnectionLog.PriorityNamesForRetryQueueCounts));
			return stringBuilder.ToString();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000160BC File Offset: 0x000142BC
		private static string GetFormattedIndividualQueueCounts(int[] countsPerPriority, string[] namesPerPriority)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			foreach (string value in namesPerPriority)
			{
				if (countsPerPriority[num] > 0)
				{
					if (flag)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value);
					stringBuilder.Append("=");
					stringBuilder.Append(countsPerPriority[num]);
					flag = true;
				}
				num++;
			}
			if (flag)
			{
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001613D File Offset: 0x0001433D
		private static void CreateLog()
		{
			ConnectionLog.log = new AsyncLog("CONNECTLOG", new LogHeaderFormatter(ConnectionLog.Schema), "ConnectivityLog");
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001615D File Offset: 0x0001435D
		private static void RegisterConfigurationChangeHandlers()
		{
			Components.Configuration.LocalServerChanged += ConnectionLog.Configure;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00016175 File Offset: 0x00014375
		private static void UnregisterConfigurationChangeHandlers()
		{
			Components.Configuration.LocalServerChanged -= ConnectionLog.Configure;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00016190 File Offset: 0x00014390
		private static void Configure(TransportServerConfiguration server)
		{
			Server transportServer = server.TransportServer;
			ConnectionLog.enabled = transportServer.ConnectivityLogEnabled;
			if (ConnectionLog.enabled)
			{
				if (transportServer.ConnectivityLogPath == null || string.IsNullOrEmpty(transportServer.ConnectivityLogPath.PathName))
				{
					ConnectionLog.enabled = false;
					return;
				}
				ConnectionLog.log.Configure(transportServer.ConnectivityLogPath.PathName, transportServer.ConnectivityLogMaxAge, (long)(transportServer.ConnectivityLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.ConnectivityLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.ConnectivityLogMaxFileSize.IsUnlimited ? 0UL : transportServer.ConnectivityLogMaxFileSize.Value.ToBytes()), Components.TransportAppConfig.Logging.ConnectivityLogBufferSize, Components.TransportAppConfig.Logging.ConnectivityLogFlushInterval, Components.TransportAppConfig.Logging.ConnectivityLogAsyncInterval);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016284 File Offset: 0x00014484
		private static string FormatTargetFailureStatus(string targetType, string targetName, ushort targetPort, bool targetMarkedUnhealthy, int currentTargetConnectionCount, int currentTargetFailureCount, ExDateTime nextRetryTime)
		{
			string result;
			if (targetMarkedUnhealthy)
			{
				result = string.Format(CultureInfo.InvariantCulture, "[{0}:{1}:{2}|MarkedUnhealthy|FailureCount:{3}|NextRetryTime:{4}]", new object[]
				{
					targetType,
					targetName,
					targetPort,
					currentTargetFailureCount,
					nextRetryTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo)
				});
			}
			else if (currentTargetConnectionCount != -2147483648)
			{
				result = string.Format(CultureInfo.InvariantCulture, "[{0}:{1}:{2}|NotMarkedUnhealthy|ActiveConnectionCount:{3}]", new object[]
				{
					targetType,
					targetName,
					targetPort,
					currentTargetConnectionCount
				});
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016324 File Offset: 0x00014524
		private static void SmtpConnectionFailed(ulong id, string host, IPAddress address, ushort port, SocketException ex, string additionalDescr)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ConnectionLog.SessionIdToString(id),
				Destination = host,
				Direction = ">",
				Source = "SMTP",
				Description = string.Format(CultureInfo.InvariantCulture, "Failed connection to {0}:{1} ({2}:{3}){4}", new object[]
				{
					address,
					port,
					ex.SocketErrorCode.ToString(),
					ex.ErrorCode.ToString("X8", NumberFormatInfo.InvariantInfo),
					additionalDescr ?? string.Empty
				})
			}.Log("SmtpConnectionFailed");
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000163DC File Offset: 0x000145DC
		private static void MapiDeliveryConnectionLog(ulong id, string mdb, string direction, string description, string component)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ((id == 0UL) ? string.Empty : ConnectionLog.SessionIdToString(id)),
				Destination = mdb,
				Direction = direction,
				Source = "MapiDelivery",
				Description = description
			}.Log(component);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016438 File Offset: 0x00014638
		private static void MapiSubmissionConnectionLog(ulong id, string mdb, string direction, string description, string component)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			new ConnectionLog.Row
			{
				Session = ((id == 0UL) ? string.Empty : ConnectionLog.SessionIdToString(id)),
				Destination = mdb,
				Direction = direction,
				Source = "MapiSubmission",
				Description = description
			}.Log(component);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016494 File Offset: 0x00014694
		private static void SmtpHostResolutionFailed(ulong id, string host, IPAddress reportingServer, string reason, string diagnosticInfo, bool nextHopIsOutboundProxyFrontend)
		{
			if (!ConnectionLog.enabled)
			{
				return;
			}
			ConnectionLog.Row row = new ConnectionLog.Row();
			row.Session = ConnectionLog.SessionIdToString(id);
			row.Destination = host;
			row.Direction = ">";
			row.Source = "SMTP";
			if (nextHopIsOutboundProxyFrontend)
			{
				row.Description = string.Format("{0} reported for the outbound proxy frontend server fqdn by {1}. {2}", reason, reportingServer, diagnosticInfo);
			}
			else
			{
				row.Description = string.Format("{0} reported by {1}. {2}", reason, reportingServer, diagnosticInfo);
			}
			row.Log("SmtpHostResolutionFailed");
		}

		// Token: 0x0400029B RID: 667
		private const string LogComponentName = "ConnectivityLog";

		// Token: 0x0400029C RID: 668
		private static LogSchema schema;

		// Token: 0x0400029D RID: 669
		private static AsyncLog log;

		// Token: 0x0400029E RID: 670
		private static bool enabled;

		// Token: 0x0400029F RID: 671
		private static string[] priorityNamesForActiveQueueCounts;

		// Token: 0x040002A0 RID: 672
		private static string[] priorityNamesForRetryQueueCounts;

		// Token: 0x040002A1 RID: 673
		private static bool priorityNamesForQueueCountsInitialized;

		// Token: 0x02000092 RID: 146
		internal class AggregateQueueStats
		{
			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000542 RID: 1346 RVA: 0x00016519 File Offset: 0x00014719
			// (set) Token: 0x06000543 RID: 1347 RVA: 0x00016521 File Offset: 0x00014721
			internal int ActiveMessageCount { get; set; }

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001652A File Offset: 0x0001472A
			// (set) Token: 0x06000545 RID: 1349 RVA: 0x00016532 File Offset: 0x00014732
			internal int RetryMessageCount { get; set; }

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001653B File Offset: 0x0001473B
			// (set) Token: 0x06000547 RID: 1351 RVA: 0x00016543 File Offset: 0x00014743
			internal int QueueCount { get; set; }

			// Token: 0x06000548 RID: 1352 RVA: 0x0001654C File Offset: 0x0001474C
			internal AggregateQueueStats(int activeMessageCount, int retryMessageCount)
			{
				this.ActiveMessageCount = activeMessageCount;
				this.RetryMessageCount = retryMessageCount;
				this.QueueCount = 1;
			}
		}

		// Token: 0x02000093 RID: 147
		internal struct Direction
		{
			// Token: 0x040002A5 RID: 677
			public const string StartConnection = "+";

			// Token: 0x040002A6 RID: 678
			public const string ExistingConnection = ">";

			// Token: 0x040002A7 RID: 679
			public const string Information = "*";

			// Token: 0x040002A8 RID: 680
			public const string StopConnection = "-";
		}

		// Token: 0x02000094 RID: 148
		private struct Source
		{
			// Token: 0x040002A9 RID: 681
			public const string Smtp = "SMTP";

			// Token: 0x040002AA RID: 682
			public const string MapiDelivery = "MapiDelivery";

			// Token: 0x040002AB RID: 683
			public const string MapiSubmission = "MapiSubmission";

			// Token: 0x040002AC RID: 684
			public const string DeliveryAgent = "DeliveryAgent";

			// Token: 0x040002AD RID: 685
			public const string Transport = "Transport";
		}

		// Token: 0x02000095 RID: 149
		private sealed class Row : LogRowFormatter
		{
			// Token: 0x06000549 RID: 1353 RVA: 0x00016569 File Offset: 0x00014769
			public Row() : base(ConnectionLog.Schema)
			{
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x00016578 File Offset: 0x00014778
			public static string[] Headers
			{
				get
				{
					if (ConnectionLog.Row.headers == null)
					{
						string[] array = new string[Enum.GetValues(typeof(ConnectionLog.Row.Field)).Length];
						array[0] = "date-time";
						array[1] = "session";
						array[2] = "source";
						array[3] = "Destination";
						array[4] = "direction";
						array[5] = "description";
						Interlocked.CompareExchange<string[]>(ref ConnectionLog.Row.headers, array, null);
					}
					return ConnectionLog.Row.headers;
				}
			}

			// Token: 0x1700012F RID: 303
			// (set) Token: 0x0600054B RID: 1355 RVA: 0x000165E8 File Offset: 0x000147E8
			public string Source
			{
				set
				{
					base[2] = value;
				}
			}

			// Token: 0x17000130 RID: 304
			// (set) Token: 0x0600054C RID: 1356 RVA: 0x000165F2 File Offset: 0x000147F2
			public string Session
			{
				set
				{
					base[1] = value;
				}
			}

			// Token: 0x17000131 RID: 305
			// (set) Token: 0x0600054D RID: 1357 RVA: 0x000165FC File Offset: 0x000147FC
			public string Destination
			{
				set
				{
					base[3] = value;
				}
			}

			// Token: 0x17000132 RID: 306
			// (set) Token: 0x0600054E RID: 1358 RVA: 0x00016606 File Offset: 0x00014806
			public string Direction
			{
				set
				{
					base[4] = value;
				}
			}

			// Token: 0x17000133 RID: 307
			// (set) Token: 0x0600054F RID: 1359 RVA: 0x00016610 File Offset: 0x00014810
			public string Description
			{
				set
				{
					base[5] = value;
				}
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x0001661C File Offset: 0x0001481C
			public void Log(string component)
			{
				try
				{
					ConnectionLog.log.Append(this, 0);
				}
				catch (ObjectDisposedException)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Connection log " + component + " append failed with ObjectDisposedException");
				}
			}

			// Token: 0x040002AE RID: 686
			private static string[] headers;

			// Token: 0x02000096 RID: 150
			public enum Field
			{
				// Token: 0x040002B0 RID: 688
				Time,
				// Token: 0x040002B1 RID: 689
				Session,
				// Token: 0x040002B2 RID: 690
				Source,
				// Token: 0x040002B3 RID: 691
				Destination,
				// Token: 0x040002B4 RID: 692
				Direction,
				// Token: 0x040002B5 RID: 693
				Description
			}
		}
	}
}
