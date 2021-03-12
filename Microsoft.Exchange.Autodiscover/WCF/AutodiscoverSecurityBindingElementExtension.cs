using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200005C RID: 92
	public class AutodiscoverSecurityBindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001227D File Offset: 0x0001047D
		public override Type BindingElementType
		{
			get
			{
				return typeof(AutodiscoverSecurityBindingElement);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00012289 File Offset: 0x00010489
		protected override BindingElement CreateBindingElement()
		{
			return new AutodiscoverSecurityBindingElement();
		}
	}
}
