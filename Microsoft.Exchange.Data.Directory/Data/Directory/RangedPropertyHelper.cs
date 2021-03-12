using System;
using System.DirectoryServices.Protocols;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000179 RID: 377
	internal static class RangedPropertyHelper
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0004EAAC File Offset: 0x0004CCAC
		public static ADPropertyDefinition CreateRangedProperty(ADPropertyDefinition originalProperty, IntRange range)
		{
			if (range == null || range.LowerBound < 0 || range.LowerBound > range.UpperBound)
			{
				throw new ArgumentException("range");
			}
			return new ADPropertyDefinition(originalProperty.Name, originalProperty.VersionAdded, originalProperty.Type, originalProperty.FormatProvider, originalProperty.LdapDisplayName + RangedPropertyHelper.GetADRangeSuffix(range), originalProperty.Flags | ADPropertyDefinitionFlags.Ranged | ADPropertyDefinitionFlags.ReadOnly, originalProperty.DefaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0004EB34 File Offset: 0x0004CD34
		public static DirectoryAttribute GetRangedPropertyValue(ADPropertyDefinition propertyDefinition, SearchResultAttributeCollection attributeCollection, out IntRange returnedRange)
		{
			DirectoryAttribute result = null;
			returnedRange = null;
			string text;
			IntRange propertyRangeFromLdapName = RangedPropertyHelper.GetPropertyRangeFromLdapName(propertyDefinition.LdapDisplayName, out text);
			if (attributeCollection.Contains(propertyDefinition.LdapDisplayName))
			{
				returnedRange = propertyRangeFromLdapName;
				result = attributeCollection[propertyDefinition.LdapDisplayName];
			}
			else
			{
				string attributeNameWithRange = ADSession.GetAttributeNameWithRange(text, propertyRangeFromLdapName.LowerBound.ToString(), "*");
				if (attributeCollection.Contains(attributeNameWithRange))
				{
					returnedRange = new IntRange(propertyRangeFromLdapName.LowerBound, int.MaxValue);
					result = attributeCollection[attributeNameWithRange];
				}
				else
				{
					string value = string.Format(CultureInfo.InvariantCulture, "{0};{1}{2}-", new object[]
					{
						text,
						"range=",
						propertyRangeFromLdapName.LowerBound
					});
					foreach (object obj in attributeCollection.AttributeNames)
					{
						string text2 = (string)obj;
						if (text2.StartsWith(value, StringComparison.OrdinalIgnoreCase))
						{
							result = attributeCollection[text2];
							returnedRange = RangedPropertyHelper.GetPropertyRangeFromLdapName(text2, out text);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0004EC60 File Offset: 0x0004CE60
		private static string GetADRangeSuffix(IntRange range)
		{
			return string.Format(CultureInfo.InvariantCulture, ";{0}{1}-{2}", new object[]
			{
				"range=",
				range.LowerBound,
				(range.UpperBound == int.MaxValue) ? "*" : range.UpperBound.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		internal static IntRange GetPropertyRangeFromLdapName(string rangedPropertyName, out string ldapNameWithoutRange)
		{
			int num = rangedPropertyName.LastIndexOf(";");
			if (num < 1)
			{
				throw new FormatException(DirectoryStrings.RangePropertyResponseDoesNotContainRangeInformation(rangedPropertyName));
			}
			IntRange result = RangedPropertyHelper.ParsePropertyValueRange(rangedPropertyName.Substring(num + 1));
			ldapNameWithoutRange = rangedPropertyName.Substring(0, num);
			return result;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		private static IntRange ParsePropertyValueRange(string value)
		{
			int num = value.IndexOf('-');
			if (num < 7)
			{
				throw new FormatException(DirectoryStrings.RangeInformationFormatInvalid(value));
			}
			int lowerBound = RangedPropertyHelper.ParsePropertyValueRangeBound(value.Substring(6, num - 6), false);
			int upperBound = RangedPropertyHelper.ParsePropertyValueRangeBound(value.Substring(num + 1), true);
			return new IntRange(lowerBound, upperBound);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0004ED5F File Offset: 0x0004CF5F
		private static int ParsePropertyValueRangeBound(string value, bool allowWildcard)
		{
			if (allowWildcard && value == "*")
			{
				return int.MaxValue;
			}
			return int.Parse(value, NumberStyles.None, CultureInfo.InvariantCulture);
		}

		// Token: 0x0400094C RID: 2380
		private const string RangeWildcard = "*";

		// Token: 0x0400094D RID: 2381
		private const string RangePrefix = "range=";

		// Token: 0x0400094E RID: 2382
		private const int RangePrefixLength = 6;

		// Token: 0x0400094F RID: 2383
		internal static readonly IntRange AllValuesRange = new IntRange(0, int.MaxValue);
	}
}
