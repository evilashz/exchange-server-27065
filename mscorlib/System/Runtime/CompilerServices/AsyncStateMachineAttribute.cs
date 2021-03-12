using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C0 RID: 2240
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AsyncStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06005CED RID: 23789 RVA: 0x00145ABE File Offset: 0x00143CBE
		[__DynamicallyInvokable]
		public AsyncStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
