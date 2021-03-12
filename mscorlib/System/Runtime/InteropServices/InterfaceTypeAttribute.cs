using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008E7 RID: 2279
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x06005EDA RID: 24282 RVA: 0x00146B3A File Offset: 0x00144D3A
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this._val = interfaceType;
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x00146B49 File Offset: 0x00144D49
		[__DynamicallyInvokable]
		public InterfaceTypeAttribute(short interfaceType)
		{
			this._val = (ComInterfaceType)interfaceType;
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x00146B58 File Offset: 0x00144D58
		[__DynamicallyInvokable]
		public ComInterfaceType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029AF RID: 10671
		internal ComInterfaceType _val;
	}
}
