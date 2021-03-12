using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020009C0 RID: 2496
	[Serializable]
	internal class OverBudgetException : LocalizedException
	{
		// Token: 0x060073BC RID: 29628 RVA: 0x0017D60C File Offset: 0x0017B80C
		public OverBudgetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.owner = info.GetString("owner");
				this.policyPart = info.GetString("policyPart");
				this.isServiceAccountBudget = info.GetBoolean("IsServiceAccountBudget");
				this.throttlingPolicyDN = info.GetString("throttlingPolicyDN");
				this.budgetType = (BudgetType)info.GetValue("budgetType", typeof(BudgetType));
				this.backoffTime = info.GetInt32("backoffTime");
				this.snapshot = info.GetString("snapshot");
				this.policyValue = info.GetString("policyValue");
			}
		}

		// Token: 0x060073BD RID: 29629 RVA: 0x0017D6C0 File Offset: 0x0017B8C0
		public OverBudgetException(Budget budget, string policyPart, string policyValue, int backoffTime) : base(DirectoryStrings.ExceptionOverBudget(policyPart, policyValue, budget.Owner.BudgetType, backoffTime))
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			this.Initialize(budget.Owner, budget.ThrottlingPolicy.FullPolicy, policyPart, policyValue, backoffTime, budget.ToString());
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x0017D718 File Offset: 0x0017B918
		public OverBudgetException(IBudget budget, string policyPart, string policyValue, int backoffTime) : base(DirectoryStrings.ExceptionOverBudget(policyPart, policyValue, budget.Owner.BudgetType, backoffTime))
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			this.Initialize(budget.Owner, budget.ThrottlingPolicy, policyPart, policyValue, backoffTime, budget.ToString());
		}

		// Token: 0x060073BF RID: 29631 RVA: 0x0017D769 File Offset: 0x0017B969
		public OverBudgetException(LocalizedString errorMessage, IBudget budget, string policyPart, string policyValue, int backoffTime) : base(errorMessage)
		{
			if (budget == null)
			{
				throw new ArgumentNullException("budget");
			}
			this.Initialize(budget.Owner, budget.ThrottlingPolicy, policyPart, policyValue, backoffTime, budget.ToString());
		}

		// Token: 0x060073C0 RID: 29632 RVA: 0x0017D7A0 File Offset: 0x0017B9A0
		private void Initialize(BudgetKey owner, IThrottlingPolicy policy, string policyPart, string policyValue, int backoffTime, string snapshot)
		{
			this.owner = owner.ToString();
			this.isServiceAccountBudget = owner.IsServiceAccountBudget;
			this.throttlingPolicyDN = policy.GetIdentityString();
			this.budgetType = owner.BudgetType;
			this.policyPart = policyPart;
			this.policyValue = policyValue;
			this.backoffTime = backoffTime;
			this.snapshot = snapshot;
			ThrottlingPerfCounterWrapper.IncrementOverBudget(owner, TimeSpan.FromMilliseconds((double)backoffTime));
			WorkloadManagementLogger.SetOverBudget(policyPart, policyValue, null);
			WorkloadManagementLogger.SetBudgetType(owner.BudgetType.ToString(), null);
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x0017D82B File Offset: 0x0017BA2B
		public OverBudgetException() : base(DirectoryStrings.ExceptionOverBudget("no part", "no policy", BudgetType.Ews, 0))
		{
		}

		// Token: 0x060073C2 RID: 29634 RVA: 0x0017D844 File Offset: 0x0017BA44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("owner", this.owner);
				info.AddValue("policyPart", this.policyPart);
				info.AddValue("IsServiceAccountBudget", this.isServiceAccountBudget);
				info.AddValue("throttlingPolicyDN", this.throttlingPolicyDN);
				info.AddValue("budgetType", this.budgetType);
				info.AddValue("backoffTime", this.backoffTime);
				info.AddValue("snapshot", this.snapshot);
				info.AddValue("policyValue", this.policyValue);
			}
		}

		// Token: 0x1700294B RID: 10571
		// (get) Token: 0x060073C3 RID: 29635 RVA: 0x0017D8EC File Offset: 0x0017BAEC
		public string ThrottlingPolicyDN
		{
			get
			{
				return this.throttlingPolicyDN;
			}
		}

		// Token: 0x1700294C RID: 10572
		// (get) Token: 0x060073C4 RID: 29636 RVA: 0x0017D8F4 File Offset: 0x0017BAF4
		public BudgetType BudgetType
		{
			get
			{
				return this.budgetType;
			}
		}

		// Token: 0x1700294D RID: 10573
		// (get) Token: 0x060073C5 RID: 29637 RVA: 0x0017D8FC File Offset: 0x0017BAFC
		public string Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x1700294E RID: 10574
		// (get) Token: 0x060073C6 RID: 29638 RVA: 0x0017D904 File Offset: 0x0017BB04
		public string PolicyPart
		{
			get
			{
				return this.policyPart;
			}
		}

		// Token: 0x1700294F RID: 10575
		// (get) Token: 0x060073C7 RID: 29639 RVA: 0x0017D90C File Offset: 0x0017BB0C
		public string PolicyValue
		{
			get
			{
				return this.policyValue;
			}
		}

		// Token: 0x17002950 RID: 10576
		// (get) Token: 0x060073C8 RID: 29640 RVA: 0x0017D914 File Offset: 0x0017BB14
		public string Snapshot
		{
			get
			{
				return this.snapshot;
			}
		}

		// Token: 0x17002951 RID: 10577
		// (get) Token: 0x060073C9 RID: 29641 RVA: 0x0017D91C File Offset: 0x0017BB1C
		public bool IsServiceAccountBudget
		{
			get
			{
				return this.isServiceAccountBudget;
			}
		}

		// Token: 0x17002952 RID: 10578
		// (get) Token: 0x060073CA RID: 29642 RVA: 0x0017D924 File Offset: 0x0017BB24
		public int BackoffTime
		{
			get
			{
				return this.backoffTime;
			}
		}

		// Token: 0x04004AB6 RID: 19126
		private const string OwnerField = "owner";

		// Token: 0x04004AB7 RID: 19127
		private const string PolicyPartField = "policyPart";

		// Token: 0x04004AB8 RID: 19128
		private const string IsServiceAccountBudgetField = "IsServiceAccountBudget";

		// Token: 0x04004AB9 RID: 19129
		private const string ThrottlingPolicyDNField = "throttlingPolicyDN";

		// Token: 0x04004ABA RID: 19130
		private const string BudgetTypeField = "budgetType";

		// Token: 0x04004ABB RID: 19131
		private const string BackOffTimeField = "backoffTime";

		// Token: 0x04004ABC RID: 19132
		private const string SnapshotField = "snapshot";

		// Token: 0x04004ABD RID: 19133
		private const string PolicyValueField = "policyValue";

		// Token: 0x04004ABE RID: 19134
		private string owner;

		// Token: 0x04004ABF RID: 19135
		private string policyPart;

		// Token: 0x04004AC0 RID: 19136
		private bool isServiceAccountBudget;

		// Token: 0x04004AC1 RID: 19137
		private string throttlingPolicyDN;

		// Token: 0x04004AC2 RID: 19138
		private BudgetType budgetType;

		// Token: 0x04004AC3 RID: 19139
		private int backoffTime;

		// Token: 0x04004AC4 RID: 19140
		private string snapshot;

		// Token: 0x04004AC5 RID: 19141
		private string policyValue;
	}
}
