using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000262 RID: 610
	public struct InferenceTags
	{
		// Token: 0x0400102C RID: 4140
		public const int ImportanceAutoLabeler = 0;

		// Token: 0x0400102D RID: 4141
		public const int MdbInferenceModelDataBinder = 1;

		// Token: 0x0400102E RID: 4142
		public const int ImportanceClassifier = 2;

		// Token: 0x0400102F RID: 4143
		public const int FeatureVectorCalculator = 3;

		// Token: 0x04001030 RID: 4144
		public const int InferenceModelWriter = 4;

		// Token: 0x04001031 RID: 4145
		public const int MdbInferenceModelWriter = 5;

		// Token: 0x04001032 RID: 4146
		public const int Service = 6;

		// Token: 0x04001033 RID: 4147
		public const int ImportanceTrainer = 7;

		// Token: 0x04001034 RID: 4148
		public const int SynchronousComponentBase = 8;

		// Token: 0x04001035 RID: 4149
		public const int ContactListIndexExtractor = 9;

		// Token: 0x04001036 RID: 4150
		public const int NestedTrainingPipelineFeeder = 10;

		// Token: 0x04001037 RID: 4151
		public const int MdbInferenceModelLoader = 11;

		// Token: 0x04001038 RID: 4152
		public const int MdbTrainingFeeder = 12;

		// Token: 0x04001039 RID: 4153
		public const int DwellEstimator = 13;

		// Token: 0x0400103A RID: 4154
		public const int ClassificationModelAccuracyCalculator = 14;

		// Token: 0x0400103B RID: 4155
		public const int OrganizationContentExtractor = 15;

		// Token: 0x0400103C RID: 4156
		public const int NaturalLanguageExtractor = 16;

		// Token: 0x0400103D RID: 4157
		public const int ConversationPropertiesExtractor = 17;

		// Token: 0x0400103E RID: 4158
		public const int InferenceLogWriter = 18;

		// Token: 0x0400103F RID: 4159
		public const int SamAccountExtractor = 19;

		// Token: 0x04001040 RID: 4160
		public const int MetadataLogger = 20;

		// Token: 0x04001041 RID: 4161
		public const int MetadataCollectionFeeder = 21;

		// Token: 0x04001042 RID: 4162
		public const int MetadataCollectionNestedPipelineFeeder = 22;

		// Token: 0x04001043 RID: 4163
		public const int NestedSentItemsPipelineFeeder = 23;

		// Token: 0x04001044 RID: 4164
		public const int RecipientExtractor = 24;

		// Token: 0x04001045 RID: 4165
		public const int ResultLogger = 25;

		// Token: 0x04001046 RID: 4166
		public const int PeopleRelevanceClassifier = 26;

		// Token: 0x04001047 RID: 4167
		public const int RecipientCacheContactWriter = 27;

		// Token: 0x04001048 RID: 4168
		public const int InferenceModelLogger = 28;

		// Token: 0x04001049 RID: 4169
		public const int FolderPredictionClassifier = 29;

		// Token: 0x0400104A RID: 4170
		public const int FolderPredictionTrainer = 30;

		// Token: 0x0400104B RID: 4171
		public const int InferenceModelPruner = 31;

		// Token: 0x0400104C RID: 4172
		public const int ClassificationModelVersionSelector = 32;

		// Token: 0x0400104D RID: 4173
		public const int ConversationClutterInfo = 33;

		// Token: 0x0400104E RID: 4174
		public const int NoisyLabelMessageRemover = 34;

		// Token: 0x0400104F RID: 4175
		public const int ActionAnalyzer = 35;

		// Token: 0x04001050 RID: 4176
		public const int MessageDeduplicator = 36;

		// Token: 0x04001051 RID: 4177
		public static Guid guid = new Guid("A9CFCC80-4C92-4060-AE34-C78406D6D4EE");
	}
}
