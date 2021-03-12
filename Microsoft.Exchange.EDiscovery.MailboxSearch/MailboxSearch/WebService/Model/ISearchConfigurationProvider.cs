using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000022 RID: 34
	internal interface ISearchConfigurationProvider
	{
		// Token: 0x060001C5 RID: 453
		void ApplyConfiguration(ISearchPolicy policy, ref SearchMailboxesInputs inputs);
	}
}
