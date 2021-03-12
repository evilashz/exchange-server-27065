using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001CC RID: 460
	[CollectionDataContract]
	public class AddressTemplateList : List<EmailAddressItem>
	{
		// Token: 0x0600251B RID: 9499 RVA: 0x00071E64 File Offset: 0x00070064
		public AddressTemplateList()
		{
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00071E6C File Offset: 0x0007006C
		public AddressTemplateList(ProxyAddressTemplateCollection templateCollection) : base(templateCollection.Count)
		{
			foreach (ProxyAddressBase address in templateCollection)
			{
				base.Add(new EmailAddressItem(address));
			}
		}
	}
}
