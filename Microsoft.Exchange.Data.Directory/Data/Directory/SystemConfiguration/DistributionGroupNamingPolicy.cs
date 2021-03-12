using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	public class DistributionGroupNamingPolicy
	{
		// Token: 0x0600202D RID: 8237 RVA: 0x0008EDA4 File Offset: 0x0008CFA4
		static DistributionGroupNamingPolicy()
		{
			DistributionGroupNamingPolicy.placeHolders.Add("<GroupName>");
			DistributionGroupNamingPolicy.placeHolders.Add("<<");
			DistributionGroupNamingPolicy.placeHolders.Add(">>");
			string[] array = DistributionGroupNamingPolicy.placeHolders.ToArray();
			DistributionGroupNamingPolicy.placeHoldersString = string.Join(", ", array);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("^(([^<>]*)");
			foreach (string text in array)
			{
				if (text == "<GroupName>")
				{
					text = "?<originalName>" + text;
				}
				stringBuilder.AppendFormat("|({0})", text);
			}
			stringBuilder.Append(")*$");
			DistributionGroupNamingPolicy.namingPolicyValidationRegex = new Regex(stringBuilder.ToString(), RegexOptions.Compiled | RegexOptions.CultureInvariant);
			string pattern = string.Join("|", array);
			DistributionGroupNamingPolicy.placeHoldersReplacementRegex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
			StringBuilder stringBuilder2 = new StringBuilder();
			List<string> list = DistributionGroupNamingPolicy.propertyMapping.Keys.ToList<string>();
			list.Add("<GroupName>");
			for (int j = 0; j < list.Count; j++)
			{
				string arg = array[j];
				stringBuilder2.AppendFormat("({0}(?!>))", arg);
				if (j < list.Count - 1)
				{
					stringBuilder2.Append("|");
				}
			}
			DistributionGroupNamingPolicy.splitRegex = new Regex(stringBuilder2.ToString(), RegexOptions.Compiled | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x0008F0FF File Offset: 0x0008D2FF
		private DistributionGroupNamingPolicy(string namingPolicy)
		{
			this.namingPolicyString = namingPolicy;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x0008F110 File Offset: 0x0008D310
		public static DistributionGroupNamingPolicy Parse(string namingPolicy)
		{
			DistributionGroupNamingPolicy result = null;
			if (!DistributionGroupNamingPolicy.TryParse(namingPolicy, out result))
			{
				throw new FormatException(DirectoryStrings.InvalidDistributionGroupNamingPolicyFormat(namingPolicy, DistributionGroupNamingPolicy.placeHoldersString));
			}
			return result;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x0008F140 File Offset: 0x0008D340
		public static bool TryParse(string namingPolicy, out DistributionGroupNamingPolicy instance)
		{
			if (string.IsNullOrEmpty(namingPolicy))
			{
				instance = new DistributionGroupNamingPolicy(namingPolicy);
				return true;
			}
			if (char.IsWhiteSpace(namingPolicy[0]) || char.IsWhiteSpace(namingPolicy[namingPolicy.Length - 1]))
			{
				instance = null;
				return false;
			}
			Match match = DistributionGroupNamingPolicy.namingPolicyValidationRegex.Match(namingPolicy);
			if (!match.Success || match.Groups["originalName"].Captures.Count != 1)
			{
				instance = null;
				return false;
			}
			instance = new DistributionGroupNamingPolicy(namingPolicy);
			return true;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0008F294 File Offset: 0x0008D494
		public string GetAppliedName(string originalName, ADUser user)
		{
			if (this.IsEmpty)
			{
				return originalName;
			}
			return DistributionGroupNamingPolicy.placeHoldersReplacementRegex.Replace(this.namingPolicyString, delegate(Match match)
			{
				string value = match.Value;
				if (value == "<GroupName>")
				{
					return originalName;
				}
				if (value == "<<")
				{
					return "<";
				}
				if (value == ">>")
				{
					return ">";
				}
				if (!DistributionGroupNamingPolicy.propertyMapping.ContainsKey(value))
				{
					return match.Value;
				}
				if (value == "<CountryCode>")
				{
					string result = string.Empty;
					if (user.CountryOrRegion != null)
					{
						result = user.CountryOrRegion.CountryCode.ToString();
					}
					return result;
				}
				return this.GetAttributeInString(user[DistributionGroupNamingPolicy.propertyMapping[value]]);
			});
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0008F2E7 File Offset: 0x0008D4E7
		private static string UnescapeText(string text)
		{
			return text.Replace("<<", "<").Replace(">>", ">");
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0008F308 File Offset: 0x0008D508
		private string GetAttributeInString(object attribute)
		{
			if (attribute == null)
			{
				return string.Empty;
			}
			return attribute.ToString();
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0008F319 File Offset: 0x0008D519
		public static string GroupNameLocString
		{
			get
			{
				return DirectoryStrings.GroupNameInNamingPolicy;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0008F325 File Offset: 0x0008D525
		public string[] PrefixElements
		{
			get
			{
				this.Split();
				return this.prefix;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0008F333 File Offset: 0x0008D533
		public string[] SuffixElements
		{
			get
			{
				this.Split();
				return this.suffix;
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x0008F344 File Offset: 0x0008D544
		private void Split()
		{
			if (this.IsEmpty)
			{
				return;
			}
			if (this.prefix != null || this.suffix != null)
			{
				return;
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			bool flag = true;
			foreach (string text in DistributionGroupNamingPolicy.splitRegex.Split(this.namingPolicyString))
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (text == "<GroupName>")
					{
						flag = false;
					}
					else if (flag)
					{
						list.Add(text);
					}
					else
					{
						list2.Add(text);
					}
				}
			}
			this.prefix = list.ToArray();
			this.suffix = list2.ToArray();
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0008F3E8 File Offset: 0x0008D5E8
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.namingPolicyString);
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0008F3F5 File Offset: 0x0008D5F5
		public override string ToString()
		{
			return this.namingPolicyString;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x0008F400 File Offset: 0x0008D600
		public string ToLocalizedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}<{1}>{2}", this.GetLocalizedString(this.PrefixElements), DistributionGroupNamingPolicy.GroupNameLocString, this.GetLocalizedString(this.SuffixElements));
			return stringBuilder.ToString();
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0008F444 File Offset: 0x0008D644
		private string GetLocalizedString(string[] elements)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in elements)
			{
				if (text.Length > 1 && text[0] == '<' && text[text.Length - 1] == '>')
				{
					string value = text.Substring(1, text.Length - 2);
					if (Enum.IsDefined(typeof(GroupNamingPolicyAttributeEnum), value))
					{
						GroupNamingPolicyAttributeEnum groupNamingPolicyAttributeEnum = (GroupNamingPolicyAttributeEnum)Enum.Parse(typeof(GroupNamingPolicyAttributeEnum), value);
						stringBuilder.AppendFormat("<{0}>", LocalizedDescriptionAttribute.FromEnum(typeof(GroupNamingPolicyAttributeEnum), groupNamingPolicyAttributeEnum));
					}
					else
					{
						stringBuilder.Append(DistributionGroupNamingPolicy.UnescapeText(text));
					}
				}
				else
				{
					stringBuilder.Append(DistributionGroupNamingPolicy.UnescapeText(text));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400137D RID: 4989
		private const string OriginalGroupNamePlaceHolder = "<GroupName>";

		// Token: 0x0400137E RID: 4990
		private static readonly Dictionary<string, ADPropertyDefinition> propertyMapping = new Dictionary<string, ADPropertyDefinition>
		{
			{
				"<Department>",
				ADOrgPersonSchema.Department
			},
			{
				"<Company>",
				ADOrgPersonSchema.Company
			},
			{
				"<Office>",
				ADOrgPersonSchema.Office
			},
			{
				"<City>",
				ADOrgPersonSchema.City
			},
			{
				"<StateOrProvince>",
				ADOrgPersonSchema.StateOrProvince
			},
			{
				"<CountryOrRegion>",
				ADOrgPersonSchema.CountryOrRegion
			},
			{
				"<CountryCode>",
				ADOrgPersonSchema.CountryCode
			},
			{
				"<Title>",
				ADOrgPersonSchema.Title
			},
			{
				"<CustomAttribute1>",
				ADRecipientSchema.CustomAttribute1
			},
			{
				"<CustomAttribute2>",
				ADRecipientSchema.CustomAttribute2
			},
			{
				"<CustomAttribute3>",
				ADRecipientSchema.CustomAttribute3
			},
			{
				"<CustomAttribute4>",
				ADRecipientSchema.CustomAttribute4
			},
			{
				"<CustomAttribute5>",
				ADRecipientSchema.CustomAttribute5
			},
			{
				"<CustomAttribute6>",
				ADRecipientSchema.CustomAttribute6
			},
			{
				"<CustomAttribute7>",
				ADRecipientSchema.CustomAttribute7
			},
			{
				"<CustomAttribute8>",
				ADRecipientSchema.CustomAttribute8
			},
			{
				"<CustomAttribute9>",
				ADRecipientSchema.CustomAttribute9
			},
			{
				"<CustomAttribute10>",
				ADRecipientSchema.CustomAttribute10
			},
			{
				"<CustomAttribute11>",
				ADRecipientSchema.CustomAttribute11
			},
			{
				"<CustomAttribute12>",
				ADRecipientSchema.CustomAttribute12
			},
			{
				"<CustomAttribute13>",
				ADRecipientSchema.CustomAttribute13
			},
			{
				"<CustomAttribute14>",
				ADRecipientSchema.CustomAttribute14
			},
			{
				"<CustomAttribute15>",
				ADRecipientSchema.CustomAttribute15
			},
			{
				"<ExtensionCustomAttribute1>",
				ADRecipientSchema.ExtensionCustomAttribute1
			},
			{
				"<ExtensionCustomAttribute2>",
				ADRecipientSchema.ExtensionCustomAttribute2
			},
			{
				"<ExtensionCustomAttribute3>",
				ADRecipientSchema.ExtensionCustomAttribute3
			},
			{
				"<ExtensionCustomAttribute4>",
				ADRecipientSchema.ExtensionCustomAttribute4
			},
			{
				"<ExtensionCustomAttribute5>",
				ADRecipientSchema.ExtensionCustomAttribute5
			}
		};

		// Token: 0x0400137F RID: 4991
		private static readonly List<string> placeHolders = DistributionGroupNamingPolicy.propertyMapping.Keys.ToList<string>();

		// Token: 0x04001380 RID: 4992
		private static readonly string placeHoldersString;

		// Token: 0x04001381 RID: 4993
		private static readonly Regex namingPolicyValidationRegex;

		// Token: 0x04001382 RID: 4994
		private static readonly Regex placeHoldersReplacementRegex;

		// Token: 0x04001383 RID: 4995
		private static readonly Regex splitRegex;

		// Token: 0x04001384 RID: 4996
		private string namingPolicyString;

		// Token: 0x04001385 RID: 4997
		private string[] prefix;

		// Token: 0x04001386 RID: 4998
		private string[] suffix;
	}
}
