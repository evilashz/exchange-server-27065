using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConsistencyCheckFactory
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002963 File Offset: 0x00000B63
		private ConsistencyCheckFactory()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000296C File Offset: 0x00000B6C
		internal static ConsistencyCheckFactory Instance
		{
			get
			{
				ConsistencyCheckFactory result;
				lock (ConsistencyCheckFactory.threadSafetyLock)
				{
					if (ConsistencyCheckFactory.instance == null)
					{
						ConsistencyCheckFactory.instance = new ConsistencyCheckFactory();
					}
					result = ConsistencyCheckFactory.instance;
				}
				return result;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029C0 File Offset: 0x00000BC0
		internal PrimaryConsistencyCheckChain CreatePrimaryConsistencyCheckChain(CalendarValidationContext context, MeetingComparisonResult comparisonResult)
		{
			PrimaryConsistencyCheckChain primaryConsistencyCheckChain = new PrimaryConsistencyCheckChain(2, comparisonResult);
			primaryConsistencyCheckChain.AddCheck(new CanValidateOwnerCheck(context));
			primaryConsistencyCheckChain.AddCheck(new ValidateStoreObjectCheck(context));
			return primaryConsistencyCheckChain;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029F0 File Offset: 0x00000BF0
		internal ConsistencyCheckChain<ConsistencyCheckResult> CreateGeneralConsistencyCheckChain(CalendarValidationContext context, MeetingComparisonResult comparisonResult, bool validationOnly)
		{
			ConsistencyCheckChain<ConsistencyCheckResult> consistencyCheckChain = null;
			if (validationOnly || context.BaseItem.CalendarItemType != CalendarItemType.Occurrence || context.OppositeItem.CalendarItemType != CalendarItemType.Occurrence)
			{
				if (context.BaseItem.IsCancelled)
				{
					if (context.BaseRole == RoleType.Attendee)
					{
						if (validationOnly)
						{
							consistencyCheckChain = new ConsistencyCheckChain<ConsistencyCheckResult>(2, comparisonResult);
							consistencyCheckChain.AddCheck(new AttendeeOnListCheck(context));
							consistencyCheckChain.AddCheck(new MeetingCancellationCheck(context));
						}
						else
						{
							consistencyCheckChain = new ConsistencyCheckChain<ConsistencyCheckResult>(0, comparisonResult);
						}
					}
					else
					{
						consistencyCheckChain = new ConsistencyCheckChain<ConsistencyCheckResult>(1, comparisonResult);
						consistencyCheckChain.AddCheck(new MeetingCancellationCheck(context));
					}
				}
				else
				{
					consistencyCheckChain = new ConsistencyCheckChain<ConsistencyCheckResult>(comparisonResult);
					if (context.BaseRole == RoleType.Organizer)
					{
						consistencyCheckChain.AddCheck(new MeetingCancellationCheck(context));
					}
					if (context.BaseRole == RoleType.Attendee)
					{
						consistencyCheckChain.AddCheck(new AttendeeOnListCheck(context));
						if (context.BaseItem.GetValueOrDefault<bool>(ItemSchema.IsResponseRequested, true))
						{
							consistencyCheckChain.AddCheck(new CorrectResponseCheck(context));
						}
					}
					if (validationOnly || context.BaseRole == RoleType.Organizer)
					{
						consistencyCheckChain.AddCheck(new TimeZoneConsistentCheck(context));
						consistencyCheckChain.AddCheck(new MeetingPropertiesMatchCheck(context));
						if (context.BaseItem.CalendarItemType == CalendarItemType.RecurringMaster)
						{
							consistencyCheckChain.AddCheck(new RecurrenceBlobsConsistentCheck(context));
							consistencyCheckChain.AddCheck(new RecurrencesMatchCheck(context));
						}
						else if (context.OppositeItem.CalendarItemType == CalendarItemType.RecurringMaster)
						{
							consistencyCheckChain.AddCheck(new RecurrenceBlobsConsistentCheck(context));
						}
					}
				}
			}
			if (consistencyCheckChain == null)
			{
				consistencyCheckChain = new ConsistencyCheckChain<ConsistencyCheckResult>(0, comparisonResult);
			}
			return consistencyCheckChain;
		}

		// Token: 0x0400000C RID: 12
		private static object threadSafetyLock = new object();

		// Token: 0x0400000D RID: 13
		private static ConsistencyCheckFactory instance;
	}
}
