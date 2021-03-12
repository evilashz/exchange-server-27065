using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class CLSCompliantAttribute : Attribute
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x00022B53 File Offset: 0x00020D53
		[__DynamicallyInvokable]
		public CLSCompliantAttribute(bool isCompliant)
		{
			this.m_compliant = isCompliant;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00022B62 File Offset: 0x00020D62
		[__DynamicallyInvokable]
		public bool IsCompliant
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_compliant;
			}
		}

		// Token: 0x0400045F RID: 1119
		private bool m_compliant;
	}
}
