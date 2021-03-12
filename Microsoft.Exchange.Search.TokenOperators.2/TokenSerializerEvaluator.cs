using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200002A RID: 42
	internal class TokenSerializerEvaluator : ProducerEvaluator<TokenSerializerOperator>
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000068BC File Offset: 0x00004ABC
		protected override IRecordProducer GetProducer(TokenSerializerOperator tokenSerializerOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new TokenSerializerProducer(tokenSerializerOperator, type, context);
		}
	}
}
