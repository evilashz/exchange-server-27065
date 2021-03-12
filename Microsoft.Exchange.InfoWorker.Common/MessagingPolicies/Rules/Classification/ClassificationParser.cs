using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Classification
{
	// Token: 0x02000160 RID: 352
	internal sealed class ClassificationParser
	{
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00028C33 File Offset: 0x00026E33
		public static ClassificationParser Instance
		{
			get
			{
				return ClassificationParser.instance;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00028C3C File Offset: 0x00026E3C
		public ClassificationRulePackage GetRulePackage(byte[] ruleStream)
		{
			byte[] bytes;
			try
			{
				this.UncompressStream(ruleStream, "config", out bytes);
			}
			catch (InvalidOperationException ex)
			{
				throw new ParserException(ex.Message);
			}
			catch (IOException ex2)
			{
				throw new ParserException(ex2.Message);
			}
			string @string;
			try
			{
				@string = Encoding.Unicode.GetString(bytes);
			}
			catch (ArgumentException ex3)
			{
				throw new ParserException(ex3.Message);
			}
			return this.GetRulePackage(@string);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00028CC0 File Offset: 0x00026EC0
		public ClassificationRulePackage GetRulePackage(string ruleString)
		{
			ClassificationRulePackage result = null;
			XmlReader reader = null;
			try
			{
				XmlReaderSettings settings = new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Auto,
					IgnoreComments = true,
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				};
				using (StringReader stringReader = new StringReader(ruleString))
				{
					using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stringReader))
					{
						XmlReader xmlReader;
						reader = (xmlReader = XmlReader.Create(xmlTextReader, settings));
						try
						{
							result = this.ParseRule(reader);
						}
						finally
						{
							if (xmlReader != null)
							{
								((IDisposable)xmlReader).Dispose();
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				throw new ParserException(e);
			}
			catch (RulesValidationException e2)
			{
				throw new ParserException(e2, reader);
			}
			return result;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00028DA0 File Offset: 0x00026FA0
		private static bool IsVersionedDataClassification(XElement dataClassificationElement)
		{
			return dataClassificationElement != null && dataClassificationElement.Parent != null && dataClassificationElement.Parent.Name != null && dataClassificationElement.Parent.Name.LocalName == "Version";
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00028F44 File Offset: 0x00027144
		private ClassificationRulePackage ParseRule(XmlReader reader)
		{
			XDocument xdocument = XDocument.Load(reader);
			ClassificationRulePackage classificationRulePackage = new ClassificationRulePackage();
			classificationRulePackage.RuleXml = xdocument.Root.ToString(SaveOptions.DisableFormatting);
			classificationRulePackage.VersionedDataClassificationIds = new HashSet<string>(from dataClassificationElement in xdocument.Descendants().AsParallel<XElement>()
			where ClassificationConstants.DataClassificationElementNames.Contains(dataClassificationElement.Name.LocalName) && ClassificationParser.IsVersionedDataClassification(dataClassificationElement)
			let dataClassificationId = dataClassificationElement.Attribute("id").Value.Trim()
			select dataClassificationId, StringComparer.OrdinalIgnoreCase);
			return classificationRulePackage;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00028FF4 File Offset: 0x000271F4
		private void UncompressStream(byte[] compressedStream, string packagePart, out byte[] uncompressedStream)
		{
			if (compressedStream == null)
			{
				throw new ArgumentNullException("compressedStream");
			}
			using (Stream stream = new MemoryStream(compressedStream))
			{
				using (Package package = Package.Open(stream))
				{
					Uri partUri = PackUriHelper.CreatePartUri(new Uri(packagePart, UriKind.Relative));
					PackagePart part = package.GetPart(partUri);
					Stream stream2 = part.GetStream(FileMode.Open, FileAccess.Read);
					using (MemoryStream memoryStream = new MemoryStream(compressedStream.Length))
					{
						stream2.CopyTo(memoryStream);
						uncompressedStream = memoryStream.ToArray();
					}
				}
			}
		}

		// Token: 0x0400075B RID: 1883
		private static readonly ClassificationParser instance = new ClassificationParser();
	}
}
