using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.Delivery;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000023 RID: 35
	internal sealed class DeliveryThrottling : DisposeTrackableBase, IDeliveryThrottling, IDisposable
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private DeliveryThrottling()
		{
			this.getMDBThreadLimitAndHealth = new GetMDBThreadLimitAndHealth(DeliveryThrottling.GetDatabaseThreadLimitAndHealth);
			this.deliveryThrottlingLog = new DeliveryThrottlingLog();
			this.deliveryThrottlingLog.Configure(DeliveryConfiguration.Instance.Throttling);
			this.deliveryThrottlingLogWorker = new DeliveryThrottlingLogWorker(this.deliveryThrottlingLog);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009B2C File Offset: 0x00007D2C
		internal DeliveryThrottling(GetMDBThreadLimitAndHealth getMDBThreadLimitAndHealth, IThrottlingConfig throttlingConfig, IDeliveryThrottlingLogWorker throttlingLogWorker)
		{
			this.getMDBThreadLimitAndHealth = getMDBThreadLimitAndHealth;
			this.deliveryThrottlingLog = new DeliveryThrottlingLog();
			this.deliveryThrottlingLog.Configure(throttlingConfig ?? DeliveryConfiguration.Instance.Throttling);
			this.deliveryThrottlingLogWorker = (throttlingLogWorker ?? new DeliveryThrottlingLogWorker(this.deliveryThrottlingLog));
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009B8C File Offset: 0x00007D8C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00009BEC File Offset: 0x00007DEC
		public static IDeliveryThrottling Instance
		{
			get
			{
				if (DeliveryThrottling.instance == null)
				{
					lock (DeliveryThrottling.syncRoot)
					{
						if (DeliveryThrottling.instance == null)
						{
							DeliveryThrottling.instance = new DeliveryThrottling();
						}
					}
				}
				return DeliveryThrottling.instance;
			}
			set
			{
				lock (DeliveryThrottling.syncRoot)
				{
					DeliveryThrottling.instance = value;
				}
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00009C30 File Offset: 0x00007E30
		public IMailboxDatabaseCollectionManager MailboxDatabaseCollectionManager
		{
			get
			{
				return this.mailboxDatabaseCollectionManager;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009C38 File Offset: 0x00007E38
		public IDeliveryThrottlingLog DeliveryThrottlingLog
		{
			get
			{
				return this.deliveryThrottlingLog;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009C40 File Offset: 0x00007E40
		public IDeliveryThrottlingLogWorker DeliveryThrottlingLogWorker
		{
			get
			{
				return this.deliveryThrottlingLogWorker;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00009C48 File Offset: 0x00007E48
		public XElement DeliveryServerDiagnostics
		{
			get
			{
				return DeliveryThrottling.deliveryServerThreadMap.GetDiagnosticInfo(new XElement("DeliveryServerThreadMap"));
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00009C64 File Offset: 0x00007E64
		public XElement DeliveryDatabaseDiagnostics
		{
			get
			{
				if (DeliveryConfiguration.Instance.Throttling.DynamicMailboxDatabaseThrottlingEnabled)
				{
					return this.mailboxDatabaseCollectionManager.GetDiagnosticInfo(new XElement("DeliveryDatabases"));
				}
				return DeliveryThrottling.deliveryDatabaseThreadMap.GetDiagnosticInfo(new XElement("DeliveryDatabaseThreadMap"));
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00009CB6 File Offset: 0x00007EB6
		public XElement DeliveryRecipientDiagnostics
		{
			get
			{
				return DeliveryThrottling.deliveryRecipientThreadMap.GetDiagnosticInfo(new XElement("DeliveryRecipientThreadMap"));
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00009CD1 File Offset: 0x00007ED1
		public GetMDBThreadLimitAndHealth GetMDBThreadLimitAndHealth
		{
			get
			{
				return this.getMDBThreadLimitAndHealth;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009CDC File Offset: 0x00007EDC
		public void ResetSession(long smtpSessionId)
		{
			ThrottleSession throttleSession = DeliveryThrottling.sessionMap.TryGetSession(smtpSessionId);
			if (throttleSession != null)
			{
				if (throttleSession.Recipients != null)
				{
					IList<RoutingAddress> list = throttleSession.Recipients.Keys.ToList<RoutingAddress>();
					foreach (RoutingAddress routingAddress in list)
					{
						int num = throttleSession.Recipients[routingAddress];
						for (int i = 0; i < num; i++)
						{
							this.DecrementRecipient(smtpSessionId, routingAddress);
						}
					}
				}
				lock (DeliveryThrottling.syncMessageSize)
				{
					DeliveryThrottling.DecrementSessionMessageSize(throttleSession);
				}
				if (throttleSession.Mdb != null)
				{
					DeliveryThrottling.deliveryDatabaseThreadMap.Decrement(throttleSession.Mdb.ToString());
					throttleSession.Mdb = null;
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public void ClearSession(long smtpSessionId)
		{
			ThrottleSession throttleSession = DeliveryThrottling.sessionMap.TryGetSession(smtpSessionId);
			if (throttleSession != null)
			{
				this.ResetSession(smtpSessionId);
				DeliveryThrottling.sessionMap.RemoveSession(smtpSessionId);
				DeliveryThrottling.deliveryServerThreadMap.Decrement("localhost");
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009E21 File Offset: 0x00008021
		public void DecrementRecipient(long smtpSessionId, RoutingAddress recipient)
		{
			DeliveryThrottling.sessionMap.RemoveRecipient(smtpSessionId, recipient);
			DeliveryThrottling.deliveryRecipientThreadMap.Decrement(recipient);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009E3C File Offset: 0x0000803C
		public void DecrementCurrentMessageSize(long smtpSessionId)
		{
			ThrottleSession throttleSession = DeliveryThrottling.sessionMap.TryGetSession(smtpSessionId);
			if (throttleSession != null)
			{
				lock (DeliveryThrottling.syncMessageSize)
				{
					DeliveryThrottling.DecrementSessionMessageSize(throttleSession);
				}
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00009E8C File Offset: 0x0000808C
		public bool CheckAndTrackThrottleServer(long smtpSessionId)
		{
			if (!DeliveryThrottling.deliveryServerThreadMap.TryCheckAndIncrement("localhost", (ulong)smtpSessionId, string.Empty))
			{
				DeliveryThrottling.Diag.TraceDebug<int, long>(0L, "Connection is skipped as it exceeds delivery server thread limit of {0}. Smtp session id: {1}", DeliveryThrottling.deliveryServerThreadMap.ThreadLimit, smtpSessionId);
				this.DeliveryThrottlingLogWorker.TrackMDBServerThrottle(true, (double)DeliveryThrottling.deliveryServerThreadMap.ThreadLimit);
				return false;
			}
			this.DeliveryThrottlingLogWorker.TrackMDBServerThrottle(false, (double)DeliveryThrottling.deliveryServerThreadMap.ThreadLimit);
			DeliveryThrottling.sessionMap.AddSession(smtpSessionId);
			return true;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009F08 File Offset: 0x00008108
		public void UpdateMdbThreadCounters()
		{
			if (DeliveryConfiguration.Instance.Throttling.DynamicMailboxDatabaseThrottlingEnabled)
			{
				this.mailboxDatabaseCollectionManager.UpdateMdbThreadCounters();
				return;
			}
			DeliveryThrottling.deliveryDatabaseThreadMap.UpdateMdbThreadCounters();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009F34 File Offset: 0x00008134
		public bool CheckAndTrackThrottleMDB(Guid databaseGuid, long smtpSessionId, out List<KeyValuePair<string, double>> mdbHealthMonitorValues)
		{
			mdbHealthMonitorValues = null;
			string text = databaseGuid.ToString();
			int num;
			if (!DeliveryThrottling.deliveryDatabaseThreadMap.TryCheckAndIncrement(text, this.getMDBThreadLimitAndHealth(databaseGuid, out num, out mdbHealthMonitorValues), (ulong)smtpSessionId))
			{
				DeliveryThrottling.Diag.TraceDebug<string, int>(0L, "Connection to database \"{0}\" is skipped as it exceeds delivery database thread limit of {1}", text, DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections);
				this.DeliveryThrottlingLogWorker.TrackMDBThrottle(true, text, (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads);
				return false;
			}
			this.DeliveryThrottlingLogWorker.TrackMDBThrottle(false, text, (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads);
			DeliveryThrottling.sessionMap.SetMdb(smtpSessionId, databaseGuid);
			return true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009FE0 File Offset: 0x000081E0
		public bool CheckAndTrackDynamicThrottleMDBPendingConnections(Guid databaseGuid, IMailboxDatabaseConnectionManager mdbConnectionManager, long smtpSessionId, IPAddress sessionRemoteEndPointAddress, out List<KeyValuePair<string, double>> mdbHealthMonitorValues)
		{
			mdbHealthMonitorValues = null;
			ArgumentValidator.ThrowIfNull("databaseGuid", databaseGuid);
			ArgumentValidator.ThrowIfNull("mdbConnectionManager", mdbConnectionManager);
			ArgumentValidator.ThrowIfNull("sessionRemoteEndPointAddress", sessionRemoteEndPointAddress);
			bool result;
			try
			{
				int num = -1;
				int num2 = -1;
				if (mdbConnectionManager.GetMdbHealthAndAddConnection(smtpSessionId, sessionRemoteEndPointAddress, out num, out mdbHealthMonitorValues, out num2))
				{
					DeliveryThrottling.Diag.TraceDebug<Guid, long, IPAddress>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection added. MDB {0} SeesionId {1} IP {2}", databaseGuid, smtpSessionId, sessionRemoteEndPointAddress);
					this.DeliveryThrottlingLogWorker.TrackMDBThrottle(false, databaseGuid.ToString(), (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads_MaxPerHub);
					mdbConnectionManager.UpdateLastActivityTime(smtpSessionId);
					result = true;
				}
				else
				{
					DeliveryThrottling.Diag.TraceDebug<long>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection from the remote IP address is already in the pending queue. SessionId {0}", smtpSessionId);
					this.DeliveryThrottlingLogWorker.TrackMDBThrottle(true, databaseGuid.ToString(), (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads_MaxPerHub);
					result = false;
				}
			}
			catch (InvalidOperationException ex)
			{
				DeliveryThrottling.Diag.TraceDebug<long, string>(0, (long)this.GetHashCode(), "Dynamic Throttling: Error attempting to add connection. SessionId {0} Error: {1}", smtpSessionId, ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A100 File Offset: 0x00008300
		public bool CheckAndTrackDynamicThrottleMDBTimeout(Guid databaseGuid, IMailboxDatabaseConnectionInfo mdbConnectionInfo, IMailboxDatabaseConnectionManager mdbConnectionManager, long smtpSessionId, IPAddress sessionRemoteEndPointAddress, TimeSpan connectionWaitTime, List<KeyValuePair<string, double>> mdbHealthMonitorValues)
		{
			ArgumentValidator.ThrowIfNull("databaseGuid", databaseGuid);
			ArgumentValidator.ThrowIfNull("mdbConnectionManager", mdbConnectionManager);
			ArgumentValidator.ThrowIfNull("sessionRemoteEndPointAddress", sessionRemoteEndPointAddress);
			if (!mdbConnectionManager.TryAcquire(smtpSessionId, sessionRemoteEndPointAddress, connectionWaitTime, out mdbConnectionInfo))
			{
				DeliveryThrottling.Diag.TraceDebug<Guid, long, IPAddress>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection was unable to be acquired. Dynamic Throttling Limit Exceeded. MDB {0} SessionId {1} IP {2}", databaseGuid, smtpSessionId, sessionRemoteEndPointAddress);
				this.DeliveryThrottlingLogWorker.TrackMDBThrottle(true, databaseGuid.ToString(), (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads_PendingConnectionTimedOut);
				return false;
			}
			DeliveryThrottling.Diag.TraceDebug<Guid, long, IPAddress>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection was acquired. MDB {0} SessionId {1} IP {2}", databaseGuid, smtpSessionId, sessionRemoteEndPointAddress);
			this.DeliveryThrottlingLogWorker.TrackMDBThrottle(false, databaseGuid.ToString(), (double)DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections, mdbHealthMonitorValues, ThrottlingResource.Threads_PendingConnectionTimedOut);
			return true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A1DC File Offset: 0x000083DC
		public bool CheckAndTrackThrottleRecipient(RoutingAddress recipient, long smtpSessionId, string mdbName, Guid tenantId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("recipient.ToString()", recipient.ToString());
			ArgumentValidator.ThrowIfNullOrEmpty("mdbName", mdbName);
			ArgumentValidator.ThrowIfNull("tenantId", tenantId);
			if (!DeliveryThrottling.deliveryRecipientThreadMap.TryCheckAndIncrement(recipient, (ulong)smtpSessionId, string.Empty))
			{
				DeliveryThrottling.Diag.TraceDebug<string, int>(0L, "Delivery for recipient \"{0}\" is skipped as it exceeds delivery recipient thread limit of {1}", recipient.ToString(), DeliveryConfiguration.Instance.Throttling.RecipientThreadLimit);
				this.DeliveryThrottlingLogWorker.TrackRecipientThrottle(true, Utils.RedactRoutingAddressIfNecessary(recipient, Utils.IsRedactionNecessary()), tenantId, mdbName, (double)DeliveryConfiguration.Instance.Throttling.RecipientThreadLimit);
				return false;
			}
			this.DeliveryThrottlingLogWorker.TrackRecipientThrottle(false, Utils.RedactRoutingAddressIfNecessary(recipient, Utils.IsRedactionNecessary()), tenantId, mdbName, (double)DeliveryConfiguration.Instance.Throttling.RecipientThreadLimit);
			DeliveryThrottling.sessionMap.AddRecipient(smtpSessionId, recipient);
			return true;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public void SetSessionMessageSize(long messageSize, long smtpSessionId)
		{
			ThrottleSession throttleSession = DeliveryThrottling.sessionMap.TryGetSession(smtpSessionId);
			if (throttleSession != null)
			{
				DeliveryThrottling.DecrementSessionMessageSize(throttleSession);
				if (messageSize < 0L)
				{
					messageSize = 0L;
				}
				throttleSession.MessageSize = messageSize;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public bool CheckAndTrackThrottleConcurrentMessageSizeLimit(long smtpSessionId, int numOfRecipients)
		{
			ThrottleSession throttleSession = DeliveryThrottling.sessionMap.TryGetSession(smtpSessionId);
			bool flag = false;
			if (throttleSession != null && throttleSession.MessageSize >= 0L)
			{
				ulong num = Convert.ToUInt64(throttleSession.MessageSize);
				lock (DeliveryThrottling.syncMessageSize)
				{
					if (DeliveryThrottling.totalConcurrentMessageSize < DeliveryThrottling.maxConcurrentMessageSizeLimit)
					{
						DeliveryThrottling.totalConcurrentMessageSize += num;
						flag = true;
					}
					else
					{
						throttleSession.MessageSize = 0L;
					}
				}
			}
			if (!flag)
			{
				DeliveryThrottling.Diag.TraceDebug<long, ulong>(0L, "Delivery for smtp session \"{0}\" is skipped as it exceeds max concurrent message size limit of {1}", smtpSessionId, DeliveryThrottling.maxConcurrentMessageSizeLimit);
				this.DeliveryThrottlingLogWorker.TrackConcurrentMessageSizeThrottle(true, DeliveryThrottling.maxConcurrentMessageSizeLimit, numOfRecipients);
			}
			else
			{
				this.DeliveryThrottlingLogWorker.TrackConcurrentMessageSizeThrottle(false, DeliveryThrottling.maxConcurrentMessageSizeLimit, numOfRecipients);
			}
			return flag;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A3BC File Offset: 0x000085BC
		private static void DecrementSessionMessageSize(ThrottleSession session)
		{
			ulong num = Convert.ToUInt64(session.MessageSize);
			if (DeliveryThrottling.totalConcurrentMessageSize >= num)
			{
				DeliveryThrottling.totalConcurrentMessageSize -= num;
			}
			else
			{
				DeliveryThrottling.totalConcurrentMessageSize = 0UL;
			}
			session.MessageSize = 0L;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A3FC File Offset: 0x000085FC
		public bool TryGetDatabaseHealth(Guid databaseGuid, out int health)
		{
			List<KeyValuePair<string, double>> list = null;
			return this.TryGetDatabaseHealth(databaseGuid, out health, out list);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A418 File Offset: 0x00008618
		public bool TryGetDatabaseHealth(Guid databaseGuid, out int health, out List<KeyValuePair<string, double>> monitorHealthValues)
		{
			health = -1;
			monitorHealthValues = null;
			if (databaseGuid != Guid.Empty)
			{
				ResourceLoadDelayInfo.Initialize();
				ResourceKey[] array = new ResourceKey[]
				{
					new MdbResourceHealthMonitorKey(databaseGuid),
					new MdbReplicationResourceHealthMonitorKey(databaseGuid)
				};
				ResourceLoad[] resourceLoadList = null;
				ResourceKey resourceKey;
				ResourceLoad resourceLoad;
				ResourceLoadDelayInfo.GetWorstResourceAndAllHealthValues(WorkloadType.Transport, array, out resourceLoadList, out resourceKey, out resourceLoad);
				if (resourceKey != null)
				{
					switch (resourceLoad.State)
					{
					case ResourceLoadState.Underloaded:
					case ResourceLoadState.Full:
						health = 100;
						break;
					case ResourceLoadState.Overloaded:
						health = (int)(100.0 / resourceLoad.LoadRatio);
						break;
					case ResourceLoadState.Critical:
						health = 0;
						break;
					}
					monitorHealthValues = this.GetMDBHealthMonitors(array, resourceLoadList);
					return true;
				}
			}
			else
			{
				DeliveryThrottling.Diag.TraceDebug<string>(0L, "Database Guid is empty for {0}.", StoreDriverDelivery.MailboxServerFqdn);
			}
			return false;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A4D8 File Offset: 0x000086D8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeliveryThrottling>(this);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A4E0 File Offset: 0x000086E0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.mailboxDatabaseCollectionManager != null)
				{
					this.mailboxDatabaseCollectionManager.Dispose();
					this.mailboxDatabaseCollectionManager = null;
				}
				if (this.deliveryThrottlingLog != null)
				{
					this.deliveryThrottlingLog.Close();
					this.deliveryThrottlingLog = null;
				}
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A519 File Offset: 0x00008719
		private static int GetDatabaseThreadLimitAndHealth(Guid databaseGuid, out int databaseHealthMeasure, out List<KeyValuePair<string, double>> monitorHealthValues)
		{
			if (!DeliveryConfiguration.Instance.Throttling.MailboxDeliveryThrottlingEnabled)
			{
				databaseHealthMeasure = -1;
				monitorHealthValues = null;
				return DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections;
			}
			DeliveryThrottling.Instance.TryGetDatabaseHealth(databaseGuid, out databaseHealthMeasure, out monitorHealthValues);
			return DeliveryThrottling.GetDatabaseThreadLimit(databaseHealthMeasure, databaseGuid);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A558 File Offset: 0x00008758
		internal static int GetDatabaseThreadLimit(int databaseHealthMeasure, Guid databaseGuid)
		{
			int maxMailboxDeliveryPerMdbConnections = DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections;
			Microsoft.Exchange.Diagnostics.Components.Data.Directory.ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3058052413U, ref maxMailboxDeliveryPerMdbConnections);
			if (databaseHealthMeasure == 100)
			{
				return maxMailboxDeliveryPerMdbConnections;
			}
			DeliveryThrottling.Diag.TraceDebug<Guid, int>(0L, "Health of database {0} is {1}", databaseGuid, databaseHealthMeasure);
			DatabaseHealthBreadcrumb databaseHealthBreadcrumb = new DatabaseHealthBreadcrumb();
			databaseHealthBreadcrumb.DatabaseHealth = databaseHealthMeasure;
			databaseHealthBreadcrumb.DatabaseGuid = databaseGuid;
			StoreDriverDeliveryDiagnostics.HealthHistory.Drop(databaseHealthBreadcrumb);
			if (databaseHealthMeasure == -1)
			{
				return maxMailboxDeliveryPerMdbConnections;
			}
			int num;
			if (databaseHealthMeasure > DeliveryConfiguration.Instance.Throttling.MdbHealthMediumToHighThreshold)
			{
				num = DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent;
			}
			else if (databaseHealthMeasure > DeliveryConfiguration.Instance.Throttling.MdbHealthLowToMediumThreshold)
			{
				num = DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent;
			}
			else if (databaseHealthMeasure > 0)
			{
				num = DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent;
			}
			else
			{
				num = DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent;
			}
			return (int)Math.Ceiling((double)(maxMailboxDeliveryPerMdbConnections * num) / 100.0);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A648 File Offset: 0x00008848
		private List<KeyValuePair<string, double>> GetMDBHealthMonitors(ResourceKey[] resourceKeyList, ResourceLoad[] resourceLoadList)
		{
			List<KeyValuePair<string, double>> list = null;
			if (resourceLoadList != null && resourceLoadList.Length > 0)
			{
				list = new List<KeyValuePair<string, double>>();
				for (int i = 0; i < resourceLoadList.Length; i++)
				{
					if (resourceLoadList[i] != ResourceLoad.Unknown)
					{
						list.Add(new KeyValuePair<string, double>(resourceKeyList[i].GetType().Name, resourceLoadList[i].LoadRatio));
					}
				}
			}
			return list;
		}

		// Token: 0x040000BD RID: 189
		private const string MailboxServerFqdn = "localhost";

		// Token: 0x040000BE RID: 190
		private static readonly Trace Diag = Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery.ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x040000BF RID: 191
		private static DeliveryServerThreadMap deliveryServerThreadMap = new DeliveryServerThreadMap(DeliveryThrottling.Diag);

		// Token: 0x040000C0 RID: 192
		private static DeliveryDatabaseThreadMap deliveryDatabaseThreadMap = new DeliveryDatabaseThreadMap(DeliveryThrottling.Diag);

		// Token: 0x040000C1 RID: 193
		private static DeliveryRecipientThreadMap deliveryRecipientThreadMap = new DeliveryRecipientThreadMap(DeliveryThrottling.Diag);

		// Token: 0x040000C2 RID: 194
		private static ThrottleSessionMap sessionMap = new ThrottleSessionMap();

		// Token: 0x040000C3 RID: 195
		private static volatile IDeliveryThrottling instance;

		// Token: 0x040000C4 RID: 196
		private static object syncRoot = new object();

		// Token: 0x040000C5 RID: 197
		private static object syncMessageSize = new object();

		// Token: 0x040000C6 RID: 198
		private static ulong totalConcurrentMessageSize = 0UL;

		// Token: 0x040000C7 RID: 199
		private static ulong maxConcurrentMessageSizeLimit = DeliveryConfiguration.Instance.Throttling.MaxConcurrentMessageSizeLimit;

		// Token: 0x040000C8 RID: 200
		private IMailboxDatabaseCollectionManager mailboxDatabaseCollectionManager = MailboxDatabaseCollectionManagerFactory.Create();

		// Token: 0x040000C9 RID: 201
		private GetMDBThreadLimitAndHealth getMDBThreadLimitAndHealth;

		// Token: 0x040000CA RID: 202
		private DeliveryThrottlingLog deliveryThrottlingLog;

		// Token: 0x040000CB RID: 203
		private readonly IDeliveryThrottlingLogWorker deliveryThrottlingLogWorker;
	}
}
