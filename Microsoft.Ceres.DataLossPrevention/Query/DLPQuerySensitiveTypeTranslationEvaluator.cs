using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000018 RID: 24
	internal class DLPQuerySensitiveTypeTranslationEvaluator : ProducerEvaluator<DLPQuerySensitiveTypeTranslationOperator>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004DD4 File Offset: 0x00002FD4
		protected override IRecordProducer GetProducer(DLPQuerySensitiveTypeTranslationOperator op, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DLPQuerySensitiveTypeTranslationProducer(context);
		}
	}
}
