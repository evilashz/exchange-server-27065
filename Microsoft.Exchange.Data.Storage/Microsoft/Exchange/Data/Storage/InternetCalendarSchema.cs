using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D98 RID: 3480
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class InternetCalendarSchema
	{
		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x060077C2 RID: 30658 RVA: 0x00210C74 File Offset: 0x0020EE74
		internal static PropertyDefinition[] ExportFreeBusy
		{
			get
			{
				if (InternetCalendarSchema.exportFreeBusy == null)
				{
					lock (InternetCalendarSchema.exportFreeBusyLock)
					{
						if (InternetCalendarSchema.exportFreeBusy == null)
						{
							InternetCalendarSchema.exportFreeBusy = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.ExportQueryOnly,
								InternetCalendarSchema.FreeBusyOnly
							});
						}
					}
				}
				return InternetCalendarSchema.exportFreeBusy;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x060077C3 RID: 30659 RVA: 0x00210CE4 File Offset: 0x0020EEE4
		internal static PropertyDefinition[] ExportLimitedDetails
		{
			get
			{
				if (InternetCalendarSchema.exportLimitedDetails == null)
				{
					lock (InternetCalendarSchema.exportLimitedDetailsLock)
					{
						if (InternetCalendarSchema.exportLimitedDetails == null)
						{
							InternetCalendarSchema.exportLimitedDetails = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.ExportFreeBusy,
								InternetCalendarSchema.LimitedDetailOnly
							});
						}
					}
				}
				return InternetCalendarSchema.exportLimitedDetails;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x060077C4 RID: 30660 RVA: 0x00210D54 File Offset: 0x0020EF54
		internal static PropertyDefinition[] ExportFullDetails
		{
			get
			{
				if (InternetCalendarSchema.exportFullDetails == null)
				{
					lock (InternetCalendarSchema.exportFullDetailsLock)
					{
						if (InternetCalendarSchema.exportFullDetails == null)
						{
							InternetCalendarSchema.exportFullDetails = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.ExportLimitedDetails,
								InternetCalendarSchema.FullDetailOnly
							});
						}
					}
				}
				return InternetCalendarSchema.exportFullDetails;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x060077C5 RID: 30661 RVA: 0x00210DC4 File Offset: 0x0020EFC4
		internal static PropertyDefinition[] ImportCompare
		{
			get
			{
				if (InternetCalendarSchema.importCompare == null)
				{
					lock (InternetCalendarSchema.importCompareLock)
					{
						if (InternetCalendarSchema.importCompare == null)
						{
							InternetCalendarSchema.importCompare = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.FreeBusyOnly,
								InternetCalendarSchema.LimitedDetailOnly,
								InternetCalendarSchema.ImportCompareOnly
							});
						}
					}
				}
				return InternetCalendarSchema.importCompare;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x060077C6 RID: 30662 RVA: 0x00210E3C File Offset: 0x0020F03C
		internal static PropertyDefinition[] ImportUpdate
		{
			get
			{
				if (InternetCalendarSchema.importUpdate == null)
				{
					lock (InternetCalendarSchema.importUpdateLock)
					{
						if (InternetCalendarSchema.importUpdate == null)
						{
							InternetCalendarSchema.importUpdate = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.ImportCompare,
								InternetCalendarSchema.ImportUpdateOnly
							});
						}
					}
				}
				return InternetCalendarSchema.importUpdate;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x060077C7 RID: 30663 RVA: 0x00210EAC File Offset: 0x0020F0AC
		internal static PropertyDefinition[] ImportQuery
		{
			get
			{
				if (InternetCalendarSchema.importQuery == null)
				{
					lock (InternetCalendarSchema.importQueryLock)
					{
						if (InternetCalendarSchema.importQuery == null)
						{
							InternetCalendarSchema.importQuery = Util.MergeArrays<PropertyDefinition>(new ICollection<PropertyDefinition>[]
							{
								InternetCalendarSchema.ImportCompare,
								InternetCalendarSchema.ImportQueryOnly
							});
						}
					}
				}
				return InternetCalendarSchema.importQuery;
			}
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x00210F1C File Offset: 0x0020F11C
		public static PropertyDefinition[] FromDetailLevel(DetailLevelEnumType detailLevel)
		{
			switch (detailLevel)
			{
			case DetailLevelEnumType.AvailabilityOnly:
				return InternetCalendarSchema.ExportFreeBusy;
			case DetailLevelEnumType.LimitedDetails:
				return InternetCalendarSchema.ExportLimitedDetails;
			case DetailLevelEnumType.FullDetails:
				return InternetCalendarSchema.ExportFullDetails;
			default:
				throw new ArgumentOutOfRangeException("detailLevel");
			}
		}

		// Token: 0x040052DE RID: 21214
		private static readonly PropertyDefinition[] FreeBusyOnly = new PropertyDefinition[]
		{
			CalendarItemInstanceSchema.StartTime,
			CalendarItemInstanceSchema.EndTime,
			CalendarItemBaseSchema.MapiIsAllDayEvent,
			CalendarItemBaseSchema.FreeBusyStatus,
			CalendarItemBaseSchema.AppointmentSequenceNumber,
			CalendarItemBaseSchema.AppointmentRecurrenceBlob,
			CalendarItemBaseSchema.TimeZoneDefinitionRecurring,
			ItemSchema.TimeZoneDefinitionStart,
			ItemSchema.Subject
		};

		// Token: 0x040052DF RID: 21215
		private static readonly PropertyDefinition[] LimitedDetailOnly = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.Location,
			ItemSchema.Sensitivity
		};

		// Token: 0x040052E0 RID: 21216
		private static readonly PropertyDefinition[] FullDetailOnly = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x040052E1 RID: 21217
		private static readonly PropertyDefinition[] ExportQueryOnly = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.TimeZone,
			CalendarItemBaseSchema.TimeZoneBlob,
			CalendarItemBaseSchema.GlobalObjectId,
			StoreObjectSchema.ItemClass,
			CalendarItemBaseSchema.IsRecurring,
			CalendarItemBaseSchema.AppointmentRecurring,
			CalendarItemBaseSchema.IsException
		};

		// Token: 0x040052E2 RID: 21218
		private static readonly PropertyDefinition[] ImportCompareOnly = new PropertyDefinition[]
		{
			ItemSchema.BodyTag
		};

		// Token: 0x040052E3 RID: 21219
		private static readonly PropertyDefinition[] ImportUpdateOnly = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.GlobalObjectId,
			CalendarItemBaseSchema.CleanGlobalObjectId,
			CalendarItemBaseSchema.TimeZone,
			CalendarItemBaseSchema.TimeZoneBlob,
			ItemSchema.TimeZoneDefinitionStart,
			CalendarItemBaseSchema.TimeZoneDefinitionEnd
		};

		// Token: 0x040052E4 RID: 21220
		private static readonly PropertyDefinition[] ImportQueryOnly = new PropertyDefinition[]
		{
			ItemSchema.Id,
			CalendarItemBaseSchema.GlobalObjectId,
			CalendarItemBaseSchema.AppointmentRecurring,
			CalendarItemBaseSchema.TimeZone,
			CalendarItemBaseSchema.TimeZoneBlob
		};

		// Token: 0x040052E5 RID: 21221
		private static PropertyDefinition[] exportFreeBusy;

		// Token: 0x040052E6 RID: 21222
		private static object exportFreeBusyLock = new object();

		// Token: 0x040052E7 RID: 21223
		private static PropertyDefinition[] exportLimitedDetails;

		// Token: 0x040052E8 RID: 21224
		private static object exportLimitedDetailsLock = new object();

		// Token: 0x040052E9 RID: 21225
		private static PropertyDefinition[] exportFullDetails;

		// Token: 0x040052EA RID: 21226
		private static object exportFullDetailsLock = new object();

		// Token: 0x040052EB RID: 21227
		private static PropertyDefinition[] importCompare;

		// Token: 0x040052EC RID: 21228
		private static object importCompareLock = new object();

		// Token: 0x040052ED RID: 21229
		private static PropertyDefinition[] importUpdate;

		// Token: 0x040052EE RID: 21230
		private static object importUpdateLock = new object();

		// Token: 0x040052EF RID: 21231
		private static PropertyDefinition[] importQuery;

		// Token: 0x040052F0 RID: 21232
		private static object importQueryLock = new object();
	}
}
