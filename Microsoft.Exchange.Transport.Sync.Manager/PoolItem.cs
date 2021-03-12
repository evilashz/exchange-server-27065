using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PoolItem<TItem> where TItem : class
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal PoolItem(TItem item, uint id)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			this.item = item;
			this.id = id;
			this.creationTime = (this.lastUsedTime = ExDateTime.UtcNow);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002DFD File Offset: 0x00000FFD
		internal TItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002E05 File Offset: 0x00001005
		internal uint ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002E0D File Offset: 0x0000100D
		internal ExDateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002E15 File Offset: 0x00001015
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002E1D File Offset: 0x0000101D
		internal ExDateTime LastUsedTime
		{
			get
			{
				return this.lastUsedTime;
			}
			set
			{
				this.lastUsedTime = value;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E28 File Offset: 0x00001028
		public override bool Equals(object obj)
		{
			PoolItem<TItem> poolItem = obj as PoolItem<TItem>;
			if (poolItem != null && poolItem.ID == this.ID)
			{
				TItem titem = poolItem.Item;
				if (titem.Equals(this.Item) && poolItem.LastUsedTime == this.LastUsedTime)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E84 File Offset: 0x00001084
		public override int GetHashCode()
		{
			int hashCode = this.id.GetHashCode();
			TItem titem = this.item;
			return hashCode | titem.GetHashCode() | this.lastUsedTime.GetHashCode();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002EC8 File Offset: 0x000010C8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ID:{0};LastUsedTime:{1};Item:{2}", new object[]
			{
				this.id,
				this.lastUsedTime,
				this.item
			});
		}

		// Token: 0x04000018 RID: 24
		private readonly TItem item;

		// Token: 0x04000019 RID: 25
		private readonly uint id;

		// Token: 0x0400001A RID: 26
		private readonly ExDateTime creationTime;

		// Token: 0x0400001B RID: 27
		private ExDateTime lastUsedTime;
	}
}
