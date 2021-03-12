using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000A0 RID: 160
	internal static class GrammarRecipientHelper
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00016198 File Offset: 0x00014398
		public static QueryFilter GetUserFilter()
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "person");
			QueryFilter queryFilter2 = new ExistsFilter(ADRecipientSchema.AddressListMembership);
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false);
			QueryFilter queryFilter4 = new ExistsFilter(ADRecipientSchema.DisplayName);
			QueryFilter queryFilter5 = new ExistsFilter(ADOrgPersonSchema.FirstName);
			QueryFilter queryFilter6 = new ExistsFilter(ADOrgPersonSchema.LastName);
			return new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2,
				queryFilter3,
				new OrFilter(new QueryFilter[]
				{
					queryFilter4,
					queryFilter5,
					queryFilter6
				})
			});
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00016238 File Offset: 0x00014438
		public static QueryFilter GetDLFilter()
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.HiddenFromAddressListsEnabled, false);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.DynamicDistributionGroup);
			QueryFilter queryFilter3 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUniversalDistributionGroup);
			QueryFilter queryFilter4 = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.MailUniversalSecurityGroup);
			QueryFilter queryFilter5 = new OrFilter(new QueryFilter[]
			{
				queryFilter2,
				queryFilter3,
				queryFilter4
			});
			return new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter5
			});
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000162C8 File Offset: 0x000144C8
		public static string GetSanitizedDisplayNameForXMLEntry(string displayName)
		{
			StringBuilder stringBuilder = new StringBuilder(displayName.Length);
			foreach (char c in displayName)
			{
				if (c > '\u001f' && c != '￾' && c != '￿')
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00016320 File Offset: 0x00014520
		public static string GetNormalizedEmailAddress(string emailAddress)
		{
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, emailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, 0, data, "NormalizationHelper.GetNormalizedEmailAddress for EmailAddress='_EmailAddress'", new object[0]);
			string inText = Utils.TrimSpaces(emailAddress);
			return SpeechUtils.SrgsEncode(inText);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00016360 File Offset: 0x00014560
		public static Dictionary<string, bool> ApplyExclusionList(string input, RecipientType recipType)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, 0, "NormalizationHelper : ApplyExclusionList - input='{0}', recipType='{1}'", new object[]
			{
				input,
				recipType
			});
			List<Replacement> list = null;
			ExclusionList exclusionList = null;
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			if (!string.IsNullOrEmpty(input))
			{
				try
				{
					exclusionList = ExclusionList.Instance;
				}
				catch (ExclusionListException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.UMGrammarGeneratorTracer, 0, "NormalizationHelper: ApplyExclusionList encountered exception while getting exclusionList. Details : {0}", new object[]
					{
						ex
					});
				}
				if (exclusionList != null)
				{
					switch (exclusionList.GetReplacementStrings(input, recipType, out list))
					{
					case MatchResult.NoMatch:
						dictionary.Add(input, true);
						return dictionary;
					case MatchResult.MatchWithReplacements:
						using (List<Replacement>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Replacement replacement = enumerator.Current;
								string value = Utils.TrimSpaces(replacement.ReplacementString);
								if (!string.IsNullOrEmpty(value) && !dictionary.ContainsKey(replacement.ReplacementString))
								{
									dictionary.Add(replacement.ReplacementString, replacement.ShouldNormalize);
								}
							}
							return dictionary;
						}
						break;
					case MatchResult.MatchWithNoReplacements:
					case MatchResult.NotFound:
						return dictionary;
					default:
						return dictionary;
					}
				}
				dictionary.Add(input, true);
			}
			return dictionary;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000164A8 File Offset: 0x000146A8
		public static string CharacterMapReplaceString(string name)
		{
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, name);
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, 0, data, "NormalizationHelper : CharacterMapReplaceString name='_UserDisplayName'", new object[]
			{
				name
			});
			string text = Utils.TrimSpaces(name);
			if (!string.IsNullOrEmpty(text))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in text)
				{
					string value = null;
					if (GrammarRecipientHelper.GetUnsafeCharacterMap().TryGetValue(c, out value))
					{
						stringBuilder.Append(value);
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00016548 File Offset: 0x00014748
		private static Dictionary<char, string> GetUnsafeCharacterMap()
		{
			if (GrammarRecipientHelper.unsafeCharMap == null)
			{
				lock (GrammarRecipientHelper.staticLock)
				{
					if (GrammarRecipientHelper.unsafeCharMap == null)
					{
						GrammarRecipientHelper.unsafeCharMap = new Dictionary<char, string>(2);
						GrammarRecipientHelper.unsafeCharMap.Add('-', " ");
						GrammarRecipientHelper.unsafeCharMap.Add('\'', " ");
					}
				}
			}
			return GrammarRecipientHelper.unsafeCharMap;
		}

		// Token: 0x04000367 RID: 871
		public const int TenantSizeThreshold = 10;

		// Token: 0x04000368 RID: 872
		private static object staticLock = new object();

		// Token: 0x04000369 RID: 873
		private static Dictionary<char, string> unsafeCharMap;

		// Token: 0x0400036A RID: 874
		public static readonly PropertyDefinition[] LookupProperties = new PropertyDefinition[]
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.PhoneticDisplayName,
			ADRecipientSchema.PrimarySmtpAddress,
			ADObjectSchema.Guid,
			ADRecipientSchema.RecipientType,
			ADRecipientSchema.RecipientTypeDetails,
			ADRecipientSchema.UMRecipientDialPlanId,
			ADObjectSchema.WhenChangedUTC,
			ADObjectSchema.DistinguishedName,
			ADRecipientSchema.AddressListMembership
		};

		// Token: 0x020000A1 RID: 161
		public enum LookupPropertiesEnum
		{
			// Token: 0x0400036C RID: 876
			DisplayName,
			// Token: 0x0400036D RID: 877
			PhoneticDisplayName,
			// Token: 0x0400036E RID: 878
			PrimarySmtpAddress,
			// Token: 0x0400036F RID: 879
			Guid,
			// Token: 0x04000370 RID: 880
			RecipientType,
			// Token: 0x04000371 RID: 881
			RecipientTypeDetails,
			// Token: 0x04000372 RID: 882
			UMRecipientDialPlanId,
			// Token: 0x04000373 RID: 883
			WhenChangedUTC,
			// Token: 0x04000374 RID: 884
			DistinguishedName,
			// Token: 0x04000375 RID: 885
			AddressListMembership
		}
	}
}
