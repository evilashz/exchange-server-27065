using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA7 RID: 3495
	internal class UMLegacyMessageEncoderWithXmlDeclarationFactory : MessageEncoderWithXmlDeclarationFactory
	{
		// Token: 0x060058B9 RID: 22713 RVA: 0x00114487 File Offset: 0x00112687
		public UMLegacyMessageEncoderWithXmlDeclarationFactory(MessageEncoderWithXmlDeclarationBindingElement bindingElement) : base(bindingElement)
		{
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x00114490 File Offset: 0x00112690
		public override MessageEncoder Encoder
		{
			get
			{
				if (this.encoder == null)
				{
					this.encoder = new UMLegacyMessageEncoderWithXmlDeclaration(this);
				}
				return this.encoder;
			}
		}
	}
}
