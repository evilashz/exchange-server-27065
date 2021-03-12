using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000019 RID: 25
	public abstract class Item : PrivateObjectPropertyBag
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x0002B79C File Offset: 0x0002999C
		protected Item(Context context, Table table, PhysicalColumn sizeColumn, Mailbox mailbox, bool changeTrackingEnabled, bool newItem, bool existsInDatabase, bool writeThrough, params ColumnValue[] initialValues) : base(context, table, newItem, changeTrackingEnabled, !existsInDatabase, writeThrough, initialValues)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				mailbox.IsValid();
				this.mailbox = mailbox;
				if (!base.IsDead)
				{
					base.LoadData(context);
					this.sizeColumn = sizeColumn;
					this.isNew = newItem;
					if (existsInDatabase)
					{
						byte[] array = (byte[])this.GetPropertyValue(context, this.ItemSubobjectsBinPropTag);
						if (array != null)
						{
							this.subobjects = new SubobjectCollection(this, array);
						}
						this.currentSize = (long)base.GetColumnValue(context, sizeColumn);
						if (!newItem)
						{
							this.originalSize = this.currentSize;
						}
					}
					else
					{
						base.SetColumn(context, sizeColumn, this.currentSize);
					}
					disposeGuard.Success();
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0002B880 File Offset: 0x00029A80
		protected Item(Context context, Table table, PhysicalColumn sizeColumn, Mailbox mailbox, bool changeTrackingEnabled, bool writeThrough, Reader reader) : base(context, table, changeTrackingEnabled, writeThrough, reader)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				mailbox.IsValid();
				this.mailbox = mailbox;
				if (!base.IsDead)
				{
					base.LoadData(context);
					this.sizeColumn = sizeColumn;
					this.isNew = false;
					byte[] array = (byte[])this.GetPropertyValue(context, this.ItemSubobjectsBinPropTag);
					if (array != null)
					{
						this.subobjects = new SubobjectCollection(this, array);
					}
					this.currentSize = (long)base.GetColumnValue(context, sizeColumn);
					this.originalSize = this.currentSize;
					disposeGuard.Success();
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0002B93C File Offset: 0x00029B3C
		public bool IsNew
		{
			get
			{
				return this.isNew;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0002B944 File Offset: 0x00029B44
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty || (this.subobjects != null && this.subobjects.IsDirty);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0002B965 File Offset: 0x00029B65
		protected override bool IsDirtyExceptDataRow
		{
			get
			{
				return base.IsDirtyExceptDataRow || (this.subobjects != null && this.subobjects.IsDirty);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0002B986 File Offset: 0x00029B86
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0002B98E File Offset: 0x00029B8E
		public override Context CurrentOperationContext
		{
			get
			{
				return this.Mailbox.CurrentOperationContext;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0002B99B File Offset: 0x00029B9B
		public override ReplidGuidMap ReplidGuidMap
		{
			get
			{
				return this.Mailbox.ReplidGuidMap;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public long CurrentSize
		{
			get
			{
				return this.currentSize;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public long CurrentSizeEstimateWithoutChildren
		{
			get
			{
				long num = this.CurrentSize;
				if (this.Subobjects != null)
				{
					foreach (int childNumber in this.Subobjects.GetChildNumbers())
					{
						long childSize = this.Subobjects.GetChildSize(childNumber);
						if (childSize != -1L)
						{
							num -= childSize;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0002BA24 File Offset: 0x00029C24
		public long OriginalSize
		{
			get
			{
				return this.originalSize;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0002BA2C File Offset: 0x00029C2C
		public int ChildrenCount
		{
			get
			{
				if (this.subobjects != null)
				{
					return this.subobjects.ChildrenCount;
				}
				return 0;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0002BA43 File Offset: 0x00029C43
		public int DescendantCount
		{
			get
			{
				if (this.subobjects != null)
				{
					return this.subobjects.DescendantCount;
				}
				return 0;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0002BA5A File Offset: 0x00029C5A
		public SubobjectCollection Subobjects
		{
			get
			{
				return this.subobjects;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0002BA62 File Offset: 0x00029C62
		public int SubobjectsChangeCookie
		{
			get
			{
				return this.subobjectsChangeCookie;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0002BA6A File Offset: 0x00029C6A
		public override ObjectPropertySchema Schema
		{
			get
			{
				if (this.propertySchema == null)
				{
					this.propertySchema = PropertySchema.GetObjectSchema(this.Mailbox.Database, this.GetObjectType());
				}
				return this.propertySchema;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600044B RID: 1099
		protected abstract StorePropTag ItemSubobjectsBinPropTag { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0002BA96 File Offset: 0x00029C96
		internal SubobjectReferenceState SubobjectReferenceState
		{
			get
			{
				if (this.subobjectReferenceState == null)
				{
					this.subobjectReferenceState = SubobjectReferenceState.GetState(this.Mailbox, true);
				}
				return this.subobjectReferenceState;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		protected int ReserveChildNumber()
		{
			if (this.subobjects == null)
			{
				this.subobjects = new SubobjectCollection(this);
			}
			return this.subobjects.ReserveChildNumber();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0002BADC File Offset: 0x00029CDC
		protected void UpdateSubobjects(Context context)
		{
			if (this.subobjects != null && this.subobjects.IsDirty)
			{
				byte[] value = this.subobjects.Serialize(true, true);
				this.SetProperty(context, this.ItemSubobjectsBinPropTag, value);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0002BB1B File Offset: 0x00029D1B
		protected override StorePropTag MapPropTag(Context context, uint propertyTag)
		{
			return this.Mailbox.GetStorePropTag(context, propertyTag, this.GetObjectType());
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0002BB30 File Offset: 0x00029D30
		public override void Flush(Context context, bool flushLargeDirtyStreams)
		{
			if (this.IsDirty)
			{
				this.CopyOnWrite(context);
				this.UpdateSubobjects(context);
				base.SetColumn(context, this.sizeColumn, this.currentSize);
				base.Flush(context, flushLargeDirtyStreams);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0002BB68 File Offset: 0x00029D68
		public int GetLargeDirtyStreamsSize()
		{
			return this.DataRow.GetLargeDirtyStreamsSize();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0002BB78 File Offset: 0x00029D78
		public virtual IChunked PrepareToSaveChanges(Context context)
		{
			this.Flush(context, false);
			return new ChunkedEnumerable("stream flush", this.DataRow.FlushDirtyStreams(context), this.Mailbox.SharedState, TimeSpan.FromMilliseconds(10.0), TimeSpan.FromMilliseconds(1500.0));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0002BBCA File Offset: 0x00029DCA
		public virtual bool SaveChanges(Context context)
		{
			this.CopyOnWrite(context);
			this.Flush(context, true);
			this.isNew = false;
			this.CommitChanges();
			this.originalSize = this.currentSize;
			return true;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		internal virtual void SaveChild(Context context, ISubobject child)
		{
			if (base.IsDead)
			{
				throw new StoreException((LID)56952U, ErrorCodeValue.ObjectDeleted);
			}
			long sizeChange = child.CurrentSize - this.subobjects.GetChildSize(child.ChildNumber);
			this.SizeChange(sizeChange);
			long? childInid = this.subobjects.GetChildInid(child.ChildNumber);
			long value = child.CurrentInid.Value;
			this.subobjects.AddOrUpdateChild(context, child.ChildNumber, value, child.CurrentSize);
			if (child.OriginalInid == childInid)
			{
				if (child.Subobjects != null)
				{
					this.subobjects.Add(context, child.Subobjects);
					this.subobjects.DeleteDeleted(context, child.Subobjects);
				}
			}
			else
			{
				if (childInid != null)
				{
					using (Item item = this.OpenChild(context, child.ChildNumber, childInid.Value))
					{
						if (item.Subobjects != null)
						{
							this.subobjects.Delete(context, item.Subobjects);
						}
					}
				}
				if (child.Subobjects != null)
				{
					this.subobjects.Add(context, child.Subobjects);
				}
			}
			this.subobjectsChangeCookie++;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0002BD5C File Offset: 0x00029F5C
		internal virtual void DeleteChild(Context context, ISubobject child)
		{
			if (base.IsDead)
			{
				throw new StoreException((LID)44664U, ErrorCodeValue.ObjectDeleted);
			}
			long? childInid = this.subobjects.GetChildInid(child.ChildNumber);
			if (childInid == null)
			{
				return;
			}
			if (child.OriginalInid == childInid)
			{
				if (child.Subobjects != null)
				{
					this.subobjects.Delete(context, child.Subobjects);
					this.subobjects.DeleteDeleted(context, child.Subobjects);
				}
			}
			else
			{
				using (Item item = this.OpenChild(context, child.ChildNumber, childInid.Value))
				{
					if (item.Subobjects != null)
					{
						this.subobjects.Delete(context, item.Subobjects);
					}
				}
			}
			this.SizeChange(-this.subobjects.GetChildSize(child.ChildNumber));
			this.subobjects.DeleteChild(context, child.ChildNumber, child.CurrentSize);
			this.subobjectsChangeCookie++;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0002BE88 File Offset: 0x0002A088
		public override void Scrub(Context context)
		{
			if (this.subobjects != null)
			{
				foreach (int childNumber in this.subobjects.GetChildNumbers())
				{
					long value = this.subobjects.GetChildInid(childNumber).Value;
					using (Item item = this.OpenChild(context, childNumber, value))
					{
						item.Delete(context);
					}
				}
				this.subobjects.ResetNextChildNumber();
				this.subobjects.SetDirty();
			}
			base.Scrub(context);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0002BF38 File Offset: 0x0002A138
		protected void DeepCopySubobjects(Context context)
		{
			if (this.subobjects != null)
			{
				foreach (int childNumber in this.subobjects.GetChildNumbers())
				{
					long value = this.subobjects.GetChildInid(childNumber).Value;
					using (Item item = this.CopyChild(context, childNumber, value))
					{
						item.SaveChanges(context);
					}
				}
			}
		}

		// Token: 0x06000458 RID: 1112
		protected abstract Item OpenChild(Context context, int childNumber, long inid);

		// Token: 0x06000459 RID: 1113
		protected abstract Item CopyChild(Context context, int childNumber, long inid);

		// Token: 0x0600045A RID: 1114 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		protected override bool TrackSizeChangeForColumn(Column column)
		{
			return true;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0002BFCF File Offset: 0x0002A1CF
		protected override bool TrackValueChangeForColumn(Column column)
		{
			return true;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0002BFD2 File Offset: 0x0002A1D2
		protected override void OnDirty(Context context)
		{
			base.OnDirty(context);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0002BFDB File Offset: 0x0002A1DB
		protected override void OnColumnChanged(Column column, long deltaSize)
		{
			this.SizeChange(deltaSize);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		protected override void OnPropertyChanged(StorePropTag propTag, long deltaSize)
		{
			this.SizeChange(deltaSize);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0002BFED File Offset: 0x0002A1ED
		protected void ResetIsNew()
		{
			this.isNew = false;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0002BFF6 File Offset: 0x0002A1F6
		internal void SizeChange(long sizeChange)
		{
			this.currentSize += sizeChange;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0002C008 File Offset: 0x0002A208
		protected void ReleaseDescendants(Context context, bool calledFromFinalizer)
		{
			SubobjectCollection subobjectCollection = Interlocked.Exchange<SubobjectCollection>(ref this.subobjects, null);
			if (subobjectCollection != null)
			{
				subobjectCollection.ReleaseAll(calledFromFinalizer, context);
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0002C02D File Offset: 0x0002A22D
		protected override void InternalDispose(bool calledFromDispose)
		{
			this.ReleaseDescendants((calledFromDispose && this.Mailbox != null) ? this.Mailbox.CurrentOperationContext : null, !calledFromDispose);
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0002C059 File Offset: 0x0002A259
		[Conditional("DEBUG")]
		internal void AssertHasChild(long inid)
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0002C05B File Offset: 0x0002A25B
		[Conditional("DEBUG")]
		internal void AssertHasAllDescendants(SubobjectCollection subobjects)
		{
		}

		// Token: 0x04000215 RID: 533
		private readonly Mailbox mailbox;

		// Token: 0x04000216 RID: 534
		private PhysicalColumn sizeColumn;

		// Token: 0x04000217 RID: 535
		private bool isNew;

		// Token: 0x04000218 RID: 536
		private long currentSize;

		// Token: 0x04000219 RID: 537
		private long originalSize;

		// Token: 0x0400021A RID: 538
		private SubobjectCollection subobjects;

		// Token: 0x0400021B RID: 539
		private int subobjectsChangeCookie;

		// Token: 0x0400021C RID: 540
		private SubobjectReferenceState subobjectReferenceState;

		// Token: 0x0400021D RID: 541
		private ObjectPropertySchema propertySchema;
	}
}
