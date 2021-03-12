using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ValidateStoreObjectCheck : ConsistencyCheckBase<PrimaryConsistencyCheckResult>
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000415E File Offset: 0x0000235E
		internal ValidateStoreObjectCheck(CalendarValidationContext context)
		{
			this.Initialize(ConsistencyCheckType.ValidateStoreObjectCheck, "Calls Validate() on the base calendar item.", SeverityType.Error, context, null);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004178 File Offset: 0x00002378
		protected override PrimaryConsistencyCheckResult DetectInconsistencies()
		{
			PrimaryConsistencyCheckResult result;
			try
			{
				StoreObjectValidationError[] errorList;
				switch (base.Context.BaseRole)
				{
				case RoleType.Organizer:
					errorList = base.Context.OrganizerItem.Validate();
					break;
				case RoleType.Attendee:
					errorList = base.Context.AttendeeItem.Validate();
					break;
				default:
					errorList = new StoreObjectValidationError[0];
					break;
				}
				result = this.GetResult(errorList);
			}
			catch (ObjectValidationException ex)
			{
				result = this.GetResult(ex.Errors);
				result.Status = CheckStatusType.Failed;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004200 File Offset: 0x00002400
		protected override void ProcessResult(PrimaryConsistencyCheckResult result)
		{
			result.ShouldBeReported = true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000420C File Offset: 0x0000240C
		private PrimaryConsistencyCheckResult GetResult(ICollection<StoreObjectValidationError> errorList)
		{
			PrimaryConsistencyCheckResult primaryConsistencyCheckResult = PrimaryConsistencyCheckResult.CreateInstance(base.Type, base.Description, true);
			if (errorList != null && errorList.Count != 0)
			{
				primaryConsistencyCheckResult.Status = CheckStatusType.Failed;
				foreach (StoreObjectValidationError storeObjectValidationError in errorList)
				{
					primaryConsistencyCheckResult.AddInconsistency(base.Context, Inconsistency.CreateInstance(base.Context.BaseRole, storeObjectValidationError.ToString(), CalendarInconsistencyFlag.StoreObjectValidation, base.Context));
				}
			}
			return primaryConsistencyCheckResult;
		}

		// Token: 0x0400001B RID: 27
		internal const string CheckDescription = "Calls Validate() on the base calendar item.";
	}
}
