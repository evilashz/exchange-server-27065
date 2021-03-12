using System;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200005A RID: 90
	public class AutodiscoverSecurityBindingElement : BindingElement, ITransportTokenAssertionProvider
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00012226 File Offset: 0x00010426
		public override BindingElement Clone()
		{
			return new AutodiscoverSecurityBindingElement();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001222D File Offset: 0x0001042D
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)new AutodiscoverSecurityBindingElement.SecurityCapabilities());
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0001225B File Offset: 0x0001045B
		public XmlElement GetTransportTokenAssertion()
		{
			return null;
		}

		// Token: 0x0200005B RID: 91
		internal class SecurityCapabilities : ISecurityCapabilities
		{
			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x00012266 File Offset: 0x00010466
			public ProtectionLevel SupportedRequestProtectionLevel
			{
				get
				{
					return ProtectionLevel.EncryptAndSign;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060002A1 RID: 673 RVA: 0x00012269 File Offset: 0x00010469
			public ProtectionLevel SupportedResponseProtectionLevel
			{
				get
				{
					return ProtectionLevel.EncryptAndSign;
				}
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001226C File Offset: 0x0001046C
			public bool SupportsClientAuthentication
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x0001226F File Offset: 0x0001046F
			public bool SupportsClientWindowsIdentity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x00012272 File Offset: 0x00010472
			public bool SupportsServerAuthentication
			{
				get
				{
					return true;
				}
			}
		}
	}
}
