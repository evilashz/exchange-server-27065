using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000071 RID: 113
	internal class PrereqConclusionSet : ConclusionSetImplementation<PrereqConclusion, PrereqSettingConclusion, PrereqRuleConclusion>
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x00026654 File Offset: 0x00024854
		public PrereqConclusionSet()
		{
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002665C File Offset: 0x0002485C
		public PrereqConclusionSet(PrereqAnalysis analysis, PrereqConclusion root) : base(root)
		{
			this.setupModes = analysis.SetupModes;
			this.setupRoles = analysis.SetupRoles;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002667D File Offset: 0x0002487D
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00026685 File Offset: 0x00024885
		public SetupMode SetupModes
		{
			get
			{
				return this.setupModes;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.setupModes = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00026694 File Offset: 0x00024894
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0002669C File Offset: 0x0002489C
		public SetupRole SetupRoles
		{
			get
			{
				return this.setupRoles;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.setupRoles = value;
			}
		}

		// Token: 0x040005C1 RID: 1473
		private SetupMode setupModes;

		// Token: 0x040005C2 RID: 1474
		private SetupRole setupRoles;
	}
}
