using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecurrenceBlobsConsistentCheck : ConsistencyCheckBase<ConsistencyCheckResult>
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000363D File Offset: 0x0000183D
		internal RecurrenceBlobsConsistentCheck(CalendarValidationContext context)
		{
			this.Initialize(ConsistencyCheckType.RecurrenceBlobsConsistentCheck, "Checks to make sure that the recurrence blobs are internally consistent.", SeverityType.Error, context, null);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003654 File Offset: 0x00001854
		protected override ConsistencyCheckResult DetectInconsistencies()
		{
			RoleType checkingRole = (RoleType)(-1);
			UserObject checkingUser = null;
			ConsistencyCheckResult consistencyCheckResult = ConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			try
			{
				Recurrence recurrence = null;
				Recurrence recurrence2 = null;
				if (base.Context.OrganizerItem != null && base.Context.OrganizerItem is CalendarItem)
				{
					checkingRole = RoleType.Organizer;
					checkingUser = base.Context.Organizer;
					recurrence = ((CalendarItem)base.Context.OrganizerItem).Recurrence;
				}
				if (base.Context.AttendeeItem != null && base.Context.AttendeeItem is CalendarItem)
				{
					checkingRole = RoleType.Attendee;
					checkingUser = base.Context.Attendee;
					recurrence2 = ((CalendarItem)base.Context.AttendeeItem).Recurrence;
				}
				consistencyCheckResult.ComparedRecurrenceBlobs = false;
				consistencyCheckResult.RecurrenceBlobComparison = true;
				if (recurrence != null)
				{
					checkingRole = (RoleType)(-1);
					checkingUser = null;
					if (recurrence2 != null)
					{
						consistencyCheckResult.ComparedRecurrenceBlobs = true;
						consistencyCheckResult.RecurrenceBlobComparison = recurrence.Equals(recurrence2);
						checkingRole = RoleType.Attendee;
						checkingUser = base.Context.Attendee;
						RecurrenceInfo recurrenceInfo = recurrence2.GetRecurrenceInfo();
						this.DetectRecurrenceInfoInconsistencies(recurrenceInfo, consistencyCheckResult);
						base.Context.AttendeeRecurrence = recurrenceInfo;
					}
					else if (base.Context.AttendeeItem != null)
					{
						this.FailCheck(consistencyCheckResult, "Attendee's missing the recurrence in a recurring meeting.", RecurrenceInconsistencyType.MissingRecurrence, base.Context.OrganizerItem.StartTime);
					}
					checkingRole = RoleType.Organizer;
					checkingUser = base.Context.Organizer;
					RecurrenceInfo recurrenceInfo2 = recurrence.GetRecurrenceInfo();
					this.DetectRecurrenceInfoInconsistencies(recurrenceInfo2, consistencyCheckResult);
					base.Context.OrganizerRecurrence = recurrenceInfo2;
				}
				else if (recurrence2 != null)
				{
					if (base.Context.OrganizerItem != null)
					{
						this.FailCheck(consistencyCheckResult, "A single meeting is recurring in the attendee's calendar.", RecurrenceInconsistencyType.ExtraRecurrence, base.Context.OrganizerItem.StartTime);
					}
					checkingRole = RoleType.Attendee;
					checkingUser = base.Context.Attendee;
					RecurrenceInfo recurrenceInfo3 = recurrence2.GetRecurrenceInfo();
					this.DetectRecurrenceInfoInconsistencies(recurrenceInfo3, consistencyCheckResult);
					base.Context.AttendeeRecurrence = recurrenceInfo3;
				}
			}
			catch (RecurrenceFormatException exception)
			{
				this.FailCheck(consistencyCheckResult, checkingRole, checkingUser, exception);
			}
			catch (StorageTransientException exception2)
			{
				this.RegisterStorageException(consistencyCheckResult, exception2);
			}
			catch (StoragePermanentException exception3)
			{
				this.RegisterStorageException(consistencyCheckResult, exception3);
			}
			return consistencyCheckResult;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003890 File Offset: 0x00001A90
		protected override void ProcessResult(ConsistencyCheckResult result)
		{
			result.ShouldBeReported = true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000389C File Offset: 0x00001A9C
		private static object GetActualValueOfExceptionalProperty(ExceptionInfo exceptionInfo, DifferencesBetweenBlobAndAttach difference)
		{
			if (difference <= DifferencesBetweenBlobAndAttach.HasAttachment)
			{
				if (difference <= DifferencesBetweenBlobAndAttach.AppointmentColor)
				{
					switch (difference)
					{
					case DifferencesBetweenBlobAndAttach.None:
					case DifferencesBetweenBlobAndAttach.StartTime | DifferencesBetweenBlobAndAttach.EndTime:
					case DifferencesBetweenBlobAndAttach.StartTime | DifferencesBetweenBlobAndAttach.Subject:
					case DifferencesBetweenBlobAndAttach.EndTime | DifferencesBetweenBlobAndAttach.Subject:
					case DifferencesBetweenBlobAndAttach.StartTime | DifferencesBetweenBlobAndAttach.EndTime | DifferencesBetweenBlobAndAttach.Subject:
						break;
					case DifferencesBetweenBlobAndAttach.StartTime:
						return exceptionInfo.StartTime;
					case DifferencesBetweenBlobAndAttach.EndTime:
						return exceptionInfo.EndTime;
					case DifferencesBetweenBlobAndAttach.Subject:
						return exceptionInfo.PropertyBag[ItemSchema.Subject];
					case DifferencesBetweenBlobAndAttach.Location:
						return exceptionInfo.PropertyBag[CalendarItemBaseSchema.Location];
					default:
						if (difference == DifferencesBetweenBlobAndAttach.AppointmentColor)
						{
							return exceptionInfo.PropertyBag[CalendarItemBaseSchema.AppointmentColor];
						}
						break;
					}
				}
				else
				{
					if (difference == DifferencesBetweenBlobAndAttach.IsAllDayEvent)
					{
						return exceptionInfo.PropertyBag[CalendarItemBaseSchema.MapiIsAllDayEvent];
					}
					if (difference == DifferencesBetweenBlobAndAttach.HasAttachment)
					{
						return exceptionInfo.PropertyBag[MessageItemSchema.MapiHasAttachment];
					}
				}
			}
			else if (difference <= DifferencesBetweenBlobAndAttach.ReminderIsSet)
			{
				if (difference == DifferencesBetweenBlobAndAttach.FreeBusyStatus)
				{
					return exceptionInfo.PropertyBag[CalendarItemBaseSchema.FreeBusyStatus];
				}
				if (difference == DifferencesBetweenBlobAndAttach.ReminderIsSet)
				{
					return exceptionInfo.PropertyBag[ItemSchema.ReminderIsSetInternal];
				}
			}
			else
			{
				if (difference == DifferencesBetweenBlobAndAttach.ReminderMinutesBeforeStartInternal)
				{
					return exceptionInfo.PropertyBag[ItemSchema.ReminderMinutesBeforeStartInternal];
				}
				if (difference == DifferencesBetweenBlobAndAttach.AppointmentState)
				{
					return exceptionInfo.PropertyBag[CalendarItemBaseSchema.AppointmentState];
				}
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000039E4 File Offset: 0x00001BE4
		private void DetectExceptionInconsistencies(ConsistencyCheckResult result, ExceptionInfo exceptionInfo)
		{
			if (exceptionInfo.BlobDifferences != DifferencesBetweenBlobAndAttach.None)
			{
				foreach (object obj in Enum.GetValues(typeof(DifferencesBetweenBlobAndAttach)))
				{
					DifferencesBetweenBlobAndAttach differencesBetweenBlobAndAttach = (DifferencesBetweenBlobAndAttach)obj;
					if ((exceptionInfo.BlobDifferences & differencesBetweenBlobAndAttach) == differencesBetweenBlobAndAttach)
					{
						this.FailCheck(result, exceptionInfo, differencesBetweenBlobAndAttach);
					}
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003A5C File Offset: 0x00001C5C
		private void DetectRecurrenceInfoInconsistencies(RecurrenceInfo recurrenceInfo, ConsistencyCheckResult result)
		{
			if (recurrenceInfo.Anomalies != AnomaliesFlags.None)
			{
				foreach (object obj in Enum.GetValues(typeof(AnomaliesFlags)))
				{
					AnomaliesFlags anomaly = (AnomaliesFlags)obj;
					this.CheckAnomaly(result, recurrenceInfo.Anomalies, anomaly);
				}
			}
			foreach (OccurrenceInfo occurrenceInfo in recurrenceInfo.ModifiedOccurrences)
			{
				if (occurrenceInfo is ExceptionInfo)
				{
					this.DetectExceptionInconsistencies(result, (ExceptionInfo)occurrenceInfo);
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003B20 File Offset: 0x00001D20
		private void RegisterStorageException(ConsistencyCheckResult result, LocalizedException exception)
		{
			result.Status = CheckStatusType.CheckError;
			result.AddInconsistency(base.Context, Inconsistency.CreateInstance(RoleType.Organizer, exception.ToString(), CalendarInconsistencyFlag.StorageException, base.Context));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003B48 File Offset: 0x00001D48
		private void CheckAnomaly(ConsistencyCheckResult result, AnomaliesFlags existingAnomalies, AnomaliesFlags anomaly)
		{
			if (anomaly != AnomaliesFlags.None && (existingAnomalies & anomaly) != AnomaliesFlags.None)
			{
				result.Status = CheckStatusType.Failed;
				result.AddInconsistency(base.Context, Inconsistency.CreateInstance(RoleType.Attendee, anomaly.ToString(), CalendarInconsistencyFlag.RecurrenceAnomaly, base.Context));
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003B82 File Offset: 0x00001D82
		private void FailCheck(ConsistencyCheckResult result, string description, RecurrenceInconsistencyType type, ExDateTime originalStartTime)
		{
			result.Status = CheckStatusType.Failed;
			result.AddInconsistency(base.Context, RecurrenceInconsistency.CreateInstance(RoleType.Attendee, description, CalendarInconsistencyFlag.RecurrenceBlob, type, originalStartTime, base.Context));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003BAC File Offset: 0x00001DAC
		private void FailCheck(ConsistencyCheckResult result, RoleType checkingRole, UserObject checkingUser, RecurrenceFormatException exception)
		{
			result.Status = CheckStatusType.Failed;
			string text = string.Format("RecurrenceBlobsConsistentCheck: RecurrenceFormatException for {0} ({1}), exception = {2}", checkingRole, checkingUser, exception.GetType());
			Globals.ConsistencyChecksTracer.TraceError((long)this.GetHashCode(), text);
			if (checkingRole == RoleType.Attendee)
			{
				result.AddInconsistency(base.Context, Inconsistency.CreateInstance(RoleType.Attendee, text, CalendarInconsistencyFlag.RecurrenceBlob, base.Context));
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003C0C File Offset: 0x00001E0C
		private void FailCheck(ConsistencyCheckResult result, ExceptionInfo exceptionInfo, DifferencesBetweenBlobAndAttach difference)
		{
			result.Status = CheckStatusType.Failed;
			result.AddInconsistency(base.Context, PropertyInconsistency.CreateInstance(RoleType.Attendee, CalendarInconsistencyFlag.RecurringException, string.Format("Exc{0}", difference), exceptionInfo.OriginalStartTime, RecurrenceBlobsConsistentCheck.GetActualValueOfExceptionalProperty(exceptionInfo, difference), base.Context));
		}

		// Token: 0x04000018 RID: 24
		internal const string CheckDescription = "Checks to make sure that the recurrence blobs are internally consistent.";
	}
}
