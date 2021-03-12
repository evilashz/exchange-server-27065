using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000004 RID: 4
	internal class DictionaryTrackerEvaluator : ProducerEvaluator<DictionaryTrackerOperator>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000230E File Offset: 0x0000050E
		protected override IRecordProducer GetProducer(DictionaryTrackerOperator dictionaryTrackerOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DictionaryTrackerProducer(dictionaryTrackerOperator, type, context);
		}
	}
}
