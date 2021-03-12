using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200086C RID: 2156
	internal sealed class DefaultValidationPipelineBuilder : IValidationPipelineBuilder
	{
		// Token: 0x06004A78 RID: 19064 RVA: 0x00132C38 File Offset: 0x00130E38
		private static IList<IDataClassificationComplexityValidator> CreateDataClassificationComplexityValidatorsChain()
		{
			return new List<IDataClassificationComplexityValidator>
			{
				new RegexProcessorReferencesValidator(),
				new CustomKeywordProcessorReferencesValidator(),
				new FunctionProcessorReferencesValidator(),
				new AnyBlocksCountValidator(),
				new AnyBlocksDepthValidator()
			};
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00132C84 File Offset: 0x00130E84
		public void BuildCoreValidators()
		{
			if (this.areCoreValidatorsBuilt)
			{
				return;
			}
			Dictionary<TextProcessorType, TextProcessorGrouping> oobProcessorsGroupedByType = TextProcessorUtils.OobProcessorsGroupedByType;
			List<IClassificationRuleCollectionValidator> collection = new List<IClassificationRuleCollectionValidator>
			{
				new ClassificationRuleCollectionIdentifierValidator(),
				new OperationTypeValidator(),
				new ClassificationRuleCollectionVersionValidator(),
				new NameUniquenessValidator(),
				new DataClassificationIdentifierValidator(),
				new TextProcessorIdAndMatchReferencesValidator(oobProcessorsGroupedByType),
				new ClassificationRuleCollectionLocalizedInfoValidator(),
				new DataClassificationLocalizedInfoValidator(),
				new ComplexityValidator(oobProcessorsGroupedByType, DefaultValidationPipelineBuilder.CreateDataClassificationComplexityValidatorsChain()),
				new RegexProcessorsValidator(),
				new KeywordProcessorsValidator(),
				new FingerprintProcessorsValidator(),
				new ClassificationRuleCollectionRuntimeValidator()
			};
			this.validationPipeline.AddRange(collection);
			this.areCoreValidatorsBuilt = true;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x00132D51 File Offset: 0x00130F51
		public void BuildSupplementaryValidators()
		{
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x00132D53 File Offset: 0x00130F53
		public IEnumerable<IClassificationRuleCollectionValidator> Result
		{
			get
			{
				return this.validationPipeline;
			}
		}

		// Token: 0x04002CC2 RID: 11458
		private bool areCoreValidatorsBuilt;

		// Token: 0x04002CC3 RID: 11459
		private readonly List<IClassificationRuleCollectionValidator> validationPipeline = new List<IClassificationRuleCollectionValidator>();
	}
}
