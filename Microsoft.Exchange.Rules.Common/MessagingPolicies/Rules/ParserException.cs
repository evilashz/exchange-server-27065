using System;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class ParserException : RuleParsingException
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000338E File Offset: 0x0000158E
		public ParserException(string message) : base(message, 0, 0)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003399 File Offset: 0x00001599
		public ParserException(LocalizedString message) : base(message, 0, 0)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000033A9 File Offset: 0x000015A9
		public ParserException(string message, XmlReader reader) : base(message, ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000033C8 File Offset: 0x000015C8
		public ParserException(LocalizedString message, XmlReader reader) : base(message, ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033EC File Offset: 0x000015EC
		public ParserException(Exception e, XmlReader reader) : base(e.Message, ((IXmlLineInfo)reader).LineNumber, ((IXmlLineInfo)reader).LinePosition, e)
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003411 File Offset: 0x00001611
		public ParserException(XmlException e) : base(e.Message, e.LineNumber, e.LinePosition, e)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000342C File Offset: 0x0000162C
		protected ParserException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
