using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000872 RID: 2162
	internal static class XmlProcessingUtils
	{
		// Token: 0x06004A98 RID: 19096 RVA: 0x0013300C File Offset: 0x0013120C
		private static XmlSchema LoadSchemaFromEmbeddedResource(string schemaName)
		{
			XmlSchema result;
			using (Stream stream = ClassificationDefinitionUtils.LoadStreamFromEmbeddedResource(schemaName))
			{
				result = XmlSchema.Read(stream, null);
			}
			return result;
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x00133048 File Offset: 0x00131248
		private static XmlSchemaSet CreateRulePackageCompiledSchema()
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet
			{
				XmlResolver = null
			};
			foreach (string schemaName in ClassificationDefinitionConstants.EmbeddedRulePackageSchemaFileNames)
			{
				xmlSchemaSet.Add(XmlProcessingUtils.LoadSchemaFromEmbeddedResource(schemaName));
			}
			xmlSchemaSet.Compile();
			return xmlSchemaSet;
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x00133095 File Offset: 0x00131295
		internal static XmlSchemaSet RulePackageSchemaInstance
		{
			get
			{
				return XmlProcessingUtils.compiledRulePackageSchema.Value;
			}
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x001330A4 File Offset: 0x001312A4
		internal static XDocument ValidateRulePackageXmlContentsLite(byte[] dataToValidate)
		{
			ArgumentValidator.ThrowIfNull("dataToValidate", dataToValidate);
			XmlReaderSettings xmlReaderSettings = ClassificationDefinitionUtils.CreateSafeXmlReaderSettings();
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.Schemas = XmlProcessingUtils.RulePackageSchemaInstance;
			XDocument xdocument;
			try
			{
				using (Stream stream = new MemoryStream(dataToValidate))
				{
					using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
					{
						xdocument = XDocument.Load(xmlReader);
					}
				}
				if (xdocument.Root == null)
				{
					throw new XmlSchemaValidationException(Strings.ClassificationRuleCollectionMissingRootElementViolation, null, 1, 1);
				}
				XNamespace defaultNamespace = xdocument.Root.GetDefaultNamespace();
				if (!string.Equals(defaultNamespace.NamespaceName, "http://schemas.microsoft.com/office/2011/mce", StringComparison.OrdinalIgnoreCase))
				{
					throw new XmlSchemaValidationException(Strings.ClassificationRuleCollectionIncorrectDocumentNamespaceViolation(defaultNamespace.NamespaceName, "http://schemas.microsoft.com/office/2011/mce"), null, 1, 1);
				}
			}
			catch (XmlSchemaValidationException ex)
			{
				throw new ClassificationRuleCollectionSchemaValidationException(ex.Message, ex.LineNumber, ex.LinePosition);
			}
			catch (XmlException ex2)
			{
				throw new ClassificationRuleCollectionSchemaValidationException(ex2.Message, ex2.LineNumber, ex2.LinePosition);
			}
			return xdocument;
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x0013347C File Offset: 0x0013167C
		private static Dictionary<CultureInfo, string> ReadLocalizableRuleDetails(XElement ruleResourceElement, XName ruleDetailElementName)
		{
			ArgumentValidator.ThrowIfNull("ruleResourceElement", ruleResourceElement);
			ArgumentValidator.ThrowIfNull("ruleDetailElementName", ruleDetailElementName);
			ParallelQuery<KeyValuePair<CultureInfo, string>> source = from localizedDetailsElement in ruleResourceElement.Elements(ruleDetailElementName).AsParallel<XElement>()
			let elementValue = localizedDetailsElement.Value.Trim()
			let langCode = localizedDetailsElement.Attribute("langcode").Value
			select new KeyValuePair<CultureInfo, string>(new CultureInfo(langCode, false), elementValue);
			Dictionary<CultureInfo, string> result;
			try
			{
				Dictionary<CultureInfo, string> dictionary = source.ToDictionary((KeyValuePair<CultureInfo, string> localizedDetails) => localizedDetails.Key, (KeyValuePair<CultureInfo, string> localizedDetails) => localizedDetails.Value);
				result = dictionary;
			}
			catch (AggregateException ex)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, ex.Flatten());
			}
			return result;
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00133580 File Offset: 0x00131780
		internal static string GetRulePackId(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			string result;
			try
			{
				result = rulePackXDoc.Root.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("RulePack")).Attribute("id").Value.ToUpperInvariant();
			}
			catch (NullReferenceException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			return result;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x001335EC File Offset: 0x001317EC
		internal static Version SetRulePackVersionFromAssemblyFileVersion(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			Version result;
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				XElement xelement = rulePackXDoc.Root.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("RulePack")).Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Version"));
				xelement.Attribute("major").Value = versionInfo.FileMajorPart.ToString("D");
				xelement.Attribute("minor").Value = versionInfo.FileMinorPart.ToString("D");
				xelement.Attribute("build").Value = versionInfo.FileBuildPart.ToString("D");
				xelement.Attribute("revision").Value = versionInfo.FilePrivatePart.ToString("D");
				result = new Version(versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart, versionInfo.FilePrivatePart);
			}
			catch (NullReferenceException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			return result;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00133724 File Offset: 0x00131924
		internal static Version GetRulePackVersion(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			Version result;
			try
			{
				XElement xelement = rulePackXDoc.Root.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("RulePack")).Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Version"));
				ushort major = ushort.Parse(xelement.Attribute("major").Value);
				ushort minor = ushort.Parse(xelement.Attribute("minor").Value);
				ushort build = ushort.Parse(xelement.Attribute("build").Value);
				ushort revision = ushort.Parse(xelement.Attribute("revision").Value);
				result = new Version((int)major, (int)minor, (int)build, (int)revision);
			}
			catch (SystemException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			return result;
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00133804 File Offset: 0x00131A04
		internal static uint? ReadRuleRecommendedConfidence(XElement ruleElement)
		{
			ArgumentValidator.ThrowIfNull("ruleElement", ruleElement);
			uint? result;
			try
			{
				XAttribute xattribute = ruleElement.Attribute("recommendedConfidence");
				if (xattribute != null)
				{
					result = new uint?(uint.Parse(xattribute.Value));
				}
				else
				{
					result = null;
				}
			}
			catch (FormatException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			catch (OverflowException innerException2)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException2);
			}
			return result;
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x00133894 File Offset: 0x00131A94
		internal static ExchangeBuild GetRulePackElementVersion(XElement rulePackElement)
		{
			ArgumentValidator.ThrowIfNull("rulePackElement", rulePackElement);
			ExchangeBuild result = ClassificationDefinitionConstants.DefaultVersion;
			while (rulePackElement != null && rulePackElement.Name.LocalName != "Rules" && rulePackElement.Name.LocalName != "Version")
			{
				rulePackElement = rulePackElement.Parent;
			}
			if (rulePackElement != null && rulePackElement.Name.LocalName == "Version")
			{
				result = ExchangeBuild.Parse(rulePackElement.Attribute("minEngineVersion").Value);
			}
			return result;
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0013397C File Offset: 0x00131B7C
		internal static DataClassificationLocalizableDetails ReadDefaultRuleMetadata(XElement ruleResourceElement)
		{
			ArgumentValidator.ThrowIfNull("ruleResourceElement", ruleResourceElement);
			XElement xelement = (from ruleNameElement in ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name"))
			where ruleNameElement.Attribute("default") != null && (bool)ruleNameElement.Attribute("default")
			select ruleNameElement).FirstOrDefault<XElement>();
			XElement xelement2 = (from ruleDescriptionElement in ruleResourceElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description"))
			where ruleDescriptionElement.Attribute("default") != null && (bool)ruleDescriptionElement.Attribute("default")
			select ruleDescriptionElement).FirstOrDefault<XElement>();
			if (xelement == null || xelement2 == null)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionIncorrectNumberOfDefaultInRuleResources(ruleResourceElement.Attribute("idRef").Value));
			}
			string name = xelement.Value.Trim();
			string description = xelement2.Value.Trim();
			CultureInfo culture;
			try
			{
				CultureInfo cultureInfo = new CultureInfo(xelement.Attribute("langcode").Value, false);
				CultureInfo obj = new CultureInfo(xelement2.Attribute("langcode").Value, false);
				if (!cultureInfo.Equals(obj))
				{
					throw new XmlException(Strings.ClassificationRuleCollectionInconsistentDefaultInRuleResource(ruleResourceElement.Attribute("idRef").Value));
				}
				culture = cultureInfo;
			}
			catch (NullReferenceException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			catch (CultureNotFoundException innerException2)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException2);
			}
			return new DataClassificationLocalizableDetails
			{
				Name = name,
				Description = description,
				Culture = culture
			};
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x00133B20 File Offset: 0x00131D20
		internal static Dictionary<CultureInfo, string> ReadAllRuleNames(XElement ruleResourceElement)
		{
			return XmlProcessingUtils.ReadLocalizableRuleDetails(ruleResourceElement, XmlProcessingUtils.GetMceNsQualifiedNodeName("Name"));
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00133B32 File Offset: 0x00131D32
		internal static Dictionary<CultureInfo, string> ReadAllRuleDescriptions(XElement ruleResourceElement)
		{
			return XmlProcessingUtils.ReadLocalizableRuleDetails(ruleResourceElement, XmlProcessingUtils.GetMceNsQualifiedNodeName("Description"));
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x00133CA0 File Offset: 0x00131EA0
		internal static MultiValuedProperty<Fingerprint> ReadAllReferredFingerprints(XElement ruleElement)
		{
			ArgumentValidator.ThrowIfNull("ruleElement", ruleElement);
			ArgumentValidator.ThrowIfNull("ruleElement.Document", ruleElement.Document);
			List<string> distinctTextProcessorRefs = (from versionedTextProcessorReference in TextProcessorUtils.GetTextProcessorReferences(ruleElement)
			select versionedTextProcessorReference.Key).Distinct(ClassificationDefinitionConstants.TextProcessorIdComparer).ToList<string>();
			IEnumerable<Fingerprint> value = from fingerprintElement in ruleElement.Document.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Fingerprint"))
			where distinctTextProcessorRefs.Contains(XmlProcessingUtils.GetAttributeValue(fingerprintElement, "id"))
			let fingerprint = Fingerprint.FromXElement(fingerprintElement)
			select fingerprint;
			return new MultiValuedProperty<Fingerprint>(value);
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x00133D7C File Offset: 0x00131F7C
		internal static ClassificationRuleCollectionLocalizableDetails ReadDefaultRulePackageMetadata(XDocument rulePackXDoc)
		{
			return XmlProcessingUtils.ReadRulePackageMetadata(rulePackXDoc, null);
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x00133D85 File Offset: 0x00131F85
		internal static ClassificationRuleCollectionLocalizableDetails ReadDefaultRulePackageMetadata(XElement rulePackDetailsElement)
		{
			return XmlProcessingUtils.ReadRulePackageMetadata(rulePackDetailsElement, null);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00133D90 File Offset: 0x00131F90
		internal static ClassificationRuleCollectionLocalizableDetails ReadRulePackageMetadata(XDocument rulePackXDoc, CultureInfo cultureInfo = null)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			XElement rulePackageMetadataElement = XmlProcessingUtils.GetRulePackageMetadataElement(rulePackXDoc);
			return XmlProcessingUtils.ReadRulePackageMetadata(rulePackageMetadataElement, cultureInfo);
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00133DE4 File Offset: 0x00131FE4
		internal static ClassificationRuleCollectionLocalizableDetails ReadRulePackageMetadata(XElement rulePackDetailsElement, CultureInfo cultureInfo = null)
		{
			ArgumentValidator.ThrowIfNull("rulePackDetailsElement", rulePackDetailsElement);
			ClassificationRuleCollectionLocalizableDetails result;
			try
			{
				string langCodeToUse = (cultureInfo != null) ? cultureInfo.Name : rulePackDetailsElement.Attribute("defaultLangCode").Value;
				XElement xelement = (from localizedDetails in rulePackDetailsElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("LocalizedDetails")).AsParallel<XElement>()
				where langCodeToUse.Equals(localizedDetails.Attribute("langcode").Value, StringComparison.OrdinalIgnoreCase)
				select localizedDetails).SingleOrDefault<XElement>();
				if (xelement != null)
				{
					string value = xelement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name")).Value;
					string value2 = xelement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("PublisherName")).Value;
					string value3 = xelement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description")).Value;
					result = new ClassificationRuleCollectionLocalizableDetails
					{
						Name = value,
						PublisherName = value2,
						Description = value3,
						Culture = new CultureInfo(langCodeToUse, false)
					};
				}
				else
				{
					if (cultureInfo == null)
					{
						throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents);
					}
					result = null;
				}
			}
			catch (NullReferenceException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			catch (InvalidOperationException innerException2)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException2);
			}
			catch (AggregateException ex)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, ex.Flatten());
			}
			catch (CultureNotFoundException innerException3)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException3);
			}
			return result;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00133F78 File Offset: 0x00132178
		internal static XElement GetRulePackageMetadataElement(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			XElement result;
			try
			{
				result = rulePackXDoc.Root.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("RulePack")).Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Details"));
			}
			catch (NullReferenceException innerException)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, innerException);
			}
			return result;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00134544 File Offset: 0x00132744
		internal static Dictionary<CultureInfo, ClassificationRuleCollectionLocalizableDetails> ReadAllRulePackageMetadata(XElement rulePackDetailsElement)
		{
			ArgumentValidator.ThrowIfNull("rulePackDetailsElement", rulePackDetailsElement);
			ParallelQuery<ClassificationRuleCollectionLocalizableDetails> source = from localizedDetailsElement in rulePackDetailsElement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("LocalizedDetails")).AsParallel<XElement>()
			let name = localizedDetailsElement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Name")).Value
			let publisherName = localizedDetailsElement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("PublisherName")).Value
			let description = localizedDetailsElement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Description")).Value
			let langCode = localizedDetailsElement.Attribute("langcode").Value
			select new ClassificationRuleCollectionLocalizableDetails
			{
				Name = name,
				PublisherName = publisherName,
				Description = description,
				Culture = new CultureInfo(langCode, false)
			};
			Dictionary<CultureInfo, ClassificationRuleCollectionLocalizableDetails> result;
			try
			{
				Dictionary<CultureInfo, ClassificationRuleCollectionLocalizableDetails> dictionary = source.ToDictionary((ClassificationRuleCollectionLocalizableDetails localizedDetails) => localizedDetails.Culture, (ClassificationRuleCollectionLocalizableDetails localizedDetails) => localizedDetails);
				result = dictionary;
			}
			catch (AggregateException ex)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, ex.Flatten());
			}
			return result;
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x001346BC File Offset: 0x001328BC
		internal static IList<string> GetAllRuleIds(XDocument rulePackXDocument)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDocument", rulePackXDocument);
			ParallelQuery<string> source = from rulePackElement in rulePackXDocument.Descendants().AsParallel<XElement>()
			where ClassificationDefinitionConstants.MceRuleElementNames.Contains(rulePackElement.Name.LocalName)
			select rulePackElement.Attribute("id").Value;
			IList<string> result;
			try
			{
				List<string> list = source.ToList<string>();
				result = list;
			}
			catch (AggregateException ex)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, ex.Flatten());
			}
			return result;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00134758 File Offset: 0x00132958
		internal static IEnumerable<QueryMatchResult> GetMatchingRulesById(XDocument rulePackXDocument, IEnumerable<string> ruleIdQueries = null)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDocument", rulePackXDocument);
			HashSet<string> ruleIdQuerySet = (ruleIdQueries != null) ? new HashSet<string>(ruleIdQueries, ClassificationDefinitionConstants.RuleIdComparer) : null;
			return XmlProcessingUtils.GetMatchingRulesById(rulePackXDocument, ruleIdQuerySet);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00134E10 File Offset: 0x00133010
		internal static IEnumerable<QueryMatchResult> GetMatchingRulesById(XDocument rulePackXDocument, ISet<string> ruleIdQuerySet = null)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDocument", rulePackXDocument);
			ParallelQuery<QueryMatchResult> parallelQuery;
			if (ruleIdQuerySet != null)
			{
				parallelQuery = from rulePackElement in rulePackXDocument.Descendants().AsParallel<XElement>()
				where ClassificationDefinitionConstants.MceRuleElementNames.Contains(rulePackElement.Name.LocalName)
				let ruleId = rulePackElement.Attribute("id").Value
				let finalRuleId = string.IsNullOrEmpty(ruleId) ? null : ruleId
				where ruleIdQuerySet.Contains(finalRuleId)
				select new QueryMatchResult
				{
					QueryString = finalRuleId,
					MatchingRuleId = finalRuleId,
					MatchingRuleXElement = rulePackElement
				};
			}
			else
			{
				parallelQuery = from rulePackElement in rulePackXDocument.Descendants().AsParallel<XElement>()
				where ClassificationDefinitionConstants.MceRuleElementNames.Contains(rulePackElement.Name.LocalName)
				let ruleId = rulePackElement.Attribute("id").Value
				let finalRuleId = string.IsNullOrEmpty(ruleId) ? null : ruleId
				select new QueryMatchResult
				{
					MatchingRuleId = finalRuleId,
					MatchingRuleXElement = rulePackElement
				};
			}
			ParallelQuery<QueryMatchResult> mainQuery = parallelQuery;
			return XmlProcessingUtils.ExecuteQueryMatching(mainQuery, (Dictionary<string, QueryMatchResult> queryMatchResultsDictionary) => from resourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")).AsParallel<XElement>()
			let ruleIdRef = (string)resourceElement.Attribute("idRef")
			where queryMatchResultsDictionary.ContainsKey(ruleIdRef)
			select new KeyValuePair<string, XElement>(ruleIdRef, resourceElement), delegate(Dictionary<string, QueryMatchResult> queryMatchResultsDictionary, KeyValuePair<string, XElement> matchAssociation)
			{
				queryMatchResultsDictionary[matchAssociation.Key].MatchingResourceXElement = matchAssociation.Value;
			}, new Func<string, LocalizedString>(Strings.ClassificationRuleCollectionResourceNotFoundViolation));
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00135CFC File Offset: 0x00133EFC
		internal static IEnumerable<QueryMatchResult> GetMatchingRulesByName(XDocument rulePackXDocument, IEnumerable<string> ruleNameQueries, NameMatchingOptions matchingOption = NameMatchingOptions.InvariantNameOrLocalizedNameMatch, bool ignoreCase = true)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDocument", rulePackXDocument);
			ArgumentValidator.ThrowIfNull("ruleNameQueries", ruleNameQueries);
			ArgumentValidator.ThrowIfInvalidValue<NameMatchingOptions>("matchingOption", matchingOption, (NameMatchingOptions nameMatchingOption) => nameMatchingOption == NameMatchingOptions.InvariantNameMatchOnly || NameMatchingOptions.InvariantNameOrLocalizedNameMatch == nameMatchingOption);
			List<string> ruleNameQueriesList = new List<string>((from query in ruleNameQueries
			where !string.IsNullOrEmpty(query)
			select query).OrderBy((string query) => query, ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal));
			if (ruleNameQueriesList.Count == 0)
			{
				return Enumerable.Empty<QueryMatchResult>();
			}
			CultureInfo currentThreadCulture = CultureInfo.CurrentCulture;
			ParallelQuery<QueryMatchResult> parallelQuery;
			if (matchingOption != NameMatchingOptions.InvariantNameMatchOnly)
			{
				parallelQuery = from resourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")).AsParallel<XElement>()
				let queryHitIndex = XmlProcessingUtils.GetInvariantOrLocalizedNameHitIndex(ruleNameQueriesList, resourceElement, currentThreadCulture, ignoreCase)
				where queryHitIndex >= 0
				let ruleIdRef = resourceElement.Attribute("idRef").Value
				let finalRuleIdRef = string.IsNullOrEmpty(ruleIdRef) ? null : ruleIdRef
				select new QueryMatchResult
				{
					QueryString = ruleNameQueriesList[queryHitIndex],
					MatchingResourceXElement = resourceElement,
					MatchingRuleId = finalRuleIdRef,
					MatchingRuleXElement = null
				};
			}
			else
			{
				parallelQuery = from resourceElement in rulePackXDocument.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource")).AsParallel<XElement>()
				let invariantLocalizedNameDetails = XmlProcessingUtils.ReadDefaultRuleMetadata(resourceElement)
				let queryHitIndex = ruleNameQueriesList.BinarySearch(invariantLocalizedNameDetails.Name, StringComparer.Create(invariantLocalizedNameDetails.Culture, ignoreCase))
				where queryHitIndex >= 0
				let ruleIdRef = resourceElement.Attribute("idRef").Value
				let finalRuleIdRef = string.IsNullOrEmpty(ruleIdRef) ? null : ruleIdRef
				select new QueryMatchResult
				{
					QueryString = ruleNameQueriesList[queryHitIndex],
					MatchingResourceXElement = resourceElement,
					MatchingRuleId = finalRuleIdRef,
					MatchingRuleXElement = null
				};
			}
			ParallelQuery<QueryMatchResult> mainQuery = parallelQuery;
			return XmlProcessingUtils.ExecuteQueryMatching(mainQuery, (Dictionary<string, QueryMatchResult> queryMatchResultsDictionary) => from ruleElement in rulePackXDocument.Descendants().AsParallel<XElement>()
			where ClassificationDefinitionConstants.MceRuleElementNames.Contains(ruleElement.Name.LocalName)
			let ruleId = (string)ruleElement.Attribute("id")
			let finalRuleId = string.IsNullOrEmpty(ruleId) ? null : ruleId
			where queryMatchResultsDictionary.ContainsKey(finalRuleId)
			select new KeyValuePair<string, XElement>(finalRuleId, ruleElement), delegate(Dictionary<string, QueryMatchResult> queryMatchResultsDictionary, KeyValuePair<string, XElement> matchAssociation)
			{
				queryMatchResultsDictionary[matchAssociation.Key].MatchingRuleXElement = matchAssociation.Value;
			}, new Func<string, LocalizedString>(Strings.ClassificationRuleCollectionOrphanedResourceViolation));
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x001360E0 File Offset: 0x001342E0
		internal static IEnumerable<KeyValuePair<string, string>> GetRegexesInRulePackage(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			return from regexElement in rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Regex"))
			let regexProcessorId = regexElement.Attribute("id").Value
			select new KeyValuePair<string, string>(regexProcessorId, regexElement.Value);
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0013614C File Offset: 0x0013434C
		internal static IEnumerable<XElement> GetFingerprintProcessorsInRulePackage(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			return rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Fingerprint"));
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0013616C File Offset: 0x0013436C
		internal static string AddFingerprintTextProcessor(XDocument rulePackXDoc, Fingerprint fingerprint)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			ArgumentValidator.ThrowIfNull("fingerprint", fingerprint);
			XElement xelement = rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("LocalizedStrings")).FirstOrDefault<XElement>();
			if (xelement == null)
			{
				throw new ClassificationRuleCollectionInvalidObjectException(Strings.ClassificationRuleCollectionInvalidObject);
			}
			XElement fingerprintProcessor = XmlProcessingUtils.GetFingerprintProcessor(rulePackXDoc, fingerprint);
			if (fingerprintProcessor == null)
			{
				fingerprint.Identity = Guid.NewGuid().ToString();
				fingerprint.ActualDescription = fingerprint.Description;
				xelement.AddBeforeSelf(fingerprint.ToXElement());
			}
			else
			{
				fingerprint.Identity = XmlProcessingUtils.GetAttributeValue(fingerprintProcessor, "id");
				string attributeValue = XmlProcessingUtils.GetAttributeValue(fingerprintProcessor, "description");
				if (string.IsNullOrEmpty(attributeValue) && !string.IsNullOrEmpty(fingerprint.Description))
				{
					fingerprintProcessor.SetAttributeValue("description", fingerprint.Description);
					fingerprint.ActualDescription = fingerprint.Description;
				}
				else
				{
					fingerprint.ActualDescription = attributeValue;
				}
			}
			return fingerprint.Identity;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x001362C4 File Offset: 0x001344C4
		private static XElement GetFingerprintProcessor(XDocument rulePackXDoc, Fingerprint fingerprint)
		{
			return (from fingerprintElement in rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Fingerprint"))
			where fingerprintElement.Attribute("shingleCount") != null && string.Equals(fingerprintElement.Attribute("shingleCount").Value, fingerprint.ShingleCount.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(fingerprintElement.Value, fingerprint.Value, StringComparison.Ordinal)
			select fingerprintElement).FirstOrDefault<XElement>();
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00136344 File Offset: 0x00134544
		internal static void AddDataClassification(XDocument rulePackXDoc, string dataClassificationId, string version, XElement dataClassificationXElement)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			ArgumentValidator.ThrowIfNullOrEmpty("dataClassificationId", dataClassificationId);
			ArgumentValidator.ThrowIfNullOrEmpty("version", version);
			ArgumentValidator.ThrowIfNull("dataClassificationXElement", dataClassificationXElement);
			List<QueryMatchResult> list = XmlProcessingUtils.GetMatchingRulesById(rulePackXDoc, new List<string>
			{
				dataClassificationId
			}).ToList<QueryMatchResult>();
			if (list.Count == 1)
			{
				list[0].MatchingRuleXElement.Remove();
			}
			else if (list.Count > 1)
			{
				throw new ClassificationRuleCollectionInvalidObjectException(Strings.ClassificationRuleCollectionInvalidObject);
			}
			XElement xelement = rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Rules")).FirstOrDefault<XElement>();
			if (xelement == null)
			{
				throw new ClassificationRuleCollectionInvalidObjectException(Strings.ClassificationRuleCollectionInvalidObject);
			}
			XElement xelement2 = (from versionElement in xelement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Version"))
			where versionElement.Attribute("minEngineVersion") != null && version.Equals(versionElement.Attribute("minEngineVersion").Value, StringComparison.OrdinalIgnoreCase)
			select versionElement).FirstOrDefault<XElement>();
			if (xelement2 == null)
			{
				xelement2 = new XElement(XmlProcessingUtils.GetMceNsQualifiedNodeName("Version"), new XAttribute("minEngineVersion", version));
				xelement.AddFirst(xelement2);
			}
			xelement2.Add(dataClassificationXElement);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001364A0 File Offset: 0x001346A0
		internal static void AddLocalizedResource(XDocument rulePackXDoc, string dataClassificationId, XElement localizedResource)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			ArgumentValidator.ThrowIfNullOrEmpty("dataClassificationId", dataClassificationId);
			ArgumentValidator.ThrowIfNull("localizedResource", localizedResource);
			XElement xelement = rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("LocalizedStrings")).FirstOrDefault<XElement>();
			if (xelement == null)
			{
				throw new ClassificationRuleCollectionInvalidObjectException(Strings.ClassificationRuleCollectionInvalidObject);
			}
			List<XElement> list = (from resourceElement in xelement.Elements(XmlProcessingUtils.GetMceNsQualifiedNodeName("Resource"))
			where resourceElement.Attribute("idRef") != null && dataClassificationId.Equals(resourceElement.Attribute("idRef").Value, StringComparison.Ordinal)
			select resourceElement).ToList<XElement>();
			foreach (XElement xelement2 in list)
			{
				xelement2.Remove();
			}
			xelement.Add(localizedResource);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00136588 File Offset: 0x00134788
		internal static bool OptimizeRulePackXDoc(XDocument rulePackXDoc, DataClassificationConfig dataClassificationConfig)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			bool result = false;
			XElement xelement = rulePackXDoc.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Rules")).FirstOrDefault<XElement>();
			if (xelement == null)
			{
				throw new ClassificationRuleCollectionInvalidObjectException(Strings.ClassificationRuleCollectionInvalidObject);
			}
			List<XElement> list = (from versionElement in xelement.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("Version"))
			where versionElement.IsEmpty
			select versionElement).ToList<XElement>();
			foreach (XElement xelement2 in list)
			{
				xelement2.Remove();
				result = true;
			}
			List<string> list2 = (from versionedTextProcessorReference in TextProcessorUtils.GetTextProcessorReferences(rulePackXDoc.Root)
			select versionedTextProcessorReference.Key).Distinct(ClassificationDefinitionConstants.TextProcessorIdComparer).ToList<string>();
			List<XElement> list3 = XmlProcessingUtils.GetFingerprintProcessorsInRulePackage(rulePackXDoc).ToList<XElement>();
			foreach (XElement xelement3 in list3)
			{
				XAttribute xattribute = xelement3.Attribute("id");
				string item = (xattribute == null) ? string.Empty : xattribute.Value;
				if (list2.Contains(item))
				{
					if (dataClassificationConfig != null)
					{
						XAttribute xattribute2 = xelement3.Attribute("threshold");
						if (xattribute2 != null && !dataClassificationConfig.FingerprintThreshold.ToString().Equals(xattribute2.Value, StringComparison.Ordinal))
						{
							xattribute2.Value = dataClassificationConfig.FingerprintThreshold.ToString();
							result = true;
						}
					}
				}
				else
				{
					xelement3.Remove();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0013675C File Offset: 0x0013495C
		internal static string GetAttributeValue(XElement element, string attributeName)
		{
			ArgumentValidator.ThrowIfNull("element", element);
			ArgumentValidator.ThrowIfNullOrEmpty("attributeName", attributeName);
			XAttribute xattribute = element.Attribute(attributeName);
			if (xattribute != null)
			{
				return xattribute.Value;
			}
			return string.Empty;
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0013679B File Offset: 0x0013499B
		internal static bool IsFingerprintRuleCollection(XDocument rulePackXDoc)
		{
			ArgumentValidator.ThrowIfNull("rulePackXDoc", rulePackXDoc);
			return XmlProcessingUtils.GetRulePackId(rulePackXDoc).StartsWith("00000000-0000-0000-0001", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x001367BC File Offset: 0x001349BC
		internal static byte[] XmlDocumentToUtf16EncodedBytes(XmlDocument xmlDocument)
		{
			ArgumentValidator.ThrowIfNull("xmlDocument", xmlDocument);
			bool flag = false;
			foreach (object obj in xmlDocument)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (XmlNodeType.XmlDeclaration == xmlNode.NodeType)
				{
					((XmlDeclaration)xmlNode).Encoding = "UTF-16";
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				xmlDocument.InsertBefore(xmlDocument.CreateXmlDeclaration("1.0", "UTF-16", null), xmlDocument.DocumentElement);
			}
			return Encoding.Unicode.GetBytes(xmlDocument.InnerXml);
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00136868 File Offset: 0x00134A68
		internal static string XDocumentToStringWithDeclaration(XDocument xDocument)
		{
			ArgumentValidator.ThrowIfNull("xDocument", xDocument);
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xDocument.Save(stringWriter, SaveOptions.DisableFormatting | SaveOptions.OmitDuplicateNamespaces);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x001368B4 File Offset: 0x00134AB4
		internal static XName GetMceNsQualifiedNodeName(string nodeName)
		{
			return XName.Get(nodeName, "http://schemas.microsoft.com/office/2011/mce");
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00136910 File Offset: 0x00134B10
		private static IEnumerable<QueryMatchResult> ExecuteQueryMatching(IEnumerable<QueryMatchResult> mainQuery, Func<Dictionary<string, QueryMatchResult>, IEnumerable<KeyValuePair<string, XElement>>> queryForResolvingRuleAssociation, Action<Dictionary<string, QueryMatchResult>, KeyValuePair<string, XElement>> setAssociationData, Func<string, LocalizedString> createErrorMessageForMissingAssociation)
		{
			Dictionary<string, QueryMatchResult> queryMatchResults;
			try
			{
				queryMatchResults = mainQuery.ToDictionary((QueryMatchResult queryMatchResult) => queryMatchResult.MatchingRuleId, ClassificationDefinitionConstants.RuleIdComparer);
				if (queryMatchResults.Count == 0)
				{
					return Enumerable.Empty<QueryMatchResult>();
				}
				Dictionary<string, XElement> matchingAssociatedElements = queryForResolvingRuleAssociation(queryMatchResults).ToDictionary((KeyValuePair<string, XElement> ruleIdRefAndResourceElement) => ruleIdRefAndResourceElement.Key, (KeyValuePair<string, XElement> ruleIdRefAndResourceElement) => ruleIdRefAndResourceElement.Value, ClassificationDefinitionConstants.RuleIdComparer);
				if (queryMatchResults.Count != matchingAssociatedElements.Count)
				{
					List<string> list = (from ruleId in queryMatchResults.Keys
					where !matchingAssociatedElements.ContainsKey(ruleId)
					select ruleId).ToList<string>();
					LocalizedString value = createErrorMessageForMissingAssociation(string.Join(Strings.ClassificationRuleCollectionOffendingListSeparator, list));
					throw ClassificationDefinitionUtils.PopulateExceptionSource<XmlException, List<string>>(new XmlException(value), list);
				}
				Parallel.ForEach<KeyValuePair<string, XElement>>(matchingAssociatedElements, delegate(KeyValuePair<string, XElement> matchingAssociation)
				{
					setAssociationData(queryMatchResults, matchingAssociation);
				});
			}
			catch (AggregateException ex)
			{
				throw new XmlException(Strings.ClassificationRuleCollectionUnexpectedContents, ex.Flatten());
			}
			return queryMatchResults.Values;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00136AB0 File Offset: 0x00134CB0
		private static int GetInvariantOrLocalizedNameHitIndex(List<string> ruleNameQueriesList, XElement currentResourceElement, CultureInfo mainThreadCulture, bool ignoreCase)
		{
			DataClassificationLocalizableDetails dataClassificationLocalizableDetails = XmlProcessingUtils.ReadDefaultRuleMetadata(currentResourceElement);
			int num = ruleNameQueriesList.BinarySearch(dataClassificationLocalizableDetails.Name, StringComparer.Create(dataClassificationLocalizableDetails.Culture, ignoreCase));
			if (num < 0 && !dataClassificationLocalizableDetails.Culture.Equals(mainThreadCulture))
			{
				Dictionary<CultureInfo, string> dictionary = XmlProcessingUtils.ReadAllRuleNames(currentResourceElement);
				CultureInfo cultureInfo = mainThreadCulture;
				string text = null;
				while (!cultureInfo.Equals(CultureInfo.InvariantCulture) && !dictionary.TryGetValue(cultureInfo, out text))
				{
					cultureInfo = cultureInfo.Parent;
				}
				if (text != null && !cultureInfo.Equals(CultureInfo.InvariantCulture))
				{
					num = ruleNameQueriesList.BinarySearch(text, StringComparer.Create(cultureInfo, ignoreCase));
				}
			}
			return num;
		}

		// Token: 0x04002CD2 RID: 11474
		private static readonly Lazy<XmlSchemaSet> compiledRulePackageSchema = new Lazy<XmlSchemaSet>(new Func<XmlSchemaSet>(XmlProcessingUtils.CreateRulePackageCompiledSchema), LazyThreadSafetyMode.PublicationOnly);
	}
}
