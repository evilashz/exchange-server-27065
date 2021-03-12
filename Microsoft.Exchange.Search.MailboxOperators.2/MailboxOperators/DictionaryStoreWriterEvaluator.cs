using System;
using Microsoft.Ceres.ContentEngine.Processing;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000002 RID: 2
	internal class DictionaryStoreWriterEvaluator : SinkProducerEvaluator<DictionaryStoreWriterOperator>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected override IRecordProducer GetProducer(DictionaryStoreWriterOperator dictionaryStoreWriterOperator, IEvaluationContext context)
		{
			return new DictionaryStoreWriterProducer(context, false);
		}
	}
}
