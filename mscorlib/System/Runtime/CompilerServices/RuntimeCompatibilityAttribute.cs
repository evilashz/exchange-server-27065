using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B6 RID: 2230
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x06005CC4 RID: 23748 RVA: 0x00144F25 File Offset: 0x00143125
		[__DynamicallyInvokable]
		public RuntimeCompatibilityAttribute()
		{
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005CC5 RID: 23749 RVA: 0x00144F2D File Offset: 0x0014312D
		// (set) Token: 0x06005CC6 RID: 23750 RVA: 0x00144F35 File Offset: 0x00143135
		[__DynamicallyInvokable]
		public bool WrapNonExceptionThrows
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_wrapNonExceptionThrows;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_wrapNonExceptionThrows = value;
			}
		}

		// Token: 0x04002981 RID: 10625
		private bool m_wrapNonExceptionThrows;
	}
}
