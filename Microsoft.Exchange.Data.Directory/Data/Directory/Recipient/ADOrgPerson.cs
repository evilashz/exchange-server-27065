using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F5 RID: 501
	internal static class ADOrgPerson
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x0006D2A4 File Offset: 0x0006B4A4
		internal static object CountryOrRegionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADOrgPersonSchema.C];
			int countryCode = (int)propertyBag[ADOrgPersonSchema.CountryCode];
			string displayName = (string)propertyBag[ADOrgPersonSchema.Co];
			CountryInfo result = null;
			if (text != null && text.Length == 2)
			{
				try
				{
					result = CountryInfo.Parse(text, countryCode, displayName);
				}
				catch (InvalidCountryOrRegionException ex)
				{
					throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("CountryOrRegion", ex.Message), ADOrgPersonSchema.CountryOrRegion, propertyBag[ADOrgPersonSchema.C]), ex);
				}
			}
			return result;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0006D340 File Offset: 0x0006B540
		internal static void CountryOrRegionSetter(object value, IPropertyBag propertyBag)
		{
			CountryInfo countryInfo = value as CountryInfo;
			if (countryInfo != null)
			{
				propertyBag[ADOrgPersonSchema.C] = countryInfo.Name;
				propertyBag[ADOrgPersonSchema.Co] = countryInfo.DisplayName;
				propertyBag[ADOrgPersonSchema.CountryCode] = countryInfo.CountryCode;
				return;
			}
			propertyBag[ADOrgPersonSchema.C] = null;
			propertyBag[ADOrgPersonSchema.Co] = null;
			propertyBag[ADOrgPersonSchema.CountryCode] = 0;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0006D3C0 File Offset: 0x0006B5C0
		internal static QueryFilter CountryOrRegionFilterBuilder(SinglePropertyFilter filter)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			CountryInfo countryInfo = (CountryInfo)comparisonFilter.PropertyValue;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, ADOrgPersonSchema.C, countryInfo.Name);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006D420 File Offset: 0x0006B620
		internal static object SanitizedPhoneNumbersGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (ProviderPropertyDefinition propertyDefinition in ADOrgPersonSchema.SanitizedPhoneNumbers.SupportingProperties)
			{
				ICollection<string> collection = propertyBag[propertyDefinition] as ICollection<string>;
				if (collection != null)
				{
					using (IEnumerator<string> enumerator2 = collection.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string property = enumerator2.Current;
							ADOrgPerson.SanitizedPhoneNumber(multiValuedProperty, property);
						}
						continue;
					}
				}
				ADOrgPerson.SanitizedPhoneNumber(multiValuedProperty, propertyBag[propertyDefinition] as string);
			}
			return multiValuedProperty;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0006D4D8 File Offset: 0x0006B6D8
		internal static object IndexedPhoneNumbersGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[ADRecipientSchema.UMDtmfMap];
			foreach (string text in multiValuedProperty2)
			{
				if (!string.IsNullOrEmpty(text) && text.StartsWith("reversedPhone:", StringComparison.OrdinalIgnoreCase) && text.Length > "reversedPhone:".Length)
				{
					string text2 = text.Substring("reversedPhone:".Length);
					if (!multiValuedProperty.Contains(text2))
					{
						multiValuedProperty.Add(DtmfString.Reverse(text2));
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0006D588 File Offset: 0x0006B788
		internal static QueryFilter IndexedPhoneNumbersGetterFilterBuilder(SinglePropertyFilter filter)
		{
			TextFilter textFilter = filter as TextFilter;
			if (textFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(TextFilter)));
			}
			if (MatchOptions.Prefix == textFilter.MatchOptions)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedMatchOptionsForProperty(textFilter.Property.Name, textFilter.MatchOptions.ToString()));
			}
			MatchOptions matchOptions;
			if (MatchOptions.Suffix == textFilter.MatchOptions)
			{
				matchOptions = MatchOptions.Prefix;
			}
			else
			{
				matchOptions = textFilter.MatchOptions;
			}
			string text = DtmfString.Reverse(textFilter.Text);
			text = "reversedPhone:" + text;
			return new TextFilter(ADRecipientSchema.UMDtmfMap, text, matchOptions, textFilter.MatchFlags);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0006D634 File Offset: 0x0006B834
		internal static object LanguagesGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADOrgPersonSchema.LanguagesRaw];
			MultiValuedProperty<CultureInfo> multiValuedProperty = new MultiValuedProperty<CultureInfo>();
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					CultureInfo cultureInfo = null;
					try
					{
						cultureInfo = new CultureInfo(array[i].Trim());
					}
					catch (ArgumentException ex)
					{
						ExTraceGlobals.ADReadDetailsTracer.TraceDebug<string, string>(0L, "ADOrgPerson::LanguagesGetter - Invalid culture {0} ignored. Exception: {1}", array[i], ex.Message);
					}
					if (cultureInfo != null)
					{
						multiValuedProperty.Add(cultureInfo);
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		internal static void LanguagesSetter(object value, IPropertyBag propertyBag)
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			if (value != null)
			{
				bool flag = true;
				MultiValuedProperty<CultureInfo> multiValuedProperty = (MultiValuedProperty<CultureInfo>)value;
				foreach (CultureInfo cultureInfo in multiValuedProperty)
				{
					stringBuilder.AppendFormat("{0}{1}", flag ? string.Empty : ",", cultureInfo.ToString());
					flag = false;
				}
			}
			propertyBag[ADOrgPersonSchema.LanguagesRaw] = stringBuilder.ToString();
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0006D768 File Offset: 0x0006B968
		internal static void SanitizedPhoneNumber(MultiValuedProperty<string> calculatedValue, string property)
		{
			string text = DtmfString.SanitizePhoneNumber(property);
			if (!string.IsNullOrEmpty(text) && !calculatedValue.Contains(text))
			{
				calculatedValue.Add(text);
			}
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0006D794 File Offset: 0x0006B994
		internal static object[][] GetManagementChainView(IRecipientSession adSession, IADOrgPerson person, bool getPeers, params PropertyDefinition[] returnProperties)
		{
			if (returnProperties == null)
			{
				throw new ArgumentNullException("returnProperties");
			}
			int? num = null;
			for (int i = 0; i < returnProperties.Length; i++)
			{
				if (returnProperties[i] == ADOrgPersonSchema.ViewDepth)
				{
					num = new int?(i);
					break;
				}
			}
			PropertyDefinition[] array = new PropertyDefinition[returnProperties.Length + 1];
			returnProperties.CopyTo(array, 1);
			array[0] = ADOrgPersonSchema.Manager;
			Collection<IADRecipient[]> collection = new Collection<IADRecipient[]>();
			Collection<Guid> collection2 = new Collection<Guid>();
			int num2 = 0;
			if (getPeers)
			{
				ADObjectId adobjectId = person.Manager;
				while (adobjectId != null && num2 < 100)
				{
					if (collection2.Contains(adobjectId.ObjectGuid))
					{
						break;
					}
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADOrgPersonSchema.Manager, adobjectId);
					IADRecipient[] item = adSession.Find(adSession.SearchRoot, QueryScope.SubTree, filter, null, 0);
					collection2.Add(adobjectId.ObjectGuid);
					collection.Add(item);
					IADOrgPerson iadorgPerson = adSession.Read(adobjectId) as IADOrgPerson;
					if (iadorgPerson == null)
					{
						adobjectId = null;
					}
					else if (iadorgPerson.Manager == null || iadorgPerson.Manager.ObjectGuid == adobjectId.ObjectGuid)
					{
						item = new IADRecipient[]
						{
							iadorgPerson
						};
						collection.Add(item);
						num2++;
						adobjectId = null;
					}
					else
					{
						adobjectId = iadorgPerson.Manager;
					}
					num2++;
				}
			}
			else
			{
				ADObjectId adobjectId = person.Id;
				while (adobjectId != null && num2 < 100 && !collection2.Contains(adobjectId.ObjectGuid))
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, adobjectId);
					IADOrgPerson iadorgPerson2 = adSession.Read(adobjectId) as IADOrgPerson;
					collection2.Add(adobjectId.ObjectGuid);
					if (iadorgPerson2 == null)
					{
						adobjectId = null;
					}
					else
					{
						collection.Add(new IADRecipient[]
						{
							iadorgPerson2
						});
						if (iadorgPerson2.Manager != null && iadorgPerson2.Manager.ObjectGuid == adobjectId.ObjectGuid)
						{
							adobjectId = null;
						}
						else
						{
							adobjectId = iadorgPerson2.Manager;
						}
						num2++;
					}
				}
			}
			int count = collection.Count;
			int num3 = 0;
			for (int j = 0; j < count; j++)
			{
				num3 += collection[j].Length;
			}
			object[][] array2 = new object[num3][];
			int k = count - 1;
			int num4 = 0;
			while (k >= 0)
			{
				for (int l = 0; l < collection[k].Length; l++)
				{
					array2[num4] = collection[k][l].GetProperties(returnProperties);
					if (num != null)
					{
						array2[num4][num.Value] = count - 1 - k;
					}
					num4++;
				}
				k--;
			}
			return array2;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0006DA34 File Offset: 0x0006BC34
		internal static object[][] GetDirectReportsView(IRecipientSession adSession, IADOrgPerson person, params PropertyDefinition[] returnProperties)
		{
			if (returnProperties == null)
			{
				throw new ArgumentNullException("returnProperties");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADOrgPersonSchema.Manager, person.Id);
			ADPagedReader<ADRawEntry> adpagedReader = adSession.FindPagedADRawEntry(adSession.SearchRoot, QueryScope.SubTree, filter, null, 0, returnProperties);
			ADRawEntry[] recipients = adpagedReader.ReadAllPages();
			return ADSession.ConvertToView(recipients, returnProperties);
		}

		// Token: 0x04000B6A RID: 2922
		private const int MaxHierarchyDepth = 100;
	}
}
