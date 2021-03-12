using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006C RID: 108
	public class LegacyMessageEncoderBindingElement : MessageEncodingBindingElement
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00013CC5 File Offset: 0x00011EC5
		public LegacyMessageEncoderBindingElement()
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00013CD8 File Offset: 0x00011ED8
		protected LegacyMessageEncoderBindingElement(MessageVersion version)
		{
			this.version = version;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00013CF2 File Offset: 0x00011EF2
		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new LegacyMessageEncoderFactory(this.version);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00013CFF File Offset: 0x00011EFF
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00013D07 File Offset: 0x00011F07
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

		// Token: 0x060002F5 RID: 757 RVA: 0x00013D10 File Offset: 0x00011F10
		public override BindingElement Clone()
		{
			return new LegacyMessageEncoderBindingElement(this.version);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00013D1D File Offset: 0x00011F1D
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			return base.BuildChannelFactory<TChannel>(context);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00013D32 File Offset: 0x00011F32
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			return base.BuildChannelListener<TChannel>(context);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00013D47 File Offset: 0x00011F47
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return base.CanBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00013D50 File Offset: 0x00011F50
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			context.BindingParameters.Add(this);
			return base.CanBuildChannelListener<TChannel>(context);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00013D65 File Offset: 0x00011F65
		public override T GetProperty<T>(BindingContext context)
		{
			return base.GetProperty<T>(context);
		}

		// Token: 0x040002D7 RID: 727
		private MessageVersion version = MessageVersion.None;
	}
}
