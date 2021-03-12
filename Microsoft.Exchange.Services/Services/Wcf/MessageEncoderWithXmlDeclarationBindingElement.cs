using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C8F RID: 3215
	public class MessageEncoderWithXmlDeclarationBindingElement : MessageEncodingBindingElement
	{
		// Token: 0x0600572E RID: 22318 RVA: 0x00112E14 File Offset: 0x00111014
		public MessageEncoderWithXmlDeclarationBindingElement(MessageVersion version)
		{
			this.version = version;
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x00112E23 File Offset: 0x00111023
		public MessageEncoderWithXmlDeclarationBindingElement(MessageEncoderWithXmlDeclarationBindingElement other)
		{
			this.version = other.version;
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x00112E37 File Offset: 0x00111037
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new MessageEncoderWithXmlDeclarationFactory(this);
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06005731 RID: 22321 RVA: 0x00112E3F File Offset: 0x0011103F
		// (set) Token: 0x06005732 RID: 22322 RVA: 0x00112E47 File Offset: 0x00111047
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x00112E50 File Offset: 0x00111050
		public override BindingElement Clone()
		{
			return new MessageEncoderWithXmlDeclarationBindingElement(this);
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x00112E58 File Offset: 0x00111058
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00112E6C File Offset: 0x0011106C
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x00112E74 File Offset: 0x00111074
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			if (Global.UseBufferRequestChannelListener.Member && typeof(TChannel) == typeof(IReplyChannel))
			{
				BufferRequestChannelListener bufferRequestChannelListener = new BufferRequestChannelListener(context.BuildInnerChannelListener<IReplyChannel>());
				return (IChannelListener<TChannel>)bufferRequestChannelListener;
			}
			return context.BuildInnerChannelListener<TChannel>();
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x00112ECD File Offset: 0x001110CD
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}

		// Token: 0x0400300F RID: 12303
		private MessageVersion version;
	}
}
