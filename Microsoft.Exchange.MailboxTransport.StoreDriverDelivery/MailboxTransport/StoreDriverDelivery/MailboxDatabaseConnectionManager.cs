using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000031 RID: 49
	internal class MailboxDatabaseConnectionManager : DisposeTrackableBase, IMailboxDatabaseConnectionManager, IDisposable
	{
		// Token: 0x06000248 RID: 584 RVA: 0x0000B860 File Offset: 0x00009A60
		public MailboxDatabaseConnectionManager(Guid mdbGuid, ThrottlingObjectPool<PooledEvent> eventPool)
		{
			this.mdbGuid = mdbGuid;
			this.eventPool = eventPool;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public bool AddConnection(long smtpSessionId, IPAddress remoteIPAddress)
		{
			int minValue = int.MinValue;
			List<KeyValuePair<string, double>> list = null;
			int num = -1;
			return this.GetMdbHealthAndAddConnection(smtpSessionId, remoteIPAddress, out minValue, out list, out num);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public bool GetMdbHealthAndAddConnection(long smtpSessionId, IPAddress remoteIPAddress, out int mdbHealthMeasure, out List<KeyValuePair<string, double>> healthMonitorList, out int currentConnectionLimit)
		{
			mdbHealthMeasure = -1;
			healthMonitorList = null;
			currentConnectionLimit = -1;
			if (remoteIPAddress == null)
			{
				throw new ArgumentNullException("remoteIPAddress");
			}
			lock (this.syncObject)
			{
				if (this.IsPending(remoteIPAddress))
				{
					return false;
				}
				currentConnectionLimit = DeliveryThrottling.Instance.GetMDBThreadLimitAndHealth(this.mdbGuid, out mdbHealthMeasure, out healthMonitorList);
				if (!this.sessionEvents.TryAdd(smtpSessionId, this.eventPool.Acquire()))
				{
					throw new InvalidOperationException("Unable to initialize synchronization events for the session.");
				}
				MailboxDatabaseConnectionManager.ConnectionInfo orAdd = this.connections.GetOrAdd(smtpSessionId, new MailboxDatabaseConnectionManager.ConnectionInfo(smtpSessionId, remoteIPAddress, ExDateTime.UtcNow, false));
				if (this.connections.Keys.Count <= currentConnectionLimit && this.pendingConnections.Count == 0)
				{
					orAdd.Active = true;
					this.SetSessionEvent(smtpSessionId);
				}
				else
				{
					this.pendingConnections.Enqueue(smtpSessionId);
					if (this.connections.Keys.Count <= currentConnectionLimit)
					{
						this.ActivateNextConnection();
					}
				}
			}
			return true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		public bool RemoveConnection(long smtpSessionId, IPAddress remoteIPAddress)
		{
			if (remoteIPAddress == null)
			{
				throw new ArgumentNullException("remoteIPAddress");
			}
			MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
			if (!this.connections.TryGetValue(smtpSessionId, out connectionInfo))
			{
				return false;
			}
			if (!remoteIPAddress.Equals(connectionInfo.RemoteIPAddress))
			{
				return false;
			}
			bool active = connectionInfo.Active;
			connectionInfo.Active = false;
			bool result = false;
			lock (this.syncObject)
			{
				result = this.RemoveConnectionTrackingItems(smtpSessionId);
				if (active)
				{
					this.ActivateNextConnection();
				}
			}
			return result;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000BA8C File Offset: 0x00009C8C
		public bool TryAcquire(long smtpSessionId, IPAddress remoteIPAddress, TimeSpan timeout, out IMailboxDatabaseConnectionInfo mdbConnectionInfo)
		{
			bool result;
			try
			{
				mdbConnectionInfo = this.Acquire(smtpSessionId, remoteIPAddress, timeout);
				result = true;
			}
			catch (InvalidOperationException)
			{
				mdbConnectionInfo = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public IMailboxDatabaseConnectionInfo Acquire(long smtpSessionId, IPAddress remoteIPAddress, TimeSpan timeout)
		{
			if (remoteIPAddress == null)
			{
				throw new ArgumentNullException("remoteIPAddress");
			}
			if (this.connections.Count == 0)
			{
				throw new InvalidOperationException("The specified mailbox database has no current connections.");
			}
			if (!this.connections.ContainsKey(smtpSessionId))
			{
				throw new InvalidOperationException("The specified session is not currently connected to this mailbox database.");
			}
			this.UpdateLastActivityTime(smtpSessionId);
			if (!this.WaitUsingEvent(smtpSessionId, timeout))
			{
				return null;
			}
			return new MailboxDatabaseConnectionInfo(this.mdbGuid, smtpSessionId, remoteIPAddress);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000BB34 File Offset: 0x00009D34
		public bool Release(ref IMailboxDatabaseConnectionInfo mdbConnectionInfo)
		{
			if (mdbConnectionInfo == null)
			{
				throw new ArgumentNullException("mdbConnectionInfo");
			}
			if (this.connections.Count == 0)
			{
				return false;
			}
			long smtpSessionId = mdbConnectionInfo.SmtpSessionId;
			mdbConnectionInfo = null;
			this.UpdateLastActivityTime(smtpSessionId);
			lock (this.syncObject)
			{
				this.DeactivateConnection(smtpSessionId);
				this.ActivateNextConnection();
			}
			return true;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public void ProcessStaleConnections(ExDateTime current, TimeSpan connectionMaxAge)
		{
			List<MailboxDatabaseConnectionManager.ConnectionInfo> list = new List<MailboxDatabaseConnectionManager.ConnectionInfo>();
			foreach (long key in this.connections.Keys)
			{
				MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
				if (this.connections.TryGetValue(key, out connectionInfo) && ExDateTime.TimeDiff(current, connectionInfo.LastActivityTime).TotalMilliseconds > connectionMaxAge.TotalMilliseconds)
				{
					list.Add(connectionInfo);
				}
			}
			foreach (MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo2 in list)
			{
				this.RemoveConnection(connectionInfo2.SmtpSessionId, connectionInfo2.RemoteIPAddress);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000BC88 File Offset: 0x00009E88
		public void UpdateLastActivityTime(long smtpSessionId)
		{
			MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
			if (this.connections.TryGetValue(smtpSessionId, out connectionInfo))
			{
				connectionInfo.LastActivityTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public XElement GetDiagnosticInfo(XElement parentElement)
		{
			XElement xelement = new XElement("Database");
			xelement.Add(new XElement("id", this.mdbGuid));
			int num = 0;
			DeliveryThrottling.Instance.TryGetDatabaseHealth(this.mdbGuid, out num);
			xelement.Add(new XElement("databaseHealthMeasure", num));
			XElement xelement2 = new XElement("activeConnectionCount");
			XElement xelement3 = new XElement("pendingConnectionCount");
			xelement.Add(xelement2);
			xelement.Add(xelement3);
			XElement xelement4 = new XElement("Sessions");
			xelement.Add(xelement4);
			int num2 = 0;
			foreach (long num3 in this.connections.Keys)
			{
				XElement xelement5 = new XElement("Session");
				xelement4.Add(xelement5);
				xelement5.Add(new XElement("id", num3));
				MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
				if (this.connections.TryGetValue(num3, out connectionInfo))
				{
					if (connectionInfo.Active)
					{
						num2++;
					}
					xelement5.Add(new XElement("state", connectionInfo.Active ? "Active" : "Pending"));
				}
			}
			xelement2.SetValue(num2);
			xelement3.SetValue(this.pendingConnections.Count);
			parentElement.Add(xelement);
			return parentElement;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000BE68 File Offset: 0x0000A068
		public int GetThreadCount()
		{
			int num = 0;
			foreach (long key in this.connections.Keys)
			{
				MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
				if (this.connections.TryGetValue(key, out connectionInfo) && connectionInfo.Active)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxDatabaseConnectionManager>(this);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				foreach (PooledEvent pooledEvent in this.sessionEvents.Values)
				{
					pooledEvent.Dispose();
				}
				this.sessionEvents.Clear();
				this.sessionEvents = null;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000BF44 File Offset: 0x0000A144
		private bool RemoveConnectionTrackingItems(long smtpSessionId)
		{
			MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
			bool result = this.connections.TryRemove(smtpSessionId, out connectionInfo);
			PooledEvent value = null;
			bool flag = this.sessionEvents.TryRemove(smtpSessionId, out value);
			if (flag)
			{
				this.eventPool.Release(value);
			}
			return result;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000BF83 File Offset: 0x0000A183
		private bool DeactivateConnection(long smtpSessionId)
		{
			if (!this.connections.ContainsKey(smtpSessionId))
			{
				return false;
			}
			this.ResetSessionEvent(smtpSessionId);
			this.connections[smtpSessionId].Active = false;
			this.pendingConnections.Enqueue(smtpSessionId);
			return true;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		private bool ActivateNextConnection()
		{
			if (this.pendingConnections.Count == 0)
			{
				return false;
			}
			long key = 0L;
			MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
			while (connectionInfo == null && this.pendingConnections.TryDequeue(out key))
			{
				if (this.connections.TryGetValue(key, out connectionInfo))
				{
					connectionInfo.Active = true;
					this.SetSessionEvent(connectionInfo.SmtpSessionId);
				}
			}
			return true;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C018 File Offset: 0x0000A218
		private bool IsPending(IPAddress remoteIPAddress)
		{
			lock (this.syncObject)
			{
				MailboxDatabaseConnectionManager.ConnectionInfo connectionInfo = null;
				foreach (long key in this.pendingConnections)
				{
					if (this.connections.TryGetValue(key, out connectionInfo) && !connectionInfo.Active && connectionInfo.RemoteIPAddress.Equals(remoteIPAddress))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		private bool IsActive(long smtpSessionId)
		{
			return this.IsConnected(smtpSessionId) && this.connections[smtpSessionId].Active;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		private bool IsConnected(long smtpSessionId)
		{
			return this.connections.ContainsKey(smtpSessionId);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		private bool WaitUsingEvent(long smtpSessionId, TimeSpan timeout)
		{
			PooledEvent pooledEvent = null;
			if (!this.sessionEvents.TryGetValue(smtpSessionId, out pooledEvent))
			{
				throw new InvalidOperationException("The session event was not able to be retrieved when attempting to wait for a connection.");
			}
			return pooledEvent.WaitHandle.WaitOne(timeout);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C124 File Offset: 0x0000A324
		private void ResetSessionEvent(long smtpSessionId)
		{
			PooledEvent pooledEvent = null;
			if (!this.sessionEvents.TryGetValue(smtpSessionId, out pooledEvent))
			{
				throw new InvalidOperationException("The session event was not able to be retrieved when attempting to wake a thread waiting for a connection.");
			}
			pooledEvent.WaitHandle.Reset();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C15C File Offset: 0x0000A35C
		private void SetSessionEvent(long smtpSessionId)
		{
			PooledEvent pooledEvent = null;
			if (!this.sessionEvents.TryGetValue(smtpSessionId, out pooledEvent))
			{
				throw new InvalidOperationException("The session event was not able to be retrieved when attempting to wake a thread waiting for a connection.");
			}
			pooledEvent.WaitHandle.Set();
		}

		// Token: 0x04000102 RID: 258
		private readonly Guid mdbGuid;

		// Token: 0x04000103 RID: 259
		private ConcurrentDictionary<long, MailboxDatabaseConnectionManager.ConnectionInfo> connections = new ConcurrentDictionary<long, MailboxDatabaseConnectionManager.ConnectionInfo>();

		// Token: 0x04000104 RID: 260
		private ConcurrentQueue<long> pendingConnections = new ConcurrentQueue<long>();

		// Token: 0x04000105 RID: 261
		private ConcurrentDictionary<long, PooledEvent> sessionEvents = new ConcurrentDictionary<long, PooledEvent>();

		// Token: 0x04000106 RID: 262
		private ThrottlingObjectPool<PooledEvent> eventPool;

		// Token: 0x04000107 RID: 263
		private object syncObject = new object();

		// Token: 0x02000032 RID: 50
		internal class ConnectionInfo
		{
			// Token: 0x0600025E RID: 606 RVA: 0x0000C192 File Offset: 0x0000A392
			public ConnectionInfo(long sessionId, IPAddress remoteIPAddress, ExDateTime connectTime, bool active)
			{
				this.SmtpSessionId = sessionId;
				this.RemoteIPAddress = remoteIPAddress;
				this.LastActivityTime = connectTime;
				this.Active = active;
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x0600025F RID: 607 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
			// (set) Token: 0x06000260 RID: 608 RVA: 0x0000C1BF File Offset: 0x0000A3BF
			public long SmtpSessionId { get; private set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000261 RID: 609 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
			// (set) Token: 0x06000262 RID: 610 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
			public IPAddress RemoteIPAddress { get; private set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000263 RID: 611 RVA: 0x0000C1D9 File Offset: 0x0000A3D9
			// (set) Token: 0x06000264 RID: 612 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
			public bool Active { get; set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x06000265 RID: 613 RVA: 0x0000C1EA File Offset: 0x0000A3EA
			// (set) Token: 0x06000266 RID: 614 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
			public ExDateTime LastActivityTime { get; set; }
		}
	}
}
