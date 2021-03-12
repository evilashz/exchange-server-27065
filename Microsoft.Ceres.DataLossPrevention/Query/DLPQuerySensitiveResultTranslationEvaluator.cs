using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000015 RID: 21
	internal class DLPQuerySensitiveResultTranslationEvaluator : ProducerEvaluator<DLPQuerySensitiveResultTranslationOperator>
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000049A0 File Offset: 0x00002BA0
		protected override IRecordProducer GetProducer(DLPQuerySensitiveResultTranslationOperator op, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DLPQuerySensitiveResultTranslationProducer();
		}
	}
}
