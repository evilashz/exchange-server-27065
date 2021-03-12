using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	internal abstract class DataInternalComponent : IDataWithinRowComponent, IDataObjectComponent
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x00018E69 File Offset: 0x00017069
		protected DataInternalComponent(DataRow dataRow)
		{
			this.dataRow = dataRow;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600062A RID: 1578
		public abstract bool PendingDatabaseUpdates { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00018E78 File Offset: 0x00017078
		public virtual int PendingDatabaseUpdateCount
		{
			get
			{
				if (!this.PendingDatabaseUpdates)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00018E85 File Offset: 0x00017085
		public virtual bool WithinParentRow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00018E88 File Offset: 0x00017088
		protected DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x0600062E RID: 1582
		public abstract void LoadFromParentRow(DataTableCursor cursor);

		// Token: 0x0600062F RID: 1583
		public abstract void SaveToParentRow(DataTableCursor cursor, Func<bool> checkpointCallback);

		// Token: 0x06000630 RID: 1584 RVA: 0x00018E90 File Offset: 0x00017090
		public virtual void Cleanup()
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00018E92 File Offset: 0x00017092
		public virtual void MinimizeMemory()
		{
		}

		// Token: 0x06000632 RID: 1586
		public abstract void CloneFrom(IDataObjectComponent other);

		// Token: 0x040002FB RID: 763
		[NonSerialized]
		private readonly DataRow dataRow;
	}
}
