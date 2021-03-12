using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000822 RID: 2082
	internal static class ClassificationDefinitionUtils
	{
		// Token: 0x06004804 RID: 18436 RVA: 0x00127D4C File Offset: 0x00125F4C
		private static ExchangeBuild GetCurrentAssemblyVersion()
		{
			if (ClassificationDefinitionUtils.currentAssemblyVersion == null)
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
				ClassificationDefinitionUtils.currentAssemblyVersion = new ExchangeBuild?(new ExchangeBuild((byte)versionInfo.FileMajorPart, (byte)versionInfo.FileMinorPart, (ushort)versionInfo.FileBuildPart, (ushort)versionInfo.FilePrivatePart));
			}
			return ClassificationDefinitionUtils.currentAssemblyVersion.Value;
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x00127DAC File Offset: 0x00125FAC
		internal static bool TryCompressXmlBytes(byte[] uncompressedXmlBytes, out byte[] compressedXmlBytes)
		{
			compressedXmlBytes = null;
			if (uncompressedXmlBytes == null)
			{
				return false;
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Package package = Package.Open(memoryStream, FileMode.Create))
				{
					PackagePart packagePart = package.CreatePart(ClassificationDefinitionUtils.defaultXmlContentPackagePartUri, "text/xml", CompressionOption.Normal);
					try
					{
						packagePart.GetStream().Write(uncompressedXmlBytes, 0, uncompressedXmlBytes.Length);
					}
					catch (IOException ex)
					{
						TaskLogger.Trace("IOException encountered while compressing classification rule collection payload: {0}", new object[]
						{
							ex.Message
						});
						return false;
					}
				}
				compressedXmlBytes = memoryStream.ToArray();
			}
			return true;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x00127E64 File Offset: 0x00126064
		internal static Stream LoadStreamFromEmbeddedResource(string embeddedResourceName)
		{
			if (string.IsNullOrWhiteSpace(embeddedResourceName))
			{
				throw new ArgumentException(new ArgumentException().Message, "embeddedResourceName");
			}
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName);
			if (manifestResourceStream == null)
			{
				throw new FileNotFoundException(new FileNotFoundException().Message, embeddedResourceName);
			}
			return manifestResourceStream;
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x00127EB0 File Offset: 0x001260B0
		internal static bool TryUncompressXmlBytes(byte[] compressedXmlBytes, out byte[] uncompressedXmlBytes, out Exception operationException)
		{
			uncompressedXmlBytes = null;
			operationException = null;
			if (compressedXmlBytes == null)
			{
				operationException = new ArgumentNullException("compressedXmlBytes");
				return false;
			}
			using (Stream stream = new MemoryStream(compressedXmlBytes))
			{
				using (Package package = Package.Open(stream, FileMode.Open))
				{
					PackagePart part;
					try
					{
						part = package.GetPart(ClassificationDefinitionUtils.defaultXmlContentPackagePartUri);
					}
					catch (InvalidOperationException ex)
					{
						operationException = ex;
						return false;
					}
					using (MemoryStream memoryStream = new MemoryStream(compressedXmlBytes.Length))
					{
						try
						{
							part.GetStream().CopyTo(memoryStream);
						}
						catch (IOException ex2)
						{
							operationException = ex2;
							return false;
						}
						uncompressedXmlBytes = memoryStream.ToArray();
					}
				}
			}
			return true;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x00127F94 File Offset: 0x00126194
		internal static ADObjectId GetClassificationRuleCollectionContainerId(IConfigDataProvider session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			return ((IConfigurationSession)session).GetOrgContainerId().GetDescendantId(ClassificationDefinitionConstants.ClassificationDefinitionsRdn);
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x00127FBC File Offset: 0x001261BC
		internal static bool IsAdObjectAClassificationRuleCollection(TransportRule transportRule)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			return ((ADObjectId)transportRule.Identity).Parent.DistinguishedName.Equals(transportRule.Session.GetOrgContainerId().GetDescendantId(ClassificationDefinitionConstants.ClassificationDefinitionsRdn).ToDNString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0012800C File Offset: 0x0012620C
		internal static XDocument GetRuleCollectionDocumentFromTransportRule(TransportRule transportRule)
		{
			if (transportRule == null)
			{
				throw new ArgumentNullException("transportRule");
			}
			if (!ClassificationDefinitionUtils.IsAdObjectAClassificationRuleCollection(transportRule))
			{
				throw new InvalidOperationException();
			}
			if (transportRule.ReplicationSignature == null)
			{
				throw new ArgumentException(new ArgumentException().Message, "transportRule");
			}
			byte[] bytes;
			Exception ex;
			if (!ClassificationDefinitionUtils.TryUncompressXmlBytes(transportRule.ReplicationSignature, out bytes, out ex))
			{
				throw new AggregateException(new Exception[]
				{
					ex
				});
			}
			XDocument result;
			try
			{
				result = XDocument.Parse(Encoding.Unicode.GetString(bytes));
			}
			catch (ArgumentException ex2)
			{
				throw new AggregateException(new Exception[]
				{
					ex2
				});
			}
			return result;
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x001280B4 File Offset: 0x001262B4
		internal static XDocument CreateRuleCollectionDocumentFromTemplate(string rulePackId, string organizationId, string organizationName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("rulePackId", rulePackId);
			ArgumentValidator.ThrowIfNullOrEmpty("organizationId", organizationId);
			ArgumentValidator.ThrowIfNullOrEmpty("organizationName", organizationName);
			string text = string.Empty;
			using (Stream stream = ClassificationDefinitionUtils.LoadStreamFromEmbeddedResource("FingerprintRulePackTemplate.xml"))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new FingerprintRulePackTemplateCorruptedException("FingerprintRulePackTemplate.xml");
			}
			XDocument xdocument = XDocument.Parse(text);
			XElement xelement = xdocument.Root.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("RulePack"));
			xelement.SetAttributeValue("id", rulePackId);
			XElement xelement2 = xelement.Element(XmlProcessingUtils.GetMceNsQualifiedNodeName("Publisher"));
			xelement2.SetAttributeValue("id", organizationId);
			foreach (XElement xelement3 in xelement.Descendants(XmlProcessingUtils.GetMceNsQualifiedNodeName("PublisherName")))
			{
				xelement3.SetValue(organizationName);
			}
			XmlProcessingUtils.SetRulePackVersionFromAssemblyFileVersion(xdocument);
			return xdocument;
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x001281F4 File Offset: 0x001263F4
		internal static string CreateHierarchicalIdentityString(string organizationHierarchy, string objectName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(organizationHierarchy ?? string.Empty);
			stringBuilder.Append(ClassificationDefinitionConstants.HierarchicalIdentitySeparatorChar);
			stringBuilder.Append(objectName ?? string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0012823C File Offset: 0x0012643C
		internal static string GetMceForAdminToolInstallBase(bool shouldHandleException = false)
		{
			string result;
			try
			{
				result = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}
			catch (PathTooLongException underlyingException)
			{
				if (!shouldHandleException)
				{
					throw;
				}
				ClassificationDefinitionsDiagnosticsReporter.Instance.WriteClassificationEngineConfigurationErrorInformation(0, underlyingException);
				result = null;
			}
			return result;
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00128284 File Offset: 0x00126484
		internal static string GetMceExecutablePath(bool shouldHandleException = false)
		{
			string mceForAdminToolInstallBase = ClassificationDefinitionUtils.GetMceForAdminToolInstallBase(shouldHandleException);
			if (mceForAdminToolInstallBase != null)
			{
				return Path.Combine(mceForAdminToolInstallBase, ClassificationDefinitionConstants.MceExecutableFileName);
			}
			return null;
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x001282A8 File Offset: 0x001264A8
		internal static string GetLocalMachineMceConfigDirectory(bool shouldHandleException = false)
		{
			string mceForAdminToolInstallBase = ClassificationDefinitionUtils.GetMceForAdminToolInstallBase(shouldHandleException);
			if (mceForAdminToolInstallBase != null)
			{
				return Path.Combine(mceForAdminToolInstallBase, ClassificationDefinitionConstants.OnDiskMceConfigurationDirName);
			}
			return null;
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x001282CC File Offset: 0x001264CC
		internal static XmlReaderSettings CreateSafeXmlReaderSettings()
		{
			return new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x001282F0 File Offset: 0x001264F0
		internal static T GetMatchingLocalizedInfo<T>(Dictionary<CultureInfo, T> localizedInfoRepository, T defaultValue) where T : class
		{
			if (localizedInfoRepository == null)
			{
				throw new ArgumentNullException("localizedInfoRepository");
			}
			CultureInfo cultureInfo = CultureInfo.CurrentCulture;
			T t = default(T);
			while (!cultureInfo.Equals(CultureInfo.InvariantCulture) && !localizedInfoRepository.TryGetValue(cultureInfo, out t))
			{
				cultureInfo = cultureInfo.Parent;
			}
			T result;
			if ((result = t) == null)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x00128345 File Offset: 0x00126545
		internal static TException PopulateExceptionSource<TException, TSource>(TException exception, TSource exceptionSource) where TException : Exception
		{
			if (exception != null)
			{
				exception.Data[ClassificationDefinitionConstants.ExceptionSourcesListKey] = exceptionSource;
			}
			return exception;
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0012837F File Offset: 0x0012657F
		internal static IEnumerable<DataClassificationPresentationObject> FilterHigherVersionRules(IEnumerable<DataClassificationPresentationObject> unfilteredRules)
		{
			if (unfilteredRules == null)
			{
				throw new ArgumentNullException("unfilteredRules");
			}
			return from rule in unfilteredRules
			where rule.MinEngineVersion <= ClassificationDefinitionUtils.GetCurrentAssemblyVersion()
			select rule;
		}

		// Token: 0x04002BE2 RID: 11234
		private static readonly Uri defaultXmlContentPackagePartUri = PackUriHelper.CreatePartUri(new Uri("config", UriKind.Relative));

		// Token: 0x04002BE3 RID: 11235
		private static ExchangeBuild? currentAssemblyVersion;
	}
}
