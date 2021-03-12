using System;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006B RID: 107
	public class LegacyMessageEncoderFactory : MessageEncoderFactory
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00013C95 File Offset: 0x00011E95
		public LegacyMessageEncoderFactory(MessageVersion version)
		{
			this.version = version;
			this.encoder = new LegacyMessageEncoder(this.version);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00013CB5 File Offset: 0x00011EB5
		public override MessageEncoder Encoder
		{
			get
			{
				return this.encoder;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00013CBD File Offset: 0x00011EBD
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x040002D5 RID: 725
		private MessageVersion version;

		// Token: 0x040002D6 RID: 726
		private MessageEncoder encoder;
	}
}
