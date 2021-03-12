using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C87 RID: 3207
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MeetingResponseSchema : MeetingMessageInstanceSchema
	{
		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06007051 RID: 28753 RVA: 0x001F1982 File Offset: 0x001EFB82
		public new static MeetingResponseSchema Instance
		{
			get
			{
				if (MeetingResponseSchema.instance == null)
				{
					MeetingResponseSchema.instance = new MeetingResponseSchema();
				}
				return MeetingResponseSchema.instance;
			}
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x001F199A File Offset: 0x001EFB9A
		protected override void AddConstraints(List<StoreObjectConstraint> constraints)
		{
			base.AddConstraints(constraints);
			constraints.Add(MeetingResponseSchema.StartTimeMustBeLessThanOrEqualToEndTimeConstraint);
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x001F19AE File Offset: 0x001EFBAE
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			base.CoreObjectUpdate(coreItem, operation);
			MeetingResponse.CoreObjectUpdateIsSilent(coreItem);
		}

		// Token: 0x04004D76 RID: 19830
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentCounterProposal = InternalSchema.AppointmentCounterProposal;

		// Token: 0x04004D77 RID: 19831
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentCounterStartWhole = InternalSchema.AppointmentCounterStartWhole;

		// Token: 0x04004D78 RID: 19832
		[Autoload]
		public static readonly StorePropertyDefinition AppointmentCounterEndWhole = InternalSchema.AppointmentCounterEndWhole;

		// Token: 0x04004D79 RID: 19833
		public static readonly StorePropertyDefinition AppointmentProposedDuration = InternalSchema.AppointmentProposedDuration;

		// Token: 0x04004D7A RID: 19834
		[Autoload]
		public static readonly StorePropertyDefinition ResponseType = InternalSchema.MeetingMessageResponseType;

		// Token: 0x04004D7B RID: 19835
		private static MeetingResponseSchema instance = null;

		// Token: 0x04004D7C RID: 19836
		private static readonly PropertyComparisonConstraint StartTimeMustBeLessThanOrEqualToEndTimeConstraint = new PropertyComparisonConstraint(InternalSchema.AppointmentCounterStartWhole, InternalSchema.AppointmentCounterEndWhole, ComparisonOperator.LessThanOrEqual);
	}
}
