using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Services.Core.PolicyNudges
{
	// Token: 0x020003D2 RID: 978
	internal class VersionedItem : IVersionedItem
	{
		// Token: 0x06001B66 RID: 7014 RVA: 0x0009C226 File Offset: 0x0009A426
		internal VersionedItem(string id, DateTime version)
		{
			this.ID = id;
			this.Version = version;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0009C23C File Offset: 0x0009A43C
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0009C244 File Offset: 0x0009A444
		public string ID { get; private set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0009C24D File Offset: 0x0009A44D
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0009C255 File Offset: 0x0009A455
		public DateTime Version { get; private set; }
	}
}
