using System;
using Microsoft.Ceres.DataLossPrevention.Crawl;
using Microsoft.Ceres.DataLossPrevention.Query;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;

namespace Microsoft.Ceres.DataLossPrevention
{
	// Token: 0x0200000E RID: 14
	public class DataLossPreventionEvaluatorBinder : AbstractEvaluatorBinder
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003AE5 File Offset: 0x00001CE5
		public override Evaluator BindEvaluator(OperatorBase op, IEvaluationContext context)
		{
			if (op is DLPClassificationOperator)
			{
				return new DLPClassificationEvaluator();
			}
			if (op is DLPQuerySecurityOperator)
			{
				return new DLPQuerySecurityEvaluator();
			}
			if (op is DLPQuerySensitiveResultTranslationOperator)
			{
				return new DLPQuerySensitiveResultTranslationEvaluator();
			}
			if (op is DLPQuerySensitiveTypeTranslationOperator)
			{
				return new DLPQuerySensitiveTypeTranslationEvaluator();
			}
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B20 File Offset: 0x00001D20
		protected override void AddBoundOperators()
		{
			base.Add(typeof(DLPClassificationOperator));
			base.Add(typeof(DLPQuerySecurityOperator));
			base.Add(typeof(DLPQuerySensitiveResultTranslationOperator));
			base.Add(typeof(DLPQuerySensitiveTypeTranslationOperator));
		}
	}
}
