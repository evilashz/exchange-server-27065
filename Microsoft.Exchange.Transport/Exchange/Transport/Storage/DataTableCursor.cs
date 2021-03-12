using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000CF RID: 207
	internal class DataTableCursor : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001DA08 File Offset: 0x0001BC08
		public DataTableCursor(JET_TABLEID tableId, DataConnection connection, DataTable dataTable)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			Interlocked.Increment(ref DataTableCursor.nextId);
			this.tableId = tableId;
			this.dataTable = dataTable;
			this.connection = connection;
			this.audit.Drop(Breadcrumb.NewItem);
			this.connection.Source.PerfCounters.CursorsOpened.Increment();
			this.connection.AddRef();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001DA99 File Offset: 0x0001BC99
		public JET_TABLEID TableId
		{
			get
			{
				return this.tableId;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001DAA1 File Offset: 0x0001BCA1
		public DataTable Table
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001DAA9 File Offset: 0x0001BCA9
		public JET_SESID Session
		{
			get
			{
				return this.connection.Session;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001DAB6 File Offset: 0x0001BCB6
		public DataConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001DABE File Offset: 0x0001BCBE
		public bool IsWithinTransaction
		{
			get
			{
				return this.connection.IsWithinTransaction;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001DACB File Offset: 0x0001BCCB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001DADA File Offset: 0x0001BCDA
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public void DeleteCurrentRow()
		{
			try
			{
				Api.JetDelete(this.Session, this.TableId);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001DB2C File Offset: 0x0001BD2C
		public void GotoBookmark(byte[] bookmark)
		{
			if (bookmark == null)
			{
				throw new ArgumentNullException("bookmark");
			}
			try
			{
				Api.JetGotoBookmark(this.Session, this.TableId, bookmark, bookmark.Length);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001DB88 File Offset: 0x0001BD88
		public byte[] GetBookmark()
		{
			try
			{
				return Api.GetBookmark(this.Session, this.TableId);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		public void PrereadForward()
		{
			try
			{
				Api.JetSetTableSequential(this.Session, this.TableId, (SetTableSequentialGrbit)1);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public void PrereadBackward()
		{
			try
			{
				Api.JetSetTableSequential(this.Session, this.TableId, (SetTableSequentialGrbit)2);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001DC6C File Offset: 0x0001BE6C
		public void SetCurrentIndex(string indexName)
		{
			try
			{
				Api.JetSetCurrentIndex(this.Session, this.TableId, indexName);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001DCB8 File Offset: 0x0001BEB8
		public void MakeKey(params byte[][] keys)
		{
			try
			{
				MakeKeyGrbit grbit = MakeKeyGrbit.NewKey;
				foreach (byte[] data in keys)
				{
					Api.MakeKey(this.Session, this.tableId, data, grbit);
					grbit = MakeKeyGrbit.None;
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001DD20 File Offset: 0x0001BF20
		public bool TrySeekGE(params byte[][] keys)
		{
			return this.TrySeek(SeekGrbit.SeekGE, keys);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001DD2A File Offset: 0x0001BF2A
		public bool TrySeek(params byte[][] keys)
		{
			return this.TrySeek(SeekGrbit.SeekEQ, keys);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001DD34 File Offset: 0x0001BF34
		public void Seek(params byte[][] keys)
		{
			this.Seek(SeekGrbit.SeekEQ, keys);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001DD3E File Offset: 0x0001BF3E
		public bool TrySeek()
		{
			return this.TrySeek(SeekGrbit.SeekEQ);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001DD47 File Offset: 0x0001BF47
		public bool TrySetIndexUpperRange(params byte[][] keys)
		{
			return this.TrySetIndexRange(SetIndexRangeGrbit.RangeInclusive | SetIndexRangeGrbit.RangeUpperLimit, keys);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001DD51 File Offset: 0x0001BF51
		public bool TryRemoveIndexRange()
		{
			return this.TrySetIndexRange(SetIndexRangeGrbit.RangeRemove);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001DD5A File Offset: 0x0001BF5A
		public bool TryMoveFirst()
		{
			return this.TryMove(JET_Move.First, false);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001DD68 File Offset: 0x0001BF68
		public bool TryMoveLast()
		{
			return this.TryMove(JET_Move.Last, false);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001DD76 File Offset: 0x0001BF76
		public bool TryMoveNext(bool skipDups = false)
		{
			return this.TryMove(JET_Move.Next, skipDups);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public bool TryMovePrevious(bool skipDups = false)
		{
			return this.TryMove(JET_Move.Previous, skipDups);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001DD8A File Offset: 0x0001BF8A
		public bool HasData()
		{
			return this.TryMove((JET_Move)0, false);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001DD94 File Offset: 0x0001BF94
		public void MoveBeforeFirst()
		{
			try
			{
				Api.MoveBeforeFirst(this.Session, this.tableId);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		public void MoveAfterLast()
		{
			try
			{
				Api.MoveAfterLast(this.Session, this.tableId);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001DE24 File Offset: 0x0001C024
		public void MoveFirst()
		{
			this.Move(JET_Move.First);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001DE31 File Offset: 0x0001C031
		public void MoveLast()
		{
			this.Move(JET_Move.Last);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001DE3E File Offset: 0x0001C03E
		public void CancelPrepare()
		{
			this.PrepareUpdate(JET_prep.Cancel);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001DE47 File Offset: 0x0001C047
		public void PrepareUpdate(bool doLock = true)
		{
			this.PrepareUpdate(doLock ? JET_prep.Replace : JET_prep.ReplaceNoLock);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001DE56 File Offset: 0x0001C056
		public void PrepareInsert(bool clone = false, bool deleteOriginal = false)
		{
			this.PrepareUpdate((!clone) ? JET_prep.Insert : (deleteOriginal ? JET_prep.InsertCopyDeleteOriginal : JET_prep.InsertCopy));
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001DE6C File Offset: 0x0001C06C
		public void Update()
		{
			try
			{
				Api.JetUpdate(this.Session, this.tableId);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public void CreateIndex(string name, string columns)
		{
			try
			{
				Api.JetCreateIndex(this.Session, this.TableId, name, CreateIndexGrbit.IndexIgnoreAnyNull, columns, columns.Length, 90);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001DF08 File Offset: 0x0001C108
		public bool TryCreateIndex(string name, string columns)
		{
			try
			{
				Api.JetCreateIndex(this.Session, this.TableId, name, CreateIndexGrbit.IndexIgnoreAnyNull, columns, columns.Length, 90);
			}
			catch (EsentIndexDuplicateException)
			{
				return false;
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			return true;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001DF70 File Offset: 0x0001C170
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DataTableCursor>(this);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001DF78 File Offset: 0x0001C178
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001DF8D File Offset: 0x0001C18D
		public Transaction BeginTransaction()
		{
			return this.connection.BeginTransaction();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001DF9C File Offset: 0x0001C19C
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (disposing)
			{
				this.audit.Drop(Breadcrumb.CloseItem);
				if (this.connection != null)
				{
					try
					{
						Api.JetCloseTable(this.Session, this.TableId);
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, this.connection.Source))
						{
							throw;
						}
					}
					this.connection.Source.PerfCounters.CursorsClosed.Increment();
					this.Table.ReleaseCursor();
					this.connection.Release();
				}
			}
			this.connection = null;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001E04C File Offset: 0x0001C24C
		private bool TrySeek(SeekGrbit grbit, params byte[][] keys)
		{
			this.MakeKey(keys);
			return this.TrySeek(grbit);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001E05C File Offset: 0x0001C25C
		private void Seek(SeekGrbit grbit, params byte[][] keys)
		{
			this.MakeKey(keys);
			this.Seek(grbit);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001E06C File Offset: 0x0001C26C
		private bool TrySeek(SeekGrbit grbit)
		{
			try
			{
				return Api.TrySeek(this.Session, this.tableId, grbit);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		private void Seek(SeekGrbit grbit)
		{
			try
			{
				Api.JetSeek(this.Session, this.tableId, grbit);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001E108 File Offset: 0x0001C308
		private bool TrySetIndexRange(SetIndexRangeGrbit grbit, params byte[][] keys)
		{
			this.MakeKey(keys);
			return this.TrySetIndexRange(grbit);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001E118 File Offset: 0x0001C318
		private bool TrySetIndexRange(SetIndexRangeGrbit grbit)
		{
			try
			{
				Api.JetSetIndexRange(this.Session, this.TableId, grbit);
				return true;
			}
			catch (EsentNoCurrentRecordException)
			{
				this.TryRemoveIndexRange();
			}
			catch (EsentInvalidOperationException)
			{
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001E18C File Offset: 0x0001C38C
		private bool TryMove(JET_Move where, bool skipDups)
		{
			try
			{
				return Api.TryMove(this.Session, this.tableId, where, skipDups ? MoveGrbit.MoveKeyNE : MoveGrbit.None);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001E1E0 File Offset: 0x0001C3E0
		private void Move(JET_Move where)
		{
			try
			{
				Api.JetMove(this.Session, this.tableId, where, MoveGrbit.None);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001E22C File Offset: 0x0001C42C
		private void PrepareUpdate(JET_prep prep)
		{
			try
			{
				Api.JetPrepareUpdate(this.Session, this.tableId, prep);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, this.connection.Source))
				{
					throw;
				}
			}
		}

		// Token: 0x04000379 RID: 889
		private static int nextId;

		// Token: 0x0400037A RID: 890
		private readonly JET_TABLEID tableId;

		// Token: 0x0400037B RID: 891
		private readonly DataTable dataTable;

		// Token: 0x0400037C RID: 892
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400037D RID: 893
		private DataConnection connection;

		// Token: 0x0400037E RID: 894
		private Breadcrumbs audit = new Breadcrumbs(64);
	}
}
