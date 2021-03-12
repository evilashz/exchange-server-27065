using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000954 RID: 2388
	[Serializable]
	internal class DlpPolicyTemplateMetaData
	{
		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06005532 RID: 21810 RVA: 0x0015ED64 File Offset: 0x0015CF64
		// (set) Token: 0x06005533 RID: 21811 RVA: 0x0015EDAB File Offset: 0x0015CFAB
		internal string Name
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.name))
				{
					return this.name;
				}
				if (this.LocalizedNames.Any<KeyValuePair<string, string>>())
				{
					return this.LocalizedNames.First<KeyValuePair<string, string>>().Value;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06005534 RID: 21812 RVA: 0x0015EDB4 File Offset: 0x0015CFB4
		// (set) Token: 0x06005535 RID: 21813 RVA: 0x0015EDBC File Offset: 0x0015CFBC
		internal string Version { get; set; }

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x0015EDC5 File Offset: 0x0015CFC5
		// (set) Token: 0x06005537 RID: 21815 RVA: 0x0015EDCD File Offset: 0x0015CFCD
		internal RuleState State { get; set; }

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06005538 RID: 21816 RVA: 0x0015EDD6 File Offset: 0x0015CFD6
		// (set) Token: 0x06005539 RID: 21817 RVA: 0x0015EDDE File Offset: 0x0015CFDE
		internal RuleMode Mode { get; set; }

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x0600553A RID: 21818 RVA: 0x0015EDE7 File Offset: 0x0015CFE7
		// (set) Token: 0x0600553B RID: 21819 RVA: 0x0015EDEF File Offset: 0x0015CFEF
		internal Guid ImmutableId { get; set; }

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x0015EDF8 File Offset: 0x0015CFF8
		// (set) Token: 0x0600553D RID: 21821 RVA: 0x0015EE00 File Offset: 0x0015D000
		internal string ContentVersion { get; set; }

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x0015EE09 File Offset: 0x0015D009
		// (set) Token: 0x0600553F RID: 21823 RVA: 0x0015EE11 File Offset: 0x0015D011
		internal string PublisherName { get; set; }

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06005540 RID: 21824 RVA: 0x0015EE1A File Offset: 0x0015D01A
		// (set) Token: 0x06005541 RID: 21825 RVA: 0x0015EE22 File Offset: 0x0015D022
		internal Dictionary<string, string> LocalizedNames { get; set; }

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x0015EE2B File Offset: 0x0015D02B
		// (set) Token: 0x06005543 RID: 21827 RVA: 0x0015EE33 File Offset: 0x0015D033
		internal Dictionary<string, string> LocalizedDescriptions { get; set; }

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06005544 RID: 21828 RVA: 0x0015EE3C File Offset: 0x0015D03C
		// (set) Token: 0x06005545 RID: 21829 RVA: 0x0015EE44 File Offset: 0x0015D044
		internal List<Dictionary<string, string>> LocalizedKeywords { get; set; }

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06005546 RID: 21830 RVA: 0x0015EE4D File Offset: 0x0015D04D
		// (set) Token: 0x06005547 RID: 21831 RVA: 0x0015EE55 File Offset: 0x0015D055
		internal List<DlpTemplateRuleParameter> RuleParameters { get; set; }

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06005548 RID: 21832 RVA: 0x0015EE5E File Offset: 0x0015D05E
		// (set) Token: 0x06005549 RID: 21833 RVA: 0x0015EE66 File Offset: 0x0015D066
		internal List<string> PolicyCommands { get; set; }

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x0015EE6F File Offset: 0x0015D06F
		// (set) Token: 0x0600554B RID: 21835 RVA: 0x0015EE77 File Offset: 0x0015D077
		internal Dictionary<string, Dictionary<string, string>> LocalizedPolicyCommandResources { get; set; }

		// Token: 0x0600554C RID: 21836 RVA: 0x0015EFDC File Offset: 0x0015D1DC
		internal void Validate()
		{
			if (this.LocalizedNames == null || !this.LocalizedNames.Any<KeyValuePair<string, string>>() || string.IsNullOrEmpty(this.Version) || this.ImmutableId == Guid.Empty || string.IsNullOrEmpty(this.ContentVersion) || string.IsNullOrEmpty(this.PublisherName) || this.LocalizedDescriptions == null || !this.LocalizedDescriptions.Any<KeyValuePair<string, string>>() || this.PolicyCommands == null || !this.PolicyCommands.Any<string>())
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements);
			}
			if (new Version(this.Version) > DlpUtils.MaxSupportedVersion)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyVersionUnsupported);
			}
			if (this.LocalizedKeywords.Any((Dictionary<string, string> keywords) => keywords.Any((KeyValuePair<string, string> keyword) => string.IsNullOrEmpty(keyword.Value))))
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyContainsEmptyKeywords);
			}
			DlpPolicyTemplateMetaData.ValidateFieldSize("version", this.Version, 16);
			DlpPolicyTemplateMetaData.ValidateFieldSize("contentVersion", this.ContentVersion, 16);
			DlpPolicyTemplateMetaData.ValidateFieldSize("publisherName", this.PublisherName, 256);
			this.LocalizedNames.Values.ToList<string>().ForEach(delegate(string x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("name", x, 64);
			});
			this.LocalizedDescriptions.Values.ToList<string>().ForEach(delegate(string x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("description", x, 1024);
			});
			this.LocalizedKeywords.ToList<Dictionary<string, string>>().ForEach(delegate(Dictionary<string, string> keywords)
			{
				keywords.Values.ToList<string>().ForEach(delegate(string keyword)
				{
					DlpPolicyTemplateMetaData.ValidateFieldSize("keyword", keyword, 64);
				});
			});
			List<DlpTemplateRuleParameter> list = this.RuleParameters.ToList<DlpTemplateRuleParameter>();
			list.ForEach(delegate(DlpTemplateRuleParameter x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("type", x.Type, 32);
			});
			list.ForEach(delegate(DlpTemplateRuleParameter x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("token", x.Token, 32);
			});
			list.ForEach(delegate(DlpTemplateRuleParameter x)
			{
				x.LocalizedDescriptions.ToList<KeyValuePair<string, string>>().ForEach(delegate(KeyValuePair<string, string> y)
				{
					DlpPolicyTemplateMetaData.ValidateFieldSize("description", y.Value, 1024);
				});
			});
			this.LocalizedPolicyCommandResources.Values.ToList<Dictionary<string, string>>().ForEach(delegate(Dictionary<string, string> resources)
			{
				resources.Values.ToList<string>().ForEach(delegate(string resource)
				{
					DlpPolicyTemplateMetaData.ValidateFieldSize("resource", resource, 1024);
				});
			});
			List<string> list2 = this.PolicyCommands.ToList<string>();
			list2.ForEach(delegate(string x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("commandBlock", x, 4096);
			});
			list2.ForEach(delegate(string command)
			{
				DlpPolicyTemplateMetaData.ValidateCmdletParameters(command);
			});
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x0015F295 File Offset: 0x0015D495
		internal static void ValidateFieldSize(string fieldName, string value, int sizeLimit)
		{
			if (value.Length > sizeLimit)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyFieldLengthsExceedsLimit(fieldName, sizeLimit));
			}
		}

		// Token: 0x0600554E RID: 21838 RVA: 0x0015F2B4 File Offset: 0x0015D4B4
		internal static void ValidateCmdletParameters(string cmdlet)
		{
			CmdletValidator cmdletValidator = new CmdletValidator(DlpPolicyTemplateMetaData.AllowedCommands, DlpPolicyTemplateMetaData.RequiredParams, DlpPolicyTemplateMetaData.NotAllowedParams);
			if (!cmdletValidator.ValidateCmdlet(cmdlet))
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyNotSupportedCmdlet(cmdlet));
			}
		}

		// Token: 0x0600554F RID: 21839 RVA: 0x0015F300 File Offset: 0x0015D500
		internal ADComplianceProgram ToAdObject()
		{
			string transportRulesXml;
			using (MemoryStream memoryStream = new MemoryStream(DlpPolicyParser.SerializeDlpPolicyTemplate(this)))
			{
				StreamReader streamReader = new StreamReader(memoryStream);
				transportRulesXml = streamReader.ReadToEnd();
			}
			ADComplianceProgram adcomplianceProgram = new ADComplianceProgram();
			adcomplianceProgram.Name = DlpPolicyTemplateMetaData.GetLocalizedStringValue(this.LocalizedNames, null);
			adcomplianceProgram.Description = DlpPolicyTemplateMetaData.GetLocalizedStringValue(this.LocalizedDescriptions, null);
			adcomplianceProgram.ImmutableId = this.ImmutableId;
			adcomplianceProgram.Keywords = (from keyword in this.LocalizedKeywords
			select DlpPolicyTemplateMetaData.GetLocalizedStringValue(keyword, DlpPolicyTemplateMetaData.DefaultCulture)).ToArray<string>();
			adcomplianceProgram.PublisherName = this.PublisherName;
			adcomplianceProgram.State = DlpUtils.RuleStateToDlpState(this.State, this.Mode);
			adcomplianceProgram.TransportRulesXml = transportRulesXml;
			adcomplianceProgram.Version = this.Version;
			return adcomplianceProgram;
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x0015F3E4 File Offset: 0x0015D5E4
		internal static string GetLocalizedStringValue(IDictionary<string, string> localizedStrings, CultureInfo culture = null)
		{
			if (culture == null)
			{
				culture = DlpPolicyTemplateMetaData.DefaultCulture;
			}
			if (!localizedStrings.Any<KeyValuePair<string, string>>())
			{
				return string.Empty;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(localizedStrings, StringComparer.CurrentCultureIgnoreCase);
			int num = 5;
			while (num-- > 0)
			{
				if (dictionary.ContainsKey(culture.Name))
				{
					return dictionary[culture.Name];
				}
				culture = culture.Parent;
				if (culture == CultureInfo.InvariantCulture)
				{
					break;
				}
			}
			if (dictionary.ContainsKey(DlpPolicyTemplateMetaData.DefaultCulture.Name))
			{
				return dictionary[DlpPolicyTemplateMetaData.DefaultCulture.Name];
			}
			return dictionary.FirstOrDefault<KeyValuePair<string, string>>().Value;
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0015F500 File Offset: 0x0015D700
		internal static IEnumerable<string> LocalizeCmdlets(IEnumerable<string> policyCommands, Dictionary<string, Dictionary<string, string>> policyCommandResources, CultureInfo culture)
		{
			IDictionary<string, string> localizedResources = (from localizedResource in policyCommandResources
			select new KeyValuePair<string, string>(localizedResource.Key, DlpPolicyTemplateMetaData.GetLocalizedStringValue(localizedResource.Value, culture))).ToDictionary((KeyValuePair<string, string> pair) => pair.Key, (KeyValuePair<string, string> pair) => pair.Value, StringComparer.OrdinalIgnoreCase);
			return from cmdlet in policyCommands
			select localizedResources.Aggregate(cmdlet, (string current, KeyValuePair<string, string> parameter) => current.Replace(parameter.Key, parameter.Value).Trim());
		}

		// Token: 0x04003149 RID: 12617
		internal static readonly CultureInfo DefaultCulture = new CultureInfo("en");

		// Token: 0x0400314A RID: 12618
		private static readonly HashSet<string> AllowedCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"New-TransportRule"
		};

		// Token: 0x0400314B RID: 12619
		private static readonly Dictionary<string, HashSet<string>> NotAllowedParams = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"New-TransportRule",
				new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"-Organization"
				}
			}
		};

		// Token: 0x0400314C RID: 12620
		private static readonly Dictionary<string, HashSet<string>> RequiredParams = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"New-TransportRule",
				new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"-DlpPolicy"
				}
			}
		};

		// Token: 0x0400314D RID: 12621
		private string name;
	}
}
