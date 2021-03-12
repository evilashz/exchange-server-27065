using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DiagnosticsAggregation;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.QueueViewer;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.DiagnosticsAggregationService
{
	// Token: 0x02000156 RID: 342
	internal static class DiagnosticsAggregationHelper
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x00039B5C File Offset: 0x00037D5C
		public static bool TryGetParsedQueueInfo(LocalLongFullPath logPath, out QueueAggregationInfo queueAggregationInfo, out Exception exception)
		{
			queueAggregationInfo = null;
			exception = null;
			if (logPath == null || string.IsNullOrEmpty(logPath.PathName))
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError(0L, "LogPath is not configured");
				exception = new ApplicationException("LogPath is not configured");
				return false;
			}
			try
			{
				string snapshotFileFullPath = DiagnosticsAggregationHelper.GetSnapshotFileFullPath(logPath.PathName);
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(QueueAggregationInfo));
				using (FileStream fileStream = File.OpenRead(snapshotFileFullPath))
				{
					queueAggregationInfo = (safeXmlSerializer.Deserialize(fileStream) as QueueAggregationInfo);
					return true;
				}
			}
			catch (ArgumentException ex)
			{
				exception = ex;
			}
			catch (NotSupportedException ex2)
			{
				exception = ex2;
			}
			catch (IOException ex3)
			{
				exception = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				exception = ex4;
			}
			catch (InvalidOperationException ex5)
			{
				exception = ex5;
			}
			catch (XmlException ex6)
			{
				exception = ex6;
			}
			if (exception != null)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<Exception>(0L, "Deserializing queue information failed. Details: {0}", exception);
			}
			return false;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00039C88 File Offset: 0x00037E88
		public static void LogQueueInfo(LocalLongFullPath logPath)
		{
			if (logPath == null || string.IsNullOrEmpty(logPath.PathName))
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError(0L, "Queue log path is not configured");
				return;
			}
			string snapshotFileFullPath = DiagnosticsAggregationHelper.GetSnapshotFileFullPath(logPath.PathName);
			if (!DiagnosticsAggregationHelper.TryCreateDirectory(snapshotFileFullPath))
			{
				return;
			}
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceDebug<string>(0L, "Collecting queue info. {0}", snapshotFileFullPath);
			QueueAggregationInfo allInterestingQueues = DiagnosticsAggregationHelper.GetAllInterestingQueues();
			allInterestingQueues.Time = DateTime.UtcNow;
			int num = 0;
			while (num < 2 && !DiagnosticsAggregationHelper.TrySerializeQueueInfoToFile(snapshotFileFullPath, allInterestingQueues))
			{
				num++;
			}
			QueueLog.Log(allInterestingQueues);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00039D10 File Offset: 0x00037F10
		public static HashSet<ADObjectId> GetGroupForServer(Server localServer, ITopologyConfigurationSession session)
		{
			HashSet<ADObjectId> hashSet;
			if (localServer.DatabaseAvailabilityGroup != null)
			{
				DatabaseAvailabilityGroup databaseAvailabilityGroup = session.Read<DatabaseAvailabilityGroup>(localServer.DatabaseAvailabilityGroup);
				hashSet = ((databaseAvailabilityGroup != null && databaseAvailabilityGroup.Servers != null) ? new HashSet<ADObjectId>(databaseAvailabilityGroup.Servers) : new HashSet<ADObjectId>());
			}
			else
			{
				if (localServer.ServerSite != null)
				{
					ADPagedReader<Server> adpagedReader = session.FindPaged<Server>(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
					{
						new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
						new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, localServer.ServerSite),
						new NotFilter(new ExistsFilter(ServerSchema.DatabaseAvailabilityGroup)),
						DiagnosticsAggregationHelper.IsE14OrHigherQueryFilter
					}), null, 0);
					hashSet = new HashSet<ADObjectId>();
					using (IEnumerator<Server> enumerator = adpagedReader.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Server server = enumerator.Current;
							if (server.MajorVersion == localServer.MajorVersion)
							{
								hashSet.Add(server.Id);
							}
						}
						return hashSet;
					}
				}
				hashSet = new HashSet<ADObjectId>();
			}
			return hashSet;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00039E20 File Offset: 0x00038020
		private static bool TrySerializeQueueInfoToFile(string fullPath, QueueAggregationInfo queueObject)
		{
			bool result;
			try
			{
				using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
				{
					SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(QueueAggregationInfo));
					safeXmlSerializer.Serialize(fileStream, queueObject);
					result = true;
				}
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<ArgumentException>(0L, "Error occured while serializing queue info. Details: {0}", arg);
				result = false;
			}
			catch (InvalidOperationException arg2)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<InvalidOperationException>(0L, "Error occured while serializing queue info. Details: {0}", arg2);
				result = false;
			}
			catch (SecurityException arg3)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<SecurityException>(0L, "Error occured while serializing queue info. Details: {0}", arg3);
				result = false;
			}
			catch (IOException arg4)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<IOException>(0L, "Error occured while serializing queue info. Details: {0}", arg4);
				result = false;
			}
			catch (UnauthorizedAccessException arg5)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<UnauthorizedAccessException>(0L, "Error occured while serializing queue info. Details: {0}", arg5);
				result = false;
			}
			catch (XmlException arg6)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<XmlException>(0L, "Error occured while serializing queue info. Details: Unable to deserialize the data element. XmlException={0}", arg6);
				result = false;
			}
			return result;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00039F54 File Offset: 0x00038154
		private static QueueAggregationInfo GetAllInterestingQueues()
		{
			int num = 0;
			QueueAggregationInfo queueAggregationInfo = new QueueAggregationInfo();
			SubmitMessageQueue submitMessageQueue = Components.CategorizerComponent.SubmitMessageQueue;
			RemoteMessageQueue unreachableMessageQueue = RemoteDeliveryComponent.UnreachableMessageQueue;
			PoisonMessageQueue poisonMessageQueue = Components.QueueManager.PoisonMessageQueue;
			num += submitMessageQueue.TotalCount;
			num += unreachableMessageQueue.TotalCount;
			num += poisonMessageQueue.Count;
			if (submitMessageQueue.IsInterestingQueueToLog())
			{
				queueAggregationInfo.QueueInfo.Add(DiagnosticsAggregationHelper.GetLocalQueueInfo(QueueInfoFactory.NewSubmissionQueueInfo(), submitMessageQueue.Key));
			}
			if (unreachableMessageQueue.IsInterestingQueueToLog())
			{
				queueAggregationInfo.QueueInfo.Add(DiagnosticsAggregationHelper.GetLocalQueueInfo(QueueInfoFactory.NewUnreachableQueueInfo(), unreachableMessageQueue.Key));
			}
			if (poisonMessageQueue.Count > 0)
			{
				queueAggregationInfo.QueueInfo.Add(DiagnosticsAggregationHelper.GetLocalQueueInfo(QueueInfoFactory.NewPoisonQueueInfo(), NextHopSolutionKey.Empty));
			}
			foreach (RoutedMessageQueue routedMessageQueue in Components.RemoteDeliveryComponent.GetQueueArray())
			{
				num += routedMessageQueue.TotalCount;
				if (routedMessageQueue.IsInterestingQueueToLog())
				{
					queueAggregationInfo.QueueInfo.Add(DiagnosticsAggregationHelper.GetLocalQueueInfo(QueueInfoFactory.NewDeliveryQueueInfo(routedMessageQueue), routedMessageQueue.Key));
				}
			}
			queueAggregationInfo.TotalMessageCount = num;
			queueAggregationInfo.PoisonMessageCount = poisonMessageQueue.Count;
			return queueAggregationInfo;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003A078 File Offset: 0x00038278
		private static bool TryCreateDirectory(string fullPath)
		{
			string directoryName = Path.GetDirectoryName(fullPath);
			Exception ex = null;
			try
			{
				Log.CreateLogDirectory(directoryName);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (InvalidOperationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<string, Exception>(0L, "Creation of directory {0} failed. Details: {1}", directoryName, ex);
			}
			return ex == null;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003A0EC File Offset: 0x000382EC
		private static LocalQueueInfo GetLocalQueueInfo(ExtensibleQueueInfo queueInfo, NextHopSolutionKey nextHopSolutionKey)
		{
			LocalQueueInfo localQueueInfo = new LocalQueueInfo();
			localQueueInfo.MessageCount = queueInfo.MessageCount;
			localQueueInfo.DeferredMessageCount = queueInfo.DeferredMessageCount;
			localQueueInfo.DeliveryType = queueInfo.DeliveryType.ToString();
			localQueueInfo.LockedMessageCount = queueInfo.LockedMessageCount;
			localQueueInfo.NextHopDomain = queueInfo.NextHopDomain;
			localQueueInfo.NextHopKey = nextHopSolutionKey.ToShortString();
			localQueueInfo.QueueIdentity = queueInfo.QueueIdentity.ToString();
			localQueueInfo.ServerIdentity = queueInfo.QueueIdentity.Server;
			localQueueInfo.Status = queueInfo.Status.ToString();
			localQueueInfo.RiskLevel = queueInfo.RiskLevel.ToString();
			localQueueInfo.OutboundIPPool = queueInfo.OutboundIPPool.ToString();
			localQueueInfo.Velocity = queueInfo.Velocity;
			localQueueInfo.NextHopCategory = queueInfo.NextHopCategory.ToString();
			localQueueInfo.IncomingRate = queueInfo.IncomingRate;
			localQueueInfo.OutgoingRate = queueInfo.OutgoingRate;
			localQueueInfo.TlsDomain = queueInfo.TlsDomain;
			localQueueInfo.NextHopConnector = queueInfo.NextHopConnector;
			string lastError;
			if (LastError.TryParseSmtpResponseString(queueInfo.LastError, out lastError))
			{
				localQueueInfo.LastError = lastError;
			}
			else
			{
				localQueueInfo.LastError = queueInfo.LastError;
			}
			return localQueueInfo;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003A22B File Offset: 0x0003842B
		private static string GetSnapshotFileFullPath(string logPath)
		{
			return Path.Combine(logPath, "QueueSnapShot.xml");
		}

		// Token: 0x04000758 RID: 1880
		public static readonly string DiagnosticsAggregationEndpointFormat = "net.tcp://{0}:{1}/";

		// Token: 0x04000759 RID: 1881
		public static readonly QueryFilter IsE15OrHigherQueryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E15MinVersion);

		// Token: 0x0400075A RID: 1882
		public static readonly QueryFilter IsE14OrHigherQueryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E14MinVersion);
	}
}
