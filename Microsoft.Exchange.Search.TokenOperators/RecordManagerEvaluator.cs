using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200001E RID: 30
	internal class RecordManagerEvaluator : ProducerEvaluator<RecordManagerOperator>
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00005AAC File Offset: 0x00003CAC
		protected override IRecordProducer GetProducer(RecordManagerOperator recordManagerOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new RecordManagerProducer(recordManagerOperator, type, context);
		}
	}
}
