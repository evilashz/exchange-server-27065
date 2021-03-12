using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000720 RID: 1824
	internal interface IExternalAndEmailAddresses
	{
		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x06005672 RID: 22130
		// (set) Token: 0x06005673 RID: 22131
		ProxyAddress ExternalEmailAddress { get; set; }

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x06005674 RID: 22132
		// (set) Token: 0x06005675 RID: 22133
		ProxyAddressCollection EmailAddresses { get; set; }
	}
}
