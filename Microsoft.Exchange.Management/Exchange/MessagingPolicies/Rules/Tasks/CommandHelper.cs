using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B52 RID: 2898
	internal static class CommandHelper
	{
		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x001B1728 File Offset: 0x001AF928
		private static IEnumerable<CmdletConfigurationEntry> ExchCmdletConfigurationEntries
		{
			get
			{
				if (CommandHelper.cmdletConfigEntries == null)
				{
					Assembly assembly = Assembly.Load("Microsoft.Exchange.PowerShell.Configuration");
					Type type = assembly.GetType("Microsoft.Exchange.Management.PowerShell.CmdletConfigurationEntries");
					CommandHelper.cmdletConfigEntries = new List<CmdletConfigurationEntry>();
					CommandHelper.cmdletConfigEntries.AddRange(CommandHelper.GetExchCmdletConfigEntries(type, "ExchangeNonEdgeCmdletConfigurationEntries"));
					CommandHelper.cmdletConfigEntries.AddRange(CommandHelper.GetExchCmdletConfigEntries(type, "ExchangeCmdletConfigurationEntries"));
					CommandHelper.cmdletConfigEntries.AddRange(CommandHelper.GetExchCmdletConfigEntries(type, "ExchangeEdgeCmdletConfigurationEntries"));
					CommandHelper.cmdletConfigEntries.AddRange(CommandHelper.GetExchCmdletConfigEntries(type, "ExchangeNonGallatinCmdletConfigurationEntries"));
				}
				return CommandHelper.cmdletConfigEntries;
			}
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x001B17B8 File Offset: 0x001AF9B8
		public static string AddOrganizationScope(string command, string organization)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (string.IsNullOrEmpty(organization))
			{
				return command;
			}
			Type cmdletImplementingType = CommandHelper.GetCmdletImplementingType(command);
			if (cmdletImplementingType == null)
			{
				return command;
			}
			if (CommandHelper.IsOrganizationParameterSupported(cmdletImplementingType))
			{
				return CommandHelper.AddOrganizationParameter(command, organization);
			}
			if (CommandHelper.IsIdentityParameterSupported(cmdletImplementingType))
			{
				return CommandHelper.AddOrganizationToIdentity(command, organization);
			}
			return command;
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x001B184C File Offset: 0x001AFA4C
		private static Type GetCmdletImplementingType(string command)
		{
			Match match = Regex.Match(command, "^\\s*(\\w+-\\w+)\\s");
			if (!match.Success)
			{
				return null;
			}
			CmdletConfigurationEntry cmdletConfigurationEntry = CommandHelper.ExchCmdletConfigurationEntries.FirstOrDefault((CmdletConfigurationEntry entry) => string.Compare(match.Groups[1].Captures[0].Value, entry.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
			if (cmdletConfigurationEntry == null)
			{
				return null;
			}
			return cmdletConfigurationEntry.ImplementingType;
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x001B18A4 File Offset: 0x001AFAA4
		private static IEnumerable<CmdletConfigurationEntry> GetExchCmdletConfigEntries(Type type, string propName)
		{
			CmdletConfigurationEntry[] array = null;
			PropertyInfo property = type.GetProperty(propName, BindingFlags.Static | BindingFlags.Public);
			if (property != null)
			{
				array = (CmdletConfigurationEntry[])property.GetValue(null, null);
			}
			return array ?? new CmdletConfigurationEntry[0];
		}

		// Token: 0x0600692A RID: 26922 RVA: 0x001B18F8 File Offset: 0x001AFAF8
		private static bool IsOrganizationParameterSupported(Type implementingType)
		{
			return CommandHelper.organizationTypes.Any((Type baseType) => CommandHelper.IsTypeDerivedFrom(implementingType, baseType));
		}

		// Token: 0x0600692B RID: 26923 RVA: 0x001B1940 File Offset: 0x001AFB40
		private static bool IsIdentityParameterSupported(Type implementingType)
		{
			return CommandHelper.identityTypes.Any((Type baseType) => CommandHelper.IsTypeDerivedFrom(implementingType, baseType));
		}

		// Token: 0x0600692C RID: 26924 RVA: 0x001B1970 File Offset: 0x001AFB70
		private static string AddOrganizationParameter(string command, string organization)
		{
			Match match = Regex.Match(command, "\\s-(?<param>\\w+)(:\\s*|\\s+)(?<value>\".*?\"(\\s*,\\s*\".*?\"){0,}|'.*?'(\\s*,\\s*'.*?'){0,}|\\@\\{.*?\\}(,\\s*\\@\\{.*?\\}){0,}|[\\w\\d\\$][\\w\\d-]+)");
			if (!CommandHelper.IsParameterSpecified(match, "Organization"))
			{
				return string.Format("{0} -{1} \"{2}\"", command, "Organization", organization);
			}
			return command;
		}

		// Token: 0x0600692D RID: 26925 RVA: 0x001B19AC File Offset: 0x001AFBAC
		private static string AddOrganizationToIdentity(string command, string organization)
		{
			Match match = Regex.Match(command, "\\s-(?<param>\\w+)(:\\s*|\\s+)(?<value>\".*?\"(\\s*,\\s*\".*?\"){0,}|'.*?'(\\s*,\\s*'.*?'){0,}|\\@\\{.*?\\}(,\\s*\\@\\{.*?\\}){0,}|[\\w\\d\\$][\\w\\d-]+)");
			if (CommandHelper.IsParameterSpecified(match, "Identity"))
			{
				CaptureCollection captures = match.Groups["param"].Captures;
				for (int i = 0; i < captures.Count; i++)
				{
					if (string.Compare(captures[i].Value, "Identity", StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return CommandHelper.ReplaceIdentityValue(match.Groups["value"].Captures[i], command, organization);
					}
				}
			}
			match = Regex.Match(command, "^\\s*\\w+-\\w+\\s+((?<id1>(?<quote>\"|').*?\\k<quote>)|(?<id2>[\\w\\d][\\w\\d\\.-]+))");
			if (match.Success)
			{
				return CommandHelper.ReplaceIdentityValue(match.Groups["id1"].Success ? match.Groups["id1"].Captures[0] : match.Groups["id2"].Captures[0], command, organization);
			}
			return command;
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x001B1AC0 File Offset: 0x001AFCC0
		private static bool IsParameterSpecified(Match match, string paramName)
		{
			if (match.Success && match.Groups["param"].Success && match.Groups["param"].Captures.Count > 0)
			{
				return match.Groups["param"].Captures.Cast<Capture>().Any((Capture capture) => string.Compare(capture.Value, paramName, StringComparison.InvariantCultureIgnoreCase) == 0);
			}
			return false;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x001B1B4C File Offset: 0x001AFD4C
		private static bool IsTypeDerivedFrom(Type type, Type baseType)
		{
			if (type != null && baseType != null)
			{
				if (CommandHelper.GetWeakerTypeName(type) == CommandHelper.GetWeakerTypeName(baseType))
				{
					return true;
				}
				if (type.BaseType != null)
				{
					return CommandHelper.IsTypeDerivedFrom(type.BaseType, baseType);
				}
			}
			return false;
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x001B1B9C File Offset: 0x001AFD9C
		private static string GetWeakerTypeName(Type type)
		{
			Match match = Regex.Match(type.FullName, "^(?<tname>[\\w\\d\\.]+(`\\d)?)(\\[\\[.*?\\]\\])?$");
			if (!match.Success)
			{
				return type.FullName;
			}
			return match.Groups["tname"].Captures[0].Value;
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x001B1BEC File Offset: 0x001AFDEC
		private static string ReplaceIdentityValue(Capture capture, string command, string organization)
		{
			string value = capture.Value;
			string text = CommandHelper.BuildStrongIdentity(value, organization);
			if (value != text)
			{
				StringBuilder stringBuilder = new StringBuilder(command.Substring(0, capture.Index));
				stringBuilder.Append(text);
				stringBuilder.Append(command.Substring(capture.Index + capture.Length));
				return stringBuilder.ToString();
			}
			return command;
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x001B1C50 File Offset: 0x001AFE50
		private static string BuildStrongIdentity(string identity, string organization)
		{
			Match match = Regex.Match(identity, "^\\s*(?<quote>\"|')(?<id>.*?)(?<right>\\k<quote>\\s*)$");
			if (match.Success)
			{
				return string.Format("{0}{1}\\{2}{3}", new object[]
				{
					match.Groups["quote"].Captures[0].Value,
					organization,
					match.Groups["id"].Captures[0].Value,
					match.Groups["right"].Captures[0].Value
				});
			}
			return Regex.Replace(identity, "^(\\S+)$", string.Format("\"{0}\\$1\"", organization));
		}

		// Token: 0x040036A8 RID: 13992
		private const string regexCmdlet = "^\\s*(\\w+-\\w+)\\s";

		// Token: 0x040036A9 RID: 13993
		private const string regexTypeName = "^(?<tname>[\\w\\d\\.]+(`\\d)?)(\\[\\[.*?\\]\\])?$";

		// Token: 0x040036AA RID: 13994
		private const string regexQuotedValue = "^\\s*(?<quote>\"|')(?<id>.*?)(?<right>\\k<quote>\\s*)$";

		// Token: 0x040036AB RID: 13995
		private const string regexIdentityPos = "^\\s*\\w+-\\w+\\s+((?<id1>(?<quote>\"|').*?\\k<quote>)|(?<id2>[\\w\\d][\\w\\d\\.-]+))";

		// Token: 0x040036AC RID: 13996
		private const string regexParams = "\\s-(?<param>\\w+)(:\\s*|\\s+)(?<value>\".*?\"(\\s*,\\s*\".*?\"){0,}|'.*?'(\\s*,\\s*'.*?'){0,}|\\@\\{.*?\\}(,\\s*\\@\\{.*?\\}){0,}|[\\w\\d\\$][\\w\\d-]+)";

		// Token: 0x040036AD RID: 13997
		private static List<CmdletConfigurationEntry> cmdletConfigEntries;

		// Token: 0x040036AE RID: 13998
		private static readonly Type[] organizationTypes = new Type[]
		{
			typeof(NewRecipientObjectTaskBase<>),
			typeof(NewMultitenancySystemConfigurationObjectTask<>),
			typeof(NewMultitenancyFixedNameSystemConfigurationObjectTask<>),
			typeof(GetMultitenancySystemConfigurationObjectTask<, >)
		};

		// Token: 0x040036AF RID: 13999
		private static readonly Type[] identityTypes = new Type[]
		{
			typeof(RemoveTaskBase<, >),
			typeof(SetObjectWithIdentityTaskBase<, , >)
		};
	}
}
