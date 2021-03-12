using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000858 RID: 2136
	internal sealed class ClassificationRuleCollectionIdentifierValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A0C RID: 18956 RVA: 0x00130D50 File Offset: 0x0012EF50
		private static void ValidateRulePackIdentifier(bool isOobRuleCollection, bool isFingerprintsRuleCollection, string rulePackIdentifier)
		{
			ExAssert.RetailAssert(rulePackIdentifier != null, "Rule package ID must be specified for rule package ID validation.");
			bool flag = rulePackIdentifier.StartsWith("00000000-0000-0000-0001");
			if ((isFingerprintsRuleCollection && !flag) || (!isFingerprintsRuleCollection && flag))
			{
				LocalizedString message = Strings.ClassificationRuleCollectionReservedFingerprintRulePackIdViolation(rulePackIdentifier);
				throw new ClassificationRuleCollectionIdentifierValidationException(message);
			}
			if (isFingerprintsRuleCollection)
			{
				return;
			}
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				return;
			}
			bool flag2 = rulePackIdentifier.StartsWith("00000000", StringComparison.OrdinalIgnoreCase);
			if (!isOobRuleCollection && flag2)
			{
				LocalizedString message2 = Strings.ClassificationRuleCollectionReservedRulePackIdViolation(rulePackIdentifier, "00000000");
				throw new ClassificationRuleCollectionIdentifierValidationException(message2);
			}
			if (isOobRuleCollection && !flag2)
			{
				LocalizedString message3 = Strings.ClassificationRuleCollectionOobRulePackIdViolation(rulePackIdentifier, "00000000");
				throw new ClassificationRuleCollectionIdentifierValidationException(message3);
			}
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x00130DF4 File Offset: 0x0012EFF4
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			ClassificationRuleCollectionIdentifierValidator.ValidateRulePackIdentifier(context.IsPayloadOobRuleCollection, context.IsPayloadFingerprintsRuleCollection, XmlProcessingUtils.GetRulePackId(rulePackXDocument));
		}
	}
}
