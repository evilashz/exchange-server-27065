using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000BC RID: 188
	internal class DataConnection : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x00019618 File Offset: 0x00017818
		private DataConnection(JET_INSTANCE instance, DataSource source)
		{
			Interlocked.Increment(ref DataConnection.nextId);
			int num = 0;
			try
			{
				try
				{
					num++;
					Api.JetBeginSession(instance, out this.session, null, null);
					num++;
					Api.JetOpenDatabase(this.session, source.DatabasePath, null, out this.database, OpenDatabaseGrbit.None);
					num++;
					this.source = source;
					this.source.AddRef();
				}
				finally
				{
					switch (num)
					{
					case 2:
						Api.JetEndSession(this.session, EndSessionGrbit.None);
						break;
					case 3:
						this.opened = true;
						break;
					}
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, source))
				{
					throw;
				}
			}
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x000196EC File Offset: 0x000178EC
		public JET_SESID Session
		{
			get
			{
				this.ThrowIfClosed();
				return this.session;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x000196FA File Offset: 0x000178FA
		public JET_DBID Database
		{
			get
			{
				this.ThrowIfClosed();
				return this.database;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00019708 File Offset: 0x00017908
		public DataSource Source
		{
			get
			{
				this.ThrowIfClosed();
				return this.source;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00019716 File Offset: 0x00017916
		public bool IsWithinTransaction
		{
			get
			{
				return this.pendingTransactions > 0;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00019724 File Offset: 0x00017924
		public static DataConnection Open(JET_INSTANCE instance, DataSource source)
		{
			DataConnection dataConnection = new DataConnection(instance, source);
			if (!dataConnection.opened)
			{
				dataConnection.Dispose(false);
				dataConnection = null;
			}
			else
			{
				dataConnection.references = 1;
			}
			return dataConnection;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019754 File Offset: 0x00017954
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DataConnection>(this);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001975C File Offset: 0x0001795C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019771 File Offset: 0x00017971
		public int AddRef()
		{
			return Interlocked.Increment(ref this.references);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019780 File Offset: 0x00017980
		public int Release()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			int num = Interlocked.Decrement(ref this.references);
			if (num == 0)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			return num;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000197BD File Offset: 0x000179BD
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000197C5 File Offset: 0x000179C5
		public void Close()
		{
			this.Release();
			if (this.opened)
			{
				throw new InvalidOperationException(Strings.ConnectionInUse);
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000197E6 File Offset: 0x000179E6
		public Transaction BeginTransaction()
		{
			return Transaction.New(this);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000197EE File Offset: 0x000179EE
		internal void NoPendingTransactions()
		{
			if (this.pendingTransactions > 0)
			{
				throw new InvalidOperationException(Strings.PendingTransactions);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00019809 File Offset: 0x00017A09
		internal void TrackStartTransaction()
		{
			this.ThrowIfClosed();
			Interlocked.Increment(ref this.pendingTransactions);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001981D File Offset: 0x00017A1D
		internal void TrackRemoveTransaction()
		{
			this.ThrowIfClosedOrNoPendingTransaction();
			Interlocked.Decrement(ref this.pendingTransactions);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00019834 File Offset: 0x00017A34
		protected virtual void Dispose(bool disposing)
		{
			if (this.opened)
			{
				if (disposing)
				{
					this.ThrowIfClosed();
					this.NoPendingTransactions();
					this.source.TrackTryConnectionClose();
					try
					{
						Api.JetCloseDatabase(this.session, this.database, CloseDatabaseGrbit.None);
						Api.JetEndSession(this.session, EndSessionGrbit.None);
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, this.source))
						{
							throw;
						}
					}
					this.source.Release();
				}
				this.source = null;
				this.opened = false;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000198C0 File Offset: 0x00017AC0
		private void ThrowIfClosed()
		{
			if (!this.opened)
			{
				throw new ObjectDisposedException("DataConnection");
			}
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000198D5 File Offset: 0x00017AD5
		private void ThrowIfClosedOrNoPendingTransaction()
		{
			this.ThrowIfClosed();
			if (this.pendingTransactions <= 0)
			{
				throw new InvalidOperationException(Strings.NotInTransaction);
			}
		}

		// Token: 0x04000309 RID: 777
		private static int nextId;

		// Token: 0x0400030A RID: 778
		private readonly JET_SESID session;

		// Token: 0x0400030B RID: 779
		private readonly JET_DBID database;

		// Token: 0x0400030C RID: 780
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400030D RID: 781
		private DataSource source;

		// Token: 0x0400030E RID: 782
		private bool opened;

		// Token: 0x0400030F RID: 783
		private int pendingTransactions;

		// Token: 0x04000310 RID: 784
		private int references;
	}
}
