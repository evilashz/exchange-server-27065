using System;
using System.Collections.Generic;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.ComplianceData
{
	// Token: 0x02000053 RID: 83
	public abstract class ComplianceItemContainer : IDisposable
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C7 RID: 455
		public abstract string Id { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C8 RID: 456
		public abstract bool HasItems { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C9 RID: 457
		public abstract string Template { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CA RID: 458
		public abstract bool SupportsAssociation { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CB RID: 459
		public abstract bool SupportsBinding { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006432 File Offset: 0x00004632
		public virtual int Level
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CD RID: 461
		public abstract List<ComplianceItemContainer> Ancestors { get; }

		// Token: 0x060001CE RID: 462
		public abstract void UpdatePolicy(Dictionary<PolicyScenario, List<PolicyRuleConfig>> rules);

		// Token: 0x060001CF RID: 463
		public abstract void AddPolicy(PolicyDefinitionConfig definition, PolicyRuleConfig rule);

		// Token: 0x060001D0 RID: 464
		public abstract void RemovePolicy(Guid id, PolicyScenario scenario);

		// Token: 0x060001D1 RID: 465
		public abstract bool HasPolicy(Guid policyId, PolicyScenario scenario);

		// Token: 0x060001D2 RID: 466
		public abstract void ForEachChildContainer(Action<ComplianceItemContainer> containerHandler, Func<ComplianceItemContainer, Exception, bool> exHandler);

		// Token: 0x060001D3 RID: 467
		public abstract bool SupportsPolicy(PolicyScenario scenario);

		// Token: 0x060001D4 RID: 468 RVA: 0x00006435 File Offset: 0x00004635
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001D5 RID: 469
		protected abstract void Dispose(bool disposing);
	}
}
