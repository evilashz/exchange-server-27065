using System;
using Microsoft.Ceres.ContentEngine.Processing;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000014 RID: 20
	internal class TransportWriterEvaluator : SinkProducerEvaluator<TransportWriterOperator>
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000035C9 File Offset: 0x000017C9
		protected override IRecordProducer GetProducer(TransportWriterOperator writerOperator, IEvaluationContext context)
		{
			return new TransportWriterProducer(context, false);
		}
	}
}
