using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C90 RID: 3216
	internal class MessageEncoderWithXmlDeclarationFactory : MessageEncoderFactory
	{
		// Token: 0x06005738 RID: 22328 RVA: 0x00112EE1 File Offset: 0x001110E1
		public MessageEncoderWithXmlDeclarationFactory(MessageEncoderWithXmlDeclarationBindingElement bindingElement)
		{
			this.version = bindingElement.MessageVersion;
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x00112EF5 File Offset: 0x001110F5
		public override MessageEncoder Encoder
		{
			get
			{
				if (this.encoder == null)
				{
					this.encoder = new MessageEncoderWithXmlDeclaration(this);
				}
				return this.encoder;
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x00112F11 File Offset: 0x00111111
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04003010 RID: 12304
		protected MessageEncoderWithXmlDeclaration encoder;

		// Token: 0x04003011 RID: 12305
		private MessageVersion version;
	}
}
