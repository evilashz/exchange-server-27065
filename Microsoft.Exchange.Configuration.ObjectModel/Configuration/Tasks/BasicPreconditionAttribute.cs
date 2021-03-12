using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	[Serializable]
	internal abstract class BasicPreconditionAttribute : ConditionAttribute
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public BasicPreconditionAttribute()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public BasicPreconditionAttribute(ConditionAttribute.EvaluationType evaluationStrategy)
		{
			base.EvaluationStrategy = evaluationStrategy;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000CA53 File Offset: 0x0000AC53
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000CA5B File Offset: 0x0000AC5B
		public Type Resolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x040000D8 RID: 216
		private Type resolver;
	}
}
