using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000820 RID: 2080
	internal static class ClassificationDefinitionConstants
	{
		// Token: 0x04002BA1 RID: 11169
		public const string ClassificationDefinitionContainerCn = "ClassificationDefinitions";

		// Token: 0x04002BA2 RID: 11170
		internal const string ClassificationDefinitionNoun = "DataClassification";

		// Token: 0x04002BA3 RID: 11171
		internal const string ClassificationDefinitionNamespacePrefix = "mce";

		// Token: 0x04002BA4 RID: 11172
		public const string ClassificationDefinitionXmlNamespace = "http://schemas.microsoft.com/office/2011/mce";

		// Token: 0x04002BA5 RID: 11173
		internal const string OutOfBoxRulePackageIdPrefix = "00000000";

		// Token: 0x04002BA6 RID: 11174
		internal const string FingerprintRulePackageIdPrefix = "00000000-0000-0000-0001";

		// Token: 0x04002BA7 RID: 11175
		internal const string OobRulePackageEmbeddedResourceFileName = "DefaultClassificationDefinitions.xml";

		// Token: 0x04002BA8 RID: 11176
		internal const string OobEngineConfigResourceFileName = "MceConfig.xml";

		// Token: 0x04002BA9 RID: 11177
		internal const string MceFunctionProcessorElementName = "Function";

		// Token: 0x04002BAA RID: 11178
		internal const string MceKeywordProcessorElementName = "Keyword";

		// Token: 0x04002BAB RID: 11179
		internal const string MceRegexProcessorElementName = "Regex";

		// Token: 0x04002BAC RID: 11180
		internal const string MceFingerprintProcessorElementName = "Fingerprint";

		// Token: 0x04002BAD RID: 11181
		internal const string MceFingerprintAttributeThreshold = "threshold";

		// Token: 0x04002BAE RID: 11182
		internal const string MceFingerprintAttributeShingleCount = "shingleCount";

		// Token: 0x04002BAF RID: 11183
		internal const string MceFingerprintAttributeDescription = "description";

		// Token: 0x04002BB0 RID: 11184
		internal const string MceLangcodeAttributeName = "langcode";

		// Token: 0x04002BB1 RID: 11185
		internal const string MceDefaultAttributeName = "default";

		// Token: 0x04002BB2 RID: 11186
		internal const string MceNameResourceElementName = "Name";

		// Token: 0x04002BB3 RID: 11187
		internal const string MceDescriptionResourceElementName = "Description";

		// Token: 0x04002BB4 RID: 11188
		internal const string MceResourceElementName = "Resource";

		// Token: 0x04002BB5 RID: 11189
		internal const string MceLocalizedStringsElementName = "LocalizedStrings";

		// Token: 0x04002BB6 RID: 11190
		internal const string MceRulesElementName = "Rules";

		// Token: 0x04002BB7 RID: 11191
		internal const string MceVersionElementName = "Version";

		// Token: 0x04002BB8 RID: 11192
		internal const string MceVersionAttributeName = "minEngineVersion";

		// Token: 0x04002BB9 RID: 11193
		internal const string MceIdAttributeName = "id";

		// Token: 0x04002BBA RID: 11194
		internal const string MceIdRefAttributeName = "idRef";

		// Token: 0x04002BBB RID: 11195
		internal const string MceRulePackElementName = "RulePack";

		// Token: 0x04002BBC RID: 11196
		internal const string McePublisherElementName = "Publisher";

		// Token: 0x04002BBD RID: 11197
		internal const string McePublisherNameElementName = "PublisherName";

		// Token: 0x04002BBE RID: 11198
		internal const string MceMatchElementName = "Match";

		// Token: 0x04002BBF RID: 11199
		internal const string MceIdMatchElementName = "IdMatch";

		// Token: 0x04002BC0 RID: 11200
		internal const string MceAnyElementName = "Any";

		// Token: 0x04002BC1 RID: 11201
		internal const string MceMinMatchesAttributeName = "minMatches";

		// Token: 0x04002BC2 RID: 11202
		internal const string MceEntityElementName = "Entity";

		// Token: 0x04002BC3 RID: 11203
		internal const string MceAffinityElementName = "Affinity";

		// Token: 0x04002BC4 RID: 11204
		internal const string MceEvidencesProximityAttributeName = "evidencesProximity";

		// Token: 0x04002BC5 RID: 11205
		internal const string MceThresholdConfidenceLevelAttributeName = "thresholdConfidenceLevel";

		// Token: 0x04002BC6 RID: 11206
		internal const string MceEvidenceElementName = "Evidence";

		// Token: 0x04002BC7 RID: 11207
		internal const string MceConfidenceLevelAttributeName = "confidenceLevel";

		// Token: 0x04002BC8 RID: 11208
		internal const string FingerprintRulePackTemplate = "FingerprintRulePackTemplate.xml";

		// Token: 0x04002BC9 RID: 11209
		internal const string RulePackageNoun = "ClassificationRuleCollection";

		// Token: 0x04002BCA RID: 11210
		internal const CompareOptions GuidIdentityStringMatchingComparison = CompareOptions.OrdinalIgnoreCase;

		// Token: 0x04002BCB RID: 11211
		internal const CompareOptions NameIdentityStringMatchingComparison = CompareOptions.IgnoreCase;

		// Token: 0x04002BCC RID: 11212
		internal static readonly string[] EmbeddedRulePackageSchemaFileNames = new string[]
		{
			"RulePackageTypes.xsd",
			"RulePackage.xsd"
		};

		// Token: 0x04002BCD RID: 11213
		internal static readonly ISet<string> MceRuleElementNames = new SortedSet<string>
		{
			"Affinity",
			"Entity"
		};

		// Token: 0x04002BCE RID: 11214
		internal static readonly ISet<string> MceProcessorElementNames = new SortedSet<string>
		{
			"Function",
			"Keyword",
			"Regex",
			"Fingerprint"
		};

		// Token: 0x04002BCF RID: 11215
		internal static readonly ISet<string> MceIdMatchElementNames = new SortedSet<string>
		{
			"IdMatch"
		};

		// Token: 0x04002BD0 RID: 11216
		internal static readonly ISet<string> MceMatchElementNames = new SortedSet<string>
		{
			"IdMatch",
			"Match"
		};

		// Token: 0x04002BD1 RID: 11217
		internal static readonly ISet<string> MceCustomProcessorElementNames = new SortedSet<string>
		{
			"Keyword",
			"Regex",
			"Fingerprint"
		};

		// Token: 0x04002BD2 RID: 11218
		internal static readonly IEqualityComparer<string> RuleCollectionIdComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04002BD3 RID: 11219
		internal static readonly IEqualityComparer<string> RuleIdComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04002BD4 RID: 11220
		internal static readonly IEqualityComparer<string> TextProcessorIdComparer = StringComparer.Ordinal;

		// Token: 0x04002BD5 RID: 11221
		public static readonly ADObjectId ClassificationDefinitionsRdn = new ADObjectId("CN=ClassificationDefinitions,CN=Rules,CN=Transport Settings");

		// Token: 0x04002BD6 RID: 11222
		internal static readonly char HierarchicalIdentitySeparatorChar = '\\';

		// Token: 0x04002BD7 RID: 11223
		internal static readonly string ExceptionSourcesListKey = "ExceptionSourcesList";

		// Token: 0x04002BD8 RID: 11224
		internal static readonly string MceExecutableFileName = "Mce.dll";

		// Token: 0x04002BD9 RID: 11225
		internal static readonly string OnDiskMceConfigurationDirName = "Configuration";

		// Token: 0x04002BDA RID: 11226
		internal static readonly string OnDiskMceConfigFileName = "Config.xml";

		// Token: 0x04002BDB RID: 11227
		internal static ExchangeBuild DefaultVersion = new ExchangeBuild(15, 0, 513, 0);

		// Token: 0x04002BDC RID: 11228
		internal static ExchangeBuild CompatibleEngineVersion = new ExchangeBuild(15, 0, 780, 0);

		// Token: 0x04002BDD RID: 11229
		internal static ExchangeBuild NovFunctionCompatibleEngineVersion = new ExchangeBuild(15, 0, 847, 13);

		// Token: 0x04002BDE RID: 11230
		internal static Dictionary<TextProcessorType, ExchangeBuild> TextProcessorTypeToVersions = new Dictionary<TextProcessorType, ExchangeBuild>
		{
			{
				TextProcessorType.Fingerprint,
				ClassificationDefinitionConstants.CompatibleEngineVersion
			}
		};

		// Token: 0x04002BDF RID: 11231
		internal static Dictionary<string, ExchangeBuild> FunctionNameToVersions = new Dictionary<string, ExchangeBuild>
		{
			{
				"Func_taiwanese_national_id",
				ClassificationDefinitionConstants.NovFunctionCompatibleEngineVersion
			},
			{
				"Func_pesel_identification_number",
				ClassificationDefinitionConstants.NovFunctionCompatibleEngineVersion
			},
			{
				"Func_polish_national_id",
				ClassificationDefinitionConstants.NovFunctionCompatibleEngineVersion
			},
			{
				"Func_polish_passport_number",
				ClassificationDefinitionConstants.NovFunctionCompatibleEngineVersion
			},
			{
				"Func_finnish_national_id",
				ClassificationDefinitionConstants.NovFunctionCompatibleEngineVersion
			}
		};
	}
}
