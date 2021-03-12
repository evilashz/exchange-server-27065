using System;
using System.Collections;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x0200002F RID: 47
	internal abstract class CalendarViewBase
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00009C56 File Offset: 0x00007E56
		protected CalendarViewBase(ISessionContext sessionContext, CalendarAdapterBase calendarAdapter)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			this.sessionContext = sessionContext;
			this.CalendarAdapter = calendarAdapter;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00009C7A File Offset: 0x00007E7A
		public ISessionContext SessionContext
		{
			get
			{
				return this.sessionContext;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009C82 File Offset: 0x00007E82
		public ICalendarDataSource DataSource
		{
			get
			{
				if (this.CalendarAdapter != null)
				{
					return this.CalendarAdapter.DataSource;
				}
				return null;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00009C99 File Offset: 0x00007E99
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00009CA1 File Offset: 0x00007EA1
		public CalendarAdapterBase CalendarAdapter { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009CAA File Offset: 0x00007EAA
		public DateRange[] DateRanges
		{
			get
			{
				if (this.CalendarAdapter != null)
				{
					return this.CalendarAdapter.DateRanges;
				}
				return null;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009CC1 File Offset: 0x00007EC1
		public int DayCount
		{
			get
			{
				return this.DateRanges.Length;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000131 RID: 305
		public abstract int MaxEventAreaRows { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000132 RID: 306
		public abstract int MaxItemsPerView { get; }

		// Token: 0x06000133 RID: 307 RVA: 0x00009CCB File Offset: 0x00007ECB
		public void RemoveItemFromView(int dataIndex)
		{
			if (this.removedItems == null)
			{
				this.removedItems = new Hashtable();
			}
			this.removedItems[dataIndex] = true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00009CF8 File Offset: 0x00007EF8
		public bool IsItemRemoved(int dataIndex)
		{
			if (this.removedItems == null)
			{
				return false;
			}
			object obj = this.removedItems[dataIndex];
			return null != obj;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00009D28 File Offset: 0x00007F28
		public int RemovedItemCount
		{
			get
			{
				if (this.removedItems == null)
				{
					return 0;
				}
				return this.removedItems.Count;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009D3F File Offset: 0x00007F3F
		protected Hashtable RemovedItems
		{
			get
			{
				return this.removedItems;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000137 RID: 311
		public abstract SanitizedHtmlString FolderDateDescription { get; }

		// Token: 0x040000C4 RID: 196
		private ISessionContext sessionContext;

		// Token: 0x040000C5 RID: 197
		private Hashtable removedItems;
	}
}
