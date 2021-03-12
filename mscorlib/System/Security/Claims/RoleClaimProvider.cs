using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x020002F4 RID: 756
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x00090151 File Offset: 0x0008E351
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x00090170 File Offset: 0x0008E370
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x04000F4C RID: 3916
		private string m_issuer;

		// Token: 0x04000F4D RID: 3917
		private string[] m_roles;

		// Token: 0x04000F4E RID: 3918
		private ClaimsIdentity m_subject;
	}
}
