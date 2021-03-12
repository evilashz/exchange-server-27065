using System;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C97 RID: 3223
	public class SecurityBindingElement : BindingElement, ITransportTokenAssertionProvider
	{
		// Token: 0x0600576F RID: 22383 RVA: 0x00113791 File Offset: 0x00111991
		public override BindingElement Clone()
		{
			return new SecurityBindingElement();
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x00113798 File Offset: 0x00111998
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(ISecurityCapabilities))
			{
				return (T)((object)new SecurityBindingElement.SecurityCapabilities());
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x001137C6 File Offset: 0x001119C6
		public XmlElement GetTransportTokenAssertion()
		{
			return null;
		}

		// Token: 0x02000C98 RID: 3224
		internal class SecurityCapabilities : ISecurityCapabilities
		{
			// Token: 0x17001445 RID: 5189
			// (get) Token: 0x06005773 RID: 22387 RVA: 0x001137D1 File Offset: 0x001119D1
			public ProtectionLevel SupportedRequestProtectionLevel
			{
				get
				{
					return ProtectionLevel.EncryptAndSign;
				}
			}

			// Token: 0x17001446 RID: 5190
			// (get) Token: 0x06005774 RID: 22388 RVA: 0x001137D4 File Offset: 0x001119D4
			public ProtectionLevel SupportedResponseProtectionLevel
			{
				get
				{
					return ProtectionLevel.EncryptAndSign;
				}
			}

			// Token: 0x17001447 RID: 5191
			// (get) Token: 0x06005775 RID: 22389 RVA: 0x001137D7 File Offset: 0x001119D7
			public bool SupportsClientAuthentication
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001448 RID: 5192
			// (get) Token: 0x06005776 RID: 22390 RVA: 0x001137DA File Offset: 0x001119DA
			public bool SupportsClientWindowsIdentity
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001449 RID: 5193
			// (get) Token: 0x06005777 RID: 22391 RVA: 0x001137DD File Offset: 0x001119DD
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
