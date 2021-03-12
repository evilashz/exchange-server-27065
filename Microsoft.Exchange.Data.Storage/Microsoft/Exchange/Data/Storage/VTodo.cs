using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200082E RID: 2094
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class VTodo : VItemBase
	{
		// Token: 0x06004DF1 RID: 19953 RVA: 0x00145B90 File Offset: 0x00143D90
		static VTodo()
		{
			VTodo.CreateSchema();
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00145B97 File Offset: 0x00143D97
		internal static Dictionary<object, SchemaInfo> GetConversionSchema()
		{
			return VTodo.conversionSchema;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x00145B9E File Offset: 0x00143D9E
		private static void AddSchemaInfo(CalendarPropertyId calendarPropertyId, object promotionMethod, object demotionMethod, CalendarMethod methods, IcalFlags flags)
		{
			VTodo.conversionSchema.Add(calendarPropertyId.Key, new SchemaInfo(calendarPropertyId, promotionMethod, demotionMethod, methods, flags));
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00145BBB File Offset: 0x00143DBB
		private static void AddSchemaInfo(PropertyId propertyId, object promotionMethod, object demotionMethod)
		{
			VTodo.AddSchemaInfo(new CalendarPropertyId(propertyId), promotionMethod, demotionMethod, CalendarMethod.All, IcalFlags.None);
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x00145BD0 File Offset: 0x00143DD0
		private static void AddSchemaInfo(string propertyName, object promotionMethod, object demotionMethod)
		{
			VTodo.AddSchemaInfo(new CalendarPropertyId(propertyName), promotionMethod, demotionMethod, CalendarMethod.All, IcalFlags.None);
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00145BE8 File Offset: 0x00143DE8
		private static void CreateSchema()
		{
			VTodo.conversionSchema = new Dictionary<object, SchemaInfo>();
			VTodo.AddSchemaInfo(PropertyId.DateTimeStart, new PromoteTaskPropertyDelegate(VTodo.PromoteStartDate), null);
			VTodo.AddSchemaInfo(PropertyId.Completed, new PromoteTaskPropertyDelegate(VTodo.PromoteCompleteDate), null);
			VTodo.AddSchemaInfo(PropertyId.DateTimeDue, new PromoteTaskPropertyDelegate(VTodo.PromoteDueDate), null);
			VTodo.AddSchemaInfo(PropertyId.Status, new PromoteTaskPropertyDelegate(VTodo.PromoteStatus), null);
			VTodo.AddSchemaInfo(PropertyId.Summary, new PromoteTaskPropertyDelegate(VItemBase.PromoteSubject), null);
			VTodo.AddSchemaInfo(PropertyId.Description, new PromoteTaskPropertyDelegate(VItemBase.PromoteDescription), null);
			VTodo.AddSchemaInfo(PropertyId.Comment, new PromoteTaskPropertyDelegate(VItemBase.PromoteComment), null);
			VTodo.AddSchemaInfo(PropertyId.Class, new PromoteTaskPropertyDelegate(VItemBase.PromoteClass), null);
			VTodo.AddSchemaInfo(PropertyId.Priority, new PromoteTaskPropertyDelegate(VItemBase.PromotePriority), null);
			VTodo.AddSchemaInfo("X-MICROSOFT-CDO-IMPORTANCE", new PromoteTaskPropertyDelegate(VItemBase.PromoteXImportance), null);
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00145CC8 File Offset: 0x00143EC8
		private static bool PromoteStartDate(VTodo vtodo, CalendarPropertyBase property)
		{
			vtodo.SetProperty(InternalSchema.StartDate, property.Value);
			return true;
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x00145CDC File Offset: 0x00143EDC
		private static bool PromoteCompleteDate(VTodo vtodo, CalendarPropertyBase property)
		{
			vtodo.SetProperty(InternalSchema.CompleteDate, property.Value);
			return true;
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00145CF0 File Offset: 0x00143EF0
		private static bool PromoteDueDate(VTodo vtodo, CalendarPropertyBase property)
		{
			vtodo.SetProperty(InternalSchema.DueDate, property.Value);
			return true;
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00145D04 File Offset: 0x00143F04
		private static bool PromoteStatus(VTodo vtodo, CalendarPropertyBase property)
		{
			TaskStatus taskStatus = TaskStatus.NotStarted;
			if (string.Compare((string)property.Value, "IN-PROCESS", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				taskStatus = TaskStatus.InProgress;
			}
			else if (string.Compare((string)property.Value, "COMPLETED", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				taskStatus = TaskStatus.Completed;
			}
			else if (string.Compare((string)property.Value, "NEEDS-ACTION", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				taskStatus = TaskStatus.NotStarted;
			}
			else if (string.Compare((string)property.Value, "CANCELLED", StringComparison.CurrentCultureIgnoreCase) == 0)
			{
				taskStatus = TaskStatus.Deferred;
			}
			vtodo.SetProperty(InternalSchema.TaskStatus, taskStatus);
			return true;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x00145D93 File Offset: 0x00143F93
		internal VTodo(CalendarComponentBase root) : base(root)
		{
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00145D9C File Offset: 0x00143F9C
		internal bool Promote(Item item)
		{
			Util.ThrowOnNullArgument(item, "item");
			bool result = false;
			this.item = item;
			this.SetTimeZone(item.PropertyBag.ExTimeZone);
			try
			{
				this.item.ClassName = "IPM.Task";
				result = (this.PromoteProperties() && this.PromoteComplexProperties() && this.PromoteRecurrence() && this.PromoteReminders());
			}
			catch (ArgumentException arg)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, ArgumentException>((long)this.GetHashCode(), "VTodo::Promote. UID:'{0}'. Found exception:'{1}'", base.Uid, arg);
				base.Context.AddError(ServerStrings.InvalidICalElement(base.ComponentName));
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.ICalTracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "VTodo::Promote. UID:'{0}'. Found exception:'{1}'", base.Uid, ex);
				base.Context.AddError(ex.LocalizedString);
			}
			return result;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x00145E88 File Offset: 0x00144088
		internal void Demote(Item item)
		{
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00145E8C File Offset: 0x0014408C
		protected override bool ValidateProperties()
		{
			if (!base.ValidateTimeZoneInfo(true))
			{
				return false;
			}
			if (base.IcalRecurrence != null)
			{
				this.xsoRecurrence = base.XsoRecurrenceFromICalRecurrence(base.IcalRecurrence, (ExDateTime)this.dueTime.Value);
				if (this.xsoRecurrence == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00145ED9 File Offset: 0x001440D9
		protected override PropertyBag GetPropertyBag()
		{
			return this.item.PropertyBag;
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x00145EE8 File Offset: 0x001440E8
		protected override bool ValidateProperty(CalendarPropertyBase calendarProperty)
		{
			bool flag = base.ValidateProperty(calendarProperty);
			PropertyId propertyId = calendarProperty.CalendarPropertyId.PropertyId;
			if (propertyId == PropertyId.DateTimeDue)
			{
				if (calendarProperty is CalendarDateTime)
				{
					this.dueTime = (CalendarDateTime)calendarProperty;
				}
				else
				{
					flag = false;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VTodo::ValidateProperty. Property validation failed. Property:'{1}'", calendarProperty.CalendarPropertyId.PropertyName);
				base.Context.AddError(ServerStrings.InvalidICalElement(calendarProperty.CalendarPropertyId.PropertyName));
			}
			return flag;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00145F68 File Offset: 0x00144168
		protected override void SetTimeZone(ExTimeZone itemTimeZone)
		{
			base.SetTimeZone(itemTimeZone);
			if (this.dueTime != null && !string.IsNullOrEmpty(this.dueTime.TimeZoneId))
			{
				this.timeZone = base.InboundContext.DeclaredTimeZones[this.dueTime.TimeZoneId];
			}
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x00145FB8 File Offset: 0x001441B8
		private bool PromoteReminders()
		{
			if (base.Context.Method == CalendarMethod.Request || base.Context.Method == CalendarMethod.Publish)
			{
				ExDateTime valueOrDefault = base.GetValueOrDefault<ExDateTime>(InternalSchema.DueDate, ExDateTime.MinValue);
				bool flag = false;
				if (this.item != null && this.item.Session != null && this.item.Session.IsOlcMoveDestination)
				{
					InternalRecurrence recurrenceFromItem = CalendarItem.GetRecurrenceFromItem(this.item);
					ExDateTime exDateTime;
					if (recurrenceFromItem != null)
					{
						exDateTime = recurrenceFromItem.EndDate + recurrenceFromItem.EndOffset;
					}
					else
					{
						exDateTime = valueOrDefault;
					}
					flag = (exDateTime.CompareTo(ExDateTime.UtcNow) < 0);
				}
				if (this.displayVAlarm != null && this.displayVAlarm.Validate() && !flag && (this.dueTime != null || this.displayVAlarm.ValueType == CalendarValueType.DateTime))
				{
					int num = VAlarm.CalculateReminderMinutesBeforeStart(this.displayVAlarm, valueOrDefault, valueOrDefault);
					base.SetProperty(InternalSchema.ReminderIsSetInternal, true);
					base.SetProperty(InternalSchema.ReminderDueBy, valueOrDefault.AddMinutes((double)(-(double)num)));
				}
				else
				{
					base.SetProperty(InternalSchema.ReminderMinutesBeforeStart, 0);
					base.SetProperty(InternalSchema.ReminderIsSetInternal, false);
				}
				if (!flag)
				{
					VAlarm.PromoteEmailReminders(this.item, this.emailVAlarms, valueOrDefault, valueOrDefault, false);
				}
			}
			return true;
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x001460FC File Offset: 0x001442FC
		private bool PromoteProperties()
		{
			this.HandleFloatingTime();
			CalendarMethod method = base.Context.Method;
			foreach (CalendarPropertyBase calendarPropertyBase in base.ICalProperties)
			{
				SchemaInfo schemaInfo;
				if (VTodo.conversionSchema.TryGetValue(calendarPropertyBase.CalendarPropertyId.Key, out schemaInfo) && schemaInfo.PromotionMethod != null)
				{
					if ((method & schemaInfo.Methods) != method)
					{
						continue;
					}
					object promotionMethod = schemaInfo.PromotionMethod;
					PromoteTaskPropertyDelegate promoteTaskPropertyDelegate = promotionMethod as PromoteTaskPropertyDelegate;
					try
					{
						if (promoteTaskPropertyDelegate != null)
						{
							if (!promoteTaskPropertyDelegate(this, calendarPropertyBase))
							{
								string propertyName = calendarPropertyBase.CalendarPropertyId.PropertyName;
								ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VTodo::PromoteProperties. Failed to promote property. Property:'{1}'.", propertyName);
								base.Context.AddError(ServerStrings.InvalidICalElement(propertyName));
								return false;
							}
						}
						else
						{
							PropertyDefinition propertyDefinition = (PropertyDefinition)promotionMethod;
							base.SetProperty(propertyDefinition, calendarPropertyBase.Value);
						}
						continue;
					}
					catch (ArgumentException)
					{
						if (calendarPropertyBase.ValueType == CalendarValueType.DateTime || calendarPropertyBase.ValueType == CalendarValueType.Date)
						{
							string propertyName2 = calendarPropertyBase.CalendarPropertyId.PropertyName;
							ExTraceGlobals.ICalTracer.TraceError<string>((long)this.GetHashCode(), "VTodo::PromoteProperties. Failed to promote data time property. Property:'{1}'.", propertyName2);
							base.Context.AddError(ServerStrings.InvalidICalElement(propertyName2));
							return false;
						}
						throw;
					}
				}
				ExTraceGlobals.ICalTracer.TraceDebug<CalendarPropertyId>((long)this.GetHashCode(), "VTodo::PromoteProperties. There is no method to promote property: {0}", calendarPropertyBase.CalendarPropertyId);
			}
			return true;
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x001462A4 File Offset: 0x001444A4
		private void HandleFloatingTime()
		{
			if (this.dtStart != null)
			{
				ExDateTime exDateTime = (ExDateTime)this.dtStart.Value;
				if (!exDateTime.HasTimeZone)
				{
					this.dtStart.Value = this.timeZone.Assign(exDateTime);
				}
			}
			if (this.dueTime != null)
			{
				ExDateTime exDateTime2 = (ExDateTime)this.dueTime.Value;
				if (!exDateTime2.HasTimeZone)
				{
					this.dueTime.Value = this.timeZone.Assign(exDateTime2);
				}
			}
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x00146330 File Offset: 0x00144530
		private bool PromoteRecurrence()
		{
			if (this.xsoRecurrence == null)
			{
				return true;
			}
			ExDateTime dt = (ExDateTime)this.dueTime.Value;
			InternalRecurrence internalRecurrence = new InternalRecurrence(this.xsoRecurrence.Pattern, this.xsoRecurrence.Range, this.item, this.xsoRecurrence.CreatedExTimeZone, this.xsoRecurrence.ReadExTimeZone, dt - dt.Date, dt - dt.Date, new ExDateTime?(this.xsoRecurrence.EndDate));
			byte[] propertyValue = internalRecurrence.ToByteArray();
			base.SetProperty(InternalSchema.TaskRecurrence, propertyValue);
			return true;
		}

		// Token: 0x04002A98 RID: 10904
		private static Dictionary<object, SchemaInfo> conversionSchema;

		// Token: 0x04002A99 RID: 10905
		private CalendarDateTime dueTime;
	}
}
