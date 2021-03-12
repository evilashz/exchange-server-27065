using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003F8 RID: 1016
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecurrenceBlobMerger
	{
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000BE89D File Offset: 0x000BCA9D
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000BE8A5 File Offset: 0x000BCAA5
		private CalendarItem CalendarItem { get; set; }

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000BE8AE File Offset: 0x000BCAAE
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x000BE8B6 File Offset: 0x000BCAB6
		private string GlobalObjectId { get; set; }

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000BE8BF File Offset: 0x000BCABF
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x000BE8C7 File Offset: 0x000BCAC7
		private StoreSession Session { get; set; }

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000BE8D0 File Offset: 0x000BCAD0
		// (set) Token: 0x06002E6C RID: 11884 RVA: 0x000BE8D8 File Offset: 0x000BCAD8
		private InternalRecurrence OriginalRecurrence { get; set; }

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000BE8E1 File Offset: 0x000BCAE1
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x000BE8E9 File Offset: 0x000BCAE9
		private InternalRecurrence NewRecurrence { get; set; }

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000BE8F2 File Offset: 0x000BCAF2
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x000BE8FA File Offset: 0x000BCAFA
		private IList<ExDateTime> OccurrencesToDelete { get; set; }

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000BE903 File Offset: 0x000BCB03
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000BE90B File Offset: 0x000BCB0B
		private List<ExDateTime> OccurrencesToRevive { get; set; }

		// Token: 0x06002E73 RID: 11891 RVA: 0x000BE914 File Offset: 0x000BCB14
		public static bool Merge(StoreSession session, CalendarItem calendarItem, GlobalObjectId globalObjectId, InternalRecurrence originalRecurrence, InternalRecurrence newRecurrence)
		{
			return new RecurrenceBlobMerger(session, calendarItem, globalObjectId, originalRecurrence, newRecurrence).Merge();
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000BE926 File Offset: 0x000BCB26
		private RecurrenceBlobMerger(StoreSession session, CalendarItem calendarItem, GlobalObjectId globalObjectId, InternalRecurrence originalRecurrence, InternalRecurrence newRecurrence)
		{
			this.Session = session;
			this.CalendarItem = calendarItem;
			this.GlobalObjectId = ((globalObjectId == null) ? string.Empty : globalObjectId.ToString());
			this.OriginalRecurrence = originalRecurrence;
			this.NewRecurrence = newRecurrence;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000BE964 File Offset: 0x000BCB64
		private bool Merge()
		{
			this.CalculateLocalDeletions();
			this.CalculateOccurrencesToRevive();
			bool flag = this.CopyLocalModifications();
			flag |= this.CopyLocalDeletions();
			return flag | this.ReviveCancelledOcurrences();
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000BE998 File Offset: 0x000BCB98
		private bool CopyLocalModifications()
		{
			bool flag = false;
			foreach (OccurrenceInfo occurrenceInfo in this.OriginalRecurrence.GetModifiedOccurrences())
			{
				ExceptionInfo exceptionInfo = occurrenceInfo as ExceptionInfo;
				if (exceptionInfo != null)
				{
					if (this.NewRecurrence.IsValidOccurrenceId(exceptionInfo.OccurrenceDateId))
					{
						if (!this.NewRecurrence.IsOccurrenceDeleted(exceptionInfo.OccurrenceDateId) && !(this.NewRecurrence.GetOccurrenceInfoByDateId(exceptionInfo.OccurrenceDateId) is ExceptionInfo))
						{
							this.NewRecurrence.ModifyOccurrence(exceptionInfo);
							flag = true;
							ExTraceGlobals.RecurrenceTracer.Information<string, ExDateTime>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CopyLocalModifications: GOID={0} has revived a local exception with id={1}", this.GlobalObjectId, exceptionInfo.OccurrenceDateId);
						}
					}
					else if (this.CalendarItem != null)
					{
						ExTraceGlobals.RecurrenceTracer.Information<string>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CopyLocalModifications: GOID={0} will remove the attachment for an invalid occurrence", this.GlobalObjectId);
						RecurrenceManager.DeleteAttachment(this.CalendarItem, this.NewRecurrence.CreatedExTimeZone, exceptionInfo.StartTime, exceptionInfo.EndTime);
					}
				}
			}
			ExTraceGlobals.RecurrenceTracer.Information<string, bool>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CopyLocalModifications: GOID={0}; will return {1}", this.GlobalObjectId, flag);
			return flag;
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000BEAD8 File Offset: 0x000BCCD8
		private bool CopyLocalDeletions()
		{
			bool flag = false;
			foreach (ExDateTime exDateTime in this.OccurrencesToDelete)
			{
				if (this.NewRecurrence.TryDeleteOccurrence(exDateTime.Date))
				{
					flag = true;
					ExTraceGlobals.RecurrenceTracer.Information<string, ExDateTime>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CopyLocalDeletions has deleted an occurrence: GOID={0}; Occurrence id={1}", this.GlobalObjectId, exDateTime.Date);
				}
			}
			ExTraceGlobals.RecurrenceTracer.Information<string, bool>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CopyLocalDeletions: GOID={0}; will return {1}", this.GlobalObjectId, flag);
			return flag;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000BEB78 File Offset: 0x000BCD78
		private bool ReviveCancelledOcurrences()
		{
			bool flag = false;
			foreach (ExDateTime exDateTime in this.OccurrencesToRevive)
			{
				ExDateTime date = exDateTime.Date;
				ExceptionInfo exceptionInfo = this.OriginalRecurrence.GetOccurrenceInfoByDateId(date) as ExceptionInfo;
				if (exceptionInfo != null && this.NewRecurrence.TryUndeleteOccurrence(date))
				{
					this.NewRecurrence.ModifyOccurrence(exceptionInfo);
					flag = true;
					ExTraceGlobals.RecurrenceTracer.Information<string, ExDateTime>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.ReviveCancelledOcurrences has revived an occurrence: GOID={0}; Occurrence Id={1}", this.GlobalObjectId, date);
				}
			}
			ExTraceGlobals.RecurrenceTracer.Information<string, bool>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.ReviveCancelledOcurrences: GOID={0}; will return {1}", this.GlobalObjectId, flag);
			return flag;
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000BEC6C File Offset: 0x000BCE6C
		private void CalculateLocalDeletions()
		{
			this.OccurrencesToDelete = Array.FindAll<ExDateTime>(this.OriginalRecurrence.GetDeletedOccurrences(false), (ExDateTime occurrence) => this.NewRecurrence.IsValidOccurrenceId(occurrence.Date) && !this.NewRecurrence.IsOccurrenceDeleted(occurrence.Date));
			ExTraceGlobals.RecurrenceTracer.Information<string>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CalculateLocalDeletions: GOID={0}", this.GlobalObjectId);
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000BECB8 File Offset: 0x000BCEB8
		private void CalculateOccurrencesToRevive()
		{
			this.OccurrencesToRevive = new List<ExDateTime>();
			foreach (ExDateTime exDateTime in this.NewRecurrence.GetDeletedOccurrences(false))
			{
				ExDateTime date = exDateTime.Date;
				if (this.OriginalRecurrence.IsValidOccurrenceId(date) && !this.OriginalRecurrence.IsOccurrenceDeleted(date))
				{
					OccurrenceInfo occurrenceInfoByDateId = this.OriginalRecurrence.GetOccurrenceInfoByDateId(date);
					using (CalendarItemOccurrence calendarItemOccurrence = CalendarItemOccurrence.Bind(this.Session, occurrenceInfoByDateId.VersionedId))
					{
						if (calendarItemOccurrence.IsCancelled)
						{
							this.OccurrencesToRevive.Add(occurrenceInfoByDateId.OccurrenceDateId);
						}
					}
				}
			}
			ExTraceGlobals.RecurrenceTracer.Information<string>((long)this.GetHashCode(), "Storage.RecurrenceBlobMerger.CalculateOccurrencesToRevive: GOID={0}", this.GlobalObjectId);
		}
	}
}
