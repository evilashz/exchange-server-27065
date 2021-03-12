using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000324 RID: 804
	internal sealed class CalendarAdapterCollection : DisposeTrackableBase
	{
		// Token: 0x06001E81 RID: 7809 RVA: 0x000AFE40 File Offset: 0x000AE040
		internal CalendarAdapterCollection(UserContext userContext, OwaStoreObjectId[] folderIds, CalendarViewType viewType)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderIds == null)
			{
				throw new ArgumentNullException("folderIds");
			}
			if (folderIds.Length == 0)
			{
				throw new ArgumentException("Length of folderIds cannot be 0");
			}
			this.userContext = userContext;
			this.folderIds = folderIds;
			this.PropertyFolderId = (folderIds[0].IsPublic ? folderIds[0] : userContext.CalendarFolderOwaId);
			this.propertyFolder = Utilities.GetFolderForContent<CalendarFolder>(userContext, this.PropertyFolderId, CalendarUtilities.FolderViewProperties);
			this.folderViewStates = userContext.GetFolderViewStates(this.propertyFolder);
			int viewWidth;
			ReadingPanePosition readingPanePosition;
			CalendarUtilities.GetCalendarViewParamsFromViewStates(this.folderViewStates, out viewWidth, ref viewType, out readingPanePosition);
			this.ViewWidth = viewWidth;
			this.ViewType = viewType;
			this.ReadingPanePosition = readingPanePosition;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000AFEF8 File Offset: 0x000AE0F8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.propertyFolder != null)
				{
					this.propertyFolder.Dispose();
					this.propertyFolder = null;
				}
				if (this.adapters != null)
				{
					foreach (CalendarAdapter calendarAdapter in this.adapters)
					{
						calendarAdapter.Dispose();
					}
					this.adapters = null;
				}
			}
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000AFF50 File Offset: 0x000AE150
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarAdapterCollection>(this);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000AFF58 File Offset: 0x000AE158
		internal void SaveViewStates(ExDateTime[] days)
		{
			this.folderViewStates.CalendarViewType = this.ViewType;
			this.folderViewStates.DailyViewDays = ((days != null && this.ViewType == CalendarViewType.Min) ? days.Length : 1);
			this.folderViewStates.Save();
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000AFF94 File Offset: 0x000AE194
		internal CalendarAdapter[] GetAdapters(ExDateTime[] days, bool addOwaConditionAdvisor)
		{
			if (days == null)
			{
				throw new ArgumentNullException("days");
			}
			if (days.Length == 0)
			{
				throw new ArgumentException("Length of days cannot be 0.");
			}
			if (this.adapters == null)
			{
				List<CalendarAdapter> list = new List<CalendarAdapter>();
				List<CalendarAdapter> list2 = new List<CalendarAdapter>();
				foreach (OwaStoreObjectId owaStoreObjectId in this.folderIds)
				{
					CalendarAdapter calendarAdapter;
					if (owaStoreObjectId.Equals(this.PropertyFolderId))
					{
						calendarAdapter = new CalendarAdapter(this.userContext, this.propertyFolder);
					}
					else
					{
						calendarAdapter = new CalendarAdapter(this.userContext, owaStoreObjectId);
					}
					try
					{
						calendarAdapter.LoadData(CalendarUtilities.QueryProperties, days, addOwaConditionAdvisor, false);
					}
					catch (Exception)
					{
						calendarAdapter.Dispose();
						calendarAdapter = null;
						list.AddRange(list2);
						foreach (CalendarAdapter calendarAdapter2 in list)
						{
							if (calendarAdapter2 != null)
							{
								calendarAdapter2.Dispose();
							}
						}
						throw;
					}
					if (calendarAdapter.DataSource is AvailabilityDataSource)
					{
						list2.Add(calendarAdapter);
					}
					else
					{
						list.Add(calendarAdapter);
					}
				}
				if (list2.Count > 0)
				{
					CalendarAdapter[] array2 = list2.ToArray();
					FreeBusyQueryResult[] array3 = AvailabilityDataSource.BatchLoadData(this.userContext, array2, CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(days));
					if (array3 != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							AvailabilityDataSource availabilityDataSource = (AvailabilityDataSource)array2[j].DataSource;
							availabilityDataSource.LoadFromQueryResult(array3[j]);
						}
					}
					list.AddRange(list2);
				}
				this.adapters = list.ToArray();
			}
			foreach (CalendarAdapter calendarAdapter3 in this.adapters)
			{
				calendarAdapter3.SaveCalendarTypeFromOlderExchangeAsNeeded();
			}
			return this.adapters;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x000B015C File Offset: 0x000AE35C
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x000B0164 File Offset: 0x000AE364
		internal int ViewWidth { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x000B016D File Offset: 0x000AE36D
		// (set) Token: 0x06001E89 RID: 7817 RVA: 0x000B0175 File Offset: 0x000AE375
		internal CalendarViewType ViewType { get; set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000B017E File Offset: 0x000AE37E
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x000B0186 File Offset: 0x000AE386
		internal ReadingPanePosition ReadingPanePosition { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000B018F File Offset: 0x000AE38F
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x000B0197 File Offset: 0x000AE397
		internal OwaStoreObjectId PropertyFolderId { get; private set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x000B01A0 File Offset: 0x000AE3A0
		internal FolderViewStates FolderViewStates
		{
			get
			{
				return this.folderViewStates;
			}
		}

		// Token: 0x0400167A RID: 5754
		private UserContext userContext;

		// Token: 0x0400167B RID: 5755
		private OwaStoreObjectId[] folderIds;

		// Token: 0x0400167C RID: 5756
		private CalendarFolder propertyFolder;

		// Token: 0x0400167D RID: 5757
		private CalendarAdapter[] adapters;

		// Token: 0x0400167E RID: 5758
		private FolderViewStates folderViewStates;
	}
}
