using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002AF RID: 687
	internal abstract class CalendarFolderDataSourceBase : ICalendarDataSource
	{
		// Token: 0x06001AD9 RID: 6873 RVA: 0x0009AD3C File Offset: 0x00098F3C
		public CalendarFolderDataSourceBase(DateRange[] dateRanges, PropertyDefinition[] properties)
		{
			if (dateRanges == null || dateRanges.Length == 0)
			{
				throw new ArgumentNullException("dateRanges");
			}
			if (properties == null || properties.Length == 0)
			{
				throw new ArgumentNullException("properties");
			}
			this.propertyIndexes = new Hashtable(properties.Length);
			for (int i = 0; i < properties.Length; i++)
			{
				this.propertyIndexes[properties[i]] = i;
			}
			this.dateRanges = dateRanges;
			this.properties = properties;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0009ADB4 File Offset: 0x00098FB4
		protected void Load(CalendarFolderDataSourceBase.GetPropertiesDelegate getProperties)
		{
			ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "CalendarFolderDataSourceBase.Load");
			ExTraceGlobals.CalendarTracer.TraceDebug((long)this.GetHashCode(), "Calling XSO's GetCalendarView to do the calendar query");
			Stopwatch watch = Utilities.StartWatch();
			this.allData = getProperties(DateRange.GetMinStartTimeInRangeArray(this.dateRanges), DateRange.GetMaxEndTimeInRangeArray(this.dateRanges));
			Utilities.StopWatch(watch, "GetCalendarView");
			if (this.allData.Length == 0)
			{
				return;
			}
			this.viewData = new ArrayList(2 * this.dateRanges.Length);
			for (int i = 0; i < this.allData.Length; i++)
			{
				object obj = this.allData[i][(int)this.propertyIndexes[CalendarItemInstanceSchema.StartTime]];
				if (obj != null && !(obj is PropertyError) && obj is ExDateTime)
				{
					ExDateTime exDateTime = (ExDateTime)obj;
					obj = this.allData[i][(int)this.propertyIndexes[CalendarItemInstanceSchema.EndTime]];
					if (obj != null && !(obj is PropertyError) && obj is ExDateTime)
					{
						ExDateTime exDateTime2 = (ExDateTime)obj;
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
									this.viewData.Add(this.allData[i]);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0009AF38 File Offset: 0x00099138
		public int Count
		{
			get
			{
				if (this.viewData != null)
				{
					return this.viewData.Count;
				}
				return 0;
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0009AF50 File Offset: 0x00099150
		protected object GetPropertyValue(int itemIndex, PropertyDefinition propertyDefinition)
		{
			if (this.viewData == null)
			{
				throw new OwaInvalidOperationException("Can't call GetPropertyValue if the data source is empty");
			}
			object[] array = (object[])this.viewData[itemIndex];
			return array[(int)this.propertyIndexes[propertyDefinition]];
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x0009AF98 File Offset: 0x00099198
		protected bool TryGetPropertyValue<T>(int itemIndex, PropertyDefinition propertyDefinition, out T propertyValue)
		{
			object propertyValue2 = this.GetPropertyValue(itemIndex, propertyDefinition);
			if (propertyValue2 != null && propertyValue2 is T)
			{
				propertyValue = (T)((object)propertyValue2);
				return true;
			}
			propertyValue = default(T);
			return false;
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x0009AFD0 File Offset: 0x000991D0
		protected string GetStringPropertyValue(int itemIndex, PropertyDefinition propertyDefinition)
		{
			string text = this.GetPropertyValue(itemIndex, propertyDefinition) as string;
			if (text == null)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Error retrieving calendar item property with id={0}, defaulting to empty string");
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0009B008 File Offset: 0x00099208
		protected ExDateTime GetDateTimePropertyValue(int itemIndex, PropertyDefinition propertyDefinition)
		{
			object propertyValue = this.GetPropertyValue(itemIndex, propertyDefinition);
			if (propertyValue == null || !(propertyValue is ExDateTime))
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<PropertyDefinition>(0L, "Error retrieving calendar item property with id={0}, defaulting to dateTime.MinValue", propertyDefinition);
				return ExDateTime.MinValue;
			}
			return (ExDateTime)propertyValue;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0009B048 File Offset: 0x00099248
		protected bool GetBoolPropertyValue(int itemIndex, PropertyDefinition propertyDefinition)
		{
			object propertyValue = this.GetPropertyValue(itemIndex, propertyDefinition);
			if (propertyValue == null || !(propertyValue is bool))
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<PropertyDefinition>(0L, "Error retrieving calendar item property with id={0}, defaulting to false", propertyDefinition);
				return false;
			}
			return (bool)propertyValue;
		}

		// Token: 0x06001AE1 RID: 6881
		public abstract OwaStoreObjectId GetItemId(int index);

		// Token: 0x06001AE2 RID: 6882
		public abstract string GetChangeKey(int index);

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0009B083 File Offset: 0x00099283
		public ExDateTime GetStartTime(int index)
		{
			return this.GetDateTimePropertyValue(index, CalendarItemInstanceSchema.StartTime);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0009B091 File Offset: 0x00099291
		public ExDateTime GetEndTime(int index)
		{
			return this.GetDateTimePropertyValue(index, CalendarItemInstanceSchema.EndTime);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0009B09F File Offset: 0x0009929F
		public string GetSubject(int index)
		{
			return this.GetStringPropertyValue(index, ItemSchema.Subject);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0009B0AD File Offset: 0x000992AD
		public string GetLocation(int index)
		{
			return this.GetStringPropertyValue(index, CalendarItemBaseSchema.Location);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0009B0BC File Offset: 0x000992BC
		public bool IsMeeting(int index)
		{
			int valueToTest;
			return this.TryGetPropertyValue<int>(index, CalendarItemBaseSchema.AppointmentState, out valueToTest) && Utilities.IsFlagSet(valueToTest, 1);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0009B0E4 File Offset: 0x000992E4
		public bool IsCancelled(int index)
		{
			int valueToTest;
			return this.TryGetPropertyValue<int>(index, CalendarItemBaseSchema.AppointmentState, out valueToTest) && Utilities.IsFlagSet(valueToTest, 4);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0009B10A File Offset: 0x0009930A
		public virtual bool HasAttachment(int index)
		{
			return this.GetBoolPropertyValue(index, ItemSchema.HasAttachment);
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0009B118 File Offset: 0x00099318
		public virtual bool IsPrivate(int index)
		{
			return this.IsPrivate(index, new bool?(false)).Value;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0009B13C File Offset: 0x0009933C
		public bool? IsPrivate(int index, bool? defaultValue)
		{
			Sensitivity sensitivity;
			if (this.TryGetPropertyValue<Sensitivity>(index, ItemSchema.Sensitivity, out sensitivity))
			{
				return new bool?(sensitivity == Sensitivity.Private);
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Error retrieving calendar item sensitivity, defaulting to normal");
			return defaultValue;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0009B178 File Offset: 0x00099378
		public CalendarItemTypeWrapper GetWrappedItemType(int index)
		{
			CalendarItemType calendarItemType;
			if (!this.TryGetPropertyValue<CalendarItemType>(index, CalendarItemBaseSchema.CalendarItemType, out calendarItemType))
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Error retrieving calendar item type, defaulting to single");
				return CalendarItemTypeWrapper.Single;
			}
			switch (calendarItemType)
			{
			case CalendarItemType.Single:
				return CalendarItemTypeWrapper.Single;
			case CalendarItemType.Occurrence:
				return CalendarItemTypeWrapper.Occurrence;
			case CalendarItemType.Exception:
				return CalendarItemTypeWrapper.Exception;
			default:
				return CalendarItemTypeWrapper.Single;
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0009B1C5 File Offset: 0x000993C5
		public virtual string GetOrganizerDisplayName(int index)
		{
			return this.GetStringPropertyValue(index, CalendarItemBaseSchema.OrganizerDisplayName);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0009B1D4 File Offset: 0x000993D4
		public BusyTypeWrapper GetWrappedBusyType(int index)
		{
			int num;
			if (!this.TryGetPropertyValue<int>(index, CalendarItemBaseSchema.FreeBusyStatus, out num))
			{
				return BusyTypeWrapper.Free;
			}
			switch (num)
			{
			case 0:
				return BusyTypeWrapper.Free;
			case 1:
				return BusyTypeWrapper.Tentative;
			case 2:
				return BusyTypeWrapper.Busy;
			case 3:
				return BusyTypeWrapper.OOF;
			default:
				return BusyTypeWrapper.Unknown;
			}
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x0009B216 File Offset: 0x00099416
		public object GetBusyType(int index)
		{
			return this.GetPropertyValue(index, CalendarItemBaseSchema.FreeBusyStatus);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0009B224 File Offset: 0x00099424
		public bool IsOrganizer(int index)
		{
			return this.GetBoolPropertyValue(index, CalendarItemBaseSchema.IsOrganizer);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0009B232 File Offset: 0x00099432
		public string[] GetCategories(int index)
		{
			return this.GetPropertyValue(index, ItemSchema.Categories) as string[];
		}

		// Token: 0x06001AF2 RID: 6898
		public abstract string GetCssClassName(int index);

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0009B248 File Offset: 0x00099448
		public string GetInviteesDisplayNames(int index)
		{
			string stringPropertyValue = this.GetStringPropertyValue(index, CalendarItemBaseSchema.DisplayAttendeesTo);
			string stringPropertyValue2 = this.GetStringPropertyValue(index, CalendarItemBaseSchema.DisplayAttendeesCc);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(stringPropertyValue);
			if (stringPropertyValue != string.Empty && stringPropertyValue2 != string.Empty)
			{
				stringBuilder.Append("; ");
			}
			stringBuilder.Append(stringPropertyValue2);
			return stringBuilder.ToString();
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001AF4 RID: 6900
		public abstract SharedType SharedType { get; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001AF5 RID: 6901
		public abstract WorkingHours WorkingHours { get; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001AF6 RID: 6902
		public abstract bool UserCanReadItem { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001AF7 RID: 6903
		public abstract bool UserCanCreateItem { get; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001AF8 RID: 6904
		public abstract string FolderClassName { get; }

		// Token: 0x04001316 RID: 4886
		public const string DefaultClass = "noClrCal";

		// Token: 0x04001317 RID: 4887
		protected DateRange[] dateRanges;

		// Token: 0x04001318 RID: 4888
		protected PropertyDefinition[] properties;

		// Token: 0x04001319 RID: 4889
		protected Hashtable propertyIndexes;

		// Token: 0x0400131A RID: 4890
		protected ArrayList viewData;

		// Token: 0x0400131B RID: 4891
		protected object[][] allData;

		// Token: 0x020002B0 RID: 688
		// (Invoke) Token: 0x06001AFA RID: 6906
		protected delegate object[][] GetPropertiesDelegate(ExDateTime start, ExDateTime end);
	}
}
