using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Microsoft.com.IPC.WSService;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009BE RID: 2494
	internal static class RMUtil
	{
		// Token: 0x06003636 RID: 13878 RVA: 0x0008A3E4 File Offset: 0x000885E4
		public static bool TryCreateUri(string toBeCreated, out Uri uriCreated)
		{
			uriCreated = null;
			if (string.IsNullOrEmpty(toBeCreated))
			{
				return false;
			}
			if (!Uri.TryCreate(toBeCreated, UriKind.Absolute, out uriCreated))
			{
				uriCreated = null;
				return false;
			}
			return true;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x0008A404 File Offset: 0x00088604
		public static bool IsWellFormedRmServiceUrl(Uri toBeChecked)
		{
			return !(toBeChecked == null) && toBeChecked.IsAbsoluteUri && (!(Uri.UriSchemeHttp != toBeChecked.Scheme) || !(Uri.UriSchemeHttps != toBeChecked.Scheme)) && string.IsNullOrEmpty(toBeChecked.Query) && string.IsNullOrEmpty(toBeChecked.Fragment) && toBeChecked.AbsoluteUri.Length <= 1024 && toBeChecked.AbsoluteUri.IndexOf(',') == -1;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x0008A491 File Offset: 0x00088691
		public static string ConvertUriToLicenseUrl(Uri offeredUri)
		{
			return RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/licensing/license.asmx");
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x0008A49E File Offset: 0x0008869E
		public static string ConvertUriToPublishUrl(Uri offeredUri)
		{
			return RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/licensing/publish.asmx");
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0008A4AB File Offset: 0x000886AB
		public static string ConvertUriToServerCertificationUrl(Uri offeredUri)
		{
			return RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/certification/servercertification.asmx");
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0008A4B8 File Offset: 0x000886B8
		public static string ConvertUriToTemplateDistributionUrl(Uri offeredUri)
		{
			return RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/licensing/templatedistribution.asmx");
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0008A4C5 File Offset: 0x000886C5
		public static string ConvertUriToServerUrl(Uri offeredUri, bool vdirCertification)
		{
			return RMUtil.ConvertUriToRmServiceUrl(offeredUri, vdirCertification ? "_wmcs/certification/server.asmx" : "_wmcs/licensing/server.asmx");
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0008A4DC File Offset: 0x000886DC
		public static Uri ConvertUriToLicenseLocationDistributionPoint(Uri offeredUri)
		{
			string text = RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/licensing");
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Uri(text.ToLowerInvariant(), UriKind.Absolute);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x0008A50C File Offset: 0x0008870C
		public static Uri ConvertUriToCertificateLocationDistributionPoint(Uri offeredUri)
		{
			string text = RMUtil.ConvertUriToRmServiceUrl(offeredUri, "_wmcs/certification");
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Uri(text.ToLowerInvariant(), UriKind.Absolute);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x0008A53C File Offset: 0x0008873C
		private static string ConvertUriToRmServiceUrl(Uri offeredUri, string localPart)
		{
			if (!RMUtil.IsWellFormedRmServiceUrl(offeredUri))
			{
				return null;
			}
			string text = offeredUri.ToString();
			if (text.EndsWith(localPart, StringComparison.OrdinalIgnoreCase))
			{
				return text;
			}
			string components = offeredUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
			if (string.IsNullOrEmpty(components))
			{
				return null;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				components,
				localPart
			});
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x0008A59C File Offset: 0x0008879C
		private static DateTime GetLicenseExpirationTime(XmlNode license)
		{
			DateTime maxValue = DateTime.MaxValue;
			XPathNavigator xpathNavigator = license.CreateNavigator();
			XPathNodeIterator xpathNodeIterator = xpathNavigator.Select("./BODY/VALIDITYTIME/UNTIL");
			if (xpathNodeIterator.MoveNext() && xpathNodeIterator.Current != null)
			{
				string value = xpathNodeIterator.Current.Value;
				if (!string.IsNullOrEmpty(value) && !DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out maxValue))
				{
					maxValue = DateTime.MaxValue;
				}
			}
			return maxValue;
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x0008A602 File Offset: 0x00088802
		public static DateTime GetRacExpirationTime(XmlNode rac)
		{
			return RMUtil.GetLicenseExpirationTime(rac);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x0008A60A File Offset: 0x0008880A
		public static DateTime GetClcExpirationTime(XmlNode clc)
		{
			return RMUtil.GetLicenseExpirationTime(clc);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0008A614 File Offset: 0x00088814
		public static bool TryConvertStringToXmlNode(string source, out XmlNode xmlNode)
		{
			xmlNode = null;
			bool result = false;
			if (!string.IsNullOrEmpty(source))
			{
				XmlDocument xmlDocument = new SafeXmlDocument();
				try
				{
					xmlDocument.InnerXml = source;
					xmlNode = xmlDocument.DocumentElement;
					if (xmlNode != null)
					{
						result = true;
					}
				}
				catch (XmlException)
				{
					xmlNode = null;
				}
			}
			return result;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x0008A664 File Offset: 0x00088864
		public static bool IsValidCertificateChain(XmlNode[] certChain)
		{
			if (certChain == null || certChain.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < certChain.Length; i++)
			{
				if (certChain[i] == null || string.IsNullOrEmpty(certChain[i].OuterXml))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0008A6A0 File Offset: 0x000888A0
		public static string ConvertXmlNodeArrayToString(XmlNode[] nodes)
		{
			if (nodes == null || nodes.Length == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(nodes[0].OuterXml);
			for (int i = 1; i < nodes.Length; i++)
			{
				stringBuilder.Append(nodes[i].OuterXml);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0008A6E8 File Offset: 0x000888E8
		public static string ConvertXrmlCertificateChainToString(XrmlCertificateChain certs)
		{
			if (certs == null)
			{
				throw new ArgumentNullException("certs");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in certs.CertificateChain)
			{
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x0008A730 File Offset: 0x00088930
		public static XrmlCertificateChain ConvertStringToXrmlCertificateChain(string appendedCerts)
		{
			if (appendedCerts == null)
			{
				throw new ArgumentNullException("appendedCerts");
			}
			int num = 0;
			int num2 = appendedCerts.IndexOf("<?xml", num, StringComparison.OrdinalIgnoreCase);
			if (num2 == -1)
			{
				throw new FormatException("Unable to find the xml version tag for the first certificate");
			}
			List<string> list = new List<string>();
			while ((num = appendedCerts.IndexOf("</XrML>", num, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				num += "</XrML>".Length;
				string item = appendedCerts.Substring(num2, num - num2);
				list.Add(item);
				num2 = appendedCerts.IndexOf("<?xml", num, StringComparison.OrdinalIgnoreCase);
				if (num2 == -1)
				{
					break;
				}
			}
			if (list.Count != 0)
			{
				XrmlCertificateChain xrmlCertificateChain = new XrmlCertificateChain();
				string[] array = new string[list.Count];
				list.CopyTo(array, 0);
				xrmlCertificateChain.CertificateChain = array;
				return xrmlCertificateChain;
			}
			throw new FormatException("No valid certificates");
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x0008A7F0 File Offset: 0x000889F0
		public static bool TryConvertAppendedCertsToXmlNodeArray(string appendedCerts, out XmlNode[] nodes)
		{
			int num = 0;
			int num2 = 0;
			LinkedList<XmlNode> linkedList = new LinkedList<XmlNode>();
			while ((num2 = appendedCerts.IndexOf("</XrML>", num2, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				num2 += "</XrML>".Length;
				string source = appendedCerts.Substring(num, num2 - num);
				XmlNode value;
				if (!RMUtil.TryConvertStringToXmlNode(source, out value))
				{
					nodes = null;
					return false;
				}
				linkedList.AddLast(value);
				num = num2;
			}
			int count = linkedList.Count;
			if (count == 0)
			{
				nodes = null;
				return false;
			}
			nodes = new XmlNode[count];
			linkedList.CopyTo(nodes, 0);
			return true;
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x0008A874 File Offset: 0x00088A74
		public static bool TryConvertCertChainToXmlNodeArray(string certChain, out XmlNode[] nodeArray)
		{
			nodeArray = null;
			bool result = false;
			if (!string.IsNullOrEmpty(certChain))
			{
				int startIndex = 0;
				if (certChain.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
				{
					startIndex = "?>".Length + certChain.IndexOf("?>", StringComparison.Ordinal);
				}
				StringBuilder stringBuilder = new StringBuilder("<SunShine>");
				stringBuilder.Append(certChain.Substring(startIndex));
				stringBuilder.Append("</SunShine>");
				XmlNode xmlNode;
				if (RMUtil.TryConvertStringToXmlNode(stringBuilder.ToString(), out xmlNode))
				{
					using (XmlNodeList childNodes = xmlNode.ChildNodes)
					{
						if (childNodes.Count > 0)
						{
							nodeArray = new XmlNode[childNodes.Count];
							for (int i = 0; i < childNodes.Count; i++)
							{
								nodeArray[i] = childNodes[i];
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x0008A954 File Offset: 0x00088B54
		public static bool TryConvertCertChainStringArrayToXmlNodeArray(string[] stringArray, out XmlNode[] nodeArray)
		{
			if (stringArray == null || stringArray.Length < 1 || string.IsNullOrEmpty(stringArray[0]))
			{
				nodeArray = null;
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in stringArray)
			{
				if (!string.IsNullOrEmpty(text))
				{
					if (text.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
					{
						int num = text.IndexOf("?>", StringComparison.Ordinal);
						if (num == -1)
						{
							nodeArray = null;
							return false;
						}
						int startIndex = "?>".Length + num;
						stringBuilder.Append(text.Substring(startIndex));
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
			}
			string appendedCerts = stringBuilder.ToString();
			return RMUtil.TryConvertAppendedCertsToXmlNodeArray(appendedCerts, out nodeArray);
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0008AA04 File Offset: 0x00088C04
		public static bool TryGetIssuanceLicenseAndUrls(string publishLicense, out Uri intranetUri, out Uri extranetUri, out XmlNode[] license)
		{
			license = null;
			intranetUri = null;
			extranetUri = null;
			Exception ex = null;
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			bool result;
			try
			{
				if (string.IsNullOrEmpty(publishLicense))
				{
					ExTraceGlobals.RightsManagementTracer.TraceError(0L, "The issuance license is not present or is empty.");
					result = false;
				}
				else if (publishLicense.Length >= 1047552)
				{
					ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Issuance license's length exceed the max allowed size.");
					result = false;
				}
				else
				{
					DrmClientUtils.ParsePublishLicense(publishLicense, out intranetUri, out extranetUri);
					if (!RMUtil.TryConvertCertChainToXmlNodeArray(publishLicense, out license) || license == null)
					{
						ExTraceGlobals.RightsManagementTracer.TraceError(0L, "Invalid issuance license");
						result = false;
					}
					else
					{
						result = true;
					}
				}
			}
			catch (RightsManagementException ex2)
			{
				ex = ex2;
				result = false;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.RightsManagementTracer.TraceError<string>(0L, "Exception thrown while trying to retrieve Issuance License and URI. Error is {0}", ex.Message);
				}
			}
			return result;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x0008AADC File Offset: 0x00088CDC
		public static string CompressSLCCertificateChain(string[] certs)
		{
			if (certs == null || certs.Length == 0)
			{
				throw new ArgumentNullException("certs");
			}
			string certificate = RMUtil.ConvertXrmlCertificateChainToString(new XrmlCertificateChain
			{
				CertificateChain = certs
			});
			return RMUtil.CompressAndBase64EncodeCertificate(certificate);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0008AB18 File Offset: 0x00088D18
		public static XrmlCertificateChain DecompressSLCCertificate(string compressedCerts)
		{
			if (string.IsNullOrEmpty(compressedCerts))
			{
				throw new ArgumentNullException("compressedCerts");
			}
			string appendedCerts = RMUtil.Base64DecodeAndDecompressCertificate(compressedCerts);
			XrmlCertificateChain xrmlCertificateChain = RMUtil.ConvertStringToXrmlCertificateChain(appendedCerts);
			return new XrmlCertificateChain(xrmlCertificateChain.CertificateChain);
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x0008AB54 File Offset: 0x00088D54
		public static string CompressTemplate(string templateXrml, RmsTemplateType type)
		{
			if (string.IsNullOrEmpty(templateXrml))
			{
				throw new ArgumentNullException("templateXrml");
			}
			return RMUtil.CompressAndBase64EncodeCertificate(string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				(int)type,
				":",
				templateXrml.Trim()
			}));
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0008ABAC File Offset: 0x00088DAC
		public static string DecompressTemplate(string encodedTemplate, out RmsTemplateType templateType)
		{
			if (string.IsNullOrEmpty(encodedTemplate))
			{
				throw new ArgumentNullException("encodedTemplate");
			}
			string text = RMUtil.Base64DecodeAndDecompressCertificate(encodedTemplate);
			int num = text.IndexOf(":<?xml", 0, StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				throw new FormatException("Failed to parse type information from the template. Could not find type index.");
			}
			string s = text.Substring(0, num);
			int num2;
			if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
			{
				throw new FormatException("Failed to parse type information from the template. Type Value not a valid RmsTemplateType.");
			}
			if (!Enum.IsDefined(typeof(RmsTemplateType), num2))
			{
				templateType = RmsTemplateType.Archived;
			}
			else
			{
				templateType = (RmsTemplateType)num2;
			}
			return text.Substring(num + ":".Length);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x0008AC48 File Offset: 0x00088E48
		public static string Base64DecodeAndDecompressCertificate(string encodedCert)
		{
			if (string.IsNullOrEmpty(encodedCert))
			{
				throw new ArgumentNullException("encodedCert");
			}
			byte[] compressedBytes = Convert.FromBase64String(encodedCert);
			return DrmEmailCompression.DecompressString(compressedBytes);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x0008AC75 File Offset: 0x00088E75
		private static string CompressAndBase64EncodeCertificate(string certificate)
		{
			if (string.IsNullOrEmpty(certificate))
			{
				throw new ArgumentNullException("certificate");
			}
			return Convert.ToBase64String(DrmEmailCompression.CompressString(certificate));
		}

		// Token: 0x04002E98 RID: 11928
		private const string RacTimeXPathQuery = "./BODY/VALIDITYTIME/UNTIL";

		// Token: 0x04002E99 RID: 11929
		private const string RacTimeFormat = "yyyy-MM-ddTHH:mm";

		// Token: 0x04002E9A RID: 11930
		private const string XmlDeclarationStart = "<?xml";

		// Token: 0x04002E9B RID: 11931
		private const string XmlDeclarationEnd = "?>";

		// Token: 0x04002E9C RID: 11932
		private const string TempBeginTag = "<SunShine>";

		// Token: 0x04002E9D RID: 11933
		private const string TempEndTag = "</SunShine>";

		// Token: 0x04002E9E RID: 11934
		private const int IssuanceLicenseMaxLength = 1047552;

		// Token: 0x04002E9F RID: 11935
		private const char Comma = ',';
	}
}
