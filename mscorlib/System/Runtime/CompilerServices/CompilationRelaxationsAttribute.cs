using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000887 RID: 2183
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x06005C88 RID: 23688 RVA: 0x00144C54 File Offset: 0x00142E54
		[__DynamicallyInvokable]
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.m_relaxations = relaxations;
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00144C63 File Offset: 0x00142E63
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.m_relaxations = (int)relaxations;
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x00144C72 File Offset: 0x00142E72
		[__DynamicallyInvokable]
		public int CompilationRelaxations
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_relaxations;
			}
		}

		// Token: 0x0400295A RID: 10586
		private int m_relaxations;
	}
}
