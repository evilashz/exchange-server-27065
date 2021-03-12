using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000266 RID: 614
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupPrincipal : ExchangePrincipal
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x0007905F File Offset: 0x0007725F
		public GroupPrincipal(IGenericADUser adGroup, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions) : base(adGroup, allMailboxes, mailboxSelector, remotingOptions)
		{
			ArgumentValidator.ThrowIfTypeInvalid<ADGroupGenericWrapper>("adGroup", adGroup);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00079077 File Offset: 0x00077277
		private GroupPrincipal(GroupPrincipal sourceGroupPrincipal) : base(sourceGroupPrincipal)
		{
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00079080 File Offset: 0x00077280
		public override string PrincipalId
		{
			get
			{
				return base.Alias;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00079088 File Offset: 0x00077288
		protected override ExchangePrincipal Clone()
		{
			return new GroupPrincipal(this);
		}
	}
}
