using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000594 RID: 1428
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class AssemblyDelaySignAttribute : Attribute
	{
		// Token: 0x0600434C RID: 17228 RVA: 0x000F7AA2 File Offset: 0x000F5CA2
		[__DynamicallyInvokable]
		public AssemblyDelaySignAttribute(bool delaySign)
		{
			this.m_delaySign = delaySign;
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x000F7AB1 File Offset: 0x000F5CB1
		[__DynamicallyInvokable]
		public bool DelaySign
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_delaySign;
			}
		}

		// Token: 0x04001B4F RID: 6991
		private bool m_delaySign;
	}
}
