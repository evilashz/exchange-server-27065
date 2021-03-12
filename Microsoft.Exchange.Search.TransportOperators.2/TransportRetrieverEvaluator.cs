using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000005 RID: 5
	internal class TransportRetrieverEvaluator : ProducerEvaluator<TransportRetrieverOperator>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022DF File Offset: 0x000004DF
		protected override IRecordProducer GetProducer(TransportRetrieverOperator retrieverOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new TransportRetrieverProducer(retrieverOperator, type, context);
		}
	}
}
