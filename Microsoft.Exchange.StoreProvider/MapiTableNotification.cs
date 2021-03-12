using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x020001F3 RID: 499
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MapiTableNotification : MapiNotification
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00022500 File Offset: 0x00020700
		public TableEvent TableEvent
		{
			get
			{
				return this.tableEvent;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00022508 File Offset: 0x00020708
		public int HResult
		{
			get
			{
				return this.hResult;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00022510 File Offset: 0x00020710
		public PropValue Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00022518 File Offset: 0x00020718
		public PropValue Prior
		{
			get
			{
				return this.prior;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00022520 File Offset: 0x00020720
		public PropValue[] Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00022528 File Offset: 0x00020728
		internal unsafe MapiTableNotification(NOTIFICATION* notification) : base(notification)
		{
			this.tableEvent = (TableEvent)notification->info.tab.ulTableEvent;
			this.hResult = notification->info.tab.hResult;
			if (this.tableEvent == TableEvent.TableRowAdded || this.tableEvent == TableEvent.TableRowDeleted || this.tableEvent == TableEvent.TableRowDeletedExtended || this.tableEvent == TableEvent.TableRowModified)
			{
				this.index = new PropValue(&notification->info.tab.propIndex);
			}
			else
			{
				this.index = new PropValue(PropTag.Null, null);
			}
			if (this.tableEvent == TableEvent.TableRowAdded || this.tableEvent == TableEvent.TableRowModified)
			{
				this.prior = new PropValue(&notification->info.tab.propPrior);
				this.row = Array<PropValue>.New(notification->info.tab.row.cValues);
				SPropValue* ptr = (SPropValue*)notification->info.tab.row.lpProps.ToPointer();
				for (int i = 0; i < this.row.Length; i++)
				{
					this.row[i] = new PropValue(ptr + i);
				}
				return;
			}
			if (this.tableEvent == TableEvent.TableRowDeletedExtended)
			{
				this.row = Array<PropValue>.New(notification->info.tab.row.cValues);
				SPropValue* ptr2 = (SPropValue*)notification->info.tab.row.lpProps.ToPointer();
				for (int j = 0; j < this.row.Length; j++)
				{
					this.row[j] = new PropValue(ptr2 + j);
				}
				return;
			}
			this.prior = new PropValue(PropTag.Null, null);
			this.row = Array<PropValue>.Empty;
		}

		// Token: 0x040006CD RID: 1741
		private readonly TableEvent tableEvent;

		// Token: 0x040006CE RID: 1742
		private readonly int hResult;

		// Token: 0x040006CF RID: 1743
		private readonly PropValue index;

		// Token: 0x040006D0 RID: 1744
		private readonly PropValue prior;

		// Token: 0x040006D1 RID: 1745
		private readonly PropValue[] row;
	}
}
