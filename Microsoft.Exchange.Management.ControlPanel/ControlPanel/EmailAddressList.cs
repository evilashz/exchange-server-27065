using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F9 RID: 1273
	[CollectionDataContract]
	public class EmailAddressList : List<EmailAddressItem>
	{
		// Token: 0x06003D84 RID: 15748 RVA: 0x000B8A54 File Offset: 0x000B6C54
		public static EmailAddressList FromProxyAddressCollection(ProxyAddressCollection emailAddresses)
		{
			return new EmailAddressList(emailAddresses);
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x000B8A5C File Offset: 0x000B6C5C
		public EmailAddressList()
		{
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000B8A64 File Offset: 0x000B6C64
		public EmailAddressList(ICollection<ProxyAddress> emailAddresses) : base((emailAddresses == null) ? 0 : emailAddresses.Count)
		{
			if (emailAddresses != null)
			{
				foreach (ProxyAddress address in emailAddresses)
				{
					base.Add(new EmailAddressItem(address));
				}
			}
		}
	}
}
