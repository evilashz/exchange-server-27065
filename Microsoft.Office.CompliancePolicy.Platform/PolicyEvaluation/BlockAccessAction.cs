using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000A6 RID: 166
	public class BlockAccessAction : Action
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		public BlockAccessAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000D5DF File Offset: 0x0000B7DF
		public override string Name
		{
			get
			{
				return "BlockAccess";
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000D5E6 File Offset: 0x0000B7E6
		public override Version MinimumVersion
		{
			get
			{
				return BlockAccessAction.minVersion;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		protected override ExecutionControl OnExecute(PolicyEvaluationContext context)
		{
			throw new NotImplementedException("The BlockAccessAction can only be used for PS object model serialization. Workloads must implement OnExecute.");
		}

		// Token: 0x040002B2 RID: 690
		private static readonly Version minVersion = new Version("1.00.0002.000");
	}
}
