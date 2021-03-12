using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000068 RID: 104
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IInferenceTrainingConfigurationSettings_Implementation_ : IInferenceTrainingConfigurationSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00005418 File Offset: 0x00003618
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000541B File Offset: 0x0000361B
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000541E File Offset: 0x0000361E
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00005426 File Offset: 0x00003626
		public bool IsLoggingEnabled
		{
			get
			{
				return this._IsLoggingEnabled_MaterializedValue_;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000542E File Offset: 0x0000362E
		public string LogPath
		{
			get
			{
				return this._LogPath_MaterializedValue_;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00005436 File Offset: 0x00003636
		public int MaxLogAgeInDays
		{
			get
			{
				return this._MaxLogAgeInDays_MaterializedValue_;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000543E File Offset: 0x0000363E
		public ulong MaxLogDirectorySizeInMB
		{
			get
			{
				return this._MaxLogDirectorySizeInMB_MaterializedValue_;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00005446 File Offset: 0x00003646
		public ulong MaxLogFileSizeInMB
		{
			get
			{
				return this._MaxLogFileSizeInMB_MaterializedValue_;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000544E File Offset: 0x0000364E
		public int MinNumberOfItemsForRetrospectiveTraining
		{
			get
			{
				return this._MinNumberOfItemsForRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00005456 File Offset: 0x00003656
		public int MaxNumberOfCyclesForRetrospectiveTraining
		{
			get
			{
				return this._MaxNumberOfCyclesForRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000545E File Offset: 0x0000365E
		public int MaxActionHistorySize
		{
			get
			{
				return this._MaxActionHistorySize_MaterializedValue_;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00005466 File Offset: 0x00003666
		public int TargetPrecisionForThresholdAnalysis
		{
			get
			{
				return this._TargetPrecisionForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000546E File Offset: 0x0000366E
		public int TargetRecallForThresholdAnalysis
		{
			get
			{
				return this._TargetRecallForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00005476 File Offset: 0x00003676
		public int TargetFalsePositiveRateForThresholdAnalysis
		{
			get
			{
				return this._TargetFalsePositiveRateForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000547E File Offset: 0x0000367E
		public int ConfidenceThresholdForThresholdAnalysis
		{
			get
			{
				return this._ConfidenceThresholdForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00005486 File Offset: 0x00003686
		public int CoefficientForUserValueComputation
		{
			get
			{
				return this._CoefficientForUserValueComputation_MaterializedValue_;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000548E File Offset: 0x0000368E
		public int NumberOfHistoryDaysForThresholdComputation
		{
			get
			{
				return this._NumberOfHistoryDaysForThresholdComputation_MaterializedValue_;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005496 File Offset: 0x00003696
		public int ActionShareExponentForScaleFactorComputation
		{
			get
			{
				return this._ActionShareExponentForScaleFactorComputation_MaterializedValue_;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000549E File Offset: 0x0000369E
		public int MinPrecisionForInvitation
		{
			get
			{
				return this._MinPrecisionForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000054A6 File Offset: 0x000036A6
		public int MinRecallForInvitation
		{
			get
			{
				return this._MinRecallForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000054AE File Offset: 0x000036AE
		public int MaxFalsePositiveRateForInvitation
		{
			get
			{
				return this._MaxFalsePositiveRateForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000054B6 File Offset: 0x000036B6
		public int ConfidenceThresholdForInvitation
		{
			get
			{
				return this._ConfidenceThresholdForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000054BE File Offset: 0x000036BE
		public int MinClutterPerDayForInvitation
		{
			get
			{
				return this._MinClutterPerDayForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000054C6 File Offset: 0x000036C6
		public int MinNonClutterPerDayForInvitation
		{
			get
			{
				return this._MinNonClutterPerDayForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000054CE File Offset: 0x000036CE
		public int NumberOfHistoryDaysForInvitationPerDayAverages
		{
			get
			{
				return this._NumberOfHistoryDaysForInvitationPerDayAverages_MaterializedValue_;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000054D6 File Offset: 0x000036D6
		public int MinPrecisionForAutoEnablement
		{
			get
			{
				return this._MinPrecisionForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000054DE File Offset: 0x000036DE
		public int MinRecallForAutoEnablement
		{
			get
			{
				return this._MinRecallForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000054E6 File Offset: 0x000036E6
		public int MaxFalsePositiveRateForAutoEnablement
		{
			get
			{
				return this._MaxFalsePositiveRateForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000054EE File Offset: 0x000036EE
		public int ConfidenceThresholdForAutoEnablement
		{
			get
			{
				return this._ConfidenceThresholdForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000054F6 File Offset: 0x000036F6
		public int MinClutterPerDayForAutoEnablement
		{
			get
			{
				return this._MinClutterPerDayForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000054FE File Offset: 0x000036FE
		public int MinNonClutterPerDayForAutoEnablement
		{
			get
			{
				return this._MinNonClutterPerDayForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00005506 File Offset: 0x00003706
		public int NumberOfHistoryDaysForAutoEnablementPerDayAverages
		{
			get
			{
				return this._NumberOfHistoryDaysForAutoEnablementPerDayAverages_MaterializedValue_;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000550E File Offset: 0x0000370E
		public bool IsModelHistoryEnabled
		{
			get
			{
				return this._IsModelHistoryEnabled_MaterializedValue_;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00005516 File Offset: 0x00003716
		public int NumberOfModelHistoryCopiesToKeep
		{
			get
			{
				return this._NumberOfModelHistoryCopiesToKeep_MaterializedValue_;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000551E File Offset: 0x0000371E
		public bool IsMultiStepTrainingEnabled
		{
			get
			{
				return this._IsMultiStepTrainingEnabled_MaterializedValue_;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00005526 File Offset: 0x00003726
		public int VacationDetectionMinActivityCountThreshold
		{
			get
			{
				return this._VacationDetectionMinActivityCountThreshold_MaterializedValue_;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000552E File Offset: 0x0000372E
		public double VacationDetectionActivityCountNumStandardDeviations
		{
			get
			{
				return this._VacationDetectionActivityCountNumStandardDeviations_MaterializedValue_;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00005536 File Offset: 0x00003736
		public int VacationDetectionMinimumConsecutiveDays
		{
			get
			{
				return this._VacationDetectionMinimumConsecutiveDays_MaterializedValue_;
			}
		}

		// Token: 0x040001B7 RID: 439
		internal string _Name_MaterializedValue_;

		// Token: 0x040001B8 RID: 440
		internal bool _IsLoggingEnabled_MaterializedValue_;

		// Token: 0x040001B9 RID: 441
		internal string _LogPath_MaterializedValue_;

		// Token: 0x040001BA RID: 442
		internal int _MaxLogAgeInDays_MaterializedValue_;

		// Token: 0x040001BB RID: 443
		internal ulong _MaxLogDirectorySizeInMB_MaterializedValue_;

		// Token: 0x040001BC RID: 444
		internal ulong _MaxLogFileSizeInMB_MaterializedValue_;

		// Token: 0x040001BD RID: 445
		internal int _MinNumberOfItemsForRetrospectiveTraining_MaterializedValue_;

		// Token: 0x040001BE RID: 446
		internal int _MaxNumberOfCyclesForRetrospectiveTraining_MaterializedValue_;

		// Token: 0x040001BF RID: 447
		internal int _MaxActionHistorySize_MaterializedValue_;

		// Token: 0x040001C0 RID: 448
		internal int _TargetPrecisionForThresholdAnalysis_MaterializedValue_;

		// Token: 0x040001C1 RID: 449
		internal int _TargetRecallForThresholdAnalysis_MaterializedValue_;

		// Token: 0x040001C2 RID: 450
		internal int _TargetFalsePositiveRateForThresholdAnalysis_MaterializedValue_;

		// Token: 0x040001C3 RID: 451
		internal int _ConfidenceThresholdForThresholdAnalysis_MaterializedValue_;

		// Token: 0x040001C4 RID: 452
		internal int _CoefficientForUserValueComputation_MaterializedValue_;

		// Token: 0x040001C5 RID: 453
		internal int _NumberOfHistoryDaysForThresholdComputation_MaterializedValue_;

		// Token: 0x040001C6 RID: 454
		internal int _ActionShareExponentForScaleFactorComputation_MaterializedValue_;

		// Token: 0x040001C7 RID: 455
		internal int _MinPrecisionForInvitation_MaterializedValue_;

		// Token: 0x040001C8 RID: 456
		internal int _MinRecallForInvitation_MaterializedValue_;

		// Token: 0x040001C9 RID: 457
		internal int _MaxFalsePositiveRateForInvitation_MaterializedValue_;

		// Token: 0x040001CA RID: 458
		internal int _ConfidenceThresholdForInvitation_MaterializedValue_;

		// Token: 0x040001CB RID: 459
		internal int _MinClutterPerDayForInvitation_MaterializedValue_;

		// Token: 0x040001CC RID: 460
		internal int _MinNonClutterPerDayForInvitation_MaterializedValue_;

		// Token: 0x040001CD RID: 461
		internal int _NumberOfHistoryDaysForInvitationPerDayAverages_MaterializedValue_;

		// Token: 0x040001CE RID: 462
		internal int _MinPrecisionForAutoEnablement_MaterializedValue_;

		// Token: 0x040001CF RID: 463
		internal int _MinRecallForAutoEnablement_MaterializedValue_;

		// Token: 0x040001D0 RID: 464
		internal int _MaxFalsePositiveRateForAutoEnablement_MaterializedValue_;

		// Token: 0x040001D1 RID: 465
		internal int _ConfidenceThresholdForAutoEnablement_MaterializedValue_;

		// Token: 0x040001D2 RID: 466
		internal int _MinClutterPerDayForAutoEnablement_MaterializedValue_;

		// Token: 0x040001D3 RID: 467
		internal int _MinNonClutterPerDayForAutoEnablement_MaterializedValue_;

		// Token: 0x040001D4 RID: 468
		internal int _NumberOfHistoryDaysForAutoEnablementPerDayAverages_MaterializedValue_;

		// Token: 0x040001D5 RID: 469
		internal bool _IsModelHistoryEnabled_MaterializedValue_;

		// Token: 0x040001D6 RID: 470
		internal int _NumberOfModelHistoryCopiesToKeep_MaterializedValue_;

		// Token: 0x040001D7 RID: 471
		internal bool _IsMultiStepTrainingEnabled_MaterializedValue_;

		// Token: 0x040001D8 RID: 472
		internal int _VacationDetectionMinActivityCountThreshold_MaterializedValue_;

		// Token: 0x040001D9 RID: 473
		internal double _VacationDetectionActivityCountNumStandardDeviations_MaterializedValue_;

		// Token: 0x040001DA RID: 474
		internal int _VacationDetectionMinimumConsecutiveDays_MaterializedValue_;
	}
}
