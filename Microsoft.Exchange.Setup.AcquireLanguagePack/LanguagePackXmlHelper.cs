using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Xml;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Setup.SignatureVerification;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000008 RID: 8
	internal static class LanguagePackXmlHelper
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000264C File Offset: 0x0000084C
		public static bool VerifyXmlSignature(SafeXmlDocument doc)
		{
			string location = Assembly.GetExecutingAssembly().Location;
			SignVerfWrapper signVerfWrapper = new SignVerfWrapper();
			bool flag = signVerfWrapper.VerifyEmbeddedSignature(location, false);
			if (flag)
			{
				bool result = false;
				SignedXml signedXml = new SignedXml(doc);
				using (XmlNodeList elementsByTagName = doc.GetElementsByTagName("Signature"))
				{
					if (elementsByTagName != null && elementsByTagName.Count > 0)
					{
						signedXml.LoadXml((XmlElement)elementsByTagName[0]);
						result = signedXml.CheckSignature();
					}
				}
				return result;
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026D8 File Offset: 0x000008D8
		public static List<DownloadFileInfo> GetDownloadFileInfoFromXml(string lpVersioningXml, bool skipSignatureCheckForTestOnly)
		{
			ValidationHelper.ThrowIfFileNotExist(lpVersioningXml, "lpVersioningXml");
			Version version = new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.PreserveWhitespace = true;
			safeXmlDocument.Load(lpVersioningXml);
			if (!skipSignatureCheckForTestOnly && !LanguagePackXmlHelper.VerifyXmlSignature(safeXmlDocument))
			{
				throw new SignatureVerificationException(Strings.SignatureFailed1(lpVersioningXml));
			}
			safeXmlDocument.PreserveWhitespace = false;
			safeXmlDocument.Load(lpVersioningXml);
			XmlElement documentElement = safeXmlDocument.DocumentElement;
			List<DownloadFileInfo> list = new List<DownloadFileInfo>();
			list.Add(LanguagePackXmlHelper.GetLPDownloadFileInfoFromXmlElement(documentElement, version));
			list.AddRange(LanguagePackXmlHelper.GetMspsDownloadFileInfoFromXmlElement(documentElement));
			if (!LanguagePackXmlHelper.VerifyDownloadFileInfo(list))
			{
				throw new LPVersioningValueException(Strings.fWLinkNotFound(version.ToString(), lpVersioningXml));
			}
			return list;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000278E File Offset: 0x0000098E
		public static string ExtractXMLFromBundle(string bundleFileName, string toPath)
		{
			ValidationHelper.ThrowIfFileNotExist(bundleFileName, "bundleFileName");
			ValidationHelper.ThrowIfDirectoryNotExist(toPath, "toPath");
			EmbeddedCabWrapper.ExtractFiles(bundleFileName, toPath, "LPVersioning.xml");
			return Path.Combine(toPath, "LPVersioning.xml");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027C0 File Offset: 0x000009C0
		public static bool ContainsOnlyDownloadedFiles(string path)
		{
			if (!Directory.Exists(path))
			{
				return false;
			}
			string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
			if (files == null || files.Length == 0)
			{
				return false;
			}
			foreach (string filePath in files)
			{
				if (!DownloadFileInfo.IsFileNameValid(filePath, "\\.msp") && !DownloadFileInfo.IsFileNameValid(filePath, "LanguagePackBundle.exe".ToLower()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002830 File Offset: 0x00000A30
		private static DownloadFileInfo GetLPDownloadFileInfoFromXmlElement(XmlElement xmlElement, Version ceilingVersion)
		{
			ValidationHelper.ThrowIfNull(xmlElement, "xmlElement");
			DownloadFileInfo result = null;
			using (XmlNodeList elementsByTagName = xmlElement.GetElementsByTagName("ExVersion"))
			{
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlAttributeCollection attributes = xmlNode.Attributes;
					Version v = new Version(attributes[0].Value.ToString());
					if (ceilingVersion >= v)
					{
						string text = xmlNode.FirstChild.InnerText.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							result = new DownloadFileInfo(text, "LanguagePackBundle.exe".ToLower(), true);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002910 File Offset: 0x00000B10
		private static List<DownloadFileInfo> GetMspsDownloadFileInfoFromXmlElement(XmlElement xmlElement)
		{
			ValidationHelper.ThrowIfNull(xmlElement, "xmlElement");
			List<DownloadFileInfo> list = new List<DownloadFileInfo>();
			using (XmlNodeList elementsByTagName = xmlElement.GetElementsByTagName("ExchangeUpdate"))
			{
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2.NodeType == XmlNodeType.Element)
						{
							string text = xmlNode2.InnerText.ToString();
							if (!string.IsNullOrEmpty(text))
							{
								list.Add(new DownloadFileInfo(text, "\\.msp", true));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A18 File Offset: 0x00000C18
		private static bool VerifyDownloadFileInfo(List<DownloadFileInfo> downloads)
		{
			if (downloads == null || downloads.Count < 3)
			{
				return false;
			}
			foreach (DownloadFileInfo downloadFileInfo in downloads)
			{
				if (downloadFileInfo == null)
				{
					return false;
				}
				if (!downloadFileInfo.UriLink.ToString().StartsWith("http://go.microsoft.com/", true, CultureInfo.CurrentCulture))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400000F RID: 15
		public const int SizeOfBundle = 500000000;

		// Token: 0x04000010 RID: 16
		public const string LPVersioningXml = "LPVersioning.xml";

		// Token: 0x04000011 RID: 17
		public const string LanguagePackBundleEXE = "LanguagePackBundle.exe";

		// Token: 0x04000012 RID: 18
		public const string LPNode = "ExVersion";

		// Token: 0x04000013 RID: 19
		public const string LanguageBundleCompatibilityNode = "LanguageBundleCompatibility";

		// Token: 0x04000014 RID: 20
		public const string BuildVersionAttribute = "BuildVersion";

		// Token: 0x04000015 RID: 21
		public const string MspNode = "ExchangeUpdate";

		// Token: 0x04000016 RID: 22
		public const string SignatureNode = "Signature";

		// Token: 0x04000017 RID: 23
		public const string MspFileFilter = "*.msp";

		// Token: 0x04000018 RID: 24
		private const string MspFileNamePattern = "\\.msp";

		// Token: 0x04000019 RID: 25
		private const int MinLinks = 3;
	}
}
