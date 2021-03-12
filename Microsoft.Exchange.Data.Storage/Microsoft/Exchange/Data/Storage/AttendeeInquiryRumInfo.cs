using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000407 RID: 1031
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttendeeInquiryRumInfo : AttendeeRumInfo
	{
		// Token: 0x06002EE3 RID: 12003 RVA: 0x000C10CC File Offset: 0x000BF2CC
		private AttendeeInquiryRumInfo(bool wouldRepair, MeetingInquiryAction predictedRepairAction) : this(null, wouldRepair, predictedRepairAction)
		{
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000C10EA File Offset: 0x000BF2EA
		private AttendeeInquiryRumInfo(ExDateTime? originalStartTime, bool wouldRepair, MeetingInquiryAction predictedRepairAction) : base(RumType.Inquiry, originalStartTime)
		{
			this.WouldRepair = wouldRepair;
			this.PredictedRepairAction = predictedRepairAction;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000C1102 File Offset: 0x000BF302
		public static AttendeeInquiryRumInfo CreateMasterInstance(bool wouldRepair, MeetingInquiryAction predictedRepairAction)
		{
			EnumValidator<MeetingInquiryAction>.ThrowIfInvalid(predictedRepairAction, "predictedRepairAction");
			return new AttendeeInquiryRumInfo(wouldRepair, predictedRepairAction);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000C1116 File Offset: 0x000BF316
		public static AttendeeInquiryRumInfo CreateOccurrenceInstance(ExDateTime originalStartTime, bool wouldRepair, MeetingInquiryAction predictedRepairAction)
		{
			EnumValidator<MeetingInquiryAction>.ThrowIfInvalid(predictedRepairAction, "predictedRepairAction");
			return new AttendeeInquiryRumInfo(new ExDateTime?(originalStartTime), wouldRepair, predictedRepairAction);
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000C1130 File Offset: 0x000BF330
		// (set) Token: 0x06002EE8 RID: 12008 RVA: 0x000C1138 File Offset: 0x000BF338
		public bool WouldRepair { get; private set; }

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000C1141 File Offset: 0x000BF341
		// (set) Token: 0x06002EEA RID: 12010 RVA: 0x000C1149 File Offset: 0x000BF349
		public MeetingInquiryAction PredictedRepairAction { get; private set; }
	}
}
