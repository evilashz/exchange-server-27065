using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CalendarInstanceContext : IDisposable
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000598C File Offset: 0x00003B8C
		internal CalendarInstanceContext(MeetingValidationResult validationResult, CalendarValidationContext validationContext)
		{
			if (validationResult == null)
			{
				throw new ArgumentNullException("validationResult");
			}
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			this.ValidationResult = validationResult;
			this.ValidationContext = validationContext;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000059BE File Offset: 0x00003BBE
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000059C6 File Offset: 0x00003BC6
		internal bool IsValidationDone { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000059CF File Offset: 0x00003BCF
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x000059D7 File Offset: 0x00003BD7
		internal MeetingValidationResult ValidationResult { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000059E0 File Offset: 0x00003BE0
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000059E8 File Offset: 0x00003BE8
		internal CalendarValidationContext ValidationContext { get; private set; }

		// Token: 0x060000F3 RID: 243 RVA: 0x000059F1 File Offset: 0x00003BF1
		public void Dispose()
		{
			if (this.ValidationContext != null)
			{
				this.ValidationContext.Dispose();
				this.ValidationContext = null;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A10 File Offset: 0x00003C10
		private static MeetingComparisonResult GetResultPerAttendee(MeetingValidationResult validationResult, UserObject user)
		{
			MeetingComparisonResult meetingComparisonResult = null;
			string key = user.EmailAddress.ToLower();
			if (!validationResult.ResultsPerAttendee.TryGetValue(key, out meetingComparisonResult))
			{
				meetingComparisonResult = MeetingComparisonResult.CreateInstance(user, validationResult.MeetingData);
				validationResult.ResultsPerAttendee.Add(key, meetingComparisonResult);
			}
			return meetingComparisonResult;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A58 File Offset: 0x00003C58
		internal void Validate(Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent, List<GlobalObjectId> inquiredMasterGoids, Action<long> onItemRepaired)
		{
			MeetingComparisonResult resultPerAttendee = CalendarInstanceContext.GetResultPerAttendee(this.ValidationResult, this.ValidationContext.Attendee);
			MeetingComparer meetingComparer = new MeetingComparer(this.ValidationContext);
			meetingComparer.Run(resultPerAttendee, ref organizerRumsSent);
			if (resultPerAttendee.InquiredMeeting && this.ValidationContext.BaseItem.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				inquiredMasterGoids.Add(this.ValidationContext.BaseItem.GlobalObjectId);
			}
			if (onItemRepaired != null)
			{
				onItemRepaired(meetingComparer.NumberOfSuccessfulRepairAttempts);
			}
			foreach (ConsistencyCheckResult consistencyCheckResult in resultPerAttendee)
			{
				if (consistencyCheckResult.Status != CheckStatusType.Passed)
				{
					this.ValidationResult.IsConsistent = false;
					break;
				}
			}
			this.IsValidationDone = true;
			this.ValidationResult.WasValidationSuccessful = true;
		}
	}
}
