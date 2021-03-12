using System;
using Microsoft.Exchange.Inference.Common.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000030 RID: 48
	internal interface ITrainingConfiguration
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000BA RID: 186
		ILogConfig TrainingStatusLogConfig { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BB RID: 187
		ILogConfig TruthLabelsStatusLogConfig { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BC RID: 188
		int MinNumberOfItemsForRetrospectiveTraining { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BD RID: 189
		int MaxNumberOfCyclesForRetrospectiveTraining { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BE RID: 190
		int MaxActionHistorySize { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BF RID: 191
		int TargetPrecisionForThresholdAnalysis { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C0 RID: 192
		int TargetRecallForThresholdAnalysis { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C1 RID: 193
		int TargetFalsePositiveRateForThresholdAnalysis { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C2 RID: 194
		int ConfidenceThresholdForThresholdAnalysis { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C3 RID: 195
		int CoefficientForUserValueComputation { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C4 RID: 196
		int NumberOfHistoryDaysForThresholdComputation { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C5 RID: 197
		int ActionShareExponentForScaleFactorComputation { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C6 RID: 198
		int? MinPrecisionForInvitation { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C7 RID: 199
		int? MinRecallForInvitation { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C8 RID: 200
		int? MaxFalsePositiveRateForInvitation { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C9 RID: 201
		int ConfidenceThresholdForInvitation { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000CA RID: 202
		int? MinClutterPerDayForInvitation { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000CB RID: 203
		int? MinNonClutterPerDayForInvitation { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000CC RID: 204
		int NumberOfHistoryDaysForInvitationPerDayAverages { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CD RID: 205
		int? MinPrecisionForAutoEnablement { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000CE RID: 206
		int? MinRecallForAutoEnablement { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000CF RID: 207
		int? MaxFalsePositiveRateForAutoEnablement { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000D0 RID: 208
		int ConfidenceThresholdForAutoEnablement { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000D1 RID: 209
		int? MinClutterPerDayForAutoEnablement { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000D2 RID: 210
		int? MinNonClutterPerDayForAutoEnablement { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000D3 RID: 211
		int NumberOfHistoryDaysForAutoEnablementPerDayAverages { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000D4 RID: 212
		bool IsModelHistoryEnabled { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000D5 RID: 213
		int NumberOfModelHistoryCopiesToKeep { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000D6 RID: 214
		bool IsMultiStepTrainingEnabled { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000D7 RID: 215
		int VacationDetectionMinActivityCountThreshold { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000D8 RID: 216
		double VacationDetectionActivityCountNumStandardDeviations { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000D9 RID: 217
		int VacationDetectionMinimumConsecutiveDays { get; }
	}
}
