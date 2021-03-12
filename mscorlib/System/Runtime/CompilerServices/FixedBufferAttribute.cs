using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200088A RID: 2186
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class FixedBufferAttribute : Attribute
	{
		// Token: 0x06005C8D RID: 23693 RVA: 0x00144C8A File Offset: 0x00142E8A
		[__DynamicallyInvokable]
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.elementType = elementType;
			this.length = length;
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06005C8E RID: 23694 RVA: 0x00144CA0 File Offset: 0x00142EA0
		[__DynamicallyInvokable]
		public Type ElementType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x00144CA8 File Offset: 0x00142EA8
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.length;
			}
		}

		// Token: 0x0400295B RID: 10587
		private Type elementType;

		// Token: 0x0400295C RID: 10588
		private int length;
	}
}
