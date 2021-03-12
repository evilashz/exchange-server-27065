using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200003B RID: 59
	internal class MeetingComparer
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000C5A2 File Offset: 0x0000A7A2
		internal MeetingComparer(CalendarValidationContext context)
		{
			this.NumberOfSuccessfulRepairAttempts = 0L;
			this.context = context;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000C5B9 File Offset: 0x0000A7B9
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		internal long NumberOfSuccessfulRepairAttempts { get; private set; }

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		internal void Run(MeetingComparisonResult comparisonResult, ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent)
		{
			if (!this.SkipItem(ref organizerRumsSent))
			{
				PrimaryConsistencyCheckChain primaryConsistencyCheckChain = ConsistencyCheckFactory.Instance.CreatePrimaryConsistencyCheckChain(this.context, comparisonResult);
				primaryConsistencyCheckChain.PerformChecks(this.context);
				if (primaryConsistencyCheckChain.ShouldTerminate)
				{
					if (RumFactory.Instance.Policy.RepairMode == CalendarRepairType.ValidateOnly && this.context.BaseItem.CalendarItemType == CalendarItemType.RecurringMaster)
					{
						RecurrenceBlobsConsistentCheck recurrenceBlobsConsistentCheck = new RecurrenceBlobsConsistentCheck(this.context);
						ConsistencyCheckResult consistencyCheckResult = recurrenceBlobsConsistentCheck.Run();
						if (consistencyCheckResult.ShouldBeReported)
						{
							comparisonResult.AddCheckResult(consistencyCheckResult);
						}
					}
				}
				else
				{
					ConsistencyCheckChain<ConsistencyCheckResult> consistencyCheckChain = ConsistencyCheckFactory.Instance.CreateGeneralConsistencyCheckChain(this.context, comparisonResult, RumFactory.Instance.Policy.RepairMode == CalendarRepairType.ValidateOnly);
					consistencyCheckChain.PerformChecks();
				}
				if (RumFactory.Instance.Policy.RepairMode == CalendarRepairType.RepairAndValidate && comparisonResult.RepairInfo.SendRums(this.context.BaseItem, ref organizerRumsSent))
				{
					this.NumberOfSuccessfulRepairAttempts += 1L;
				}
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		private bool SkipItem(ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent)
		{
			bool result;
			if (organizerRumsSent == null)
			{
				organizerRumsSent = new Dictionary<GlobalObjectId, List<Attendee>>();
				this.context.HasSentUpdateForItemOrMaster = false;
				result = false;
			}
			else if (this.context.BaseRole == RoleType.Organizer && RumFactory.Instance.Policy.RepairMode == CalendarRepairType.RepairAndValidate && organizerRumsSent.Count != 0)
			{
				if (this.HasSentUpdateForItem(this.context.OrganizerItem.GlobalObjectId, this.context.Attendee, organizerRumsSent))
				{
					this.context.HasSentUpdateForItemOrMaster = true;
					result = true;
				}
				else if (this.context.OrganizerItem.CalendarItemType == CalendarItemType.Occurrence || this.context.OrganizerItem.CalendarItemType == CalendarItemType.Exception)
				{
					this.context.HasSentUpdateForItemOrMaster = this.HasSentUpdateForMaster(this.context.OrganizerItem, this.context.Attendee, organizerRumsSent);
					result = (this.context.HasSentUpdateForItemOrMaster && this.context.OrganizerItem.CalendarItemType == CalendarItemType.Exception);
				}
				else
				{
					this.context.HasSentUpdateForItemOrMaster = false;
					result = false;
				}
			}
			else
			{
				this.context.HasSentUpdateForItemOrMaster = false;
				result = false;
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		private bool HasSentUpdateForMaster(CalendarItemBase organizerItem, UserObject attendee, Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent)
		{
			return this.HasSentUpdateForItem(new GlobalObjectId(organizerItem.CleanGlobalObjectId), attendee, organizerRumsSent);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		private bool HasSentUpdateForItem(GlobalObjectId goid, UserObject attendee, Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent)
		{
			bool result = false;
			if (organizerRumsSent.ContainsKey(goid) && RumAgent.Instance.AttendeeListContainsParticipant(organizerRumsSent[goid], attendee.Participant))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x04000146 RID: 326
		private CalendarValidationContext context;
	}
}
