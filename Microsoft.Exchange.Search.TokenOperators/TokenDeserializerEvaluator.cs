using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000021 RID: 33
	internal class TokenDeserializerEvaluator : ProducerEvaluator<TokenDeserializerOperator>
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00005BEC File Offset: 0x00003DEC
		protected override IRecordProducer GetProducer(TokenDeserializerOperator tokenDeserializerOperator, IRecordSetTypeDescriptor type, IEvaluationContext context)
		{
			return new TokenDeserializerProducer(tokenDeserializerOperator, type, context);
		}
	}
}
