using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000F8 RID: 248
	public static class ExtensionDataHelper
	{
		// Token: 0x06000A72 RID: 2674 RVA: 0x00028C7C File Offset: 0x00026E7C
		internal static string FormatExtensionId(string extensionId)
		{
			if (string.IsNullOrWhiteSpace(extensionId))
			{
				return extensionId;
			}
			string text = extensionId.Replace("urn:uuid:", string.Empty).Trim(ExtensionDataHelper.TrimCharArray);
			return text.ToLowerInvariant();
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00028CB4 File Offset: 0x00026EB4
		public static string GetDeploymentId(string domain)
		{
			string text = string.Empty;
			try
			{
				ADSessionSettings adsessionSettings = ExtensionDataHelper.CreateRootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domain);
				if (adsessionSettings != null)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 80, "GetDeploymentId", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\extension\\ExtensionDataHelper.cs");
					AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
					if (defaultAcceptedDomain != null && defaultAcceptedDomain.DomainName != null && defaultAcceptedDomain.DomainName.Domain != null)
					{
						text = defaultAcceptedDomain.DomainName.Domain;
					}
					else
					{
						ExtensionDataHelper.Tracer.TraceError(0L, "Failed to get a valid default accepted domain for deployment id.");
					}
				}
			}
			catch (ADTransientException ex)
			{
				ExtensionDataHelper.Tracer.TraceError<string>(0L, "Failed to get default accepted domain for deployment id. Exception: {0}", ex.Message);
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				ExtensionDataHelper.Tracer.TraceInformation<string>(0, 0L, "Can not get default authorative accepted domain, fall back to primary smtp domain: {0}.", domain);
				ExtensionDiagnostics.Logger.LogEvent(ApplicationLogicEventLogConstants.Tuple_FailedToGetDeploymentId, null, new object[]
				{
					"ProcessEntitlementToken",
					domain
				});
				text = domain;
			}
			return text;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00028DA0 File Offset: 0x00026FA0
		internal static bool VerifyDeploymentId(string deploymentId, string domain)
		{
			if (!string.IsNullOrWhiteSpace(deploymentId))
			{
				try
				{
					ADSessionSettings adsessionSettings = ExtensionDataHelper.CreateRootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domain);
					if (adsessionSettings != null)
					{
						IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, adsessionSettings, 142, "VerifyDeploymentId", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\extension\\ExtensionDataHelper.cs");
						AcceptedDomain acceptedDomainByDomainName = tenantOrTopologyConfigurationSession.GetAcceptedDomainByDomainName(deploymentId);
						if (acceptedDomainByDomainName != null)
						{
							return true;
						}
					}
				}
				catch (ADTransientException ex)
				{
					ExtensionDataHelper.Tracer.TraceError<string>(0L, "Failed to get accepted domain by deployment id. Exception: {0}", ex.Message);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00028E1C File Offset: 0x0002701C
		internal static bool TryGetAttributeValue(XmlAttribute attribute, out string value)
		{
			value = null;
			if (attribute == null)
			{
				return false;
			}
			value = attribute.Value;
			return !string.IsNullOrEmpty(value);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00028E3C File Offset: 0x0002703C
		internal static bool TryGetNameSpaceStrippedAttributeValue(XmlAttribute attribute, out string value)
		{
			if (!ExtensionDataHelper.TryGetAttributeValue(attribute, out value))
			{
				return false;
			}
			int num = value.LastIndexOf(':');
			if (-1 != num)
			{
				num++;
				value = value.Substring(num, value.Length - num);
				if (string.IsNullOrEmpty(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00028E85 File Offset: 0x00027085
		internal static void ValidateRegex(string regexPattern)
		{
			new Regex(regexPattern, RegexOptions.ECMAScript);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00028E93 File Offset: 0x00027093
		internal static bool ConvertXmlStringToBoolean(string value)
		{
			return value != null && (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value == "1");
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00028EB8 File Offset: 0x000270B8
		internal static bool IsValidUri(string uriString)
		{
			Uri uri;
			return Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out uri);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00028ED0 File Offset: 0x000270D0
		internal static SafeXmlDocument GetManifest(SafeXmlDocument xmlDoc)
		{
			if (ExtensionDataHelper.xmlSchemaSet.Count == 0)
			{
				ExtensionDataHelper.xmlSchemaSet = new XmlSchemaSet();
				foreach (string text in SchemaConstants.SchemaNamespaceUriToFile.Keys)
				{
					string schemaUri = Path.Combine(ExchangeSetupContext.InstallPath, "bin", SchemaConstants.SchemaNamespaceUriToFile[text]);
					ExtensionDataHelper.xmlSchemaSet.Add(text, schemaUri);
				}
			}
			xmlDoc.Schemas = ExtensionDataHelper.xmlSchemaSet;
			xmlDoc.Validate(new ValidationEventHandler(ExtensionDataHelper.InvalidManifestEventHandler));
			string uri;
			string text2;
			if (!ExtensionDataHelper.TryGetOfficeAppSchemaInfo(xmlDoc, "http://schemas.microsoft.com/office/appforoffice/", out uri, out text2))
			{
				throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonManifestSchemaUnknown));
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
			xmlNamespaceManager.AddNamespace("owe", uri);
			SafeXmlDocument safeXmlDocument = null;
			string xpath = "//owe:OfficeApp";
			XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath, xmlNamespaceManager);
			if (xmlNode != null)
			{
				safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.PreserveWhitespace = true;
				safeXmlDocument.LoadXml(xmlNode.OuterXml);
			}
			return safeXmlDocument;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00028FF8 File Offset: 0x000271F8
		internal static bool TryGetOfficeAppSchemaInfo(XmlNode xmlDoc, string nonVersionSpecificNamspacePart, out string namespaceUri, out string schemaVersion)
		{
			namespaceUri = null;
			schemaVersion = null;
			for (XmlNode xmlNode = xmlDoc.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NamespaceURI.StartsWith(nonVersionSpecificNamspacePart, StringComparison.OrdinalIgnoreCase))
				{
					namespaceUri = xmlNode.NamespaceURI;
					schemaVersion = xmlNode.NamespaceURI.Substring(nonVersionSpecificNamspacePart.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002904C File Offset: 0x0002724C
		internal static SchemaParser GetSchemaParser(SafeXmlDocument xmlDoc, ExtensionInstallScope scope)
		{
			string text;
			string schemaVersion;
			if (!ExtensionDataHelper.TryGetOfficeAppSchemaInfo(xmlDoc, "http://schemas.microsoft.com/office/appforoffice/", out text, out schemaVersion))
			{
				throw new OwaExtensionOperationException(Strings.ErrorReasonManifestSchemaUnknown);
			}
			string a;
			if ((a = text) != null)
			{
				if (a == "http://schemas.microsoft.com/office/appforoffice/1.0")
				{
					return new SchemaParser1_0(xmlDoc, scope);
				}
				if (a == "http://schemas.microsoft.com/office/appforoffice/1.1")
				{
					return new SchemaParser1_1(xmlDoc, scope);
				}
			}
			throw new OwaExtensionOperationException(Strings.ErrorReasonManifestVersionNotSupported(schemaVersion, ExchangeSetupContext.InstalledVersion));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000290B8 File Offset: 0x000272B8
		internal static string GetExceptionMessages(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder(2 * e.Message.Length);
			do
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(e.Message);
				e = e.InnerException;
			}
			while (e != null);
			return stringBuilder.ToString();
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002910C File Offset: 0x0002730C
		private static void InvalidManifestEventHandler(object sender, ValidationEventArgs e)
		{
			if (e.Exception != null)
			{
				XmlSchemaValidationException ex = e.Exception as XmlSchemaValidationException;
				if (ex != null)
				{
					XmlElement xmlElement = ex.SourceObject as XmlElement;
					if (xmlElement != null && string.Equals(xmlElement.Name, "Signature", StringComparison.OrdinalIgnoreCase))
					{
						return;
					}
				}
			}
			throw new OwaExtensionOperationException(Strings.ErrorInvalidManifestData(Strings.ErrorReasonManifestValidationError(e.Exception.Message)), e.Exception);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00029178 File Offset: 0x00027378
		private static ADSessionSettings CreateRootOrgOrSingleTenantFromAcceptedDomainAutoDetect(string domain)
		{
			ADSessionSettings result = null;
			try
			{
				if (Globals.IsDatacenter)
				{
					if (!string.IsNullOrWhiteSpace(domain))
					{
						result = ADSessionSettings.FromTenantAcceptedDomain(domain);
					}
				}
				else
				{
					result = ADSessionSettings.FromRootOrgScopeSet();
				}
			}
			catch (CannotResolveTenantNameException ex)
			{
				ExtensionDataHelper.Tracer.TraceError<string>(0L, "Failed to resolve tenant name based on provided domain", ex.Message);
			}
			return result;
		}

		// Token: 0x04000523 RID: 1315
		private const string LeftBracket = "{";

		// Token: 0x04000524 RID: 1316
		private const string RightBracket = "}";

		// Token: 0x04000525 RID: 1317
		private const string BinFolderName = "bin";

		// Token: 0x04000526 RID: 1318
		private static readonly char[] TrimCharArray = new char[]
		{
			'{',
			'}'
		};

		// Token: 0x04000527 RID: 1319
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x04000528 RID: 1320
		private static XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x06000A82 RID: 2690
		internal delegate bool TryModifySourceLocationDelegate(IExchangePrincipal exchangePrincipal, XmlAttribute xmlAttribute, ExtensionData extensionData, out Exception exception);
	}
}
