using System;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000007 RID: 7
	public class TransportEvaluatorBinder : AbstractEvaluatorBinder
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003076 File Offset: 0x00001276
		public override Evaluator BindEvaluator(OperatorBase op, IEvaluationContext context)
		{
			if (op is TransportRetrieverOperator)
			{
				return new TransportRetrieverEvaluator();
			}
			if (op is TransportWriterOperator)
			{
				return new TransportWriterEvaluator();
			}
			if (op is TransportErrorOperator)
			{
				return new TransportErrorEvaluator();
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000030A3 File Offset: 0x000012A3
		protected override void AddBoundOperators()
		{
			base.Add(typeof(TransportRetrieverOperator));
			base.Add(typeof(TransportWriterOperator));
			base.Add(typeof(TransportErrorOperator));
		}
	}
}
