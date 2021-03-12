using System;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000014 RID: 20
	public class MailboxEvaluatorBinder : AbstractEvaluatorBinder
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00006720 File Offset: 0x00004920
		public override Evaluator BindEvaluator(OperatorBase op, IEvaluationContext context)
		{
			if (op is RetrieverOperator)
			{
				return new RetrieverEvaluator();
			}
			if (op is ErrorOperator)
			{
				return new ErrorEvaluator();
			}
			if (op is PostDocParserOperator)
			{
				return new PostDocParserEvaluator();
			}
			if (op is DocumentTrackerOperator)
			{
				return new DocumentTrackerEvaluator();
			}
			if (op is FolderUpdateRetrieverOperator)
			{
				return new FolderUpdateRetrieverEvaluator();
			}
			if (op is DictionaryStoreWriterOperator)
			{
				return new DictionaryStoreWriterEvaluator();
			}
			if (op is DictionaryTrackerOperator)
			{
				return new DictionaryTrackerEvaluator();
			}
			if (op is ErrorBypassOperator)
			{
				return new ErrorBypassEvaluator();
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000067A0 File Offset: 0x000049A0
		protected override void AddBoundOperators()
		{
			base.Add(typeof(RetrieverOperator));
			base.Add(typeof(ErrorBypassOperator));
			base.Add(typeof(ErrorOperator));
			base.Add(typeof(PostDocParserOperator));
			base.Add(typeof(DocumentTrackerOperator));
			base.Add(typeof(FolderUpdateRetrieverOperator));
			base.Add(typeof(DictionaryStoreWriterOperator));
			base.Add(typeof(DictionaryTrackerOperator));
		}
	}
}
