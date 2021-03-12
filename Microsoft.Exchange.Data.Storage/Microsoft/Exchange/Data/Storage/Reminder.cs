using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A8 RID: 936
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Reminder
	{
		// Token: 0x06002AAB RID: 10923 RVA: 0x000AA8F8 File Offset: 0x000A8AF8
		internal Reminder(Item item)
		{
			this.item = item;
			this.SaveStateAsInitial(false);
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000AA910 File Offset: 0x000A8B10
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000AA988 File Offset: 0x000A8B88
		public virtual ExDateTime? DueBy
		{
			get
			{
				ExDateTime? valueAsNullable;
				try
				{
					valueAsNullable = this.Item.GetValueAsNullable<ExDateTime>(InternalSchema.ReminderDueBy);
				}
				catch (PropertyErrorException ex)
				{
					if (ex.PropertyErrors.Length == 1 && ex.PropertyErrors[0].PropertyDefinition.Equals(InternalSchema.ReminderDueBy) && ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.GetCalculatedPropertyError)
					{
						throw new CorruptDataException(ex.LocalizedString, ex);
					}
					throw;
				}
				return valueAsNullable;
			}
			set
			{
				if (value != null)
				{
					this.Item[InternalSchema.ReminderDueBy] = value.Value;
					return;
				}
				throw new ArgumentNullException("value");
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000AA9BA File Offset: 0x000A8BBA
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000AA9CC File Offset: 0x000A8BCC
		public virtual bool IsSet
		{
			get
			{
				return this.Item.GetValueOrDefault<bool>(InternalSchema.ReminderIsSet);
			}
			set
			{
				this.Item[InternalSchema.ReminderIsSet] = value;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000AA9F6 File Offset: 0x000A8BF6
		public virtual int MinutesBeforeStart
		{
			get
			{
				return this.Item.GetValueOrDefault<int>(InternalSchema.ReminderMinutesBeforeStart);
			}
			set
			{
				throw this.PropertyNotSupported("MinutesBeforeStart");
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000AAA04 File Offset: 0x000A8C04
		public static ExDateTime GetNominalReminderTimeForOccurrence(IStorePropertyBag recurringMasterItem, OccurrenceInfo occurrence)
		{
			ExceptionInfo exceptionInfo = occurrence as ExceptionInfo;
			int num;
			if (exceptionInfo != null && (exceptionInfo.ModificationType & ModificationType.ReminderDelta) == ModificationType.ReminderDelta)
			{
				num = Reminder.NormalizeMinutesBeforeStart(exceptionInfo.PropertyBag.GetValueOrDefault<int>(ItemSchema.ReminderMinutesBeforeStart, 15), 15);
			}
			else
			{
				num = Reminder.NormalizeMinutesBeforeStart(recurringMasterItem.GetValueOrDefault<int>(ItemSchema.ReminderMinutesBeforeStartInternal, 15), 15);
			}
			return occurrence.StartTime.AddMinutes((double)(-(double)num));
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000AAA68 File Offset: 0x000A8C68
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x000AAA80 File Offset: 0x000A8C80
		public virtual ExDateTime? ReminderNextTime
		{
			get
			{
				return StartTimeProperty.GetNormalizedTime(this.Item.PropertyBag, InternalSchema.ReminderNextTime, null);
			}
			protected set
			{
				if (value != null)
				{
					this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(63989U);
					this.Item[InternalSchema.ReminderNextTime] = value.Value;
					EndTimeProperty.DenormalizeTimeProperty(this.Item.PropertyBag, value.Value, InternalSchema.ReminderNextTime, null);
					return;
				}
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(37525U);
				this.Item.DeleteProperties(new PropertyDefinition[]
				{
					InternalSchema.ReminderNextTime
				});
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x000AAB15 File Offset: 0x000A8D15
		protected ExDateTime? DefaultReminderNextTime
		{
			get
			{
				return Reminder.GetDefaultReminderNextTime(this.DueBy, this.MinutesBeforeStart);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000AAB28 File Offset: 0x000A8D28
		protected Item Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x000AAB30 File Offset: 0x000A8D30
		protected ExDateTime MaxOutlookDate
		{
			get
			{
				return this.Item.Session.ExTimeZone.ConvertDateTime(Reminder.MaxOutlookDateUtc);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000AAB4C File Offset: 0x000A8D4C
		private ExDateTime Now
		{
			get
			{
				return Reminder.GetTimeNow(this.item.PropertyBag.ExTimeZone);
			}
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000AAB64 File Offset: 0x000A8D64
		public virtual void Dismiss(ExDateTime actualizationTime)
		{
			ExDateTime probeTime = this.GetProbeTime(actualizationTime);
			Reminder.ReminderInfo nextPertinentItemInfo = this.GetNextPertinentItemInfo(probeTime);
			this.SetReminderTo(nextPertinentItemInfo);
			CalendarItemBase calendarItemBase = this.item as CalendarItemBase;
			if (calendarItemBase != null)
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(39413U, LastChangeAction.DismissReminder);
			}
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000AABA9 File Offset: 0x000A8DA9
		public void Disable()
		{
			this.IsSet = false;
			this.MinutesBeforeStart = 0;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000AABBC File Offset: 0x000A8DBC
		public Item GetPertinentItem(ExDateTime actualizationTime)
		{
			Reminder.ReminderInfo pertinentItemInfo = this.GetPertinentItemInfo(this.GetProbeTime(actualizationTime));
			if (pertinentItemInfo == null || pertinentItemInfo.PertinentItemId == null)
			{
				return null;
			}
			return Item.Bind(this.Item.Session, pertinentItemInfo.PertinentItemId);
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000AABFC File Offset: 0x000A8DFC
		public virtual void Snooze(ExDateTime actualizationTime, ExDateTime snoozeTime)
		{
			ExDateTime probeTime = this.GetProbeTime(actualizationTime);
			Reminder.ReminderInfo nextPertinentItemInfo = this.GetNextPertinentItemInfo(probeTime);
			if (probeTime < snoozeTime)
			{
				if (nextPertinentItemInfo == null || nextPertinentItemInfo.DefaultReminderNextTime > snoozeTime)
				{
					this.ReminderNextTime = new ExDateTime?(snoozeTime);
				}
				else
				{
					this.Dismiss(actualizationTime);
				}
				CalendarItemBase calendarItemBase = this.item as CalendarItemBase;
				if (calendarItemBase != null)
				{
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(55797U, LastChangeAction.SnoozeReminder);
				}
			}
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000AAC6C File Offset: 0x000A8E6C
		public void SnoozeBeforeDueBy(ExDateTime actualizationTime, TimeSpan beforeDueBy)
		{
			if (beforeDueBy.TotalMilliseconds < 0.0)
			{
				throw new ArgumentOutOfRangeException("beforeDueBy");
			}
			Reminder.ReminderInfo pertinentItemInfo = this.GetPertinentItemInfo(this.GetProbeTime(actualizationTime));
			if (pertinentItemInfo == null)
			{
				return;
			}
			ExDateTime exDateTime = pertinentItemInfo.DefaultDueBy;
			if (exDateTime - beforeDueBy > this.Now)
			{
				exDateTime -= beforeDueBy;
			}
			this.Snooze(actualizationTime, exDateTime);
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000AACD3 File Offset: 0x000A8ED3
		public virtual void Adjust()
		{
			this.Adjust(this.Now);
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000AACE4 File Offset: 0x000A8EE4
		internal static void Adjust(StoreObject storeObject)
		{
			Item item = storeObject as Item;
			if (item != null && item.Reminder != null)
			{
				item.LocationIdentifierHelperInstance.SetLocationIdentifier(43509U);
				item.Reminder.Adjust();
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000AAD1E File Offset: 0x000A8F1E
		internal static void EnsureMinutesBeforeStartIsInRange(Item item)
		{
			Reminder.EnsureMinutesBeforeStartIsInRange(item, 15);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000AAD28 File Offset: 0x000A8F28
		internal static void EnsureMinutesBeforeStartIsInRange(Item item, int consumerDefaultMinutesBeforeStart)
		{
			int valueOrDefault = item.GetValueOrDefault<int>(InternalSchema.ReminderMinutesBeforeStart);
			int num = Reminder.NormalizeMinutesBeforeStart(valueOrDefault, consumerDefaultMinutesBeforeStart);
			if (valueOrDefault != num)
			{
				if (valueOrDefault != 1525252321 || !(item is MeetingMessage))
				{
					ExTraceGlobals.StorageTracer.TraceDebug<int, int, string>((long)item.GetHashCode(), "Value for ReminderMinutesBeforeStart is outside of the legitimate bounds: {0}, using {1} instead. Item class = {2}", valueOrDefault, num, item.ClassName);
				}
				item[InternalSchema.ReminderMinutesBeforeStart] = num;
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000AAD8C File Offset: 0x000A8F8C
		internal static int NormalizeMinutesBeforeStart(int minutesBeforeStart, int consumerDefaultMinutesBeforeStart)
		{
			if (minutesBeforeStart != 1525252321 && minutesBeforeStart >= 0 && minutesBeforeStart <= 2629800)
			{
				return minutesBeforeStart;
			}
			return consumerDefaultMinutesBeforeStart;
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000AADA8 File Offset: 0x000A8FA8
		internal static ExDateTime GetProbeTime(ExDateTime actualizationTime, ExDateTime? reminderNextTime)
		{
			ExDateTime exDateTime = reminderNextTime ?? ExDateTime.MaxValue;
			if (!(actualizationTime > exDateTime))
			{
				return exDateTime;
			}
			return actualizationTime;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000AADDC File Offset: 0x000A8FDC
		internal static ExDateTime GetTimeNow(ExTimeZone timeZone)
		{
			ExDateTime exDateTime = ExDateTime.GetNow(timeZone);
			Reminder.TestTimeHook testTimeHook = Reminder.testTimeHook;
			if (testTimeHook != null)
			{
				exDateTime = testTimeHook(exDateTime);
			}
			return exDateTime;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000AAE04 File Offset: 0x000A9004
		internal static Reminder.TestTimeHook SetTestTimeHook(Reminder.TestTimeHook newTestTimeHook)
		{
			Reminder.TestTimeHook result = Reminder.testTimeHook;
			Reminder.testTimeHook = newTestTimeHook;
			return result;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000AAE20 File Offset: 0x000A9020
		protected internal bool HasAcrAffectedReminders(ConflictResolutionResult acrResults)
		{
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				InternalSchema.ReminderIsSetInternal,
				InternalSchema.ReminderDueByInternal,
				InternalSchema.ReminderNextTime
			};
			if (acrResults.SaveStatus == SaveResult.SuccessWithConflictResolution)
			{
				foreach (PropertyConflict propertyConflict in acrResults.PropertyConflicts)
				{
					foreach (PropertyDefinition obj in array)
					{
						if (propertyConflict.PropertyDefinition.Equals(obj))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000AAEB0 File Offset: 0x000A90B0
		protected internal virtual void SaveStateAsInitial(bool throwOnFailure)
		{
			this.lastSetTo = null;
			this.isSnoozed = false;
			if (this.ReminderNextTime != null)
			{
				try
				{
					Reminder.ReminderInfo pertinentItemInfo = this.GetPertinentItemInfo(this.ReminderNextTime.Value);
					this.isSnoozed = (pertinentItemInfo != null && this.ReminderNextTime != pertinentItemInfo.DefaultReminderNextTime);
					this.lastSetTo = pertinentItemInfo;
				}
				catch (CorruptDataException arg)
				{
					ExTraceGlobals.RecurrenceTracer.Information<Type, CorruptDataException>((long)this.GetHashCode(), "{0}.SaveStateAsInitial failed: {1}", base.GetType(), arg);
					if (throwOnFailure)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000AAF68 File Offset: 0x000A9168
		protected static void Adjust(Reminder reminder, ExDateTime actualizationTime)
		{
			reminder.Adjust(actualizationTime);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000AAF74 File Offset: 0x000A9174
		protected static ExDateTime? GetDefaultReminderNextTime(ExDateTime? dueBy, int minutesBeforeStart)
		{
			if (dueBy == null)
			{
				return null;
			}
			int num = Reminder.NormalizeMinutesBeforeStart(minutesBeforeStart, 15);
			ExDateTime? result;
			try
			{
				if (dueBy <= ExDateTime.MinValue.AddMinutes((double)num))
				{
					num = 15;
				}
				result = new ExDateTime?(dueBy.Value.AddMinutes((double)(-(double)num)));
			}
			catch (ArgumentOutOfRangeException)
			{
				result = new ExDateTime?(dueBy.Value);
			}
			return result;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000AB00C File Offset: 0x000A920C
		protected virtual void Adjust(ExDateTime actualizationTime)
		{
			this.EnsureRequiredPropertiesArePresent();
			Reminder.ReminderInfo pertinentItemInfo = this.GetPertinentItemInfo(actualizationTime);
			Reminder.ReminderInfo nextPertinentItemInfo = this.GetNextPertinentItemInfo(actualizationTime);
			Reminder.ReminderInfo reminderInfo;
			if (this.ReminderNextTime == null || this.lastSetTo == null || pertinentItemInfo == null)
			{
				reminderInfo = (nextPertinentItemInfo ?? pertinentItemInfo);
			}
			else if (nextPertinentItemInfo != null && this.ReminderNextTime >= nextPertinentItemInfo.DefaultReminderNextTime)
			{
				reminderInfo = nextPertinentItemInfo;
			}
			else if (this.ReminderNextTime >= pertinentItemInfo.DefaultReminderNextTime)
			{
				if (Reminder.ReminderInfo.IsForSamePertinentItem(pertinentItemInfo, this.lastSetTo))
				{
					if (this.lastSetTo.DefaultReminderNextTime != pertinentItemInfo.DefaultReminderNextTime || this.lastSetTo.DefaultDueBy != pertinentItemInfo.DefaultDueBy)
					{
						reminderInfo = pertinentItemInfo;
					}
					else
					{
						reminderInfo = null;
					}
				}
				else
				{
					reminderInfo = nextPertinentItemInfo;
				}
			}
			else
			{
				reminderInfo = null;
			}
			if (reminderInfo != null)
			{
				this.SetReminderTo(reminderInfo);
			}
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000AB10C File Offset: 0x000A930C
		protected virtual Reminder.ReminderInfo GetNextPertinentItemInfo(ExDateTime actualizationTime)
		{
			if (!this.IsSet || !(this.DefaultReminderNextTime > actualizationTime))
			{
				return null;
			}
			return this.GetPertinentItemInfo(this.DefaultReminderNextTime.Value);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000AB15C File Offset: 0x000A935C
		protected virtual Reminder.ReminderInfo GetPertinentItemInfo(ExDateTime actualizationTime)
		{
			if (!this.IsSet || !(this.DefaultReminderNextTime <= actualizationTime))
			{
				return null;
			}
			return new Reminder.ReminderInfo(this.DueBy.Value, this.DefaultReminderNextTime.Value, this.Item.Id);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000AB1C3 File Offset: 0x000A93C3
		protected Exception PropertyNotSupported(string propertyName)
		{
			return new NotSupportedException(ServerStrings.ReminderPropertyNotSupported(this.Item.GetType().Name, propertyName));
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000AB1E8 File Offset: 0x000A93E8
		private void EnsureRequiredPropertiesArePresent()
		{
			if (PropertyError.IsPropertyNotFound(this.Item.TryGetProperty(InternalSchema.ReminderIsSetInternal)))
			{
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(57205U);
				this.Item[InternalSchema.ReminderIsSetInternal] = false;
			}
			if (PropertyError.IsPropertyNotFound(this.Item.TryGetProperty(InternalSchema.ReminderMinutesBeforeStartInternal)))
			{
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(44917U);
				this.Item[InternalSchema.ReminderMinutesBeforeStartInternal] = 0;
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000AB279 File Offset: 0x000A9479
		private ExDateTime GetProbeTime(ExDateTime actualizationTime)
		{
			return Reminder.GetProbeTime(actualizationTime, this.ReminderNextTime);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000AB288 File Offset: 0x000A9488
		private void SetReminderTo(Reminder.ReminderInfo newPertinentItem)
		{
			if (newPertinentItem != null)
			{
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(61301U);
				this.Item[InternalSchema.ReminderIsSetInternal] = true;
				this.Item[InternalSchema.ReminderDueByInternal] = newPertinentItem.DefaultDueBy;
				this.ReminderNextTime = new ExDateTime?(newPertinentItem.DefaultReminderNextTime);
			}
			else
			{
				this.Item.LocationIdentifierHelperInstance.SetLocationIdentifier(53109U);
				this.Item[InternalSchema.ReminderIsSetInternal] = false;
				this.Item[InternalSchema.TaskResetReminder] = true;
			}
			this.lastSetTo = newPertinentItem;
			this.isSnoozed = false;
		}

		// Token: 0x0400181A RID: 6170
		public const int DefaultMinutesBeforeStart = 15;

		// Token: 0x0400181B RID: 6171
		public const int MarkerForDefaultMinutesBeforeStart = 1525252321;

		// Token: 0x0400181C RID: 6172
		public const int MinutesBeforeStartMin = 0;

		// Token: 0x0400181D RID: 6173
		public const int MinutesBeforeStartMax = 2629800;

		// Token: 0x0400181E RID: 6174
		internal static ExDateTime MaxOutlookDateUtc = new ExDateTime(ExTimeZone.UtcTimeZone, new DateTime(4501, 1, 1));

		// Token: 0x0400181F RID: 6175
		protected Reminder.ReminderInfo lastSetTo;

		// Token: 0x04001820 RID: 6176
		protected bool isSnoozed;

		// Token: 0x04001821 RID: 6177
		private readonly Item item;

		// Token: 0x04001822 RID: 6178
		private static Reminder.TestTimeHook testTimeHook;

		// Token: 0x020003A9 RID: 937
		// (Invoke) Token: 0x06002AD3 RID: 10963
		internal delegate ExDateTime TestTimeHook(ExDateTime localNow);

		// Token: 0x020003AA RID: 938
		protected class ReminderInfo
		{
			// Token: 0x06002AD6 RID: 10966 RVA: 0x000AB35D File Offset: 0x000A955D
			public ReminderInfo(ExDateTime defaultDueBy, ExDateTime defaultReminderNextTime, StoreId pertinentItemId)
			{
				this.DefaultDueBy = defaultDueBy;
				this.DefaultReminderNextTime = defaultReminderNextTime;
				this.PertinentItemId = pertinentItemId;
			}

			// Token: 0x06002AD7 RID: 10967 RVA: 0x000AB37C File Offset: 0x000A957C
			public static bool IsForSamePertinentItem(Reminder.ReminderInfo v1, Reminder.ReminderInfo v2)
			{
				bool? flag = v1.IsForSamePertinentItem(v2);
				if (flag == null)
				{
					return v2.IsForSamePertinentItem(v1) ?? true;
				}
				return flag.GetValueOrDefault();
			}

			// Token: 0x06002AD8 RID: 10968 RVA: 0x000AB3BC File Offset: 0x000A95BC
			protected virtual bool? IsForSamePertinentItem(Reminder.ReminderInfo reminderInfo)
			{
				return null;
			}

			// Token: 0x04001823 RID: 6179
			public readonly ExDateTime DefaultDueBy;

			// Token: 0x04001824 RID: 6180
			public readonly ExDateTime DefaultReminderNextTime;

			// Token: 0x04001825 RID: 6181
			public readonly StoreId PertinentItemId;
		}
	}
}
