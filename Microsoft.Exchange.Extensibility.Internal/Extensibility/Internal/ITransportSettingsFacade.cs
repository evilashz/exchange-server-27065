using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000053 RID: 83
	internal interface ITransportSettingsFacade
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002F9 RID: 761
		bool ClearCategories { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002FA RID: 762
		bool Rfc2231EncodingEnabled { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002FB RID: 763
		bool OpenDomainRoutingEnabled { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002FC RID: 764
		bool AddressBookPolicyRoutingEnabled { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002FD RID: 765
		IList<string> SupervisionTags { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002FE RID: 766
		string OrganizationFederatedMailbox { get; }
	}
}
