using System;
using System.IO;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000D2 RID: 210
	internal class LazyBytes : IDataWithinRowComponent, IDataObjectComponent
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x0001E331 File Offset: 0x0001C531
		public LazyBytes(DataRow row, DataColumn column)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			this.row = row;
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			this.column = column;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001E363 File Offset: 0x0001C563
		public LazyBytes(DataRow row, BlobCollection blobCollection, byte blobCollectionKey)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			this.row = row;
			if (blobCollection == null)
			{
				throw new ArgumentNullException("blobCollection");
			}
			this.blobCollection = blobCollection;
			this.blobCollectionKey = blobCollectionKey;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001E39C File Offset: 0x0001C59C
		public LazyBytes()
		{
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001E3A4 File Offset: 0x0001C5A4
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x0001E3F8 File Offset: 0x0001C5F8
		public byte[] Value
		{
			get
			{
				if (this.IsDeferred)
				{
					lock (this)
					{
						if (this.IsDeferred)
						{
							this.DeferredLoad();
						}
					}
				}
				return this.bytes;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.state &= ~LazyBytes.State.Deferred;
				this.state |= LazyBytes.State.Dirty;
				this.bytes = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001E431 File Offset: 0x0001C631
		public bool PendingDatabaseUpdates
		{
			get
			{
				return this.IsDirty;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001E439 File Offset: 0x0001C639
		public int PendingDatabaseUpdateCount
		{
			get
			{
				if (!this.IsDirty)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001E446 File Offset: 0x0001C646
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001E458 File Offset: 0x0001C658
		public bool IsReadOnly
		{
			get
			{
				return (byte)(this.state & LazyBytes.State.ReadOnly) == 16;
			}
			set
			{
				if (value)
				{
					this.state |= LazyBytes.State.ReadOnly;
					return;
				}
				this.state &= ~LazyBytes.State.ReadOnly;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x0001E489 File Offset: 0x0001C689
		private bool IsDirty
		{
			get
			{
				return (byte)(this.state & LazyBytes.State.Dirty) == 1;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001E499 File Offset: 0x0001C699
		private bool IsDeleted
		{
			get
			{
				return (byte)(this.state & LazyBytes.State.Deleted) == 8;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x0001E4A9 File Offset: 0x0001C6A9
		private bool IsDeferred
		{
			get
			{
				return (byte)(this.state & LazyBytes.State.Deferred) == 4;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001E4B9 File Offset: 0x0001C6B9
		private bool IsSaved
		{
			get
			{
				return (byte)(this.state & LazyBytes.State.PreviouslySaved) == 2;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001E4C9 File Offset: 0x0001C6C9
		public void CloneFrom(IDataObjectComponent other)
		{
			this.ThrowIfReadOnly();
			this.state = (LazyBytes.State.PreviouslySaved | LazyBytes.State.Deferred);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001E4DA File Offset: 0x0001C6DA
		public void MarkDeleted()
		{
			this.state |= LazyBytes.State.Deleted;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001E4EF File Offset: 0x0001C6EF
		public void MinimizeMemory()
		{
			if (this.IsDeleted)
			{
				this.bytes = null;
				return;
			}
			if (this.IsSaved && !this.IsDirty)
			{
				this.bytes = null;
				this.state |= LazyBytes.State.Deferred;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001E52B File Offset: 0x0001C72B
		public void LoadFromParentRow(DataTableCursor cursor)
		{
			this.ThrowIfReadOnly();
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			this.state = (LazyBytes.State.PreviouslySaved | LazyBytes.State.Deferred);
			this.row.PerfCounters.LazyBytesLoadRequested.Increment();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001E560 File Offset: 0x0001C760
		public void SaveToParentRow(DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (!cursor.IsWithinTransaction)
			{
				throw new InvalidOperationException(Strings.NotInTransaction);
			}
			if (this.IsDeleted || !this.IsDirty)
			{
				return;
			}
			using (Stream stream = (this.blobCollection == null) ? this.column.OpenImmediateWriter(cursor, this.row, this.IsSaved, 1) : this.blobCollection.OpenWriter(this.blobCollectionKey, cursor, this.IsSaved, false, null))
			{
				if (this.bytes != null)
				{
					stream.Write(this.bytes, 0, this.bytes.Length);
				}
				else
				{
					stream.SetLength(0L);
				}
			}
			this.state = (LazyBytes.State)(2 | (byte)(this.state & LazyBytes.State.ReadOnly));
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001E63C File Offset: 0x0001C83C
		public void Cleanup()
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001E640 File Offset: 0x0001C840
		private void DeferredLoad()
		{
			using (DataTableCursor dataTableCursor = this.OpenCursor())
			{
				using (dataTableCursor.BeginTransaction())
				{
					this.MoveToItem(dataTableCursor);
					this.LoadFromCursor(dataTableCursor);
				}
			}
			this.row.PerfCounters.LazyBytesLoadPerformed.Increment();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		private void LoadFromCursor(DataTableCursor cursor)
		{
			using (Stream stream = (this.blobCollection == null) ? this.column.OpenImmediateReader(cursor, this.row, 1) : this.blobCollection.OpenReader(this.blobCollectionKey, cursor, false))
			{
				if (stream.Length > 0L)
				{
					this.bytes = new byte[stream.Length];
					stream.Read(this.bytes, 0, (int)stream.Length);
				}
			}
			this.state = (LazyBytes.State)(2 | (byte)(this.state & LazyBytes.State.ReadOnly));
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001E758 File Offset: 0x0001C958
		private DataTableCursor OpenCursor()
		{
			return this.row.Table.GetCursor();
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001E777 File Offset: 0x0001C977
		private void MoveToItem(DataTableCursor cursor)
		{
			this.row.SeekCurrent(cursor);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001E785 File Offset: 0x0001C985
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException("This LazyBytes operation cannot be performed in read-only mode.");
			}
		}

		// Token: 0x040003C2 RID: 962
		private readonly DataRow row;

		// Token: 0x040003C3 RID: 963
		private readonly DataColumn column;

		// Token: 0x040003C4 RID: 964
		private readonly byte blobCollectionKey;

		// Token: 0x040003C5 RID: 965
		private readonly BlobCollection blobCollection;

		// Token: 0x040003C6 RID: 966
		private volatile LazyBytes.State state;

		// Token: 0x040003C7 RID: 967
		private byte[] bytes;

		// Token: 0x020000D3 RID: 211
		[Flags]
		private enum State : byte
		{
			// Token: 0x040003C9 RID: 969
			New = 0,
			// Token: 0x040003CA RID: 970
			Dirty = 1,
			// Token: 0x040003CB RID: 971
			PreviouslySaved = 2,
			// Token: 0x040003CC RID: 972
			Deferred = 4,
			// Token: 0x040003CD RID: 973
			Deleted = 8,
			// Token: 0x040003CE RID: 974
			ReadOnly = 16
		}
	}
}
