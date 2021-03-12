using System;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000023 RID: 35
	public class TokenEvaluatorBinder : AbstractEvaluatorBinder
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000061F1 File Offset: 0x000043F1
		public override Evaluator BindEvaluator(OperatorBase op, IEvaluationContext context)
		{
			if (op is TokenDeserializerOperator)
			{
				return new TokenDeserializerEvaluator();
			}
			if (op is TokenSerializerOperator)
			{
				return new TokenSerializerEvaluator();
			}
			if (op is RecordManagerOperator)
			{
				return new RecordManagerEvaluator();
			}
			if (op is DiagnosticOperator)
			{
				return new DiagnosticEvaluator();
			}
			return null;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000622C File Offset: 0x0000442C
		protected override void AddBoundOperators()
		{
			base.Add(typeof(TokenDeserializerOperator));
			base.Add(typeof(TokenSerializerOperator));
			base.Add(typeof(RecordManagerOperator));
			base.Add(typeof(DiagnosticOperator));
		}
	}
}
