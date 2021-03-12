using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000019 RID: 25
	internal class RetrieverEvaluator : ProducerEvaluator<RetrieverOperator>
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00006A4F File Offset: 0x00004C4F
		protected override IRecordProducer GetProducer(RetrieverOperator retrieverOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new RetrieverProducer(retrieverOperator, type, context);
		}
	}
}
