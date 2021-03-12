using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000012 RID: 18
	public class DLPQuerySecurityEvaluator : ProducerEvaluator<DLPQuerySecurityOperator>
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000043BB File Offset: 0x000025BB
		protected override IRecordProducer GetProducer(DLPQuerySecurityOperator op, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DLPQuerySecurityProducer(context);
		}
	}
}
