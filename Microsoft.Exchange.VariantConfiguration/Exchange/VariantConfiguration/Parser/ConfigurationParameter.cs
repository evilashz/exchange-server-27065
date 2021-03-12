using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000132 RID: 306
	internal class ConfigurationParameter
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x00022E28 File Offset: 0x00021028
		internal ConfigurationParameter(string name, string value, IEnumerable<KeyValuePair<string, string>> variants)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (variants == null)
			{
				throw new ArgumentNullException("variants");
			}
			this.Variants = variants;
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00022E66 File Offset: 0x00021066
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00022E6E File Offset: 0x0002106E
		public string Name { get; private set; }

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00022E77 File Offset: 0x00021077
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00022E7F File Offset: 0x0002107F
		public string Value { get; private set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00022E88 File Offset: 0x00021088
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00022E90 File Offset: 0x00021090
		public IEnumerable<KeyValuePair<string, string>> Variants { get; private set; }

		// Token: 0x06000E6E RID: 3694 RVA: 0x00022E9C File Offset: 0x0002109C
		public static ConfigurationParameter Create(string line)
		{
			string name;
			KeyValuePair<string, string>[] variants;
			string value;
			if (ConfigurationParameter.TryParseParameterLine(line, out name, out variants, out value))
			{
				return new ConfigurationParameter(name, value, variants);
			}
			throw new VariantConfigurationSyntaxException(string.Format("Unabled to parse line {0}", line));
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00022ED0 File Offset: 0x000210D0
		public static bool TryParseParameterLine(string line, out string parameterName, out KeyValuePair<string, string>[] variants, out string parameterValue)
		{
			bool result;
			try
			{
				Match match = ConfigurationParameter.ParameterRegex.Match(line);
				if (!match.Success)
				{
					parameterName = null;
					variants = null;
					parameterValue = null;
					result = false;
				}
				else
				{
					parameterName = match.Groups["PN"].Value;
					Group group = match.Groups["VN"];
					Group group2 = match.Groups["VV"];
					List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
					if (group.Success && group2.Success)
					{
						for (int i = 0; i < group.Captures.Count; i++)
						{
							list.Add(new KeyValuePair<string, string>(group.Captures[i].Value, group2.Captures[i].Value));
						}
					}
					variants = list.ToArray();
					parameterValue = match.Groups["PV"].Value;
					result = true;
				}
			}
			catch (Exception)
			{
				parameterName = null;
				variants = null;
				parameterValue = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00022FE0 File Offset: 0x000211E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format("{0} equals {1}", this.Name, this.Value));
			if (this.Variants.Count<KeyValuePair<string, string>>() > 0)
			{
				stringBuilder.Append(" if ");
				stringBuilder.Append(string.Join<KeyValuePair<string, string>>(" and ", this.Variants));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00023048 File Offset: 0x00021248
		public override bool Equals(object obj)
		{
			ConfigurationParameter configurationParameter = obj as ConfigurationParameter;
			return configurationParameter != null && string.Equals(this.Name, configurationParameter.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Value, configurationParameter.Value, StringComparison.OrdinalIgnoreCase) && this.Variants.SequenceEqual(configurationParameter.Variants);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0002309A File Offset: 0x0002129A
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.ToLowerInvariant().GetHashCode() ^ this.Value.ToLowerInvariant().GetHashCode() ^ this.CalculateVariantsHash();
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000230D0 File Offset: 0x000212D0
		private int CalculateVariantsHash()
		{
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.Variants)
			{
				num ^= keyValuePair.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000498 RID: 1176
		private static readonly string VariantValueUnquoted = "(?<VV>[\\w\\.\\-\\+\\^]+)";

		// Token: 0x04000499 RID: 1177
		private static readonly string VariantValueQuoted = "(?:\"(?<VV>(?:\"\"|[^\"])*)\")";

		// Token: 0x0400049A RID: 1178
		private static readonly string ParameterName = "[\\w\\.\\-\\+\\\\\\/\\{\\}\\(\\)#\\*\\$\\[\\]:]+";

		// Token: 0x0400049B RID: 1179
		private static readonly Regex ParameterRegex = new Regex(string.Format("^\\s*(?<PN>{0})\\s*((&\\s*(?<VN>[\\w\\.\\-\\+\\^]+)\\s*:\\s*(?:{1}|{2})\\s*)+)?=\\s*(?<PV>.*?)\\s*$", ConfigurationParameter.ParameterName, ConfigurationParameter.VariantValueUnquoted, ConfigurationParameter.VariantValueQuoted), RegexOptions.Compiled);
	}
}
