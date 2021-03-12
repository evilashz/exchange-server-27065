using System;
using System.Globalization;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D0 RID: 2512
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ClientLanguageConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06005CBD RID: 23741 RVA: 0x00182A7C File Offset: 0x00180C7C
		public static bool IsSupportedCulture(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			CultureInfo[] installedLanguagePackCultures = LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.Client);
			CultureInfo cultureInfo = Array.Find<CultureInfo>(installedLanguagePackCultures, delegate(CultureInfo installedCulture)
			{
				for (CultureInfo cultureInfo2 = culture; cultureInfo2 != null; cultureInfo2 = cultureInfo2.Parent)
				{
					if (installedCulture.LCID == cultureInfo2.LCID)
					{
						return true;
					}
					if (cultureInfo2.Parent == cultureInfo2)
					{
						break;
					}
				}
				return false;
			});
			return cultureInfo != null;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x00182ACC File Offset: 0x00180CCC
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				ClientSupportedLanguage value2 = ((ClientSupportedLanguage?)value).Value;
				if (!ClientLanguageConstraint.IsSupportedCulture(CultureInfo.GetCultureInfo((int)value2)))
				{
					return new PropertyConstraintViolationError(ServerStrings.ErrorNotSupportedLanguageWithInstalledLanguagePack(value.ToString()), propertyDefinition, value, this);
				}
			}
			return null;
		}
	}
}
