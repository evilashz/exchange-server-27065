using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000AC RID: 172
	public class RetentionExpireAction : Action
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		internal RetentionExpireAction(List<Argument> arguments, string externalName = null) : base(arguments, externalName)
		{
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		public override string Name
		{
			get
			{
				return "RetentionExpire";
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000DBC5 File Offset: 0x0000BDC5
		public override Type[] ArgumentsType
		{
			get
			{
				return RetentionExpireAction.argumentTypes;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		protected override ExecutionControl OnExecute(PolicyEvaluationContext context)
		{
			throw new NotImplementedException("The RetentionExpireAction can only be used for PS object model serialization. Workloads must implement OnExecute.");
		}

		// Token: 0x040002C5 RID: 709
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
