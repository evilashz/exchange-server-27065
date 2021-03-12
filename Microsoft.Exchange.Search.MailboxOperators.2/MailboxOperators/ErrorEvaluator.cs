using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200000A RID: 10
	internal class ErrorEvaluator : ProducerEvaluator<ErrorOperator>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000267F File Offset: 0x0000087F
		protected override IRecordProducer GetProducer(ErrorOperator errorOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new ErrorProducer(errorOperator, type, context);
		}
	}
}
