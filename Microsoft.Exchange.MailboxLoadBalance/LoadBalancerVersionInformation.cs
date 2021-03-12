using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LoadBalancerVersionInformation
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002848 File Offset: 0x00000A48
		static LoadBalancerVersionInformation()
		{
			LoadBalancerVersionInformation.LoadBalancerVersion[0] = true;
			LoadBalancerVersionInformation.LoadBalancerVersion[1] = true;
			LoadBalancerVersionInformation.LoadBalancerVersion[2] = true;
			LoadBalancerVersionInformation.LoadBalancerVersion[3] = true;
			LoadBalancerVersionInformation.LoadBalancerVersion[4] = true;
			LoadBalancerVersionInformation.LoadBalancerVersion[5] = true;
			LoadBalancerVersionInformation.InjectorVersion = new VersionInformation(4);
			LoadBalancerVersionInformation.InjectorVersion[0] = true;
			LoadBalancerVersionInformation.InjectorVersion[1] = true;
			LoadBalancerVersionInformation.InjectorVersion[2] = true;
			LoadBalancerVersionInformation.InjectorVersion[3] = true;
		}

		// Token: 0x0400000F RID: 15
		public static readonly VersionInformation LoadBalancerVersion = new VersionInformation(6);

		// Token: 0x04000010 RID: 16
		public static readonly VersionInformation InjectorVersion;
	}
}
