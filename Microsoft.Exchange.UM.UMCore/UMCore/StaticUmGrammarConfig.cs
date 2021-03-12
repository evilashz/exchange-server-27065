using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E7 RID: 487
	internal class StaticUmGrammarConfig : UMGrammarConfig
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x000407F2 File Offset: 0x0003E9F2
		internal StaticUmGrammarConfig(string name, string rule, string condition, ActivityManagerConfig managerConfig) : base(name, rule, condition, managerConfig)
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00040800 File Offset: 0x0003EA00
		internal override UMGrammar GetGrammar(ActivityManager manager, CultureInfo culture)
		{
			string path = Path.Combine(Utils.GrammarPathFromCulture(culture), base.GrammarName);
			return new UMGrammar(path, base.GrammarRule, culture);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0004082C File Offset: 0x0003EA2C
		internal override void Validate()
		{
			UMGrammar grammar = this.GetGrammar(null, GlobCfg.VuiCultures[0]);
			this.ValidateGrammar(grammar.Path, grammar.RuleName);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0004085E File Offset: 0x0003EA5E
		private static bool ValidateRuleName(XmlReader xmlReader, string ruleName)
		{
			if (xmlReader.MoveToFirstAttribute())
			{
				while (string.Compare(xmlReader.Name, "id", StringComparison.Ordinal) != 0 || string.Compare(xmlReader.Value, ruleName, StringComparison.Ordinal) != 0)
				{
					if (!xmlReader.MoveToNextAttribute())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00040895 File Offset: 0x0003EA95
		private static bool ValidateRuleScope(XmlReader xmlReader)
		{
			if (xmlReader.MoveToFirstAttribute())
			{
				while (string.Compare(xmlReader.Name, "scope", StringComparison.Ordinal) != 0 || string.Compare(xmlReader.Value, "public", StringComparison.Ordinal) != 0)
				{
					if (!xmlReader.MoveToNextAttribute())
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000408D0 File Offset: 0x0003EAD0
		private static bool ValidateRecoEventDeclaration(XmlReader xmlReader)
		{
			xmlReader.MoveToContent();
			xmlReader.Read();
			xmlReader.MoveToContent();
			return xmlReader.NodeType == XmlNodeType.Element && string.Compare(xmlReader.LocalName, "Tag", StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(xmlReader.ReadInnerXml(), "$.RecoEvent={};", StringComparison.Ordinal) == 0;
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00040924 File Offset: 0x0003EB24
		private static bool ValidateRecoEventTags(XmlReader xmlReader, out string invalidEventName)
		{
			invalidEventName = string.Empty;
			while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.EndElement || string.Compare(xmlReader.Name, "rule", StringComparison.OrdinalIgnoreCase) != 0))
			{
				if (xmlReader.NodeType == XmlNodeType.Element && string.Compare(xmlReader.LocalName, "Tag", StringComparison.OrdinalIgnoreCase) == 0)
				{
					Match match = Regex.Match(xmlReader.ReadInnerXml(), "\\s*\\$\\.RecoEvent._value=\"(?<eventName>[^\"]*)\"");
					if (match.Success && !ConstantValidator.Instance.ValidateRecoEvent(match.Groups["eventName"].Value))
					{
						invalidEventName = match.Groups["eventName"].Value;
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000409D8 File Offset: 0x0003EBD8
		private void ValidateGrammar(string path, string ruleName)
		{
			bool flag = false;
			bool flag2 = false;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (XmlReader xmlReader = XmlReader.Create(fileStream))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.NodeType == XmlNodeType.Element && string.Compare(xmlReader.LocalName, "rule", StringComparison.OrdinalIgnoreCase) == 0 && StaticUmGrammarConfig.ValidateRuleName(xmlReader, ruleName))
						{
							if (flag2)
							{
								throw new FsmConfigurationException(Strings.DuplicateGrammarRule(path, ruleName));
							}
							flag2 = true;
							if (!StaticUmGrammarConfig.ValidateRuleScope(xmlReader))
							{
								throw new FsmConfigurationException(Strings.RuleNotPublic(path, ruleName));
							}
							if (!StaticUmGrammarConfig.ValidateRecoEventDeclaration(xmlReader))
							{
								throw new FsmConfigurationException(Strings.InvalidRecoEventDeclaration(path, ruleName));
							}
							string name;
							if (!StaticUmGrammarConfig.ValidateRecoEventTags(xmlReader, out name))
							{
								throw new FsmConfigurationException(Strings.UndeclaredRecoEventName(path, ruleName, name));
							}
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				throw new FsmConfigurationException(Strings.UnknownGrammarRule(path, ruleName));
			}
		}
	}
}
