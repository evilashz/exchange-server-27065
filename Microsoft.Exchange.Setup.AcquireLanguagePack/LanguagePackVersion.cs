using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000007 RID: 7
	internal class LanguagePackVersion
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002381 File Offset: 0x00000581
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002388 File Offset: 0x00000588
		public static bool SkipSignVerfForTesting
		{
			get
			{
				return LanguagePackVersion.skipSignVerfForTesting;
			}
			set
			{
				LanguagePackVersion.skipSignVerfForTesting = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002390 File Offset: 0x00000590
		public string LanguagePackVersioningPath
		{
			get
			{
				return this.langPackVersioningPath;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002398 File Offset: 0x00000598
		private static bool VerifyXmlSignature(SafeXmlDocument doc)
		{
			return LanguagePackVersion.SkipSignVerfForTesting || LanguagePackXmlHelper.VerifyXmlSignature(doc);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023AC File Offset: 0x000005AC
		public LanguagePackVersion(string pathToLocalXML, string pathToLangPackBundleXML)
		{
			ValidationHelper.ThrowIfFileNotExist(pathToLocalXML, "pathToLocalXML");
			ValidationHelper.ThrowIfFileNotExist(pathToLangPackBundleXML, "pathToLangPackBundleXML");
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.Load(pathToLocalXML);
			if (!LanguagePackVersion.VerifyXmlSignature(safeXmlDocument))
			{
				throw new LPVersioningValueException(Strings.SignatureFailed1(pathToLocalXML));
			}
			safeXmlDocument.Load(pathToLangPackBundleXML);
			if (!LanguagePackVersion.VerifyXmlSignature(safeXmlDocument))
			{
				throw new LPVersioningValueException(Strings.SignatureFailed1(pathToLangPackBundleXML));
			}
			Version version = new Version(LanguagePackVersion.GetBuildVersion(Path.GetFullPath(pathToLocalXML)));
			Version v = new Version(LanguagePackVersion.GetBuildVersion(Path.GetFullPath(pathToLangPackBundleXML)));
			if (version != null && v != null)
			{
				if (v >= version)
				{
					this.langPackVersioningPath = pathToLangPackBundleXML;
				}
				else
				{
					this.langPackVersioningPath = pathToLocalXML;
				}
				this.xmlDoc.Load(this.langPackVersioningPath);
				this.xmlElement = this.xmlDoc.DocumentElement;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000249C File Offset: 0x0000069C
		internal static string GetBuildVersion(string inputPath)
		{
			ValidationHelper.ThrowIfFileNotExist(inputPath, "inputPath");
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(inputPath);
			XmlElement documentElement = safeXmlDocument.DocumentElement;
			if (documentElement.HasAttribute("BuildVersion"))
			{
				return documentElement.GetAttribute("BuildVersion");
			}
			throw new LPVersioningValueException(Strings.UnableToFindBuildVersion1(inputPath));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024F8 File Offset: 0x000006F8
		public bool IsExchangeInApplicableRange(Version lpVersion)
		{
			Version exchangeVersion = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
			return this.IsExchangeInApplicableRange(lpVersion, exchangeVersion);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002528 File Offset: 0x00000728
		public bool IsExchangeInApplicableRange(Version lpVersion, Version exchangeVersion)
		{
			ValidationHelper.ThrowIfNull(this.xmlElement, "this.xmlElement");
			ValidationHelper.ThrowIfNull(this.xmlDoc, "this.xmlDoc");
			ValidationHelper.ThrowIfNull(lpVersion, "lpVersion");
			ValidationHelper.ThrowIfNull(exchangeVersion, "exchangeVersion");
			Version version = null;
			Version version2 = null;
			using (XmlNodeList elementsByTagName = this.xmlElement.GetElementsByTagName("ExVersion"))
			{
				ValidationHelper.ThrowIfNull(elementsByTagName, "xmlNodeList");
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlAttributeCollection attributes = xmlNode.Attributes;
					Version version3 = new Version(attributes[0].Value.ToString());
					if (!(lpVersion >= version3))
					{
						version2 = version3;
						break;
					}
					version = version3;
				}
			}
			return version != null && exchangeVersion >= version && (version2 == null || exchangeVersion < version2);
		}

		// Token: 0x0400000B RID: 11
		private static bool skipSignVerfForTesting;

		// Token: 0x0400000C RID: 12
		private SafeXmlDocument xmlDoc = new SafeXmlDocument();

		// Token: 0x0400000D RID: 13
		private XmlElement xmlElement;

		// Token: 0x0400000E RID: 14
		private string langPackVersioningPath;
	}
}
