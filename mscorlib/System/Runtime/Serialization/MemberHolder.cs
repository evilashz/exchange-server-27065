using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200070B RID: 1803
	[Serializable]
	internal class MemberHolder
	{
		// Token: 0x060050A7 RID: 20647 RVA: 0x0011B2E1 File Offset: 0x001194E1
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this.memberType = type;
			this.context = ctx;
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x0011B2F7 File Offset: 0x001194F7
		public override int GetHashCode()
		{
			return this.memberType.GetHashCode();
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0011B304 File Offset: 0x00119504
		public override bool Equals(object obj)
		{
			if (!(obj is MemberHolder))
			{
				return false;
			}
			MemberHolder memberHolder = (MemberHolder)obj;
			return memberHolder.memberType == this.memberType && memberHolder.context.State == this.context.State;
		}

		// Token: 0x04002385 RID: 9093
		internal MemberInfo[] members;

		// Token: 0x04002386 RID: 9094
		internal Type memberType;

		// Token: 0x04002387 RID: 9095
		internal StreamingContext context;
	}
}
