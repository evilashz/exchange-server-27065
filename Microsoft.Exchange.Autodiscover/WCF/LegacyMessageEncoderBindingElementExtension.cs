using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200006D RID: 109
	public class LegacyMessageEncoderBindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00013D76 File Offset: 0x00011F76
		public override Type BindingElementType
		{
			get
			{
				return typeof(LegacyMessageEncoderBindingElement);
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00013D82 File Offset: 0x00011F82
		protected override BindingElement CreateBindingElement()
		{
			return new LegacyMessageEncoderBindingElement();
		}
	}
}
