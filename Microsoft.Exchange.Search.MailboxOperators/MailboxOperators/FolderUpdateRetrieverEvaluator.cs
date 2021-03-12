using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000010 RID: 16
	internal class FolderUpdateRetrieverEvaluator : ProducerEvaluator<FolderUpdateRetrieverOperator>
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00005E78 File Offset: 0x00004078
		protected override IRecordProducer GetProducer(FolderUpdateRetrieverOperator retrieverOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new FolderUpdateRetrieverProducer(retrieverOperator, type, context);
		}
	}
}
