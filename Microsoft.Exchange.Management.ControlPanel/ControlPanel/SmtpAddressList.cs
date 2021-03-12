using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004FA RID: 1274
	[CollectionDataContract]
	public class SmtpAddressList : EmailAddressList
	{
		// Token: 0x06003D87 RID: 15751 RVA: 0x000B8AC8 File Offset: 0x000B6CC8
		public SmtpAddressList()
		{
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000B8ADB File Offset: 0x000B6CDB
		public SmtpAddressList(ICollection<ProxyAddress> emailAddresses)
		{
			ICollection<ProxyAddress> emailAddresses2;
			if (emailAddresses != null)
			{
				emailAddresses2 = (from emailAddress in emailAddresses
				where emailAddress is SmtpProxyAddress
				select emailAddress).ToList<ProxyAddress>();
			}
			else
			{
				emailAddresses2 = null;
			}
			base..ctor(emailAddresses2);
		}
	}
}
