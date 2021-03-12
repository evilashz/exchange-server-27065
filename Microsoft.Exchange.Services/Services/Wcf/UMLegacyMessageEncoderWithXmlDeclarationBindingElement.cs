using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA6 RID: 3494
	public class UMLegacyMessageEncoderWithXmlDeclarationBindingElement : MessageEncoderWithXmlDeclarationBindingElement
	{
		// Token: 0x060058B5 RID: 22709 RVA: 0x00114465 File Offset: 0x00112665
		public UMLegacyMessageEncoderWithXmlDeclarationBindingElement(MessageVersion version) : base(version)
		{
		}

		// Token: 0x060058B6 RID: 22710 RVA: 0x0011446E File Offset: 0x0011266E
		public UMLegacyMessageEncoderWithXmlDeclarationBindingElement(MessageEncoderWithXmlDeclarationBindingElement other) : base(other)
		{
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x00114477 File Offset: 0x00112677
		public override BindingElement Clone()
		{
			return new UMLegacyMessageEncoderWithXmlDeclarationBindingElement(this);
		}

		// Token: 0x060058B8 RID: 22712 RVA: 0x0011447F File Offset: 0x0011267F
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new UMLegacyMessageEncoderWithXmlDeclarationFactory(this);
		}
	}
}
