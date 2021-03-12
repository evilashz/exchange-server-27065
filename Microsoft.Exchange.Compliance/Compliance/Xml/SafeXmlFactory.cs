using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.Exchange.Compliance.Xml
{
	// Token: 0x02000007 RID: 7
	internal class SafeXmlFactory
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00003BAC File Offset: 0x00001DAC
		public static XmlTextReader CreateSafeXmlTextReader(Stream stream)
		{
			return new XmlTextReader(stream)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public static XmlTextReader CreateSafeXmlTextReader(string url)
		{
			return new XmlTextReader(url)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public static XmlTextReader CreateSafeXmlTextReader(TextReader input)
		{
			return new XmlTextReader(input)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003C18 File Offset: 0x00001E18
		public static XmlTextReader CreateSafeXmlTextReader(Stream input, XmlNameTable nt)
		{
			return new XmlTextReader(input, nt)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003C3C File Offset: 0x00001E3C
		public static XmlTextReader CreateSafeXmlTextReader(string url, Stream input)
		{
			return new XmlTextReader(url, input)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003C60 File Offset: 0x00001E60
		public static XmlTextReader CreateSafeXmlTextReader(string url, TextReader input)
		{
			return new XmlTextReader(url, input)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003C84 File Offset: 0x00001E84
		public static XmlTextReader CreateSafeXmlTextReader(string url, XmlNameTable nt)
		{
			return new XmlTextReader(url, nt)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public static XmlTextReader CreateSafeXmlTextReader(TextReader input, XmlNameTable nt)
		{
			return new XmlTextReader(input, nt)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003CCC File Offset: 0x00001ECC
		public static XmlTextReader CreateSafeXmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			return new XmlTextReader(xmlFragment, fragType, context)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static XmlTextReader CreateSafeXmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			return new XmlTextReader(url, input, nt)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static XmlTextReader CreateSafeXmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			return new XmlTextReader(url, input, nt)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003D44 File Offset: 0x00001F44
		public static XmlTextReader CreateSafeXmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			return new XmlTextReader(xmlFragment, fragType, context)
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003D69 File Offset: 0x00001F69
		public static XmlReader CreateSafeXmlReader(Stream stream)
		{
			return XmlReader.Create(stream, SafeXmlFactory.defaultSettings);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003D76 File Offset: 0x00001F76
		public static XmlReader CreateSafeXmlReader(Stream stream, XmlReaderSettings settings)
		{
			settings.DtdProcessing = DtdProcessing.Prohibit;
			settings.XmlResolver = null;
			return XmlReader.Create(stream, settings);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003D90 File Offset: 0x00001F90
		public static XPathDocument CreateXPathDocument(Stream stream)
		{
			XPathDocument result;
			using (XmlReader xmlReader = XmlReader.Create(stream, SafeXmlFactory.defaultSettings))
			{
				result = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public static XPathDocument CreateXPathDocument(string uri)
		{
			XPathDocument result;
			using (XmlReader xmlReader = XmlReader.Create(uri, SafeXmlFactory.defaultSettings))
			{
				result = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003E10 File Offset: 0x00002010
		public static XPathDocument CreateXPathDocument(TextReader textReader)
		{
			XPathDocument result;
			using (XmlReader xmlReader = XmlReader.Create(textReader, SafeXmlFactory.defaultSettings))
			{
				result = SafeXmlFactory.CreateXPathDocument(xmlReader);
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003E50 File Offset: 0x00002050
		public static XPathDocument CreateXPathDocument(XmlReader reader)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return new XPathDocument(reader);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003E74 File Offset: 0x00002074
		public static XPathDocument CreateXPathDocument(string uri, XmlSpace space)
		{
			XPathDocument result;
			using (XmlReader xmlReader = XmlReader.Create(uri, SafeXmlFactory.defaultSettings))
			{
				result = SafeXmlFactory.CreateXPathDocument(xmlReader, space);
			}
			return result;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003EB4 File Offset: 0x000020B4
		public static XPathDocument CreateXPathDocument(XmlReader reader, XmlSpace space)
		{
			if (reader.Settings != null && reader.Settings.DtdProcessing != DtdProcessing.Prohibit)
			{
				throw new XmlDtdException();
			}
			return new XPathDocument(reader, space);
		}

		// Token: 0x0400000C RID: 12
		private static XmlReaderSettings defaultSettings = new XmlReaderSettings
		{
			DtdProcessing = DtdProcessing.Prohibit,
			XmlResolver = null
		};
	}
}
