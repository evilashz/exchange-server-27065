using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C92 RID: 3218
	public class MessageEncoderWithXmlDeclarationSoap11WSAddressing10BindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x0600573E RID: 22334 RVA: 0x00112F39 File Offset: 0x00111139
		public override Type BindingElementType
		{
			get
			{
				return typeof(MessageEncoderWithXmlDeclarationBindingElement);
			}
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x00112F45 File Offset: 0x00111145
		protected override BindingElement CreateBindingElement()
		{
			return new MessageEncoderWithXmlDeclarationBindingElement(MessageVersion.Soap11WSAddressing10);
		}
	}
}
