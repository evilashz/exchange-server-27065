using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000020 RID: 32
	internal class FirstOrgCacheScanner : CacheScanner
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00006406 File Offset: 0x00004606
		internal FirstOrgCacheScanner(AnchorContext context, WaitHandle stopEvent) : base(context, stopEvent)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006410 File Offset: 0x00004610
		internal override string Name
		{
			get
			{
				return "FirstOrgCacheScanner";
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006417 File Offset: 0x00004617
		protected override IEnumerable<ADUser> GetLocalMailboxUsers()
		{
			return AnchorADProvider.GetRootOrgProvider(base.Context).GetOrganizationMailboxesByCapability(base.Context.ActiveCapability);
		}
	}
}
