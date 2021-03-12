using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200009F RID: 159
	public abstract class ObjectPropertyBag : PropertyBag, IColumnValueBag, IDisposableImpl, IDisposable, IColumnStreamAccess
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001F2C9 File Offset: 0x0001D4C9
		protected ObjectPropertyBag(Context context, bool changeTrackingEnabled) : base(changeTrackingEnabled)
		{
			this.disposableImpl = new DisposableImpl<ObjectPropertyBag>(this);
			this.renameDictionary = this.GetColumnRenames(context);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		~ObjectPropertyBag()
		{
			this.disposableImpl.FinalizeImpl(this);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001F320 File Offset: 0x0001D520
		public bool IsDisposed
		{
			get
			{
				return this.disposableImpl.IsDisposed;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001F32D File Offset: 0x0001D52D
		public virtual bool NeedsToPublishNotification
		{
			get
			{
				return this.IsDirty;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001F335 File Offset: 0x0001D535
		public bool IsSaved
		{
			get
			{
				return this.DataRow != null && !this.DataRow.IsNew;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001F34F File Offset: 0x0001D54F
		public bool IsDead
		{
			get
			{
				return this.DataRow == null || this.DataRow.IsDead;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001F366 File Offset: 0x0001D566
		public Table Table
		{
			get
			{
				this.CheckNotDead();
				return this.DataRow.Table;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001F379 File Offset: 0x0001D579
		public override bool IsDirty
		{
			get
			{
				return !this.IsDead && (base.IsDirty || this.DataRow.IsDirty);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001F39A File Offset: 0x0001D59A
		protected virtual bool IsDirtyExceptDataRow
		{
			get
			{
				return base.IsDirty;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001F3A2 File Offset: 0x0001D5A2
		public override bool IsChanged
		{
			get
			{
				this.CheckNotDead();
				return base.IsChanged || this.dataRowChanged;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001F3BA File Offset: 0x0001D5BA
		public bool OffPagePropertyBlobLoaded
		{
			get
			{
				return this.offpagePropertyBlobLoaded;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005AC RID: 1452
		// (set) Token: 0x060005AD RID: 1453
		internal abstract DataRow DataRow { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001F3C2 File Offset: 0x0001D5C2
		internal DataRow DataRowForTest
		{
			get
			{
				return this.DataRow;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001F3CA File Offset: 0x0001D5CA
		public override void MakeClean()
		{
			this.CheckNotDead();
			base.MakeClean();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		public override void CommitChanges()
		{
			this.CheckNotDead();
			if (base.ChangeTrackingEnabled && this.IsChanged)
			{
				base.CommitChanges();
				this.dataRowChanged = false;
				this.changedColumns.SetAll(false);
				Array.Clear(this.originalColumnValues, 0, this.originalColumnValues.Length);
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001F428 File Offset: 0x0001D628
		public bool CheckTableExists(Context context)
		{
			return this.DataRow != null && this.DataRow.CheckTableExists(context);
		}

		// Token: 0x060005B2 RID: 1458
		protected abstract ObjectType GetObjectType();

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001F440 File Offset: 0x0001D640
		protected void LoadData(Context context)
		{
			if (this.DataRow != null)
			{
				this.LoadOnPageBlob(context);
				if (!this.IsSaved)
				{
					this.LoadOffPageBlob(context);
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001F460 File Offset: 0x0001D660
		public override object GetBlobPropertyValue(Context context, StorePropTag propTag)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			object obj;
			if (base.TryGetBlobPropertyValue(context, propTag, out obj) && !(obj is ValueReference))
			{
				return obj;
			}
			if (this.OffPagePropertyBlobLoaded || (this.IsPropertyPromotedByDefault(context, propTag.PropId) && !(obj is ValueReference)))
			{
				return null;
			}
			this.LoadOffPageBlob(context);
			return base.GetBlobPropertyValue(context, propTag);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001F4C4 File Offset: 0x0001D6C4
		public override bool TryGetBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			if (base.TryGetBlobProperty(context, propId, out propTag, out value) && !(value is ValueReference))
			{
				return value != null;
			}
			if (this.OffPagePropertyBlobLoaded || (this.IsPropertyPromotedByDefault(context, propTag.PropId) && !(value is ValueReference)))
			{
				value = null;
				return false;
			}
			this.LoadOffPageBlob(context);
			return base.TryGetBlobProperty(context, propId, out propTag, out value);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001F538 File Offset: 0x0001D738
		public override object GetPhysicalColumnValue(Context context, PhysicalColumn column)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			Column column2;
			if (this.renameDictionary != null && this.renameDictionary.TryGetValue(column, out column2))
			{
				return column2.Evaluate(this);
			}
			return this.DataRow.GetValue(context, column);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001F57F File Offset: 0x0001D77F
		public override void EnumerateBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			this.LoadOffPageBlobIfNecessary(context);
			base.EnumerateBlobProperties(context, action, showValue);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		public override void SetBlobProperty(Context context, StorePropTag propTag, object value)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			if (!this.OffPagePropertyBlobLoaded)
			{
				StorePropTag storePropTag;
				object obj;
				bool flag = base.TryGetBlobProperty(context, propTag.PropId, out storePropTag, out obj);
				if ((!flag && !this.IsPropertyPromotedByDefault(context, propTag.PropId)) || (flag && obj is ValueReference))
				{
					this.LoadOffPageBlob(context);
				}
			}
			base.SetBlobProperty(context, propTag, value, !this.OffPagePropertyBlobLoaded);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001F60C File Offset: 0x0001D80C
		public override void SetPhysicalColumn(Context context, PhysicalColumn column, object value)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			this.SetColumn(context, column, value);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001F624 File Offset: 0x0001D824
		public override ErrorCode OpenPhysicalColumnReadStream(Context context, PhysicalColumn column, out Stream stream)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			return this.OpenReadStream(context, column, out stream);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001F63C File Offset: 0x0001D83C
		public override ErrorCode OpenPhysicalColumnWriteStream(Context context, PhysicalColumn column, out Stream stream)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			stream = this.OpenWriteStream(context, column);
			return ErrorCode.NoError;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001F65A File Offset: 0x0001D85A
		public override bool IsPhysicalColumnChanged(Context context, PhysicalColumn column)
		{
			return this.IsColumnChanged(context, column);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001F664 File Offset: 0x0001D864
		public override object GetOriginalPhysicalColumnValue(Context context, PhysicalColumn column)
		{
			return this.GetOriginalColumnValue(context, column);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001F670 File Offset: 0x0001D870
		public object GetColumnValue(Context context, Column column)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
			if (extendedPropertyColumn != null)
			{
				return this.GetPropertyValue(context, extendedPropertyColumn.StorePropTag);
			}
			return this.GetPhysicalColumnValue(context, (PhysicalColumn)column);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001F6B5 File Offset: 0x0001D8B5
		public bool CheckAlive(Context context)
		{
			return !this.IsDead;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public void SetColumn(Context context, PhysicalColumn column, object value)
		{
			this.CheckNotDead();
			this.OnAccess(context);
			if (!this.SaveColumnPreImage(context, column, true, value))
			{
				return;
			}
			int num = 0;
			bool flag = this.TrackSizeChangeForColumn(column);
			if (flag)
			{
				num = -this.DataRow.GetColumnSize(context, column).GetValueOrDefault();
			}
			if (!this.IsDirty)
			{
				this.OnDirty(context);
			}
			this.DataRow.SetValue(context, column, value);
			if (flag)
			{
				num += this.DataRow.GetColumnSize(context, column).GetValueOrDefault();
			}
			this.OnColumnChanged(column, (long)num);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001F74C File Offset: 0x0001D94C
		protected void DirtyColumn(Context context, PhysicalColumn column)
		{
			this.CheckNotDead();
			if (!this.IsDirty)
			{
				this.OnDirty(context);
			}
			this.DataRow.DirtyValue(context, column);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001F770 File Offset: 0x0001D970
		public bool IsColumnChanged(Context context, Column column)
		{
			this.CheckNotDead();
			ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
			if (extendedPropertyColumn != null)
			{
				return this.IsPropertyChanged(context, extendedPropertyColumn.StorePropTag);
			}
			if (!this.dataRowChanged)
			{
				return false;
			}
			Column column2;
			if (this.renameDictionary == null || !this.renameDictionary.TryGetValue(column, out column2))
			{
				return this.changedColumns[((PhysicalColumn)column).Index];
			}
			if (column2 is FunctionColumn)
			{
				object originalColumnValue = this.GetOriginalColumnValue(context, column);
				object obj = column2.Evaluate(this);
				return obj.Equals(originalColumnValue);
			}
			return this.IsColumnChanged(context, column2);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001F804 File Offset: 0x0001DA04
		public object GetOriginalColumnValue(Context context, Column column)
		{
			this.CheckNotDead();
			ExtendedPropertyColumn extendedPropertyColumn = column as ExtendedPropertyColumn;
			if (extendedPropertyColumn != null)
			{
				return this.GetOriginalPropertyValue(context, extendedPropertyColumn.StorePropTag);
			}
			Column column2;
			if (this.renameDictionary != null && this.renameDictionary.TryGetValue(column, out column2))
			{
				return column2.Evaluate(base.OriginalBag);
			}
			if (this.dataRowChanged && this.changedColumns[((PhysicalColumn)column).Index])
			{
				return this.originalColumnValues[((PhysicalColumn)column).Index];
			}
			return this.DataRow.GetValue(context, (PhysicalColumn)column);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001F89F File Offset: 0x0001DA9F
		protected Stream OpenWriteStream(Context context, PhysicalColumn column)
		{
			this.CheckNotDead();
			this.SetColumn(context, column, new byte[0]);
			return new PhysicalColumnStream(this, column, false);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001F8C0 File Offset: 0x0001DAC0
		private ErrorCode OpenReadStream(Context context, PhysicalColumn column, out Stream stream)
		{
			this.CheckNotDead();
			stream = null;
			if (this.DataRow.GetColumnSize(context, column) == null)
			{
				return ErrorCode.CreateNotFound((LID)44304U);
			}
			stream = new PhysicalColumnStream(this, column, true);
			return ErrorCode.NoError;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001F90C File Offset: 0x0001DB0C
		public void SetPrimaryKey(params ColumnValue[] primaryKeyValues)
		{
			this.CheckNotDead();
			this.DataRow.SetPrimaryKey(primaryKeyValues);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001F920 File Offset: 0x0001DB20
		protected virtual void CopyOnWrite(Context context)
		{
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001F922 File Offset: 0x0001DB22
		internal virtual void AutoSave(Context context)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001F924 File Offset: 0x0001DB24
		public void Flush(Context context)
		{
			this.Flush(context, true);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001F930 File Offset: 0x0001DB30
		public virtual void Flush(Context context, bool flushLargeDirtyStreams)
		{
			this.CheckNotDead();
			if (base.IsDirty || this.DataRow.IsNew)
			{
				this.UpdateBlobColumns(context);
			}
			if (this.DataRow.IsDirty)
			{
				this.OnBeforeDataRowFlushOrDelete(context, false);
				try
				{
					this.DataRow.Flush(context, flushLargeDirtyStreams);
				}
				catch (DuplicateKeyException exception)
				{
					context.OnExceptionCatch(exception);
					if (!this.RecoverFromDuplicateKey(context))
					{
						throw;
					}
					DiagnosticContext.TraceLocation((LID)64384U);
					this.DataRow.Flush(context, flushLargeDirtyStreams);
				}
				this.OnAfterDataRowFlushOrDelete(context, false);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		public virtual void Delete(Context context)
		{
			this.Delete(context, true);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001F9D6 File Offset: 0x0001DBD6
		protected virtual bool RecoverFromDuplicateKey(Context context)
		{
			return false;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001F9DC File Offset: 0x0001DBDC
		protected virtual void Delete(Context context, bool notifyParent)
		{
			this.CheckNotDead();
			base.MakeClean();
			if (notifyParent)
			{
				this.OnBeforeDataRowFlushOrDelete(context, true);
			}
			try
			{
				this.DataRowDeletionImplementation(context);
			}
			finally
			{
				this.DataRow.Dispose();
				this.DataRow = null;
				if (notifyParent)
				{
					this.OnAfterDataRowFlushOrDelete(context, true);
				}
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001FA38 File Offset: 0x0001DC38
		protected virtual void DataRowDeletionImplementation(Context context)
		{
			this.DataRow.Delete(context);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001FA46 File Offset: 0x0001DC46
		public void MarkAsDeleted(Context context)
		{
			this.CheckNotDead();
			base.MakeClean();
			base.CommitChanges();
			this.DataRow.MarkAsDeleted(context);
			this.DataRow.Dispose();
			this.DataRow = null;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001FA78 File Offset: 0x0001DC78
		public override void DiscardPrivateCache(Context context)
		{
			this.dataRowChanged = false;
			this.changedColumns = null;
			this.originalColumnValues = null;
			if (!this.IsDead && this.Table.SpecialCols.OffPagePropertyBlob != null)
			{
				this.offpagePropertyBlobLoaded = false;
			}
			base.DiscardPrivateCache(context);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		public override ulong GetChangedPropertyGroups(Context context)
		{
			ulong num = base.GetChangedPropertyGroups(context);
			if (this.dataRowChanged && this.Schema != null)
			{
				for (int i = 0; i < this.originalColumnValues.Length; i++)
				{
					if (this.changedColumns[i])
					{
						num |= this.Schema.ColumnGroups[i];
					}
				}
			}
			return num;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001FB1F File Offset: 0x0001DD1F
		protected virtual bool TrackSizeChangeForColumn(Column column)
		{
			return false;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001FB22 File Offset: 0x0001DD22
		protected virtual bool TrackValueChangeForColumn(Column column)
		{
			return true;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001FB25 File Offset: 0x0001DD25
		protected virtual void OnColumnChanged(Column column, long deltaSize)
		{
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001FB27 File Offset: 0x0001DD27
		protected virtual void OnBeforeDataRowFlushOrDelete(Context context, bool delete)
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001FB29 File Offset: 0x0001DD29
		protected virtual void OnAfterDataRowFlushOrDelete(Context context, bool delete)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001FB2B File Offset: 0x0001DD2B
		internal int ReadColumn(Context context, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			this.CheckNotDead();
			return this.DataRow.ReadStream(context, column, position, buffer, offset, count);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001FB48 File Offset: 0x0001DD48
		internal void WriteColumn(Context context, PhysicalColumn column, long position, byte[] buffer, int offset, int count)
		{
			this.CheckNotDead();
			this.SaveColumnPreImage(context, column, false, null);
			if (this.DataRow.WriteThrough)
			{
				this.CopyOnWrite(context);
			}
			if (!this.IsDirty)
			{
				this.OnDirty(context);
			}
			long deltaSize;
			this.DataRow.WriteStream(context, column, position, buffer, offset, count, out deltaSize);
			this.OnColumnChanged(column, deltaSize);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		internal int GetColumnLength(Context context, PhysicalColumn column)
		{
			this.CheckNotDead();
			return this.DataRow.GetColumnSize(context, column).GetValueOrDefault();
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		internal void LoadOffPageBlobForTest(Context context)
		{
			this.LoadOffPageBlobIfNecessary(context);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001FBD9 File Offset: 0x0001DDD9
		int IColumnStreamAccess.GetColumnSize(PhysicalColumn column)
		{
			return this.GetColumnLength(this.CurrentOperationContext, column);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		int IColumnStreamAccess.ReadStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			return this.ReadColumn(this.CurrentOperationContext, physicalColumn, position, buffer, offset, count);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001FBFD File Offset: 0x0001DDFD
		void IColumnStreamAccess.WriteStream(PhysicalColumn physicalColumn, long position, byte[] buffer, int offset, int count)
		{
			this.WriteColumn(this.CurrentOperationContext, physicalColumn, position, buffer, offset, count);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001FC14 File Offset: 0x0001DE14
		private void UpdateBlobColumns(Context context)
		{
			this.CheckNotDead();
			if (this.Table.SpecialCols.PropertyBlob != null || this.Table.SpecialCols.OffPagePropertyBlob != null)
			{
				if (!this.IsDirty)
				{
					this.OnDirty(context);
				}
				object obj = null;
				try
				{
					HashSet<ushort> additionalPromotedProperties = null;
					HashSet<ushort> hashSet = null;
					HashSet<ushort> alwaysPromotedProperties = null;
					if (this.Table.SpecialCols.OffPagePropertyBlob != null)
					{
						additionalPromotedProperties = this.GetAdditionalPromotedPropertyIds(context);
						hashSet = this.GetDefaultPromotedPropertyIds(context);
						alwaysPromotedProperties = this.GetAlwaysPromotedPropertyIds(context);
					}
					byte[] array;
					if (this.Table.SpecialCols.OffPagePropertyBlob != null && this.OffPagePropertyBlobLoaded)
					{
						PropertyBlob.BuildTwoBlobs(this.Properties, alwaysPromotedProperties, hashSet, additionalPromotedProperties, out array, out obj);
					}
					else
					{
						array = PropertyBlob.BuildBlob(this.Properties, hashSet);
						obj = null;
						if (this.Table.SpecialCols.OffPagePropertyBlob != null && array != null && array.Length > 3110)
						{
							this.LoadOffPageBlob(context);
							PropertyBlob.BuildTwoBlobs(this.Properties, alwaysPromotedProperties, hashSet, additionalPromotedProperties, out array, out obj);
						}
					}
					if (this.Table.SpecialCols.PropertyBlob != null)
					{
						this.DataRow.SetValue(context, this.Table.SpecialCols.PropertyBlob, array);
					}
					if (this.Table.SpecialCols.OffPagePropertyBlob != null && this.OffPagePropertyBlobLoaded)
					{
						this.DataRow.SetValue(context, this.Table.SpecialCols.OffPagePropertyBlob, obj);
						obj = null;
					}
				}
				finally
				{
					IDisposable disposable = obj as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			base.MakeClean();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		private void LoadOnPageBlob(Context context)
		{
			this.CheckNotDead();
			if (this.Table.SpecialCols.PropertyBlob != null && this.Properties == null)
			{
				this.offpagePropertyBlobLoaded = false;
				byte[] propertyBlob = (byte[])this.DataRow.GetValue(context, this.Table.SpecialCols.PropertyBlob);
				Dictionary<ushort, KeyValuePair<StorePropTag, object>> dictionary = null;
				base.LoadPropertiesFromPropertyBlob(context, propertyBlob, ref dictionary);
				if (dictionary == null)
				{
					dictionary = new Dictionary<ushort, KeyValuePair<StorePropTag, object>>(3);
				}
				this.AssignPropertiesToUse(dictionary);
			}
			if (this.Table.SpecialCols.OffPagePropertyBlob == null)
			{
				this.offpagePropertyBlobLoaded = true;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001FE6C File Offset: 0x0001E06C
		private void Refresh(Context context)
		{
			this.CheckNotDead();
			this.LoadOnPageBlob(context);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001FE7C File Offset: 0x0001E07C
		private void LoadOffPageBlob(Context context)
		{
			this.CheckNotDead();
			if (this.Table.SpecialCols.OffPagePropertyBlob != null)
			{
				object value = this.DataRow.GetValue(context, this.Table.SpecialCols.OffPagePropertyBlob);
				if (value != null)
				{
					Dictionary<ushort, KeyValuePair<StorePropTag, object>> properties = this.Properties;
					byte[] array = value as byte[];
					if (array != null)
					{
						base.LoadPropertiesFromPropertyBlob(context, array, ref properties);
					}
					else
					{
						using (Stream stream = new PhysicalColumnStream(this, this.Table.SpecialCols.OffPagePropertyBlob, true))
						{
							base.LoadPropertiesFromPropertyBlobStream(context, stream, ref properties);
						}
					}
					if (properties != this.Properties)
					{
						this.AssignPropertiesToUse(properties);
					}
				}
				this.RemoveNullProperties();
			}
			this.offpagePropertyBlobLoaded = true;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001FF44 File Offset: 0x0001E144
		public virtual HashSet<ushort> GetDefaultPromotedPropertyIds(Context context)
		{
			return null;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001FF47 File Offset: 0x0001E147
		public virtual HashSet<ushort> GetAlwaysPromotedPropertyIds(Context context)
		{
			return null;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001FF4A File Offset: 0x0001E14A
		public virtual HashSet<ushort> GetAdditionalPromotedPropertyIds(Context context)
		{
			return null;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001FF50 File Offset: 0x0001E150
		private bool IsPropertyPromotedByDefault(Context context, ushort propId)
		{
			HashSet<ushort> defaultPromotedPropertyIds = this.GetDefaultPromotedPropertyIds(context);
			return defaultPromotedPropertyIds != null && defaultPromotedPropertyIds.Contains(propId);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001FF74 File Offset: 0x0001E174
		private void RemoveNullProperties()
		{
			if (this.Properties != null && this.Properties.Count != 0)
			{
				ushort[] array = new ushort[this.Properties.Count];
				int num = 0;
				foreach (KeyValuePair<ushort, KeyValuePair<StorePropTag, object>> keyValuePair in this.Properties)
				{
					if (keyValuePair.Value.Value == null || keyValuePair.Value.Value is ValueReference)
					{
						array[num++] = keyValuePair.Key;
					}
				}
				for (int i = 0; i < num; i++)
				{
					this.Properties.Remove(array[i]);
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00020044 File Offset: 0x0001E244
		private bool SaveColumnPreImage(Context context, PhysicalColumn column, bool compareValue, object newValue)
		{
			this.CheckNotDead();
			bool flag = this.DataRow.ColumnFetched(column);
			if (compareValue && flag)
			{
				object value = this.DataRow.GetValue(context, column);
				if (ValueHelper.ValuesEqual(value, newValue))
				{
					return false;
				}
			}
			if (base.ChangeTrackingEnabled && this.TrackValueChangeForColumn(column))
			{
				if (this.dataRowChanged && this.changedColumns[column.Index])
				{
					if (compareValue && ValueHelper.ValuesEqual(this.originalColumnValues[column.Index], newValue))
					{
						this.changedColumns[column.Index] = false;
						this.originalColumnValues[column.Index] = null;
					}
				}
				else
				{
					object value2 = this.DataRow.GetValue(context, column);
					if (compareValue && !flag && ValueHelper.ValuesEqual(value2, newValue))
					{
						return false;
					}
					this.SaveColumnPreImage(column, value2);
				}
			}
			return true;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00020118 File Offset: 0x0001E318
		protected void SaveColumnPreImage(PhysicalColumn column, object currentValue)
		{
			if (this.changedColumns == null)
			{
				this.changedColumns = new BitArray(this.Table.Columns.Count);
				this.originalColumnValues = new object[this.Table.Columns.Count];
			}
			this.changedColumns[column.Index] = true;
			this.originalColumnValues[column.Index] = currentValue;
			this.dataRowChanged = true;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0002018A File Offset: 0x0001E38A
		protected void ForgetColumnPreImage(PhysicalColumn column)
		{
			if (this.changedColumns == null)
			{
				return;
			}
			this.changedColumns[column.Index] = false;
			this.originalColumnValues[column.Index] = null;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000201B5 File Offset: 0x0001E3B5
		public override void Scrub(Context context)
		{
			this.LoadOffPageBlobIfNecessary(context);
			base.Scrub(context);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000201C5 File Offset: 0x0001E3C5
		public void Dispose()
		{
			this.disposableImpl.DisposeImpl(this);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000201D3 File Offset: 0x0001E3D3
		protected void CheckDisposed()
		{
			this.disposableImpl.CheckDisposed(this);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000201E1 File Offset: 0x0001E3E1
		protected virtual DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ObjectPropertyBag>(this);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000201E9 File Offset: 0x0001E3E9
		protected virtual void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000201EB File Offset: 0x0001E3EB
		protected virtual IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			return null;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000201EE File Offset: 0x0001E3EE
		DisposeTracker IDisposableImpl.InternalGetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000201F6 File Offset: 0x0001E3F6
		void IDisposableImpl.InternalDispose(bool calledFromDispose)
		{
			this.InternalDispose(calledFromDispose);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000201FF File Offset: 0x0001E3FF
		protected void CheckNotDead()
		{
			if (this.IsDead)
			{
				throw new StoreException((LID)34936U, ErrorCodeValue.ObjectDeleted, "Object is already deleted");
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00020223 File Offset: 0x0001E423
		private void LoadOffPageBlobIfNecessary(Context context)
		{
			if (!this.OffPagePropertyBlobLoaded)
			{
				this.LoadOffPageBlob(context);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00020234 File Offset: 0x0001E434
		protected virtual void OnAccess(Context context)
		{
			if (this.Properties == null && !this.IsDirty && !base.ChangeTrackingEnabled)
			{
				this.Refresh(context);
			}
		}

		// Token: 0x040003F5 RID: 1013
		private readonly IReadOnlyDictionary<Column, Column> renameDictionary;

		// Token: 0x040003F6 RID: 1014
		private DisposableImpl<ObjectPropertyBag> disposableImpl;

		// Token: 0x040003F7 RID: 1015
		protected bool offpagePropertyBlobLoaded;

		// Token: 0x040003F8 RID: 1016
		protected bool dataRowChanged;

		// Token: 0x040003F9 RID: 1017
		private BitArray changedColumns;

		// Token: 0x040003FA RID: 1018
		private object[] originalColumnValues;
	}
}
