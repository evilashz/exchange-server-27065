using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000956 RID: 2390
	internal class DlpPolicyParser
	{
		// Token: 0x06005580 RID: 21888 RVA: 0x0015FA24 File Offset: 0x0015DC24
		internal static IEnumerable<DlpPolicyTemplateMetaData> ParseDlpPolicyTemplates(Stream data)
		{
			List<DlpPolicyTemplateMetaData> result;
			try
			{
				XDocument xdocument = XDocument.Load(data);
				result = (from dlpPolicyTemplate in xdocument.Element("dlpPolicyTemplates").Elements("dlpPolicyTemplate")
				select DlpPolicyParser.ParseDlpPolicyTemplate(dlpPolicyTemplate.ToString())).ToList<DlpPolicyTemplateMetaData>();
			}
			catch (ArgumentException innerException)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyStateStateInvalid, innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements, innerException2);
			}
			catch (XmlException innerException3)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlInvalid, innerException3);
			}
			return result;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x0015FAF4 File Offset: 0x0015DCF4
		internal static IEnumerable<DlpPolicyMetaData> ParseDlpPolicyInstances(Stream data)
		{
			List<DlpPolicyMetaData> result;
			try
			{
				XDocument xdocument = XDocument.Load(data);
				result = (from dlpPolicy in xdocument.Element("dlpPolicies").Elements("dlpPolicy")
				select DlpPolicyParser.ParseDlpPolicyInstance(dlpPolicy.ToString())).ToList<DlpPolicyMetaData>();
			}
			catch (ArgumentException innerException)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyStateStateInvalid, innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements, innerException2);
			}
			catch (XmlException innerException3)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlInvalid, innerException3);
			}
			return result;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x0015FC94 File Offset: 0x0015DE94
		internal static DlpPolicyTemplateMetaData ParseDlpPolicyTemplate(string data)
		{
			DlpPolicyTemplateMetaData result;
			try
			{
				XElement root = XDocument.Parse(data).Root;
				DlpPolicyTemplateMetaData dlpPolicyTemplateMetaData = new DlpPolicyTemplateMetaData();
				dlpPolicyTemplateMetaData.Version = root.Attribute("version").Value.Trim();
				dlpPolicyTemplateMetaData.State = (RuleState)Enum.Parse(typeof(RuleState), root.Attribute("state").Value.Trim());
				dlpPolicyTemplateMetaData.Mode = (RuleMode)Enum.Parse(typeof(RuleMode), root.Attribute("mode").Value.Trim());
				dlpPolicyTemplateMetaData.ImmutableId = ((root.Attribute("id") == null) ? Guid.Empty : Guid.Parse(root.Attribute("id").Value.Trim()));
				dlpPolicyTemplateMetaData.ContentVersion = root.Element("contentVersion").Value.Trim();
				dlpPolicyTemplateMetaData.PublisherName = root.Element("publisherName").Value.Trim();
				dlpPolicyTemplateMetaData.LocalizedNames = DlpPolicyParser.ParseLocalizedString(root.Element("name"));
				dlpPolicyTemplateMetaData.LocalizedDescriptions = DlpPolicyParser.ParseLocalizedString(root.Element("description"));
				dlpPolicyTemplateMetaData.LocalizedKeywords = (from localizedKeyword in root.Element("keywords").Elements("keyword")
				select DlpPolicyParser.ParseLocalizedString(localizedKeyword)).ToList<Dictionary<string, string>>();
				dlpPolicyTemplateMetaData.RuleParameters = (from ruleParameter in root.Element("ruleParameters").Elements("ruleParameter")
				select new DlpTemplateRuleParameter
				{
					Type = ruleParameter.Attribute("type").Value.Trim(),
					Required = bool.Parse(ruleParameter.Attribute("required").Value),
					Token = ruleParameter.Attribute("token").Value.Trim(),
					LocalizedDescriptions = DlpPolicyParser.ParseLocalizedString(ruleParameter.Element("description"))
				}).ToList<DlpTemplateRuleParameter>();
				dlpPolicyTemplateMetaData.PolicyCommands = (from policyCommand in root.Element("policyCommands").Elements("commandBlock")
				select policyCommand.Value.Trim()).ToList<string>();
				dlpPolicyTemplateMetaData.LocalizedPolicyCommandResources = (from localizedResource in root.Element("policyCommandsResources").Elements("resource")
				select new KeyValuePair<string, Dictionary<string, string>>(localizedResource.Attribute("token").Value.Trim(), DlpPolicyParser.ParseLocalizedString(localizedResource))).ToDictionary((KeyValuePair<string, Dictionary<string, string>> pair) => pair.Key, (KeyValuePair<string, Dictionary<string, string>> pair) => pair.Value, StringComparer.OrdinalIgnoreCase);
				DlpPolicyTemplateMetaData dlpPolicyTemplateMetaData2 = dlpPolicyTemplateMetaData;
				dlpPolicyTemplateMetaData2.Validate();
				result = dlpPolicyTemplateMetaData2;
			}
			catch (ArgumentException innerException)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyStateStateInvalid, innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements, innerException2);
			}
			catch (XmlException innerException3)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlInvalid, innerException3);
			}
			return result;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00160010 File Offset: 0x0015E210
		internal static DlpPolicyMetaData ParseDlpPolicyInstance(string data)
		{
			DlpPolicyMetaData result;
			try
			{
				XElement root = XDocument.Parse(data).Root;
				DlpPolicyMetaData dlpPolicyMetaData = new DlpPolicyMetaData();
				dlpPolicyMetaData.Version = root.Attribute("version").Value.Trim();
				dlpPolicyMetaData.State = (RuleState)Enum.Parse(typeof(RuleState), root.Attribute("state").Value.Trim());
				dlpPolicyMetaData.Mode = (RuleMode)Enum.Parse(typeof(RuleMode), root.Attribute("mode").Value.Trim());
				dlpPolicyMetaData.ImmutableId = ((root.Attribute("id") == null) ? Guid.Empty : Guid.Parse(root.Attribute("id").Value.Trim()));
				dlpPolicyMetaData.ContentVersion = root.Element("contentVersion").Value.Trim();
				dlpPolicyMetaData.PublisherName = root.Element("publisherName").Value.Trim();
				dlpPolicyMetaData.Name = root.Element("name").Value.Trim();
				dlpPolicyMetaData.Description = root.Element("description").Value.Trim();
				dlpPolicyMetaData.Keywords = (from keyword in root.Element("keywords").Elements("keyword")
				select keyword.Value.Trim()).ToList<string>();
				dlpPolicyMetaData.PolicyCommands = (from policyCommand in root.Element("policyCommands").Elements("commandBlock")
				select policyCommand.Value.Trim()).ToList<string>();
				DlpPolicyMetaData dlpPolicyMetaData2 = dlpPolicyMetaData;
				dlpPolicyMetaData2.Validate();
				result = dlpPolicyMetaData2;
			}
			catch (ArgumentException innerException)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyStateStateInvalid, innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements, innerException2);
			}
			catch (XmlException innerException3)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlInvalid, innerException3);
			}
			return result;
		}

		// Token: 0x06005584 RID: 21892 RVA: 0x001602E4 File Offset: 0x0015E4E4
		internal static Dictionary<string, string> ParseLocalizedString(XElement element)
		{
			Dictionary<string, string> result;
			try
			{
				Dictionary<string, string> dictionary = (from localizedString in element.Elements("localizedString")
				select new KeyValuePair<string, string>(localizedString.Attribute("lang").Value.Trim(), localizedString.Value.Trim())).ToDictionary((KeyValuePair<string, string> pair) => pair.Key, (KeyValuePair<string, string> pair) => pair.Value, StringComparer.OrdinalIgnoreCase);
				string text;
				if (!dictionary.TryGetValue(DlpPolicyTemplateMetaData.DefaultCulture.Name, out text))
				{
					throw new DlpPolicyParsingException(Strings.DlpPolicyMissingEnString(element.Name.ToString(), element.ToString()));
				}
				result = dictionary;
			}
			catch (NullReferenceException innerException)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyMissingLocaleAttribute(element.Name.ToString(), element.ToString()), innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyDuplicateLocalizedString(element.Name.ToString(), element.ToString()), innerException2);
			}
			return result;
		}

		// Token: 0x06005585 RID: 21893 RVA: 0x00160414 File Offset: 0x0015E614
		public static byte[] SerializeDlpPolicyInstances(IEnumerable<DlpPolicyMetaData> dlpPolicies)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement(XName.Get("dlpPolicies"), from dlpPolicy in dlpPolicies
				select DlpPolicyParser.CreateDlpPolicyXelement(dlpPolicy, true));
				XDocument xdocument = new XDocument(declaration, array);
				xdocument.Save(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06005586 RID: 21894 RVA: 0x001604AC File Offset: 0x0015E6AC
		public static byte[] SerializeDlpPolicyInstance(DlpPolicyMetaData dlpPolicyMetaData)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XElement xelement = DlpPolicyParser.CreateDlpPolicyXelement(dlpPolicyMetaData, false);
				xelement.Save(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06005587 RID: 21895 RVA: 0x00160520 File Offset: 0x0015E720
		private static XElement CreateDlpPolicyXelement(DlpPolicyMetaData dlpPolicyMetaData, bool includePolicyCommands = false)
		{
			XName name = XName.Get("dlpPolicy");
			object[] array = new object[10];
			array[0] = new XAttribute(XName.Get("version"), dlpPolicyMetaData.Version);
			array[1] = new XAttribute(XName.Get("state"), dlpPolicyMetaData.State);
			array[2] = new XAttribute(XName.Get("mode"), dlpPolicyMetaData.Mode);
			array[3] = new XAttribute(XName.Get("id"), dlpPolicyMetaData.ImmutableId);
			array[4] = new XElement(XName.Get("name"), dlpPolicyMetaData.Name);
			array[5] = new XElement(XName.Get("contentVersion"), dlpPolicyMetaData.ContentVersion);
			array[6] = new XElement(XName.Get("publisherName"), dlpPolicyMetaData.PublisherName);
			array[7] = new XElement(XName.Get("description"), dlpPolicyMetaData.Description);
			array[8] = new XElement(XName.Get("keywords"), from keyword in dlpPolicyMetaData.Keywords
			select new XElement(XName.Get("keyword"), keyword));
			array[9] = new XElement(XName.Get("policyCommands"), from commandBlock in includePolicyCommands ? dlpPolicyMetaData.PolicyCommands : new List<string>()
			select new XElement(XName.Get("commandBlock"), new XCData(commandBlock)));
			return new XElement(name, array);
		}

		// Token: 0x06005588 RID: 21896 RVA: 0x00160964 File Offset: 0x0015EB64
		public static byte[] SerializeDlpPolicyTemplate(DlpPolicyTemplateMetaData dlpTemplateMetaData)
		{
			XName name = XName.Get("dlpPolicyTemplate");
			object[] array = new object[12];
			array[0] = new XAttribute(XName.Get("version"), dlpTemplateMetaData.Version);
			array[1] = new XAttribute(XName.Get("state"), dlpTemplateMetaData.State);
			array[2] = new XAttribute(XName.Get("mode"), dlpTemplateMetaData.Mode);
			array[3] = new XAttribute(XName.Get("id"), dlpTemplateMetaData.ImmutableId);
			array[4] = new XElement(XName.Get("contentVersion"), dlpTemplateMetaData.ContentVersion);
			array[5] = new XElement(XName.Get("publisherName"), dlpTemplateMetaData.PublisherName);
			array[6] = new XElement(XName.Get("name"), from localizedName in dlpTemplateMetaData.LocalizedNames
			select new XElement(XName.Get("localizedString"), new object[]
			{
				new XAttribute(XName.Get("lang"), localizedName.Key),
				localizedName.Value
			}));
			array[7] = new XElement(XName.Get("description"), from localizedDescription in dlpTemplateMetaData.LocalizedDescriptions
			select new XElement(XName.Get("localizedString"), new object[]
			{
				new XAttribute(XName.Get("lang"), localizedDescription.Key),
				localizedDescription.Value
			}));
			array[8] = new XElement(XName.Get("keywords"), from keywords in dlpTemplateMetaData.LocalizedKeywords
			select new XElement(XName.Get("keyword"), from localizedKeyword in keywords
			select new XElement(XName.Get("localizedString"), new object[]
			{
				new XAttribute(XName.Get("lang"), localizedKeyword.Key),
				localizedKeyword.Value
			})));
			array[9] = new XElement(XName.Get("ruleParameters"), dlpTemplateMetaData.RuleParameters.Select(delegate(DlpTemplateRuleParameter ruleParameter)
			{
				XName name2 = XName.Get("ruleParameter");
				object[] array2 = new object[4];
				array2[0] = new XAttribute(XName.Get("type"), ruleParameter.Type);
				array2[1] = new XAttribute(XName.Get("required"), ruleParameter.Required.ToString());
				array2[2] = new XAttribute(XName.Get("token"), ruleParameter.Token);
				array2[3] = new XElement(XName.Get("description"), from localizedDescription in ruleParameter.LocalizedDescriptions
				select new XElement(XName.Get("localizedString"), new object[]
				{
					new XAttribute(XName.Get("lang"), localizedDescription.Key),
					localizedDescription.Value
				}));
				return new XElement(name2, array2);
			}));
			array[10] = new XElement(XName.Get("policyCommands"), from commandBlock in dlpTemplateMetaData.PolicyCommands
			select new XElement(XName.Get("commandBlock"), new XCData(commandBlock)));
			array[11] = new XElement(XName.Get("policyCommandsResources"), dlpTemplateMetaData.LocalizedPolicyCommandResources.Select(delegate(KeyValuePair<string, Dictionary<string, string>> resources)
			{
				XName name2 = XName.Get("resource");
				object[] array2 = new object[2];
				array2[0] = new XAttribute(XName.Get("token"), resources.Key);
				array2[1] = from resource in resources.Value
				select new XElement(XName.Get("localizedString"), new object[]
				{
					new XAttribute(XName.Get("lang"), resource.Key),
					resource.Value
				});
				return new XElement(name2, array2);
			}));
			XElement xelement = new XElement(name, array);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				xelement.Save(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x02000957 RID: 2391
		internal static class Declaration
		{
			// Token: 0x04003191 RID: 12689
			public const string Version = "1.0";

			// Token: 0x04003192 RID: 12690
			public const string Encoding = "utf-8";

			// Token: 0x04003193 RID: 12691
			public const string StandAlone = "yes";
		}

		// Token: 0x02000958 RID: 2392
		internal static class Attribute
		{
			// Token: 0x04003194 RID: 12692
			public const string Value = "value";

			// Token: 0x04003195 RID: 12693
			public const string Lang = "lang";

			// Token: 0x04003196 RID: 12694
			public const string Version = "version";

			// Token: 0x04003197 RID: 12695
			public const string State = "state";

			// Token: 0x04003198 RID: 12696
			public const string Mode = "mode";

			// Token: 0x04003199 RID: 12697
			public const string ImmutableId = "id";

			// Token: 0x0400319A RID: 12698
			public const string Type = "type";

			// Token: 0x0400319B RID: 12699
			public const string Required = "required";

			// Token: 0x0400319C RID: 12700
			public const string Token = "token";
		}

		// Token: 0x02000959 RID: 2393
		internal static class Element
		{
			// Token: 0x0400319D RID: 12701
			public const string Name = "name";

			// Token: 0x0400319E RID: 12702
			public const string PublisherName = "publisherName";

			// Token: 0x0400319F RID: 12703
			public const string ContentVersion = "contentVersion";

			// Token: 0x040031A0 RID: 12704
			public const string DlpPolicies = "dlpPolicies";

			// Token: 0x040031A1 RID: 12705
			public const string DlpPolicy = "dlpPolicy";

			// Token: 0x040031A2 RID: 12706
			public const string DlpPolicyTemplates = "dlpPolicyTemplates";

			// Token: 0x040031A3 RID: 12707
			public const string DlpPolicyTemplate = "dlpPolicyTemplate";

			// Token: 0x040031A4 RID: 12708
			public const string Description = "description";

			// Token: 0x040031A5 RID: 12709
			public const string LocalizedString = "localizedString";

			// Token: 0x040031A6 RID: 12710
			public const string Keywords = "keywords";

			// Token: 0x040031A7 RID: 12711
			public const string Keyword = "keyword";

			// Token: 0x040031A8 RID: 12712
			public const string RuleParameters = "ruleParameters";

			// Token: 0x040031A9 RID: 12713
			public const string RuleParameter = "ruleParameter";

			// Token: 0x040031AA RID: 12714
			public const string PolicyCommands = "policyCommands";

			// Token: 0x040031AB RID: 12715
			public const string CommandBlock = "commandBlock";

			// Token: 0x040031AC RID: 12716
			public const string PolicyCommandsResources = "policyCommandsResources";

			// Token: 0x040031AD RID: 12717
			public const string Resource = "resource";
		}
	}
}
