using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Ceres.Evaluation.Processing.Utilities;
using Microsoft.Ceres.Flighting;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Ceres.DataLossPrevention.Crawl
{
	// Token: 0x0200000B RID: 11
	internal class DLPClassificationEvaluator : ProducerEvaluator<DLPClassificationOperator>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00003314 File Offset: 0x00001514
		protected override IRecordProducer GetProducer(DLPClassificationOperator op, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			if (op == null)
			{
				throw new ArgumentNullException("op");
			}
			try
			{
				ULS.SendTraceTag(4850019U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPClassificationEvaluator.GetProducer :: Using Operator :: {0}", new object[]
				{
					op
				});
				if (!DLPClassificationEvaluator.initialized)
				{
					DLPClassificationEvaluator.InitializeClassificationEngine(op.ClassificationConfiguration);
				}
				return new DLPClassificationEvaluator.DLPClassificationProducer(DLPClassificationEvaluator.classificationService, DLPClassificationEvaluator.ruleStore);
			}
			catch (ThreadAbortException)
			{
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ULS.SendTraceTag(4850048U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 10, "Exception in DLPClassificationEvaluator.GetProducer :: {0}", new object[]
				{
					ex
				});
			}
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033CC File Offset: 0x000015CC
		private static void InitializeClassificationEngine(ClassificationConfiguration configuration)
		{
			if (!DLPClassificationEvaluator.initialized)
			{
				lock (DLPClassificationEvaluator.lockObj)
				{
					if (!DLPClassificationEvaluator.initialized)
					{
						ULS.SendTraceTag(4850049U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPClassificationEvaluator.InitializeClassificationEngine :: Creating Classification Service");
						DLPClassificationEvaluator.ruleStore = new FASTClassificationStore();
						ExecutionLog executionLog = new FASTExcecutionLog();
						DLPClassificationEvaluator.classificationService = new ClassificationService(DLPClassificationEvaluator.ruleStore, configuration, executionLog);
						DLPClassificationEvaluator.initialized = true;
					}
				}
			}
		}

		// Token: 0x04000035 RID: 53
		private static bool initialized = false;

		// Token: 0x04000036 RID: 54
		private static object lockObj = new object();

		// Token: 0x04000037 RID: 55
		private static ClassificationService classificationService;

		// Token: 0x04000038 RID: 56
		private static FASTClassificationStore ruleStore;

		// Token: 0x0200000C RID: 12
		internal class DLPClassificationProducer : TransformationBasedProducer<DLPClassificationOperator>
		{
			// Token: 0x06000050 RID: 80 RVA: 0x0000346A File Offset: 0x0000166A
			internal DLPClassificationProducer(ClassificationService classificationService, FASTClassificationStore ruleStore)
			{
				this.classificationService = classificationService;
				this.ruleStore = ruleStore;
			}

			// Token: 0x06000051 RID: 81 RVA: 0x00003490 File Offset: 0x00001690
			protected override void Initialize(TransformationHandler handler)
			{
				this.pathPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.PathField].Position;
				this.tenantIdPosition = base.InputProperties.RecordSetType.RecordType[base.Operator.TenantIdField].Position;
				this.sw = new Stopwatch();
				this.classificationItem = new FASTClassificationItem(this.ruleStore, base.Operator, base.InputProperties.RecordSetType.RecordType);
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00003530 File Offset: 0x00001730
			public override void ProcessRecordCore(IRecord record)
			{
				string text = null;
				if (record == null)
				{
					throw new ArgumentNullException("record");
				}
				IField field = record[this.pathPosition];
				if (field != null)
				{
					text = (field.Value as string);
				}
				if (string.IsNullOrEmpty(text))
				{
					base.SetNextRecord();
					return;
				}
				string text2 = null;
				field = record[this.tenantIdPosition];
				if (field != null && field.Value is Guid)
				{
					text2 = ((Guid)field.Value).ToString();
				}
				bool flag;
				bool persistClassificationData;
				this.EvaluateFlights(text, text2, record, out flag, out persistClassificationData);
				if (!flag)
				{
					base.SetNextRecord();
					return;
				}
				try
				{
					this.sw.Restart();
					this.classificationItem.Reset(base.OutputProperties.Holder, text, persistClassificationData, () => base.Aborted);
					bool flag2 = this.classificationService.Classify(this.classificationItem);
					this.sw.Stop();
					if (base.Aborted)
					{
						ULS.SendTraceTag(6127683U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 20, "DLPClassificationEvaluator.ProcessRecordCore :: Processing has been aborted :: tenantId={0}; path={1}; ms={2}; charactersProcessed={3}; totalContentLength={4};", new object[]
						{
							text2,
							text,
							this.sw.ElapsedMilliseconds,
							this.classificationItem.CharactersProcessed,
							this.classificationItem.ContentLength
						});
					}
					if (this.classificationItem.ResultCount > 0 || this.classificationItem.IsExcelAlternateFeedUsed || this.sw.ElapsedMilliseconds >= 100L)
					{
						ULS.SendTraceTag(4850050U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "DLPClassificationEvaluator.ProcessRecordCore :: tenantId={0}; path={1}; classified={2}; results={3}; ms={4}; excelAlternateFeed={5}; charactersProcessed={6}; aborted={7};", new object[]
						{
							text2,
							text,
							flag2,
							this.classificationItem.ResultCount,
							this.sw.ElapsedMilliseconds,
							this.classificationItem.IsExcelAlternateFeedUsed,
							this.classificationItem.CharactersProcessed,
							base.Aborted
						});
					}
					this.classificationItem.ReleaseObjects();
					base.SetNextRecord();
				}
				catch (ThreadAbortException)
				{
				}
				catch (OutOfMemoryException)
				{
					throw;
				}
				catch (Exception ex)
				{
					ULS.SendTraceTag(4850051U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 10, "DLPClassificationEvaluator.ProcessRecordCore :: tenantId={0}; path={1}; Classification failed with exception: {2}", new object[]
					{
						text2,
						text,
						ex
					});
					this.classificationItem.ReleaseObjects();
					base.SetNextRecord();
				}
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003804 File Offset: 0x00001A04
			private void EvaluateFlights(string path, string tenantId, IRecord record, out bool crawl, out bool persist)
			{
				crawl = false;
				persist = false;
				if (VariantConfiguration.IsFeatureEnabled(41, tenantId))
				{
					crawl = true;
					persist = true;
				}
				ULS.SendTraceTag(4850052U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "DLPClassificationEvaluator.EvaluateFlights :: path=[{0}] crawl={1}  persist={2}", new object[]
				{
					path,
					crawl,
					persist
				});
			}

			// Token: 0x04000039 RID: 57
			private const int ElapsedTimeLoggingThreshold = 100;

			// Token: 0x0400003A RID: 58
			private ClassificationService classificationService;

			// Token: 0x0400003B RID: 59
			private FASTClassificationStore ruleStore;

			// Token: 0x0400003C RID: 60
			private Stopwatch sw;

			// Token: 0x0400003D RID: 61
			private FASTClassificationItem classificationItem;

			// Token: 0x0400003E RID: 62
			private int pathPosition = -1;

			// Token: 0x0400003F RID: 63
			private int tenantIdPosition = -1;
		}
	}
}
