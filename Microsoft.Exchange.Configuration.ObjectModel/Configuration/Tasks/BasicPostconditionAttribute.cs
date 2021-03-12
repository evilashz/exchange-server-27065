using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal abstract class BasicPostconditionAttribute : ConditionAttribute
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public BasicPostconditionAttribute()
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public BasicPostconditionAttribute(ConditionAttribute.EvaluationType evaluationStrategy)
		{
			base.EvaluationStrategy = evaluationStrategy;
		}
	}
}
