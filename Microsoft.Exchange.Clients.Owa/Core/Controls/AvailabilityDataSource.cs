using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x0200031A RID: 794
	internal sealed class AvailabilityDataSource : ICalendarDataSource
	{
		// Token: 0x06001E23 RID: 7715 RVA: 0x000AEC34 File Offset: 0x000ACE34
		public AvailabilityDataSource(UserContext userContext, string smtpAddress, StoreObjectId folderStoreId, DateRange[] dateRanges, bool pendingLoad)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (smtpAddress == null)
			{
				throw new ArgumentNullException("smtpAddress");
			}
			if (dateRanges == null)
			{
				throw new ArgumentNullException("dateRanges");
			}
			if (dateRanges.Length == 0)
			{
				throw new ArgumentException("Length of dateRanges cannot be 0");
			}
			this.userContext = userContext;
			this.dateRanges = dateRanges;
			if (!pendingLoad)
			{
				FreeBusyQueryResult[] array = AvailabilityDataSource.BatchLoadData(userContext, new string[]
				{
					smtpAddress
				}, new StoreObjectId[]
				{
					folderStoreId
				}, dateRanges);
				if (array != null)
				{
					this.LoadFromQueryResult(array[0]);
				}
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x000AECC4 File Offset: 0x000ACEC4
		public static FreeBusyQueryResult[] BatchLoadData(UserContext userContext, CalendarAdapter[] adapters, DateRange[] dateRanges)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (adapters == null)
			{
				throw new ArgumentNullException("adapters");
			}
			if (adapters.Length == 0)
			{
				throw new ArgumentException("Length of adapters cannot be 0");
			}
			if (dateRanges == null)
			{
				throw new ArgumentNullException("dateRanges");
			}
			if (dateRanges.Length == 0)
			{
				throw new ArgumentException("Length of dateRanges cannot be 0");
			}
			string[] array = new string[adapters.Length];
			StoreObjectId[] array2 = new StoreObjectId[adapters.Length];
			for (int i = 0; i < adapters.Length; i++)
			{
				array[i] = adapters[i].SmtpAddress;
				array2[i] = (adapters[i].IsGSCalendar ? null : adapters[i].FolderId.StoreObjectId);
			}
			return AvailabilityDataSource.BatchLoadData(userContext, array, array2, dateRanges);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x000AED6C File Offset: 0x000ACF6C
		public static FreeBusyQueryResult[] BatchLoadData(UserContext userContext, string[] smtpAddresses, StoreObjectId[] folderStoreIds, DateRange[] dateRanges)
		{
			if (smtpAddresses == null || smtpAddresses.Length == 0)
			{
				throw new ArgumentNullException("smtpAddresses");
			}
			if (dateRanges == null || dateRanges.Length == 0)
			{
				throw new ArgumentNullException("dateRanges");
			}
			ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "GSCalendarDataSource.Load");
			AvailabilityQuery availabilityQuery = new AvailabilityQuery();
			availabilityQuery.MailboxArray = new MailboxData[smtpAddresses.Length];
			availabilityQuery.ClientContext = ClientContext.Create(userContext.LogonIdentity.ClientSecurityContext, userContext.ExchangePrincipal.MailboxInfo.OrganizationId, OwaContext.TryGetCurrentBudget(), userContext.TimeZone, userContext.UserCulture, AvailabilityQuery.CreateNewMessageId());
			for (int i = 0; i < smtpAddresses.Length; i++)
			{
				availabilityQuery.MailboxArray[i] = new MailboxData();
				availabilityQuery.MailboxArray[i].Email = new EmailAddress();
				availabilityQuery.MailboxArray[i].Email.Address = smtpAddresses[i];
				availabilityQuery.MailboxArray[i].AssociatedFolderId = folderStoreIds[i];
			}
			availabilityQuery.DesiredFreeBusyView = new FreeBusyViewOptions
			{
				TimeWindow = new Duration(),
				TimeWindow = 
				{
					StartTime = (DateTime)DateRange.GetMinStartTimeInRangeArray(dateRanges),
					EndTime = (DateTime)DateRange.GetMaxEndTimeInRangeArray(dateRanges)
				},
				MergedFreeBusyIntervalInMinutes = userContext.UserOptions.HourIncrement,
				RequestedView = FreeBusyViewType.Detailed
			};
			AvailabilityQueryResult availabilityQueryResult = Utilities.ExecuteAvailabilityQuery(availabilityQuery);
			if (availabilityQueryResult == null)
			{
				return null;
			}
			return availabilityQueryResult.FreeBusyResults;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x000AEEC4 File Offset: 0x000AD0C4
		public void LoadFromQueryResult(FreeBusyQueryResult queryResult)
		{
			if (queryResult == null)
			{
				throw new ArgumentNullException("queryResult");
			}
			this.AssociatedCalendarType = AvailabilityDataSource.CalendarType.Unknown;
			if (queryResult.ExceptionInfo != null)
			{
				if (queryResult.ExceptionInfo is NotDefaultCalendarException)
				{
					this.AssociatedCalendarType = AvailabilityDataSource.CalendarType.Secondary;
				}
				this.userCanReadItem = false;
				return;
			}
			this.AssociatedCalendarType = AvailabilityDataSource.CalendarType.Primary;
			this.userCanReadItem = true;
			this.workingHours = WorkingHours.CreateFromAvailabilityWorkingHours(this.userContext, queryResult.WorkingHours);
			if (queryResult.CalendarEventArray == null && queryResult.MergedFreeBusy != null)
			{
				this.items = this.GetItemsFromMergedFreeBusy(queryResult.MergedFreeBusy);
				return;
			}
			if (queryResult.CalendarEventArray != null && queryResult.CalendarEventArray.Length > 0)
			{
				this.items = this.GetItemsFromEventArray(queryResult.CalendarEventArray);
			}
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x000AEF7C File Offset: 0x000AD17C
		private GSCalendarItem[] GetItemsFromEventArray(CalendarEvent[] eventArray)
		{
			List<GSCalendarItem> list = new List<GSCalendarItem>(eventArray.Length);
			foreach (CalendarEvent calendarEvent in eventArray)
			{
				ExDateTime exDateTime = new ExDateTime(this.userContext.TimeZone, calendarEvent.StartTime);
				ExDateTime exDateTime2 = new ExDateTime(this.userContext.TimeZone, calendarEvent.EndTime);
				if (exDateTime > exDateTime2)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Skipping appointment with an end time earlier than a start time");
				}
				else
				{
					for (int j = 0; j < this.dateRanges.Length; j++)
					{
						if (this.dateRanges[j].Intersects(exDateTime, exDateTime2))
						{
							GSCalendarItem gscalendarItem = new GSCalendarItem();
							gscalendarItem.StartTime = exDateTime;
							gscalendarItem.EndTime = exDateTime2;
							gscalendarItem.BusyType = this.ConvertBusyType(calendarEvent.BusyType);
							if (calendarEvent.CalendarEventDetails != null)
							{
								CalendarEventDetails calendarEventDetails = calendarEvent.CalendarEventDetails;
								gscalendarItem.Subject = calendarEventDetails.Subject;
								gscalendarItem.Location = calendarEventDetails.Location;
								gscalendarItem.IsMeeting = calendarEventDetails.IsMeeting;
								gscalendarItem.IsPrivate = calendarEventDetails.IsPrivate;
								if (calendarEventDetails.IsException)
								{
									gscalendarItem.CalendarItemType = CalendarItemTypeWrapper.Exception;
								}
								else if (calendarEventDetails.IsRecurring)
								{
									gscalendarItem.CalendarItemType = CalendarItemTypeWrapper.Occurrence;
								}
								else
								{
									gscalendarItem.CalendarItemType = CalendarItemTypeWrapper.Single;
								}
							}
							else
							{
								gscalendarItem.CalendarItemType = CalendarItemTypeWrapper.Single;
							}
							list.Add(gscalendarItem);
							break;
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000AF0F0 File Offset: 0x000AD2F0
		private GSCalendarItem[] GetItemsFromMergedFreeBusy(string mergedFreeBusy)
		{
			List<GSCalendarItem> list = new List<GSCalendarItem>(mergedFreeBusy.Length);
			GSCalendarItem gscalendarItem = null;
			BusyTypeWrapper busyTypeWrapper = BusyTypeWrapper.Free;
			int i;
			for (i = 0; i < mergedFreeBusy.Length; i++)
			{
				int num;
				if (!int.TryParse(mergedFreeBusy[i].ToString(), out num) || num < 0 || num > 4)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Availability Service returns invalid data in MergedFreeBusy:" + mergedFreeBusy[i]);
				}
				else
				{
					BusyTypeWrapper busyTypeWrapper2 = (BusyTypeWrapper)num;
					if (busyTypeWrapper2 != busyTypeWrapper)
					{
						busyTypeWrapper = busyTypeWrapper2;
						if (gscalendarItem != null)
						{
							this.CheckAndAddCurrentItem(list, ref gscalendarItem, i);
						}
						if (busyTypeWrapper2 != BusyTypeWrapper.Free)
						{
							gscalendarItem = new GSCalendarItem
							{
								StartTime = this.GetDateTimeFromIndex(i),
								BusyType = busyTypeWrapper2,
								CalendarItemType = CalendarItemTypeWrapper.Single
							};
						}
					}
				}
			}
			if (gscalendarItem != null)
			{
				this.CheckAndAddCurrentItem(list, ref gscalendarItem, i);
			}
			return list.ToArray();
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000AF1C4 File Offset: 0x000AD3C4
		private void CheckAndAddCurrentItem(IList<GSCalendarItem> itemList, ref GSCalendarItem currentItem, int i)
		{
			currentItem.EndTime = this.GetDateTimeFromIndex(i);
			for (int j = 0; j < this.dateRanges.Length; j++)
			{
				if (this.dateRanges[j].Intersects(currentItem.StartTime, currentItem.EndTime))
				{
					itemList.Add(currentItem);
					break;
				}
			}
			currentItem = null;
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x000AF21C File Offset: 0x000AD41C
		private ExDateTime GetDateTimeFromIndex(int i)
		{
			return this.dateRanges[0].Start.AddMinutes((double)(i * this.userContext.UserOptions.HourIncrement));
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x000AF254 File Offset: 0x000AD454
		private BusyTypeWrapper ConvertBusyType(Microsoft.Exchange.InfoWorker.Common.Availability.BusyType busyType)
		{
			switch (busyType)
			{
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Free:
				return BusyTypeWrapper.Free;
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Tentative:
				return BusyTypeWrapper.Tentative;
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Busy:
				return BusyTypeWrapper.Busy;
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.OOF:
				return BusyTypeWrapper.OOF;
			default:
				return BusyTypeWrapper.Unknown;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x000AF284 File Offset: 0x000AD484
		public int Count
		{
			get
			{
				if (this.items != null)
				{
					return this.items.Length;
				}
				return 0;
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000AF298 File Offset: 0x000AD498
		public OwaStoreObjectId GetItemId(int index)
		{
			return null;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000AF29B File Offset: 0x000AD49B
		public string GetChangeKey(int index)
		{
			return null;
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000AF29E File Offset: 0x000AD49E
		public ExDateTime GetStartTime(int index)
		{
			return this.items[index].StartTime;
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000AF2AD File Offset: 0x000AD4AD
		public ExDateTime GetEndTime(int index)
		{
			return this.items[index].EndTime;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000AF2BC File Offset: 0x000AD4BC
		public string GetSubject(int index)
		{
			return this.items[index].Subject ?? this.GetSubjectOfFreeBusyOnlyItem(index);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x000AF2D6 File Offset: 0x000AD4D6
		public string GetLocation(int index)
		{
			return this.items[index].Location ?? string.Empty;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x000AF2EE File Offset: 0x000AD4EE
		public bool IsMeeting(int index)
		{
			return this.items[index].IsMeeting;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x000AF2FD File Offset: 0x000AD4FD
		public bool IsCancelled(int index)
		{
			return false;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000AF300 File Offset: 0x000AD500
		public bool HasAttachment(int index)
		{
			return false;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000AF303 File Offset: 0x000AD503
		public bool IsPrivate(int index)
		{
			return this.items[index].IsPrivate;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x000AF312 File Offset: 0x000AD512
		public CalendarItemTypeWrapper GetWrappedItemType(int index)
		{
			return this.items[index].CalendarItemType;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x000AF321 File Offset: 0x000AD521
		public string GetOrganizerDisplayName(int index)
		{
			return string.Empty;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000AF328 File Offset: 0x000AD528
		public BusyTypeWrapper GetWrappedBusyType(int index)
		{
			return this.items[index].BusyType;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x000AF337 File Offset: 0x000AD537
		public bool IsOrganizer(int index)
		{
			return false;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000AF33A File Offset: 0x000AD53A
		public string[] GetCategories(int index)
		{
			return null;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000AF33D File Offset: 0x000AD53D
		public string GetCssClassName(int index)
		{
			return "noClrCal";
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000AF344 File Offset: 0x000AD544
		public string GetInviteesDisplayNames(int index)
		{
			return null;
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000AF348 File Offset: 0x000AD548
		private string GetSubjectOfFreeBusyOnlyItem(int index)
		{
			switch (this.items[index].BusyType)
			{
			case BusyTypeWrapper.Free:
				return LocalizedStrings.GetNonEncoded(-971703552);
			case BusyTypeWrapper.Tentative:
				return LocalizedStrings.GetNonEncoded(1797669216);
			case BusyTypeWrapper.Busy:
				return LocalizedStrings.GetNonEncoded(2052801377);
			case BusyTypeWrapper.OOF:
				return LocalizedStrings.GetNonEncoded(2047193656);
			default:
				return LocalizedStrings.GetNonEncoded(-1280331347);
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x000AF3B1 File Offset: 0x000AD5B1
		public SharedType SharedType
		{
			get
			{
				return SharedType.InternalFreeBusy;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x000AF3B4 File Offset: 0x000AD5B4
		public WorkingHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x000AF3BC File Offset: 0x000AD5BC
		public bool UserCanReadItem
		{
			get
			{
				return this.userCanReadItem;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000AF3C4 File Offset: 0x000AD5C4
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x000AF3CC File Offset: 0x000AD5CC
		public AvailabilityDataSource.CalendarType AssociatedCalendarType { get; private set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x000AF3D5 File Offset: 0x000AD5D5
		public bool UserCanCreateItem
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x000AF3D8 File Offset: 0x000AD5D8
		public string FolderClassName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0400165C RID: 5724
		private DateRange[] dateRanges;

		// Token: 0x0400165D RID: 5725
		private UserContext userContext;

		// Token: 0x0400165E RID: 5726
		private GSCalendarItem[] items;

		// Token: 0x0400165F RID: 5727
		private bool userCanReadItem;

		// Token: 0x04001660 RID: 5728
		private WorkingHours workingHours;

		// Token: 0x0200031B RID: 795
		public enum CalendarType
		{
			// Token: 0x04001663 RID: 5731
			Unknown,
			// Token: 0x04001664 RID: 5732
			Primary,
			// Token: 0x04001665 RID: 5733
			Secondary
		}
	}
}
