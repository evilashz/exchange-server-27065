using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B3 RID: 1459
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DefaultMemberAttribute : Attribute
	{
		// Token: 0x06004493 RID: 17555 RVA: 0x000FC7FA File Offset: 0x000FA9FA
		[__DynamicallyInvokable]
		public DefaultMemberAttribute(string memberName)
		{
			this.m_memberName = memberName;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000FC809 File Offset: 0x000FAA09
		[__DynamicallyInvokable]
		public string MemberName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_memberName;
			}
		}

		// Token: 0x04001BD8 RID: 7128
		private string m_memberName;
	}
}
