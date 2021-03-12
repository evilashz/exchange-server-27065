using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000227 RID: 551
	internal class MembershipChecker : IStringComparer
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x0004A4C2 File Offset: 0x000486C2
		public MembershipChecker(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004A4D1 File Offset: 0x000486D1
		public bool Equals(string senderEmailAddress, string distributionListRoutingAddress)
		{
			return !string.IsNullOrEmpty(senderEmailAddress) && !string.IsNullOrEmpty(distributionListRoutingAddress) && ADUtils.IsMemberOf(senderEmailAddress, (RoutingAddress)distributionListRoutingAddress, this.organizationId);
		}

		// Token: 0x04000B54 RID: 2900
		private readonly OrganizationId organizationId;
	}
}
