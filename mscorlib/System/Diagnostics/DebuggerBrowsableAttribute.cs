using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003C0 RID: 960
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		// Token: 0x060031EF RID: 12783 RVA: 0x000C01A8 File Offset: 0x000BE3A8
		[__DynamicallyInvokable]
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000C01CA File Offset: 0x000BE3CA
		[__DynamicallyInvokable]
		public DebuggerBrowsableState State
		{
			[__DynamicallyInvokable]
			get
			{
				return this.state;
			}
		}

		// Token: 0x040015EC RID: 5612
		private DebuggerBrowsableState state;
	}
}
