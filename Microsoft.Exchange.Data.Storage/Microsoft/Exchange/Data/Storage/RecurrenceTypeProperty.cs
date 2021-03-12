using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CB4 RID: 3252
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecurrenceTypeProperty : SmartPropertyDefinition
	{
		// Token: 0x0600713C RID: 28988 RVA: 0x001F6480 File Offset: 0x001F4680
		internal RecurrenceTypeProperty() : base("RecurrenceType", typeof(RecurrenceType), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.IsOneOff, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TaskRecurrence, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.MapiRecurrenceType, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x001F64E8 File Offset: 0x001F46E8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass);
			if (valueOrDefault == null)
			{
				return RecurrenceType.None;
			}
			if (ObjectClass.IsTask(valueOrDefault) || ObjectClass.IsTaskRequest(valueOrDefault))
			{
				if (propertyBag.GetValueOrDefault<bool>(InternalSchema.IsOneOff))
				{
					return RecurrenceType.None;
				}
				byte[] valueOrDefault2 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TaskRecurrence);
				return RecurrenceTypeProperty.GetTaskRecurrenceTypeFromBlob(valueOrDefault2);
			}
			else
			{
				if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(valueOrDefault) || ObjectClass.IsMeetingMessage(valueOrDefault))
				{
					return propertyBag.GetValueOrDefault<RecurrenceType>(InternalSchema.MapiRecurrenceType);
				}
				return RecurrenceType.None;
			}
		}

		// Token: 0x0600713E RID: 28990 RVA: 0x001F6574 File Offset: 0x001F4774
		private static RecurrenceType GetTaskRecurrenceTypeFromBlob(byte[] blob)
		{
			if (blob == null)
			{
				return RecurrenceType.None;
			}
			RecurrenceType result;
			try
			{
				TaskRecurrence taskRecurrence = InternalRecurrence.InternalParseTask(blob, null, null, null);
				result = RecurrenceTypeProperty.TaskRecurrencePatternToRecurrenceType(taskRecurrence);
			}
			catch (RecurrenceFormatException)
			{
				result = RecurrenceType.None;
			}
			return result;
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x001F65B0 File Offset: 0x001F47B0
		private static RecurrenceType TaskRecurrencePatternToRecurrenceType(TaskRecurrence taskRecurrence)
		{
			if (taskRecurrence == null)
			{
				return RecurrenceType.None;
			}
			RecurrencePattern pattern = taskRecurrence.Pattern;
			if (pattern is DailyRegeneratingPattern)
			{
				return RecurrenceType.DailyRegenerating;
			}
			if (pattern is WeeklyRegeneratingPattern)
			{
				return RecurrenceType.WeeklyRegenerating;
			}
			if (pattern is MonthlyRegeneratingPattern)
			{
				return RecurrenceType.MonthlyRegenerating;
			}
			if (pattern is YearlyRegeneratingPattern)
			{
				return RecurrenceType.YearlyRegenerating;
			}
			if (pattern is DailyRecurrencePattern)
			{
				return RecurrenceType.Daily;
			}
			if (pattern is WeeklyRecurrencePattern)
			{
				return RecurrenceType.Weekly;
			}
			if (pattern is MonthlyRecurrencePattern || pattern is MonthlyThRecurrencePattern)
			{
				return RecurrenceType.Monthly;
			}
			if (pattern is YearlyRecurrencePattern || pattern is YearlyThRecurrencePattern)
			{
				return RecurrenceType.Yearly;
			}
			return RecurrenceType.None;
		}
	}
}
