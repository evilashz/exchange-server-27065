using System;
using System.Collections.Generic;
using System.ServiceModel;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CD RID: 1229
	public class SetUMExtensionParameteres : SetObjectProperties
	{
		// Token: 0x170023CC RID: 9164
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000B5229 File Offset: 0x000B3429
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000B5231 File Offset: 0x000B3431
		public IEnumerable<UMExtension> SecondaryExtensions { get; set; }

		// Token: 0x170023CD RID: 9165
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000B523A File Offset: 0x000B343A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-Mailbox";
			}
		}

		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x000B5241 File Offset: 0x000B3441
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000B5248 File Offset: 0x000B3448
		public void UpdateSecondaryExtensions(UMMailbox mailbox)
		{
			ProxyAddressCollection emailAddresses = mailbox.EmailAddresses;
			EumProxyAddress other = null;
			for (int i = emailAddresses.Count - 1; i >= 0; i--)
			{
				if (emailAddresses[i] is EumProxyAddress)
				{
					if (((EumProxyAddress)emailAddresses[i]).IsPrimaryAddress)
					{
						other = (EumProxyAddress)emailAddresses[i];
					}
					else
					{
						emailAddresses.RemoveAt(i);
					}
				}
			}
			if (this.SecondaryExtensions != null)
			{
				foreach (UMExtension umextension in this.SecondaryExtensions)
				{
					if (string.IsNullOrEmpty(umextension.PhoneContext) || string.IsNullOrEmpty(umextension.Extension))
					{
						throw new FaultException(Strings.InvalidSecondaryExtensionError);
					}
					ProxyAddress proxyAddress = UMMailbox.BuildProxyAddressFromExtensionAndPhoneContext(umextension.Extension, ProxyAddressPrefix.UM.SecondaryPrefix, umextension.PhoneContext);
					if (emailAddresses.Contains(proxyAddress))
					{
						if (proxyAddress.Equals(other))
						{
							throw new ProxyAddressExistsException(new LocalizedString(string.Format(Strings.DuplicateExtensionError, proxyAddress)));
						}
						throw new FaultException(string.Format(Strings.DuplicateSecondaryExtensionError, proxyAddress));
					}
					else
					{
						emailAddresses.Add(proxyAddress);
					}
				}
			}
			base[MailEnabledRecipientSchema.EmailAddresses] = emailAddresses;
		}
	}
}
