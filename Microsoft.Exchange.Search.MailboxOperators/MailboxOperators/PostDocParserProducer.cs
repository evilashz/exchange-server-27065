using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000018 RID: 24
	internal class PostDocParserProducer : PostDocParserProducerBase
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00006A2A File Offset: 0x00004C2A
		public PostDocParserProducer(PostDocParserOperator postDocParserOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(postDocParserOperator, inputType, context)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006A35 File Offset: 0x00004C35
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PostDocParserProducer>(this);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006A3D File Offset: 0x00004C3D
		protected override void CleanupExchangeResources()
		{
			ItemDepot.Instance.DisposeItems(base.FlowIdentifier);
		}
	}
}
