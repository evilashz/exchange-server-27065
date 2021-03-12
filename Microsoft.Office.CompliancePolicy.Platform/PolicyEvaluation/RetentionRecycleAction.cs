using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000AD RID: 173
	public class RetentionRecycleAction : Action
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0000DBFF File Offset: 0x0000BDFF
		internal RetentionRecycleAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000DC09 File Offset: 0x0000BE09
		public override Type[] ArgumentsType
		{
			get
			{
				return RetentionRecycleAction.argumentTypes;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public override string Name
		{
			get
			{
				return "RetentionRecycle";
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DC17 File Offset: 0x0000BE17
		protected override ExecutionControl OnExecute(PolicyEvaluationContext context)
		{
			throw new NotImplementedException("The RetentionRecycle can only be used for PS object model serialization. Workloads must implement OnExecute.");
		}

		// Token: 0x040002C6 RID: 710
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
