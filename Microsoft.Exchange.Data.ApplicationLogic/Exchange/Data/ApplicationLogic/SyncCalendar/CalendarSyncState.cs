using System;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.SyncCalendar
{
	// Token: 0x020001B6 RID: 438
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CalendarSyncState
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x00045ABB File Offset: 0x00043CBB
		public CalendarSyncState(string base64IcsSyncState, CalendarViewQueryResumptionPoint queryResumptionPoint, ExDateTime? oldWindowEndTime)
		{
			this.IcsSyncState = base64IcsSyncState;
			this.QueryResumptionPoint = queryResumptionPoint;
			this.OldWindowEnd = oldWindowEndTime;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00045AD8 File Offset: 0x00043CD8
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00045AE0 File Offset: 0x00043CE0
		public string IcsSyncState { get; protected set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00045AE9 File Offset: 0x00043CE9
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00045AF1 File Offset: 0x00043CF1
		public CalendarViewQueryResumptionPoint QueryResumptionPoint { get; protected set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00045AFA File Offset: 0x00043CFA
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00045B02 File Offset: 0x00043D02
		public ExDateTime? OldWindowEnd { get; protected set; }

		// Token: 0x060010E1 RID: 4321 RVA: 0x00045B0C File Offset: 0x00043D0C
		public static bool IsEmpty(CalendarSyncState calendarSyncState)
		{
			return calendarSyncState.IcsSyncState == null && calendarSyncState.QueryResumptionPoint == null && calendarSyncState.OldWindowEnd == null;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00045B3C File Offset: 0x00043D3C
		public static ExDateTime? SafeGetDateTimeValue(string value)
		{
			long ticks;
			if (!string.IsNullOrEmpty(value) && long.TryParse(value, out ticks))
			{
				return new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, ticks));
			}
			return null;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00045B78 File Offset: 0x00043D78
		public static CalendarViewQueryResumptionPoint SafeGetResumptionPoint(string version, string recurringPhase, string instanceKey, string sortKeyValue)
		{
			CalendarViewQueryResumptionPoint result = null;
			bool resumeToRecurringMeetings;
			if (version == CalendarViewQueryResumptionPoint.CurrentVersion && bool.TryParse(recurringPhase, out resumeToRecurringMeetings))
			{
				result = CalendarViewQueryResumptionPoint.CreateInstance(resumeToRecurringMeetings, CalendarSyncState.SafeGetInstanceKey(instanceKey), CalendarSyncState.SafeGetDateTimeValue(sortKeyValue));
			}
			return result;
		}

		// Token: 0x060010E4 RID: 4324
		public abstract IFolderSyncState CreateFolderSyncState(StoreObjectId folderObjectId, ISyncProvider syncProvider);

		// Token: 0x060010E5 RID: 4325 RVA: 0x00045BB4 File Offset: 0x00043DB4
		public override string ToString()
		{
			string text;
			string text2;
			string text3;
			string text4;
			if (this.QueryResumptionPoint == null)
			{
				text = null;
				text2 = null;
				text3 = null;
				text4 = null;
			}
			else
			{
				text = this.QueryResumptionPoint.Version;
				text2 = this.QueryResumptionPoint.ResumeToRecurringMeetings.ToString();
				text3 = this.InstanceKeyToString(this.QueryResumptionPoint.InstanceKey);
				text4 = this.DateTimeToString(new ExDateTime?(this.QueryResumptionPoint.SortKeyValue));
			}
			string[] value = new string[]
			{
				"v2",
				this.IcsSyncState,
				text,
				text2,
				text3,
				text4,
				this.DateTimeToString(this.OldWindowEnd)
			};
			return string.Join(','.ToString(), value);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00045C74 File Offset: 0x00043E74
		private static byte[] SafeGetInstanceKey(string value)
		{
			byte[] result = null;
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					result = Convert.FromBase64String(value);
				}
				catch (FormatException ex)
				{
					ExTraceGlobals.SyncCalendarCallTracer.TraceDebug<string, string>(0L, "CalendarSyncState.SafeGetInstanceKey trying to parse invalid instance key ({0}). Error: {1}", value, ex.Message);
				}
			}
			return result;
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00045CC0 File Offset: 0x00043EC0
		private string InstanceKeyToString(byte[] instanceKey)
		{
			return Convert.ToBase64String(instanceKey ?? Array<byte>.Empty);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00045CD4 File Offset: 0x00043ED4
		private string DateTimeToString(ExDateTime? dateTime)
		{
			if (dateTime == null)
			{
				return string.Empty;
			}
			return dateTime.Value.UtcTicks.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040008EA RID: 2282
		public const string CurrentVersion = "v2";

		// Token: 0x040008EB RID: 2283
		public const char TokenSeparator = ',';
	}
}
