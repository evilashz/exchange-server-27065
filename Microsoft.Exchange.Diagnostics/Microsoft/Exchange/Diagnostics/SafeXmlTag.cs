using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000C1 RID: 193
	internal class SafeXmlTag : IDisposable
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00014C49 File Offset: 0x00012E49
		public SafeXmlTag(XmlWriter writer, string tagName)
		{
			this.writer = writer;
			this.writer.WriteStartElement(tagName);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00014C64 File Offset: 0x00012E64
		public SafeXmlTag WithAttribute(string name, string value)
		{
			value = SafeXmlTag.SanitiseString(value);
			this.writer.WriteAttributeString(name, value);
			return this;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00014C7C File Offset: 0x00012E7C
		public SafeXmlTag WithAttribute(string nspace, string name, string value)
		{
			value = SafeXmlTag.SanitiseString(value);
			if (nspace == "xml")
			{
				this.writer.WriteAttributeString(nspace, name, "http://www.w3.org/XML/1998/namespace", value);
			}
			else
			{
				this.writer.WriteAttributeString(name, nspace, value);
			}
			return this;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00014CB7 File Offset: 0x00012EB7
		public void SetContent(string content)
		{
			content = SafeXmlTag.SanitiseString(content);
			this.writer.WriteString(content);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00014CCD File Offset: 0x00012ECD
		public void Dispose()
		{
			this.writer.WriteEndElement();
			this.writer = null;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00014CE1 File Offset: 0x00012EE1
		private static string SanitiseString(string content)
		{
			if (!string.IsNullOrEmpty(content))
			{
				return SafeXmlTag.SanitiseContentRegex.Replace(content, "?");
			}
			return string.Empty;
		}

		// Token: 0x040003CA RID: 970
		private static readonly Regex SanitiseContentRegex = new Regex("(?<![\\uD800-\\uDBFF])[\\uDC00-\\uDFFF]|[\\uD800-\\uDBFF](?![\\uDC00-\\uDFFF])|[\\x00-\\x08\\x0B\\x0C\\x0E-\\x1F\\x7F-\\x9F\\uFEFF\\uFFFE\\uFFFF]", RegexOptions.Compiled);

		// Token: 0x040003CB RID: 971
		private XmlWriter writer;
	}
}
