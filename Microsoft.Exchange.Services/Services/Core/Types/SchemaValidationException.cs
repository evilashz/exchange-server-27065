using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086D RID: 2157
	internal class SchemaValidationException : ServicePermanentException, IProvideXmlNodeArray
	{
		// Token: 0x06003DCF RID: 15823 RVA: 0x000D7EB0 File Offset: 0x000D60B0
		public SchemaValidationException(Exception innerException, int lineNumber, int linePosition, string violation) : base((CoreResources.IDs)2523006528U, innerException)
		{
			this.BuildDetailsFromException(lineNumber, linePosition, violation);
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x000D7ECD File Offset: 0x000D60CD
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000D7ED4 File Offset: 0x000D60D4
		private void BuildDetailsFromException(int lineNumber, int linePosition, string failureReason)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = ServiceXml.CreateElement(safeXmlDocument, SchemaValidationException.DummyElementName, "http://schemas.microsoft.com/exchange/services/2006/types");
			safeXmlDocument.AppendChild(xmlElement);
			this.details = new XmlNodeArray();
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.LineNumberElementName, lineNumber.ToString()));
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.LinePositionElementName, linePosition.ToString()));
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.ViolationElementName, failureReason));
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x000D7F6B File Offset: 0x000D616B
		public XmlNodeArray NodeArray
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x0400237F RID: 9087
		internal static readonly string LineNumberElementName = "LineNumber";

		// Token: 0x04002380 RID: 9088
		internal static readonly string LinePositionElementName = "LinePosition";

		// Token: 0x04002381 RID: 9089
		internal static readonly string ViolationElementName = "Violation";

		// Token: 0x04002382 RID: 9090
		internal static readonly string DummyElementName = "Dummy";

		// Token: 0x04002383 RID: 9091
		private XmlNodeArray details;
	}
}
