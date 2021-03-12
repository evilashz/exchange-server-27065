using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000859 RID: 2137
	internal abstract class LocalizedInfoValidator : IClassificationRuleCollectionValidator
	{
		// Token: 0x06004A0F RID: 18959 RVA: 0x00130E18 File Offset: 0x0012F018
		protected static bool ValidateResourceLangCodes(IEnumerable<string> langCodes, out IList<string> invalidLangCodes, out IList<string> nonUniqueLangCodes)
		{
			ExAssert.RetailAssert(langCodes != null, "The langcode values to be validated must be specified!");
			invalidLangCodes = new List<string>();
			nonUniqueLangCodes = new List<string>();
			HashSet<CultureInfo> hashSet = new HashSet<CultureInfo>();
			foreach (string text in langCodes)
			{
				try
				{
					CultureInfo item = new CultureInfo(text);
					if (!hashSet.Add(item))
					{
						nonUniqueLangCodes.Add(text);
					}
				}
				catch (CultureNotFoundException)
				{
					invalidLangCodes.Add(text);
				}
			}
			return invalidLangCodes.Count == 0 && 0 == nonUniqueLangCodes.Count;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x00130ED4 File Offset: 0x0012F0D4
		protected void ValidateResourceLangCodes(IEnumerable<string> langCodes, Func<IEnumerable<string>, LocalizedString> invalidLangCodesExceptionMessageBuilder, Func<IEnumerable<string>, LocalizedString> nonUniqueLangCodesExceptionMessageBuilder)
		{
			ExAssert.RetailAssert(langCodes != null, "The langcode(s) passed to ValidateResourceLangCodes must not be null!");
			ExAssert.RetailAssert(invalidLangCodesExceptionMessageBuilder != null, "The invalid langcode(s) exception message builder passed to ValidateResourceLangCodes must not be null!");
			ExAssert.RetailAssert(nonUniqueLangCodesExceptionMessageBuilder != null, "The non-unique langcode(s) exception message builder passed to ValidateResourceLangCodes must not be null!");
			IList<string> list;
			IList<string> list2;
			if (!LocalizedInfoValidator.ValidateResourceLangCodes(langCodes, out list, out list2))
			{
				if (list.Count > 0)
				{
					LocalizedString localizedExceptionMessage = invalidLangCodesExceptionMessageBuilder(from invalidLangCode in list
					select string.Format("\"{0}\"", invalidLangCode));
					throw ClassificationDefinitionUtils.PopulateExceptionSource<LocalizedException, IList<string>>(this.CreateInvalidLangCodeException(localizedExceptionMessage), list);
				}
				if (list2.Count > 0)
				{
					LocalizedString localizedExceptionMessage2 = nonUniqueLangCodesExceptionMessageBuilder(list2);
					throw ClassificationDefinitionUtils.PopulateExceptionSource<LocalizedException, IList<string>>(this.CreateNonUniqueLangCodeException(localizedExceptionMessage2), list2);
				}
			}
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x00130F80 File Offset: 0x0012F180
		private static LocalizedException CreateDefaultValidationException(LocalizedString localizedExceptionMessage)
		{
			return new ClassificationRuleCollectionLocalizationInfoValidationException(localizedExceptionMessage);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x00130F88 File Offset: 0x0012F188
		protected virtual LocalizedException CreateInvalidLangCodeException(LocalizedString localizedExceptionMessage)
		{
			return LocalizedInfoValidator.CreateDefaultValidationException(localizedExceptionMessage);
		}

		// Token: 0x06004A13 RID: 18963 RVA: 0x00130F90 File Offset: 0x0012F190
		protected virtual LocalizedException CreateNonUniqueLangCodeException(LocalizedString localizedExceptionMessage)
		{
			return LocalizedInfoValidator.CreateDefaultValidationException(localizedExceptionMessage);
		}

		// Token: 0x06004A14 RID: 18964
		protected abstract void InternalValidate(XDocument rulePackXDocument);

		// Token: 0x06004A15 RID: 18965 RVA: 0x00130F98 File Offset: 0x0012F198
		public void Validate(ValidationContext context, XDocument rulePackXDocument)
		{
			this.InternalValidate(rulePackXDocument);
		}
	}
}
