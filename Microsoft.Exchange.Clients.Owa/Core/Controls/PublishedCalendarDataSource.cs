using System;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002CE RID: 718
	internal sealed class PublishedCalendarDataSource : CalendarFolderDataSourceBase
	{
		// Token: 0x06001BE0 RID: 7136 RVA: 0x000A0B1C File Offset: 0x0009ED1C
		public PublishedCalendarDataSource(AnonymousSessionContext sessionContext, PublishedCalendar folder, DateRange[] dateRanges, PropertyDefinition[] properties) : base(dateRanges, properties)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.sessionContext = sessionContext;
			this.folder = folder;
			base.Load((ExDateTime start, ExDateTime end) => folder.GetCalendarView(start, end, properties));
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000A0B98 File Offset: 0x0009ED98
		public override OwaStoreObjectId GetItemId(int index)
		{
			return null;
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000A0B9C File Offset: 0x0009ED9C
		public StoreObjectId GetItemStoreObjectId(int index)
		{
			VersionedId versionedId;
			if (base.TryGetPropertyValue<VersionedId>(index, ItemSchema.Id, out versionedId))
			{
				return versionedId.ObjectId;
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Couldn't get id from the item, skipping...");
			return null;
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000A0BD2 File Offset: 0x0009EDD2
		public override string GetChangeKey(int index)
		{
			return string.Empty;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000A0BD9 File Offset: 0x0009EDD9
		public override bool IsPrivate(int index)
		{
			return this.DetailLevel != DetailLevelEnumType.AvailabilityOnly && base.IsPrivate(index);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000A0BED File Offset: 0x0009EDED
		public override string GetOrganizerDisplayName(int index)
		{
			return string.Empty;
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x000A0BF4 File Offset: 0x0009EDF4
		public override string GetCssClassName(int index)
		{
			return "noClrCal";
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x000A0BFB File Offset: 0x0009EDFB
		public override bool HasAttachment(int index)
		{
			return false;
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x000A0C00 File Offset: 0x0009EE00
		public PublishedCalendarItemData? GetItem(int index)
		{
			if (this.IsPrivate(index))
			{
				return null;
			}
			return new PublishedCalendarItemData?(this.folder.GetItemData(this.GetItemStoreObjectId(index)));
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x000A0C37 File Offset: 0x0009EE37
		public override SharedType SharedType
		{
			get
			{
				return SharedType.AnonymousAccess;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x000A0C3A File Offset: 0x0009EE3A
		public override WorkingHours WorkingHours
		{
			get
			{
				return this.sessionContext.WorkingHours;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x000A0C47 File Offset: 0x0009EE47
		public override bool UserCanReadItem
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x000A0C4A File Offset: 0x0009EE4A
		public override bool UserCanCreateItem
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x000A0C4D File Offset: 0x0009EE4D
		public override string FolderClassName
		{
			get
			{
				return "IPF.Appointment";
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000A0C54 File Offset: 0x0009EE54
		public DetailLevelEnumType DetailLevel
		{
			get
			{
				return this.folder.DetailLevel;
			}
		}

		// Token: 0x040014A6 RID: 5286
		private AnonymousSessionContext sessionContext;

		// Token: 0x040014A7 RID: 5287
		private PublishedCalendar folder;
	}
}
