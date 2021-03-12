using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Mce.Interop.Api;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000857 RID: 2135
	internal sealed class ClassificationEngineValidator
	{
		// Token: 0x06004A07 RID: 18951 RVA: 0x001308F0 File Offset: 0x0012EAF0
		private static string ReadRegexTestTemplate()
		{
			string result;
			using (Stream stream = ClassificationDefinitionUtils.LoadStreamFromEmbeddedResource("RegexTestTemplate.xml"))
			{
				StreamReader streamReader = new StreamReader(stream);
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x00130BD8 File Offset: 0x0012EDD8
		private static IEnumerable<string> GetInvalidRegexes(MicrosoftClassificationEngine classificationEngine, RulePackageLoader rulePackageLoader, XDocument rulePackXDoc)
		{
			foreach (KeyValuePair<string, string> regexProcessor in XmlProcessingUtils.GetRegexesInRulePackage(rulePackXDoc))
			{
				string value = ClassificationEngineValidator.regexTestTemplate.Value;
				KeyValuePair<string, string> keyValuePair = regexProcessor;
				string rulePackage = string.Format(value, keyValuePair.Value);
				RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS = default(RULE_PACKAGE_DETAILS);
				rule_PACKAGE_DETAILS.RulePackageSetID = "not-used-here";
				KeyValuePair<string, string> keyValuePair2 = regexProcessor;
				rule_PACKAGE_DETAILS.RulePackageID = keyValuePair2.Key;
				rule_PACKAGE_DETAILS.RuleIDs = null;
				RULE_PACKAGE_DETAILS rulePackageDetails = rule_PACKAGE_DETAILS;
				rulePackageLoader.SetRulePackage(rulePackageDetails.RulePackageID, rulePackage);
				string badRegex = null;
				try
				{
					RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS2 = rulePackageDetails;
					classificationEngine.GetClassificationDefinitions(ref rule_PACKAGE_DETAILS2);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == -2147220978)
					{
						KeyValuePair<string, string> keyValuePair3 = regexProcessor;
						badRegex = keyValuePair3.Key;
					}
				}
				if (badRegex != null)
				{
					yield return badRegex;
				}
			}
			yield break;
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x00130C04 File Offset: 0x0012EE04
		internal static void ValidateRulePackage(XDocument rulePackXDoc, string rulePackContents)
		{
			string mceExecutablePath = ClassificationDefinitionUtils.GetMceExecutablePath(true);
			if (mceExecutablePath == null || !File.Exists(mceExecutablePath))
			{
				throw new COMException("Unable to locate the Microsoft Classification Engine", -2147287038);
			}
			try
			{
				using (ActivationContextActivator.FromInternalManifest(mceExecutablePath, Path.GetDirectoryName(mceExecutablePath)))
				{
					MicrosoftClassificationEngine microsoftClassificationEngine = new MicrosoftClassificationEngine();
					PropertyBag propertyBag = new PropertyBag();
					RulePackageLoader rulePackageLoader = new RulePackageLoader();
					microsoftClassificationEngine.Init(propertyBag, rulePackageLoader);
					RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS = default(RULE_PACKAGE_DETAILS);
					rule_PACKAGE_DETAILS.RulePackageSetID = "not-used-here";
					rule_PACKAGE_DETAILS.RulePackageID = "test-rule-pack";
					rule_PACKAGE_DETAILS.RuleIDs = null;
					RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS2 = rule_PACKAGE_DETAILS;
					rulePackageLoader.SetRulePackage(rule_PACKAGE_DETAILS2.RulePackageID, rulePackContents);
					try
					{
						MicrosoftClassificationEngine microsoftClassificationEngine2 = microsoftClassificationEngine;
						RULE_PACKAGE_DETAILS rule_PACKAGE_DETAILS3 = rule_PACKAGE_DETAILS2;
						microsoftClassificationEngine2.GetClassificationDefinitions(ref rule_PACKAGE_DETAILS3);
					}
					catch (COMException ex)
					{
						if (ex.ErrorCode == -2147220978)
						{
							List<string> value = ClassificationEngineValidator.GetInvalidRegexes(microsoftClassificationEngine, rulePackageLoader, rulePackXDoc).ToList<string>();
							ex.Data[ClassificationEngineValidator.BadRegexesKey] = value;
						}
						throw ex;
					}
				}
			}
			catch (ActivationContextActivatorException)
			{
				throw new COMException("Unable to instantiate the Microsoft Classification Engine", -2147164127);
			}
		}

		// Token: 0x04002C97 RID: 11415
		internal static readonly string BadRegexesKey = "BadRegexes";

		// Token: 0x04002C98 RID: 11416
		private static readonly Lazy<string> regexTestTemplate = new Lazy<string>(new Func<string>(ClassificationEngineValidator.ReadRegexTestTemplate), LazyThreadSafetyMode.PublicationOnly);
	}
}
