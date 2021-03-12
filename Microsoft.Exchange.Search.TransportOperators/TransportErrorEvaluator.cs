using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000003 RID: 3
	internal class TransportErrorEvaluator : ProducerEvaluator<TransportErrorOperator>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021DF File Offset: 0x000003DF
		protected override IRecordProducer GetProducer(TransportErrorOperator errorOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new TransportErrorProducer(errorOperator, type, context);
		}
	}
}
