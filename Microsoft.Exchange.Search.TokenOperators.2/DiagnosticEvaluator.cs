using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000007 RID: 7
	internal class DiagnosticEvaluator : ProducerEvaluator<DiagnosticOperator>
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000344C File Offset: 0x0000164C
		protected override IRecordProducer GetProducer(DiagnosticOperator diagnosticOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DiagnosticProducer(diagnosticOperator, type, context);
		}
	}
}
