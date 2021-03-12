using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008E8 RID: 2280
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06005EDD RID: 24285 RVA: 0x00146B60 File Offset: 0x00144D60
		[__DynamicallyInvokable]
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._val = defaultInterface;
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x00146B6F File Offset: 0x00144D6F
		[__DynamicallyInvokable]
		public Type Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029B0 RID: 10672
		internal Type _val;
	}
}
