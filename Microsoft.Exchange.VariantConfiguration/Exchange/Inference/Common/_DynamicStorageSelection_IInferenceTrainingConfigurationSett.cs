using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000066 RID: 102
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_ : VariantObjectDataAccessorBase<IInferenceTrainingConfigurationSettings, _DynamicStorageSelection_IInferenceTrainingConfigurationSettings_Implementation_, _DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_>
	{
		// Token: 0x0400016E RID: 366
		internal string _Name_MaterializedValue_;

		// Token: 0x0400016F RID: 367
		internal bool _IsLoggingEnabled_MaterializedValue_;

		// Token: 0x04000170 RID: 368
		internal ValueProvider<bool> _IsLoggingEnabled_ValueProvider_;

		// Token: 0x04000171 RID: 369
		internal string _LogPath_MaterializedValue_;

		// Token: 0x04000172 RID: 370
		internal ValueProvider<string> _LogPath_ValueProvider_;

		// Token: 0x04000173 RID: 371
		internal int _MaxLogAgeInDays_MaterializedValue_;

		// Token: 0x04000174 RID: 372
		internal ValueProvider<int> _MaxLogAgeInDays_ValueProvider_;

		// Token: 0x04000175 RID: 373
		internal ulong _MaxLogDirectorySizeInMB_MaterializedValue_;

		// Token: 0x04000176 RID: 374
		internal ValueProvider<ulong> _MaxLogDirectorySizeInMB_ValueProvider_;

		// Token: 0x04000177 RID: 375
		internal ulong _MaxLogFileSizeInMB_MaterializedValue_;

		// Token: 0x04000178 RID: 376
		internal ValueProvider<ulong> _MaxLogFileSizeInMB_ValueProvider_;

		// Token: 0x04000179 RID: 377
		internal int _MinNumberOfItemsForRetrospectiveTraining_MaterializedValue_;

		// Token: 0x0400017A RID: 378
		internal ValueProvider<int> _MinNumberOfItemsForRetrospectiveTraining_ValueProvider_;

		// Token: 0x0400017B RID: 379
		internal int _MaxNumberOfCyclesForRetrospectiveTraining_MaterializedValue_;

		// Token: 0x0400017C RID: 380
		internal ValueProvider<int> _MaxNumberOfCyclesForRetrospectiveTraining_ValueProvider_;

		// Token: 0x0400017D RID: 381
		internal int _MaxActionHistorySize_MaterializedValue_;

		// Token: 0x0400017E RID: 382
		internal ValueProvider<int> _MaxActionHistorySize_ValueProvider_;

		// Token: 0x0400017F RID: 383
		internal int _TargetPrecisionForThresholdAnalysis_MaterializedValue_;

		// Token: 0x04000180 RID: 384
		internal ValueProvider<int> _TargetPrecisionForThresholdAnalysis_ValueProvider_;

		// Token: 0x04000181 RID: 385
		internal int _TargetRecallForThresholdAnalysis_MaterializedValue_;

		// Token: 0x04000182 RID: 386
		internal ValueProvider<int> _TargetRecallForThresholdAnalysis_ValueProvider_;

		// Token: 0x04000183 RID: 387
		internal int _TargetFalsePositiveRateForThresholdAnalysis_MaterializedValue_;

		// Token: 0x04000184 RID: 388
		internal ValueProvider<int> _TargetFalsePositiveRateForThresholdAnalysis_ValueProvider_;

		// Token: 0x04000185 RID: 389
		internal int _ConfidenceThresholdForThresholdAnalysis_MaterializedValue_;

		// Token: 0x04000186 RID: 390
		internal ValueProvider<int> _ConfidenceThresholdForThresholdAnalysis_ValueProvider_;

		// Token: 0x04000187 RID: 391
		internal int _CoefficientForUserValueComputation_MaterializedValue_;

		// Token: 0x04000188 RID: 392
		internal ValueProvider<int> _CoefficientForUserValueComputation_ValueProvider_;

		// Token: 0x04000189 RID: 393
		internal int _NumberOfHistoryDaysForThresholdComputation_MaterializedValue_;

		// Token: 0x0400018A RID: 394
		internal ValueProvider<int> _NumberOfHistoryDaysForThresholdComputation_ValueProvider_;

		// Token: 0x0400018B RID: 395
		internal int _ActionShareExponentForScaleFactorComputation_MaterializedValue_;

		// Token: 0x0400018C RID: 396
		internal ValueProvider<int> _ActionShareExponentForScaleFactorComputation_ValueProvider_;

		// Token: 0x0400018D RID: 397
		internal int _MinPrecisionForInvitation_MaterializedValue_;

		// Token: 0x0400018E RID: 398
		internal ValueProvider<int> _MinPrecisionForInvitation_ValueProvider_;

		// Token: 0x0400018F RID: 399
		internal int _MinRecallForInvitation_MaterializedValue_;

		// Token: 0x04000190 RID: 400
		internal ValueProvider<int> _MinRecallForInvitation_ValueProvider_;

		// Token: 0x04000191 RID: 401
		internal int _MaxFalsePositiveRateForInvitation_MaterializedValue_;

		// Token: 0x04000192 RID: 402
		internal ValueProvider<int> _MaxFalsePositiveRateForInvitation_ValueProvider_;

		// Token: 0x04000193 RID: 403
		internal int _ConfidenceThresholdForInvitation_MaterializedValue_;

		// Token: 0x04000194 RID: 404
		internal ValueProvider<int> _ConfidenceThresholdForInvitation_ValueProvider_;

		// Token: 0x04000195 RID: 405
		internal int _MinClutterPerDayForInvitation_MaterializedValue_;

		// Token: 0x04000196 RID: 406
		internal ValueProvider<int> _MinClutterPerDayForInvitation_ValueProvider_;

		// Token: 0x04000197 RID: 407
		internal int _MinNonClutterPerDayForInvitation_MaterializedValue_;

		// Token: 0x04000198 RID: 408
		internal ValueProvider<int> _MinNonClutterPerDayForInvitation_ValueProvider_;

		// Token: 0x04000199 RID: 409
		internal int _NumberOfHistoryDaysForInvitationPerDayAverages_MaterializedValue_;

		// Token: 0x0400019A RID: 410
		internal ValueProvider<int> _NumberOfHistoryDaysForInvitationPerDayAverages_ValueProvider_;

		// Token: 0x0400019B RID: 411
		internal int _MinPrecisionForAutoEnablement_MaterializedValue_;

		// Token: 0x0400019C RID: 412
		internal ValueProvider<int> _MinPrecisionForAutoEnablement_ValueProvider_;

		// Token: 0x0400019D RID: 413
		internal int _MinRecallForAutoEnablement_MaterializedValue_;

		// Token: 0x0400019E RID: 414
		internal ValueProvider<int> _MinRecallForAutoEnablement_ValueProvider_;

		// Token: 0x0400019F RID: 415
		internal int _MaxFalsePositiveRateForAutoEnablement_MaterializedValue_;

		// Token: 0x040001A0 RID: 416
		internal ValueProvider<int> _MaxFalsePositiveRateForAutoEnablement_ValueProvider_;

		// Token: 0x040001A1 RID: 417
		internal int _ConfidenceThresholdForAutoEnablement_MaterializedValue_;

		// Token: 0x040001A2 RID: 418
		internal ValueProvider<int> _ConfidenceThresholdForAutoEnablement_ValueProvider_;

		// Token: 0x040001A3 RID: 419
		internal int _MinClutterPerDayForAutoEnablement_MaterializedValue_;

		// Token: 0x040001A4 RID: 420
		internal ValueProvider<int> _MinClutterPerDayForAutoEnablement_ValueProvider_;

		// Token: 0x040001A5 RID: 421
		internal int _MinNonClutterPerDayForAutoEnablement_MaterializedValue_;

		// Token: 0x040001A6 RID: 422
		internal ValueProvider<int> _MinNonClutterPerDayForAutoEnablement_ValueProvider_;

		// Token: 0x040001A7 RID: 423
		internal int _NumberOfHistoryDaysForAutoEnablementPerDayAverages_MaterializedValue_;

		// Token: 0x040001A8 RID: 424
		internal ValueProvider<int> _NumberOfHistoryDaysForAutoEnablementPerDayAverages_ValueProvider_;

		// Token: 0x040001A9 RID: 425
		internal bool _IsModelHistoryEnabled_MaterializedValue_;

		// Token: 0x040001AA RID: 426
		internal ValueProvider<bool> _IsModelHistoryEnabled_ValueProvider_;

		// Token: 0x040001AB RID: 427
		internal int _NumberOfModelHistoryCopiesToKeep_MaterializedValue_;

		// Token: 0x040001AC RID: 428
		internal ValueProvider<int> _NumberOfModelHistoryCopiesToKeep_ValueProvider_;

		// Token: 0x040001AD RID: 429
		internal bool _IsMultiStepTrainingEnabled_MaterializedValue_;

		// Token: 0x040001AE RID: 430
		internal ValueProvider<bool> _IsMultiStepTrainingEnabled_ValueProvider_;

		// Token: 0x040001AF RID: 431
		internal int _VacationDetectionMinActivityCountThreshold_MaterializedValue_;

		// Token: 0x040001B0 RID: 432
		internal ValueProvider<int> _VacationDetectionMinActivityCountThreshold_ValueProvider_;

		// Token: 0x040001B1 RID: 433
		internal double _VacationDetectionActivityCountNumStandardDeviations_MaterializedValue_;

		// Token: 0x040001B2 RID: 434
		internal ValueProvider<double> _VacationDetectionActivityCountNumStandardDeviations_ValueProvider_;

		// Token: 0x040001B3 RID: 435
		internal int _VacationDetectionMinimumConsecutiveDays_MaterializedValue_;

		// Token: 0x040001B4 RID: 436
		internal ValueProvider<int> _VacationDetectionMinimumConsecutiveDays_ValueProvider_;
	}
}
