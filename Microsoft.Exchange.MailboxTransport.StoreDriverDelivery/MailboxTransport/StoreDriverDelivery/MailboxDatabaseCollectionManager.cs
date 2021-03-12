using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200002D RID: 45
	internal class MailboxDatabaseCollectionManager : DisposeTrackableBase, IMailboxDatabaseCollectionManager, IDisposable
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000B5B4 File Offset: 0x000097B4
		public MailboxDatabaseCollectionManager() : this(TimeSpan.FromMinutes(10.0), TimeSpan.FromMinutes(15.0))
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B5D8 File Offset: 0x000097D8
		public MailboxDatabaseCollectionManager(TimeSpan staleConnectionProcessingInterval, TimeSpan connectionMaxIdle)
		{
			this.staleConnectionsProcessor = new GuardedTimer(new TimerCallback(this.ProcessStaleConnections), null, staleConnectionProcessingInterval, staleConnectionProcessingInterval);
			this.connectionMaxIdle = connectionMaxIdle;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B631 File Offset: 0x00009831
		public IMailboxDatabaseConnectionManager GetConnectionManager(Guid mdbGuid)
		{
			return this.databases.GetOrAdd(mdbGuid, new Func<Guid, MailboxDatabaseConnectionManager>(this.CreateConnectionManagerForKey));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B64C File Offset: 0x0000984C
		public XElement GetDiagnosticInfo(XElement parentElement)
		{
			parentElement.Add(new XElement("MaxMailboxDeliveryPerMdbConnections", DeliveryConfiguration.Instance.Throttling.MaxMailboxDeliveryPerMdbConnections));
			foreach (MailboxDatabaseConnectionManager mailboxDatabaseConnectionManager in this.databases.Values)
			{
				mailboxDatabaseConnectionManager.GetDiagnosticInfo(parentElement);
			}
			return parentElement;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B6CC File Offset: 0x000098CC
		public void UpdateMdbThreadCounters()
		{
			foreach (KeyValuePair<Guid, MailboxDatabaseConnectionManager> keyValuePair in this.databases)
			{
				StoreDriverDatabasePerfCounters.AddDeliveryThreadSample(keyValuePair.Key.ToString(), (long)keyValuePair.Value.GetThreadCount());
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B73C File Offset: 0x0000993C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxDatabaseCollectionManager>(this);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B744 File Offset: 0x00009944
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.staleConnectionsProcessor.Dispose(true);
				this.staleConnectionsProcessor = null;
				foreach (IMailboxDatabaseConnectionManager mailboxDatabaseConnectionManager in this.databases.Values)
				{
					mailboxDatabaseConnectionManager.Dispose();
				}
				this.databases.Clear();
				this.databases = null;
				this.eventPool.Dispose();
				this.eventPool = null;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000B7D0 File Offset: 0x000099D0
		private void ProcessStaleConnections(object state)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			foreach (MailboxDatabaseConnectionManager mailboxDatabaseConnectionManager in this.databases.Values)
			{
				mailboxDatabaseConnectionManager.ProcessStaleConnections(utcNow, this.connectionMaxIdle);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000B830 File Offset: 0x00009A30
		private MailboxDatabaseConnectionManager CreateConnectionManagerForKey(Guid mdbGuid)
		{
			return new MailboxDatabaseConnectionManager(mdbGuid, this.eventPool);
		}

		// Token: 0x040000FD RID: 253
		private readonly TimeSpan connectionMaxIdle;

		// Token: 0x040000FE RID: 254
		private GuardedTimer staleConnectionsProcessor;

		// Token: 0x040000FF RID: 255
		private ConcurrentDictionary<Guid, MailboxDatabaseConnectionManager> databases = new ConcurrentDictionary<Guid, MailboxDatabaseConnectionManager>();

		// Token: 0x04000100 RID: 256
		private ThrottlingObjectPool<PooledEvent> eventPool = new ThrottlingObjectPool<PooledEvent>(DeliveryConfiguration.Instance.Throttling.MailboxServerThreadLimit);
	}
}
