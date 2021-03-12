using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000067 RID: 103
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IInferenceTrainingConfigurationSettings_Implementation_ : IInferenceTrainingConfigurationSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004D30 File Offset: 0x00002F30
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00004D38 File Offset: 0x00002F38
		_DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00004D40 File Offset: 0x00002F40
		void IDataAccessorBackedObject<_DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004D50 File Offset: 0x00002F50
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004D5D File Offset: 0x00002F5D
		public bool IsLoggingEnabled
		{
			get
			{
				if (this.dataAccessor._IsLoggingEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._IsLoggingEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._IsLoggingEnabled_MaterializedValue_;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00004D8E File Offset: 0x00002F8E
		public string LogPath
		{
			get
			{
				if (this.dataAccessor._LogPath_ValueProvider_ != null)
				{
					return this.dataAccessor._LogPath_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._LogPath_MaterializedValue_;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004DBF File Offset: 0x00002FBF
		public int MaxLogAgeInDays
		{
			get
			{
				if (this.dataAccessor._MaxLogAgeInDays_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxLogAgeInDays_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxLogAgeInDays_MaterializedValue_;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public ulong MaxLogDirectorySizeInMB
		{
			get
			{
				if (this.dataAccessor._MaxLogDirectorySizeInMB_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxLogDirectorySizeInMB_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxLogDirectorySizeInMB_MaterializedValue_;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004E21 File Offset: 0x00003021
		public ulong MaxLogFileSizeInMB
		{
			get
			{
				if (this.dataAccessor._MaxLogFileSizeInMB_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxLogFileSizeInMB_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxLogFileSizeInMB_MaterializedValue_;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00004E52 File Offset: 0x00003052
		public int MinNumberOfItemsForRetrospectiveTraining
		{
			get
			{
				if (this.dataAccessor._MinNumberOfItemsForRetrospectiveTraining_ValueProvider_ != null)
				{
					return this.dataAccessor._MinNumberOfItemsForRetrospectiveTraining_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinNumberOfItemsForRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00004E83 File Offset: 0x00003083
		public int MaxNumberOfCyclesForRetrospectiveTraining
		{
			get
			{
				if (this.dataAccessor._MaxNumberOfCyclesForRetrospectiveTraining_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxNumberOfCyclesForRetrospectiveTraining_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxNumberOfCyclesForRetrospectiveTraining_MaterializedValue_;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00004EB4 File Offset: 0x000030B4
		public int MaxActionHistorySize
		{
			get
			{
				if (this.dataAccessor._MaxActionHistorySize_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxActionHistorySize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxActionHistorySize_MaterializedValue_;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00004EE5 File Offset: 0x000030E5
		public int TargetPrecisionForThresholdAnalysis
		{
			get
			{
				if (this.dataAccessor._TargetPrecisionForThresholdAnalysis_ValueProvider_ != null)
				{
					return this.dataAccessor._TargetPrecisionForThresholdAnalysis_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._TargetPrecisionForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004F16 File Offset: 0x00003116
		public int TargetRecallForThresholdAnalysis
		{
			get
			{
				if (this.dataAccessor._TargetRecallForThresholdAnalysis_ValueProvider_ != null)
				{
					return this.dataAccessor._TargetRecallForThresholdAnalysis_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._TargetRecallForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00004F47 File Offset: 0x00003147
		public int TargetFalsePositiveRateForThresholdAnalysis
		{
			get
			{
				if (this.dataAccessor._TargetFalsePositiveRateForThresholdAnalysis_ValueProvider_ != null)
				{
					return this.dataAccessor._TargetFalsePositiveRateForThresholdAnalysis_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._TargetFalsePositiveRateForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004F78 File Offset: 0x00003178
		public int ConfidenceThresholdForThresholdAnalysis
		{
			get
			{
				if (this.dataAccessor._ConfidenceThresholdForThresholdAnalysis_ValueProvider_ != null)
				{
					return this.dataAccessor._ConfidenceThresholdForThresholdAnalysis_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ConfidenceThresholdForThresholdAnalysis_MaterializedValue_;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004FA9 File Offset: 0x000031A9
		public int CoefficientForUserValueComputation
		{
			get
			{
				if (this.dataAccessor._CoefficientForUserValueComputation_ValueProvider_ != null)
				{
					return this.dataAccessor._CoefficientForUserValueComputation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._CoefficientForUserValueComputation_MaterializedValue_;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00004FDA File Offset: 0x000031DA
		public int NumberOfHistoryDaysForThresholdComputation
		{
			get
			{
				if (this.dataAccessor._NumberOfHistoryDaysForThresholdComputation_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfHistoryDaysForThresholdComputation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfHistoryDaysForThresholdComputation_MaterializedValue_;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000500B File Offset: 0x0000320B
		public int ActionShareExponentForScaleFactorComputation
		{
			get
			{
				if (this.dataAccessor._ActionShareExponentForScaleFactorComputation_ValueProvider_ != null)
				{
					return this.dataAccessor._ActionShareExponentForScaleFactorComputation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ActionShareExponentForScaleFactorComputation_MaterializedValue_;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000503C File Offset: 0x0000323C
		public int MinPrecisionForInvitation
		{
			get
			{
				if (this.dataAccessor._MinPrecisionForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._MinPrecisionForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinPrecisionForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000506D File Offset: 0x0000326D
		public int MinRecallForInvitation
		{
			get
			{
				if (this.dataAccessor._MinRecallForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._MinRecallForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinRecallForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000509E File Offset: 0x0000329E
		public int MaxFalsePositiveRateForInvitation
		{
			get
			{
				if (this.dataAccessor._MaxFalsePositiveRateForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxFalsePositiveRateForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxFalsePositiveRateForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000050CF File Offset: 0x000032CF
		public int ConfidenceThresholdForInvitation
		{
			get
			{
				if (this.dataAccessor._ConfidenceThresholdForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._ConfidenceThresholdForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ConfidenceThresholdForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00005100 File Offset: 0x00003300
		public int MinClutterPerDayForInvitation
		{
			get
			{
				if (this.dataAccessor._MinClutterPerDayForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._MinClutterPerDayForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinClutterPerDayForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005131 File Offset: 0x00003331
		public int MinNonClutterPerDayForInvitation
		{
			get
			{
				if (this.dataAccessor._MinNonClutterPerDayForInvitation_ValueProvider_ != null)
				{
					return this.dataAccessor._MinNonClutterPerDayForInvitation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinNonClutterPerDayForInvitation_MaterializedValue_;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00005162 File Offset: 0x00003362
		public int NumberOfHistoryDaysForInvitationPerDayAverages
		{
			get
			{
				if (this.dataAccessor._NumberOfHistoryDaysForInvitationPerDayAverages_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfHistoryDaysForInvitationPerDayAverages_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfHistoryDaysForInvitationPerDayAverages_MaterializedValue_;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005193 File Offset: 0x00003393
		public int MinPrecisionForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._MinPrecisionForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._MinPrecisionForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinPrecisionForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000051C4 File Offset: 0x000033C4
		public int MinRecallForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._MinRecallForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._MinRecallForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinRecallForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000051F5 File Offset: 0x000033F5
		public int MaxFalsePositiveRateForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._MaxFalsePositiveRateForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxFalsePositiveRateForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxFalsePositiveRateForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00005226 File Offset: 0x00003426
		public int ConfidenceThresholdForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._ConfidenceThresholdForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._ConfidenceThresholdForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ConfidenceThresholdForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005257 File Offset: 0x00003457
		public int MinClutterPerDayForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._MinClutterPerDayForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._MinClutterPerDayForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinClutterPerDayForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00005288 File Offset: 0x00003488
		public int MinNonClutterPerDayForAutoEnablement
		{
			get
			{
				if (this.dataAccessor._MinNonClutterPerDayForAutoEnablement_ValueProvider_ != null)
				{
					return this.dataAccessor._MinNonClutterPerDayForAutoEnablement_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MinNonClutterPerDayForAutoEnablement_MaterializedValue_;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000052B9 File Offset: 0x000034B9
		public int NumberOfHistoryDaysForAutoEnablementPerDayAverages
		{
			get
			{
				if (this.dataAccessor._NumberOfHistoryDaysForAutoEnablementPerDayAverages_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfHistoryDaysForAutoEnablementPerDayAverages_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfHistoryDaysForAutoEnablementPerDayAverages_MaterializedValue_;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000052EA File Offset: 0x000034EA
		public bool IsModelHistoryEnabled
		{
			get
			{
				if (this.dataAccessor._IsModelHistoryEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._IsModelHistoryEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._IsModelHistoryEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000531B File Offset: 0x0000351B
		public int NumberOfModelHistoryCopiesToKeep
		{
			get
			{
				if (this.dataAccessor._NumberOfModelHistoryCopiesToKeep_ValueProvider_ != null)
				{
					return this.dataAccessor._NumberOfModelHistoryCopiesToKeep_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._NumberOfModelHistoryCopiesToKeep_MaterializedValue_;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000534C File Offset: 0x0000354C
		public bool IsMultiStepTrainingEnabled
		{
			get
			{
				if (this.dataAccessor._IsMultiStepTrainingEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._IsMultiStepTrainingEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._IsMultiStepTrainingEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000537D File Offset: 0x0000357D
		public int VacationDetectionMinActivityCountThreshold
		{
			get
			{
				if (this.dataAccessor._VacationDetectionMinActivityCountThreshold_ValueProvider_ != null)
				{
					return this.dataAccessor._VacationDetectionMinActivityCountThreshold_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._VacationDetectionMinActivityCountThreshold_MaterializedValue_;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000053AE File Offset: 0x000035AE
		public double VacationDetectionActivityCountNumStandardDeviations
		{
			get
			{
				if (this.dataAccessor._VacationDetectionActivityCountNumStandardDeviations_ValueProvider_ != null)
				{
					return this.dataAccessor._VacationDetectionActivityCountNumStandardDeviations_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._VacationDetectionActivityCountNumStandardDeviations_MaterializedValue_;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000053DF File Offset: 0x000035DF
		public int VacationDetectionMinimumConsecutiveDays
		{
			get
			{
				if (this.dataAccessor._VacationDetectionMinimumConsecutiveDays_ValueProvider_ != null)
				{
					return this.dataAccessor._VacationDetectionMinimumConsecutiveDays_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._VacationDetectionMinimumConsecutiveDays_MaterializedValue_;
			}
		}

		// Token: 0x040001B5 RID: 437
		private _DynamicStorageSelection_IInferenceTrainingConfigurationSettings_DataAccessor_ dataAccessor;

		// Token: 0x040001B6 RID: 438
		private VariantContextSnapshot context;
	}
}
