using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000065 RID: 101
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IInferenceTrainingConfigurationSettings : ISettings
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000204 RID: 516
		bool IsLoggingEnabled { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000205 RID: 517
		string LogPath { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000206 RID: 518
		int MaxLogAgeInDays { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000207 RID: 519
		ulong MaxLogDirectorySizeInMB { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000208 RID: 520
		ulong MaxLogFileSizeInMB { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000209 RID: 521
		int MinNumberOfItemsForRetrospectiveTraining { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600020A RID: 522
		int MaxNumberOfCyclesForRetrospectiveTraining { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600020B RID: 523
		int MaxActionHistorySize { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600020C RID: 524
		int TargetPrecisionForThresholdAnalysis { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600020D RID: 525
		int TargetRecallForThresholdAnalysis { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600020E RID: 526
		int TargetFalsePositiveRateForThresholdAnalysis { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600020F RID: 527
		int ConfidenceThresholdForThresholdAnalysis { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000210 RID: 528
		int CoefficientForUserValueComputation { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000211 RID: 529
		int NumberOfHistoryDaysForThresholdComputation { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000212 RID: 530
		int ActionShareExponentForScaleFactorComputation { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000213 RID: 531
		int MinPrecisionForInvitation { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000214 RID: 532
		int MinRecallForInvitation { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000215 RID: 533
		int MaxFalsePositiveRateForInvitation { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000216 RID: 534
		int ConfidenceThresholdForInvitation { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000217 RID: 535
		int MinClutterPerDayForInvitation { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000218 RID: 536
		int MinNonClutterPerDayForInvitation { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000219 RID: 537
		int NumberOfHistoryDaysForInvitationPerDayAverages { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600021A RID: 538
		int MinPrecisionForAutoEnablement { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600021B RID: 539
		int MinRecallForAutoEnablement { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600021C RID: 540
		int MaxFalsePositiveRateForAutoEnablement { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600021D RID: 541
		int ConfidenceThresholdForAutoEnablement { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600021E RID: 542
		int MinClutterPerDayForAutoEnablement { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600021F RID: 543
		int MinNonClutterPerDayForAutoEnablement { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000220 RID: 544
		int NumberOfHistoryDaysForAutoEnablementPerDayAverages { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000221 RID: 545
		bool IsModelHistoryEnabled { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000222 RID: 546
		int NumberOfModelHistoryCopiesToKeep { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000223 RID: 547
		bool IsMultiStepTrainingEnabled { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000224 RID: 548
		int VacationDetectionMinActivityCountThreshold { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000225 RID: 549
		double VacationDetectionActivityCountNumStandardDeviations { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000226 RID: 550
		int VacationDetectionMinimumConsecutiveDays { get; }
	}
}
