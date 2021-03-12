using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000006 RID: 6
	internal class DocumentTrackerEvaluator : ProducerEvaluator<DocumentTrackerOperator>
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000242D File Offset: 0x0000062D
		protected override IRecordProducer GetProducer(DocumentTrackerOperator documentTrackerOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new DocumentTrackerProducer(documentTrackerOperator, type, context);
		}
	}
}
