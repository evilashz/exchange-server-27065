using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000056 RID: 86
	internal class PolicyContainer<T>
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000E4DD File Offset: 0x0000C6DD
		public List<T> Policies
		{
			get
			{
				return this.policies;
			}
			set
			{
				this.policies = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000E4EE File Offset: 0x0000C6EE
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			set
			{
				this.organizationId = value;
			}
		}

		// Token: 0x04000125 RID: 293
		private List<T> policies;

		// Token: 0x04000126 RID: 294
		private OrganizationId organizationId;
	}
}
