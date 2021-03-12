using System;

namespace Microsoft.Exchange.Diagnostics.Components.Inference
{
	// Token: 0x02000361 RID: 865
	public static class ExTraceGlobals
	{
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x000534DA File Offset: 0x000516DA
		public static Trace ImportanceAutoLabelerTracer
		{
			get
			{
				if (ExTraceGlobals.importanceAutoLabelerTracer == null)
				{
					ExTraceGlobals.importanceAutoLabelerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.importanceAutoLabelerTracer;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000534F8 File Offset: 0x000516F8
		public static Trace MdbInferenceModelDataBinderTracer
		{
			get
			{
				if (ExTraceGlobals.mdbInferenceModelDataBinderTracer == null)
				{
					ExTraceGlobals.mdbInferenceModelDataBinderTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mdbInferenceModelDataBinderTracer;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00053516 File Offset: 0x00051716
		public static Trace ImportanceClassifierTracer
		{
			get
			{
				if (ExTraceGlobals.importanceClassifierTracer == null)
				{
					ExTraceGlobals.importanceClassifierTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.importanceClassifierTracer;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x00053534 File Offset: 0x00051734
		public static Trace FeatureVectorCalculatorTracer
		{
			get
			{
				if (ExTraceGlobals.featureVectorCalculatorTracer == null)
				{
					ExTraceGlobals.featureVectorCalculatorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.featureVectorCalculatorTracer;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00053552 File Offset: 0x00051752
		public static Trace InferenceModelWriterTracer
		{
			get
			{
				if (ExTraceGlobals.inferenceModelWriterTracer == null)
				{
					ExTraceGlobals.inferenceModelWriterTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.inferenceModelWriterTracer;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x00053570 File Offset: 0x00051770
		public static Trace MdbInferenceModelWriterTracer
		{
			get
			{
				if (ExTraceGlobals.mdbInferenceModelWriterTracer == null)
				{
					ExTraceGlobals.mdbInferenceModelWriterTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.mdbInferenceModelWriterTracer;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0005358E File Offset: 0x0005178E
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x000535AC File Offset: 0x000517AC
		public static Trace ImportanceTrainerTracer
		{
			get
			{
				if (ExTraceGlobals.importanceTrainerTracer == null)
				{
					ExTraceGlobals.importanceTrainerTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.importanceTrainerTracer;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000535CA File Offset: 0x000517CA
		public static Trace SynchronousComponentBaseTracer
		{
			get
			{
				if (ExTraceGlobals.synchronousComponentBaseTracer == null)
				{
					ExTraceGlobals.synchronousComponentBaseTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.synchronousComponentBaseTracer;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x000535E8 File Offset: 0x000517E8
		public static Trace ContactListIndexExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.contactListIndexExtractorTracer == null)
				{
					ExTraceGlobals.contactListIndexExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.contactListIndexExtractorTracer;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00053607 File Offset: 0x00051807
		public static Trace NestedTrainingPipelineFeederTracer
		{
			get
			{
				if (ExTraceGlobals.nestedTrainingPipelineFeederTracer == null)
				{
					ExTraceGlobals.nestedTrainingPipelineFeederTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.nestedTrainingPipelineFeederTracer;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00053626 File Offset: 0x00051826
		public static Trace MdbInferenceModelLoaderTracer
		{
			get
			{
				if (ExTraceGlobals.mdbInferenceModelLoaderTracer == null)
				{
					ExTraceGlobals.mdbInferenceModelLoaderTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.mdbInferenceModelLoaderTracer;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00053645 File Offset: 0x00051845
		public static Trace MdbTrainingFeederTracer
		{
			get
			{
				if (ExTraceGlobals.mdbTrainingFeederTracer == null)
				{
					ExTraceGlobals.mdbTrainingFeederTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.mdbTrainingFeederTracer;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00053664 File Offset: 0x00051864
		public static Trace DwellEstimatorTracer
		{
			get
			{
				if (ExTraceGlobals.dwellEstimatorTracer == null)
				{
					ExTraceGlobals.dwellEstimatorTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.dwellEstimatorTracer;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00053683 File Offset: 0x00051883
		public static Trace ClassificationModelAccuracyCalculatorTracer
		{
			get
			{
				if (ExTraceGlobals.classificationModelAccuracyCalculatorTracer == null)
				{
					ExTraceGlobals.classificationModelAccuracyCalculatorTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.classificationModelAccuracyCalculatorTracer;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x000536A2 File Offset: 0x000518A2
		public static Trace OrganizationContentExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.organizationContentExtractorTracer == null)
				{
					ExTraceGlobals.organizationContentExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.organizationContentExtractorTracer;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000536C1 File Offset: 0x000518C1
		public static Trace NaturalLanguageExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.naturalLanguageExtractorTracer == null)
				{
					ExTraceGlobals.naturalLanguageExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.naturalLanguageExtractorTracer;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000536E0 File Offset: 0x000518E0
		public static Trace ConversationPropertiesExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.conversationPropertiesExtractorTracer == null)
				{
					ExTraceGlobals.conversationPropertiesExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.conversationPropertiesExtractorTracer;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000536FF File Offset: 0x000518FF
		public static Trace InferenceLogWriterTracer
		{
			get
			{
				if (ExTraceGlobals.inferenceLogWriterTracer == null)
				{
					ExTraceGlobals.inferenceLogWriterTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.inferenceLogWriterTracer;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0005371E File Offset: 0x0005191E
		public static Trace SamAccountExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.samAccountExtractorTracer == null)
				{
					ExTraceGlobals.samAccountExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.samAccountExtractorTracer;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0005373D File Offset: 0x0005193D
		public static Trace MetadataLoggerTracer
		{
			get
			{
				if (ExTraceGlobals.metadataLoggerTracer == null)
				{
					ExTraceGlobals.metadataLoggerTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.metadataLoggerTracer;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0005375C File Offset: 0x0005195C
		public static Trace MetadataCollectionFeederTracer
		{
			get
			{
				if (ExTraceGlobals.metadataCollectionFeederTracer == null)
				{
					ExTraceGlobals.metadataCollectionFeederTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.metadataCollectionFeederTracer;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0005377B File Offset: 0x0005197B
		public static Trace MetadataCollectionNestedPipelineFeederTracer
		{
			get
			{
				if (ExTraceGlobals.metadataCollectionNestedPipelineFeederTracer == null)
				{
					ExTraceGlobals.metadataCollectionNestedPipelineFeederTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.metadataCollectionNestedPipelineFeederTracer;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0005379A File Offset: 0x0005199A
		public static Trace NestedSentItemsPipelineFeederTracer
		{
			get
			{
				if (ExTraceGlobals.nestedSentItemsPipelineFeederTracer == null)
				{
					ExTraceGlobals.nestedSentItemsPipelineFeederTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.nestedSentItemsPipelineFeederTracer;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000537B9 File Offset: 0x000519B9
		public static Trace RecipientExtractorTracer
		{
			get
			{
				if (ExTraceGlobals.recipientExtractorTracer == null)
				{
					ExTraceGlobals.recipientExtractorTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.recipientExtractorTracer;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x000537D8 File Offset: 0x000519D8
		public static Trace ResultLoggerTracer
		{
			get
			{
				if (ExTraceGlobals.resultLoggerTracer == null)
				{
					ExTraceGlobals.resultLoggerTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.resultLoggerTracer;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x000537F7 File Offset: 0x000519F7
		public static Trace PeopleRelevanceClassifierTracer
		{
			get
			{
				if (ExTraceGlobals.peopleRelevanceClassifierTracer == null)
				{
					ExTraceGlobals.peopleRelevanceClassifierTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.peopleRelevanceClassifierTracer;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00053816 File Offset: 0x00051A16
		public static Trace RecipientCacheContactWriterTracer
		{
			get
			{
				if (ExTraceGlobals.recipientCacheContactWriterTracer == null)
				{
					ExTraceGlobals.recipientCacheContactWriterTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.recipientCacheContactWriterTracer;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00053835 File Offset: 0x00051A35
		public static Trace InferenceModelLoggerTracer
		{
			get
			{
				if (ExTraceGlobals.inferenceModelLoggerTracer == null)
				{
					ExTraceGlobals.inferenceModelLoggerTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.inferenceModelLoggerTracer;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00053854 File Offset: 0x00051A54
		public static Trace FolderPredictionClassifierTracer
		{
			get
			{
				if (ExTraceGlobals.folderPredictionClassifierTracer == null)
				{
					ExTraceGlobals.folderPredictionClassifierTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.folderPredictionClassifierTracer;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00053873 File Offset: 0x00051A73
		public static Trace FolderPredictionTrainerTracer
		{
			get
			{
				if (ExTraceGlobals.folderPredictionTrainerTracer == null)
				{
					ExTraceGlobals.folderPredictionTrainerTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.folderPredictionTrainerTracer;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00053892 File Offset: 0x00051A92
		public static Trace InferenceModelPrunerTracer
		{
			get
			{
				if (ExTraceGlobals.inferenceModelPrunerTracer == null)
				{
					ExTraceGlobals.inferenceModelPrunerTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.inferenceModelPrunerTracer;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000538B1 File Offset: 0x00051AB1
		public static Trace ClassificationModelVersionSelectorTracer
		{
			get
			{
				if (ExTraceGlobals.classificationModelVersionSelectorTracer == null)
				{
					ExTraceGlobals.classificationModelVersionSelectorTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.classificationModelVersionSelectorTracer;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x000538D0 File Offset: 0x00051AD0
		public static Trace ConversationClutterInfoTracer
		{
			get
			{
				if (ExTraceGlobals.conversationClutterInfoTracer == null)
				{
					ExTraceGlobals.conversationClutterInfoTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.conversationClutterInfoTracer;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x000538EF File Offset: 0x00051AEF
		public static Trace NoisyLabelMessageRemoverTracer
		{
			get
			{
				if (ExTraceGlobals.noisyLabelMessageRemoverTracer == null)
				{
					ExTraceGlobals.noisyLabelMessageRemoverTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.noisyLabelMessageRemoverTracer;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0005390E File Offset: 0x00051B0E
		public static Trace ActionAnalyzerTracer
		{
			get
			{
				if (ExTraceGlobals.actionAnalyzerTracer == null)
				{
					ExTraceGlobals.actionAnalyzerTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.actionAnalyzerTracer;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005392D File Offset: 0x00051B2D
		public static Trace MessageDeduplicatorTracer
		{
			get
			{
				if (ExTraceGlobals.messageDeduplicatorTracer == null)
				{
					ExTraceGlobals.messageDeduplicatorTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.messageDeduplicatorTracer;
			}
		}

		// Token: 0x040018FD RID: 6397
		private static Guid componentGuid = new Guid("A9CFCC80-4C92-4060-AE34-C78406D6D4EE");

		// Token: 0x040018FE RID: 6398
		private static Trace importanceAutoLabelerTracer = null;

		// Token: 0x040018FF RID: 6399
		private static Trace mdbInferenceModelDataBinderTracer = null;

		// Token: 0x04001900 RID: 6400
		private static Trace importanceClassifierTracer = null;

		// Token: 0x04001901 RID: 6401
		private static Trace featureVectorCalculatorTracer = null;

		// Token: 0x04001902 RID: 6402
		private static Trace inferenceModelWriterTracer = null;

		// Token: 0x04001903 RID: 6403
		private static Trace mdbInferenceModelWriterTracer = null;

		// Token: 0x04001904 RID: 6404
		private static Trace serviceTracer = null;

		// Token: 0x04001905 RID: 6405
		private static Trace importanceTrainerTracer = null;

		// Token: 0x04001906 RID: 6406
		private static Trace synchronousComponentBaseTracer = null;

		// Token: 0x04001907 RID: 6407
		private static Trace contactListIndexExtractorTracer = null;

		// Token: 0x04001908 RID: 6408
		private static Trace nestedTrainingPipelineFeederTracer = null;

		// Token: 0x04001909 RID: 6409
		private static Trace mdbInferenceModelLoaderTracer = null;

		// Token: 0x0400190A RID: 6410
		private static Trace mdbTrainingFeederTracer = null;

		// Token: 0x0400190B RID: 6411
		private static Trace dwellEstimatorTracer = null;

		// Token: 0x0400190C RID: 6412
		private static Trace classificationModelAccuracyCalculatorTracer = null;

		// Token: 0x0400190D RID: 6413
		private static Trace organizationContentExtractorTracer = null;

		// Token: 0x0400190E RID: 6414
		private static Trace naturalLanguageExtractorTracer = null;

		// Token: 0x0400190F RID: 6415
		private static Trace conversationPropertiesExtractorTracer = null;

		// Token: 0x04001910 RID: 6416
		private static Trace inferenceLogWriterTracer = null;

		// Token: 0x04001911 RID: 6417
		private static Trace samAccountExtractorTracer = null;

		// Token: 0x04001912 RID: 6418
		private static Trace metadataLoggerTracer = null;

		// Token: 0x04001913 RID: 6419
		private static Trace metadataCollectionFeederTracer = null;

		// Token: 0x04001914 RID: 6420
		private static Trace metadataCollectionNestedPipelineFeederTracer = null;

		// Token: 0x04001915 RID: 6421
		private static Trace nestedSentItemsPipelineFeederTracer = null;

		// Token: 0x04001916 RID: 6422
		private static Trace recipientExtractorTracer = null;

		// Token: 0x04001917 RID: 6423
		private static Trace resultLoggerTracer = null;

		// Token: 0x04001918 RID: 6424
		private static Trace peopleRelevanceClassifierTracer = null;

		// Token: 0x04001919 RID: 6425
		private static Trace recipientCacheContactWriterTracer = null;

		// Token: 0x0400191A RID: 6426
		private static Trace inferenceModelLoggerTracer = null;

		// Token: 0x0400191B RID: 6427
		private static Trace folderPredictionClassifierTracer = null;

		// Token: 0x0400191C RID: 6428
		private static Trace folderPredictionTrainerTracer = null;

		// Token: 0x0400191D RID: 6429
		private static Trace inferenceModelPrunerTracer = null;

		// Token: 0x0400191E RID: 6430
		private static Trace classificationModelVersionSelectorTracer = null;

		// Token: 0x0400191F RID: 6431
		private static Trace conversationClutterInfoTracer = null;

		// Token: 0x04001920 RID: 6432
		private static Trace noisyLabelMessageRemoverTracer = null;

		// Token: 0x04001921 RID: 6433
		private static Trace actionAnalyzerTracer = null;

		// Token: 0x04001922 RID: 6434
		private static Trace messageDeduplicatorTracer = null;
	}
}
