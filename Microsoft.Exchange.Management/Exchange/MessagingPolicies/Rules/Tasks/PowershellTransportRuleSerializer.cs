using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B60 RID: 2912
	internal class PowershellTransportRuleSerializer
	{
		// Token: 0x06006B0A RID: 27402 RVA: 0x001B70C4 File Offset: 0x001B52C4
		public static byte[] Serialize(IEnumerable<Rule> powershellRules)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-16", "yes");
				object[] array = new object[1];
				object[] array2 = array;
				int num = 0;
				XName name = XName.Get("rules");
				object[] array3 = new object[2];
				array3[0] = new XAttribute("name", "TransportVersioned");
				array3[1] = from rule in powershellRules
				select new XElement("rule", new object[]
				{
					new XAttribute(XName.Get("name"), rule.Name),
					new XAttribute(XName.Get("id"), rule.ImmutableId),
					new XAttribute(XName.Get("format"), "cmdlet"),
					new XElement(XName.Get("version"), new object[]
					{
						new XAttribute(XName.Get("requiredMinVersion"), "15.0.3.0"),
						new XElement(XName.Get("commandBlock"), new XCData(rule.ToCmdlet()))
					})
				});
				array2[num] = new XElement(name, array3);
				XDocument xdocument = new XDocument(declaration, array);
				xdocument.Save(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x001B7180 File Offset: 0x001B5380
		private static string GetDuplicateRuleName(IEnumerable<string> ruleNames)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in ruleNames)
			{
				string item = text.ToUpper();
				if (hashSet.Contains(item))
				{
					return text;
				}
				hashSet.Add(item);
			}
			return null;
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x001B7240 File Offset: 0x001B5440
		internal static IEnumerable<string> ParseStream(Stream data)
		{
			IEnumerable<string> result;
			try
			{
				XDocument xdocument = XDocument.Load(data);
				IEnumerable<string> ruleNames = from rule in xdocument.Elements("rules").Elements("rule")
				select rule.Attribute("name").Value;
				string duplicateRuleName = PowershellTransportRuleSerializer.GetDuplicateRuleName(ruleNames);
				if (duplicateRuleName != null)
				{
					throw new ParserException(RulesStrings.RuleNameExists(duplicateRuleName));
				}
				IEnumerable<string> enumerable = from version in xdocument.Elements("rules").Elements("rule").Elements("version")
				where PowershellTransportRuleSerializer.IsSupportedVersion(version.Attribute("requiredMinVersion").Value)
				select version.Element("commandBlock").Value.Trim();
				result = enumerable;
			}
			catch (NullReferenceException ex)
			{
				throw new XmlException("Malformed XML: " + ex.Message);
			}
			return result;
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x001B7350 File Offset: 0x001B5550
		internal static bool IsSupportedVersion(string versionString)
		{
			try
			{
				Version v = new Version(versionString);
				return v >= PowershellTransportRuleSerializer.LowestSupportedVersion && v <= PowershellTransportRuleSerializer.HighestSupportedVersion;
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			return false;
		}

		// Token: 0x040036EB RID: 14059
		internal static Version HighestSupportedVersion = new Version("15.0.3.0");

		// Token: 0x040036EC RID: 14060
		internal static Version LowestSupportedVersion = new Version("15.0.3.0");

		// Token: 0x02000B61 RID: 2913
		internal static class Declaration
		{
			// Token: 0x040036F1 RID: 14065
			public const string Version = "1.0";

			// Token: 0x040036F2 RID: 14066
			public const string Encoding = "utf-16";

			// Token: 0x040036F3 RID: 14067
			public const string StandAlone = "yes";
		}

		// Token: 0x02000B62 RID: 2914
		internal static class Attribute
		{
			// Token: 0x040036F4 RID: 14068
			public const string Name = "name";

			// Token: 0x040036F5 RID: 14069
			public const string Id = "id";

			// Token: 0x040036F6 RID: 14070
			public const string Format = "format";

			// Token: 0x040036F7 RID: 14071
			public const string State = "state";

			// Token: 0x040036F8 RID: 14072
			public const string RequiredMinVersion = "requiredMinVersion";
		}

		// Token: 0x02000B63 RID: 2915
		internal static class Element
		{
			// Token: 0x040036F9 RID: 14073
			public const string Rules = "rules";

			// Token: 0x040036FA RID: 14074
			public const string Rule = "rule";

			// Token: 0x040036FB RID: 14075
			public const string Version = "version";

			// Token: 0x040036FC RID: 14076
			public const string CommandBlock = "commandBlock";
		}

		// Token: 0x02000B64 RID: 2916
		internal static class Value
		{
			// Token: 0x040036FD RID: 14077
			public const string Container = "TransportVersioned";

			// Token: 0x040036FE RID: 14078
			public const string Format = "cmdlet";

			// Token: 0x040036FF RID: 14079
			public const string Version = "15.0.3.0";
		}
	}
}
