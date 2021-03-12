using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200001F RID: 31
	internal class RecordManagerProducer : ExchangeProducerBase<RecordManagerOperator>
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00005ABE File Offset: 0x00003CBE
		public RecordManagerProducer(RecordManagerOperator recordManagerOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(recordManagerOperator, inputType, context, ExTraceGlobals.RecordManagerOperatorTracer)
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005ACE File Offset: 0x00003CCE
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecordManagerProducer>(this);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public override void InternalProcessRecord(IRecord record)
		{
			string stringValue = ((IStringField)record["ExchangeCtsFlowOperation"]).StringValue;
			if ("Indexing" == stringValue)
			{
				ItemDepot.Instance.ClearAssociatedRecordInformation(base.FlowIdentifier);
				base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalTimeSpentProcessingDocuments, (long)base.Diagnostics.GetSplitTime().TotalMilliseconds);
			}
			IndexableRecordValidation.Instance.ValidateIndexableRecord(record, base.FlowIdentifier, base.InputProperties.RecordSetType.RecordType, "WatermarkUpdate" == stringValue);
			base.SetNextRecord(record);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005B68 File Offset: 0x00003D68
		protected override void Initialize()
		{
			base.Initialize();
			IRecordImplDescriptor recordImplDescriptor = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			base.OutputProperties = new RecordSetProperties(base.OutputType, recordImplDescriptor.CreateUpdatableRecord(), recordImplDescriptor);
		}
	}
}
