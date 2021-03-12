using System;
using System.Collections.Generic;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.HygieneRules
{
	// Token: 0x02000006 RID: 6
	internal sealed class HygieneTransportRule : Rule
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002248 File Offset: 0x00000448
		public HygieneTransportRule(string name) : this(name, null)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002252 File Offset: 0x00000452
		public HygieneTransportRule(string name, Condition condition) : base(name, condition)
		{
			this.Fork = new List<BifurcationInfo>();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002267 File Offset: 0x00000467
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000226F File Offset: 0x0000046F
		public List<BifurcationInfo> Fork { get; set; }

		// Token: 0x06000012 RID: 18 RVA: 0x00002278 File Offset: 0x00000478
		public override int GetEstimatedSize()
		{
			int num = 0;
			foreach (BifurcationInfo bifurcationInfo in this.Fork)
			{
				num += bifurcationInfo.GetEstimatedSize();
			}
			return num + base.GetEstimatedSize();
		}
	}
}
