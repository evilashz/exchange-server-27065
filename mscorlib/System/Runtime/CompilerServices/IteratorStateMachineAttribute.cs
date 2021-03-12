using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BE RID: 2238
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class IteratorStateMachineAttribute : StateMachineAttribute
	{
		// Token: 0x06005CEA RID: 23786 RVA: 0x00145AB5 File Offset: 0x00143CB5
		[__DynamicallyInvokable]
		public IteratorStateMachineAttribute(Type stateMachineType) : base(stateMachineType)
		{
		}
	}
}
