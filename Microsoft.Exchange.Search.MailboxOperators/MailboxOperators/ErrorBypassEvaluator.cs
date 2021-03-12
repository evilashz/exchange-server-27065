using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000008 RID: 8
	internal class ErrorBypassEvaluator : ProducerEvaluator<ErrorBypassOperator>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002637 File Offset: 0x00000837
		protected override IRecordProducer GetProducer(ErrorBypassOperator errorBypassOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new ErrorBypassProducer(errorBypassOperator, type, context);
		}
	}
}
