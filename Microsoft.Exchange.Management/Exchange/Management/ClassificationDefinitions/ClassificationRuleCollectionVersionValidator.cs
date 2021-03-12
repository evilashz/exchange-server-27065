using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x0200085C RID: 2140
	internal sealed class ClassificationRuleCollectionVersionValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A21 RID: 18977 RVA: 0x0013123C File Offset: 0x0012F43C
		private static void ValidateOobRulePackVersionGreaterThanOrEqual(Version rulePackVersion, Version existingVersion)
		{
			ExAssert.RetailAssert(rulePackVersion != null && existingVersion != null, "Both new and existing rule package version must be specified for version validation purpose");
			if (rulePackVersion < existingVersion)
			{
				LocalizedString message = Strings.ClassificationRuleCollectionVersionViolation(rulePackVersion.ToString(), existingVersion.ToString());
				throw new ClassificationRuleCollectionVersionValidationException(message);
			}
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x00131288 File Offset: 0x0012F488
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			if (context.ExistingRulePackDataObject == null)
			{
				return;
			}
			Version version = null;
			try
			{
				version = context.GetExistingRulePackVersion();
			}
			catch (XmlException)
			{
				if (context.IsPayloadOobRuleCollection)
				{
					throw;
				}
			}
			if (null == version)
			{
				return;
			}
			Version rulePackVersion = XmlProcessingUtils.GetRulePackVersion(rulePackXDocument);
			ClassificationRuleCollectionVersionValidator.ValidateOobRulePackVersionGreaterThanOrEqual(rulePackVersion, version);
		}
	}
}
