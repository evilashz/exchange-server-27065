using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MeetingExistenceCheck : ConsistencyCheckBase<MeetingExistenceConsistencyCheckResult>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002C34 File Offset: 0x00000E34
		internal MeetingExistenceCheck(CalendarValidationContext context)
		{
			SeverityType severity = (context.BaseItem != null && !context.BaseItem.IsCancelled) ? SeverityType.FatalError : SeverityType.Information;
			this.Initialize(ConsistencyCheckType.MeetingExistenceCheck, "Checkes whether the item can be found in the owner's calendar or not.", severity, context, null);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C70 File Offset: 0x00000E70
		private UserObject UserToCheck
		{
			get
			{
				if (base.Context.OppositeRole != RoleType.Organizer)
				{
					return base.Context.Attendee;
				}
				return base.Context.Organizer;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C96 File Offset: 0x00000E96
		private bool FailOnAbsence
		{
			get
			{
				return base.Context.BaseRole == RoleType.Attendee || base.Context.Attendee.ResponseType != ResponseType.Decline;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CC0 File Offset: 0x00000EC0
		protected override MeetingExistenceConsistencyCheckResult DetectInconsistencies()
		{
			MeetingExistenceConsistencyCheckResult meetingExistenceConsistencyCheckResult = MeetingExistenceConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			meetingExistenceConsistencyCheckResult.Status = CheckStatusType.CheckError;
			if (base.Context.BaseItem != null)
			{
				try
				{
					if (this.CheckFoundItem(null, base.Context.OppositeItem, meetingExistenceConsistencyCheckResult, base.Context.ErrorString) && (meetingExistenceConsistencyCheckResult.ItemIsFound || !this.FailOnAbsence))
					{
						meetingExistenceConsistencyCheckResult.Status = CheckStatusType.Passed;
					}
				}
				catch (ArgumentException ex)
				{
					Globals.ConsistencyChecksTracer.TraceError((long)this.GetHashCode(), "{0}: Could not open item store session or calendar for {1} ({2}), exception = {3}", new object[]
					{
						base.Type,
						base.Context.OppositeRole,
						this.UserToCheck,
						ex
					});
				}
			}
			return meetingExistenceConsistencyCheckResult;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D90 File Offset: 0x00000F90
		private bool CheckFoundItem(CalendarFolder folder, CalendarItemBase item, MeetingExistenceConsistencyCheckResult result, string itemQueryError)
		{
			bool flag = false;
			if (item != null)
			{
				CalendarItemType calendarItemType = base.Context.BaseItem.CalendarItemType;
				CalendarItemType calendarItemType2 = item.CalendarItemType;
				if ((calendarItemType == CalendarItemType.Single || calendarItemType == CalendarItemType.RecurringMaster) && (calendarItemType2 == CalendarItemType.Occurrence || calendarItemType2 == CalendarItemType.Exception))
				{
					result.ItemIsFound = false;
					string description = string.Format("Item type mismatch. [BaseType|BaseGoid - FoundType|FoundGoid] ({0}|{1} - {2}|{3})", new object[]
					{
						calendarItemType,
						base.Context.BaseItem.GlobalObjectId,
						calendarItemType2,
						item.GlobalObjectId
					});
					this.FailCheck(result, this.GetInconsistencyFullDescription(description, itemQueryError));
					flag = true;
				}
				else
				{
					result.ItemIsFound = true;
				}
			}
			else
			{
				result.ItemIsFound = false;
				if (this.FailOnAbsence)
				{
					string description2 = string.Format("Could not find the matching meeting in {0}'s calendar.", base.Context.OppositeRole.ToString().ToLower());
					try
					{
						string inconsistencyFullDescription = this.GetInconsistencyFullDescription(description2, itemQueryError);
						this.FailCheck(result, base.Context.CalendarInstance.GetInconsistency(base.Context, inconsistencyFullDescription));
					}
					catch (CalendarVersionStoreNotPopulatedException ex)
					{
						this.FailCheck(result, Inconsistency.CreateInstance(base.Context.OppositeRole, string.Format("The Calendar Version Store is not fully populated yet (Wait Time: {0}).", ex.WaitTimeBeforeThrow), CalendarInconsistencyFlag.MissingCvs, base.Context));
					}
					flag = true;
				}
			}
			return !flag;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002EF4 File Offset: 0x000010F4
		private string GetInconsistencyFullDescription(string description, string errorString)
		{
			return string.Format("{0} Error: {1}", description, errorString);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F02 File Offset: 0x00001102
		private void FailCheck(MeetingExistenceConsistencyCheckResult result, Inconsistency inconsistency)
		{
			result.Status = CheckStatusType.Failed;
			if (inconsistency != null)
			{
				result.AddInconsistency(base.Context, inconsistency);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002F1B File Offset: 0x0000111B
		private void FailCheck(MeetingExistenceConsistencyCheckResult result, string errorString)
		{
			result.Status = CheckStatusType.Failed;
			result.ErrorString = errorString;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F2C File Offset: 0x0000112C
		protected override void ProcessResult(MeetingExistenceConsistencyCheckResult result)
		{
			if (base.Context.CalendarInstance == null)
			{
				result.ShouldBeReported = false;
				return;
			}
			if (result.Status == CheckStatusType.Passed)
			{
				result.ShouldBeReported = true;
				return;
			}
			if (base.Context.BaseItem.IsCancelled)
			{
				result.ShouldBeReported = false;
				return;
			}
			if (base.Context.OppositeRole == RoleType.Organizer)
			{
				result.ShouldBeReported = true;
				return;
			}
			if (!base.Context.OrganizerItem.GetValueOrDefault<bool>(ItemSchema.IsResponseRequested, true))
			{
				result.ShouldBeReported = false;
				return;
			}
			result.ShouldBeReported = (base.Context.Attendee.ResponseType == ResponseType.Accept || base.Context.Attendee.ResponseType == ResponseType.Tentative);
		}

		// Token: 0x04000010 RID: 16
		internal const string CheckDescription = "Checkes whether the item can be found in the owner's calendar or not.";
	}
}
