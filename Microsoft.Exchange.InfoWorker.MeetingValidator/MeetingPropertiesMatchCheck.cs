using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MeetingPropertiesMatchCheck : ConsistencyCheckBase<ConsistencyCheckResult>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002FE0 File Offset: 0x000011E0
		internal MeetingPropertiesMatchCheck(CalendarValidationContext context)
		{
			SeverityType severity = context.AreItemsOccurrences ? SeverityType.Warning : SeverityType.Error;
			this.Initialize(ConsistencyCheckType.MeetingPropertiesMatchCheck, "Checks to make sure that the attendee has the correct critical properties for the meeting.", severity, context, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003010 File Offset: 0x00001210
		protected override ConsistencyCheckResult DetectInconsistencies()
		{
			ConsistencyCheckResult result = ConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			ExDateTime startTime = base.Context.OrganizerItem.StartTime;
			ExDateTime endTime = base.Context.OrganizerItem.EndTime;
			string location = base.Context.OrganizerItem.Location;
			ExDateTime startTime2 = base.Context.AttendeeItem.StartTime;
			ExDateTime endTime2 = base.Context.AttendeeItem.EndTime;
			string location2 = base.Context.AttendeeItem.Location;
			int appointmentSequenceNumber = base.Context.OrganizerItem.AppointmentSequenceNumber;
			int appointmentLastSequenceNumber = base.Context.OrganizerItem.AppointmentLastSequenceNumber;
			ExDateTime ownerCriticalChangeTime = base.Context.OrganizerItem.OwnerCriticalChangeTime;
			ExDateTime attendeeCriticalChangeTime = base.Context.OrganizerItem.AttendeeCriticalChangeTime;
			int appointmentSequenceNumber2 = base.Context.AttendeeItem.AppointmentSequenceNumber;
			ExDateTime ownerCriticalChangeTime2 = base.Context.AttendeeItem.OwnerCriticalChangeTime;
			ExDateTime attendeeCriticalChangeTime2 = base.Context.AttendeeItem.AttendeeCriticalChangeTime;
			bool flag = false;
			if (appointmentSequenceNumber == appointmentSequenceNumber2 || (appointmentSequenceNumber != appointmentLastSequenceNumber && appointmentLastSequenceNumber >= appointmentSequenceNumber2))
			{
				flag = true;
			}
			else if (ExDateTime.Compare(ownerCriticalChangeTime, ownerCriticalChangeTime2, MeetingPropertiesMatchCheck.DateTimeComparisonTreshold) == 0)
			{
				flag = true;
			}
			else if (ExDateTime.Compare(attendeeCriticalChangeTime, attendeeCriticalChangeTime2, MeetingPropertiesMatchCheck.DateTimeComparisonTreshold) == 0)
			{
				flag = true;
			}
			else
			{
				this.FailCheck(result, CalendarInconsistencyFlag.VersionInfo, "SequenceNumber", appointmentSequenceNumber, appointmentSequenceNumber2);
				this.FailCheck(result, CalendarInconsistencyFlag.VersionInfo, "OwnerCriticalChangeTime", ownerCriticalChangeTime, ownerCriticalChangeTime2);
				this.FailCheck(result, CalendarInconsistencyFlag.VersionInfo, "AttendeeCriticalChangeTime", attendeeCriticalChangeTime, attendeeCriticalChangeTime2);
			}
			if (!flag)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (ExDateTime.Compare(ownerCriticalChangeTime, utcNow, TimeSpan.FromMinutes(120.0)) != 0)
				{
					this.FailCheck(result, CalendarInconsistencyFlag.VersionInfo, "DelayedUpdates", ownerCriticalChangeTime.ToUtc().ToString(), utcNow.ToUtc().ToString());
				}
			}
			bool flag2 = MeetingPropertiesMatchCheck.CheckForMeetingOverlapInconsistency(startTime, endTime, startTime2, endTime2);
			if (flag2)
			{
				this.FailCheck(result, CalendarInconsistencyFlag.TimeOverlap, "MeetingOverlap", (startTime - endTime).TotalMinutes, 0);
			}
			bool flag3 = false;
			this.CheckTimeConsistency(result, MeetingPropertiesMatchCheck.TimeProperty.StartTime, ref flag3);
			this.CheckTimeConsistency(result, MeetingPropertiesMatchCheck.TimeProperty.EndTime, ref flag3);
			if (location != null && location2 != null && !location2.Contains(location))
			{
				try
				{
					ClientIntentFlags? clientIntentFlags;
					if (base.Context.BaseRole == RoleType.Attendee)
					{
						ICalendarItemStateDefinition initialState = new LocationBasedCalendarItemStateDefinition(location);
						ICalendarItemStateDefinition targetState = new LocationBasedCalendarItemStateDefinition(location2);
						ClientIntentQuery clientIntentQuery = new TransitionalClientIntentQuery(base.Context.AttendeeItem.GlobalObjectId, initialState, targetState);
						clientIntentFlags = clientIntentQuery.Execute((MailboxSession)base.Context.AttendeeItem.Session, base.Context.CvsGateway).Intent;
					}
					else
					{
						clientIntentFlags = base.Context.CalendarInstance.GetLocationIntent(base.Context, base.Context.AttendeeItem.GlobalObjectId, location, location2);
					}
					if (!ClientIntentQuery.CheckDesiredClientIntent(clientIntentFlags, new ClientIntentFlags[]
					{
						ClientIntentFlags.ModifiedLocation
					}))
					{
						this.FailCheck(result, CalendarInconsistencyFlag.Location, "Location", location, location2, clientIntentFlags);
					}
				}
				catch (CalendarVersionStoreNotPopulatedException exc)
				{
					this.FailCheck(result, Inconsistency.CreateMissingCvsInconsistency(RoleType.Attendee, exc, base.Context));
				}
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003368 File Offset: 0x00001568
		protected override void ProcessResult(ConsistencyCheckResult result)
		{
			result.ShouldBeReported = true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003374 File Offset: 0x00001574
		private static bool CheckForMeetingOverlapInconsistency(ExDateTime organizerStartTime, ExDateTime organizerEndTime, ExDateTime attendeeStartTime, ExDateTime attendeeEndTime)
		{
			bool result;
			if (organizerStartTime.Equals(organizerEndTime))
			{
				result = (attendeeEndTime < organizerStartTime || attendeeStartTime > organizerEndTime);
			}
			else if (attendeeStartTime < organizerStartTime)
			{
				result = (attendeeEndTime <= organizerStartTime);
			}
			else
			{
				result = (attendeeStartTime >= organizerEndTime && attendeeEndTime > organizerEndTime);
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000033CC File Offset: 0x000015CC
		private void CheckTimeConsistency(ConsistencyCheckResult result, MeetingPropertiesMatchCheck.TimeProperty propertyToCheck, ref bool timeZoneCheckFailed)
		{
			ExDateTime exDateTime;
			ExDateTime exDateTime2;
			switch (propertyToCheck)
			{
			case MeetingPropertiesMatchCheck.TimeProperty.StartTime:
				exDateTime = base.Context.OrganizerItem.StartTime;
				exDateTime2 = base.Context.AttendeeItem.StartTime;
				break;
			case MeetingPropertiesMatchCheck.TimeProperty.EndTime:
				exDateTime = base.Context.OrganizerItem.EndTime;
				exDateTime2 = base.Context.AttendeeItem.EndTime;
				break;
			default:
				throw new ArgumentException(string.Format("Time property ({0}) is not valid for consistency check.", propertyToCheck));
			}
			if (!exDateTime.Equals(exDateTime2))
			{
				if (Math.Abs(exDateTime.Subtract(exDateTime2).TotalMinutes) > 120.0)
				{
					this.FailCheck(result, CalendarInconsistencyFlag.StartTime, propertyToCheck.ToString(), exDateTime, exDateTime2);
					return;
				}
				if (!timeZoneCheckFailed)
				{
					REG_TIMEZONE_INFO? effectiveTimeZoneRule = this.GetEffectiveTimeZoneRule(base.Context.OrganizerItem);
					if (effectiveTimeZoneRule != null)
					{
						REG_TIMEZONE_INFO? effectiveTimeZoneRule2 = this.GetEffectiveTimeZoneRule(base.Context.AttendeeItem);
						if (effectiveTimeZoneRule2 == null || !effectiveTimeZoneRule2.Equals(effectiveTimeZoneRule))
						{
							this.FailCheck(result, CalendarInconsistencyFlag.StartTimeZone, propertyToCheck.ToString(), exDateTime, exDateTime2);
							timeZoneCheckFailed = true;
						}
					}
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000350C File Offset: 0x0000170C
		private REG_TIMEZONE_INFO? GetEffectiveTimeZoneRule(CalendarItemBase item)
		{
			REG_TIMEZONE_INFO? result = null;
			byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(ItemSchema.TimeZoneDefinitionStart);
			if (valueOrDefault != null)
			{
				ExTimeZone timeZone = null;
				if (O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out timeZone))
				{
					result = new REG_TIMEZONE_INFO?(TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(timeZone, item.StartTime));
				}
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003558 File Offset: 0x00001758
		private void FailCheck(ConsistencyCheckResult result, CalendarInconsistencyFlag inconsistencyFlag, string propertyName, object expectedValue, object actualValue)
		{
			this.FailCheck(result, inconsistencyFlag, propertyName, expectedValue, actualValue, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000357C File Offset: 0x0000177C
		private void FailCheck(ConsistencyCheckResult result, CalendarInconsistencyFlag inconsistencyFlag, string propertyName, object expectedValue, object actualValue, ClientIntentFlags? inconsistencyIntent)
		{
			PropertyInconsistency propertyInconsistency = PropertyInconsistency.CreateInstance(RoleType.Attendee, inconsistencyFlag, propertyName, expectedValue, actualValue, base.Context);
			propertyInconsistency.Intent = inconsistencyIntent;
			this.FailCheck(result, propertyInconsistency);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000035AC File Offset: 0x000017AC
		private void FailCheck(ConsistencyCheckResult result, Inconsistency inconsistency)
		{
			result.Status = CheckStatusType.Failed;
			result.AddInconsistency(base.Context, inconsistency);
		}

		// Token: 0x04000011 RID: 17
		internal const string CheckDescription = "Checks to make sure that the attendee has the correct critical properties for the meeting.";

		// Token: 0x04000012 RID: 18
		private const double MaxTravelTimeThresholdInMinutes = 120.0;

		// Token: 0x04000013 RID: 19
		private static readonly TimeSpan DateTimeComparisonTreshold = TimeSpan.FromSeconds(1.0);

		// Token: 0x0200000B RID: 11
		private enum TimeProperty
		{
			// Token: 0x04000015 RID: 21
			StartTime,
			// Token: 0x04000016 RID: 22
			EndTime
		}
	}
}
