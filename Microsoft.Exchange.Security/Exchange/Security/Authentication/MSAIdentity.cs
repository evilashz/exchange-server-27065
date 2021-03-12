using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000084 RID: 132
	internal class MSAIdentity : GenericIdentity
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000253B0 File Offset: 0x000235B0
		public string MemberName
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000253B8 File Offset: 0x000235B8
		public MSAIdentity(string netId, string memberName) : base(memberName, "MSA")
		{
			this.member = memberName;
			this.netId = netId;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000253D4 File Offset: 0x000235D4
		public IStandardBudget AcquireBudget(BudgetType budgetType)
		{
			BudgetKey budgetKey = new StringBudgetKey(this.MemberName, false, budgetType);
			return StandardBudget.Acquire(budgetKey);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000253F5 File Offset: 0x000235F5
		public string NetId
		{
			get
			{
				return this.netId;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x000253FD File Offset: 0x000235FD
		public override string Name
		{
			get
			{
				return this.MemberName;
			}
		}

		// Token: 0x04000503 RID: 1283
		private const string MSAAuthenticationType = "MSA";

		// Token: 0x04000504 RID: 1284
		private readonly string member;

		// Token: 0x04000505 RID: 1285
		private readonly string netId;
	}
}
