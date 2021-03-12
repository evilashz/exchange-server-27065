using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA8 RID: 3496
	public class UMLegacyMessageEncoderWithXmlDeclarationSoap11BindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060058BB RID: 22715 RVA: 0x001144AC File Offset: 0x001126AC
		public override Type BindingElementType
		{
			get
			{
				return typeof(UMLegacyMessageEncoderWithXmlDeclarationBindingElement);
			}
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x001144B8 File Offset: 0x001126B8
		protected override BindingElement CreateBindingElement()
		{
			return new UMLegacyMessageEncoderWithXmlDeclarationBindingElement(MessageVersion.Soap11);
		}
	}
}
