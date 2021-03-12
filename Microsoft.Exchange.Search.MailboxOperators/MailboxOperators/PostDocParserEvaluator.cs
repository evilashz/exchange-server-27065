using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000017 RID: 23
	internal class PostDocParserEvaluator : ProducerEvaluator<PostDocParserOperator>
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00006A18 File Offset: 0x00004C18
		protected override IRecordProducer GetProducer(PostDocParserOperator postDocParserOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new PostDocParserProducer(postDocParserOperator, type, context);
		}
	}
}
