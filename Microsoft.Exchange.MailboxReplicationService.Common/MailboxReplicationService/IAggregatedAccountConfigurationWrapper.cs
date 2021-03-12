using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200012E RID: 302
	internal interface IAggregatedAccountConfigurationWrapper
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A54 RID: 2644
		// (set) Token: 0x06000A55 RID: 2645
		ADUser TargetUser { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A56 RID: 2646
		// (set) Token: 0x06000A57 RID: 2647
		Guid TargetExchangeGuid { get; set; }

		// Token: 0x06000A58 RID: 2648
		IExchangePrincipal GetExchangePrincipal();

		// Token: 0x06000A59 RID: 2649
		void SetExchangePrincipal();

		// Token: 0x06000A5A RID: 2650
		void UpdateData(RequestJobBase requestJob);

		// Token: 0x06000A5B RID: 2651
		void Save(MailboxStoreTypeProvider provider);

		// Token: 0x06000A5C RID: 2652
		void Delete(MailboxStoreTypeProvider provider);
	}
}
