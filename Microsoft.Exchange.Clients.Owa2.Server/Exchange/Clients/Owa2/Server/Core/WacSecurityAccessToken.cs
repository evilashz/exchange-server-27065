using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200005B RID: 91
	internal class WacSecurityAccessToken : ISecurityAccessToken
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000B539 File Offset: 0x00009739
		internal WacSecurityAccessToken(string sid)
		{
			this.userSid = sid;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B548 File Offset: 0x00009748
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000B550 File Offset: 0x00009750
		public string UserSid
		{
			get
			{
				return this.userSid;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B557 File Offset: 0x00009757
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B55A File Offset: 0x0000975A
		public SidStringAndAttributes[] GroupSids
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B561 File Offset: 0x00009761
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000B564 File Offset: 0x00009764
		public SidStringAndAttributes[] RestrictedGroupSids
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000155 RID: 341
		private readonly string userSid;
	}
}
