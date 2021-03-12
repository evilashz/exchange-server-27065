using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C91 RID: 3217
	public class MessageEncoderWithXmlDeclarationSoap11BindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x0600573B RID: 22331 RVA: 0x00112F19 File Offset: 0x00111119
		public override Type BindingElementType
		{
			get
			{
				return typeof(MessageEncoderWithXmlDeclarationBindingElement);
			}
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x00112F25 File Offset: 0x00111125
		protected override BindingElement CreateBindingElement()
		{
			return new MessageEncoderWithXmlDeclarationBindingElement(MessageVersion.Soap11);
		}
	}
}
