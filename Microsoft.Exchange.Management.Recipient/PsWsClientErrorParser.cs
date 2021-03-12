using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200003A RID: 58
	internal class PsWsClientErrorParser
	{
		// Token: 0x060002BC RID: 700 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		static PsWsClientErrorParser()
		{
			PsWsClientErrorParser.namespaceManager.AddNamespace("meta", PsWsClientErrorParser.MetadataNamespace.NamespaceName);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D500 File Offset: 0x0000B700
		public static string Parse(string input)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(input))
			{
				int num = input.IndexOf(PsWsClientErrorParser.responseBody);
				if (num != -1)
				{
					string text = input.Substring(num + PsWsClientErrorParser.responseBody.Length);
					using (XmlReader xmlReader = XmlReader.Create(new StringReader(text.Trim())))
					{
						XDocument xdocument = XDocument.Load(xmlReader);
						XElement xelement = xdocument.Root.XPathSelectElement("/meta:error/meta:innererror/meta:message", PsWsClientErrorParser.namespaceManager);
						string text2 = xelement.Value.ToString();
						int num2 = text2.IndexOf(PsWsClientErrorParser.errorSeparator);
						if (num2 != -1)
						{
							result = text2.Substring(num2 + PsWsClientErrorParser.errorSeparator.Length);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0400006E RID: 110
		private static readonly XNamespace MetadataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

		// Token: 0x0400006F RID: 111
		private static readonly XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());

		// Token: 0x04000070 RID: 112
		private static readonly string responseBody = "Response body:";

		// Token: 0x04000071 RID: 113
		private static readonly string errorSeparator = ":";
	}
}
