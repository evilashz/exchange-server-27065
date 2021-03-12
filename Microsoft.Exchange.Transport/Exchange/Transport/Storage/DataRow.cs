using System;
using System.Collections.Generic;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000BE RID: 190
	internal abstract class DataRow
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x000198F8 File Offset: 0x00017AF8
		protected DataRow(DataTableView tableView)
		{
			this.audit.Drop(Breadcrumb.NewItem);
			this.tableView = tableView;
			this.dataCache = new DataColumnsCache(this);
			this.AddComponent(this.dataCache);
			this.PerfCounterAttribution = null;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00019960 File Offset: 0x00017B60
		protected DataRow(DataTable table)
		{
			this.audit.Drop(Breadcrumb.NewItem);
			this.tableView = table.DefaultView;
			this.dataCache = new DataColumnsCache(this);
			this.AddComponent(this.dataCache);
			this.PerfCounterAttribution = null;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x000199CC File Offset: 0x00017BCC
		public bool Updating
		{
			get
			{
				return this.updating;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x000199D4 File Offset: 0x00017BD4
		public DataTable Table
		{
			get
			{
				return this.tableView.Table;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x000199E1 File Offset: 0x00017BE1
		public DataTableView TableView
		{
			get
			{
				return this.tableView;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000199EC File Offset: 0x00017BEC
		public bool PendingDatabaseUpdates
		{
			get
			{
				if (this.IsDeletePending)
				{
					return true;
				}
				if (this.IsDeleted)
				{
					return false;
				}
				foreach (IDataObjectComponent dataObjectComponent in this.components)
				{
					if (dataObjectComponent.PendingDatabaseUpdates)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00019A5C File Offset: 0x00017C5C
		public bool PendingDatabaseUpdateWithinRow
		{
			get
			{
				if (this.IsDeletePending)
				{
					return true;
				}
				if (this.IsDeleted)
				{
					return false;
				}
				foreach (IDataWithinRowComponent dataWithinRowComponent in this.ComponentsWithinRow)
				{
					if (dataWithinRowComponent.PendingDatabaseUpdates)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public int DatabaseUpdateCount
		{
			get
			{
				if (!this.IsDeleted)
				{
					int num = 0;
					foreach (IDataObjectComponent dataObjectComponent in this.components)
					{
						num += dataObjectComponent.PendingDatabaseUpdateCount;
					}
					return num;
				}
				if (this.IsDeletePending)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00019B34 File Offset: 0x00017D34
		public virtual bool IsDeleted
		{
			get
			{
				return this.objectState == DataRow.DataRowState.Delete;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00019B3F File Offset: 0x00017D3F
		public bool IsNew
		{
			get
			{
				return this.objectState == DataRow.DataRowState.New;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00019B4C File Offset: 0x00017D4C
		public bool IsMaterialized
		{
			get
			{
				switch (this.objectState)
				{
				case DataRow.DataRowState.New:
				case DataRow.DataRowState.Delete:
					return false;
				case DataRow.DataRowState.Materialized:
				case DataRow.DataRowState.MoveSource:
					return true;
				}
				return false;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00019B87 File Offset: 0x00017D87
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00019B8F File Offset: 0x00017D8F
		public string PerfCounterAttribution
		{
			get
			{
				return this.perfCounterAttribution;
			}
			set
			{
				this.perfCounterAttribution = (value ?? "other");
				this.perfCounters = DatabasePerfCounters.GetInstance(this.perfCounterAttribution);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00019BB2 File Offset: 0x00017DB2
		public DatabasePerfCountersInstance PerfCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00019BBA File Offset: 0x00017DBA
		protected internal DataColumnsCache Columns
		{
			get
			{
				return this.dataCache;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00019BC2 File Offset: 0x00017DC2
		protected List<IDataObjectComponent> ComponentsAll
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00019BCA File Offset: 0x00017DCA
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00019BD2 File Offset: 0x00017DD2
		private bool IsDeletePending
		{
			get
			{
				return this.pendingRowDelete;
			}
			set
			{
				this.pendingRowDelete = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00019D7C File Offset: 0x00017F7C
		private IEnumerable<IDataExternalComponent> ComponentsExternal
		{
			get
			{
				foreach (IDataObjectComponent component in this.components)
				{
					IDataExternalComponent ret = component as IDataExternalComponent;
					if (ret != null)
					{
						yield return ret;
					}
				}
				yield break;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00019F3C File Offset: 0x0001813C
		private IEnumerable<IDataWithinRowComponent> ComponentsWithinRow
		{
			get
			{
				foreach (IDataObjectComponent component in this.components)
				{
					IDataWithinRowComponent ret = component as IDataWithinRowComponent;
					if (ret != null)
					{
						yield return ret;
					}
				}
				yield break;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00019F5C File Offset: 0x0001815C
		public virtual void MinimizeMemory()
		{
			foreach (IDataObjectComponent dataObjectComponent in this.components)
			{
				dataObjectComponent.MinimizeMemory();
			}
			this.perfCounters.MinimizeMemory.Increment();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00019FC0 File Offset: 0x000181C0
		public void SeekCurrent(DataTableCursor cursor)
		{
			if (!this.TrySeekCurrent(cursor))
			{
				throw new DataSeekException(this, cursor, Strings.SeekFailed);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00019FEC File Offset: 0x000181EC
		public bool TrySeekCurrent(DataTableCursor cursor)
		{
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			bool flag = false;
			try
			{
				Api.JetSetCurrentIndex(cursor.Session, cursor.TableId, null);
				this.dataCache.MakeKey(cursor);
				flag = Api.TrySeek(cursor.Session, cursor.TableId, SeekGrbit.SeekEQ);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
			this.perfCounters.Seeks.Increment();
			this.audit.Drop(flag ? Breadcrumb.Seek : Breadcrumb.SeekFail);
			return flag;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001A090 File Offset: 0x00018290
		public bool TrySeekCurrentPrefix(DataTableCursor cursor, int prefixColumns)
		{
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (this.IsDeleted)
			{
				throw new InvalidOperationException(Strings.RowDeleted);
			}
			try
			{
				Api.JetSetCurrentIndex(cursor.Session, cursor.TableId, null);
				this.dataCache.MakeStartPrefixKey(cursor, prefixColumns);
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
			bool flag = false;
			try
			{
				Api.JetSeek(cursor.Session, cursor.TableId, SeekGrbit.SeekGE);
				flag = true;
			}
			catch (EsentErrorException)
			{
				DataRow.IgnoredException();
			}
			this.perfCounters.PrefixSeeks.Increment();
			this.audit.Drop(flag ? Breadcrumb.Seek : Breadcrumb.SeekFail);
			return flag;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001A168 File Offset: 0x00018368
		public virtual void MarkToDelete()
		{
			this.audit.Drop(Breadcrumb.MarkDeleted);
			switch (this.objectState)
			{
			case DataRow.DataRowState.New:
				this.objectState = DataRow.DataRowState.Delete;
				this.MarkRowClean();
				this.MarkExternalComponentsToDelete();
				this.CleanupInternalComponents();
				return;
			case DataRow.DataRowState.Materialized:
				this.objectState = DataRow.DataRowState.Delete;
				this.IsDeletePending = true;
				this.MarkExternalComponentsToDelete();
				this.CleanupInternalComponents();
				return;
			case DataRow.DataRowState.Delete:
				return;
			default:
				throw new InvalidOperationException(Strings.InvalidDeleteState);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001A1E7 File Offset: 0x000183E7
		protected void Reconnect(DataTableCursor cursor)
		{
			if (this.TrySeekCurrent(cursor))
			{
				this.objectState = DataRow.DataRowState.Materialized;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001A1F9 File Offset: 0x000183F9
		protected void AddComponent(IDataObjectComponent component)
		{
			this.components.Add(component);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001A207 File Offset: 0x00018407
		protected void AddFirstComponent(IDataObjectComponent component)
		{
			this.components.Insert(0, component);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001A218 File Offset: 0x00018418
		protected void ReplaceComponent(IDataObjectComponent oldComponent, IDataObjectComponent newComponent)
		{
			int num = this.components.IndexOf(oldComponent);
			if (num < 0)
			{
				throw new ArgumentException("oldComponent is not found in the component list", "oldComponent");
			}
			this.components[num] = newComponent;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001A253 File Offset: 0x00018453
		protected void Commit()
		{
			this.Commit(TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A25C File Offset: 0x0001845C
		protected void Commit(TransactionCommitMode commitMode)
		{
			if (!this.PendingDatabaseUpdates)
			{
				return;
			}
			using (DataConnection dataConnection = this.Table.DataSource.DemandNewConnection())
			{
				using (Transaction transaction = dataConnection.BeginTransaction())
				{
					this.Materialize(transaction);
					transaction.Commit(commitMode);
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001A2CC File Offset: 0x000184CC
		protected void Materialize(Transaction transaction)
		{
			if (!this.PendingDatabaseUpdates)
			{
				return;
			}
			using (DataTableCursor dataTableCursor = this.Table.OpenCursor(transaction.Connection))
			{
				this.MaterializeToCursor(transaction, dataTableCursor, null);
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001A31C File Offset: 0x0001851C
		protected void MaterializeToCursor(Transaction transaction, DataTableCursor cursor)
		{
			this.MaterializeToCursor(transaction, cursor, null);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001A328 File Offset: 0x00018528
		protected virtual void MaterializeToCursor(Transaction transaction, DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			if (!this.PendingDatabaseUpdates)
			{
				return;
			}
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (!cursor.IsWithinTransaction)
			{
				throw new InvalidOperationException(Strings.NotInTransaction);
			}
			lock (this)
			{
				if (this.PendingDatabaseUpdates)
				{
					this.audit.Drop(Breadcrumb.MaterializeToRow);
					this.updating = true;
					try
					{
						switch (this.objectState)
						{
						case DataRow.DataRowState.New:
							this.MaterializeSave(transaction, cursor, false, checkpointCallback);
							goto IL_11F;
						case DataRow.DataRowState.Materialized:
							this.MaterializeSave(transaction, cursor, true, null);
							goto IL_11F;
						case DataRow.DataRowState.Delete:
							this.MaterializeDelete(transaction, cursor);
							this.MinimizeMemory();
							goto IL_11F;
						case DataRow.DataRowState.CloneTarget:
							this.MaterializeCloneMove(transaction, cursor, true);
							goto IL_11F;
						case DataRow.DataRowState.MoveSource:
							this.MaterializeSave(transaction, cursor, true, null);
							goto IL_11F;
						case DataRow.DataRowState.MoveTarget:
							if (this.cloneOrMoveSource.Table == this.Table)
							{
								this.MaterializeCloneMove(transaction, cursor, false);
								goto IL_11F;
							}
							this.cloneOrMoveSource.Materialize(transaction);
							this.MaterializeSave(transaction, cursor, false, null);
							goto IL_11F;
						}
						throw new InvalidOperationException(Strings.InvalidRowState);
						IL_11F:;
					}
					catch (EsentErrorException ex)
					{
						if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
						{
							throw;
						}
					}
					this.updating = false;
				}
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001A4B8 File Offset: 0x000186B8
		protected void LoadFromCurrentRow(DataTableCursor cursor)
		{
			if (!cursor.IsWithinTransaction)
			{
				throw new InvalidOperationException(Strings.NotInTransaction);
			}
			try
			{
				foreach (IDataWithinRowComponent dataWithinRowComponent in this.ComponentsWithinRow)
				{
					dataWithinRowComponent.LoadFromParentRow(cursor);
				}
			}
			catch (EsentErrorException ex)
			{
				if (!DataSource.HandleIsamException(ex, cursor.Connection.Source))
				{
					throw;
				}
			}
			this.objectState = DataRow.DataRowState.Materialized;
			this.perfCounters.LoadFromCurrent.Increment();
			this.audit.Drop(Breadcrumb.Loaded);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001A56C File Offset: 0x0001876C
		protected void SetCloneOrMoveSource(DataRow source, bool clone)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Strings.CloneMoveSourceNull);
			}
			if (clone && source.Table != this.Table)
			{
				throw new InvalidOperationException("Can't clone between different tables");
			}
			if (!this.IsNew || this.cloneOrMoveSource != null)
			{
				throw new InvalidOperationException(Strings.CloneMoveTargetNotNew);
			}
			if (source.PendingDatabaseUpdates)
			{
				throw new InvalidOperationException(Strings.CloneMoveSourceModified);
			}
			DataRow.DataRowState dataRowState = source.objectState;
			if (dataRowState == DataRow.DataRowState.Materialized)
			{
				if (clone)
				{
					source.objectState = DataRow.DataRowState.CloneSource;
					this.objectState = DataRow.DataRowState.CloneTarget;
				}
				else
				{
					source.objectState = DataRow.DataRowState.MoveSource;
					this.objectState = DataRow.DataRowState.MoveTarget;
				}
				this.cloneOrMoveSource = source;
				return;
			}
			throw new InvalidOperationException(Strings.CloneMoveSourceNotSaved);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001A62C File Offset: 0x0001882C
		protected void SourceMoveOrCloneCompleted()
		{
			switch (this.objectState)
			{
			case DataRow.DataRowState.CloneSource:
				this.objectState = DataRow.DataRowState.Materialized;
				return;
			case DataRow.DataRowState.MoveSource:
				this.objectState = DataRow.DataRowState.InvalidatedByMove;
				return;
			}
			throw new InvalidOperationException(Strings.CloneMoveComplete);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001A675 File Offset: 0x00018875
		private static void IgnoredException()
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001A678 File Offset: 0x00018878
		private void MaterializeSave(Transaction transaction, DataTableCursor cursor, bool update, Func<bool> checkpointCallback)
		{
			if (this.PendingDatabaseUpdateWithinRow)
			{
				if (update)
				{
					this.SeekCurrent(cursor);
					checkpointCallback = null;
				}
				Api.JetPrepareUpdate(cursor.Session, cursor.TableId, update ? JET_prep.ReplaceNoLock : JET_prep.Insert);
				bool flag = false;
				try
				{
					this.SaveComponentsWithinRow(cursor, checkpointCallback);
					flag = true;
				}
				finally
				{
					if (flag)
					{
						Api.JetUpdate(cursor.Session, cursor.TableId);
					}
					else
					{
						Api.JetPrepareUpdate(cursor.Session, cursor.TableId, JET_prep.Cancel);
					}
				}
			}
			this.SaveComponentsExternal(transaction);
			this.objectState = DataRow.DataRowState.Materialized;
			if (update)
			{
				this.perfCounters.Update.Increment();
				this.audit.Drop(Breadcrumb.MaterializeUpdate);
				return;
			}
			this.perfCounters.New.Increment();
			this.audit.Drop(Breadcrumb.MaterializeNew);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001A750 File Offset: 0x00018950
		private void MaterializeCloneMove(Transaction transaction, DataTableCursor cursor, bool clone)
		{
			this.cloneOrMoveSource.SeekCurrent(cursor);
			Api.JetPrepareUpdate(cursor.Session, cursor.TableId, clone ? JET_prep.InsertCopy : JET_prep.InsertCopyDeleteOriginal);
			bool flag = false;
			try
			{
				this.SaveComponentsWithinRow(cursor, null);
				flag = true;
			}
			finally
			{
				if (flag)
				{
					Api.JetUpdate(cursor.Session, cursor.TableId);
				}
				else
				{
					Api.JetPrepareUpdate(cursor.Session, cursor.TableId, JET_prep.Cancel);
				}
			}
			this.SaveComponentsExternal(transaction);
			this.objectState = DataRow.DataRowState.Materialized;
			this.cloneOrMoveSource.SourceMoveOrCloneCompleted();
			this.cloneOrMoveSource = null;
			if (clone)
			{
				this.perfCounters.Clone.Increment();
				this.audit.Drop(Breadcrumb.CloneItem);
				return;
			}
			this.perfCounters.Move.Increment();
			this.audit.Drop(Breadcrumb.Moved);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001A830 File Offset: 0x00018A30
		private void MaterializeDelete(Transaction transaction, DataTableCursor cursor)
		{
			this.SeekCurrent(cursor);
			Api.JetDelete(cursor.Session, cursor.TableId);
			foreach (IDataExternalComponent dataExternalComponent in this.ComponentsExternal)
			{
				dataExternalComponent.SaveToExternalRow(transaction);
			}
			this.MarkRowClean();
			this.perfCounters.Delete.Increment();
			this.audit.Drop(Breadcrumb.Deleted);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001A8BC File Offset: 0x00018ABC
		private void SaveComponentsWithinRow(DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			foreach (IDataWithinRowComponent dataWithinRowComponent in this.ComponentsWithinRow)
			{
				dataWithinRowComponent.SaveToParentRow(cursor, checkpointCallback);
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001A90C File Offset: 0x00018B0C
		private void SaveComponentsExternal(Transaction transaction)
		{
			foreach (IDataExternalComponent dataExternalComponent in this.ComponentsExternal)
			{
				dataExternalComponent.SaveToExternalRow(transaction);
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001A95C File Offset: 0x00018B5C
		private void MarkRowClean()
		{
			this.IsDeletePending = false;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001A968 File Offset: 0x00018B68
		private void MarkExternalComponentsToDelete()
		{
			foreach (IDataExternalComponent dataExternalComponent in this.ComponentsExternal)
			{
				dataExternalComponent.MarkToDelete();
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001A9B4 File Offset: 0x00018BB4
		private void CleanupInternalComponents()
		{
			foreach (IDataWithinRowComponent dataWithinRowComponent in this.ComponentsWithinRow)
			{
				dataWithinRowComponent.Cleanup();
			}
		}

		// Token: 0x04000311 RID: 785
		protected Breadcrumbs audit = new Breadcrumbs(8);

		// Token: 0x04000312 RID: 786
		protected DatabasePerfCountersInstance perfCounters;

		// Token: 0x04000313 RID: 787
		private readonly DataTableView tableView;

		// Token: 0x04000314 RID: 788
		private string perfCounterAttribution;

		// Token: 0x04000315 RID: 789
		private DataColumnsCache dataCache;

		// Token: 0x04000316 RID: 790
		private DataRow cloneOrMoveSource;

		// Token: 0x04000317 RID: 791
		private bool updating;

		// Token: 0x04000318 RID: 792
		private bool pendingRowDelete;

		// Token: 0x04000319 RID: 793
		private DataRow.DataRowState objectState = DataRow.DataRowState.New;

		// Token: 0x0400031A RID: 794
		private List<IDataObjectComponent> components = new List<IDataObjectComponent>();

		// Token: 0x020000BF RID: 191
		protected enum DataRowState
		{
			// Token: 0x0400031C RID: 796
			Undefined,
			// Token: 0x0400031D RID: 797
			New,
			// Token: 0x0400031E RID: 798
			Materialized,
			// Token: 0x0400031F RID: 799
			Delete,
			// Token: 0x04000320 RID: 800
			CloneSource,
			// Token: 0x04000321 RID: 801
			CloneTarget,
			// Token: 0x04000322 RID: 802
			MoveSource,
			// Token: 0x04000323 RID: 803
			MoveTarget,
			// Token: 0x04000324 RID: 804
			InvalidatedByMove
		}
	}
}
