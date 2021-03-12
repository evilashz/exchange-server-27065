using System;
using System.Xml.Linq;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000868 RID: 2152
	internal sealed class OperationTypeValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A6A RID: 19050 RVA: 0x00132848 File Offset: 0x00130A48
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			if (context.OperationType == ClassificationRuleCollectionOperationType.Import && context.ExistingRulePackDataObject != null)
			{
				throw new ClassificationRuleCollectionAlreadyExistsException();
			}
		}
	}
}
