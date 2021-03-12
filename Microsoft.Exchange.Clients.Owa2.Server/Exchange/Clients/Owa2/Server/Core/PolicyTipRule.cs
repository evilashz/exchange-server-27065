using System;
using System.Collections.Generic;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200021C RID: 540
	internal sealed class PolicyTipRule : Rule
	{
		// Token: 0x060014C1 RID: 5313 RVA: 0x00049C56 File Offset: 0x00047E56
		public PolicyTipRule(string name) : base(name)
		{
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00049C5F File Offset: 0x00047E5F
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x00049C67 File Offset: 0x00047E67
		public List<Condition> ForkConditions
		{
			get
			{
				return this.forkConditions;
			}
			set
			{
				this.forkConditions = value;
			}
		}

		// Token: 0x04000B42 RID: 2882
		private List<Condition> forkConditions;
	}
}
