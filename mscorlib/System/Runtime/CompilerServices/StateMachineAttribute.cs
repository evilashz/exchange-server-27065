using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BD RID: 2237
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StateMachineAttribute : Attribute
	{
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x00145A95 File Offset: 0x00143C95
		// (set) Token: 0x06005CE8 RID: 23784 RVA: 0x00145A9D File Offset: 0x00143C9D
		[__DynamicallyInvokable]
		public Type StateMachineType { [__DynamicallyInvokable] get; private set; }

		// Token: 0x06005CE9 RID: 23785 RVA: 0x00145AA6 File Offset: 0x00143CA6
		[__DynamicallyInvokable]
		public StateMachineAttribute(Type stateMachineType)
		{
			this.StateMachineType = stateMachineType;
		}
	}
}
