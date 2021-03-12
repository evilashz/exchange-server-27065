using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C99 RID: 3225
	public class SecurityBindingElementExtension : BindingElementExtensionElement
	{
		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06005779 RID: 22393 RVA: 0x001137E8 File Offset: 0x001119E8
		public override Type BindingElementType
		{
			get
			{
				return typeof(SecurityBindingElement);
			}
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x001137F4 File Offset: 0x001119F4
		protected override BindingElement CreateBindingElement()
		{
			return new SecurityBindingElement();
		}
	}
}
