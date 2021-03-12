using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000867 RID: 2151
	internal class ResponseSchemaValidationException : ServicePermanentException, IProvideXmlNodeArray
	{
		// Token: 0x06003DB4 RID: 15796 RVA: 0x000D7BD6 File Offset: 0x000D5DD6
		public ResponseSchemaValidationException(Exception innerException, int lineNumber, int linePosition, string violation, string badResponse) : base(CoreResources.IDs.ErrorResponseSchemaValidation, innerException)
		{
			this.BuildDetailsFromException(lineNumber, linePosition, violation, badResponse);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000D7BF8 File Offset: 0x000D5DF8
		private void BuildDetailsFromException(int lineNumber, int linePosition, string failureReason, string badResponse)
		{
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = ServiceXml.CreateElement(safeXmlDocument, SchemaValidationException.DummyElementName, "http://schemas.microsoft.com/exchange/services/2006/types");
			safeXmlDocument.AppendChild(xmlElement);
			this.details = new XmlNodeArray();
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.LineNumberElementName, lineNumber.ToString()));
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.LinePositionElementName, linePosition.ToString()));
			this.details.Nodes.Add(ServiceXml.CreateTextElement(xmlElement, SchemaValidationException.ViolationElementName, failureReason));
			XmlElement xmlElement2 = ServiceXml.CreateElement(xmlElement, ResponseSchemaValidationException.BadResponseElementName);
			xmlElement2.AppendChild(safeXmlDocument.CreateCDataSection(badResponse));
			this.details.Nodes.Add(xmlElement2);
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x000D7CBB File Offset: 0x000D5EBB
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000D7CC2 File Offset: 0x000D5EC2
		public XmlNodeArray NodeArray
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x0400237A RID: 9082
		private static readonly string BadResponseElementName = "BadResponse";

		// Token: 0x0400237B RID: 9083
		private XmlNodeArray details;
	}
}
