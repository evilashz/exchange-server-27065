using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200014C RID: 332
	internal class CalendarRepairAssistantLogEntry : AssistantLogEntryBase
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000522FA File Offset: 0x000504FA
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00052302 File Offset: 0x00050502
		[LogField("MG")]
		public Guid MailboxGuid { get; internal set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0005230B File Offset: 0x0005050B
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x00052313 File Offset: 0x00050513
		[LogField("TG")]
		public Guid TenantGuid { get; internal set; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0005231C File Offset: 0x0005051C
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00052324 File Offset: 0x00050524
		[LogField("DG")]
		public Guid DatabaseGuid { get; internal set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0005232D File Offset: 0x0005052D
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00052335 File Offset: 0x00050535
		[LogField("RST")]
		public string RangeStartTime { get; internal set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0005233E File Offset: 0x0005053E
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00052346 File Offset: 0x00050546
		[LogField("RET")]
		public string RangeEndTime { get; internal set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0005234F File Offset: 0x0005054F
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00052357 File Offset: 0x00050557
		[LogField("RM")]
		public string RepairMode { get; internal set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00052360 File Offset: 0x00050560
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00052368 File Offset: 0x00050568
		[LogField("TRT")]
		public string TotalProcessingTime { get; internal set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00052371 File Offset: 0x00050571
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00052379 File Offset: 0x00050579
		[LogField("TMP")]
		public int TotalMeetingsProcessed { get; private set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00052382 File Offset: 0x00050582
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0005238A File Offset: 0x0005058A
		[LogField("TMC")]
		public int TotalMeetingsConsistent { get; private set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00052393 File Offset: 0x00050593
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x0005239B File Offset: 0x0005059B
		[LogField("TMIC")]
		public int TotalMeetingsInconsistent { get; private set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000523A4 File Offset: 0x000505A4
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x000523AC File Offset: 0x000505AC
		[LogField("TMAO")]
		public int TotalMeetingsAsOrganizer { get; private set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x000523B5 File Offset: 0x000505B5
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x000523BD File Offset: 0x000505BD
		[LogField("TMPA")]
		public int TotalMeetingsParticipants { get; private set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000523C6 File Offset: 0x000505C6
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x000523CE File Offset: 0x000505CE
		[LogField("TDM")]
		public int TotalDuplicateMeetings { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x000523D7 File Offset: 0x000505D7
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x000523DF File Offset: 0x000505DF
		[LogField("TMD")]
		public int TotalMeetingDelegates { get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x000523E8 File Offset: 0x000505E8
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x000523F0 File Offset: 0x000505F0
		[LogField("TMSV")]
		public int TotalMeetingSucessfullyValidated { get; private set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x000523F9 File Offset: 0x000505F9
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00052401 File Offset: 0x00050601
		[LogField("TRG")]
		public string TotalRumsGenerated { get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0005240A File Offset: 0x0005060A
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00052412 File Offset: 0x00050612
		[LogField("TRSS")]
		public string TotalRumsSuccessfullySent { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0005241B File Offset: 0x0005061B
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00052423 File Offset: 0x00050623
		[LogField("TRS")]
		public int TotalRumsCount { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0005242C File Offset: 0x0005062C
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x00052434 File Offset: 0x00050634
		[LogField("CIT")]
		public string CalendarItemTypes { get; private set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0005243D File Offset: 0x0005063D
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x00052445 File Offset: 0x00050645
		[LogField("TCCF")]
		public string TotalConsistencyCheckFailures { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0005244E File Offset: 0x0005064E
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x00052456 File Offset: 0x00050656
		[LogField("TIC")]
		public string TotalInconsistencies { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0005245F File Offset: 0x0005065F
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x00052467 File Offset: 0x00050667
		[LogField("TGIC")]
		public string TotalGroupMailboxInconsistencies { get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00052470 File Offset: 0x00050670
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x00052478 File Offset: 0x00050678
		[LogField("US")]
		public string UpgradeStatus { get; set; }

		// Token: 0x06000D7D RID: 3453 RVA: 0x00052C74 File Offset: 0x00050E74
		internal void AddValidationResults(List<MeetingValidationResult> validationResults)
		{
			if (validationResults != null && validationResults.Count != 0)
			{
				this.TotalMeetingsProcessed = validationResults.Count;
				this.TotalMeetingsAsOrganizer = (from mvr in validationResults
				where mvr.IsOrganizer
				select mvr).Count<MeetingValidationResult>();
				this.TotalMeetingsParticipants = validationResults.Sum((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Count);
				this.TotalMeetingsConsistent = (from mvr in validationResults
				where mvr.IsConsistent
				select mvr).Count<MeetingValidationResult>();
				this.TotalMeetingsInconsistent = (from mvr in validationResults
				where !mvr.IsConsistent
				select mvr).Count<MeetingValidationResult>();
				this.TotalDuplicateMeetings = (from mvr in validationResults
				where mvr.DuplicatesDetected
				select mvr).Count<MeetingValidationResult>();
				this.TotalMeetingDelegates = validationResults.Sum((MeetingValidationResult mvr) => mvr.NumberOfDelegates) / validationResults.Count;
				this.TotalMeetingSucessfullyValidated = (from mvr in validationResults
				where mvr.WasValidationSuccessful
				select mvr).Count<MeetingValidationResult>();
				this.TotalRumsCount = validationResults.Sum((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.Sum((MeetingComparisonResult mcr) => mcr.RepairInfo.SendableRumsCount));
				this.TotalRumsGenerated = base.FormatDictionaryToString((from x in validationResults.SelectMany((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => mcr.RepairInfo.SendableRums))
				group x by x.Type into y
				select new
				{
					Item = y.Key.ToString(),
					Count = y.Count<RumInfo>()
				}).ToDictionary(z => z.Item, z => z.Count));
				this.TotalRumsSuccessfullySent = base.FormatDictionaryToString((from x in validationResults.SelectMany((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => from rumInfo in mcr.RepairInfo.SendableRums
				where rumInfo.IsSuccessfullySent
				select rumInfo))
				group x by x.Type into y
				select new
				{
					Item = y.Key.ToString(),
					Count = y.Count<RumInfo>()
				}).ToDictionary(z => z.Item, z => z.Count));
				this.CalendarItemTypes = base.FormatDictionaryToString((from mvr in validationResults
				group mvr by mvr.ItemType into x
				select new
				{
					Item = x.Key.ToString(),
					Count = x.Count<MeetingValidationResult>()
				}).ToDictionary(y => y.Item, y => y.Count));
				this.TotalConsistencyCheckFailures = base.FormatDictionaryToString((from x in validationResults.SelectMany((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => from cr in mcr.CheckResultList
				where cr.Status == CheckStatusType.Failed
				select cr))
				group x by x.CheckType into y
				select new
				{
					Item = y.Key.ToString(),
					Count = y.Count<ConsistencyCheckResult>()
				}).ToDictionary(z => z.Item, z => z.Count));
				this.TotalInconsistencies = base.FormatDictionaryToString((from x in validationResults.SelectMany((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => mcr.CheckResultList.SelectMany((ConsistencyCheckResult cr) => from i in cr.Inconsistencies
				where i.ShouldFix
				select i)))
				group x by new
				{
					x.Flag,
					x.Owner
				} into y
				select new
				{
					Item = y.Key.Flag.ToString(),
					Owner = y.Key.Owner.ToString(),
					Count = y.Count<Inconsistency>()
				}).ToDictionary(z => z.Item + "-" + z.Owner, z => z.Count));
				this.TotalGroupMailboxInconsistencies = base.FormatDictionaryToString((from x in validationResults.SelectMany((MeetingValidationResult mvr) => mvr.ResultsPerAttendee.Values.SelectMany((MeetingComparisonResult mcr) => mcr.CheckResultList.SelectMany((ConsistencyCheckResult cr) => from i in cr.Inconsistencies
				where i.ShouldFix && i.OwnerIsGroupMailbox
				select i)))
				group x by new
				{
					x.Flag,
					x.Owner
				} into y
				select new
				{
					Item = y.Key.Flag.ToString(),
					Owner = y.Key.Owner.ToString(),
					Count = y.Count<Inconsistency>()
				}).ToDictionary(z => z.Item + "-" + z.Owner, z => z.Count));
			}
		}
	}
}
