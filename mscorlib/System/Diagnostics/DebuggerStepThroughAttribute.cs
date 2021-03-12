using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003BA RID: 954
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DebuggerStepThroughAttribute : Attribute
	{
		// Token: 0x060031E6 RID: 12774 RVA: 0x000C011E File Offset: 0x000BE31E
		[__DynamicallyInvokable]
		public DebuggerStepThroughAttribute()
		{
		}
	}
}
