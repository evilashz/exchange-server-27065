using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000999 RID: 2457
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		// Token: 0x060062A3 RID: 25251 RVA: 0x0014FD8B File Offset: 0x0014DF8B
		[__DynamicallyInvokable]
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x0014FD9A File Offset: 0x0014DF9A
		[__DynamicallyInvokable]
		public Type DefaultInterface
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002C1F RID: 11295
		private Type m_defaultInterface;
	}
}
